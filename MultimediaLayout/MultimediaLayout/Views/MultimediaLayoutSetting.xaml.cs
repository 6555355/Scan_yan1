using MultimediaLayout.Models;
using MultimediaLayout.ViewModels;
using System;
using System.Collections.Generic;
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

namespace MultimediaLayout.Views
{
    /// <summary>
    /// Interaction logic for MultimediaLayoutSetting.xaml
    /// </summary>
    public partial class MultimediaLayoutSetting : Window
    {
        SettingViewModel viewModel;
        public MultimediaLayoutSetting(List<Paper> orginPaper)
        {
            InitializeComponent();
            viewModel = new SettingViewModel(orginPaper);
            this.DataContext = viewModel;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            viewModel.SaveUpdate();
            this.DialogResult = true;
        }
    }
}
