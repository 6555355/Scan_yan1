using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using BYHXPrinterManager.JobListView;

namespace BYHXPrinterManager.Preview
{
    public partial class PrintingInfo_AllWin_JetRix : UserControl
    {
        DateTime m_StartTime = DateTime.Now;
        TimeSpan m_spendTime = new TimeSpan(0);
        private string m_sJobInfo = "";
        private string m_sJobPath = "";
        private float m_fArea = 0;

        private IPrinterChange m_iPrinterChange = null;
        private bool m_bCreateImageWithPercent = false;
        private bool m_bPrintingPreview = true;
        private UILengthUnit m_curUnit = UILengthUnit.Null;
        private UIJob m_curJob;
        private int m_JobID;
        public PrintingInfo_AllWin_JetRix()
        {
            InitializeComponent();
        }
        public void OnPreferenceChange(UIPreference up)
        {
            UpdateJobInfo(up.Unit, m_curJob);
            if (m_curUnit != up.Unit)
            {
                m_curUnit = up.Unit;
                if (m_curJob != null)
                {
                    UpdateJobInfo(m_curUnit, m_curJob);
                }
                else if (m_bPrintingPreview && m_bCreateImageWithPercent)
                {
                    OnSwitchToPrintingPreview();
                }
            }
        }
        public void SetPrinterChange(IPrinterChange ic)
        {
            m_iPrinterChange = ic;
            m_PrintPreview.SetPrinterChange(ic);
        }
        public void OnSwitchToPrintingPreview()
        {
            bool bPrinting = m_bPrintingPreview;
            SPrtFileInfo jobInfo = new SPrtFileInfo();
            if (CoreInterface.Printer_GetJobInfo(ref jobInfo) > 0)
            {
                UpdatePrintingJobInfo(m_curUnit, jobInfo);
                CreatePrintingPreviewAsPercentage(0);
                SizeF previewsize = GetJobSize(jobInfo.sImageInfo.nImageWidth, jobInfo.sImageInfo.nImageHeight, jobInfo.sFreSetting.nResolutionX, jobInfo.sFreSetting.nResolutionY);
                UpdateJobSizeInfo(previewsize);
            }
            else
            {
                //??????????????????????????????????????????  should do it
                UpdatePrintingJobInfo(m_curUnit, jobInfo);
                UpdatePreviewImage("");
                UpdateJobSizeInfo(SizeF.Empty);
            }
        }
        public void UpdateUIJobPreview(UIJob job, Image img = null)
        {
            if (job != null)
            {
                if (img == null)
                {
                    if (job.IsClipOrTile)
                        UpdatePreviewImage(PubFunc.GetFullPreviewPath(job.TilePreViewFile));
                    else
                        UpdatePreviewImage(PubFunc.GetFullPreviewPath(job.PreViewFile));
                }
                else
                {
                    UpdatePreviewImage(img);
                }
                UpdateJobSizeInfo(job.JobSize);
                UpdateJobInfo(m_curUnit, job);
            }
            else
            {
                //No one job in joblist
                UpdatePreviewImage("");
                UpdateJobSizeInfo(SizeF.Empty);
                UpdateJobInfo(m_curUnit, null);
            }
            m_curJob = job;
        }
        private string curPreview = string.Empty;
        private void UpdatePreviewImage(string preview)
        {
            if (curPreview != preview)
            {
                curPreview = preview;
                Image preImage = null;
                try
                {
                    if (File.Exists(preview))
                        preImage = new Bitmap(preview);
                }
                catch
                {
                    preImage = null;
                }
                m_PrintPreview.UpdatePreviewImage(preImage);
            }
        }
        private void UpdateJobSizeInfo(SizeF size)
        {
            m_PrintPreview.UpdateJobSizeInfo(size);
        }
        private void UpdateJobInfo(UILengthUnit unit, UIJob jobInfo)
        {
            if (jobInfo == null)
            {
                InitNull();
            }
            else
            {
                //				float width = this.CalcRealJobWidth(unit,jobInfo.JobSize.Width);
                UpdateJobInfoText(unit, jobInfo.JobSize.Width, jobInfo.JobSize.Height, jobInfo.ResolutionX, jobInfo.ResolutionY,
                    jobInfo.ColorDeep, jobInfo.PassNumber, jobInfo.PrintingDirection, jobInfo.FileLocation, jobInfo.LangID);
            }
        }

        private void InitNull()
        {
            this.m_LabelPrintingJobInfo.Text = null;
        }

