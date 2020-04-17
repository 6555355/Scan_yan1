using System;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using BYHXPrinterManager;

public class MyMessageBox : Form
{
    public static int liveObjectNum = 0;
    public static Point originLocation = new Point(0,0);
    public delegate void ShowCalled();

    private ShowCalled _showCalled;
    private bool _bModleDialog = true;//是否为模式窗口
    public MyMessageBox(ShowCalled showCalled,bool rememberMyChoose = false,bool bModleDialog = true)
	{
		InitializeComponent();
        checkBoxRememberMyChoose.Visible = rememberMyChoose;
        if (!rememberMyChoose)
            this.Height -= checkBoxRememberMyChoose.Height;

        _showCalled = showCalled;
        _bModleDialog = bModleDialog;
        this.Load += new EventHandler(MyMessageBox_Load);
        this.Closed += new EventHandler(MyMessageBox_Closed);
	}

    void MyMessageBox_Closed(object sender, EventArgs e)
    {
        if (!_bModleDialog)
            liveObjectNum--;
    }

    void MyMessageBox_Load(object sender, EventArgs e)
    {
        if (liveObjectNum == 0)
        {
            originLocation = this.Location;
        }
        if (!_bModleDialog)
        {
            liveObjectNum++;
            this.Location = new Point((this.Location.X + 10 * liveObjectNum)%Screen.PrimaryScreen.WorkingArea.Width,
                (this.Location.Y + 10 * liveObjectNum) % Screen.PrimaryScreen.WorkingArea.Height);
        }
    }

    public bool RememberMyChoose
    {
        get { return checkBoxRememberMyChoose.Visible && checkBoxRememberMyChoose.Checked; }
        set
        {
            if (checkBoxRememberMyChoose.Visible)
                checkBoxRememberMyChoose.Checked = value;
        }
    }

