using BYHXPrinterManager.JobListView;
using MultimediaLayout.Controls;
using MultimediaLayout.Helper;
using MultimediaLayout.Models;
using MultimediaLayout.ViewModels;
using MultimediaLayout.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MultimediaLayout
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel;
        private UIElement elementForContextMenu;
        private DragCanvas canvasForContextMenu;
        public static Brush DottedLineBrush;

        private string prtFile;

        public string PrtFile
        {
            get { return prtFile; }
            set { prtFile = value; }
        }

        public MainWindow()
        {
            InitializeComponent();
            mainViewModel = new MainViewModel(new List<UIJob>() 
            { 
                new UIJob(),
                new UIJob(),
                new UIJob(),
                new UIJob(),
                new UIJob()
            },
            new List<Paper>() 
            { 
                new Paper(400,0),
                new Paper(450,10),
                new Paper(400,20),
            });
            this.DataContext = mainViewModel;
        }

        public MainWindow(List<UIJob> jobList,List<Paper> papers)
        {
            InitializeComponent();
            mainViewModel = new MainViewModel(jobList,papers);
            this.DataContext = mainViewModel;
            MainWindow.DottedLineBrush = this.FindResource("DottedLineBrush") as Brush;
            this.Closed += MainWindow_Closed;
        }

        private void MainWindow_Closed(object sender, EventArgs e)
        {
            LayoutHelper.Instance.LayoutAreaWidth = 0;//重置。
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Width = this.ActualWidth;
            this.Height = this.ActualHeight;
            LayoutHelper.Instance.LayoutAreaWidth = papersItemsControl.ActualWidth;
        }
        /// <summary>
        /// 在DataTemplate中调用（勿误删）
        /// </summary>
        private void DragCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            DragCanvas canvas = sender as DragCanvas;
            LayoutHelper.Instance.OnDragCanvasLoaded(canvas);
        }

        #region DragCanvas ContextMenu Event Handler

        /// <summary>
        /// 在DataTemplate中调用（勿误删）
        /// </summary>
        private void DragCanvas_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragCanvas canvas = sender as DragCanvas;
            if (canvas != null)
            {
                this.canvasForContextMenu = canvas;
                // If the user right-clicks while dragging an element, assume that they want 
                // to manipulate the z-index of the element being dragged (even if it is  
                // behind another element at the time).
                if (this.canvasForContextMenu.ElementBeingDragged != null)
                    this.elementForContextMenu = this.canvasForContextMenu.ElementBeingDragged;
                else
                    this.elementForContextMenu =
                        this.canvasForContextMenu.FindCanvasChild(e.Source as DependencyObject);

            }
        }

        private void menuItemBringToFront_Click(object sender, RoutedEventArgs e)
        {
            if (this.elementForContextMenu != null)
                this.canvasForContextMenu.BringToFront(this.elementForContextMenu);
        }

        private void menuItemSendToBack_Click(object sender, RoutedEventArgs e)
        {
            if (this.elementForContextMenu != null)
                this.canvasForContextMenu.SendToBack(this.elementForContextMenu);
        }

        private void menuItemDelete_Click(object sender, RoutedEventArgs e)
        {
            if (this.elementForContextMenu != null)
            {
                LayoutHelper.Instance.RemoveLayoutData((int)this.canvasForContextMenu.Tag, this.elementForContextMenu);
            }
        }
        private void menuItemClear_Click(object sender, RoutedEventArgs e)
        {
            LayoutHelper.Instance.ClearLayoutData((int)this.canvasForContextMenu.Tag);
        }

        private void menuItemClearAll_Click(object sender, RoutedEventArgs e)
        {
            LayoutHelper.Instance.ClearAllLayoutData();
        }
        #endregion   

        #region Menu Event　Handler
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //LayoutHelper.Instance.GenNewUIJob();
            LayoutHelper.Instance.SaveToFile();
            PrtFile=LayoutHelper.Instance.GenPrtData();
            if (string.IsNullOrEmpty(PrtFile))
            {
                string failedMsg = FindResource("SaveLayoutFailed") as string;
                MessageBox.Show(failedMsg);
            }
            else
            {
                this.DialogResult = true;
            }

        }

        private void exitMenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void Preview_Click(object sender, RoutedEventArgs e)
        {

        } 
        #endregion

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            MultimediaLayoutSetting settingWidow = new MultimediaLayoutSetting(LayoutHelper.Instance.RealPapers);
            LocalizationHelper.SetCurrentCultrues(settingWidow);
            settingWidow.Owner = this;
            var ret=settingWidow.ShowDialog();
            //if(ret==true)
            //  mainViewModel.OnUpdatePapers();

        }
      
    }
}
