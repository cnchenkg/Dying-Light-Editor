using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace 消失的光芒编辑器aa
{
    /// <summary>
    ///  读取Inventory数据
    /// </summary>
    class ReadInventory_Gen
    {

        /// <summary>
        ///  近战
        /// </summary>
        public Dictionary<string, CategoryType> melee = new Dictionary<string, CategoryType>();
        /// <summary>
        ///  创建一个近战对象。
        /// </summary>
        private CategoryType category = new CategoryType();
        /// <summary>
        ///  获取武器所有参数。
        /// </summary>
        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> keyValues = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
        /// <summary>
        ///  武器属性是方法时存放到这里。
        /// </summary>
        public Dictionary<string, List<string>> values = new Dictionary<string, List<string>>();
        private string propertyType = "Color，Crafting， Skin， NoiseType";
        // 截取武器类名。
        private static string typeName;
        // 截取物品名。
        private static string name;
        // 截取武器属性。
        private static string extractProperty;
        // 截取武器。
        private static string extractValue;
        /// <summary>
        ///  读取所有武器。
        /// </summary>
        /// <param name="path">读取路径。</param>
        public void ReadAllArms(string path)
        {

            // 读取文件全部行数。
            string[] fileContext = File.ReadAllLines(path);

            for (int i = 0; i < fileContext.Length; i++)
            {
                // 进入该函数既表示当前行是武器命名行及分类行，既后面几行就是武器属性了。
                if (fileContext[i].Contains("Item(\"") && fileContext[i].Contains(")") && !fileContext[i].Contains(");"))
                {
                    // 截取武器类别及武器名称。
                    typeName = ExtractString.ExtractContent(fileContext[i], ", ", ")");
                    name = ExtractString.ExtractContent(fileContext[i], "\"", "\",");
                    // 判断是否存在该武器类别。
                    if (!keyValues.ContainsKey(typeName))
                    {
                        keyValues.Add(typeName, new Dictionary<string, Dictionary<string, string>>());
                    }
                    // 判断如果不存在该键则添加武器名。
                    if (!keyValues[typeName].ContainsKey(name))
                    {
                        // 添加武器名及其分类。
                        keyValues[typeName].Add(name, new Dictionary<string, string>());
                        i++;

                        // 循环该武器下的属性并赋值。条件1找到，且条件而没有找到时才能进入循环。
                        // fileContext[i].Contains("}") 找到为true，没找到为false。
                        // !fileContext[i + 1].Contains(");") 找到为true并取反，没找到为false并取反。
                        // 最后与!(条件1&& !条件1) 条件1为真条件2为真，那么最后结果就是取反值为false既不满足条件。
                        // 条件1为假，条件2取反为假那么括号内的条件就为假取反为真，
                        // 条件1为真，条件2取反为假那么括号内的条件就为假取反为真。
                        // 条件1为真，条件2取反为真那么括号内的条件就为真取反为假。
                        while (!(fileContext[i].Contains("}") && !fileContext[i + 1].Contains(");")))
                        {
                            // 判断是否存在属性语句，且是非使用函数的语句。
                            if (fileContext[i].Contains(");") && !fileContext[i].Contains("use "))
                            {
                                if (!fileContext[i].Contains("Effect("))
                                {
                                    // 截取属性名，截取属性值。
                                    extractProperty = ExtractString.ExtractContent(fileContext[i], "(");
                                    extractValue = ExtractString.ExtractContent(fileContext[i], "(", ");");
                                    // 判断，如果不存在该属性名则向键添加值，否则什么也不做继续读取下一行。
                                    if (!keyValues[typeName][name].ContainsKey(extractProperty))
                                    {
                                        keyValues[typeName][name].Add(extractProperty, extractValue);
                                    }
                                }
                                
                            }
                            i++;
                        }
                    }

                }
            }

        }

    }
}
