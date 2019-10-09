using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Markdown {

    /// <summary>
    /// Markdown
    /// </summary>
    public static class Parser {

        // 空操作，等待定义操作符
        private const int Parse_Null = 0x00;

        // 文本相关
        private const int Parse_Text = 0x10;
        private const int Parse_Text_Bold = 0x11;
        private const int Parse_Text_Italic = 0x12;
        private const int Parse_Text_BoldItalic = 0x13;
        private const int Parse_Text_Code = 0x14;
        private const int Parse_Text_Link_Name = 0x15;
        private const int Parse_Text_Link_Url = 0x16;
        private const int Parse_Text_Finish = 0x1F;

        // 标题
        private const int Parse_Title = 0x20;

        // 列表
        private const int Parse_List = 0x30;
        private const int Parse_List_Unordered = 0x31;
        private const int Parse_List_Ordered = 0x32;
        private const int Parse_List_Finish = 0x3F;

        // 区块
        private const int Parse_Block = 0x40;
        private const int Parse_Block_Finish = 0x4F;

        // 代码
        private const int Parse_Code = 0x50;
        private const int Parse_Code_Line = 0x51;
        private const int Parse_Code_Block = 0x52;

        // 表格
        private const int Parse_Table = 0x60;
        private const int Parse_Table_Header = 0x61;
        private const int Parse_Table_Align = 0x62;
        private const int Parse_Table_Data = 0x63;

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static MdDocument GetDocument(string str) {

            MdDocument doc = new MdDocument();

            // 当前操作的区块
            List<MdBlockBasic> block = new List<MdBlockBasic>();

            // 初始化字符串
            int lv = 0;
            StringBuilder sb = new StringBuilder();

            // 初始化字符串和转义标志
            bool isEscape = false;

            // 初始化操作类型为空操作
            int pt = Parse_Null;

            // 行列计数器
            int line = 1;
            int col = 0;

            //try {

            //遍历字符串进行解析
            for (int i = 0; i < str.Length; i++) {
                col++;
                char chr = str[i];
                switch (chr) {
                    case '\\':
                        #region [=====处理转义符=====]
                        // 当处于字符串模式时，操作转义
                        if (isEscape) {
                            // 当处于转义模式，则直接添加字符并退出转义
                            sb.Append(chr);
                            isEscape = false;
                        } else {
                            // 当未处于转义模式，则进入转义模式
                            if (pt == Parse_Code_Line || pt == Parse_Code_Block) {
                                // 代码行及块不支持转义
                                sb.Append(chr);
                            } else {
                                // 当为空时，增加文本行
                                if (pt == Parse_Null) {
                                    pt = Parse_Text;
                                    MdTextLine mdTextLine = new MdTextLine();
                                    doc.Children.Add(mdTextLine);
                                    block.Add(mdTextLine);
                                }
                                isEscape = true;
                            }
                        }
                        break;
                    #endregion
                    case '#':
                        #region [=====处理转义符=====]
                        // 当处于字符串模式时，操作转义
                        if (isEscape) {
                            // 当处于转义模式，则直接添加字符并退出转义
                            sb.Append(chr);
                            isEscape = false;
                        } else {
                            sb.Append(chr);
                            if (pt == Parse_Null) {
                                pt = Parse_Title;
                            }
                        }
                        break;
                    #endregion
                    case '\r': break;//忽略回车符
                    case '\n':
                        #region [=====处理换行=====]

                        // 文本模式时
                        if ((pt & Parse_Text) == Parse_Text & pt != Parse_Text_Finish) {
                            if (sb.Length > 0) {
                                // 添加文本
                                block[0].Children.Add(new MdText() { Content = sb.ToString() });
                                // 清理缓存
                                sb.Clear();
                            }
                            // 设置为文本结束
                            pt = Parse_Text_Finish;
                        }

                        // 文本结束时，将行数据变成段
                        if (pt == Parse_Text_Finish) {
                            MdTextLine mdTextLine = (MdTextLine)block[0];
                            mdTextLine.IsSection = true;
                            // 设置空
                            pt = Parse_Null;
                            block.Clear();
                        }

                        // 无序列表模式
                        if (pt == Parse_List_Unordered) {
                            // 判断层级是否合理
                            if (block.Count >= lv + 1) {
                                // 添加列表项
                                block[lv].Children.Add(new MdUnorderedListItem() { Level = lv, Content = sb.ToString() });
                                // 清理多余层级
                                if (block.Count > lv + 1) block.RemoveRange(lv + 1, block.Count - lv - 1);
                                sb.Remove(0, 2);
                            } else if (block.Count == lv) {
                                // 添加一个新的无序列表
                                MdUnorderedList mdUnorderedList = new MdUnorderedList() { Level = lv };
                                if (lv == 0) {
                                    doc.Children.Add(mdUnorderedList);
                                } else {
                                    block[lv - 1].Children.Add(mdUnorderedList);
                                }
                                block.Add(mdUnorderedList);
                                // 添加列表项
                                sb.Remove(0, 2);
                                block[lv].Children.Add(new MdUnorderedListItem() { Level = lv, Content = sb.ToString() });
                            } else {
                                // 不合理的层级直接附加到上一行数据中
                                StringBuilder builder = new StringBuilder();
                                for (int s = 0; s < lv; s++) {
                                    builder.Append("    ");
                                }
                                builder.Append(sb.ToString());
                                int idxBlock = block.Count - 1;
                                int idxItem = block[idxBlock].Children.Count - 1;
                                MdContentBasic mdContent = (MdContentBasic)block[idxBlock].Children[idxItem];
                                mdContent.Content += builder.ToString();
                            }

                            // 清理缓存
                            sb.Clear();
                            // 设置为完成
                            pt = Parse_List_Finish;
                        }

                        // 有序列表模式
                        if (pt == Parse_List_Ordered) {
                            // 判断层级是否合理
                            if (block.Count >= lv + 1) {
                                // 添加列表项
                                string temp = sb.ToString();
                                int idxDot = temp.IndexOf(".");
                                block[lv].Children.Add(new MdOrderedListItem() { Level = lv, Content = temp.Substring(idxDot + 1), SerialNumber = temp.Substring(0, idxDot).ToInteger() });
                                // 清理多余层级
                                if (block.Count > lv + 1) block.RemoveRange(lv + 1, block.Count - lv - 1);
                            } else if (block.Count == lv) {
                                // 添加一个新的无序列表
                                string temp = sb.ToString();
                                int idxDot = temp.IndexOf(".");
                                MdOrderedList mdOrderedList = new MdOrderedList() { Level = lv, SerialNumber = temp.Substring(0, idxDot).ToInteger() };
                                if (lv == 0) {
                                    doc.Children.Add(mdOrderedList);
                                } else {
                                    block[lv - 1].Children.Add(mdOrderedList);
                                }
                                block.Add(mdOrderedList);
                                // 添加列表项
                                block[lv].Children.Add(new MdOrderedListItem() { Level = lv, Content = temp.Substring(idxDot + 1), SerialNumber = temp.Substring(0, idxDot).ToInteger() });
                            } else {
                                // 不合理的层级直接附加到上一行数据中
                                StringBuilder builder = new StringBuilder();
                                for (int s = 0; s < lv; s++) {
                                    builder.Append("    ");
                                };
                                builder.Append(sb.ToString());
                                int idxBlock = block.Count - 1;
                                int idxItem = block[idxBlock].Children.Count - 1;
                                MdContentBasic mdContent = (MdContentBasic)block[idxBlock].Children[idxItem];
                                mdContent.Content += builder.ToString();
                            }

                            // 清理缓存
                            sb.Clear();
                            // 设置为完成
                            pt = Parse_List_Finish;
                        }

                        // 结束模式
                        if (pt == Parse_List_Finish || pt == Parse_Block_Finish) {
                            // 设置空
                            pt = Parse_Null;
                            block.Clear();
                        }

                        // 标题模式
                        if (pt == Parse_Title) {
                            if (sb.Length <= 1) {
                                MdTextLine mdTextLine = new MdTextLine();
                                doc.Children.Add(mdTextLine);
                                mdTextLine.Children.Add(new MdText() { Content = sb.ToString() });
                                // 设置为文本结束
                                pt = Parse_Text_Finish;
                            } else if (sb[0] == '#' && sb[1] == ' ') {
                                // 标题1
                                sb.Remove(0, 2);
                                doc.Children.Add(new MdTitle1() { Content = sb.ToString() });
                                // 设置空
                                pt = Parse_Null;
                                block.Clear();
                            } else if (sb[0] == '#' && sb[1] == '#' && sb[2] == ' ') {
                                // 标题2
                                sb.Remove(0, 3);
                                doc.Children.Add(new MdTitle2() { Content = sb.ToString() });
                                // 设置空
                                pt = Parse_Null;
                                block.Clear();
                            } else if (sb[0] == '#' && sb[1] == '#' && sb[2] == '#' && sb[3] == ' ') {
                                // 标题3
                                sb.Remove(0, 4);
                                doc.Children.Add(new MdTitle3() { Content = sb.ToString() });
                                // 设置空
                                pt = Parse_Null;
                                block.Clear();
                            } else if (sb[0] == '#' && sb[1] == '#' && sb[2] == '#' && sb[3] == '#' && sb[4] == ' ') {
                                // 标题4
                                sb.Remove(0, 5);
                                doc.Children.Add(new MdTitle4() { Content = sb.ToString() });
                                // 设置空
                                pt = Parse_Null;
                                block.Clear();
                            } else if (sb[0] == '#' && sb[1] == '#' && sb[2] == '#' && sb[3] == '#' && sb[4] == '#' && sb[5] == ' ') {
                                // 标题5
                                sb.Remove(0, 6);
                                doc.Children.Add(new MdTitle5() { Content = sb.ToString() });
                                // 设置空
                                pt = Parse_Null;
                                block.Clear();
                            } else if (sb[0] == '#' && sb[1] == '#' && sb[2] == '#' && sb[3] == '#' && sb[4] == '#' && sb[5] == '#' && sb[6] == ' ') {
                                // 标题6
                                sb.Remove(0, 7);
                                doc.Children.Add(new MdTitle6() { Content = sb.ToString() });
                                // 设置空
                                pt = Parse_Null;
                                block.Clear();
                            } else {
                                MdTextLine mdTextLine = new MdTextLine();
                                doc.Children.Add(mdTextLine);
                                mdTextLine.Children.Add(new MdText() { Content = sb.ToString() });
                                // 设置为文本结束
                                pt = Parse_Text_Finish;
                            }
                            // 清理缓存
                            sb.Clear();
                        }

                        line++;
                        col = 0;
                        break;
                    #endregion
                    default:
                        // 当为空时，增加文本行
                        if (pt == Parse_Null) {
                            pt = Parse_Text;
                            MdTextLine mdTextLine = new MdTextLine();
                            doc.Children.Add(mdTextLine);
                            block.Add(mdTextLine);
                        }
                        // 添加字符
                        sb.Append(chr);
                        break;
                }
            }

            return doc;

        }

        /// <summary>
        /// 获取HTML转码后的序列化字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Escape(string str) {
            if (str.IsNull()) return null;
            string res = str;
            res = res.Replace("\\", @"\\");
            res = res.Replace("`", @"\`");
            res = res.Replace("*", @"\*");
            res = res.Replace("{", @"\{");
            res = res.Replace("}", @"\}");
            res = res.Replace("[", @"\[");
            res = res.Replace("]", @"\]");
            res = res.Replace("(", @"\(");
            res = res.Replace(")", @"\)");
            res = res.Replace("#", @"\#");
            res = res.Replace("+", @"\+");
            res = res.Replace("-", @"\-");
            res = res.Replace(".", @"\.");
            res = res.Replace("!", @"\!");
            return res;
        }

    }
}
