using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace MultimediaLayout.Utilities
{
    public class SerializationUtility
    {
        /// <summary>
        /// 将对象序列化到文件
        /// </summary>
        /// <param name="FilePath">文件(支持绝大多数数据类型)</param>
        /// <param name="obj">要序列化的对象(如哈希表,数组等等)</param>
        public static void FileSerialize(string FilePath, object obj)
        {
            try
            {
                FileStream fs = new FileStream(FilePath, FileMode.Create);
                BinaryFormatter sl = new BinaryFormatter();
                sl.Serialize(fs, obj);
                fs.Close();
            }
            catch
            {
                Console.WriteLine("序列化存储失败！");
            }

        }

        /// <summary>
        /// 将文件反序列化
        /// </summary>
        /// <param name="FilePath">文件路径(必须是经过当前序列化后的文件)</param>
        /// <returns>返回 null 表示序列反解失败或者目标文件不存在</returns>
        public static object FileDeSerialize(string FilePath)
        {
            if (System.IO.File.Exists(FilePath))
            {
                try
                {
                    FileStream fs = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                    BinaryFormatter sl = new BinaryFormatter();
                    object obg = sl.Deserialize(fs);
                    fs.Close();
                    return obg;
                }
                catch
                {
                    return null;
                }

            }
            else
            {
                return null;
            }

        }
    }
}
