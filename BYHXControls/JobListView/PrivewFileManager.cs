using System;
using System.Collections;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Diagnostics;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.JobListView
{
    public class PrivewFileManager
    {
        public struct previewForlderStruct:ICloneable
        {
            public string JobForlderPath;
            public string PreviewForlderName;
            public previewForlderStruct(string jobForlderPath, string previewForlderName)
            {
                this.JobForlderPath = jobForlderPath;
                this.PreviewForlderName = previewForlderName;
			}
			#region ICloneable ��Ա

			public object Clone()
			{
				// TODO:  ��� previewForlderStruct.Clone ʵ��
				return new previewForlderStruct((string)this.JobForlderPath.Clone(),(string)this.PreviewForlderName.Clone());
			}

			#endregion
		}

        private static ArrayList m_PreviewList = new ArrayList();
        private const string LISTFORLDERNAME = "PreviewForlder_";
        const string m_PreviewFolder = "Preview";
        const string m_JobListFile = "JobPreviewlist.xml";
        string sPreviewFolder = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder + Path.DirectorySeparatorChar;

        public static ArrayList PreviewList
        {
            get { return m_PreviewList; }
            set { m_PreviewList = value; }
        }

        public PrivewFileManager()
        {
            this.LoadListFromXML();
        }

        public bool LoadListFromXML()
        {
            string fileName = Application.StartupPath + Path.DirectorySeparatorChar + m_JobListFile;
            if (!File.Exists(fileName))
            {
                if (Directory.Exists(sPreviewFolder))
                {
                    foreach (string d in Directory.GetDirectories(sPreviewFolder))
                    {
                        modifyFile.DeleteFolder(d);
                    }
                }
                return false;
            }

            SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
            try
            {
               if(!doc.Load(fileName))
                   return false;
                XmlElement root = (XmlElement)doc.DocumentElement;
				m_PreviewList.Clear();

                foreach (XmlNode xn in root.ChildNodes)
                {
                    if (xn.Name == "PreviewForlder")
                    {
                        previewForlderStruct pfs = new previewForlderStruct();
                        pfs.JobForlderPath = xn.Attributes["JobForlderPath"].Value;
                        pfs.PreviewForlderName = xn.Attributes["PreviewForlderName"].Value;
                        if (!Directory.Exists(pfs.JobForlderPath))
                        {
                            if (Directory.Exists(sPreviewFolder + pfs.PreviewForlderName))
                                modifyFile.DeleteFolder(sPreviewFolder + pfs.PreviewForlderName);
                        }
                        else
                        {
                            m_PreviewList.Add(pfs);
                        }
                    }
                }
                if (Directory.Exists(sPreviewFolder))
                {
                    foreach (string d in Directory.GetDirectories(sPreviewFolder))
                    {
                        bool bDel = true;
                        foreach (previewForlderStruct pfs in m_PreviewList)
                        {
                            string[] subpath = d.Split(Path.DirectorySeparatorChar);
                            if (pfs.PreviewForlderName.ToLower() == subpath[subpath.Length - 1].ToLower())
                                bDel = false;
                        }
                        if (bDel == true)
                            modifyFile.DeleteFolder(d);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Debug.Assert(false, e.Message);
                return false;
            }
        }

        public bool SaveListToXML()
        {
            string fileName = Application.StartupPath + Path.DirectorySeparatorChar + m_JobListFile;

            SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
            bool success = true;
            try
            {
                XmlElement root = doc.CreateElement("PreviewForlderList");
                for (int i = 0; i < m_PreviewList.Count; i++)
                {
                    XmlNode Pforlder = doc.CreateElement("PreviewForlder");
                    Pforlder.Attributes.Append(doc.CreateAttribute("JobForlderPath")).Value = ((previewForlderStruct)m_PreviewList[i]).JobForlderPath;
                    Pforlder.Attributes.Append(doc.CreateAttribute("PreviewForlderName")).Value = ((previewForlderStruct)m_PreviewList[i]).PreviewForlderName;
                    root.AppendChild(Pforlder);
                }
                doc.AppendChild(root);
                doc.Save(fileName);
            }
            catch (Exception e)
            {
                success = false;

                Debug.Assert(false, e.Message + e.StackTrace);
            }

            return success;
        }

        public string Add(string strForlderName)
        {
            try
            {
                string subPrevewForderPath = string.Empty;
                if (this.Contains(strForlderName))
                {
					previewForlderStruct old = this.GetByForlderName(strForlderName);
					previewForlderStruct newone = (previewForlderStruct)old.Clone();
					m_PreviewList.Remove(old);
					m_PreviewList.Add(newone);

					subPrevewForderPath = sPreviewFolder + newone.PreviewForlderName;
					if (!Directory.Exists(subPrevewForderPath))
					{
						Directory.CreateDirectory(subPrevewForderPath);
					}
                }
                else
                {
                    previewForlderStruct pfs = new previewForlderStruct();
                    pfs.JobForlderPath = strForlderName;
                    pfs.PreviewForlderName = this.GenerateSubPreviewForlderName(strForlderName);
                    m_PreviewList.Add(pfs);

                    subPrevewForderPath = sPreviewFolder + pfs.PreviewForlderName;
                    if (!Directory.Exists(subPrevewForderPath))
                    {
                        Directory.CreateDirectory(subPrevewForderPath);
                    }
                }
                return subPrevewForderPath + Path.DirectorySeparatorChar;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return string.Empty;
            }
        }

        public void Remove(string strForlderName)
        {
            try
            {
                previewForlderStruct pfs = this.GetByForlderName(strForlderName);
                string subPrevewForderPath = sPreviewFolder + pfs.PreviewForlderName;
                if (Directory.Exists(subPrevewForderPath))
                {
                    Directory.Delete(subPrevewForderPath);
                }
                m_PreviewList.Remove(pfs);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GenerateSubPreviewForlderName(string subPreviewForlderName)
        {
            //string previewName = Path.GetFileNameWithoutExtension(jobName);
            string mPreviewFolder = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder;
            if (!Directory.Exists(mPreviewFolder))
            {
                Directory.CreateDirectory(mPreviewFolder);
            }
            string mPreviewFile = subPreviewForlderName;
            for (int i = 0; i < 1000; i++)
            {
                mPreviewFile = LISTFORLDERNAME + "_" + i.ToString("D3");
                string cur = mPreviewFolder + Path.DirectorySeparatorChar + mPreviewFile;
                if (!Directory.Exists(cur))
                    return mPreviewFile;
            }
            return "";
        }

        private string GeneratePreviewName(string jobName)
        {
            string previewName = Path.GetFileNameWithoutExtension(jobName);
            string mPreviewFolder = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder;
            if (!Directory.Exists(mPreviewFolder))
            {
                Directory.CreateDirectory(mPreviewFolder);
            }
            string mPreviewFile = previewName;
            for (int i = 0; i < 1000; i++)
            {
                mPreviewFile = previewName + "_" + i.ToString("D3") + ".bmp";
                string cur = mPreviewFolder + Path.DirectorySeparatorChar + mPreviewFile;
                if (!File.Exists(cur))
                    return mPreviewFile;
            }
            return "";
        }

        public bool Contains(string strForlderName)
        {
            foreach (previewForlderStruct pfs in m_PreviewList)
            {
                if (pfs.JobForlderPath == strForlderName)
                    return true;
            }
            return false;
        }

        public previewForlderStruct GetByForlderName(string strForlderName)
        {
            foreach (previewForlderStruct pfs in m_PreviewList)
            {
                if (pfs.JobForlderPath == strForlderName)
                    return pfs;
            }
            return new previewForlderStruct();
        }

        public string GetTheLastForderName()
        {
            if (m_PreviewList.Count > 0)
                return ((previewForlderStruct)m_PreviewList[m_PreviewList.Count - 1]).JobForlderPath;
            else
                return null;
        }
    }

    public class modifyFile
    {
        private modifyFile()
        {
        }
        // ����ָ������/�ļ����е�����ֻ���ļ�������ΪNormal
        public static void SetAllDirNormal(string drive)
        {
            DirectoryInfo dir = new DirectoryInfo(drive);   // �õ�����(drive)�ڵ������ļ���
            DirectoryInfo[] dirList = dir.GetDirectories(); // ���dir�ļ����µ��������ļ���

            foreach (DirectoryInfo d in dirList)
            {
                FileInfo dirInfo = new FileInfo(d.FullName);  // Ϊ���������ļ���������׼��
                SetDirNormal(dirInfo);                        // �����ļ�������
                SetAllDirNormal(d.FullName);                  // �ݹ���� 
                //Console.WriteLine("{0}", d.FullName);
            }
        }

        // ���ļ��е�����ΪReadOnly��������ΪNormal
        public static void SetDirNormal(FileInfo info)
        {
            if ((info.Attributes & FileAttributes.ReadOnly) != 0)
            {
                info.Attributes = FileAttributes.Normal;
            }
        }

        // ���±���ɾ�����µ����ļ�����Ŀ¼
        public static void DeleteSetting(string dir)
        {
            if (Directory.Exists(dir))
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                    {
                        FileInfo fi = new FileInfo(d);

                        SetDirNormal(fi);
                        File.Delete(d);//ֱ��ɾ�����е��ļ�   
                    }
                    else
                        DeleteSetting(d);//�ݹ�ɾ�����ļ���      
                }
                Directory.Delete(dir);
            }
        }

        public static void DeleteFolder(string dir)
        {
            DeleteSetting(dir);
        }
    }
}
