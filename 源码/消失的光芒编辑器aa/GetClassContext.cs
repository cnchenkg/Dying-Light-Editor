using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace 消失的光芒编辑器aa
{
    /// <summary>
    ///  获取属性或字段。
    /// </summary>
    class GetClassContext
    {
        /// <summary>
        ///  存放读取出来的字段或者属性。
        /// </summary>
      private  List<string> getConctext = new List<string>();
        /// <summary>
        ///  反射对象变量，
        /// </summary>
        private PropertyInfo[] fields=null;
        /// <summary>
        ///  存放该类对象的变量。
        /// </summary>
        private static GetClassContext getClassContext = null;
        /// <summary>
        ///  私有构造方法。
        /// </summary>
        private GetClassContext()
        {

        }
        /// <summary>
        ///  实例化类。如果被实例化了，那么直接返回这个对象。
        /// </summary>
        /// <returns>返回对象。</returns>
        public static GetClassContext GetInstance()
        {
            if (getClassContext==null)
            {
                getClassContext = new GetClassContext();
            }
            return getClassContext;
        }
        /// <summary>
        ///  获取类中的字段。
        /// </summary>
        /// <param name="category">要获取的类。</param>
        /// <returns>返回字段列表。</returns>
        public List<string> GetClassProperties(object category)
        {
            fields = category.GetType().GetProperties(BindingFlags.Public|BindingFlags.Instance);
            if (fields.Length<=0)
            {
                return getConctext;
            }
            foreach (PropertyInfo field in fields)
            {
                // 判断当前对象是否是值类型或开头是string。
                if (field.PropertyType.IsValueType||field.PropertyType.Name.StartsWith("String"))
                {
                    getConctext.Add(field.Name);
                }
            }
            return getConctext;

        }
    }
}
