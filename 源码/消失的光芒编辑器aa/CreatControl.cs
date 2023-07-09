using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 消失的光芒编辑器aa
{
    /// <summary>
    ///  创建控件类。
    /// </summary>
    class CreatControl
    {
        #region 字段
        // 字段，存放该中英对照值。
        private Dictionary<string, string> englishAndChinese = new Dictionary<string, string>();
        // 存放该类本身，该类只能被实例化一次
        private static CreatControl creatControl = null;
        private string filePath = Environment.CurrentDirectory + @"\Config\属性对照表.ini";
        // 临时中文对照变量。
        private static string temporary=null;
        private string[] listTemp = new string[] { };
        private List<string> listStTemp = new List<string>();
        #endregion
        #region 方法
        /// <summary>
        ///  根据传入数量创建控件并显示在group上面既传入进来的要显示的空间。
        /// </summary>
        /// <param name="flow">向该流容器创建控件</param>
        /// <param name="keyValues">读取该字典创建控件。</param>
        /// <param name="dropDown">可选下拉菜单栏。</param>
        /// <param name="sender">触发者。</param>
        public void Creat(FlowLayoutPanel flow, Dictionary<string, string> keyValues, Dictionary<string, List<string>> dropDown, EventHandler sender)
        {
            PropertyValue[] propertyValues = new PropertyValue[keyValues.Count];
            for (int i = 0; i < propertyValues.Length; i++)
            {
                englishAndChinese.TryGetValue(keyValues.Keys.ElementAt(i), out temporary);
                propertyValues[i] = new PropertyValue();
                propertyValues[i].Name = "PropertyValue_" + i.ToString();
                propertyValues[i].label1.Text = keyValues.Keys.ElementAt(i);
                propertyValues[i].label2.Text = temporary;
                //if (dropDown.TryGetValue(keyValues.Keys.ElementAt(i), out listStTemp))
                //{
                //    listTemp = listStTemp.ToArray();
                //    propertyValues[i].textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                //    propertyValues[i].textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                //    propertyValues[i].textBox1.AutoCompleteCustomSource.AddRange(listTemp);
                //    propertyValues[i].textBox1.Text=keyValues.Keys.ElementAt(i);
                //}
                propertyValues[i].textBox1.Text = keyValues.Values.ElementAt(i);
                propertyValues[i].button1.Click += sender;
                flow.Controls.Add(propertyValues[i]);
            }
        }
        /// <summary>
        ///  获取中英文对照表。
        /// </summary>
        private void GetEnglishAndChinese()
        {
            // 获取中英文对照表
            englishAndChinese = ReadWriteIni.getSectionPairs("属性对照表", filePath);
        }

        #endregion
        #region 构造方法。
        /// <summary>
        ///  构造方法私有，外部无法实例化对象。
        /// </summary>
        private CreatControl()
        {
            // 获取字典。
            this.GetEnglishAndChinese();
        }
        public static CreatControl GetInstance()
        {
            // 判断有无被实例化。
            if (creatControl == null)
            {
                creatControl = new CreatControl();
                return creatControl;
            }
            return creatControl;
        }

        #endregion

    }
}
