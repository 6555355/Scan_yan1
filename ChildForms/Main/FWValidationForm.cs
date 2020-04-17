using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Main
{
	public class FWValidationForm : Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FWValidationForm));
			this.labelFWpdw = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.buttonVerify = new System.Windows.Forms.Button();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.labelMSG = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// labelFWpdw
			// 
			this.labelFWpdw.AccessibleDescription = null;
			this.labelFWpdw.AccessibleName = null;
			resources.ApplyResources(this.labelFWpdw, "labelFWpdw");
			this.labelFWpdw.Font = null;
			this.labelFWpdw.Name = "labelFWpdw";
			// 
			// textBox1
			// 
			this.textBox1.AccessibleDescription = null;
			this.textBox1.AccessibleName = null;
			resources.ApplyResources(this.textBox1, "textBox1");
			this.textBox1.BackgroundImage = null;
			this.textBox1.Font = null;
			this.textBox1.Name = "textBox1";
			this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
			// 
			// buttonVerify
			// 
			this.buttonVerify.AccessibleDescription = null;
			this.buttonVerify.AccessibleName = null;
			resources.ApplyResources(this.buttonVerify, "buttonVerify");
			this.buttonVerify.BackgroundImage = null;
			this.buttonVerify.Font = null;
			this.buttonVerify.Name = "buttonVerify";
			this.buttonVerify.Click += new System.EventHandler(this.buttonVerify_Click);
			// 
			// buttonCancel
			// 
			this.buttonCancel.AccessibleDescription = null;
			this.buttonCancel.AccessibleName = null;
			resources.ApplyResources(this.buttonCancel, "buttonCancel");
			this.buttonCancel.BackgroundImage = null;
			this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Font = null;
			this.buttonCancel.Name = "buttonCancel";
			// 
			// labelMSG
			// 
			this.labelMSG.AccessibleDescription = null;
			this.labelMSG.AccessibleName = null;
			resources.ApplyResources(this.labelMSG, "labelMSG");
			this.labelMSG.BackColor = System.Drawing.SystemColors.Info;
			this.labelMSG.Font = null;
			this.labelMSG.Name = "labelMSG";
			// 
			// FWValidationForm
			// 
			this.AcceptButton = this.buttonVerify;
			this.AccessibleDescription = null;
			this.AccessibleName = null;
			resources.ApplyResources(this, "$this");
			this.BackgroundImage = null;
			this.Controls.Add(this.labelMSG);
			this.Controls.Add(this.buttonCancel);
			this.Controls.Add(this.buttonVerify);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.labelFWpdw);
			this.Font = null;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = null;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FWValidationForm";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label labelFWpdw;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button buttonVerify;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Label labelMSG;
		public FWValidationForm()
		{
			InitializeComponent();
		}

		private void buttonVerify_Click(object sender, EventArgs e)
		{
			string idversion = string.Empty;
			SBoardInfo sBoardInfo = new SBoardInfo();
			if (CoreInterface.GetBoardInfo(0, ref sBoardInfo) != 0)
			{
				//idversion = sBoardInfo.m_nBoardSerialNum.ToString();
				idversion = sBoardInfo.m_nBoardManufatureID.ToString("X4") + sBoardInfo.m_nBoardProductID.ToString("X4");
			}
			else
			{
				this.labelMSG.Visible = true;
				this.labelMSG.Image = SystemIcons.Error.ToBitmap();
				this.labelMSG.Text = SErrorCode.GetEnumDisplayName(typeof(COMCommand_Abort), COMCommand_Abort.IllegalPwd);
				return;
			}
			if (idversion.ToUpper() == this.textBox1.Text.Trim().ToUpper())
				this.DialogResult = DialogResult.OK;
			else
			{
				this.labelMSG.Visible = true;
				this.labelMSG.Image = SystemIcons.Error.ToBitmap();
				this.labelMSG.Text = SErrorCode.GetEnumDisplayName(typeof(COMCommand_Abort), COMCommand_Abort.IllegalPwd);
			}
		}

		private void textBox1_TextChanged(object sender, EventArgs e)
		{
			this.labelMSG.Visible = false;
		}
	}
}
