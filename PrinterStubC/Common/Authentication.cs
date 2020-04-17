using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Xml;

namespace PrinterStubC.Common
{
    public class Authentication
    {
        /// <summary>
        /// 更新或者添加认证信息；
        /// </summary>
        /// <param name="person"></param>
        /// <param name="time"></param>
        /// <param name="company"></param>
        /// <param name="path">路径</param>
        /// <param name="fand">true更新，false添加</param>
        public static void WriteXml(string person, DateTime time, string company, string path, bool fand)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            if (fand)
            {
                XmlNode selectSingleNode = xmlDoc.SelectSingleNode("/VenderDisplay/Authentication");
                if (selectSingleNode != null)
                {
                    selectSingleNode.InnerText = MakeEncryptContext(xmlDoc, person, time, company);
                }
            }
            else
            {
                XmlNode root = xmlDoc.SelectSingleNode("VenderDisplay");
                XmlElement authentication = xmlDoc.CreateElement("Authentication");
                authentication.InnerText = MakeEncryptContext(xmlDoc,person,time,company);
                root.AppendChild(authentication);
            }
            xmlDoc.Save(path);
        }
        /// <summary>
        /// 生成加密后的密文内容
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="person"></param>
        /// <param name="time"></param>
        /// <param name="company"></param>
        /// <returns></returns>
        private static string MakeEncryptContext(XmlDocument xmlDoc, string person, DateTime time, string company)
        {
            string xmlStr = string.Empty;
            XmlElement xel;
            xel = xmlDoc.CreateElement("Person");
            xel.InnerText = person;
            xmlStr += xel.OuterXml;
            xel = xmlDoc.CreateElement("Time");
            xel.InnerText = time.ToBinary().ToString();
            xmlStr += xel.OuterXml;
            xel = xmlDoc.CreateElement("Company");
            xel.InnerText = company;
            xmlStr += xel.OuterXml;
            return Encrypt(xmlStr, key);
        }

        /// <summary>
        /// 读取认证文件，返回Dictionary类型的认证信息；TKey为Person,Time,Company
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Dictionary<string,string> ReadXml(string path)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            XmlNode xnAut = xmlDoc.SelectSingleNode("/VenderDisplay/Authentication");
            if (xnAut != null)
            {
                xnAut.InnerXml = Decrypt(xnAut.InnerText, key);
                if (xnAut != null)
                {
                    XmlNodeList nls = xnAut.ChildNodes;
                    if (nls != null)
                    {
                        foreach (XmlNode xn in nls)
                        {
                            XmlElement xe = (XmlElement)xn;
                            switch (xe.Name)
                            {
                                case "Person":
                                    dic.Add("Person", xe.InnerText);
                                    break;
                                case "Time":
                                    dic.Add("Time", xe.InnerText);
                                    break;
                                case "Company":
                                    dic.Add("Company", xe.InnerText);
                                    break;
                            }
                        }
                    }
                }
            }
            return dic;
        }
        /// <summary>
        /// 删除认证文件中认证部分的节点；
        /// </summary>
        /// <param name="path"></param>
        public static void DeleteXml(string path)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);
            var root = xmlDoc.DocumentElement;
            var element = xmlDoc.SelectSingleNode("/VenderDisplay/Authentication");
            if (element != null)
            {
                root.RemoveChild(element);
                xmlDoc.Save(path);
            }
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="encryptStr">明文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string Encrypt(string encryptStr, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(encryptStr);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string key = "ae125efkk4454eeff444ferfkny6oxi8";
        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="decryptStr">密文</param>
        /// <param name="key">密钥</param>
        /// <returns></returns>
        public static string Decrypt(string decryptStr, string key)
        {
            byte[] keyArray = UTF8Encoding.UTF8.GetBytes(key);
            byte[] toEncryptArray = Convert.FromBase64String(decryptStr);
            RijndaelManaged rDel = new RijndaelManaged();
            rDel.Key = keyArray;
            rDel.Mode = CipherMode.ECB;
            rDel.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = rDel.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        /// <summary>
        /// 根据认证文件和版本日期校验是否是经过认证的版本
        /// </summary>
        /// <param name="path">认证文件路径</param>
        /// <param name="versionDt">软件版本编译日期时间</param>
        /// <returns></returns>
        public static bool HasAuthenticationed(string path,DateTime versionDt)
        {
            try
            {
                var ret = ReadXml(path);
                if (ret.Count > 0 && ret.Keys.Contains("Time"))
                {
                    DateTime authDt = DateTime.FromBinary(long.Parse(ret["Time"]));
                    return (authDt > versionDt); //授权时间要晚于版本生成时间
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
