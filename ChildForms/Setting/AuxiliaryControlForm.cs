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
    public partial class AuxiliaryControlForm : Form
    {
        public AuxiliaryControlForm()
        {
            InitializeComponent();
        }

        public void OnPrinterPropertyChanged(SPrinterProperty property)
        {
            this.maintenanceSystemSetting1.OnPrinterPropertyChanged(property);
        }
    }
}
