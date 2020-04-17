using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FileBrowser;
using ShellDll;
using System.IO;

namespace BrowserPlugins
{
    public class DefaultColumnPlugin : IColumnPlugin
    {
        private string[] columns = new string[]
            {
                "Size",
                "Date Created",
                "Date Modified"
            };

        #region IColumnPlugin Members

        public string[] ColumnNames
        {
            get { return columns; }
        }

        public string GetFileInfo(IFileInfoProvider provider, string columnName, ShellItem item)
        {
            string retVal = string.Empty;
            ShellAPI.STATSTG info = provider.GetFileInfo();

            switch (columnName)
            {
                case "Size":
                    #region Size
                    {
                        retVal = GetSizeString(info.cbSize);
                    }
                    #endregion
                    break;

                case "Date Created":
                    #region Date Created
                    {
                        DateTime dateTime = ShellAPI.FileTimeToDateTime(info.ctime);
                        string time = dateTime.ToLongTimeString();
                        retVal = string.Format("{0} {1}",
                            dateTime.ToShortDateString(),
                            time.Remove(time.Length - 3, 3));
                    }
                    #endregion
                    break;

                case "Date Modified":
                    #region Date Modified
                    {
                        DateTime dateTime = ShellAPI.FileTimeToDateTime(info.mtime);
                        string time = dateTime.ToLongTimeString();
                        retVal = string.Format("{0} {1}",
                            dateTime.ToShortDateString(),
                            time.Remove(time.Length - 3, 3));
                    }
                    #endregion
                    break;
            }

            return retVal;
        }

        public string GetFolderInfo(IDirInfoProvider provider, string columnName, ShellItem item)
        {
            string retVal = string.Empty;

            if (columnName != "Size" && !item.IsSystemFolder && !item.IsDisk && item.IsFileSystem)
            {
                ShellAPI.STATSTG info = provider.GetDirInfo();

                switch (columnName)
                {
                    case "Date Created":
                        #region Date Created
                        {
                            DateTime dateTime = ShellAPI.FileTimeToDateTime(info.ctime);
                            string time = dateTime.ToLongTimeString();
                            retVal = string.Format("{0} {1}",
                                dateTime.ToShortDateString(),
                                time.Remove(time.Length - 3, 3));
                        }
                        #endregion
                        break;

                    case "Date Modified":
                        #region Date Modified
                        {
                            DateTime dateTime = ShellAPI.FileTimeToDateTime(info.mtime);
                            string time = dateTime.ToLongTimeString();
                            retVal = string.Format("{0} {1}",
                                dateTime.ToShortDateString(),
                                time.Remove(time.Length - 3, 3));
                        }
                        #endregion
                        break;
                }
            }

            return retVal;
        }

        public HorizontalAlignment GetAlignment(string columnName)
        {
            if (columnName == "Size")
                return HorizontalAlignment.Right;
            else
                return HorizontalAlignment.Left;
        }

        #endregion

        #region IBrowserPlugin Members

        public string Name
        {
            get { return "Default IColumnPlugin"; }
        }

        public string Info
        {
            get { return "Column plugin for the default columns for all items."; }
        }

        #endregion

        private string GetSizeString(long bytes)
        {
            if (bytes < 1000)
                return string.Format("{0} bytes", bytes);
            else if (bytes < 1000000)
                return string.Format("{0} KB", Math.Round((double)bytes / 1000, 3, MidpointRounding.ToEven));
            else if (bytes < 1000000000)
                return string.Format("{0} MB", Math.Round((double)bytes / 1000000, 3, MidpointRounding.ToEven));
            else
                return string.Format("{0} GB", Math.Round((double)bytes / 1000000000, 3, MidpointRounding.ToEven));
        }
    }
}
