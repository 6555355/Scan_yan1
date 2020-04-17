// -----------------------------------------------------------------------
// <copyright file="SerializationUnit.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BYHXPrinterManager.TcpIp
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Xml.Serialization;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class SerializationUnit
    {
        #region 二进制序列化

        /// <summary>
        /// 把对象序列化为字节数组
        /// </summary>
        public static byte[] SerializeObject(object obj)
        {
            if (obj == null) return null;
            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(ms, obj);
            ms.Position = 0;
            byte[] bytes = new byte[ms.Length];
            ms.Read(bytes, 0, bytes.Length);
            ms.Close();
            return bytes;
        }

        /// <summary>
        /// 把字节数组反序列化成对象
        /// </summary>
        public static object DeserializeObject(byte[] bytes)
        {
            object obj = null;
            if (bytes == null) return obj;
            MemoryStream ms = new MemoryStream(bytes);
            ms.Position = 0;
            BinaryFormatter formatter = new BinaryFormatter();
            obj = formatter.Deserialize(ms);
            ms.Close();
            return obj;
        }

        #endregion

        #region XML序列化

        /// <summary>   
        /// 序列化对象   
        /// </summary>   
        /// <typeparam name="T">对象类型</typeparam>   
        /// <param name="t">对象</param>   
        /// <returns></returns>   
        public static string Serialize(object t,Type type)
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer xz = new XmlSerializer(type);                
                xz.Serialize(sw, t);
                return sw.ToString();
            }

        }



        /// <summary>   
        /// 反序列化为对象   
        /// </summary>   
        /// <param name="type">对象类型</param>   
        /// <param name="s">对象序列化后的Xml字符串</param>   
        /// <returns></returns>   
        public static object Deserialize(Type type, string s)
        {
            using (StringReader sr = new StringReader(s))
            {
                XmlSerializer xz = new XmlSerializer(type);
                return xz.Deserialize(sr);
            }

        }

        #endregion

        #region struct<=>byte[] by Marshal

        /// <summary>
        /// 结构体转byte数组
        /// </summary>
        /// <param name="struvtObj">要转换的结构体</param>
        /// <returns>转换后的数组</returns>
        public static byte[] StructToBytes(object struvtObj)
        {
            //得到结构体大小
            int size = Marshal.SizeOf(struvtObj);
            //创建byte数组
            byte[] bytes = new byte[size];
            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(struvtObj, structPtr, true);
            //从内存空间拷到byte数组
            Marshal.Copy(structPtr, bytes, 0, size);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);

            //返回byte数组
            return bytes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object BytesToStruct(byte[] bytes, Type type)
        {
            //得到结构体大小
            int size = Marshal.SizeOf(type);
            //byte数组长度小于结构体的大小
            if (size > bytes.Length)
            {
                //返回空
                return null;
            }
            object obj = null;
            //分配结构体大小的内存空间
            IntPtr structPtr = Marshal.AllocHGlobal(size);
            //将byte数组拷到分配好的内存空间
            Marshal.Copy(bytes, 0, structPtr, size);
            //将内存空间转换为目标结构体
            obj = Marshal.PtrToStructure(structPtr, type);
            //释放内存空间
            Marshal.FreeHGlobal(structPtr);

            //返回byte数组
            return obj;
        }

#endregion
    }
}
