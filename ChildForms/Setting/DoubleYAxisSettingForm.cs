using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class DoubleYAxisSettingForm : ByhxBaseChildForm
    {
        public DoubleYAxisSettingForm()
        {
            InitializeComponent();
        }

        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
            doubleYAxisSetting1.OnPrinterSettingChange(ss);
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            doubleYAxisSetting1.OnPrinterPropertyChange(sp);
        }

    }
}
