# 关于 dpz3 开发组件

一套基于.Net Standard的综合性辅助开发套件基础件，包含基础类型扩展、字符串处理、文件处理、汉字拼音处理、常见协议解析处理(json/xml/html/css/Markdown)及网络访问

## 致力于更有开发效率

dpz3组件最初的版本构建，就是基于长期的C#开发经验，为的就是降低重复代码量，增加代码效率。

一些常用的代码进行了封装从而使开发变得简单，如简单的强兼容数值转换（无法转换不报错直接返回0）：

```
using System;
using dpz3;

namespace demo {
    class Program {
        static void Main(string[] args) {
            string a = "abc";
            string b = "123";
            Console.WriteLine($"int a={a.ToInteger()}");
            Console.WriteLine($"int b={b.ToInteger()}");
        }
    }
}
```

其执行结果为：

```
int a=0
int b=123
```

一些常用的数据格式进行了解析实现，如Json数据处理：

```
using System;
using dpz3;

namespace demo {
    class Program {
        static void Main(string[] args) {
            using (dpz3.Json.JsonObject json = new dpz3.Json.JsonObject()) {
                json["obj"].Str["a"] = "abc";
                json["obj"].Num["b"] = 123;
                Console.WriteLine(json.ToJsonString());
            }
        }
    }
}
```

其执行结果为：

```
{"obj":{"a":"abc","b":123}}
```

## 基于 dpz3 的扩展组件

+ dpz3.AspNetCore2 - 服务于asp.net core 2.x的MVC相关扩展组件
+ dpz3.AspNetCore3 - 一套服务于Asp.Net Core 3.0的扩展，包含Kestrel服务的快速配置及WebApi基础件
+ dpz3.Jttp - 一套服务于Json数据传输协议扩展(.Net Standard)，在标准的Json结构上加入Header、Data、Message等标准化的顶层对象
+ dpz3.Modular - 一套服务于模块化Asp.Net Core站点的扩展，包含站点模块化的的专用特性、基类和Aspx网页转换为C#类的解析等功能
+ dpz3.User - 一套服务于用户管理的扩展(.Net Standard)，支持MD5/sha1/sha256/sha512方式密码加密，及文件式的用户信息存取
+ dpz3.VDisk - 一套基于4K数据块可扩展虚拟磁盘文件存储组件(.Net Standard)
+ dpz3.XOrm - 一套服务于XML定义式对象关系映射(XOrm)组件(.Net Standard)
+ dpz3.db - 一套服务于数据库应用的扩展(.Net Standard)，支持Sql Server、MySql、Sqlite和PostgreSql的原生SQL语句和通用封装函数操作
+ dpz3.ssr - 一套服务于Simple Server的扩展(.Net Standard)，用于UTF8文本通讯的简单C/S服务开发组件
+ dpz3.ssr.SessionClient - 一套基于dpz3.ssr组件二次开发的简易交互信息客户端组件(.Net Standard)
+ dpz3.ssr.SessionServer - 一套基于dpz3.ssr组件开发的简易交互信息服务端组件(.Net Standard)

## 在Nuget仓库中下载

本开发组件及其扩展组件均可在Nuget仓库中下载

+ dpz3 [https://www.nuget.org/packages/dpz3/](https://www.nuget.org/packages/dpz3/)
+ dpz3.AspNetCore2 [https://www.nuget.org/packages/dpz3.AspNetCore2/](https://www.nuget.org/packages/dpz3.AspNetCore2/)
+ dpz3.AspNetCore3 [https://www.nuget.org/packages/dpz3.AspNetCore3/](https://www.nuget.org/packages/dpz3.AspNetCore3/)
+ dpz3.Jttp [https://www.nuget.org/packages/dpz3.jttp/](https://www.nuget.org/packages/dpz3.jttp/)
+ dpz3.Modular [https://www.nuget.org/packages/dpz3.Modular/](https://www.nuget.org/packages/dpz3.Modular/)
+ dpz3.User [https://www.nuget.org/packages/dpz3.User/](https://www.nuget.org/packages/dpz3.User/)
+ dpz3.VDisk [https://www.nuget.org/packages/dotnet-dpz3-vdisk/](https://www.nuget.org/packages/dotnet-dpz3-vdisk/)
+ dpz3.XOrm [https://www.nuget.org/packages/dpz3.XOrm/](https://www.nuget.org/packages/dpz3.XOrm/)
+ dpz3.db [https://www.nuget.org/packages/dpz3.db/](https://www.nuget.org/packages/dpz3.db/)
+ dpz3.ssr [https://www.nuget.org/packages/dpz3.ssr/](https://www.nuget.org/packages/dpz3.ssr/)
+ dpz3.ssr.SessionClient [https://www.nuget.org/packages/dpz3.ssr.SessionClient/](https://www.nuget.org/packages/dpz3.ssr.SessionClient/)
+ dpz3.ssr.SessionServer [https://www.nuget.org/packages/dpz3.ssr.SessionServer/](https://www.nuget.org/packages/dpz3.ssr.SessionServer/)