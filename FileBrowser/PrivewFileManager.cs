using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Diagnostics;

namespace FileBrowser
{
    class PrivewFileManager
    {
        public struct previewForlderStruct
        {
            public string JobForlderPath;
            public string PreviewForlderName;
            public previewForlderStruct(string jobForlderPath, string previewForlderName)
            {
                this.JobForlderPath = jobForlderPath;
                this.PreviewForlderName = previewForlderName;
            }
        }

        private List<previewForlderStruct> m_PreviewList = new List<previewForlderStruct>();
        private const string LISTFORLDERNAME = "PreviewForlder_";
        const string m_PreviewFolder = "Preview";
        const string m_JobListFile = "JobPreviewlist.xml";
        string sPreviewFolder = Application.StartupPath + Path.DirectorySeparatorChar + m_PreviewFolder + Path.DirectorySeparatorChar;

        public List<previewForlderStruct> PreviewList
        {
            get { return this.m_PreviewList; }
            set { this.m_PreviewList = value; }
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

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(fileName);
                XmlElement root = (XmlElement)doc.DocumentElement;

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
                            this.m_PreviewList.Add(pfs);
                        }
                    }
                }
                if (Directory.Exists(sPreviewFolder))
                {
                    foreach (string d in Directory.GetDirectories(sPreviewFolder))
                    {
                        bool bDel = true;
                        foreach (previewForlderStruct pfs in this.m_PreviewList)
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

            XmlDocument doc = new XmlDocument();
            bool success = true;
            try
            {
                XmlElement root = doc.CreateElement("PreviewForlderList");
                for (int i = 0; i < this.m_PreviewList.Count; i++)
                {
                    XmlNode Pforlder = doc.CreateElement("PreviewForlder");
                    Pforlder.Attributes.Append(doc.CreateAttribute("JobForlderPath")).Value = this.m_PreviewList[i].JobForlderPath;
                    Pforlder.Attributes.Append(doc.CreateAttribute("PreviewForlderName")).Value = this.m_PreviewList[i].PreviewForlderName;
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
                    subPrevewForderPath = sPreviewFolder + this.GetByForlderName(strForlderName).PreviewForlderName;
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
                    this.m_PreviewList.Add(pfs);

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
                this.m_PreviewList.Remove(pfs);
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
            foreach (previewForlderStruct pfs in this.m_PreviewList)
            {
                if (pfs.JobForlderPath == strForlderName)
                    return true;
            }
            return false;
        }

        public previewForlderStruct GetByForlderName(string strForlderName)
        {
            foreach (previewForlderStruct pfs in this.m_PreviewList)
            {
                if (pfs.JobForlderPath == strForlderName)
                    return pfs;
            }
            return new previewForlderStruct();
        }

        public string GetTheLastForderName()
        {
            if (this.m_PreviewList.Count > 0)
                return this.m_PreviewList[this.m_PreviewList.Count - 1].JobForlderPath;
            else
                return null;
        }
    }

    public class modifyFile
    {
        private modifyFile()
        {
        }
        // 设置指定分区/文件夹中的所有只读文件夹属性为Normal
        public static void SetAllDirNormal(string drive)
        {
            DirectoryInfo dir = new DirectoryInfo(drive);   // 得到磁盘(drive)内的所有文件夹
            DirectoryInfo[] dirList = dir.GetDirectories(); // 获得dir文件夹下的所有子文件夹

            foreach (DirectoryInfo d in dirList)
            {
                FileInfo dirInfo = new FileInfo(d.FullName);  // 为下面设置文件夹属性做准备
                SetDirNormal(dirInfo);                        // 设置文件夹属性
                SetAllDirNormal(d.FullName);                  // 递归调用 
                //Console.WriteLine("{0}", d.FullName);
            }
        }

        // 若文件夹的属性为ReadOnly，则设置为Normal
        public static void SetDirNormal(FileInfo info)
        {
            if ((info.Attributes & FileAttributes.ReadOnly) != 0)
            {
                info.Attributes = FileAttributes.Normal;
            }
        }

        // 向下遍历删除其下的子文件及子目录
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
                        File.Delete(d);//直接删除其中的文件   
                    }
                    else
                        DeleteSetting(d);//递归删除子文件夹      
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
