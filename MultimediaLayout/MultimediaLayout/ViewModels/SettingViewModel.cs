using MultimediaLayout.Helper;
using MultimediaLayout.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MultimediaLayout.ViewModels
{
    public class SettingViewModel:INotifyPropertyChanged
    {
        private List<Paper> papers;

        public List<Paper> Papers
        {
            get { return papers; }
            set { papers = value; }
        }

        private Paper selectedPaper;

        public Paper SelectedPaper
        {
            get { return selectedPaper; }
            set { selectedPaper = value; }
        }

        public SettingViewModel(List<Paper> orginPaper)
        {
            papers = new List<Paper>();
            for (int i = 0; i < orginPaper.Count; i++)
            {
                var item = orginPaper[i];
                papers.Add(new Paper(item.Width, item.LeftMargin, i+1));
            }
            
        }

        public void SaveUpdate()
        {
            LayoutHelper.Instance.UpdatePaperSize(Papers);
        }



        #region INotifyPropertyChanged Impl
        public event PropertyChangedEventHandler PropertyChanged;
        protected void RaisePropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
