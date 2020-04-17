using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text;

namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// ErrorViewForm 的摘要说明。
	/// </summary>
    public class ErrorViewForm : ByhxBaseChildForm
	{
		private System.Windows.Forms.Panel tableLayoutPanel1;
		private BYHXPrinterManager.GradientControls.CrystalPanel panel1;
        private System.Windows.Forms.Button button1;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		protected Control _oOwner;
		private Point _oOrigin;
		private Form _oOwnerForm;
        private bool _bMergeSameMsg = true; //默认启用合并功能
        private ListView richTextBox1;
	    private Tuple<string, ListViewItem, int> _lastMsg = null;
        private ColumnHeader columnHeader1;
        /// <summary>
        /// 合并显示相同信息
        /// </summary>
        public bool MergeSameMsg
	    {
	        get
	        {
                return _bMergeSameMsg;
	        }
	        set
	        {
                this._bMergeSameMsg = value;
	        }
	    }

		private bool _bExpand = false;
		public bool IsExpanded
		{
			get
			{
				return _bExpand;
			}
			set
			{
				this._bExpand = value;
			}
		}
		/// <summary>
		/// Default constructor
		/// </summary>
		public ErrorViewForm() : this(null)
		{
            richTextBox1.Items.Clear();
		}
		/// <summary>
		/// Constructor with parent window and step of sliding motion
		/// </summary>
		public ErrorViewForm(Control poOwner)
		{
			InitializeComponent();
		    richTextBox1.Items.Clear();
		    richTextBox1.Columns[0].Width = this.Width;
			_oOwner = poOwner;
			if (poOwner != null)
				_oOwnerForm = poOwner.FindForm();
		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("11111111111111111");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("22222222222222");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("3333333333333333");
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            this.tableLayoutPanel1 = new System.Windows.Forms.Panel();
            this.richTextBox1 = new System.Windows.Forms.ListView();
            this.panel1 = new BYHXPrinterManager.GradientControls.CrystalPanel();
            this.button1 = new System.Windows.Forms.Button();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Control;
            this.tableLayoutPanel1.Controls.Add(this.richTextBox1);
            this.tableLayoutPanel1.Controls.Add(this.panel1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(5, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Size = new System.Drawing.Size(542, 171);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Info;
            this.richTextBox1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.FullRowSelect = true;
            this.richTextBox1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.richTextBox1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.richTextBox1.Location = new System.Drawing.Point(0, 20);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(542, 151);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.UseCompatibleStateImageBehavior = false;
            this.richTextBox1.View = System.Windows.Forms.View.Details;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Divider = false;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            style1.Color1 = System.Drawing.Color.LightBlue;
            style1.Color2 = System.Drawing.Color.SteelBlue;
            this.panel1.GradientColors = style1;
            this.panel1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(542, 20);
            this.panel1.TabIndex = 1;
            this.panel1.TabStop = false;
            this.panel1.TreeColorGradient = true;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Transparent;
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Location = new System.Drawing.Point(522, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(20, 20);
            this.button1.TabIndex = 0;
            this.button1.TabStop = false;
            this.button1.Text = "X";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 112;
            // 
            // ErrorViewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(552, 176);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ErrorViewForm";
            this.Padding = new System.Windows.Forms.Padding(5, 0, 5, 5);
            this.ShowInTaskbar = false;
            this.Text = "ErrorViewForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public void Slide()
		{
			if(!_bExpand)
				Show();
			else
				Hide();
			_bExpand = !_bExpand;
		}

		private void SlideDialog_Move(object sender, System.EventArgs e)
		{
			SetSlideLocation();
		}

		private void SlideDialog_Resize(object sender, System.EventArgs e)
		{
			SetSlideLocation();
		}
		private void SlideDialog_Closed(object sender, System.EventArgs e)
		{
			Close();
		}

		private void SetSlideLocation()
		{
			if (_oOwner != null)
			{
				_oOrigin = new Point();
				_oOrigin = _oOwnerForm.PointToScreen(_oOwner.Location);
				Width = _oOwner.Width;
				_oOrigin.Y -= this.Height;
				Location = _oOrigin;
			}
		}
		protected override void OnLoad(System.EventArgs e)
		{
			SetSlideLocation();
			if (_oOwner != null)
			{
				_oOwnerForm.LocationChanged += new System.EventHandler(this.SlideDialog_Move);
				_oOwnerForm.SizeChanged +=new EventHandler(this.SlideDialog_Move);
				_oOwner.Resize += new System.EventHandler(this.SlideDialog_Resize);
				_oOwnerForm.Closed += new System.EventHandler(this.SlideDialog_Closed);
			}
		}
        /// <summary>
        /// 信息列表显示的最大行数
        /// </summary>
	    private const int MaxMsgCount = 10000;

	    public void PrintJobInfomation(string mSErrorinfo, Color textcolor)
	    {
	        if (_bMergeSameMsg)
	        {
	            if (_lastMsg == null
	                || (_lastMsg != null && _lastMsg.Item1 != mSErrorinfo))
	            {
	                ListViewItem item = new ListViewItem();
	                string info = DateTime.Now.GetDateTimeFormats()[0x5c] + " " + mSErrorinfo;
	                item.Text = info;
	                item.ForeColor = textcolor;
	                richTextBox1.Items.Add(item);
	                _lastMsg = new Tuple<string, ListViewItem, int>(mSErrorinfo, item, 1);
	            }
	            else
	            {
	                if (_lastMsg != null)
	                {
	                    _lastMsg = new Tuple<string, ListViewItem, int>(_lastMsg.Item1, _lastMsg.Item2,
	                        _lastMsg.Item3 + 1);
	                    ListViewItem item = _lastMsg.Item2;
	                    string info = DateTime.Now.GetDateTimeFormats()[0x5c] + " " + mSErrorinfo + "(" + _lastMsg.Item3 +
	                                  ")";
	                    item.Text = info;
	                    item.ForeColor = textcolor;
	                }
	            }
	        }
	        else
	        {
	            ListViewItem item = new ListViewItem();
	            string info = DateTime.Now.GetDateTimeFormats()[0x5c] + " " + mSErrorinfo;
	            item.Text = info;
	            item.ForeColor = textcolor;
	            richTextBox1.Items.Add(item);
	            _lastMsg = new Tuple<string, ListViewItem, int>(mSErrorinfo, item, 1);
	        }

	        if (richTextBox1.Items.Count > MaxMsgCount)
	        {
	            richTextBox1.Items.RemoveAt(0);
	        }

	        this.richTextBox1.EnsureVisible(richTextBox1.Items.Count - 1); //滚动到指定的行位置
	    }


	    public void PrintString(string info,UserLevel Level,ErrorAction erroraction)
		{
			if(erroraction == ErrorAction.Updating) return;

			Color textcolor = Color.Black;
			// Set the color of the item text.
			if(erroraction == ErrorAction.UserResume)
				textcolor = Color.Green;
			else if(erroraction == ErrorAction.Warning)
				textcolor = Color.Purple;
			else
				textcolor = Color.Red;
			PrintJobInfomation(info,textcolor);
		}

		public UserLevel getUserLevel()
		{
			return UserLevel.user;
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Hide();
			this._bExpand = false;
		}
	}
}
