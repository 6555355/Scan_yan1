using System;
using System.Diagnostics;
using System.Windows.Forms;
using BYHXPrinterManager.JobListView;
using Timer = System.Windows.Forms.Timer;

namespace BYHXPrinterManager.Main
{
    public partial class DoubleSidePrintForm : Form
    {
        private readonly UIJob _job;
        private readonly IPrinterChange _printerChange;
        private JetStatusEnum _curStatus;
        private bool _bAllJobFinished = false;
        private Timer _adjustTimer;
        private UILengthUnit _mCurrentUnit;
        private bool _bAborted = false;
        private bool _useNewFunction = false;
        public DoubleSidePrintForm(UIJob job, IPrinterChange iChange,bool useNewFunction)
        {
            InitializeComponent();

            _useNewFunction = useNewFunction;
            _job = job;
            _printerChange = iChange;

            _adjustTimer = new Timer();
            _adjustTimer.Interval = 1000;
            _adjustTimer.Tick += new EventHandler(_adjustTimer_Tick);
#if DEBUG
            this.buttondebug.Visible = true;
#else
            this.buttondebug.Visible = false;
#endif
            UpdateViewByPrintingJob(true);
            OnJobStatusChanged(_job);

            label4.Visible = label3.Visible = numCursorPosX.Visible = numCursorPosY.Visible = _useNewFunction;
            label5.Visible = numFb.Visible = !_useNewFunction;
        }

        void _adjustTimer_Tick(object sender, EventArgs e)
        {
            SBiSideSetting sideSetting = new SBiSideSetting();
            CoreInterface.GetSBiSideSetting(ref sideSetting);
            UpdateDoubleSideAdjustSetting(sideSetting);
            if(sideSetting.IsEmpty)
            {
                _adjustTimer.Stop();
            }

            //this.numAdjustStep.Value = new decimal(UIPreference.ToInchLength(UILengthUnit.Millimeter, 5));
        }

        private void UpdateViewByPrintingJob(bool isBackSidejob)
        {

        }

        public void OnJobStatusChanged(UIJob job)
        {
        }

        private float GetInkStripeWidth(SColorBarSetting iss)
        {
            float inkSW = iss.fStripeWidth +
                iss.fStripeOffset;
            switch (iss.eStripePosition)
            {
                case InkStrPosEnum.Both:
                    inkSW *= 2;
                    break;
                case InkStrPosEnum.Left:
                    break;
                case InkStrPosEnum.Right:
                    break;
                case InkStrPosEnum.None:
                    inkSW = 0;
                    break;
                default:
                    inkSW = 0;
                    break;
            }
            return inkSW;
        }

        public void GetPrintableArea(out float Area, out float height)
        {
            AllParam mAllParam = _printerChange.GetAllParam();
            float mediaHeight = mAllParam.PrinterSetting.sBaseSetting.fPaperHeight;
            float mediaWidth = mAllParam.PrinterSetting.sBaseSetting.fPaperWidth;
            float origin = mAllParam.PrinterSetting.sFrequencySetting.fXOrigin;
            float originY = mAllParam.PrinterSetting.sBaseSetting.fYOrigin;
            float xPaperLeft = mAllParam.PrinterSetting.sBaseSetting.fLeftMargin;
            float xPaperTop = mAllParam.PrinterSetting.sBaseSetting.fTopMargin;
            float inkstripeWidth = GetInkStripeWidth(mAllParam.PrinterSetting.sBaseSetting.sStripeSetting);
            float usableWidth = xPaperLeft + mediaWidth - origin - inkstripeWidth;
            float usableHeight = xPaperTop + mediaWidth - originY;

            Area = usableWidth;
            height = usableHeight;
        }

        public void OnPrintingProgressChanged(int percent)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

	    public void OnPrinterStatusChanged(JetStatusEnum status)
	    {
	        _curStatus = status;
            if ((_job!=null&&_job.Status == JobStatus.Printed) || _bAborted)
            {
                this.Close();
            }
	        UpdateButtonStates(status);
	    }

	    public void OnPrinterSettingChange(SPrinterSetting ss)
	    {
            numCursorPosX.Value = (decimal) UIPreference.ToDisplayLength(_mCurrentUnit, ss.sExtensionSetting.fXRightHeadToCurosr);
            numCursorPosY.Value = (decimal) UIPreference.ToDisplayLength(_mCurrentUnit, ss.sExtensionSetting.fYRightHeadToCurosr);
	    }


        public void OnGetPrinterSetting(ref SPrinterSetting ss)
        {
            ss.sExtensionSetting.fXRightHeadToCurosr=UIPreference.ToInchLength(_mCurrentUnit,(float)numCursorPosX.Value);//  
            ss.sExtensionSetting.fYRightHeadToCurosr= UIPreference.ToInchLength(_mCurrentUnit, (float)numCursorPosY.Value);//
        }

        public void OnExtendedSettingsChange(PeripheralExtendedSettings ss)
	    {
            numAdjustStep.Value = new decimal(UIPreference.ToDisplayLength(_mCurrentUnit, ss.StepAdjust)); //should control by UI tony not test
	    }

        public void OnGetExtendedSettings(ref PeripheralExtendedSettings ss)
        {
            ss.StepAdjust = UIPreference.ToInchLength(_mCurrentUnit, (float)numAdjustStep.Value);//  
        }
        
