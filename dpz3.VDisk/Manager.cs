using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.VDisk {

    /// <summary>
    /// 虚拟磁盘管理器
    /// </summary>
    public class Manager : dpz3.Object {

        // 数据大小定义
        //private const int Path_Info_Size = 1024;
        private const int Path_Name_Size = 256;
        private const int Data_Info_Size = 4096;
        private const int Data_Size = Data_Info_Size - 6;

        // 路径类型
        private const byte Path_Type_Dir = 0x10;
        private const byte Path_Type_Root = 0x11;
        private const byte Path_Type_File = 0x20;

        // 工作目录
        private string workPath;

        // 磁盘信息
        private Types.VirtualDiskInfo diskInfo;

        // 从整型获取字节数组
        private byte[] GetBytesFromInt(int val) {
            return new byte[] { (byte)(val % 256), (byte)(val / 256) };
        }

        // 从长整型获取字节数组
        private byte[] GetBytesFromLong(long val) {
            return new byte[] { (byte)(val % 256), (byte)(val / 256 % 256), (byte)(val / (256 ^ 2) % 256), (byte)(val / (256 ^ 3)) };
        }

        // 保存磁盘信息
        private void SaveDiskInfo() {

            using (dpz3.File.BinaryFile file = new File.BinaryFile(workPath, System.IO.FileMode.OpenOrCreate)) {
                file.Position = 0;

                // 输出Sign
                file.Write(diskInfo.Sign);

                // 输出Version
                file.Write(GetBytesFromInt(diskInfo.Version));

                // 输出DataBlocks
                file.Write(GetBytesFromLong(diskInfo.DataBlocks));
            }
        }

        // 加载磁盘信息
        private void LoadDiskInfo() {
            diskInfo = new Types.VirtualDiskInfo();
            diskInfo.Sign = new byte[4];

            // 判断文件是否存在，不存在则创建
            if (System.IO.File.Exists(workPath)) {
                using (dpz3.File.BinaryFile file = new File.BinaryFile(workPath, System.IO.FileMode.Open)) {
                    file.Position = 0;

                    // 读取Sign
                    if (!file.IsEnd) diskInfo.Sign[0] = file.Read();
                    if (!file.IsEnd) diskInfo.Sign[1] = file.Read();
                    if (!file.IsEnd) diskInfo.Sign[2] = file.Read();
                    if (!file.IsEnd) diskInfo.Sign[3] = file.Read();

                    if (System.Text.Encoding.ASCII.GetString(diskInfo.Sign) != "dvdk") throw new Exception("磁盘数据损坏");

                    // 读取Version
                    if (!file.IsEnd) diskInfo.Version = file.Read();
                    if (!file.IsEnd) diskInfo.Version += file.Read() * 256;

                    // 读取DataBlocks
                    if (!file.IsEnd) diskInfo.DataBlocks = file.Read();
                    if (!file.IsEnd) diskInfo.DataBlocks += file.Read() * 256;
                    if (!file.IsEnd) diskInfo.DataBlocks += file.Read() * 256 * 256;
                    if (!file.IsEnd) diskInfo.DataBlocks += file.Read() * 256 * 256 * 256;
                }
            } else {
                diskInfo.Sign[0] = (byte)'d';
                diskInfo.Sign[1] = (byte)'v';
                diskInfo.Sign[2] = (byte)'d';
                diskInfo.Sign[3] = (byte)'k';

                diskInfo.Version = 1;
                diskInfo.DataBlocks = 0;

                // 保存信息
                SaveDiskInfo();

            }


        }

        /// <summary>
        /// 实例化一个虚拟磁盘管理器
        /// </summary>
        /// <param name="path"></param>
        public Manager(string path) {

            // 指定工作路径
            workPath = path;

            // 加载磁盘信息
            LoadDiskInfo();
        }

        /// <summary>
        /// 获取父路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetParentFolder(string path) {
            if (path.IsNoneOrNull()) throw new Exception("尚未父路径");
            if (path == "/") return null;

            string[] pathes = path.Split('/');
            if (pathes.Length <= 1) throw new Exception($"\"{path}\"不是有效路径");
            if (pathes[0] != "") throw new Exception($"\"{path}\"不是有效路径");

            // 拼接父路径
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < pathes.Length - 1; i++) {
                sb.AppendFormat("/{0}", pathes[i]);
            }
            if (sb.Length <= 0) sb.Append("/");
            return sb.ToString();
        }

        /// <summary>
        /// 获取路径中的名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetName(string path) {
            if (path.IsNoneOrNull()) throw new Exception("尚未父路径");
            if (path == "/") return null;

            string[] pathes = path.Split('/');
            if (pathes.Length <= 1) throw new Exception($"\"{path}\"不是有效路径");
            if (pathes[0] != "") throw new Exception($"\"{path}\"不是有效路径");

            return pathes[pathes.Length - 1];
        }

        // 保存路径信息
        private void SavePathInfo(Types.VirtualPathInfo info) {

            using (dpz3.File.BinaryFile file = new File.BinaryFile(workPath, System.IO.FileMode.OpenOrCreate)) {
                file.Position = info.Position * Data_Info_Size;

                // 输出NextPosition
                file.Write(GetBytesFromLong(info.NextPosition));

                // 输出FirstChildPosition
                file.Write(GetBytesFromLong(info.FirstChildPosition));

                // 读取Type
                file.Write(new byte[] { info.Type });

                // 读取Name
                file.Write(info.Name);
            }
        }

        // 获取路径信息
        private Types.VirtualPathInfo GetPathInfo(long pos) {

            Types.VirtualPathInfo res = new Types.VirtualPathInfo();
            res.Position = pos;

            using (dpz3.File.BinaryFile file = new File.BinaryFile(workPath, System.IO.FileMode.OpenOrCreate)) {
                file.Position = pos * Data_Info_Size;

                // 读取Sign
                try {

                    // 读取NextPosition
                    res.NextPosition = file.Read();
                    res.NextPosition += file.Read() * 256;
                    res.NextPosition += file.Read() * 256 * 256;
                    res.NextPosition += file.Read() * 256 * 256 * 256;

                    // 读取FirstChildPosition
                    res.FirstChildPosition = file.Read();
                    res.FirstChildPosition += file.Read() * 256;
                    res.FirstChildPosition += file.Read() * 256 * 256;
                    res.FirstChildPosition += file.Read() * 256 * 256 * 256;

                    // 读取Type
                    res.Type = file.Read();

                    // 读取Name
                    res.Name = file.Read(Path_Name_Size);

                } catch (Exception ex) {
                    throw new Exception("磁盘数据损坏", ex);
                }
            }

            return res;
        }

        // 获取路径寻址
        private Types.VirtualPathInfo GetPathInfo(Types.VirtualPathInfo parent, string name) {

            long pos = parent.FirstChildPosition;

            while (pos > 0) {

                // 读取对应路径的路径信息
                var info = GetPathInfo(pos);

                // 生成名称
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < Path_Name_Size; i += 2) {

                    int code = info.Name[i] + info.Name[i + 1] * 256;

                    if (code > 0) {
                        sb.Append((char)code);
                    } else {
                        break;
                    }
                }
                string pName = sb.ToString();

                // 判断名称是否匹配
                if (pName == name) {
                    return info;
                } else {
                    // 对比下一个路径
                    pos = info.NextPosition;
                }
            }

            return new Types.VirtualPathInfo();
        }

        // 获取路径信息
        private Types.VirtualPathInfo GetPathInfo(string path) {

            // 判断路径是否为空
            if (path.IsNoneOrNull()) throw new Exception("尚未指定父路径");

            // 定义路径信息
            Types.VirtualPathInfo info = new Types.VirtualPathInfo();
            info.Position = 0;
            info.Type = Path_Type_Root;
            info.FirstChildPosition = 1;

            // 当路径为根路径时，直接返回一个Root的特有信息
            if (path == "/") {
                return info;
            }

            // 获取所有的子路径
            string parentPath = GetParentFolder(path);
            var parentInfo = GetPathInfo(parentPath);

            // 当类型为空时，表示路径不存在，则直接返回空信息
            if (parentInfo.Type == 0) return parentInfo;
            if ((parentInfo.Type & Path_Type_File) == Path_Type_File) throw new Exception($"路径\"{parentPath}\"不是目录");

            // 获取路径名称信息
            string name = GetName(path);
            return GetPathInfo(parentInfo, name);
        }

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool FileExist(string path) {
            var info = GetPathInfo(path);
            return (info.Type & Path_Type_File) == Path_Type_File;
        }

        /// <summary>
        /// 判断文件夹是否存在
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public bool FolderExist(string path) {
            var info = GetPathInfo(path);
            return (info.Type & Path_Type_Dir) == Path_Type_Dir;
        }

        // 添加一个所属记录
        private void AddToParentPath(Types.VirtualPathInfo parent, long posInfo) {

            // 父对象为根目录，且当前位置为1时，不保存直接退出
            if (parent.Type == Path_Type_Root && posInfo == 1) return;

            // 指针移向子路径
            long pos = parent.FirstChildPosition;

            // 当父路径中无子路径时，直接设置FirstChildPosition
            if (pos == 0) {
                parent.FirstChildPosition = posInfo;
                SavePathInfo(parent);
                return;
            }

            while (pos > 0) {

                // 读取对应路径的路径信息
                var info = GetPathInfo(pos);

                // 判断是否为最后一个子目录
                pos = info.NextPosition;
                if (info.NextPosition == 0) {
                    info.NextPosition = posInfo;
                    SavePathInfo(info);
                }
            }

        }

        /// <summary>
        /// 创建一个目录
        /// </summary>
        /// <param name="path"></param>
        public void CreateFolder(string path) {

            // 判断路径是否为空
            if (path.IsNoneOrNull()) throw new Exception("尚未指定路径");

            // 当路径为根路径时，直接返回一个Root的特有信息
            if (path == "/") throw new Exception($"\"{path}\"不是有效路径");

            // 获取所有的子路径
            string parentPath = GetParentFolder(path);
            var parentInfo = GetPathInfo(parentPath);

            if ((parentInfo.Type & Path_Type_File) == Path_Type_File) throw new Exception($"路径\"{parentPath}\"不是目录");

            // 当类型为空时，表示路径不存在，则进行创建
            if (parentInfo.Type == 0) {
                CreateFolder(parentPath);
                parentInfo = GetPathInfo(parentPath);
            }

            // 获取路径名称信息
            string name = GetName(path);
            var info = GetPathInfo(parentInfo, name);

            if (info.Type != 0) throw new Exception($"路径已经存在");

            // 路径区块增加
            diskInfo.DataBlocks++;
            info.Position = diskInfo.DataBlocks;

            // 保存磁盘信息
            SaveDiskInfo();

            // 创建一个目录
            info.NextPosition = 0;
            info.Name = new byte[Path_Name_Size];
            info.Type = Path_Type_Dir;
            byte[] bsName = System.Text.Encoding.Unicode.GetBytes(name);
            Array.Copy(bsName, 0, info.Name, 0, bsName.Length);

            // 保存路径信息
            SavePathInfo(info);

            // 向父路径添加寻址
            AddToParentPath(parentInfo, info.Position);

        }

        /// <summary>
        /// 创建一个文件
        /// </summary>
        /// <param name="path"></param>
        public void CreateFile(string path) {

            // 判断路径是否为空
            if (path.IsNoneOrNull()) throw new Exception("尚未指定路径");

            // 获取所有的子路径
            string parentPath = GetParentFolder(path);
            var parentInfo = GetPathInfo(parentPath);

            if ((parentInfo.Type & Path_Type_File) == Path_Type_File) throw new Exception($"路径\"{parentPath}\"不是目录");

            // 当类型为空时，表示路径不存在，则进行创建
            if (parentInfo.Type == 0) throw new Exception($"路径\"{parentPath}\"不存在");

            // 获取路径名称信息
            string name = GetName(path);
            var info = GetPathInfo(parentInfo, name);

            if (info.Type != 0) throw new Exception($"路径已经存在");

            // 路径区块增加
            diskInfo.DataBlocks++;
            info.Position = diskInfo.DataBlocks;

            // 保存磁盘信息
            SaveDiskInfo();

            // 创建一个目录
            info.NextPosition = 0;
            info.Name = new byte[Path_Name_Size];
            info.Type = Path_Type_File;
            byte[] bsName = System.Text.Encoding.Unicode.GetBytes(name);
            Array.Copy(bsName, 0, info.Name, 0, bsName.Length);

            // 保存路径信息
            SavePathInfo(info);

            // 向父路径添加寻址
            AddToParentPath(parentInfo, info.Position);
        }

        // 获取数据信息
        private Types.VirtualDataInfo GetDataInfo(long pos) {
            Types.VirtualDataInfo res = new Types.VirtualDataInfo();
            res.Position = pos;

            using (dpz3.File.BinaryFile file = new File.BinaryFile(workPath, System.IO.FileMode.OpenOrCreate)) {
                file.Position = pos * Data_Info_Size;

                // 读取Sign
                try {

                    // 读取NextPosition
                    res.NextPosition = file.Read();
                    res.NextPosition += file.Read() * 256;
                    res.NextPosition += file.Read() * 256 * 256;
                    res.NextPosition += file.Read() * 256 * 256 * 256;

                    // 读取FirstChildPosition
                    res.Length = file.Read();
                    res.Length += file.Read() * 256;

                } catch (Exception ex) {
                    throw new Exception("磁盘数据损坏", ex);
                }
            }

            return res;
        }

        // 获取数据信息
        private void ReadData(ref Types.VirtualDataInfo info) {

            using (dpz3.File.BinaryFile file = new File.BinaryFile(workPath, System.IO.FileMode.OpenOrCreate)) {
                file.Position = info.Position * Data_Info_Size + 6;

                // 读取Sign
                try {

                    // 读取Data
                    info.Data = file.Read(info.Length);

                } catch (Exception ex) {
                    throw new Exception("磁盘数据损坏", ex);
                }
            }

        }

        // 保存数据信息
        private void SaveDataInfo(Types.VirtualDataInfo info) {

            using (dpz3.File.BinaryFile file = new File.BinaryFile(workPath, System.IO.FileMode.OpenOrCreate)) {
                file.Position = info.Position * Data_Info_Size;

                // 读取Sign
                try {

                    // 输出NextPosition
                    file.Write(GetBytesFromLong(info.NextPosition));

                    // 输出Length
                    file.Write(GetBytesFromInt(info.Length));

                } catch (Exception ex) {
                    throw new Exception("磁盘数据损坏", ex);
                }
            }

        }

        // 保存数据
        private void SaveData(Types.VirtualDataInfo info) {

            using (dpz3.File.BinaryFile file = new File.BinaryFile(workPath, System.IO.FileMode.OpenOrCreate)) {
                file.Position = info.Position * Data_Info_Size;

                // 读取Sign
                try {

                    // 输出NextPosition
                    file.Write(GetBytesFromLong(info.NextPosition));

                    // 输出Length
                    file.Write(GetBytesFromInt(info.Length));

                    // 输出Data
                    file.Write(info.Data);

                } catch (Exception ex) {
                    throw new Exception("磁盘数据损坏", ex);
                }
            }

        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="bytes"></param>
        public void SaveFile(string path, byte[] bytes) {

            // 获取文件信息
            var info = GetPathInfo(path);

            // 判断路径类型
            if ((info.Type & Path_Type_Dir) == Path_Type_Dir) throw new Exception($"\"{path}\"是文件夹");

            // 文件不存在，则创建
            if (info.Type == 0) {
                CreateFile(path);
                info = GetPathInfo(path);
            }

            int idx = 0;

            var pos = info.FirstChildPosition;
            Types.VirtualDataInfo dInfo = new Types.VirtualDataInfo();

            // 覆盖原有的数据段
            while (pos > 0) {
                // 读取数据段
                dInfo = GetDataInfo(pos);

                // 判断可写入长度
                int len = bytes.Length - idx;
                if (len > Data_Size) len = Data_Size;

                // 拷贝数据
                if (len > 0) {
                    dInfo.Data = new byte[len];
                    Array.Copy(bytes, idx, dInfo.Data, 0, len);
                    idx += len;
                } else {
                    dInfo.Data = new byte[0];
                }

                // 转移到下一个数据块
                pos = dInfo.NextPosition;

                // 保存数据段
                dInfo.Length = len;

                // 清理寻址在文件变小后可以降低文件的寻址数量，但是当文件忽大忽小时，会产生冗余存储块，不建议清理
                //if (dInfo.Length < Data_Info_Size - 6) dInfo.NextPosition = 0;
                SaveData(dInfo);
            }

            // 循环添加数据段
            while (idx < bytes.Length) {

                Types.VirtualDataInfo newInfo = new Types.VirtualDataInfo();
                newInfo.NextPosition = 0;

                // 判断可写入长度
                int len = bytes.Length - idx;
                if (len > Data_Size) len = Data_Size;

                // 拷贝数据
                newInfo.Data = new byte[len];
                Array.Copy(bytes, idx, newInfo.Data, 0, len);
                idx += len;

                // 数据区块增加
                diskInfo.DataBlocks++;
                newInfo.Position = diskInfo.DataBlocks;
                newInfo.Length = len;

                // 保存磁盘信息
                SaveDiskInfo();

                // 保存数据段
                SaveData(newInfo);

                // 向文件信息或上一个数据段增加
                if (info.FirstChildPosition == 0) {
                    info.FirstChildPosition = newInfo.Position;
                    SavePathInfo(info);
                } else {
                    dInfo.NextPosition = newInfo.Position;
                    SaveDataInfo(dInfo);
                }

                // 将当前段填充为缓存段
                dInfo = newInfo;
            }
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public byte[] ReadFile(string path) {
            List<byte> list = new List<byte>();

            // 获取文件信息
            var info = GetPathInfo(path);

            // 判断路径类型
            if ((info.Type & Path_Type_File) != Path_Type_File) throw new Exception($"\"{path}\"不是文件");

            var pos = info.FirstChildPosition;

            while (pos > 0) {
                // 读取数据段
                var dInfo = GetDataInfo(pos);
                ReadData(ref dInfo);
                list.AddRange(dInfo.Data);

                // 转移到下一个数据块
                // 为兼容冗余存储块，当读取的存储数据不为完整数据块时，即认为之后为冗余存储块，不再进行读取
                if (dInfo.Length == Data_Size) {
                    pos = dInfo.NextPosition;
                } else {
                    pos = 0;
                }
            }

            return list.ToArray();
        }

        /// <summary>
        /// 获取子路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public List<string> GetPathes(string path) {

            // 建立列表信息
            List<string> list = new List<string>();

            // 获取路径信息
            var parent = GetPathInfo(path);

            // 根目录或是
            if ((parent.Type & Path_Type_Dir) == Path_Type_Dir) {
                long pos = parent.FirstChildPosition;

                while (pos > 0) {

                    // 读取对应路径的路径信息
                    var info = GetPathInfo(pos);

                    // 生成名称
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < Path_Name_Size; i += 2) {
                        int code = info.Name[i] + info.Name[i + 1] * 256;
                        if (code > 0) {
                            sb.Append((char)code);
                        } else {
                            break;
                        }
                    }
                    string pName = sb.ToString();

                    if (path.EndsWith("/")) {
                        list.Add(path + pName);
                    } else {
                        list.Add(path + "/" + pName);
                    }

                    // 转向下一个路径
                    pos = info.NextPosition;
                }
            } else {
                throw new Exception($"\"{path}\"不存在或不为文件夹");
            }

            return list;
        }

    }
}
