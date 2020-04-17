using BYHXPrinterManager;
using BYHXPrinterManager.JobListView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Forms;

namespace PrintRecordPreview
{
    /// <summary>
    /// Interaction logic for PrintHistoryExplorer.xaml
    /// </summary>
    public partial class PrintHistoryExplorer : Window, INotifyPropertyChanged
    {
        public PrintHistoryExplorer()
        {
            this.DataContext = this;
            InitializeComponent();
            try
            {
                AllParam cur = new AllParam();
                int lcid = cur.GetLanguage();
                CultureInfo culture = new CultureInfo(lcid);
                ResourceDictionary newRD = new ResourceDictionary();
                newRD.Source =
                    new Uri(string.Format("PrintRecordPreview;component/Resources/langs/{0}.xaml", culture.Name),
                        UriKind.RelativeOrAbsolute);
                //this.Resources.MergedDictionaries.RemoveAt(1);
                this.Resources.MergedDictionaries[0] = newRD;
            }
            catch
            {
            }
        }

        private UILengthUnit currentUnit = UILengthUnit.Null;

        public UILengthUnit CurrentUnit
        {
            get { return currentUnit; }
            set { currentUnit = value; }
        }
        private FolderFile printHistroyFolder;

        /// <summary>
        /// 打印记录根目录
        /// </summary>
        public FolderFile PrintHistoryFolder
        {
            get { return printHistroyFolder; }
            set
            {
                printHistroyFolder = value;
                RaisePropertyChanged("PrintHistoryFolder");
            }
        }

        private FolderFile currentFileItem;

        public FolderFile CurrentFileItem
        {
            get { return currentFileItem; }
            set
            {
                if (currentFileItem != value)
                {
                    currentFileItem = value;
                    RaisePropertyChanged("CurrentFileItem");
                }
            }
        }


        private List<PrintRecord> printRecords;

        public List<PrintRecord> PrintRecords
        {
            get { return printRecords; }
            set
            {
                if (printRecords != value)
                {
                    printRecords = value;
                    RaisePropertyChanged("PrintRecords");
                }
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            new Action<FolderFile, string>(Match).Invoke(PrintHistoryFolder, tbSearchTxt.Text.Trim());
        }

        //private ICommand searchCommand;

        //public ICommand SearchCommand
        //{
        //    get
        //    {
        //        return searchCommand ?? (searchCommand = new DelegateCommand(o =>
        //            {
        //                Application.Current.Dispatcher.BeginInvoke(new Action<FolderFile, string>(Match), PrintHistoryFolder, SearchCondition);
        //            }));
        //    }
        //}

        private void Match(FolderFile folderFile, string condition)
        {
            if (folderFile == null) return;

            folderFile.IsExpanded = folderFile.IsMatch = false;
            if (string.IsNullOrEmpty(condition))
            {
                //Nothing to  do
            }
            else if (folderFile.Name.Contains(condition) || tbSearchTxt.Text.Trim().Contains(folderFile.Name))
            {
                folderFile.IsMatch = true;
                folderFile.IsExpanded = true;
            }
            foreach (var item in folderFile.SubFolders)
            {
                new Action<FolderFile, string>(Match).Invoke(item, condition);
            }
        }

        private void InitPrintHistroyFolder()
        {
            string rootPath = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "PrintHistroy");
            DirectoryInfo rootDir = new DirectoryInfo(rootPath);
            if (!rootDir.Exists)
            {
                Directory.CreateDirectory(rootPath);
            }
            PrintHistoryFolder = new FolderFile();
            PrintHistoryFolder.Name = (string) this.FindResource("PrintRecord") ?? rootDir.Name;
            PrintHistoryFolder.FullName = rootDir.FullName;
            GetSubFolders(PrintHistoryFolder, rootPath);
        }

