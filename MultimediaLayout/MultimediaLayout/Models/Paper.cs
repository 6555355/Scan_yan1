using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace MultimediaLayout.Models
{
    public class Paper : INotifyPropertyChanged
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

       
        private double width;

        public double Width
        {
            get { return width; }
            set 
            { 
                if(value!=width)
                width = value;
                {
                    width = value;
                    RaisePropertyChanged("Width");
                }
            }
        }

        private double leftMargin;

        public double LeftMargin
        {
            get { return leftMargin; }
            set
            {
                if (value != leftMargin)
                {
                    leftMargin = value;
                    RaisePropertyChanged("LeftMargin");
                }
            }
        }

        public Paper(double width,double leftMargin)
        {
            this.width = width;
            this.leftMargin = leftMargin;
        }
        /// <summary>
        /// id值要求唯一
        /// </summary>
        /// <param name="width"></param>
        /// <param name="leftMargin"></param>
        /// <param name="id"></param>
        public Paper(double width, double leftMargin,int id)
        {
            this.width = width;
            this.leftMargin = leftMargin;
            this.id = id;
        }

        public override string ToString()
        {
            return string.Format("{0}", Id);
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
