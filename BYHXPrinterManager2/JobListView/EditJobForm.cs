using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using BYHXPrinterManager.JobListView;
using BYHXPrinterManager.Preview;

namespace BYHXPrinterManager
{
    public partial class EditJobForm : Form
    {
        #region 变量

        public Image m_image;
        private BYHXPrinterManager.Preview.UserRect m_CliPRect;
        private bool m_PreviewChanged = true;
        private UIJob m_EditJob = new UIJob();
        private bool m_bInitControl = false;
        private IPrinterChange m_iPrinterChange;

        #endregion

        public EditJobForm()
        {
            InitializeComponent();

            m_CliPRect = new UserRect(new Rectangle(10, 10, 100, 100));
            m_CliPRect.SetPictureBox(this.m_OldPrintingPreview.ImagePictureBox);
            m_CliPRect.OnClipRecChanged += new UserRect.OnClipRecChangedEventHandler(CliPRect_OnClipRecChanged);
            //if (m_image != null)
            //{
            //    this.m_OldPrintingPreview.UpdatePreviewImage(this.m_image);
            //}
        }


        #region 事件

        private void CliPRect_OnClipRecChanged(object sender, OnClipRecChangedEventArgs e)
        {
            RectangleF ClipRec = this.CalculateRealClipRec(e.ClipRectangle);
            this.numericUpDownX.Value = (decimal)ClipRec.X;
            this.numericUpDownY.Value = (decimal)ClipRec.Y;
            this.numericUpDownW.Value = (decimal)ClipRec.Width;
            this.numericUpDownH.Value = (decimal)ClipRec.Height;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            //this.m_OldPreviewPictureBox1.Enabled = this.checkBoxRegion.Checked;
            this.m_CliPRect.Visble = this.checkBoxRegion.Checked;
            this.m_OldPrintingPreview.Refresh();
            this.groupBoxRegion.Enabled = this.checkBoxRegion.Checked;
        }

        private void EditJobForm_Load(object sender, EventArgs e)
        {
            this.m_OldPrintingPreview.UpdatePreviewImage(this.m_EditJob.Miniature);
            //this.m_PreviewPictureBox.Image = this.m_EditJob.Miniature;
            //SPrtFileInfo job = this.m_EditJob.PrtFileInfo;
            //SizeF previewsize = GetJobSize(job.sImageInfo.nImageWidth, job.sImageInfo.nImageHeight, job.sFreSetting.nResolutionX, job.sFreSetting.nResolutionY);
            this.m_OldPrintingPreview.UpdateJobSizeInfo(this.m_EditJob.JobSize);

            this.printingInfo1.UpdateUIJobPreview(this.m_EditJob);
        }

        private void checkBoxMultiCopy_CheckedChanged(object sender, EventArgs e)
        {
            this.groupBoxMultiCopy.Enabled = this.checkBoxMultiCopy.Checked;
        }


        private void checkBoxFootNote_CheckedChanged(object sender, EventArgs e)
        {
            this.textBoxFootNote.Enabled = this.checkBoxFootNote.Checked;
        }

        private void m_AnyControl_ValueChanged(object sender, System.EventArgs e)
        {
            if (m_bInitControl == false)
                m_iPrinterChange.NotifyUIParamChanged();
        }

        private void m_ComboBoxSpeed_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            m_AnyControl_ValueChanged(sender, e);
        }

        private void m_ComboBoxPass_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            int PassListNum;
            int[] PassList;
            m_iPrinterChange.GetAllParam().PrinterProperty.GetPassListNumber(out PassListNum, out PassList);
            int[] PassStepArray = m_iPrinterChange.GetAllParam().PrinterSetting.sCalibrationSetting.nPassStepArray;
            //this.m_NumericUpDownStep.Value = PassStepArray[m_ComboBoxPass.SelectedIndex];

            m_AnyControl_ValueChanged(sender, e);
        }

        private void numericUpDownX_ValueChanged(object sender, EventArgs e)
        {
            Rectangle newRec = new Rectangle();
            newRec.X = (int)this.numericUpDownX.Value;
            newRec.Y = (int)this.numericUpDownY.Value;
            newRec.Width = (int)this.numericUpDownW.Value;
            newRec.Height = (int)this.numericUpDownH.Value;
            this.m_CliPRect.SetClipRectangle(newRec);
            this.m_OldPrintingPreview.Refresh();
        }
        #endregion

        #region 属性

        public UIJob EditJob
        {
            get { return m_EditJob; }
            set { m_EditJob = value; }
        }
        public int Copies
        {
            get
            {
                return Decimal.ToInt32(m_NumericUpDownCopy.Value);
            }
            set
            {
                m_NumericUpDownCopy.Value = value;
            }
        }

        public bool PreviewChanged
        {
            get
            {
                return m_PreviewChanged;
            }
            set
            {
                m_PreviewChanged = value;
            }
        }

        #endregion

        #region 方法

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            m_bInitControl = true;

            //m_ComboBoxUnit
            m_ComboBoxSpeed.Items.Clear();
            foreach (SpeedEnum mode in Enum.GetValues(typeof(SpeedEnum)))
            {
                if (mode == SpeedEnum.CustomSpeed) continue;
                string cmode = ResString.GetEnumDisplayName(typeof(SpeedEnum), mode);
                m_ComboBoxSpeed.Items.Add(cmode);
            }
            this.m_ComboBoxSpeed.SelectedIndex = (int)this.m_EditJob.PrtFileInfo.sFreSetting.nSpeed;

            string passStr = (string)m_ComboBoxPass.SelectedItem;
            m_ComboBoxPass.Items.Clear();
#if false
			int PassListNum;
			int [] PassList;
			sp.GetPassListNumber(out PassListNum,out PassList);
			for(int i = 0;i <PassListNum; i++)
			{
				int passNum = PassList[i];
				string dispPass = PassList[i].ToString() + " " + ResString.GetDisplayPass();
				m_ComboBoxPass.Items.Add(dispPass);
			}
			m_ComboBoxPass.SelectedIndex = FoundMatchPass(passStr);
#else
            for (int i = 0; i < CoreConst.MAX_PASS_NUM; i++)
            {
                //int passNum = PassList[i];
                string dispPass = (i + 1).ToString() + " " + ResString.GetDisplayPass();
                m_ComboBoxPass.Items.Add(dispPass);
            }
            m_ComboBoxPass.SelectedIndex = this.m_EditJob.PrtFileInfo.sFreSetting.nPass;
#endif
            m_bInitControl = false;
        }

        public void SetPrinterChange(IPrinterChange ic)
        {
            m_iPrinterChange = ic;
        }

        private int FoundMatchPass(string dispPass)
        {
            for (int i = 0; i < m_ComboBoxPass.Items.Count; i++)
            {
                if (string.Compare((string)m_ComboBoxPass.Items[i], dispPass) == 0)
                    return i;
            }
            return -1;
        }

        private SizeF GetJobSize(int imgWidth, int imgHeight, int resX, int resY)
        {
            float width = 0;

            if (resX != 0)
            {
                width = (float)imgWidth / (float)resX;
            }
            float height = 0;
            if (resY != 0)
            {
                height = (float)imgHeight / (float)resY;
            }
            return new SizeF(width, height);
        }

        private RectangleF CalculateRealClipRec(Rectangle ScreenRec)
        {
            // to do
            
            return ScreenRec;
        }

        #endregion
    }
}
