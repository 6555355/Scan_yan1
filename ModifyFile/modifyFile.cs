using System;
using System.Text;
using System.IO;
using System.Threading;

namespace FileManager
{
    public class ModifyFile
    {
        private ModifyFile()
        {
        }
        // 设置指定分区/文件夹中的所有只读文件夹属性为Normal
        public static void SetAllDirNormal(string drive)
        {
            DirectoryInfo dir = new DirectoryInfo(drive);   // 得到磁盘(drive)内的所有文件夹
            dir.Attributes = FileAttributes.Normal;

            foreach (string d in Directory.GetFileSystemEntries(drive))
            {
                if (File.Exists(d))
                {
                    FileInfo dirInfo = new FileInfo(d);  // 为下面设置文件夹属性做准备
                    SetDirNormal(dirInfo);                        // 设置文件夹属性
                }
                else
                    SetAllDirNormal(d);                  // 递归调用 
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
                        if (fi.Name == "Setting.bin"|| fi.Name == "Setting.xml")
                        {
                            SetDirNormal(fi);
                            File.Delete(d);//直接删除其中的文件   
                            Console.WriteLine("    删除: " + d);
                        }
                    }
                    else
                        DeleteSetting(d);//递归删除子文件夹      
                }
            }
        }

        public static void DeleteAll(string dir)
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
                        Console.WriteLine("    删除: " + d);
                    }
                    else
                    {
                        DeleteAll(d);//递归删除子文件夹     
                    }
                }
                Directory.Delete(dir, true);
                Console.WriteLine("    删除: " + dir);
            }
        }

        public static void DeleteFolder(string dir)
        {
            if (Directory.Exists(Path.Combine(dir, "Preview")))
            {
                Console.WriteLine("开始删除Preview...");
                SetAllDirNormal(Path.Combine(dir, "Preview"));
                DeleteAll(Path.Combine(dir, "Preview"));
                //Console.WriteLine("    删除: " + Path.Combine(dir, "Preview").ToString());
                //Directory.Delete(Path.Combine(dir, "Preview"), true);
            }

            Console.WriteLine("开始删除Setting.bin...");
            DeleteSetting(dir);

            Console.WriteLine("开始删除xml文件...");
            foreach (string d in Directory.GetFiles(dir, "*.xml"))
            {

                FileInfo fi = new FileInfo(d);
                if (fi.Name == "Joblist.xml" || fi.Name == "JobPreviewlist.xml" || fi.Name == "Setting.xml")
                {
                    SetDirNormal(fi);
                    File.Delete(d);//直接删除其中的文件   
                    Console.WriteLine("    删除: " + d);
                }
            }
        }
    }
}
