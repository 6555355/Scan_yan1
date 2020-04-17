using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BYHXPrinterManager.JobListView;

namespace BYHXPrinterManager.Main
{
    public partial class InkCounterForm : Form
    {
        private List<Label> clolorsLabels;
        private List<Label> clolorsInk;
        private List<Label> clolorsInkMl;
        private SPrinterProperty _property;
        private BackgroundWorker worker= null;
        private UIJob workJob = null;
        private volatile bool _bCancel = false;
        public InkCounterForm(UIJob jobInfo)
        {
            InitializeComponent();

            workJob = jobInfo;
            clolorsLabels = new List<Label>() {label1, label2, label3, label4, label5, label6, label7, label8};
            clolorsInk = new List<Label>() {label9, label10, label11, label12, label13, label14, label15, label16};
            clolorsInkMl = new List<Label>() { label17, label18, label19, label20, label21, label22, label23, label24 };
            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = true;
            worker.RunWorkerAsync();
            buttonStart.Enabled = false;
        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_result == 0)
            {
                MessageBox.Show("统计失败！");
                return;
            }
            float headPl = PubFunc.GetHeadPlNum(_property.ePrinterHead);
            ulong[] buf = (ulong[])e.Result;
            double totalL = 0;
            double totalML = 0;
            for (int i = 0; i < buf.Length; i++)
            {
                double plnum = (buf[i]*headPl)/1000000000000;
                clolorsInk[i].Text = plnum.ToString("F6");
                totalL += plnum;
                double mlnum = (buf[i] * headPl) / 1000000000;
                totalML += mlnum;
                clolorsInkMl[i].Text = mlnum.ToString("F3");
            }

            labelTTL.Text = totalL.ToString("F6");
            labelTTMl.Text = totalML.ToString("F3");
            buttonStart.Enabled = true;
        }

        private int _result = 0;
        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            string filename = workJob.FileLocation;
            int filetype = 1;
            int inkindex = 1;
            ulong[] buf = new ulong[8];
            ulong[]bufs=new ulong[128];
            int x = 0;
            int y = 0;
            int w = workJob.PrtFileInfo.sImageInfo.nImageWidth; //(int) (workJob.JobSize.Width*workJob.ResolutionX);
            int h = workJob.PrtFileInfo.sImageInfo.nImageHeight;//(int) (workJob.JobSize.Height*workJob.ResolutionY);
            int xCopy = 1;
            int yCopy = 1;
            float xInternal = 0;
            float yInternal = 0;

            if (workJob.IsClipOrTile)
            {
                if (workJob.IsClip)
                {
                    w = (int)(workJob.Clips.ClipRect.Width);
                    h = (int)(workJob.Clips.ClipRect.Height);

                    x = workJob.Clips.ClipRect.Left;
                    y = workJob.Clips.ClipRect.Top;
                    w = workJob.Clips.ClipRect.Width;
                    h = workJob.Clips.ClipRect.Height;
                }
                if (workJob.IsTile)
                {
                    xCopy = workJob.Clips.XCnt;
                    yCopy = workJob.Clips.YCnt;
                    xInternal = workJob.Clips.XDis;
                    yInternal = workJob.Clips.YDis; 
                }
            }
            _result = CoreInterface.CalcInkCounter(filename, filetype, inkindex, buf, x, y, w, h, xCopy, yCopy,
                xInternal, yInternal, bufs);
            e.Result = buf;
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            _property = sp;
            for (int i = 0; i < clolorsLabels.Count; i++)
            {
                if (i < _property.nColorNum - _property.nWhiteInkNum)
                {
                    //clolorsLabels[i].Text = _property.Get_ColorIndex(i);
                }
                else
                {
                    //clolorsLabels[i].Text = string.Empty;
                    clolorsInkMl[i].Visible = clolorsLabels[i].Visible = clolorsInk[i].Visible = false;
                }
            }
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            worker.RunWorkerAsync();
            buttonStart.Enabled = false;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            _bCancel = true;
        }
    }
}
