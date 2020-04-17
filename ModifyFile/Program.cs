using System;
using System.IO;
using System.Text;
using FileManager;
using System.Windows.Forms;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            //string[] drives = Environment.GetCommandLineArgs(); //从终端获得用户输入的参数
            string str = Application.StartupPath;

            //for (int i = 1; i < drives.Length; i++) // 程序名是第一个参数，所以i从1开始，而不是从0开始
            //{
            //    str = drives[i];
            ModifyFile.DeleteFolder(str); // 对用户输入的每个参数，调用DeleteFolder函数
            //}、
            Console.WriteLine("Has Done.");
            Console.WriteLine("按下回车键退出");
            Console.Read();
            //            ConsoleKeyInfo keyinfo = Console.ReadKey(false);
            //if(keyinfo.Key == ConsoleKey.Enter)
            return;
        }
    }
}
