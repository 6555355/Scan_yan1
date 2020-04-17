using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BYHXPrinterManager.Preview;
using System.IO;
using BYHXPrinterManager.GradientControls;

namespace BYHXPrinterManager.JobListView
{
    public partial class EditJobForm_AllWin_JetRix : Form
    {
        #region 变量

		public Image m_image;
		private BYHXPrinterManager.Preview.UserRect m_CliPRect;
		//		private bool m_PreviewChanged = true;
		private UIJob m_EditJob = new UIJob();
		private UIJob m_PreviewJob = new UIJob();
		private IPrinterChange m_iPrinterChange;
		private UILengthUnit m_CurrentUnit = UILengthUnit.Null;
		private bool isSetting = false;

		#endregion
        public EditJobForm_AllWin_JetRix()
        {
            InitializeComponent();
            m_CliPRect = new UserRect(new Rectangle(0, 0, 100, 100));
            m_CliPRect.SetPictureBox(this.m_OldPrintingPreview.ImagePictureBox);
            m_CliPRect.OnClipRecChanged += new UserRect.OnClipRecChangedEventHandler(CliPRect_OnClipRecChanged);
        }
        #region 属性

		private bool isDirty = false;

		public bool IsDirty
		{
			get { return isDirty; }
			set { isDirty = value; }
		}

