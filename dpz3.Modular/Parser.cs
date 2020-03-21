using System;
using System.Collections.Generic;
using System.Text;

namespace dpz3.Modular {

    /// <summary>
    /// 模块化网站专用解析器
    /// </summary>
    public static class Parser {

        private enum ParseAspxToCodeTypes {
            Content = 0x00,  // 内容
            Define = 0x10,   // 定义开始
            Code = 0x20,    // 代码块
        }

        /// <summary>
        /// 将Html解析为可执行代码
        /// </summary>
        /// <param name="html"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string ParseAspxToCode(string html, string name) {
            StringBuilder sb = new StringBuilder();
            StringBuilder line = new StringBuilder();
            // 初始化解析类型为内容
            ParseAspxToCodeTypes tp = ParseAspxToCodeTypes.Content;
            bool isString = false;
            // 添加输出内容定义语句
            sb.Append($"public IResult {name}() {{\n");
            sb.Append("    StringBuilder sb = new StringBuilder();\n");
            for (int i = 0; i < html.Length; i++) {
                char chr = html[i];
                switch (chr) {
                    // 切换字符串模式
                    case '"':
                        if(tp == ParseAspxToCodeTypes.Code) {
                            isString = !isString;
                        }
                        line.Append(chr);
                        break;
                    case '<':
                        // 处理在字符串中的情况
                        if (isString) {
                            line.Append(chr);
                            break;
                        }
                        // 进入定义模式
                        if (tp == ParseAspxToCodeTypes.Content) {
                            // 进入代码模式
                            tp = ParseAspxToCodeTypes.Define;
                        } else {
                            line.Append(chr);
                        }
                        break;
                    case '%':
                        // 处理在字符串中的情况
                        if (isString) {
                            line.Append(chr);
                            break;
                        }
                        if (tp == ParseAspxToCodeTypes.Define) {
                            // 将已有代码进行处理
                            if (line.Length > 0) sb.Append($"    sb.Append(\"{line.ToString().Replace("\"", "\\\"")}\");\n");
                            // 进入代码模式
                            tp = ParseAspxToCodeTypes.Code;
                            // 清理缓存
                            line.Clear();
                        } else {
                            line.Append(chr);
                        }
                        break;
                    case '>':
                        // 处理在字符串中的情况
                        if (isString) {
                            line.Append(chr);
                            break;
                        }
                        if (tp == ParseAspxToCodeTypes.Code) {
                            if (line.Length > 0) {
                                // 判断是否代码结束
                                if (line[line.Length - 1] == '%') {
                                    line.Remove(line.Length - 1, 1);
                                    if (line.Length == 0) {
                                        // 进入代码模式
                                        tp = ParseAspxToCodeTypes.Content;
                                        // 清理缓存
                                        line.Clear();
                                        // 结束处理
                                        break;
                                    }
                                    if (line[0] == '=') {
                                        // 将已有代码进行处理
                                        line.Remove(0, 1);
                                        sb.Append($"    sb.Append({line.ToString()});\n");
                                        // 进入代码模式
                                        tp = ParseAspxToCodeTypes.Content;
                                        // 清理缓存
                                        line.Clear();
                                    } else if (line[0] == '@') {
                                        // 申明部分不进行任何处理
                                        // 进入代码模式
                                        tp = ParseAspxToCodeTypes.Content;
                                        // 清理缓存
                                        line.Clear();
                                    } else {
                                        // 将已有代码进行处理
                                        sb.Append($"    {line.ToString()}\n");
                                        // 进入代码模式
                                        tp = ParseAspxToCodeTypes.Content;
                                        // 清理缓存
                                        line.Clear();
                                    }
                                } else {
                                    line.Append(chr);
                                }
                            } else {
                                line.Append(chr);
                            }
                        } else {
                            line.Append(chr);
                        }
                        break;
                    case ' ':
                        // 处理在字符串中的情况
                        if (isString) {
                            line.Append(chr);
                            break;
                        }
                        if (line.Length > 0) {
                            line.Append(chr);
                        }
                        break;
                    case '\r': break;
                    case '\n':
                        // 退出代码定义
                        if (tp == ParseAspxToCodeTypes.Define) {
                            tp = ParseAspxToCodeTypes.Content;
                            line.Append('<');
                        }
                        if (tp == ParseAspxToCodeTypes.Content) {
                            // 输出文本内容
                            if (line.Length > 0) sb.Append($"    sb.Append(\"{line.ToString().Replace("\"", "\\\"")}\\n\");\n");
                            // 清理缓存
                            line.Clear();
                        } else if (tp == ParseAspxToCodeTypes.Code) {
                            // 输出代码
                            if (line.Length > 0) sb.Append($"    {line.ToString()}\n");
                            // 清理缓存
                            line.Clear();
                        }
                        break;
                    default:
                        // 退出代码定义
                        if (tp == ParseAspxToCodeTypes.Define) {
                            tp = ParseAspxToCodeTypes.Content;
                            line.Append('<');
                        }
                        line.Append(chr);
                        break;
                }
            }
            if (line.Length > 0) {
                sb.Append($"    sb.Append(\"{line.ToString().Replace("\"", "\\\"")}\");\n");
            }
            sb.Append("    return Html(sb.ToString());\n");
            sb.Append("}\n");
            return sb.ToString();
        }

    }
}