	private void InitializeComponent()
	{
        this.components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MyMessageBox));
        this.imageList1 = new System.Windows.Forms.ImageList(this.components);
        this.panel1 = new System.Windows.Forms.Panel();
        this.panel2 = new System.Windows.Forms.Panel();
        this.panel3 = new System.Windows.Forms.Panel();
        this.frmMessage = new System.Windows.Forms.Label();
        this.label1 = new System.Windows.Forms.Label();
        this.label2 = new System.Windows.Forms.Label();
        this.label3 = new System.Windows.Forms.Label();
        this.label4 = new System.Windows.Forms.Label();
        this.label5 = new System.Windows.Forms.Label();
        this.label6 = new System.Windows.Forms.Label();
        this.label7 = new System.Windows.Forms.Label();
        this.panel4 = new System.Windows.Forms.Panel();
        this.labelAutoPumpInk = new System.Windows.Forms.Label();
        this.labelContinue = new System.Windows.Forms.Label();
        this.m_LabelAbortCurrent = new System.Windows.Forms.Label();
        this.m_LabelAbortAll = new System.Windows.Forms.Label();
        this.checkBoxRememberMyChoose = new System.Windows.Forms.CheckBox();
        this.SuspendLayout();
        // 
        // imageList1
        // 
        this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
        this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
        this.imageList1.Images.SetKeyName(0, "");
        this.imageList1.Images.SetKeyName(1, "");
        this.imageList1.Images.SetKeyName(2, "");
        this.imageList1.Images.SetKeyName(3, "");
        // 
        // panel1
        // 
        resources.ApplyResources(this.panel1, "panel1");
        this.panel1.Name = "panel1";
        // 
        // panel2
        // 
        resources.ApplyResources(this.panel2, "panel2");
        this.panel2.Name = "panel2";
        // 
        // panel3
        // 
        resources.ApplyResources(this.panel3, "panel3");
        this.panel3.Name = "panel3";
        // 
        // frmMessage
        // 
        resources.ApplyResources(this.frmMessage, "frmMessage");
        this.frmMessage.ImageList = this.imageList1;
        this.frmMessage.Name = "frmMessage";
        this.frmMessage.Paint += new System.Windows.Forms.PaintEventHandler(this.frmMessage_Paint);
        // 
        // label1
        // 
        resources.ApplyResources(this.label1, "label1");
        this.label1.Name = "label1";
        // 
        // label2
        // 
        resources.ApplyResources(this.label2, "label2");
        this.label2.Name = "label2";
        // 
        // label3
        // 
        resources.ApplyResources(this.label3, "label3");
        this.label3.Name = "label3";
        // 
        // label4
        // 
        resources.ApplyResources(this.label4, "label4");
        this.label4.Name = "label4";
        // 
        // label5
        // 
        resources.ApplyResources(this.label5, "label5");
        this.label5.Name = "label5";
        // 
        // label6
        // 
        resources.ApplyResources(this.label6, "label6");
        this.label6.Name = "label6";
        // 
        // label7
        // 
        resources.ApplyResources(this.label7, "label7");
        this.label7.Name = "label7";
        // 
        // panel4
        // 
        resources.ApplyResources(this.panel4, "panel4");
        this.panel4.Name = "panel4";
        // 
        // labelAutoPumpInk
        // 
        resources.ApplyResources(this.labelAutoPumpInk, "labelAutoPumpInk");
        this.labelAutoPumpInk.Name = "labelAutoPumpInk";
        // 
        // labelContinue
        // 
        resources.ApplyResources(this.labelContinue, "labelContinue");
        this.labelContinue.Name = "labelContinue";
        // 
        // m_LabelAbortCurrent
        // 
        resources.ApplyResources(this.m_LabelAbortCurrent, "m_LabelAbortCurrent");
        this.m_LabelAbortCurrent.Name = "m_LabelAbortCurrent";
        // 
        // m_LabelAbortAll
        // 
        resources.ApplyResources(this.m_LabelAbortAll, "m_LabelAbortAll");
        this.m_LabelAbortAll.Name = "m_LabelAbortAll";
        // 
        // checkBoxRememberMyChoose
        // 
        resources.ApplyResources(this.checkBoxRememberMyChoose, "checkBoxRememberMyChoose");
        this.checkBoxRememberMyChoose.Name = "checkBoxRememberMyChoose";
        this.checkBoxRememberMyChoose.UseVisualStyleBackColor = true;
        // 
        // MyMessageBox
        // 
        resources.ApplyResources(this, "$this");
        this.Controls.Add(this.checkBoxRememberMyChoose);
        this.Controls.Add(this.m_LabelAbortAll);
        this.Controls.Add(this.m_LabelAbortCurrent);
        this.Controls.Add(this.labelContinue);
        this.Controls.Add(this.labelAutoPumpInk);
        this.Controls.Add(this.label7);
        this.Controls.Add(this.label6);
        this.Controls.Add(this.label5);
        this.Controls.Add(this.label4);
        this.Controls.Add(this.label3);
        this.Controls.Add(this.label2);
        this.Controls.Add(this.label1);
        this.Controls.Add(this.frmMessage);
        this.Controls.Add(this.panel1);
        this.Controls.Add(this.panel2);
        this.Controls.Add(this.panel3);
        this.Controls.Add(this.panel4);
        this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.Name = "MyMessageBox";
        this.Paint += new System.Windows.Forms.PaintEventHandler(this.newMessageBox_Paint);
        this.ResumeLayout(false);
        this.PerformLayout();

	}

    public uint m_KernelMessage = SystemCall.RegisterWindowMessage("BYHX_Message_PrinterManager");

    private Image frmIcon = null;

    private Button btnOK;
    private Button btnAbort;
    private Button btnRetry;
    private Button btnIgnore;
    private Button btnCancel;
    private Button btnYes;
    private Button btnNo;
	private Button btnStopPumpnk;
	private Button btnContinue;
    private Button btnAbortCurrent;
    private Button btnAbortAll;
#if DEBUG
    private Button btnDebug;
