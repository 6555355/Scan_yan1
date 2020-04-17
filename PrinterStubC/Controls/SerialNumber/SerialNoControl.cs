/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using System.Diagnostics;

namespace BYHXControls
{
	/// <summary>
	/// Summary description for SerialNoControl.
	/// </summary>
	public class SerialNoControl : System.Windows.Forms.UserControl
	{
		private string m_sepString = "-";
		private int m_nCharPerUnit = 4;
		private const int m_Gap = 0;
		private const int m_TextWidth = 56;
		private const int m_TextHeight = 21;
		private SerialNoTextBox m_TextBoxSample;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public SerialNoControl()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
			m_TextBoxSample.m_ParentHandle = this.Handle;
		}

		/// <summary> 
		/// Clean up any resources being used.
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(SerialNoControl));
			this.m_TextBoxSample = new BYHXControls.SerialNoTextBox();
			this.SuspendLayout();
			// 
			// m_TextBoxSample
			// 
			this.m_TextBoxSample.AccessibleDescription = resources.GetString("m_TextBoxSample.AccessibleDescription");
			this.m_TextBoxSample.AccessibleName = resources.GetString("m_TextBoxSample.AccessibleName");
			this.m_TextBoxSample.Anchor = ((System.Windows.Forms.AnchorStyles)(resources.GetObject("m_TextBoxSample.Anchor")));
			this.m_TextBoxSample.AutoSize = ((bool)(resources.GetObject("m_TextBoxSample.AutoSize")));
			this.m_TextBoxSample.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("m_TextBoxSample.BackgroundImage")));
			this.m_TextBoxSample.Dock = ((System.Windows.Forms.DockStyle)(resources.GetObject("m_TextBoxSample.Dock")));
			this.m_TextBoxSample.Enabled = ((bool)(resources.GetObject("m_TextBoxSample.Enabled")));
			this.m_TextBoxSample.Font = ((System.Drawing.Font)(resources.GetObject("m_TextBoxSample.Font")));
			this.m_TextBoxSample.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("m_TextBoxSample.ImeMode")));
			this.m_TextBoxSample.Location = ((System.Drawing.Point)(resources.GetObject("m_TextBoxSample.Location")));
			this.m_TextBoxSample.MaxLength = ((int)(resources.GetObject("m_TextBoxSample.MaxLength")));
			this.m_TextBoxSample.Multiline = ((bool)(resources.GetObject("m_TextBoxSample.Multiline")));
			this.m_TextBoxSample.Name = "m_TextBoxSample";
			this.m_TextBoxSample.PasswordChar = ((char)(resources.GetObject("m_TextBoxSample.PasswordChar")));
			this.m_TextBoxSample.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("m_TextBoxSample.RightToLeft")));
			this.m_TextBoxSample.ScrollBars = ((System.Windows.Forms.ScrollBars)(resources.GetObject("m_TextBoxSample.ScrollBars")));
			this.m_TextBoxSample.Size = ((System.Drawing.Size)(resources.GetObject("m_TextBoxSample.Size")));
			this.m_TextBoxSample.TabIndex = ((int)(resources.GetObject("m_TextBoxSample.TabIndex")));
			this.m_TextBoxSample.Text = resources.GetString("m_TextBoxSample.Text");
			this.m_TextBoxSample.TextAlign = ((System.Windows.Forms.HorizontalAlignment)(resources.GetObject("m_TextBoxSample.TextAlign")));
			this.m_TextBoxSample.Visible = ((bool)(resources.GetObject("m_TextBoxSample.Visible")));
			this.m_TextBoxSample.WordWrap = ((bool)(resources.GetObject("m_TextBoxSample.WordWrap")));
			this.m_TextBoxSample.TextChanged += new System.EventHandler(this.OnTextChanged);
			// 
			// SerialNoControl
			// 
			this.AccessibleDescription = resources.GetString("$this.AccessibleDescription");
			this.AccessibleName = resources.GetString("$this.AccessibleName");
			this.AutoScroll = ((bool)(resources.GetObject("$this.AutoScroll")));
			this.AutoScrollMargin = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMargin")));
			this.AutoScrollMinSize = ((System.Drawing.Size)(resources.GetObject("$this.AutoScrollMinSize")));
			this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
			this.Controls.Add(this.m_TextBoxSample);
			this.Enabled = ((bool)(resources.GetObject("$this.Enabled")));
			this.Font = ((System.Drawing.Font)(resources.GetObject("$this.Font")));
			this.ImeMode = ((System.Windows.Forms.ImeMode)(resources.GetObject("$this.ImeMode")));
			this.Location = ((System.Drawing.Point)(resources.GetObject("$this.Location")));
			this.Name = "SerialNoControl";
			this.RightToLeft = ((System.Windows.Forms.RightToLeft)(resources.GetObject("$this.RightToLeft")));
			this.Size = ((System.Drawing.Size)(resources.GetObject("$this.Size")));
			this.Leave += new System.EventHandler(this.OnKillFocus);
			this.ResumeLayout(false);

		}
		#endregion
		public int Count
		{
			get
			{
				return this.Controls.Count;
			}
			set
			{
				int curCount = this.Controls.Count;
				if(value <1 || curCount == value)
					return;
				this.SuspendLayout();
				if(value>curCount)
				{
					for (int i=curCount; i< value;i++)
					{
						SerialNoTextBox textBox = new SerialNoTextBox();
						//TextBoxClone(textBox,m_TextBoxSample);
						textBox.Size = new System.Drawing.Size(m_TextWidth, m_TextHeight);
						textBox.Location = new System.Drawing.Point((m_TextWidth+m_Gap)*i, 0);
						textBox.m_ParentHandle = this.Handle;
						textBox.TextChanged += new System.EventHandler(this.OnTextChanged);


						this.Controls.Add(textBox);
					}
				}
				else
				{
					for (int j=curCount-1;j>value;j++)
					{
						SerialNoTextBox textBox = (SerialNoTextBox)this.Controls[j];
						textBox.m_ParentHandle = IntPtr.Zero;
						this.Controls.Remove(textBox);
					}
				}
				this.Size = new System.Drawing.Size((m_TextWidth+m_Gap)*value-m_Gap, m_TextHeight);
				this.ResumeLayout(false);
			}
		}
		public int CharsPerUnit
		{
			get
			{
				return m_nCharPerUnit;
			}
			set
			{
				m_nCharPerUnit = value;
				int count = this.Controls.Count;
				for (int i=0; i< count;i++)
					((SerialNoTextBox)this.Controls[i]).MaxLength = m_nCharPerUnit;

			}
		}
		public string SeparateString
		{
			get
			{
				return m_sepString;
			}
			set
			{
				m_sepString = value;
			}
		}
		public static void TextBoxClone(TextBox textBox, TextBox controlsource)
		{
			textBox.AccessibleDescription = controlsource.AccessibleDescription;
			textBox.AccessibleName = controlsource.AccessibleName;
			textBox.Anchor = controlsource.Anchor;
			textBox.AutoSize = controlsource.AutoSize;
			textBox.BackgroundImage = controlsource.BackgroundImage;
			textBox.Dock = controlsource.Dock;
			textBox.Enabled = controlsource.Enabled;
			textBox.Font = controlsource.Font;
			textBox.ImeMode = controlsource.ImeMode;
			textBox.MaxLength = controlsource.MaxLength;
			textBox.Multiline = controlsource.Multiline;
			textBox.PasswordChar = controlsource.PasswordChar;
			textBox.ReadOnly = controlsource.ReadOnly;
			textBox.RightToLeft = controlsource.RightToLeft;
			textBox.ScrollBars = controlsource.ScrollBars;
			textBox.Size = controlsource.Size;
			textBox.TextAlign = controlsource.TextAlign;
			textBox.WordWrap = controlsource.WordWrap;
		}

		public string GetText()
		{
			SerialNoTextBox txtBox = (SerialNoTextBox)this.Controls[0];
			string strText = txtBox.Text;
			int count = this.Controls.Count;
			for (int i=1;i<count;i++)
			{
				strText +=(m_sepString + ((SerialNoTextBox)this.Controls[i]).Text);
			}
			if(strText != null && strText.Length == m_nCharPerUnit * count + count -1)
				return strText;
			else
				return null;
		}

		public void SetText(string strText)
		{
			string pwTexts = GetValid(strText,m_sepString,m_nCharPerUnit);
			if(pwTexts != null)
			{
				int count = this.Controls.Count;
				for (int i=0; i< count;i++)
					((SerialNoTextBox)this.Controls[i]).Text = pwTexts.Substring(m_nCharPerUnit *i,m_nCharPerUnit);
			}
		}


		private void OnTextChanged(object sender, System.EventArgs e)
		{
			SerialNoTextBox txtBox = (SerialNoTextBox)sender;
			if(txtBox.Text.Length == m_nCharPerUnit)
			{
				int count = this.Controls.Count;
				int curIndex = this.Controls.IndexOf(txtBox);
				//if(curIndex<count-1)
				((SerialNoTextBox)this.Controls[(curIndex+1)%count]).Focus();
			}
		}
		const int WM_PASTE	= 0x0302;
		const int WM_LBUTTONDOWN = 0x0201;
		protected override void WndProc(ref System.Windows.Forms.Message msg)
		{
			if(msg.Msg == WM_PASTE)
			{
				IDataObject data = Clipboard.GetDataObject();
				if(data != null && data.GetDataPresent(DataFormats.Text))
				{
					string str = (string)data.GetData(DataFormats.Text);
					str = str.Replace("-", "");

					int count = this.Controls.Count;
					int strlen = str.Length;
					for (int i=0; i< count;i++)
					{
						SerialNoTextBox txtBox = (SerialNoTextBox)this.Controls[i];
						if(strlen>=m_nCharPerUnit)
						{
							txtBox.Text =  str.Substring(m_nCharPerUnit * i, m_nCharPerUnit);
							strlen -= m_nCharPerUnit;
						}
						else if(strlen > 0)
						{
							txtBox.Text =  str.Substring(m_nCharPerUnit * i, strlen);
							strlen = 0;
						}
				
						txtBox.BackColor = Color.Beige;
					}
				}
				return;
			}
			else if(msg.Msg == WM_LBUTTONDOWN)
			{
				int count = this.Controls.Count;
				for (int i=0; i< count;i++)
					((SerialNoTextBox)this.Controls[i]).BackColor = Color.White;
				return;
			}
			base.WndProc(ref msg);
		}

		private void OnKillFocus(object sender, System.EventArgs e)
		{
			int count = this.Controls.Count;
			for (int i=0; i< count;i++)
				((SerialNoTextBox)this.Controls[i]).BackColor = Color.White;
		}


		public string GetValid(string str,string sSep, int  nCharPerUnit)
		{
			if(str == null)
				return null;
			string strTemp = str.Replace(sSep, "");
			strTemp = strTemp.Replace(" ", "");
			int index = strTemp.IndexOf("//",0);
			if(index >= 0)
				strTemp = strTemp.Substring(0,index);

			//是否有非法密码：
			int iLength = strTemp.Length;
			for(int i = 0; i < iLength; i++)
			{
				if(!System.Char.IsLetterOrDigit(strTemp, i))
					return null;
			}
			//是否有非法密码。
			int nChars = m_nCharPerUnit *  this.Controls.Count;
			if(strTemp.Length > nChars)
				strTemp = strTemp.Substring(0,nChars);
			if(strTemp.Length != nChars)
				return null;
			return strTemp;
		}

		public string GetStandard(string str,string sSep, int  nCharPerUnit)
		{
			int nChars = m_nCharPerUnit *  this.Controls.Count;
			if(str == null || str.Length < nChars)
				return "";
			str = str.Replace(sSep,"");
			str = str.Insert(nCharPerUnit,sSep);
			str = str.Insert(nCharPerUnit*2+1,sSep);
			str = str.Insert(nCharPerUnit*3+2,sSep);
			return str;
		}

	}
}
