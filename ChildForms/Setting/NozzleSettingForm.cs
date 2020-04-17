using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class NozzleSettingForm : Form
    {
        public NozzleSettingForm()
        {
            InitializeComponent();
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            this.nozzleXYoffset1.OnPrinterPropertyChange(sp);
        }
        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
            nozzleXYoffset1.OnPrinterSettingChange(ss);
        }
        public void OnPreferenceChange(UIPreference up)
        {
            nozzleXYoffset1.OnPreferenceChange(up);
        }
        public void OnGetPrinterSetting(ref AllParam allParam, ref bool bChangeProperty)
        {
            nozzleXYoffset1.OnGetPrinterSetting(ref allParam.PrinterSetting);
        }
        public void OnRealTimeChange()
        {
            nozzleXYoffset1.OnRealTimeChange();
        }
    }
}
