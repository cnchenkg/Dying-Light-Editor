using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 消失的光芒编辑器aa
{
    /// <summary>
    ///  静态类，读写ini配置文件。
    /// </summary>
    class ReadWriteIni
    {
        /// <summary>
        ///  存放截取出来的字符串变量。
        /// </summary>
        private static string extract;
        /// <summary>
        ///  配置文件名。
        /// </summary>
        private static string iniFileName = Application.StartupPath + "\\Config\\config.ini";
        /// <summary>
        ///  将指定的键和值写入到指定节点中，如果存在则替换掉，不存在则创建新的。
        /// </summary>
        /// <param name="section">写入节点。</param>
        /// <param name="key">键。</param>
        /// <param name="val">值。</param>
        /// <param name="filePath">写入路径。</param>
        /// <returns>返回写入结果。</returns>
        [DllImport("kernel32", CharSet = CharSet.Unicode)]// 返回0表示失败，相反成功。
        private static extern uint WritePrivateProfileString(string section, string key, string val, string filePath);
        /// <summary>
        ///  读取指定路径指定节点下的键值对。
        /// </summary>
        /// <param name="section">读取节点。</param>
        /// <param name="key">键。</param>
        /// <param name="def">如果没有指定的键，默认使用的值。</param>
        /// <param name="retVal">返回的sb型字符串。</param>
        /// <param name="size">读取内容大小。</param>
        /// <param name="filePath">读取路径。</param>
        /// <returns>返回读取长度。</returns>
        [DllImport("kernel32", CharSet = CharSet.Unicode)]// 返回取得给定字符串缓冲区的长度。
        private static extern uint GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);
        /// <summary>
        ///  读取指定路径指定节点下的键值对数组类型。
        /// </summary>
        /// <param name="section">要读取的节点。 如果此参数为 NULL， GetPrivateProfileString 函数会将文件中的所有节名称复制到提供的缓冲区。</param>
        /// <param name="key">要读取的键。如果此参数为 NULL，则将 lpAppName 参数指定的节中的所有键名称复制到 lpReturnedString 参数指定的缓冲区。</param>
        /// <param name="def">找不到的话，默认返回值。</param>
        /// <param name="lpReturnedString">接受找到键值数据的的缓冲区。</param>
        /// <param name="size">缓冲区大小。</param>
        /// <param name="filePath">文件路径。</param>
        /// <returns>返回值是复制到缓冲区的字符数，不包括终止 null 字符。
        /// 如果键值都不是 NULL ，并且提供的目标缓冲区太小，无法保存请求的字符串，则字符串将被截断，后跟 null 字符，并且返回值等于 nSize 减一。
        /// 如果 键 或 值 为NULL ，并且提供的目标缓冲区太小，无法容纳所有字符串，则最后一个字符串将被截断，后跟两个 null 字符。 在这种情况下，返回值等于 nSize 减 2。
        /// 如果找不到 lpFileName 指定的初始化文件或包含无效值，则调用 GetLastError 将返回“0x2” (找不到文件) 。 若要检索扩展的错误信息，请调用 GetLastError。</returns>
        [DllImport("kernel32", CharSet = CharSet.Unicode)]// 返回取得给定字符串缓冲区的长度。
        private static extern uint GetPrivateProfileString(string section, string key, string def, byte[] lpReturnedString, int size, string filePath);
        /// <summary>
        ///  获取给顶ini文件中所有节点的名称。
        /// </summary>
        /// <param name="lpszReturnBuffer">缓冲区，指向与存放该ini文件所有节点的数据的临时缓冲区。
        /// 缓冲区用一个或多个null结尾。最后的字符串后的null为第二个null。</param>
        /// <param name="nSize">临时缓冲区的大小。</param>
        /// <param name="lpFileName">ini文件路径。</param>
        /// <returns>返回读取上来的字节总数，不包含null，如果缓冲区不够大，则返回值为缓冲区-2的值。</returns>
        [DllImport("kernel32", CharSet = CharSet.Unicode)]// 获得ini文件所有的节点名。
        private static extern uint GetPrivateProfileSectionNames(byte[] lpReturnedString, uint nSize, string lpFileName);
        /// <summary>
        ///  获取指定ini文件下指定节点下的所有键值对。
        /// </summary>
        /// <param name="lpAppName">指定节点名。</param>
        /// <param name="lpReturnedString">缓冲区，指向与存放该ini文件所有节点的数据的临时缓冲区。
        /// 缓冲区用一个或多个null结尾。最后的字符串后的null为第二个null。</param>
        /// <param name="nSize">临时缓冲区大小。</param>
        /// <param name="lpFileName">ini文件路径。</param>
        /// <returns>返回读取上来的字节总数，不包含null，如果缓冲区不够大，则返回值为缓冲区-2的值。</returns>
        [DllImport("kernel32", CharSet = CharSet.Unicode)]// 获得ini文件指定节点名下的所有键与值。
        private static extern uint GetPrivateProfileSection(string lpAppName, byte[] lpReturnedString, uint nSize, string lpFileName);
        /// <summary>
        ///  将键值对内容写入指定路径指定文件中的指定节点。
        /// </summary>
        /// <param name="section">写入节点。</param>
        /// <param name="key">键。</param>
        /// <param name="val">值。</param>
        /// <param name="filePath">写入路径</param>
        public static void iniWrite(string section, string key, string val, string filePath)
        {
            // 判断文件路径是否存在。
            operationPath(filePath);
            WritePrivateProfileString(section, key, val, filePath);
        }
        /// <summary>
        ///  将键值对内容写入应用目录文件下的指定文件中的指定节点。
        /// </summary>
        /// <param name="section">写入节点。</param>
        /// <param name="key">键。</param>
        /// <param name="val">值。</param>
        public static void iniWrite(string section, string key, string val)
        {
            // 判断文件路径是否存在。
            operationPath(iniFileName);
            // 写入文件。
            WritePrivateProfileString(section, key, val, iniFileName);
        }
        /// <summary>
        ///  读取默认路径既，程序目录下指定节点下的指定键的值。
        /// </summary>
        /// <param name="section">指定节点。</param>
        /// <param name="key">指定键。</param>
        /// <param name="defaultValue">如果没有此键默认使用的值。</param>
        /// <returns>返回读取结果。</returns>
        public static string iniRead(string section, string key, string defaultValue)
        {
            if (string.IsNullOrEmpty(section) || string.IsNullOrEmpty(key))
            {
                return defaultValue;
            }
            if (!File.Exists(iniFileName))
            {
                return "文件目录不存在或文件不存在。";
            }
            const int size = 1024 * 10;
            StringBuilder sb = new StringBuilder(1024);
            uint result = GetPrivateProfileString(section, key, defaultValue, sb, size, iniFileName);
            if (result == 0)
            {
                return defaultValue;
            }
            return sb.ToString();
        }
        /// <summary>
        ///  读取指定路径下的，指定节点下的指定键的值。
        /// </summary>
        /// <param name="section">指定节点。</param>
        /// <param name="key">指定键。</param>
        /// <param name="defaultValue">如果没有此键默认使用的值。</param>
        /// <param name="filePath">指定文件路径。</param>
        /// <returns>返回读取结果。</returns>
        public static string iniRead(string section, string key, string filePath, string defaultValue)
        {

            if (string.IsNullOrEmpty(section) || string.IsNullOrEmpty(key))
            {
                return defaultValue;
            }
            if (!File.Exists(iniFileName))
            {
                return "文件目录不存在或文件不存在。";
            }
            const int size = 1024 * 10;
            StringBuilder sb = new StringBuilder(1024);
            uint result = GetPrivateProfileString(section, key, defaultValue, sb, size, filePath);
            if (result == 0)
            {
                return defaultValue;
            }
            return sb.ToString();
        }
        /// <summary>
        ///  返回指定ini路径下的所有节点名。
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>节点名数组。</returns>
        public static string[] FileSectionNames(string path)
        {
            //// 缓冲区大小。
            //uint buffer = 32767;
            // 接收读取出来的节点名。
            string[] result = new string[0];
            //　分配内存，大小为buffer*2的大小。
            // StringBuilder returnBuffer = new StringBuilder((int)buffer*2);
            byte[] buffer = new byte[32767 * 2];
            // 读取文件。
            uint readByteLength = GetPrivateProfileSectionNames(buffer, 32767, iniFileName);
            if (readByteLength != 0)
            {
                // 从内存中将数据读取出来并转为字符串数组。
                // result = Marshal.PtrToStringAuto(returnBuffer, (int)readByteLength).Split(new char[] { '\0' },StringSplitOptions.RemoveEmptyEntries);
                result = Encoding.Unicode.GetString(buffer, 0, ((int)readByteLength) * 2).Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }
            // Marshal.FreeCoTaskMem(returnBuffer);
            return result;

        }
        /// <summary>
        ///  获取指定路径下指定节点中所有键值对。
        /// </summary>
        /// <param name="section">要获取的节点。</param>
        /// <param param="path">要获取的路径。</param>
        /// <returns>返回结果</returns>
        public static string[] getValue(string section,string path)
        {
            //// 缓冲区大小。
            //uint buffer = 32767;
            // 接收读取出来的节点名。
            string[] result = new string[0];
            //　分配内存，大小为buffer*2的大小。
            // StringBuilder returnBuffer = new StringBuilder((int)buffer*2);
            byte[] buffer = new byte[32767 * 2];
            // 读取文件。
            uint readByteLength = GetPrivateProfileSection(section, buffer, 32767, path);
            if (readByteLength != 0)
            {
                // 从内存中将数据读取出来并转为字符串数组。
                // result = Marshal.PtrToStringAuto(returnBuffer, (int)readByteLength).Split(new char[] { '\0' },StringSplitOptions.RemoveEmptyEntries);
                result = Encoding.Unicode.GetString(buffer, 0, ((int)readByteLength) * 2).Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }
            // Marshal.FreeCoTaskMem(returnBuffer);
            return result;
        }
        /// <summary>
        ///  获取默认路径下指定节点中所有键值对。
        /// </summary>
        /// <param name="section">要获取的节点。</param>
        /// <returns>返回结果</returns>
        public static string[] getValue(string section)
        {
            //// 缓冲区大小。
            //uint buffer = 32767;
            // 接收读取出来的节点名。
            string[] result = new string[0];
            //　分配内存，大小为buffer*2的大小。
            // StringBuilder returnBuffer = new StringBuilder((int)buffer*2);
            byte[] buffer = new byte[32767 * 2];
            // 读取文件。
            uint readByteLength = GetPrivateProfileSection(section, buffer, 32767, iniFileName);
            if (readByteLength != 0)
            {
                // 从内存中将数据读取出来并转为字符串数组。
                // result = Marshal.PtrToStringAuto(returnBuffer, (int)readByteLength).Split(new char[] { '\0' },StringSplitOptions.RemoveEmptyEntries);
                result = Encoding.Unicode.GetString(buffer, 0, ((int)readByteLength)*2).Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            }
            // Marshal.FreeCoTaskMem(returnBuffer);
            return result;
        }
        /// <summary>
        ///  获取指定路径下指定节点夏所有键值对。
        /// </summary>
        /// <param name="section">要获取的节点。</param>
        /// <param name="path">文件路径。</param>
        /// <returns>返回获取的键值对。</returns>
        public static Dictionary<string, string> getSectionPairs(string section,string path)
        {
            // 返回键值对字典。
            Dictionary<string, string> temporary = new Dictionary<string, string>();
            // 接收读取出来的节点名。
            string[] result = new string[0];
            // 存放所有的值
            String resultValue;
            //　分配内存，大小为buffer*2的大小。
            // StringBuilder returnBuffer = new StringBuilder((int)buffer*2);
            byte[] buffer = new byte[32767 * 3];
            // 读取文件。接受读取长度。
            uint readByteLength = GetPrivateProfileString(section, null,null, buffer, 32767*2, path);
            if (readByteLength>0)
            {
                // 将获取到的所有键转为字符串数组。
                result = Encoding.Unicode.GetString(buffer, 0, ((int)readByteLength)*2).Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
                
                // 循环赋值。
                for (int i = 0; i < result.Length; i++)
                {
                    // 方法，获取指定节点下的指定键的值。
                    readByteLength = GetPrivateProfileString(section, result[i], null, buffer, 32767, path);
                    // 乘2的原因是返回的字符串长度，比实际读取到的字节少2倍。
                    resultValue = Encoding.Unicode.GetString(buffer, 0, ((int)readByteLength)*2);
                    // 判断当前键有无添加到字典中。
                    if (!temporary.ContainsKey(result[i]))
                    {
                        // 添加到字典中。
                        temporary.Add(result[i], resultValue);
                    }
                    
                }
            }
            return temporary;
        }

        /// <summary>
        ///  操作文件路径如果存在则不管否则将创建。
        /// </summary>
        /// <param name="path">路径。</param>
        private static void operationPath(string path)
        {
            try
            {
                // 判断文件是否包含ini后缀。
                if (path.Contains(".ini"))
                {
                    // 获取文件路径。
                    extract = ExtractString.ExtractContent(path, "\\", 0);
                    // 判断文件路径是否存在。
                    if (!Directory.Exists(extract))
                    {
                        // 创建路径。
                        Directory.CreateDirectory(extract);
                    }
                    // 判断文件是否存在。
                    if (!File.Exists(path))
                    {
                        // 创建文件。
                        using (FileStream fs = new FileStream(path, FileMode.CreateNew))
                        {
                        }

                    }
                }
                
            }
            catch (Exception)
            {


            }


        }
    }
}
