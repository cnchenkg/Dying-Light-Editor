using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 消失的光芒编辑器aa
{
    public partial class Form1 : Form
    {
        #region 属性字段。
        // 读取武器对象。
        ReadInventory_Gen gen = new ReadInventory_Gen();
        // 存放选择的武器键与值。
        Dictionary<string, string> keyValues;
        /// <summary>
        ///  中文与英文翻译。键为英文，值为中文。
        /// </summary>
        Dictionary<string, string> keyChinese;
        /// <summary>
        ///  存放路径的数组。
        /// </summary>
        private static string[] filePath;
        /// <summary>
        ///  当前读写的路径。
        /// </summary>
        string currentPath;
        /// <summary>
        ///  保存所有修改过的值。
        /// </summary>
        public Dictionary<string, Dictionary<string, Dictionary<string, string>>> saveModifyValue = new Dictionary<string, Dictionary<string, Dictionary<string, string>>>();
        /// <summary>
        ///  创建集合对象。
        /// </summary>
        readonly CreatControl creat = CreatControl.GetInstance();
        /// <summary>
        ///  获取物品名中英对照的配置文件。
        /// </summary>
        public static string ENandCN = Environment.CurrentDirectory + @"\Config\物品名.ini";
        /// <summary>
        ///   将下拉列表值更新到该列表。
        /// </summary>
        private List<string> listDrow = new List<string>();
        /// <summary>
        ///  将下来列表中文添加进来。
        /// </summary>
        private List<string> listDrowCN = new List<string>();
        #endregion
        #region 方法
        /// <summary>
        ///   方法- 判断是否还有未保存数据。
        /// </summary>
        private void tips()
        {
            if (saveModifyValue.Count > 0)
            {
                DialogResult dr = MessageBox.Show("您有未保存的数据，是否保存", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK)
                {
                    fileSave.PerformClick();
                }
            }
        }
        /// <summary>
        ///  方法-判断修改后的字典中是否包含该值。
        /// </summary>
        /// <param name="property">将修改过值得自定义控件传入进来。</param>
        private void judgement(PropertyValue property)
        {
            // 如果不存在该武器名就添加。
            if (!saveModifyValue[comboBox2.SelectedItem.ToString()].ContainsKey(listDrow[comboBox1.SelectedIndex]))
            {
                // 添加武器名。
                saveModifyValue[comboBox2.SelectedItem.ToString()].Add(listDrow[comboBox1.SelectedIndex], new Dictionary<string, string>());
                // 如果已经存在该值就该武器属性就不添加，否则就添加。
                if (!saveModifyValue[comboBox2.SelectedItem.ToString()][listDrow[comboBox1.SelectedIndex]].ContainsKey(property.label1.Text))
                {

                    saveModifyValue[comboBox2.SelectedItem.ToString()][listDrow[comboBox1.SelectedIndex]].Add(property.label1.Text, property.textBox1.Text);
                }
                //else
                //{
                //    saveModifyValue[comboBox2.SelectedItem.ToString()][listDrow[comboBox1.SelectedIndex]].Add(property.label1.Text, property.textBox1.Text);
                //}
            }
            else
            {
                // 如果已经存在该值就该武器属性就不添加，否则就添加。
                //saveModifyValue[comboBox2.SelectedItem.ToString()].Add(listDrow[comboBox1.SelectedIndex], new Dictionary<string, string>());
                if (!saveModifyValue[comboBox2.SelectedItem.ToString()][listDrow[comboBox1.SelectedIndex]].ContainsKey(property.label1.Text))
                {
                    saveModifyValue[comboBox2.SelectedItem.ToString()][listDrow[comboBox1.SelectedIndex]].Add(property.label1.Text, property.textBox1.Text);
                }
            }
        }
        /// <summary>
        ///  方法-文件窗口选择打开文件路径。
        /// </summary>
        /// <returns>返回结果，打开文件为true，否则false</returns>
        private bool determinePath()
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                // 文件初始路径。
                openFile.InitialDirectory = "\\";
                // 数据筛选文件。
                openFile.Filter = "inventory药品数据文件 (inventory.scr)|inventory.scr|inventory武器数据文件 (*gen.scr)|*gen.scr|所有scr文件 (*.scr)|*.scr";
                // 默认筛选文件索引。
                openFile.FilterIndex = 3;
                // 是否还原路径，既下次打开的默认路径。
                openFile.RestoreDirectory = false;
                // 禁止一次选着多个文件。
                openFile.Multiselect = false;
                // 窗口标题。
                openFile.Title = "请选择要打开的游戏数据文件。";
                // 判断文件是否打开。
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    if (openFile.FileName != null)
                    {
                        //aaasss = openFile.FileNames;
                        currentPath = openFile.FileName;
                        return true;
                    }

                    //// 得到路径并存放在用户配置文件中。
                    //ReadWriteIni.iniWrite("读取文件路径", openFile.SafeFileName, openFile.FileName);
                    //Properties.Settings.Default.inventory = openFile.FileName;
                    //Properties.Settings.Default.Save();
                }
            }
            return false;
        }
        /// <summary>
        ///  方法-加载指定节点文件。
        /// </summary>
        /// <param name="section">节点名。</param>
        private void loading(string section)
        {
            filePath = ReadWriteIni.getValue(section);
            for (int i = 0; i < filePath.Length; i++)
            {
                filePath[i] = ExtractString.ExtractContent(filePath[i], filePath[i].IndexOf('=') + 1);
            }
        }
        /// <summary>
        ///  将下拉列表中的英文翻译为中文。
        /// </summary>
        private void translate()
        {
            // 临时变量。
            string temproary = null;
            // 将下来列表清空。
            comboBox1.DataSource = null;
            // 清空中文对照表。
            listDrowCN.Clear();
            // 循环赋值。
            foreach (var item in listDrow)
            {
                // 获取返回值。并将返回值赋值
                keyChinese.TryGetValue(item, out temproary);
                if (temproary != null)
                {
                    // 将获取到得到中文值添加进入下拉列表。
                    listDrowCN.Add(temproary);
                }
                else
                {
                    listDrowCN.Add(item);

                }
            }
            // 将获取到的中文添加进下来列表。
            comboBox1.DataSource = listDrowCN;

        }
        #endregion
        #region 窗体
        /// <summary>
        ///  窗口加载方法。
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            filePath = new string[defaultPath.DropDownItems.Count];
            loading("读取文件路径");
            if (filePath.Length > 0)
            {
                foreach (var item in filePath)
                {
                    comboBox3.Items.Add(Path.GetFileName(item));

                }
                comboBox3.SelectedIndex = 0;
            }
            keyChinese = ReadWriteIni.getSectionPairs("物品名", ENandCN);
            this.comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            this.comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;

        }
        /// <summary>
        ///  按钮-加载数据按钮。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {

            if (currentPath != null)
            {
                // 读取文件。
                gen.ReadAllArms(currentPath);
                // 加载到下拉框中
                if (gen.keyValues.Count > 0)
                {

                    comboBox2.DataSource = gen.keyValues.Keys.ToList();
                    comboBox3.Enabled = false;
                }
            }
            #region 加载中英对照表
            //List<string> ss = new List<string>();
            //Dictionary<string, string> sss = new Dictionary<string, string>();
            //foreach (var item in gen.keyValues)
            //{
            //    foreach (var a in item.Value)
            //    {
            //        ss.Add(a.Key);
            //    }
            //}
            //// 读取文件全部行数。
            //string[] fileContext = File.ReadAllLines(@"D:\EPIC\APP\DyingLight\mod\文本编辑\common_texts_all.bin.txt");
            //string asss;
            //string asss2;
            //int aa = 0;
            //for (int i = 0; i < fileContext.Length; i++)
            //{
            //    if (fileContext[i].Contains("_N="))
            //    {
            //        for (int j = 0; j < ss.Count; j++)
            //        {

            //            if (fileContext[i].Contains(ss[j] + "_N="))
            //            {
            //                if (!sss.ContainsKey(ss[j]))
            //                {
            //                    sss.Add(ss[j], fileContext[i].Substring(fileContext[i].IndexOf('=') + 1));
            //                    ReadWriteIni.iniWrite("物品名", ss[j], fileContext[i].Substring(fileContext[i].IndexOf('=') + 1), @"D:\vs\消失的光芒编辑器aa\bin\Debug\Config\物品名.ini");
            //                    break;
            //                }

            //            }
            //            fileContext[i].Contains(ss[j]);

            //        }
            //    }
            //}
            #endregion
        }
        /// <summary>
        ///   按钮-所有武器名下拉列表选中事件。
        /// </summary>
        /// <param name="sender"> 数据发送者。</param>
        /// <param name="e"></param>
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 获取当前选中物品分类中的物品的所有属性值。
            if (checkBox1.Checked)
            {

                gen.keyValues[comboBox2.SelectedItem.ToString()].TryGetValue(listDrow[comboBox1.SelectedIndex], out keyValues);
            }
            else
            {
                // 如果为被选中的话，就进这个语句
                gen.keyValues[comboBox2.SelectedItem.ToString()].TryGetValue(listDrow[comboBox1.SelectedIndex], out keyValues);
            }


            // 清空属性值显示窗口。
            flowLayoutPanel1.Controls.Clear();
            // 显示选中的所有属性值。
            creat.Creat(flowLayoutPanel1, keyValues, gen.values, saveModify);
        }
        /// <summary>
        ///   按钮-所有物品分类下拉列表事件。
        /// </summary>
        /// <param name="sender">发送者。</param>
        /// <param name="e">事件</param>
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 减少事件，当选着项变更时不进行判断。
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            // 将数据源置为空。
            comboBox1.DataSource = null;
            // 清空物品下拉框。
            comboBox1.Items.Clear();
            // 判断有无选中该。
            if (checkBox1.Checked)
            {
                // 将该列表值添加进listdrow列表
                listDrow.Clear();
                listDrow = gen.keyValues[comboBox2.SelectedItem.ToString()].Keys.ToList();
                // 赋中文值。
                translate();
                // 将选中项设置为第一个。
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                // 将当前选中物品分类的的物品加载进下拉列表。
                comboBox1.DataSource = gen.keyValues[comboBox2.SelectedItem.ToString()].Keys.ToList();
            }
            // 添加事件，当下来列表1选着内容发生变化时，触发该事件。
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;

        }
        /// <summary>
        ///  确定按钮-修改后的数值。
        /// </summary>
        /// <param name="sender">触发者。</param>
        /// <param name="e">事件。</param>
        private void saveModify(object sender, EventArgs e)
        {
            // 转换为按钮，确定是哪个窗口点击的。
            Button button = (Button)sender;
            // 转换为自定义控件。
            PropertyValue property = (PropertyValue)button.Parent;
            // 接受结果。
            string result = null;
            // 得到字典中的值。
            keyValues.TryGetValue(property.label1.Text, out result);
            // 判断当前控件中的值有无被修改。
            if (!property.textBox1.Equals(result))
            {
                // 如果保存修改结果的字典中不包含该键就将其添加进去。
                if (!saveModifyValue.ContainsKey(comboBox2.SelectedItem.ToString()))
                {
                    // 将修改过的属性名传入字典中。
                    saveModifyValue.Add(comboBox2.SelectedItem.ToString(), new Dictionary<string, Dictionary<string, string>>());
                    // 判断字典中有无该值。
                    judgement(property);
                }
                else
                {
                    // 判断字典中有无该值。
                    judgement(property);
                }
            }

        }
        /// <summary>
        ///  按钮-保存文件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveFile(object sender, EventArgs e)
        {
            if (saveModifyValue.Count > 0 && currentPath != null)
            {
                WriteInventory_Gen.WriteFile(saveModifyValue, currentPath);
                MessageBox.Show("保存成功。");
                Process.Start("notepad.exe", currentPath);
            }
        }
        /// <summary>
        ///  选择文件窗口设置物品路径。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void openFileSelect(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                openFile.InitialDirectory = "\\";
                ToolStripMenuItem item = (ToolStripMenuItem)sender;
                if (item.Name == inventoryscr.Name)
                {
                    openFile.Filter = "inventory药品数据文件 (inventory.scr)|inventory.scr";
                }
                else
                {
                    openFile.Filter = "inventory武器数据文件 (*gen.scr)|*gen.scr";
                }
                openFile.FilterIndex = 1;
                openFile.RestoreDirectory = true;
                openFile.Title = "设置inventory或Gen文件。";

                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    // 得到路径并存放在用户配置文件中。
                    ReadWriteIni.iniWrite("读取文件路径", openFile.SafeFileName, openFile.FileName);
                    //Properties.Settings.Default.inventory = openFile.FileName;
                    //Properties.Settings.Default.Save();
                }
            }
        }
        /// <summary>
        ///  按钮-触发事件，打开文件如果没有就弹出窗口选择文件。
        /// </summary>
        /// <param name="sender">触发者。</param>
        /// <param name="e">事件参数。</param>
        private void fileOpen_Click(object sender, EventArgs e)
        {
            // 如果有选中文件则读取。
            if (determinePath())
            {
                #region 加载中文翻译代码，调试时使用
                //for (int i = 0; i < aaasss.Length; i++)
                //{
                //    currentPath = aaasss[i];

                //}
                // 读取文件。
                #endregion

                readFile.PerformClick();
            }


        }
        /// <summary>
        ///  按钮-快捷键。
        /// </summary>
        /// <param name="sender">触发者。</param>
        /// <param name="e">事件。</param>
        private void shortcutkeys(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (e.KeyCode)
                {
                    case Keys.S:
                        fileSave.PerformClick();
                        break;
                    case Keys.O:
                        fileOpen.PerformClick();
                        break;
                    case Keys.Q:
                        closed.PerformClick();
                        break;
                    default:
                        break;
                }
            }


        }
        /// <summary>
        ///  按钮-当选项目更改时对当前项目进行赋值。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filePath.Length > 0)
            {
                try
                {
                    currentPath = filePath[comboBox3.SelectedIndex];
                }
                catch (Exception)
                {

                    MessageBox.Show("设置默认路径并勾选要打开的文件或直接打开文件。");
                }

            }
        }
        /// <summary>
        ///   按钮- 说明
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 说明ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("如果使用中遇到问题可以通过邮件联系我：\r\n联系方法1:1610109605@qq.com\r\n联系方法2:cnchenkaige@foxmail.com", "说明");
        }
        /// <summary>
        ///  按钮-关闭当前打开的文件。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tips();
            gen.keyValues.Clear();
            if (comboBox2.DataSource == null && comboBox1.DataSource == null && flowLayoutPanel1.Controls == null)
            {

            }
            else
            {
                // 将各项清空。
                comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
                comboBox2.SelectedIndexChanged -= comboBox2_SelectedIndexChanged;
                comboBox2.DataSource = null;
                comboBox1.DataSource = null;
                comboBox2.SelectedIndexChanged += comboBox2_SelectedIndexChanged;
                comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
                comboBox3.Enabled = true;
                flowLayoutPanel1.Controls.Clear();
                listDrow.Clear();
                listDrowCN.Clear();
            }



        }
        /// <summary>
        ///  按钮-退出程序。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tips();
            Environment.Exit(0);
        }
        /// <summary>
        ///  检索下拉框的文本。
        /// </summary>
        /// <param name="sender">触发者。</param>
        /// <param name="e">事件。</param>
        /// 
        private void retrieval(object sender, EventArgs e)
        {
            // 减少事件，当选着项变更时不进行判断。
            comboBox1.SelectedIndexChanged -= comboBox1_SelectedIndexChanged;
            // 清空数据源。
            comboBox1.DataSource = null;
            // 获取输入的文本并将其转换为大写的形式。
            string input = comboBox1.Text.ToUpper();
            // 清空下拉列表。
            comboBox1.Items.Clear();
            if (string.IsNullOrEmpty(input))
            {
                // 如果为空字符，那么就将数据源添加进去。
                comboBox1.Items.AddRange(listDrowCN.ToArray());
            }
            else
            {
                // 将查找到的包含该文本的项填充进该数组。
                string[] temporary = listDrowCN.Where(x => x.IndexOf(input, StringComparison.CurrentCultureIgnoreCase) != -1).ToArray();
                // 给数组赋值。
                comboBox1.Items.AddRange(temporary);
            }
            // 设置选中第一个。
            comboBox1.Select(comboBox1.Text.Length, 0);
            // 打开下拉框。
            comboBox1.DroppedDown = true;
            // 将鼠标设置为默认。
            Cursor = Cursors.Default;
            comboBox1.Items.AddRange(listDrowCN.ToArray());
            // 减少事件，当选着项变更时不进行判断。
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
        }
        #endregion

    }
}
