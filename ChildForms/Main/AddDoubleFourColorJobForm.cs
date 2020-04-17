using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// AddDoubleFourColorJobForm 的摘要说明。
	/// </summary>
	public class AddDoubleFourColorJobForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Button buttonCancel;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AddDoubleFourColorJobForm()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddDoubleFourColorJobForm));
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // textBox1
            // 
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox2
            // 
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // buttonAdd
            // 
            resources.ApplyResources(this.buttonAdd, "buttonAdd");
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancel.Name = "buttonCancel";
            // 
            // AddDoubleFourColorJobForm
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Name = "AddDoubleFourColorJobForm";
            this.Load += new System.EventHandler(this.AddDoubleFourColorJobForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


		public string FileName1{get{return this.textBox1.Text;}}
		public string FileName2{get{return this.textBox2.Text;}}

		private void button1_Click(object sender, System.EventArgs e)
		{
			if(sender == this.button1)
			{
				SelecetDouble4ColorFile(1);
			}
			if(sender == this.button2)
			{
				SelecetDouble4ColorFile(2);
			}
		}

		private void AddDoubleFourColorJobForm_Load(object sender, System.EventArgs e)
		{
			//SelecetDouble4ColorFile(1);
		}

		private void SelecetDouble4ColorFile(int index)
		{
			OpenFileDialog fileDialog = new OpenFileDialog();

			fileDialog.Multiselect = true;
			fileDialog.CheckFileExists = true;
			fileDialog.DefaultExt = ".prn";
			fileDialog.Filter = ResString.GetEnumDisplayName(typeof(FileFilter), FileFilter.Prn);

			if(fileDialog.ShowDialog(this) == DialogResult.OK)
			{
				switch(index)
				{
					case 1:
						this.textBox1.Text = fileDialog.FileName;
						break;
					case 2:
						this.textBox2.Text = fileDialog.FileName;
						break;					
				}
			}
		}

		private void buttonAdd_Click(object sender, System.EventArgs e)
		{
			if(FileName1 != null && FileName1!= string.Empty
				&&FileName2 != null && FileName2!= string.Empty
				&&FileName1 != FileName2)
			{
				SPrtFileInfo	jobInfo = new SPrtFileInfo();
				SPrtFileInfo	jobInfo1 = new SPrtFileInfo();
				Int32 bret = 0;
				bret = CoreInterface.Printer_GetFileInfo(FileName1,ref jobInfo,0);
				if(bret == 1)
				{
					bret = CoreInterface.Printer_GetFileInfo(FileName2,ref jobInfo1,0);
					if(bret == 1)
					{ 
						if(
							jobInfo.sImageInfo.nImageResolutionX !=jobInfo1.sImageInfo.nImageResolutionX 
							|| jobInfo.sImageInfo.nImageResolutionY !=jobInfo1.sImageInfo.nImageResolutionY 
							|| jobInfo.sImageInfo.nImageWidth !=jobInfo1.sImageInfo.nImageWidth 
							|| jobInfo.sImageInfo.nImageHeight !=jobInfo1.sImageInfo.nImageHeight 
                            //|| jobInfo.sImageInfo.nImageColorNum !=jobInfo1.sImageInfo.nImageColorNum 
							|| jobInfo.sImageInfo.nImageColorDeep !=jobInfo1.sImageInfo.nImageColorDeep 
							)
						{
							string info = "俩个文件属性不匹配.[分辨率,尺寸,色深]";//SErrorCode.GetEnumDisplayName(typeof(Software),Software.Parser);
							MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Information);
						}
						else
						{
						    this.DialogResult = DialogResult.OK;
						}
					}
				}
				if(bret !=1)
				{
					string info = SErrorCode.GetEnumDisplayName(typeof(Software),Software.Parser);
					info += ":"+ FileName1;
					MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Information);
					return;
				}
			}
			else
			{
				string info = "必须选择俩个文件,而且是不同的.";//SErrorCode.GetEnumDisplayName(typeof(Software),Software.Parser);
				MessageBox.Show(info,ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Information);
			}
		}
	}
}