		public UIJob EditJob
		{
			get 
			{
				m_EditJob.sJobSetting.bReversePrint = CheckBoxMirrorPrint.Checked;
				m_EditJob.sJobSetting.bAlternatingPrint = CheckBoxAlternatingprint.Checked;
			    m_EditJob.sJobSetting.cNegMaxGray = (byte) ((float)numNegMaxGray.Value/100f*255f);
				return m_EditJob; 
			}
			set 
			{ 
				m_EditJob = value;

				isSetting = true;
				m_PreviewJob = m_EditJob.Clone();
				//				SPrtFileInfo ss = new SPrtFileInfo();
				//				int bret = CoreInterface.Printer_GetFileInfo(m_PreviewJob.FileLocation,ref this.m_PreviewJob.Clips.PrtFileInfo,0);
				m_PreviewJob.Clips.PrtFileInfo = m_PreviewJob.PrtFileInfo;
				if(!m_PreviewJob.IsClipOrTile)
				{
					if(this.m_PreviewJob.Miniature==null)
						EnableUI(false);
					else
						this.m_PreviewJob.Clips.SrcMiniature = this.m_PreviewJob.Miniature;
				}
				else
				{
					if(this.m_PreviewJob.Clips.SrcMiniature==null)
						EnableUI(false);
				}
			    string path = PubFunc.GetFullPreviewPath(this.m_PreviewJob.PreViewFile);
                if (File.Exists(path))
			    {
                    Image srcImage = new Bitmap(path);
                    this.m_OldPrintingPreview.UpdatePreviewImage(srcImage);
                }

				bool bclip = m_PreviewJob.IsClip;
				bool btile = m_PreviewJob.IsTile;
				m_PreviewJob.IsClip = m_PreviewJob.IsTile = false;
                //SizeF srcjobsize = this.m_PreviewJob.JobSize;
                Size Dimension = new Size(m_PreviewJob.PrtFileInfo.sImageInfo.nImageWidth, m_PreviewJob.PrtFileInfo.sImageInfo.nImageHeight);
                int ResolutionX = m_PreviewJob.PrtFileInfo.sFreSetting.nResolutionX * m_PreviewJob.PrtFileInfo.sImageInfo.nImageResolutionX;
                int ResolutionY = m_PreviewJob.PrtFileInfo.sFreSetting.nResolutionY * m_PreviewJob.PrtFileInfo.sImageInfo.nImageResolutionY;
                float width = (float)Dimension.Width / (float)ResolutionX;
                float height = (float)Dimension.Height / (float)ResolutionY;
                SizeF srcjobsize = new SizeF(width, height);
                this.m_OldPrintingPreview.UpdateJobSizeInfo(srcjobsize);
				numericUpDownX.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0));
				numericUpDownX.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,srcjobsize.Width));
				numericUpDownY.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0));
				numericUpDownY.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,srcjobsize.Height));
				numericUpDownH.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0));
				numericUpDownH.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,srcjobsize.Height));
				numericUpDownW.Minimum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,0));
				numericUpDownW.Maximum = new Decimal(UIPreference.ToDisplayLength(m_CurrentUnit,srcjobsize.Width));
				m_PreviewJob.IsClip = bclip;
				m_PreviewJob.IsTile = btile;

				//			m_PreviewJob.Clips.Miniature = m_EditJob.Miniature;

				this.textBox_srcPath.Text  =this.m_PreviewJob.FileLocation;
                if (m_PreviewJob.sJobSetting.bIsDouble4CJob)
                    this.textBox_srcPath.Text +=Environment.NewLine+ this.m_PreviewJob.FileLocation2;

				this.printingInfo1.UpdateUIJobPreview(this.m_PreviewJob);
				if(this.m_PreviewJob.IsClip)
				{
					RectangleF UIClip = this.CalculateUIClipRecByReal(m_PreviewJob.Clips.ClipRect);
					UIClip.Intersect(new Rectangle(0,0,m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Width,m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Height));
					this.m_CliPRect.SetClipRectangle(UIClip);

					RectangleF ClipRec = this.CalculateDisClipRec(UIClip);
					UIPreference.SetValueAndClampWithMinMax(this.numericUpDownX,ClipRec.X);
					UIPreference.SetValueAndClampWithMinMax(this.numericUpDownY,ClipRec.Y);
					
					if((decimal)ClipRec.Width< numericUpDownW.Minimum)
						ClipRec.Width = (float)numericUpDownW.Minimum;
					if((decimal)ClipRec.Width>numericUpDownW.Maximum)
						ClipRec.Width = (float)numericUpDownW.Maximum;
					UIPreference.SetValueAndClampWithMinMax(this.numericUpDownW,ClipRec.Width);

                    if ((decimal)ClipRec.Height < numericUpDownH.Minimum)
                        ClipRec.Height = (float)numericUpDownH.Minimum;
                    if ((decimal)ClipRec.Height > numericUpDownH.Maximum)
                        ClipRec.Height = (float)numericUpDownH.Maximum;
					UIPreference.SetValueAndClampWithMinMax(this.numericUpDownH,ClipRec.Height);
					this.m_OldPrintingPreview.Refresh();
				}
				if(this.m_PreviewJob.IsTile)
				{
					UIPreference.SetValueAndClampWithMinMax(this.numericUpDownXCnt,m_PreviewJob.Clips.XCnt);
					UIPreference.SetValueAndClampWithMinMax(this.numericUpDownYCnt,m_PreviewJob.Clips.YCnt);
                    UIPreference.SetValueAndClampWithMinMax(this.numericUpDownXDis, m_CurrentUnit, (float)m_PreviewJob.Clips.XDis / m_PreviewJob.ResolutionX);
					UIPreference.SetValueAndClampWithMinMax(this.numericUpDownYDis,m_CurrentUnit, (float)m_PreviewJob.Clips.YDis/m_PreviewJob.ResolutionY);
				}
				checkBoxJobSize.Checked = (m_PreviewJob.Clips.AddtionInfoMask & 0x00001) > 0;
				checkBoxResolution.Checked = (m_PreviewJob.Clips.AddtionInfoMask & 0x00010)> 0;
				checkBoxPassNum.Checked = (m_PreviewJob.Clips.AddtionInfoMask & 0x00100)> 0;
				checkBoxDirection.Checked = (m_PreviewJob.Clips.AddtionInfoMask & 0x01000)> 0;
				checkBoxFilepath.Checked = (m_PreviewJob.Clips.AddtionInfoMask & 0x10000)> 0;
                checkBoxVoltage.Checked=(m_PreviewJob.Clips.AddtionInfoMask & 0x100000)>0;

				this.textBoxFootNote.Text = m_PreviewJob.Clips.Note;
				this.textBoxFootNote.Font = m_PreviewJob.Clips.GetNoteFont();
				UIPreference.SetValueAndClampWithMinMax(this.numericUpDown_noteMargin,m_CurrentUnit, (float)m_PreviewJob.Clips.NoteMargin/m_PreviewJob.ResolutionY);
				this.checkBoxRegion.Checked = m_PreviewJob.IsClip;
				this.checkBox_MultiCopy.Checked = m_PreviewJob.IsTile;
				CheckBoxMirrorPrint.Checked = m_PreviewJob.sJobSetting.bReversePrint;
				CheckBoxAlternatingprint.Checked = m_PreviewJob.sJobSetting.bAlternatingPrint;
                numNegMaxGray.Value = (decimal)(m_EditJob.sJobSetting.cNegMaxGray/255f*100);
                isSetting = false;
            }
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

		#endregion
        #region 事件

        private void CliPRect_OnClipRecChanged(object sender, OnClipRecChangedEventArgs e)
        {
            //生成剪切后的作业预览并更新预览
            //			UIJob clipjob = m_EditJob.Clone();
            m_PreviewJob.Clips.noClip = m_CliPRect.clipAll;

            if (m_PreviewJob.Clips.noClip)
                m_PreviewJob.Clips.ClipRect = new Rectangle(0, 0, m_PreviewJob.Clips.PrtFileInfo.sImageInfo.nImageWidth, m_PreviewJob.Clips.PrtFileInfo.sImageInfo.nImageHeight);
            else
                m_PreviewJob.Clips.ClipRect = this.CalculateRealClipRec(e.ClipRectangle);
            RectangleF ClipRec = this.CalculateDisClipRec(m_PreviewJob.Clips.ClipRect);

            isSetting = true;
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownX, ClipRec.X);
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownY, ClipRec.Y);
            if ((decimal)ClipRec.Width < numericUpDownW.Minimum)
                ClipRec.Width = (float)numericUpDownW.Minimum;
            if ((decimal)ClipRec.Width > numericUpDownW.Maximum)
                ClipRec.Width = (float)numericUpDownW.Maximum;
            this.numericUpDownW.Value = (decimal)ClipRec.Width;
            if ((decimal)ClipRec.Width < numericUpDownH.Minimum)
                ClipRec.Width = (float)numericUpDownH.Minimum;
            if ((decimal)ClipRec.Width > numericUpDownH.Maximum)
                ClipRec.Width = (float)numericUpDownH.Maximum;
            UIPreference.SetValueAndClampWithMinMax(this.numericUpDownH, ClipRec.Height);
            isSetting = false;

            UpdatePreviewAndClipBox();
        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (isSetting) return;
            //this.m_OldPreviewPictureBox1.Enabled = this.checkBoxRegion.Checked;
            this.groupBoxRegion.Enabled = this.checkBoxRegion.Checked;
            this.m_PreviewJob.IsClip = this.checkBoxRegion.Checked;
            this.m_PreviewJob.Clips.noClip = !this.m_PreviewJob.IsClip;

            if (this.checkBoxRegion.Checked)
            {
                this.m_CliPRect.rect.Intersect(new Rectangle(0, 0, m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Width, m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Height));
                m_PreviewJob.Clips.ClipRect = CalculateRealClipRec(this.m_CliPRect.rect);
            }
            else
            {

                //					DialogResult dr = MessageBox.Show(this,"该操作会丢失前面的设置,您确定这么做吗?",this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                //					if(dr == DialogResult.Yes)
                //						m_PreviewJob = m_EditJob.Clone();
                //					else
                //						checkBoxRegion.Checked = true;
            }

            this.m_CliPRect.Visble = this.checkBoxRegion.Checked;
            UpdatePreviewAndClipBox();
            this.m_OldPrintingPreview.Refresh();
        }


        private void EditJobForm_Load(object sender, EventArgs e)
        {
            this.groupBoxMultiCopy.Enabled = this.checkBox_MultiCopy.Checked;
            this.groupBoxRegion.Enabled = this.checkBoxRegion.Checked;
            if (this.m_PreviewJob.IsClipOrTile || m_PreviewJob.Clips.XCnt > 1 || m_PreviewJob.Clips.YCnt > 1)
                this.UpdatePreviewAndClipBox();

            // 某些机器上showdialog前会引发sizechangged;造成clipbox显示不正常
            // 奇怪的是我的开发机器上确不引发sizechangged;
            this.OnClipSettingChanged(sender, e);
            this.m_CliPRect.Visble = checkBoxRegion.Checked;
        }


        private void m_AnyControl_ValueChanged(object sender, System.EventArgs e)
        {
            ((NumericUpDown)sender).EndInit();
        }


        private void m_ComboBoxSpeed_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            m_AnyControl_ValueChanged(sender, e);
        }

        private void OnClipSettingChanged(object sender, System.EventArgs e)
        {
            if (isSetting) return;

            m_PreviewJob.Clips.XCnt = Convert.ToInt32(this.numericUpDownXCnt.Value);
            m_PreviewJob.Clips.YCnt = Convert.ToInt32(this.numericUpDownYCnt.Value);
            m_PreviewJob.Clips.XDis = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numericUpDownXDis.Value) * m_PreviewJob.ResolutionX);
            m_PreviewJob.Clips.YDis = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numericUpDownYDis.Value) * m_PreviewJob.ResolutionY);

            int infomask = 0;
            if (checkBoxJobSize.Checked)
                infomask |= 0x00001;
            else
                infomask &= ~0x00001;
            if (checkBoxResolution.Checked)
                infomask |= 0x00010;
            else
                infomask &= ~0x00010;
            if (checkBoxPassNum.Checked)
                infomask |= 0x00100;
            else
                infomask &= ~0x00100;
            if (checkBoxDirection.Checked)
                infomask |= 0x01000;
            else
                infomask &= ~0x01000;
            if (checkBoxFilepath.Checked)
                infomask |= 0x10000;
            else
                infomask &= ~0x10000;
            if (checkBoxVoltage.Checked)
                infomask |= 0x100000;
            else
                infomask &= ~0x100000;

            m_PreviewJob.Clips.AddtionInfoMask = infomask;
            m_PreviewJob.Clips.Note = this.textBoxFootNote.Text;
            //			m_PreviewJob.Clips.NoteFontName = this.textBoxFootNote.Font.Name;
            //			m_PreviewJob.Clips.NoteFontSize = this.textBoxFootNote.Font.Size;
            m_PreviewJob.Clips.NoteFont = this.textBoxFootNote.Font;

            if (m_PreviewJob.Clips.NotePosition == 1 || m_PreviewJob.Clips.NotePosition == 3)
                m_PreviewJob.Clips.NoteMargin = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numericUpDown_noteMargin.Value) * m_PreviewJob.ResolutionY);
            else
                m_PreviewJob.Clips.NoteMargin = Convert.ToInt32(UIPreference.ToInchLength(m_CurrentUnit, (float)this.numericUpDown_noteMargin.Value) * m_PreviewJob.ResolutionX);
            if (m_PreviewJob.IsClip)
            {
                RectangleF disrect = new RectangleF((float)this.numericUpDownX.Value, (float)this.numericUpDownY.Value,
                    (float)this.numericUpDownW.Value, (float)this.numericUpDownH.Value);
                RectangleF uirect = CalculateUIClipRec(disrect);
                RectangleF uirect1 = uirect;
                uirect1.Intersect(m_OldPrintingPreview.ImagePictureBox.ClientRectangle);
                if (!this.m_CliPRect.SetClipRectangle(uirect1))//(uirect1 != uirect)
                {
                    RectangleF ClipRec = this.CalculateDisClipRec(m_CliPRect.rect);
                    isSetting = true;
                    this.numericUpDownX.Value = (decimal)ClipRec.X;
                    this.numericUpDownY.Value = (decimal)ClipRec.Y;
                    if ((decimal)ClipRec.Width < numericUpDownW.Minimum)
                        ClipRec.Width = (float)numericUpDownW.Minimum;
                    if ((decimal)ClipRec.Width > numericUpDownW.Maximum)
                        ClipRec.Width = (float)numericUpDownW.Maximum;
                    this.numericUpDownW.Value = (decimal)ClipRec.Width;
                    if ((decimal)ClipRec.Width < numericUpDownH.Minimum)
                        ClipRec.Width = (float)numericUpDownH.Minimum;
                    if ((decimal)ClipRec.Width > numericUpDownH.Maximum)
                        ClipRec.Width = (float)numericUpDownH.Maximum;
                    UIPreference.SetValueAndClampWithMinMax(this.numericUpDownH, ClipRec.Height);
                    isSetting = false;
                }
                //				this.m_CliPRect.SetClipRectangle(uirect1);
                this.m_OldPrintingPreview.Refresh();

                m_PreviewJob.Clips.ClipRect = CalculateRealClipRec(m_CliPRect.rect);
            }
            if (m_PreviewJob.IsTile)
            {
                AllParam allp = m_iPrinterChange.GetAllParam();
                float pgW = allp.PrinterSetting.sBaseSetting.fPaperWidth;
                float pgH = allp.PrinterSetting.sBaseSetting.fPaperHeight;
                float realPgW = PubFunc.CalcRealJobWidth(pgW, this.m_iPrinterChange.GetAllParam());
                float JobW = this.m_PreviewJob.JobSize.Width;
                if (JobW > realPgW)
                {
                    isSetting = true;
                    UIPreference.SetValueAndClampWithMinMax(this.numericUpDownXCnt, m_PreviewJob.Clips.CalcXMaxCount(Convert.ToInt32(realPgW * this.m_PreviewJob.ResolutionX)));
                    isSetting = false;
                    m_PreviewJob.Clips.XCnt = Convert.ToInt32(this.numericUpDownXCnt.Value);
                }
            }
            UpdatePreviewAndClipBox();

            //GC.Collect();
        }

        private void buttonOk_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.m_PreviewJob.Clips.XCnt > 1 && !this.InvalidCheck())
                {
                    //this.tabControl_setting.SelectedIndex = 0;
                    this.numericUpDownX.Focus();
                    return;
                }
                this.m_EditJob = this.m_PreviewJob.Clone();
                if (this.m_EditJob.IsClipOrTile)
                {
                    m_EditJob.TilePreViewFile = m_EditJob.GeneratePreviewName(true);
                    if (this.m_image != null)
                    {
                        try
                        {
                            this.m_image.Save(PubFunc.GetFullPreviewPath(m_EditJob.TilePreViewFile));
                            m_image.Dispose();
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.DialogResult = DialogResult.OK;
        }

        private void checkBox_MultiCopy_CheckedChanged(object sender, System.EventArgs e)
        {
            if (isSetting) return;
            this.m_PreviewJob.IsTile = this.checkBox_MultiCopy.Checked;
            //			this.m_PreviewJob.Clips.noClip = this.m_PreviewJob.IsTile && !this.m_PreviewJob.IsClip;
            this.m_PreviewJob.Clips.noClip = !this.m_PreviewJob.IsClip;
            if (!this.checkBox_MultiCopy.Checked)
            {
                this.m_PreviewJob.Clips.XCnt = this.m_PreviewJob.Clips.YCnt = 1;
                UpdatePreviewAndClipBox();
            }
            else
            {
                OnClipSettingChanged(sender, e);
            }

            this.groupBoxMultiCopy.Enabled = this.checkBox_MultiCopy.Checked;
        }

        private void textBoxFootNote_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                OnClipSettingChanged(sender, e);
        }

        private void button_Font_Click(object sender, System.EventArgs e)
        {
            FontDialog fontDialog1 = new FontDialog();
            fontDialog1.Font = this.textBoxFootNote.Font;// new Font(font.Name, size);
            fontDialog1.AllowSimulations = false;
            fontDialog1.AllowVectorFonts = false;
            fontDialog1.AllowVerticalFonts = false;
            fontDialog1.FontMustExist = true;
            try
            {
                if (fontDialog1.ShowDialog() == DialogResult.OK)
                {
                    this.textBoxFootNote.Font = fontDialog1.Font;
                    OnClipSettingChanged(sender, e);
                }
            }
            catch
            {
                MessageBox.Show(ResString.GetResString("RulerConstantSeting_UnsupportedFont"), ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region 方法

        public void OnPrinterSettingChange(SPrinterSetting ps)
        {
            this.numericUpDownXCnt.Minimum = 1;
            this.numericUpDownXCnt.Maximum = new Decimal(ps.sBaseSetting.fPaperWidth);
            this.numericUpDownYCnt.Minimum = 1;
            this.numericUpDownYCnt.Maximum = Decimal.MaxValue;
            this.numericUpDownXDis.Minimum = 0;
            this.numericUpDownXDis.Maximum = new Decimal(ps.sBaseSetting.fPaperWidth);
            this.numericUpDownYDis.Minimum = 0;
            this.numericUpDownYDis.Maximum = Decimal.MaxValue;
        }

        public void OnPreferenceChange(UIPreference up)
        {
            this.printingInfo1.OnPreferenceChange(up);
            if (m_CurrentUnit != up.Unit)
            {
                isSetting = true;
                OnUnitChange(up.Unit);
                m_CurrentUnit = up.Unit;
                isSetting = false;
            }
        }


        private void OnUnitChange(UILengthUnit newUnit)
        {
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericUpDownX);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericUpDownY);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericUpDownW);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericUpDownH);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericUpDownXDis);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericUpDownYDis);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, m_CurrentUnit, numericUpDown_noteMargin);

            string newUnitdis = ResString.GetEnumDisplayName(typeof(UILengthUnit), newUnit);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownX, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownY, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownW, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownH, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownXDis, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDownYDis, this.m_ToolTip);
            UIPreference.NumericUpDownToolTip(newUnitdis, this.numericUpDown_noteMargin, this.m_ToolTip);

            //			this.isDirty = false;
        }


        public void SetPrinterChange(IPrinterChange ic)
        {
            m_iPrinterChange = ic;
            this.printingInfo1.SetPrinterChange(ic);
            m_OldPrintingPreview.SetPrinterChange(ic);
        }


        /// <summary>
        /// 把缩略图上的剪切矩形按比例计算出作业实际的剪切大小(单位为像素)
        /// </summary>
        /// <param name="rect">缩略图上的剪切矩形</param>
        /// <returns>业实际的剪切大小(单位为像素)</returns>
        private Rectangle CalculateRealClipRec(RectangleF rect)
        {
            // to do
            //float parx = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageWidth /(this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Width);
            //float pary = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageHeight /(this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Height);
            //Rectangle clip = new Rectangle(Convert.ToInt32(rect.Left*parx),Convert.ToInt32(rect.Top*pary),
            //    Convert.ToInt32(rect.Width*parx),Convert.ToInt32(rect.Height*pary));
            //return clip;
            int cW = this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Width;
            int cH = this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Height;
            if (m_OldPrintingPreview.Rotate == RotateFlipType.Rotate270FlipNone)
            {
                //rect = new RectangleF(cH - rect.Top - rect.Height, cW - rect.Left - rect.Width, rect.Height, rect.Width);
                rect = new RectangleF(cH - rect.Top - rect.Height, rect.Left, rect.Height, rect.Width);
                cW = this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Height;
                cH = this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Width;
            }
            if (m_OldPrintingPreview.Rotate == RotateFlipType.Rotate90FlipNone)
            {
                rect = new RectangleF(rect.Top, cW - rect.Left - rect.Width, rect.Height, rect.Width);
                // rect = new RectangleF(cH - rect.Top - rect.Height, rect.Left, rect.Height, rect.Width);
                cW = this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Height;
                cH = this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Width;
            }
            float parx = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageWidth / cW;
            float pary = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageHeight / cH;
            Rectangle clip = new Rectangle(Convert.ToInt32(rect.Left * parx), Convert.ToInt32(rect.Top * pary),
                Convert.ToInt32(rect.Width * parx), Convert.ToInt32(rect.Height * pary));
            return clip;
        }


        /// <summary>
        /// 把作业实际的剪切大小(单位为像素)按比例换算成缩略图上的剪切矩形
        /// </summary>
        /// <param name="rect">业实际的剪切大小(单位为像素)</param>
        /// <returns>缩略图上的剪切矩形</returns>
        private RectangleF CalculateUIClipRecByReal(RectangleF rect)
        {
            // to do
            //float parx = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageWidth /(this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Width);
            //float pary = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageHeight /(this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Height);
            //RectangleF clip = new RectangleF(rect.Left/parx,rect.Top/pary,rect.Width/parx,rect.Height/pary);
            //return clip;
            int cW = this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Width;
            int cH = this.m_OldPrintingPreview.ImagePictureBox.ClientRectangle.Height;
            float parx = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageWidth / cW;
            float pary = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageHeight / cH;
            if (m_OldPrintingPreview.Rotate == RotateFlipType.Rotate270FlipNone)
            {
                rect = new RectangleF(rect.Top,
                    this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageWidth - rect.Left - rect.Width,
                    rect.Height, rect.Width);
                parx = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageHeight / cW;
                pary = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageWidth / cH;
            }
            if (m_OldPrintingPreview.Rotate == RotateFlipType.Rotate90FlipNone)
            {
                //20171121
                rect = new RectangleF(this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageHeight - rect.Top - rect.Height,
                    rect.Left,
                    rect.Height, rect.Width);
                parx = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageHeight / cW;
                pary = (float)this.m_PreviewJob.PrtFileInfo.sImageInfo.nImageWidth / cH;
            }
            RectangleF clip = new RectangleF(rect.Left / parx, rect.Top / pary, rect.Width / parx, rect.Height / pary);
            return clip;
        }


        /// <summary>
        /// 把缩略图上的剪切矩形换算成按照界面显示的单位
        /// </summary>
        /// <param name="ScreenRec">缩略图上的剪切矩形</param>
        /// <returns>换算后的矩形</returns>
        private RectangleF CalculateDisClipRec(RectangleF ScreenRec)
        {
            // to do
            RectangleF scliprect = CalculateRealClipRec(this.m_CliPRect.rect);
            RectangleF ret = new RectangleF(
                UIPreference.ToDisplayLength(m_CurrentUnit, scliprect.Left / m_PreviewJob.ResolutionX),
                UIPreference.ToDisplayLength(m_CurrentUnit, scliprect.Top / m_PreviewJob.ResolutionY),
                UIPreference.ToDisplayLength(m_CurrentUnit, scliprect.Width / m_PreviewJob.ResolutionX),
                UIPreference.ToDisplayLength(m_CurrentUnit, scliprect.Height / m_PreviewJob.ResolutionY)
                );
            return ret;
        }


        /// <summary>
        /// 把界面输入的剪切矩形大小数据换算成UI缩略图上的剪切矩形
        /// </summary>
        /// <param name="ScreenRec">界面输入的剪切矩形</param>
        /// <returns>换算后的矩形</returns>
        private RectangleF CalculateUIClipRec(RectangleF ScreenRec)
        {
            // to do
            RectangleF ret = new RectangleF(
                UIPreference.ToInchLength(m_CurrentUnit, ScreenRec.Left) * m_PreviewJob.ResolutionX,
                UIPreference.ToInchLength(m_CurrentUnit, ScreenRec.Top) * m_PreviewJob.ResolutionY,
                UIPreference.ToInchLength(m_CurrentUnit, ScreenRec.Width) * m_PreviewJob.ResolutionX,
                UIPreference.ToInchLength(m_CurrentUnit, ScreenRec.Height) * m_PreviewJob.ResolutionY
                );
            return CalculateUIClipRecByReal(ret);
        }


        private void UpdatePreviewAndClipBox()
        {
            this.groupBoxOthers.Enabled = true;//this.m_PreviewJob.IsClipOrTile;
            m_image = this.m_PreviewJob.Clips.CreateClipsMiniature();
            this.printingInfo1.UpdateUIJobPreview(m_PreviewJob, m_image);
        }


        public void UpdateClipBox(Image img, Image imgsrc)
        {
            //this.m_EditJob.Miniature = this.m_PreviewJob.Miniature =img;
            //this.m_EditJob.Clips.SrcMiniature = this.m_PreviewJob.Clips.SrcMiniature = imgsrc;
            this.m_OldPrintingPreview.UpdatePreviewImage(imgsrc);
            EnableUI(img != null);
        }


        private void EnableUI(bool enable)
        {
            this.groupBoxOthers.Enabled = true;
            panel1.Enabled = enable;
        }

        public void SetGroupBoxStyle(Grouper ts)
        {
            //if (ts == null)
            //    return;
            ////foreach(Control con in this.tabPage_setting.Controls)
            //foreach (Control con in this.splitContainer3.Controls)
            //{
            //    if (con is Grouper)
            //    {
            //        (con as Grouper).CloneGrouperStyle(ts);
            //    }
            //}
            ////			this.groupBox1.CloneGrouperStyle(ts);
            ////			this.groupBoxMultiCopy.CloneGrouperStyle(ts);
            ////			this.groupBoxOthers.CloneGrouperStyle(ts);
            ////			this.groupBoxRegion.CloneGrouperStyle(ts);
        }

        private bool InvalidCheck()
        {
            AllParam allp = m_iPrinterChange.GetAllParam();
            float pgW = allp.PrinterSetting.sBaseSetting.fPaperWidth;
            float pgH = allp.PrinterSetting.sBaseSetting.fPaperHeight;
            float realPgW = PubFunc.CalcRealJobWidth(pgW, this.m_iPrinterChange.GetAllParam());

            float JobW = this.m_PreviewJob.JobSize.Width;
            string info = string.Empty;
            if (JobW > realPgW)
            {
                info += SErrorCode.GetResString("Software_MediaTooSmall");
                MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        # endregion
    }
}
