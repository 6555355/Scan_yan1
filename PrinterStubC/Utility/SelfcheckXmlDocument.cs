using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Xml;
using BYHXPrinterManager;

namespace PrinterStubC.Utility
{
    /// <summary>
    /// 在SelfcheckXmlDocument基础上增加了hash校验文件完整性
    /// </summary>
    public class SelfcheckXmlDocument : XmlDocument
    {
        public bool Load(string filename)
        {
            try
            {
                bool fileInUse = false;
                for (int i = 0; i < 10; i++)
                {
                    fileInUse = PubFunc.IsFileInUse(filename, FileAccess.Read);
                    if (fileInUse)
                    {
                        Thread.Sleep(50);
                        continue;
                    }
                    break;
                }
                FileStream stream = new FileStream(filename,FileMode.Open,FileAccess.Read,FileShare.Read);
                StreamReader textWriter = new StreamReader(stream);
                string strHash = textWriter.ReadLine();
                if (strHash != null && (strHash.StartsWith("<Hash") && strHash.EndsWith("</Hash>"))) //包含哈希码且格式完整
                {
                    string strOuterxml = textWriter.ReadToEnd();
                    textWriter.Close();
                    stream.Close();
                    if (strHash.StartsWith("<Hash CRCVers=\"V1.0\""))
                    {
                        string fileHash = GetHashString(GetCRC32(strOuterxml));
                        if ((!string.IsNullOrEmpty(strOuterxml) &&
                             string.Equals(strHash, fileHash)))
                        {
                            LoadXml(strOuterxml);
                        }
                        else
                        {
                            LogWriter.SaveOptionLog(string.Format("文件不完整!![{0}]", filename));
                            MessageBox.Show(string.Format("文件不完整!![{0}]", filename));
                            return false;
                        }
                    }
                    else
                    {
                        LoadXml(strOuterxml);
                    }
                }
                else
                {
                    textWriter.Close();
                    stream.Close();
                    base.Load(filename);
                }
                return true;
            }
            catch (Exception ex)
            {
                LogWriter.SaveOptionLog(string.Format("{1}[{0}]", filename, ex.Message));
                MessageBox.Show(string.Format("{1}[{0}]", filename, ex.Message));
                return false;
            }
        }

        public override void Save(string filename)
        {
            try
            {
                bool fileInUse = false;
                for (int i = 0; i < 10; i++)
                {
                    fileInUse = PubFunc.IsFileInUse(filename, FileAccess.ReadWrite);
                    if (fileInUse)
                    {
                        Thread.Sleep(50);
                        continue;
                    }
                    break;
                }
                FileStream stream = new FileStream(filename,FileMode.Create,FileAccess.Write,FileShare.None);
                XmlTextWriter xmlWriter = new XmlTextWriter(stream, Encoding.UTF8);
                xmlWriter.Formatting = Formatting.Indented;
                xmlWriter.WriteStartElement("Hash");
                xmlWriter.WriteAttributeString("CRCVers", "V1.0");
                xmlWriter.WriteString(GetCRC32(OuterXml));
                xmlWriter.WriteEndElement();
                this.WriteContentTo(xmlWriter);
                xmlWriter.Close();
                stream.Close();
                //int hashcode = this.OuterXml.GetHashCode();
                //StreamWriter textWriter = new StreamWriter(filename, false);
                //// 插入哈希码
                //textWriter.WriteLine(GetHashString(GetCRC32(OuterXml)));
                ////写入xml内容
                //textWriter.Write(OuterXml);
                //textWriter.Flush();
                //textWriter.Close();
            }
            catch (Exception ex)
            {
                LogWriter.SaveOptionLog(string.Format("{1}[{0}]", filename, ex.Message));
                MessageBox.Show(string.Format("{1}[{0}]", filename, ex.Message));
            }
        }

        private string GetHashString(string hashcode)
        {
            return string.Format("<Hash CRCVers=\"V1.0\">{0}</Hash>", hashcode);
        }
        private static ulong[] _crc32Table ;
        private static bool _bCrc32TableCreated = false;
        static public void GetCRC32Table()
        {
            if (_bCrc32TableCreated)
                return;
            ulong Crc;
            _crc32Table = new ulong[256];
            int i, j;
            for (i = 0; i < 256; i++)
            {
                Crc = (ulong)i;
                for (j = 8; j > 0; j--)
                {
                    if ((Crc & 1) == 1)
                        Crc = (Crc >> 1) ^ 0xEDB88320;
                    else
                        Crc >>= 1;
                }
                _crc32Table[i] = Crc;
            }
            _bCrc32TableCreated = true;
        }


        public static string GetCRC32(string input)
        {
            string Texts = "";
            string pattern = "(?<=>).*?(?=<)";
            Regex regex=new Regex(pattern);
            Texts = regex.Replace(input, string.Empty);
            Texts = Regex.Replace(Texts, @"[\r\n]", "");
            Texts = Regex.Replace(Texts, " ", ""); 
            GetCRC32Table();
            System.Text.ASCIIEncoding cvt = new System.Text.ASCIIEncoding();
            byte[] bytes = cvt.GetBytes(Texts.Trim());
            int iCount = bytes.Length;
            //  1 0 1 0  0 1 1 1  0 0 0 1  1 0 1 0  0 1 0 0  0 0 1 0  0 0 1 1  0 1 0 1
            //  a  7  1  a  4  2  3  
            ulong crc = 0xa71a423;
            string crc2 = "";
            crc = crc ^ 0xffffffff;
            for (int i = 0; i < iCount; i++)
            {
                crc = _crc32Table[((int)crc ^ bytes[i]) & 0xFF] ^ (crc >> 8);
                ulong temp = crc ^ 0xffffffffL;
                crc2 = String.Format("{0:X00000000}", temp);
            }
            return crc2;
        }
    }
}
