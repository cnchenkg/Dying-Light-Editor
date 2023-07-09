using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace 消失的光芒编辑器aa
{
    /// <summary>
    ///  类型工厂。
    /// </summary>
    class CategoryFactory
    {
        /// <summary>
        ///  存放该类型对象。
        /// </summary>
        private static CategoryFactory categoryFactory = null;
        /// <summary>
        ///  私有构造方法。
        /// </summary>
        private CategoryFactory()
        {

        }
        /// <summary>
        ///  单例对象。
        /// </summary>
        /// <returns>返回当前对象。</returns>
        public static CategoryFactory GetInstance()
        {
            if (categoryFactory == null)
            {
                categoryFactory = new CategoryFactory();
            }
            return categoryFactory;
        }
        /// <summary>
        ///  向对象指定属性添加值。
        /// </summary>
        /// <param name="name">属性名。</param>
        /// <param name="category">要添加的对象。</param>
        /// <param name="value">值。</param>
        /// <returns>返回有值属性名。</returns>
        public void Attach(string name, CategoryType category, string value)
        {
            PropertyInfo[] properties = category.GetType().GetProperties();
            foreach (PropertyInfo item in properties)
            {
                if (item.Name.Equals(name))
                {
                    item.SetValue(category, value);
                   
                }
            }
        }
    }
}
