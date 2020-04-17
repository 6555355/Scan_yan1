using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Main
{
     public enum UserLevel
    {
        user,
        manager,
    }

    public class PrintInformation : UserControl
    {
        public PrintInformation()
        {
            InitializeComponent();

//			m_Errorring_Timer.Enabled = true;
			m_Errorring_Timer.Interval = 500;
			m_Errorring_Timer.Tick +=new EventHandler(Errorring_Timer_Tick);

            string iconpath = Path.Combine(Application.StartupPath, "setup\\app.ico");
            if (File.Exists(iconpath))
                button2.Image = new Bitmap(iconpath);
        }

		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.Panel panelButtons;
		private System.Windows.Forms.ImageList imageList2;
		private System.Windows.Forms.Label labelMsg;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.Button button2;
		/// <summary> 
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintInformation));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panelButtons = new System.Windows.Forms.Panel();
            this.labelMsg = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tabControl1.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            resources.ApplyResources(this.tabControl1, "tabControl1");
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.ImageList = this.imageList1;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tabControl1_MouseClick);
            // 
            // tabPage1
            // 
            resources.ApplyResources(this.tabPage1, "tabPage1");
            this.tabPage1.Name = "tabPage1";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Magenta;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "");
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.labelMsg);
            this.panelButtons.Controls.Add(this.tabControl1);
            this.panelButtons.Controls.Add(this.button2);
            resources.ApplyResources(this.panelButtons, "panelButtons");
            this.panelButtons.Name = "panelButtons";
            // 
            // labelMsg
            // 
            resources.ApplyResources(this.labelMsg, "labelMsg");
            this.labelMsg.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.labelMsg.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labelMsg.ImageList = this.imageList1;
            this.labelMsg.Name = "labelMsg";
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(101)))), ((int)(((byte)(147)))), ((int)(((byte)(183)))));
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "");
            // 
            // PrintInformation
            // 
            this.Controls.Add(this.panelButtons);
            this.Name = "PrintInformation";
            resources.ApplyResources(this, "$this");
            this.Load += new System.EventHandler(this.PrintInformation_Load);
            this.tabControl1.ResumeLayout(false);
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabPage tabPage1;
		public System.Windows.Forms.TabControl tabControl1;
		private Timer m_Errorring_Timer = new Timer();
		private ErrorViewForm mErrorlistForm = new ErrorViewForm();

		public event EventHandler StartButtonClicked;

        private void tabControl1_MouseClick(object sender, MouseEventArgs e)
        {
//			this.Expened = !this._Expened;
			if(PubFunc.IsInDesignMode())
				return;
			SlideErrorlist();
        }
		public void SlideErrorlist()
		{
			this.mErrorlistForm.Slide();
			m_Errorring_Timer.Stop();
			this.tabPage1.ImageIndex = 0;
        }

        public void PrintJobInfomation(int mSErrorCode, UserLevel Level,string errStr)
        {
			if(mSErrorCode == 0)
				this.labelMsg.Text = string.Empty; //清空错误信息
			else
				this.labelMsg.Text = errStr;
			
			SErrorCode err = new SErrorCode(mSErrorCode);
            if (Level != getUserLevel()
				|| this.labelMsg.Text == ""
				|| err.nErrorAction == (byte)ErrorAction.Init
				)
                return;
			if(mErrorlistForm != null)
			{
				Color textC = Color.Red;
				if(SErrorCode.IsOnlyPauseError(mSErrorCode))
				{
					labelMsg.ImageIndex = 1;
					this.tabPage1.ImageIndex = 1;
					textC = Color.Green;
				}
				else if(SErrorCode.IsWarningError(mSErrorCode))
				{
					labelMsg.ImageIndex = 3;
					this.tabPage1.ImageIndex = 3;
					textC = Color.Purple;
				}
				else
				{
					labelMsg.ImageIndex = 0;
					this.tabPage1.ImageIndex = 0;
					textC = Color.Red;
				}
				this.mErrorlistForm.PrintJobInfomation(this.labelMsg.Text,textC);
				if(!this.mErrorlistForm.IsExpanded)
					m_Errorring_Timer.Start();
			}
        }

		public void PrintString(string info, UserLevel Level,ErrorAction erroraction)
		{
			this.labelMsg.Text = info;
			this.mErrorlistForm.PrintString(info,Level,erroraction);
		}

        public static UserLevel getUserLevel()
        {
            return UserLevel.user;
		}

		private void Errorring_Timer_Tick(object sender, EventArgs e)
		{
			if(this.tabPage1.ImageIndex == -1)
			{
				this.tabPage1.ImageIndex = labelMsg.ImageIndex;
			}
			else
				this.tabPage1.ImageIndex = -1;
		}

		private void PrintInformation_Load(object sender, System.EventArgs e)
		{
			mErrorlistForm = new ErrorViewForm(this);
			this.AutoSize();
		}

		private void AutoSize()
		{
			int height = 0;
			if(this.tabControl1.Visible)
				height += this.tabControl1.Height;

			if(height != 0)
				this.Height = height;
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			if(PubFunc.IsInDesignMode())
				return;
			if(this.StartButtonClicked != null)
				this.StartButtonClicked(this.button2,new EventArgs());
		}
	}
}