	    public void OnPreferenceChange(UIPreference up)
	    {
            OnUnitChange(up.Unit);
            _mCurrentUnit = up.Unit;
	    }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {

        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            AllParam allParam = _printerChange.GetAllParam();
            if (_useNewFunction)
            {
                SBiSideSetting sideSetting = new SBiSideSetting();
                sideSetting.fLeftTotalAdjust = UIPreference.ToInchLength(_mCurrentUnit, (float)numLeftUp.Value);
                sideSetting.fRightTotalAdjust = UIPreference.ToInchLength(_mCurrentUnit, (float)numRightUp.Value);
                sideSetting.fxTotalAdjust = UIPreference.ToInchLength(_mCurrentUnit, (float)numLr.Value);
                //sideSetting.fyTotalAdjust = UIPreference.ToInchLength(_mCurrentUnit, (float)numFb.Value);
                sideSetting.fStepAdjust = UIPreference.ToInchLength(_mCurrentUnit, (float)numAdjustStep.Value);

                float fXRightHeadToCurosr =UIPreference.ToInchLength(_mCurrentUnit,(float)numCursorPosX.Value);//  表示最右喷头到光标安装位置的X 偏差
                float fYRightHeadToCurosr = UIPreference.ToInchLength(_mCurrentUnit, (float)numCursorPosY.Value);//  表示最右喷头到光标安装位置的Y 偏差

                // 此处不可省略,且必须在SetCurPosSBideSetting之前设置,否则x原点变更会被覆盖
                this.OnGetPrinterSetting(ref allParam.PrinterSetting);
                CoreInterface.SetPrinterSetting(ref allParam.PrinterSetting);

                CoreInterface.SetCurPosSBideSetting(ref sideSetting, fXRightHeadToCurosr, fYRightHeadToCurosr);
                this.Close();
            }
            else
            {
                SBiSideSetting sideSetting = new SBiSideSetting();
                sideSetting.fLeftTotalAdjust = UIPreference.ToInchLength(_mCurrentUnit, (float)numLeftUp.Value);
                sideSetting.fRightTotalAdjust = UIPreference.ToInchLength(_mCurrentUnit, (float)numRightUp.Value);
                sideSetting.fxTotalAdjust = UIPreference.ToInchLength(_mCurrentUnit, (float)numLr.Value);
                sideSetting.fyTotalAdjust = UIPreference.ToInchLength(_mCurrentUnit, (float)numFb.Value);
                sideSetting.fStepAdjust = UIPreference.ToInchLength(_mCurrentUnit, (float)numAdjustStep.Value);

                CoreInterface.SetSBiSideSetting(ref sideSetting);
                this._adjustTimer.Start();
            }
            this.OnGetExtendedSettings(ref allParam.ExtendedSettings);
        }

        private void UpdateDoubleSideAdjustSetting(SBiSideSetting sideSetting)
        {
            numLeftUp.Value = new decimal(UIPreference.ToDisplayLength(_mCurrentUnit,sideSetting.fLeftTotalAdjust));
            numRightUp.Value = new decimal(UIPreference.ToDisplayLength(_mCurrentUnit,sideSetting.fRightTotalAdjust));
            numLr.Value = new decimal(UIPreference.ToDisplayLength(_mCurrentUnit,sideSetting.fxTotalAdjust));
            numFb.Value = new decimal(UIPreference.ToDisplayLength(_mCurrentUnit,sideSetting.fyTotalAdjust));
            //numAdjustStep.Value = new decimal(UIPreference.ToDisplayLength(_mCurrentUnit,sideSetting.fStepAdjust)); should control by UI tony not test
        }

        private void buttondebug_Click(object sender, EventArgs e)
        {
            SPrtFileInfo jobInfo = new SPrtFileInfo();
            if (CoreInterface.Printer_GetJobInfo(ref jobInfo) > 0)
            {
                Debug.WriteLine(string.Format(" Printing Pass ={0}", jobInfo.sFreSetting.nPass));
            }
        }

        private void OnUnitChange(UILengthUnit newUnit)
        {
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, _mCurrentUnit, numLeftUp);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, _mCurrentUnit, numRightUp);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, _mCurrentUnit, numLr);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, _mCurrentUnit, numFb);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, _mCurrentUnit, numAdjustStep);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, _mCurrentUnit, numCursorPosX);
            UIPreference.OnFloatNumericUpDownUnitChanged(newUnit, _mCurrentUnit, numCursorPosY);

            string newUnitdis = ResString.GetEnumDisplayName(typeof (UILengthUnit), newUnit);
            toolTip1.SetToolTip(numLeftUp, newUnitdis);
            toolTip1.SetToolTip(numRightUp, newUnitdis);
            toolTip1.SetToolTip(numLr, newUnitdis);
            toolTip1.SetToolTip(numFb, newUnitdis);
            toolTip1.SetToolTip(numAdjustStep, newUnitdis);
            toolTip1.SetToolTip(numCursorPosX, newUnitdis);
            toolTip1.SetToolTip(numCursorPosY, newUnitdis);
        }

        private void UpdateButtonStates(JetStatusEnum status)
        {
        }

        private void DoubleSidePrintForm_Load(object sender, EventArgs e)
        {
            SBiSideSetting sideSetting = new SBiSideSetting();
            CoreInterface.GetSBiSideSetting(ref sideSetting);
            UpdateDoubleSideAdjustSetting(sideSetting);
        }

    }
}