#endif
    private System.Windows.Forms.Panel panel1;
    private System.Windows.Forms.Panel panel2;
    private System.Windows.Forms.Panel panel3;
	private System.Windows.Forms.ImageList imageList1;
	private System.ComponentModel.IContainer components;
	private System.Windows.Forms.Label frmMessage;

    private DialogResult CYReturnButton;
	private int margin = 10;
	private System.Windows.Forms.Label label1;
	private System.Windows.Forms.Label label2;
	private System.Windows.Forms.Label label3;
	private System.Windows.Forms.Label label4;
	private System.Windows.Forms.Label label5;
	private System.Windows.Forms.Label label6;
	private System.Windows.Forms.Label label7;
	private System.Windows.Forms.Panel panel4;
	private System.Windows.Forms.Label labelContinue;
	private System.Windows.Forms.Label labelAutoPumpInk;
	private string Msg = string.Empty;
    private Label m_LabelAbortCurrent;
    private Label m_LabelAbortAll;
    private CheckBox checkBoxRememberMyChoose;
	private bool m_bShowIcon = false;

	protected override void WndProc(ref Message m)
	{
		base.WndProc(ref m);

		if(m.Msg == this.m_KernelMessage)
		{
			ProceedKernelMessage(m.WParam,m.LParam);
		}
	}

	private void ProceedKernelMessage(IntPtr wParam, IntPtr lParam)
	{
		CoreMsgEnum	kParam	= (CoreMsgEnum)wParam.ToInt32();

		switch(kParam)
		{
			case CoreMsgEnum.Power_On:
			{
				this.DialogResult = CYReturnButton = DialogResult.Cancel;
				this.Close();
				break;
			}
			case CoreMsgEnum.Status_Change:
			{
				JetStatusEnum status = (JetStatusEnum)lParam.ToInt32();
				if(status == JetStatusEnum.PowerOff || status == JetStatusEnum.Error)
				{
					this.DialogResult = CYReturnButton = DialogResult.Cancel;
					this.Close();
				}
				break;
			}
		}
	}

    private void ShowDialogEx()
    {
        if (_bModleDialog)
        {
            this.ShowDialog();
        }
        else
        {
            this.Show();
        }
    }

	/// <summary>
	/// Message: Text to display in the message box.
	/// </summary>
	public DialogResult Show(string Message)
	{
		try
		{
		    if (_showCalled != null)
		        _showCalled();
			Msg = Message;
			ShowOKButton(panel2);
			AutoSizeControl(Message,MessageBoxButtons.OK, false);
		    if (_bModleDialog)
		    {
		        this.ShowDialogEx();
		    }
            else
            {
                this.Show();
            }
		}
		catch (Exception ex)
		{
			System.Diagnostics.Debug.Assert(false,ex.Message);
		}
		return CYReturnButton;

	}

	/// <summary>
	/// Title: Text to display in the title bar of the messagebox.
	/// </summary>
	public DialogResult Show(string Message, string Title)
	{
		try
		{
            if (_showCalled != null)
                _showCalled();
            this.Text = Title;
			Msg = Message;
			ShowOKButton(panel2);
			AutoSizeControl(Message,MessageBoxButtons.OK, false);
			this.ShowDialogEx();
		}
		catch (Exception ex)
		{
			System.Diagnostics.Debug.Assert(false,ex.Message);
		}
		return CYReturnButton;
	}

	/// <summary>
	/// MButtons: Display CYButtons on the message box.
	/// </summary>
	public DialogResult Show(string Message, string Title, MessageBoxButtons MButtons)
	{
		try
		{
            if (_showCalled != null)
                _showCalled();
            this.Text = Title; // Set the title of the MessageBox
			Msg = Message; //Set the text of the MessageBox
			ButtonStatements(MButtons); // ButtonStatements method is responsible for showing the appropreiate buttons
			AutoSizeControl(Message,MButtons, false);
			this.ShowDialogEx(); // Show the MessageBox as a Dialog.
		}
		catch (Exception ex)
		{
			System.Diagnostics.Debug.Assert(false,ex.Message);
		}
		return CYReturnButton; // Return the button click as an Enumerator
	}

	/// <summary>
	/// MIcon: Display CYIcon on the message box.
	/// </summary>
	public DialogResult Show(string Message, string Title, MessageBoxButtons MButtons, MessageBoxIcon MIcon)
	{
		try
		{
            if (_showCalled != null)
                _showCalled();
            this.Text = Title;
			Msg = Message;
			ButtonStatements(MButtons);
			IconStatements(MIcon);
			AutoSizeControl(Message,MButtons, true);
			this.ShowDialogEx();
		}
		catch (Exception ex)
		{
			System.Diagnostics.Debug.Assert(false,ex.Message);
		}
		return CYReturnButton;
	}


	public DialogResult ShowPumpInkTimeOut(string Message, string Title)
	{
		try
		{
            if (_showCalled != null)
                _showCalled();
            this.Text = Title;
			Msg = Message;
			//ShowStopPumpnkButton(panel1);
			ShowContinueButton(panel2);
            //this.panel1.Visible = true;
			this.panel2.Visible = true;
			
			IconStatements(MessageBoxIcon.Warning);
			AutoSizeControl(Message,MessageBoxButtons.YesNo, true);
			this.ShowDialogEx();
		}
		catch (Exception ex)
		{
			System.Diagnostics.Debug.Assert(false,ex.Message);
		}
		return CYReturnButton;
	}

    public DialogResult ShowAbortPrinting(string Message, string Title)
    {
        try
        {
            if (_showCalled != null)
                _showCalled();
            this.Text = Title;
            Msg = Message;
            ShowAbortCurrentButton(panel1);
            ShowAbortAllButton(panel2);
            ShowCancelButton(panel3);
            this.panel1.Visible = true;
            this.panel2.Visible = true;
            this.panel3.Visible = true;

            IconStatements(MessageBoxIcon.Warning);
            AutoSizeControl(Message, MessageBoxButtons.YesNoCancel, true);
            this.ShowDialogEx();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.Assert(false, ex.Message);
        }
        return CYReturnButton;
    }


    void btnOK_Click(object sender, EventArgs e)
    {
        CYReturnButton = DialogResult.OK;
        this.Dispose();
    }

    void btnAbort_Click(object sender, EventArgs e)
    {
        CYReturnButton = DialogResult.Abort;
        this.Dispose();
    }

    void btnRetry_Click(object sender, EventArgs e)
    {
        CYReturnButton = DialogResult.Retry;
        this.Dispose();
    }

    void btnIgnore_Click(object sender, EventArgs e)
    {
        CYReturnButton = DialogResult.Ignore;
        this.Dispose();
    }

    void btnCancel_Click(object sender, EventArgs e)
    {
        CYReturnButton = DialogResult.Cancel;
        this.Dispose();
    }

    void btnYes_Click(object sender, EventArgs e)
    {
        CYReturnButton = DialogResult.Yes;
        this.Dispose();
    }

    void btnNo_Click(object sender, EventArgs e)
    {
        CYReturnButton = DialogResult.No;
        this.Dispose();
    }

	void btnStopPumpink_Click(object sender, EventArgs e)
	{
		CYReturnButton = DialogResult.Abort;
		this.Dispose();
	}

	void btnContinue_Click(object sender, EventArgs e)
	{
		CYReturnButton = DialogResult.Ignore;
		this.Dispose();
	}

    void btnAbortCurrent_Click(object sender, EventArgs e)
    {
        CYReturnButton = DialogResult.OK;
        this.Dispose();
    }

    void btnAbortAll_Click(object sender, EventArgs e)
    {
        CYReturnButton = DialogResult.Yes;
        this.Dispose();
    }

	void btnDebug_Click(object sender, EventArgs e)
    {
#if DEBUG
        byte[] buf = new byte[64];
        CoreInterface.GetDebugInfo(buf, 64);
#endif
    }

    private void ShowOKButton(Panel flpButtons)
    {
        btnOK = new Button();
		btnOK.FlatStyle = FlatStyle.System;
        btnOK.Text = this.label1.Text;//"OK";
        btnOK.Dock = DockStyle.Fill;
        btnOK.Size = new System.Drawing.Size(80, 25);
        btnOK.Font = new Font("Tahoma", 8, FontStyle.Regular);
        btnOK.Click += new EventHandler(btnOK_Click);
		btnOK.Dock = DockStyle.Fill;
        flpButtons.Controls.Add(btnOK);
    }

    private void ShowAbortButton(Panel flpButtons)
    {
        btnAbort = new Button();
		btnAbort.FlatStyle = FlatStyle.System;
        btnAbort.Text = this.label6.Text;//"Abort";
        btnAbort.Size = new System.Drawing.Size(80, 25);
        btnAbort.Font = new Font("Tahoma", 8, FontStyle.Regular);
        btnAbort.Click += new EventHandler(btnAbort_Click);
       	btnAbort.Dock = DockStyle.Fill; 
		flpButtons.Controls.Add(btnAbort);
    }

    private void ShowRetryButton(Panel flpButtons)
    {
        btnRetry = new Button();
		btnRetry.FlatStyle = FlatStyle.System;
        btnRetry.Text = this.label2.Text;//"Retry";
        btnRetry.Size = new System.Drawing.Size(80, 25);
        btnRetry.Font = new Font("Tahoma", 8, FontStyle.Regular);
        btnRetry.Click += new EventHandler(btnRetry_Click);
		btnRetry.Dock = DockStyle.Fill; 
		flpButtons.Controls.Add(btnRetry);
    }

    private void ShowIgnoreButton(Panel flpButtons)
    {
        btnIgnore = new Button();
		btnIgnore.FlatStyle = FlatStyle.System;
        btnIgnore.Text = this.label7.Text;//"Ignore";
        btnIgnore.Size = new System.Drawing.Size(80, 25);
        btnIgnore.Font = new Font("Tahoma", 8, FontStyle.Regular);
        btnIgnore.Click += new EventHandler(btnIgnore_Click);
		btnIgnore.Dock = DockStyle.Fill; 
		flpButtons.Controls.Add(btnIgnore);
    }

    private void ShowCancelButton(Panel flpButtons)
    {
        btnCancel = new Button();
		btnCancel.FlatStyle = FlatStyle.System;
        btnCancel.Text = this.label3.Text;//"Cancel";
        btnCancel.Size = new System.Drawing.Size(80, 25);
        btnCancel.Font = new Font("Tahoma", 8, FontStyle.Regular);
        btnCancel.Click += new EventHandler(btnCancel_Click);
		btnCancel.Dock = DockStyle.Fill; 
		flpButtons.Controls.Add(btnCancel);
    }

    private void ShowYesButton(Panel flpButtons)
    {
        btnYes = new Button();
		btnYes.FlatStyle = FlatStyle.System;
        btnYes.Text = this.label4.Text;//"Yes";
        btnYes.Size = new System.Drawing.Size(80, 25);
        btnYes.Font = new Font("Tahoma", 8, FontStyle.Regular);
        btnYes.Click += new EventHandler(btnYes_Click);
		btnYes.Dock = DockStyle.Fill; 
		flpButtons.Controls.Add(btnYes);
    }

    private void ShowNoButton(Panel flpButtons)
    {
        btnNo = new Button();
		btnNo.FlatStyle = FlatStyle.System;
        btnNo.Text = this.label5.Text;//"No";
        btnNo.Size = new System.Drawing.Size(80, 25);
        btnNo.Font = new Font("Tahoma", 8, FontStyle.Regular);
        btnNo.Click += new EventHandler(btnNo_Click);
		btnNo.Dock = DockStyle.Fill; 
		flpButtons.Controls.Add(btnNo);
    }

	private void ShowStopPumpnkButton(Panel flpButtons)
	{
		btnStopPumpnk = new Button();
		btnStopPumpnk.FlatStyle = FlatStyle.System;
		btnStopPumpnk.Text = this.labelAutoPumpInk.Text;//"Yes";
		btnStopPumpnk.Size = new System.Drawing.Size(80, 25);
		btnStopPumpnk.Font = new Font("Tahoma", 8, FontStyle.Regular);
		btnStopPumpnk.Click += new EventHandler(btnStopPumpink_Click);
		btnStopPumpnk.Dock = DockStyle.Fill; 
		flpButtons.Controls.Add(btnStopPumpnk);
	}

	private void ShowContinueButton(Panel flpButtons)
	{
		btnContinue = new Button();
		btnContinue.FlatStyle = FlatStyle.System;
		btnContinue.Text = this.labelContinue.Text;//"ignore";label7
		btnContinue.Size = new System.Drawing.Size(80, 25);
		btnContinue.Font = new Font("Tahoma", 8, FontStyle.Regular);
		btnContinue.Click += new EventHandler(btnContinue_Click);
		btnContinue.Dock = DockStyle.Fill; 
		flpButtons.Controls.Add(btnContinue);
	}

    private void ShowAbortCurrentButton(Panel flpButtons)
    {
        btnAbortCurrent = new Button();
        btnAbortCurrent.FlatStyle = FlatStyle.System;
        btnAbortCurrent.Text = this.m_LabelAbortCurrent.Text;
        btnAbortCurrent.Size = new System.Drawing.Size(80, 25);
        btnAbortCurrent.Font = new Font("Tahoma", 8, FontStyle.Regular);
        btnAbortCurrent.Click += new EventHandler(btnAbortCurrent_Click);
        btnAbortCurrent.Dock = DockStyle.Fill;
        flpButtons.Controls.Add(btnAbortCurrent);
    }

    private void ShowAbortAllButton(Panel flpButtons)
    {
        btnAbortAll = new Button();
        btnAbortAll.FlatStyle = FlatStyle.System;
        btnAbortAll.Text = this.m_LabelAbortAll.Text;
        btnAbortAll.Size = new System.Drawing.Size(80, 25);
        btnAbortAll.Font = new Font("Tahoma", 8, FontStyle.Regular);
        btnAbortAll.Click += new EventHandler(btnAbortAll_Click);
        btnAbortAll.Dock = DockStyle.Fill;
        flpButtons.Controls.Add(btnAbortAll);
    }

