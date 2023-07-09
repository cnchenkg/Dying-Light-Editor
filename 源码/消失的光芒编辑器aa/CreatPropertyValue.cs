using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 消失的光芒编辑器aa
{
    /// <summary>
    ///  创建属性值类。
    /// </summary>
    class CreatPropertyValue
    {
        /// <summary>
        ///  根据传入字典获取指定的字典值，既属性值。
        /// </summary>
        /// <param name="type">获取的类型。</param>
        /// <param name="name">类型中的那个值。</param>
        /// <param name="keyValuePairs">字典。</param>
        /// <returns>返回字典。</returns>
        public Dictionary<string, string> GetPropertyValue(string type,string name,Dictionary<string, Dictionary<string, Dictionary<string, string>>> keyValuePairs)
        {
            if (keyValuePairs.Count>0)
            {
                Dictionary<string, string> result = new Dictionary<string, string>();
                //
                keyValuePairs[type].TryGetValue(name, out result);
                return result;
            }
            return null;
        }
    }
}