        private void GetSubFolders(FolderFile rootFolder, string rootPath)
        {
            DirectoryInfo rootDir = new DirectoryInfo(rootPath);

            #region 递归结束条件

            if (rootDir.Attributes.HasFlag(FileAttributes.Device))
            {
                return;
            }

            if (Directory.GetFileSystemEntries(rootPath).Length <= 0)
            {
                return;
            }

            #endregion

            try
            {
                foreach (string dir in Directory.GetDirectories(rootPath))
                {
                    DirectoryInfo tmpDir = new DirectoryInfo(dir);
                    if (tmpDir.Attributes.HasFlag(FileAttributes.Hidden) ||
                        tmpDir.Attributes.HasFlag(FileAttributes.System))
                        continue;
                    FolderFile subFolder = new FolderFile();
                    subFolder.Name = dir.Substring(dir.LastIndexOf("\\") + 1);
                    subFolder.FullName = dir;
                    // AddWeakEventListener(subFolder, OnFileSelected);
                    Action<FolderFile, string> getSubFoldersAction = new Action<FolderFile, string>(GetSubFolders);
                    getSubFoldersAction.Invoke(subFolder, dir);
                    rootFolder.SubFolders.Add(subFolder);
                }
                //foreach (var file in rootDir.GetFiles())
                //{
                //    FolderFile fileInfo = new FolderFile(true);
                //    fileInfo.Name = file.Name;
                //    fileInfo.FullName = file.FullName;
                //    rootFolder.SubFolders.Add(fileInfo);
                //    AddWeakEventListener(fileInfo, OnFileSelected);
                //}
            }
            catch (Exception)
            {
                //TODO:处理异常，也作为递归结束条件
                return;
            }


        }

