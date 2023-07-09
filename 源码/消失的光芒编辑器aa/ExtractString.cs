using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 消失的光芒编辑器aa
{
    /// <summary>
    ///  截取指定字符串类。
    /// </summary>
    static class  ExtractString
    {
        ///// <summary>
        /////  该对象的变量。
        ///// </summary>
        //private static ExtractString extract = null;
        ///// <summary>
        /////  私有构造函数。
        ///// </summary>
        //private ExtractString()
        //{
        //}
        ///// <summary>
        /////  单例截取类。
        ///// </summary>
        ///// <returns>返回对象。</returns>
        //public static ExtractString GetInstance()
        //{
        //    if (extract == null)
        //    {
        //        extract = new ExtractString();
        //    }
        //    return extract;
        //}

        /// <summary>
        /// 传入读取的内容进行截取，根据""截取它们两个中间的内容
        /// </summary>
        /// <param name="wantExtrantContent">要截取的内容 want 要 extrant 截取摘录 content 内容</param>
        /// <returns>返回截取结果</returns>
        public static string ExtractContent(string wantExtrantContent)
        {
            try
            {
                //进行验证的
                //MessageBox.Show(")");
                //读取指定长度，即开始位为第一次出现的位置，结束位为最后一次出现的为止减开始位
                return wantExtrantContent.Substring(wantExtrantContent.IndexOf("\"") + 1,
                    (wantExtrantContent.IndexOf("\")")) - ((wantExtrantContent.IndexOf("\"") + 1)));
            }
            catch (Exception)
            {
                return null;
                // MessageBox.Show("不要传入非法内容哦哦哦哦");

                throw;
            }
        }
        /// <summary>
        /// 根据指定分割符截取内容，将截取起始位与结束位中间的内容
        /// 将截取第一次出现开始分割符的地方与第一次出现结束分割符之间的内容
        /// </summary>
        /// <param name="wantExtrantContent">要截取的内容 want 要 extrant 截取摘录 content 内容</param>
        /// <param name="startSize">起始位分隔符，要截取的内容 want 要 extrant 截取摘录 content 内容</param>
        /// <param name="endSize">结束位分隔符要截取的内容 want 要 extrant 截取摘录 content 内容</param>
        /// <returns>返回截取结果</returns>
        public static string ExtractContent(string wantExtrantContent, string startSize, string endSize)
        {
            try
            {
                //进行验证的
                //MessageBox.Show(")");
                //读取指定长度，即开始位为第一次出现的位置，结束位为最后一次出现的为止减开始位
                return wantExtrantContent.Substring(wantExtrantContent.IndexOf(startSize) + 1,
                    (wantExtrantContent.IndexOf(endSize) - (wantExtrantContent.IndexOf(startSize) + 1))).Trim();
            }
            catch (Exception)
            {
                return null;
                // MessageBox.Show("不要传入非法内容哦哦哦哦");

                // throw;
            }
        }
        /// <summary>
        /// 截取开始分隔符与结束分割符之间的内容
        /// 开分割符计数指第一次出现的位置计算。
        /// 结束分割符是指最后一次出现的地方计数
        /// </summary>
        /// <param name="wantExtrantContent">要分割的字符</param>
        /// <param name="startSize">开始分割位置分割符号</param>
        /// <param name="endSize">结束分割位置分割符号</param>
        /// <param name="station">占位符号无作用</param>
        /// <returns>返回分割后的值</returns>
        public static string ExtractContent(string wantExtrantContent, string startSize, string endSize, int station)
        {
            try
            {
                //进行验证的
                //MessageBox.Show(")");
                //读取指定长度，即开始位为第一次出现的位置，结束位为最后一次出现的为止减开始位
                return wantExtrantContent.Substring(wantExtrantContent.IndexOf(startSize) + 1,
                    (wantExtrantContent.LastIndexOf(endSize) - (wantExtrantContent.IndexOf(startSize) + 1))).Trim();
            }
            catch (Exception)
            {
                return null;
                // MessageBox.Show("不要传入非法内容哦哦哦哦");

                // throw;
            }
        }
        /// <summary>
        /// 读取从0到指定分隔符位置之间的内容
        /// </summary>
        /// <param name="wantExtrantContent">要截取的字符串</param>
        /// <param name="stopSize">结束位分割符，要读取到的指定位置</param>
        /// <returns>读取结果</returns>
        public static string ExtractContent(string wantExtrantContent, string stopSize)
        {
            try
            {
                //进行验证的
                //MessageBox.Show(")");
                //读取从0开始到指定位置的文件
                return wantExtrantContent.Substring(0, wantExtrantContent.IndexOf(stopSize)).Trim();
            }
            catch (Exception)
            {
                return null;
                // MessageBox.Show("不要传入非法内容哦哦哦哦");

                // throw;
            }
        }
        /// <summary>
        ///  传入字符串，截取从0到最后一个分割符位置的值返回。
        /// </summary>
        /// <param name="wantExtrantContent">要截取的内容。</param>
        /// <param name="stopSize">停止位。</param>
        /// <param name="station">占位符，任意数字即可。</param>
        /// <returns>返回截取后的值。</returns>
        public static string ExtractContent(string wantExtrantContent, string stopSize, int station)
        {
            try
            {
                //进行验证的
                //MessageBox.Show(")");
                //读取从0开始到指定位置的文件
                return wantExtrantContent.Substring(0, wantExtrantContent.LastIndexOf(stopSize)).Trim();
            }
            catch (Exception)
            {
                return null;
                // MessageBox.Show("不要传入非法内容哦哦哦哦");

                // throw;
            }
        }
        /// <summary>
        /// 读取指定位置后的所有内容
        /// </summary>
        /// <param name="wantExtrantContent">要截取的字符串</param>
        /// <param name="startSize">截取结束的位置</param>
        /// <returns>读取结果</returns>
        public static string ExtractContent(string wantExtrantContent, int EndSize)
        {
            try
            {
                //进行验证的
                //MessageBox.Show(")");
                //读取从0开始到指定位置的文件
                return wantExtrantContent.Substring(EndSize, wantExtrantContent.Length - EndSize).Trim();
            }
            catch (Exception)
            {
                return null;
                // MessageBox.Show("不要传入非法内容哦哦哦哦");

                // throw;
            }
        }
    }
}