#if DEBUG
	private void ShowDebugButton(Panel flpButtons)
	{
		btnDebug = new Button();
		btnDebug.FlatStyle = FlatStyle.System;
		btnDebug.Text = "Debug";//"No";
		btnDebug.Size = new System.Drawing.Size(80, 25);
		btnDebug.Font = new Font("Tahoma", 8, FontStyle.Regular);
		btnDebug.Click += new EventHandler(btnDebug_Click);
		btnDebug.Dock = DockStyle.Fill; 
		flpButtons.Controls.Add(btnDebug);
	}
#endif
    private void ButtonStatements(MessageBoxButtons MButtons)
    {
#if DEBUG
		if(PubFunc.IsFactoryUser())		
		{
			this.ShowDebugButton(this.panel4);
			this.panel4.Visible = true;
		}
#endif
        if (MButtons == MessageBoxButtons.AbortRetryIgnore)
        {
            ShowIgnoreButton(panel1);
            ShowRetryButton(panel2);
            ShowAbortButton(panel3);
			this.panel1.Visible = true;
			this.panel2.Visible = true;
			this.panel3.Visible = true;
			return;
        }

        if (MButtons == MessageBoxButtons.OK)
        {
            ShowOKButton(panel1);
			this.panel1.Visible = true;
			return;
        }

        if (MButtons == MessageBoxButtons.OKCancel)
        {
            ShowCancelButton(panel1);
            ShowOKButton(panel2);
			this.panel1.Visible = true;
			this.panel2.Visible = true;
			return;
        }

        if (MButtons == MessageBoxButtons.RetryCancel)
        {
            ShowCancelButton(panel1);
            ShowRetryButton(panel2);
			this.panel1.Visible = true;
			this.panel2.Visible = true;
			return;
        }

        if (MButtons == MessageBoxButtons.YesNo)
        {
            ShowNoButton(panel1);
            ShowYesButton(panel2);
			this.panel1.Visible = true;
			this.panel2.Visible = true;
			return;
        }

        if (MButtons == MessageBoxButtons.YesNoCancel)
        {
            ShowCancelButton(panel1);
            ShowNoButton(panel2);
            ShowYesButton(panel3);
			this.panel1.Visible = true;
			this.panel2.Visible = true;
			this.panel3.Visible = true;
			return;
        }
    }

    private void IconStatements(MessageBoxIcon MIcon)
    {
        if (MIcon == MessageBoxIcon.Error ||MIcon == MessageBoxIcon.Hand ||MIcon == MessageBoxIcon.Stop )
        {
			this.frmMessage.ImageIndex = 0;
        }

		if (MIcon == MessageBoxIcon.Warning  || MIcon == MessageBoxIcon.Exclamation)
		{
			this.frmMessage.ImageIndex = 1;
		}

        if (MIcon == MessageBoxIcon.Information || MIcon == MessageBoxIcon.Asterisk)
        {
			this.frmMessage.ImageIndex = 2;
        }

        if (MIcon == MessageBoxIcon.Question)
        {
			this.frmMessage.ImageIndex = 3;
        }
    }

	private void AutoSizeControl(string msg,MessageBoxButtons MButtons, bool bIcon)
	{
		this.m_bShowIcon = bIcon;
		Graphics g = this.frmMessage.CreateGraphics();
		SizeF szf = g.MeasureString(this.Msg,this.frmMessage.Font,int.MaxValue);
		g.Dispose();
		SizeF icosz = this.imageList1.ImageSize;
		float frmW = SystemInformation.BorderSize.Width * 2 + szf.Width;
		if(bIcon)
			frmW += icosz.Width + 2*margin;

		this.Width = Convert.ToInt32(frmW+1);
		
		int buttonAreaW = 0;
		if(MButtons == MessageBoxButtons.OK)
		{
			buttonAreaW = this.panel1.Width + margin * 2 + SystemInformation.BorderSize.Width * 2;
			if(this.Width < buttonAreaW)
				this.Width = buttonAreaW;
			this.panel1.Location = new Point((this.Width - this.panel1.Width)/2,this.panel1.Location.Y);
		}
		else if(MButtons == MessageBoxButtons.OKCancel || MButtons == MessageBoxButtons.RetryCancel || MButtons == MessageBoxButtons.YesNo)
		{
			buttonAreaW = this.panel1.Width * 2 + margin * 3 + SystemInformation.BorderSize.Width * 2;
			if(this.Width < buttonAreaW)
				this.Width = buttonAreaW;
			this.panel1.Location = new Point((this.Width - this.panel1.Width * 2 - margin)/2,this.panel1.Location.Y);
			this.panel2.Location = new Point(this.panel1.Location.X + this.panel1.Width + margin,this.panel1.Location.Y);
		}
		else
		{
			buttonAreaW = this.panel1.Width * 3 + margin * 4 + SystemInformation.BorderSize.Width * 2;
			if(this.Width < buttonAreaW)
				this.Width = buttonAreaW;
			this.panel1.Location = new Point((this.Width - this.panel1.Width * 3 - margin * 2)/2,this.panel1.Location.Y);
			this.panel2.Location = new Point(this.panel1.Location.X + this.panel1.Width + margin,this.panel1.Location.Y);
			this.panel3.Location = new Point(this.panel2.Location.X + this.panel2.Width + margin,this.panel1.Location.Y);
		}
#if DEBUG
		if(PubFunc.IsFactoryUser())
		{
			this.Height += this.panel4.Height+10;
			this.panel4.Location = new Point(this.panel1.Location.X,this.panel1.Bottom +8);
		}
#endif
	}
    void newMessageBox_Paint(object sender, PaintEventArgs e)
    {
        //Graphics g = e.Graphics;
        //Rectangle frmTitleL = new Rectangle(0, 0, (this.Width / 2), 22);
        //Rectangle frmTitleR = new Rectangle((this.Width / 2), 0, (this.Width / 2), 22);
        //Rectangle frmMessageBox = new Rectangle(0, 0, (this.Width - 1), (this.Height - 1));
        //LinearGradientBrush frmLGBL = new LinearGradientBrush(frmTitleL, Color.FromArgb(87, 148, 160), Color.FromArgb(209, 230, 243), LinearGradientMode.Horizontal);
        //LinearGradientBrush frmLGBR = new LinearGradientBrush(frmTitleR, Color.FromArgb(209, 230, 243), Color.FromArgb(87, 148, 160), LinearGradientMode.Horizontal);
        //Pen frmPen = new Pen(Color.FromArgb(63, 119, 143), 1);
        //g.FillRectangle(frmLGBL, frmTitleL);
        //g.FillRectangle(frmLGBR, frmTitleR);
        //g.DrawRectangle(frmPen, frmMessageBox);
    }

	private void frmMessage_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
	{
		PointF pp = new Point(margin,0);
		if(m_bShowIcon)
			pp = new Point(this.imageList1.ImageSize.Width + margin,0);
		SizeF szf = e.Graphics.MeasureString(this.Msg,this.frmMessage.Font,int.MaxValue);
		if(this.frmMessage.Height > szf.Height)
			pp.Y = (this.frmMessage.Height - szf.Height) / 2;

		RectangleF rc = new RectangleF(pp,szf.ToSize());
		e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
		e.Graphics.DrawString(this.Msg,this.frmMessage.Font,SystemBrushes.WindowText,rc.Location);
	}
}