        public void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            FolderFile fileItem = e.NewValue as FolderFile;
            if (fileItem == null)
            {
                return;
            }
            CurrentFileItem = fileItem;
            //if (this.ViewCore.FileBrowser != null)
            //{
            //    this.ViewCore.FileBrowser.Navigate(new Uri(@"file:///" + fileItem.FullName));
            //}
            List<PrintRecord> temp = new List<PrintRecord>();
            //if (System.IO.Path.GetExtension(fileItem.FullName) == ".xml")
            //{
            //    temp = PrintRecordService.Load(fileItem.FullName);
            //}
            //else 
            LoadRecordFronForlder(temp, fileItem.FullName);
            foreach (PrintRecord record in temp) 
            {
                record.FXOrigin = UIPreference.ToDisplayLength(currentUnit, record.FXOrigin);
                record.FYOrigin = UIPreference.ToDisplayLength(currentUnit, record.FYOrigin);
            }
            if (temp.Count > 0)
            {
                temp = temp.OrderBy(p => p.Job.PrintedDate).ToList();
                PrintRecord totalRecord = new PrintRecord();
                totalRecord.Job = new UIJob();
                totalRecord.Job.Name = "总计";
                totalRecord.Job.Copies = temp.Sum(p => p.Job.Copies);
                totalRecord.AllCopiesTime = temp.Sum(p => p.UsedTime.Ticks);
                totalRecord.PrintedArea = temp.Sum(p => p.PrintedArea);
                totalRecord.PrintedLength = temp.Sum(p => p.PrintedLength);
                totalRecord.Job.PrintedDate = default(DateTime);
                List<Tuple<ColorEnum, double>> tempcolor = new List<Tuple<ColorEnum, double>>();
                temp.ForEach(p =>
                {
                    foreach (var item in p.InkCount)
                    {
                        Tuple<ColorEnum, double> a = new Tuple<ColorEnum, double>(item.Key, item.Value);
                        tempcolor.Add(a);
                    }
                });
                var colorGroup = tempcolor.GroupBy(p => p.Item1).ToList();
                totalRecord.InkCount = new Dictionary<ColorEnum, double>();
                foreach (var item in colorGroup)
                {
                    totalRecord.InkCount.Add(item.Key, item.Sum(p => p.Item2));
                }
                    if (fileItem.Name.Length != 8) //年月日的文件夹 
                        temp.Clear();// 年和月的文件夹仅显示合计
                temp.Add(totalRecord);
            }
            PrintRecords = temp;
        }
        void LoadRecordFronForlder(List<PrintRecord> temp, string forlderPaht)
        {
            DirectoryInfo info = new DirectoryInfo(forlderPaht);
            DirectoryInfo[] directorys = info.GetDirectories();
            foreach (var item in info.GetFiles())
            {
                temp.AddRange(PrintRecordService.Load(item.FullName));
            }

            for (int i = 0; i < directorys.Length; i++)
            {
                LoadRecordFronForlder(temp, directorys[i].FullName);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitPrintHistroyFolder();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged(string PropertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(PropertyName));
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sflg = new SaveFileDialog {Filter = @"CSV(*.csv)|*.csv"};
            if (sflg.ShowDialog() == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            Export(sflg.FileName);
        }

        private void Export(string filename)
        {
            try
            {
                string path = System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "PrintHistroy");
                List<PrintRecord> temp = new List<PrintRecord>();
                DirectoryInfo dir = new DirectoryInfo(path);
                List<string> exportstr = new List<string>();
                exportstr.Add(Resources["FileName"].ToString() + "," +
                              Resources["Copies"].ToString() + "," +
                              Resources["PrintedDate"].ToString() + "," +
                              Resources["PrintedTime"].ToString() + "," +
                              Resources["PrintedLength"].ToString() + "," +
                              Resources["PrintedArea"].ToString() + "," +
                              Resources["PrintedTileCount"].ToString() + "," +
                              Resources["XOrigin"].ToString() + "," +
                              Resources["YOrigin"].ToString() + "," +
                              Resources["CountL"].ToString());
                foreach (DirectoryInfo directoryYear in dir.GetDirectories())
                {
                    foreach (DirectoryInfo directoryMonth in directoryYear.GetDirectories())
                    {
                        foreach (DirectoryInfo directoryDay in directoryMonth.GetDirectories())
                        {
                            foreach (FileInfo fileInfo in directoryDay.GetFiles())
                            {
                                temp.AddRange(PrintRecordService.Load(fileInfo.FullName));
                            }
                        }
                    }
                }
                foreach (PrintRecord record in temp)
                {
                    string inkCount = string.Empty;
                    foreach (KeyValuePair<ColorEnum, double> keyValuePair in record.InkCount)
                    {
                        inkCount += keyValuePair.Key.ToString("G") + ":" + keyValuePair.Value + " ";
                    }
                    exportstr.Add(record.Job.Name + "," +
                                  record.Job.Copies + "," +
                                  record.Job.PrintedDate.ToString("yyyy-MM-dd HH:mm:ss") + "," +
                                  record.UsedTime + "," +
                                  record.PrintedLength + "," +
                                  record.PrintedArea + "," +
                                  record.PrintedTileCount + "," +
                                  record.FXOrigin + "," +
                                  record.FYOrigin + "," +
                                  inkCount);
                }
                using (FileStream fs = new FileStream(filename, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        foreach (string s in exportstr)
                        {
                            sw.WriteLine(s);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
    }

    public class FolderFile : DataModel
    {
        private string name;
        private string fullName;
        private ObservableCollection<FolderFile> subFolders;
        private bool isFile;
        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    RaisePropertyChanged("IsSelected");
                }
            }
        }

        private bool isExpanded;

        public bool IsExpanded
        {
            get { return isExpanded; }
            set
            {
                if (isExpanded != value)
                {
                    isExpanded = value;
                    RaisePropertyChanged("IsExpanded");
                }
            }
        }


        public FolderFile(bool isFile = false)
        {
            name = "Untitled";
            subFolders = new ObservableCollection<FolderFile>();
            this.isFile = isFile;
        }

        /// <summary>
        /// 短文件名
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        /// <summary>
        /// 包含完整路径的文件名
        /// </summary>
        public string FullName
        {
            get { return fullName; }
            set
            {
                if (fullName != value)
                {
                    fullName = value;
                    RaisePropertyChanged("FullName");
                }
            }
        }

        public ObservableCollection<FolderFile> SubFolders
        {
            get { return subFolders; }
            set
            {
                if (subFolders != value)
                {
                    subFolders = value;
                    RaisePropertyChanged("SubFolders");
                }
            }
        }
        public bool IsFile
        {
            get { return isFile; }
            set
            {
                if (isFile != value)
                {
                    isFile = value;
                    RaisePropertyChanged("IsFile");
                }
            }
        }

        private bool isMatch;

        public bool IsMatch
        {
            get { return isMatch; }
            set
            {
                if (isMatch != value)
                {
                    isMatch = value;
                    RaisePropertyChanged("IsMatch");
                }
            }
        }


    }
}
