using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace 消失的光芒编辑器aa
{
    /// <summary>
    ///  写入类。
    /// </summary>
  static  class WriteInventory_Gen
    {

         private  static  string[] fileContext = File.ReadAllLines("D:\\EPIC\\APP\\DyingLight\\mod\\mod解压文件\\scripts\\inventory\\inventory_gen.scr");
        /// <summary>
        ///  向文件写入修改后的值。
        /// </summary>
        /// <param name="itemName">写入项名称既武器名。</param>
        /// <param name="name">武器属性名。</param>
        /// <param name="value">修改值。</param>
        /// <param name="path">文件路径。</param>
        public static void WriteFile(string itemName, string name, string value, string path)
        {
            string[] fileContext = File.ReadAllLines(path);
            AutoBackFile(fileContext);
            for (int i = 0; i < fileContext.Length; i++)
            {
                if (fileContext[i].Contains(itemName))
                {
                    for (int j = i; j < fileContext.Length; j++)
                    {
                        if (fileContext[j].Contains(name))
                        {
                            fileContext[j] = "        " + name + "(" + value + ");" + "          // 原始数据：" + fileContext[j];
                            break;
                        }
                    }

                }
            }


            //using (File file=new File(path,FileMode.Open))
            //{

            //    Byte[] buffer = new byte[1024 * 1024 * 5];
            //    int readOffset= file.Read(buffer, 0, buffer.Length);
            //   string readResult= Encoding.UTF8.GetString( buffer,0,readOffset);
            //    if (readResult.Contains(itemName))
            //    {
            //        if (readResult.Contains(itemName)&& readResult.Contains(name))
            //        {
            //        }
            //    }
            //}
        }
        /// <summary>
        ///  传入字典，将这个字典所有值写入进文件。
        /// </summary>
        /// <param name="category">要写入的字典。</param>
        /// <param name="path">写入路径。</param>
        public static void WriteFile(Dictionary<string, Dictionary<string, Dictionary<string, string>>> category, string path)
        {
            if (category.Count>0)
            {
                // 定义属性，物品类型，物品名，截取字符串名，物品数据开始位置，物品数据结束位置。
                string typeName;
                string name;
                string extract;
                int state=0;
                int endStop=0;
                // 备份文件。
                AutoBackFile(fileContext);
                // 循环整个集合字典。并将字典中的值添加到文件中。
                for (int i = 0; i < category.Count; i++)
                {
                    // 截取类型名。截取物品名。
                    typeName = category.Keys.ElementAt(i);
                    name = category[typeName].Keys.ElementAt(i);
                    // 得到包含该物品名称开始行号，并加2指定定位属性开始行。
                    for (int j = state; j < fileContext.Length; j++)
                    {
                        if (fileContext[j].Contains(typeName)&& fileContext[j].Contains(name))
                        {
                            state = j+2;
                            break;
                        }
                    }
                    // 得该物品数据结束行的特征行号。
                    for (int j = state; j < fileContext.Length; j++)
                    {
                        if (fileContext[j].Contains("}"))
                        {
                            endStop = j;
                            break;
                        }
                    }
                    // 循环向该行武器下的属性写入修改后的值。
                    for (int j = state; j < endStop; j++)
                    {
                       // 判断是否存在属性行的特征。
                        if (fileContext[j].Contains(");") && !fileContext[j].Contains("use "))
                        {
                            // 截取该行属性名。
                            extract = ExtractString.ExtractContent(fileContext[j], "(");
                            // 当字典中存在该属性名称时，将该属性名称追加到行中。
                            if (category[typeName][name].ContainsKey(extract))
                            {
                                // 修改该行信息。并将原始数据追加到行后面。
                                fileContext[j] = "        " + extract + "(" + category[typeName][name][extract] + ");"+ "      //原始数据：  "+ fileContext[j];
                            }
                        }
                    }
                }
                // 写入文件。
                File.WriteAllLines(path, fileContext);
                category.Clear();
            }
        }
        /// <summary>
        ///  根据传入内容写入文件。
        /// </summary>
        /// <param name="name">武器名。</param>
        /// <param name="category">字典。</param>
        /// <param name="property">属性。</param>
        /// <param name="path">路径。</param>
        public static void WriteFile(string name, Dictionary<string, CategoryType> category, List<string> property, string path)
        {
            if (property.Count > 0)
            {
                string[] fileContext = File.ReadAllLines(path);
                AutoBackFile(fileContext);
                for (int i = 0; i < fileContext.Length; i++)
                {
                    if (fileContext[i].Contains(name))
                    {
                        for (int j = i; j < fileContext.Length; j++)
                        {
                            if (fileContext[j].Contains("(") && fileContext[j].Contains(")"))
                            {
                                for (int k = 0; k < property.Count; k++)
                                {
                                    if (fileContext[j].Contains(property[k]))
                                    {
                                        fileContext[j] = "        " + property[k] + "(" + category[name].GetType().GetProperty(property[k]).GetValue(category[name]) + ");" + "          // 原始数据：" + fileContext[j];
                                        break;
                                    }
                                }
                            }


                        }

                    }
                }
            }

        }
        private static void AutoLog()
        {

        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="backfile"></param>
        private static void AutoBackFile(string[] backfile)
        {
            string filePath = @"D:\vs\消失的光芒编辑器aa\bin\Debug\aaa.txt";
            if (!File.Exists(filePath))
            {
                File.Create(filePath);
            }
            File.WriteAllLines(filePath, backfile, Encoding.UTF8);
        }
    }
}
