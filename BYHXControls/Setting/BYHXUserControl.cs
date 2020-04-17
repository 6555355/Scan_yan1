using System;
using BYHXPrinterManager.GradientControls;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
	/// <summary>
	/// BYHXUserControl 的摘要说明。
	/// </summary>
    public class BYHXUserControl : GradientControls.CrystalPanel
	{
		public BYHXUserControl()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		private Grouper V_TitleStyle = null;
		[Category("Appearance"), Description("SampleGrouper")]
		public Grouper GrouperTitleStyle
		{
			get
			{
				return V_TitleStyle;
			} 
			set
			{
				V_TitleStyle=value;
				if( V_TitleStyle == null)
					return;
			    SetGrouperTitleStyle(V_TitleStyle, this);
				this.Refresh();
			}
		}

        private void SetGrouperTitleStyle(Grouper style,Control parent)
        {
            foreach (Control con in parent.Controls)
            {
                if (con is Grouper)
                {
                    (con as Grouper).CloneGrouperStyle(V_TitleStyle);
                }
                if (con.Controls.Count > 0)
                {
                    SetGrouperTitleStyle(style, con);
                }
            }
        }

        public void SetBackgroundimage(Image imgbgk)
        {
            this.BackgroundImage = imgbgk;
        }
	}
}