        private void UpdateJobInfoText(UILengthUnit unit, float width, float height, int resX, int resY,
            int deep, int passnum, int direction, string filepath, int LangID)
        {

            string unitStr = ResString.GetUnitSuffixDispName(unit);
            string pass = ResString.GetDisplayPass();

            string strSize = string.Format("{0}x{1} {2}",
                UIPreference.ToDisplayLength(unit, width).ToString("f1"),
                UIPreference.ToDisplayLength(unit, height).ToString("f1"), unitStr);
            if (this.m_iPrinterChange != null)
            {
                width = PubFunc.CalcRealJobWidth(width, this.m_iPrinterChange.GetAllParam());
            }
            m_fArea = UIPreference.ToDisplayLength(UILengthUnit.Meter, width) * UIPreference.ToDisplayLength(UILengthUnit.Meter, height);

            string strRes = string.Format("{0}x{1}",
                (resX),
                (resY));

            string strDeep;
            if (1 == deep)
            {
                strDeep = "Normal";
            }
            else
            {
                strDeep = "High Quality";
            }


            string strPass = string.Format("{0} {1}", passnum, pass);
            string strDir = ResString.GetEnumDisplayName(typeof(PrintDirection), (PrintDirection)direction);
            string sLangID = "";
            if (LangID != 0xffff)
            {
                try
                {
                    foreach (CultureInfo cInfo in CultureInfo.GetCultures(CultureTypes.AllCultures))
                    {
                        if (cInfo.LCID == LangID)
                        {
                            sLangID = cInfo.EnglishName;
                            break;
                        }
                    }

                }
                catch (Exception)
                {
                    sLangID = "";
                }
            }
            //DisplayName EnglishName
            m_sJobInfo =
                strSize
                + "\n" + strRes
                + "\n" + strDeep
                + "\n" + strPass
                + "\n" + strDir
                + "\n" + sLangID;
            m_sJobPath = filepath;
            unitStr = ResString.GetUnitSuffixDispName(UILengthUnit.Meter);
            string strEff = string.Empty;
            float efficient = 0;
            if (m_spendTime.TotalSeconds != 0)
                efficient = (float)(m_fArea) / (float)m_spendTime.TotalHours;
            if (efficient > 0)
                strEff = efficient.ToString() + " " + unitStr + "2/h";
            this.m_LabelPrintingJobInfo.Text = strEff + "\n" + m_sJobInfo;
            this.m_LabelPrintingJobPath.Text = m_sJobPath;
        }
        private void UpdatePrintingJobInfo(UILengthUnit unit, SPrtFileInfo jobInfo)
        {
            float width = 0;
            if (jobInfo.sFreSetting.nResolutionX != 0)
            {
                width = (float)jobInfo.sImageInfo.nImageWidth / (float)(jobInfo.sFreSetting.nResolutionX);
                //				width = this.CalcRealJobWidth(unit,width);
                //width = UIPreference.ToDisplayLength(unit,width);
            }
            float height = 0;
            if (jobInfo.sFreSetting.nResolutionY != 0)
            {
                height = (float)jobInfo.sImageInfo.nImageHeight / (float)(jobInfo.sFreSetting.nResolutionY);
                //height = UIPreference.ToDisplayLength(unit,height);
            }
            UpdateJobInfoText(unit, width, height, jobInfo.sFreSetting.nResolutionX * jobInfo.sImageInfo.nImageResolutionX, jobInfo.sFreSetting.nResolutionY * jobInfo.sImageInfo.nImageResolutionY,
                jobInfo.sImageInfo.nImageColorDeep, jobInfo.sFreSetting.nPass, jobInfo.sFreSetting.nBidirection, null, 0);
        }

        private void UpdateJobInfoTextWith(int percent)
        {
            m_spendTime = DateTime.Now - m_StartTime;
            string strTime = m_spendTime.Hours.ToString() + ":" + m_spendTime.Minutes.ToString() + ":" + m_spendTime.Seconds.ToString();
            string strPercentage = percent.ToString() + "%";

            string unitStr = ResString.GetUnitSuffixDispName(UILengthUnit.Meter);
            string strArea = (m_fArea * percent / 100.0f).ToString() + " " + unitStr + "2";
            float efficient = 0;
            if (m_spendTime.TotalSeconds != 0)
                efficient = (float)(m_fArea * percent / 100.0f) / (float)m_spendTime.TotalHours;
            string strEff = efficient.ToString() + " " + unitStr + "2/h";
            this.m_LabelPrintingJobInfo.Text = strTime
                + "\n" + strPercentage
                + "\n" + strArea
                + "\n" + strEff
                + "\n" + m_sJobInfo;
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
        unsafe private void CreatePrintingPreviewAsPercentage(int percentage)
        {
            SPrtFileInfo jobInfo = new SPrtFileInfo();
            if (CoreInterface.Printer_GetJobInfo(ref jobInfo) <= 0) return;
            IntPtr handle = (IntPtr)jobInfo.sImageInfo.nImageData;
            if (handle == IntPtr.Zero) return;
            SPrtImagePreview previewData = (SPrtImagePreview)Marshal.PtrToStructure(handle, typeof(SPrtImagePreview));
            Bitmap image = SerialFunction.CreateImageWithPreview(previewData);

            if (image != null)
            {
                int clipy = (image.Height * percentage / 100);
                int clipheight = image.Height - clipy;

                if (clipheight > 0)
                {
                    BitmapData data = image.LockBits(new Rectangle(0, clipy, image.Width, clipheight), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

                    byte* buf = (byte*)data.Scan0;
                    int size = data.Stride * clipheight;

                    for (int i = 0; i < size; i++)
                    {
                        buf[i] = 0xff;
                    }

                    image.UnlockBits(data);
                }

                UpdatePreviewImage(image);
            }
        }

        private void UpdatePreviewImage(Image image)
        {
            m_PrintPreview.UpdatePreviewImage(image);
        }
    }
}
