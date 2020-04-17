using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// Summary description for ManualTakeMediaForm.
	/// </summary>
	public class ManualTakeMediaForm : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.RadioButton cbkrearInverterToCW;
		private System.Windows.Forms.RadioButton cbkrearInverterToCCW;
		private System.Windows.Forms.CheckBox cbkfrontInverterToCW;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
        private ImageList imageList2;
		private System.ComponentModel.IContainer components;

		public ManualTakeMediaForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManualTakeMediaForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbkrearInverterToCCW = new System.Windows.Forms.RadioButton();
            this.imageList2 = new System.Windows.Forms.ImageList(this.components);
            this.cbkrearInverterToCW = new System.Windows.Forms.RadioButton();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbkfrontInverterToCW = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbkrearInverterToCCW);
            this.groupBox1.Controls.Add(this.cbkrearInverterToCW);
            this.groupBox1.Location = new System.Drawing.Point(12, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(161, 173);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Rear inverter";
            // 
            // cbkrearInverterToCCW
            // 
            this.cbkrearInverterToCCW.ImageIndex = 1;
            this.cbkrearInverterToCCW.ImageList = this.imageList2;
            this.cbkrearInverterToCCW.Location = new System.Drawing.Point(19, 95);
            this.cbkrearInverterToCCW.Name = "cbkrearInverterToCCW";
            this.cbkrearInverterToCCW.Size = new System.Drawing.Size(125, 57);
            this.cbkrearInverterToCCW.TabIndex = 1;
            this.cbkrearInverterToCCW.Text = "CCW";
            // 
            // imageList2
            // 
            this.imageList2.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList2.ImageStream")));
            this.imageList2.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList2.Images.SetKeyName(0, "À≥ ±’Î.png");
            this.imageList2.Images.SetKeyName(1, "ƒÊ ±’Î.png");
            // 
            // cbkrearInverterToCW
            // 
            this.cbkrearInverterToCW.Checked = true;
            this.cbkrearInverterToCW.ImageIndex = 0;
            this.cbkrearInverterToCW.ImageList = this.imageList2;
            this.cbkrearInverterToCW.Location = new System.Drawing.Point(19, 26);
            this.cbkrearInverterToCW.Name = "cbkrearInverterToCW";
            this.cbkrearInverterToCW.Size = new System.Drawing.Size(125, 57);
            this.cbkrearInverterToCW.TabIndex = 0;
            this.cbkrearInverterToCW.TabStop = true;
            this.cbkrearInverterToCW.Text = "CW";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "");
            this.imageList1.Images.SetKeyName(1, "");
            this.imageList1.Images.SetKeyName(2, "");
            this.imageList1.Images.SetKeyName(3, "22.png");
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbkfrontInverterToCW);
            this.groupBox2.Location = new System.Drawing.Point(192, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(163, 173);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Front inverter";
            // 
            // cbkfrontInverterToCW
            // 
            this.cbkfrontInverterToCW.Checked = true;
            this.cbkfrontInverterToCW.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbkfrontInverterToCW.ImageIndex = 3;
            this.cbkfrontInverterToCW.ImageList = this.imageList1;
            this.cbkfrontInverterToCW.Location = new System.Drawing.Point(19, 60);
            this.cbkfrontInverterToCW.Name = "cbkfrontInverterToCW";
            this.cbkfrontInverterToCW.Size = new System.Drawing.Size(125, 57);
            this.cbkfrontInverterToCW.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button1.Location = new System.Drawing.Point(250, 198);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 25);
            this.button1.TabIndex = 2;
            this.button1.Text = "&Cancel";
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button2.Location = new System.Drawing.Point(125, 198);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 25);
            this.button2.TabIndex = 3;
            this.button2.Text = "&Apply";
            // 
            // ManualTakeMediaForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(381, 234);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "ManualTakeMediaForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Media Setting";
            this.Load += new System.EventHandler(this.ManualTakeMediaForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		public byte Rear 
		{
			get 
			{
				if(this.cbkrearInverterToCW.Checked)
					return 1;
				else 
					return 0;
			}
            set
            {
                this.cbkrearInverterToCW.Checked = value == 1;
                this.cbkrearInverterToCCW.Checked = value == 0;
            }
        }

		public byte Front 
		{
			get 
			{
                if (this.cbkfrontInverterToCW.Checked)
					return 1;
			    return 0;
			}
		    set
		    {
		        this.cbkfrontInverterToCW.Checked = value == 1;
		    }
		}

        private void ManualTakeMediaForm_Load(object sender, EventArgs e)
        {
            byte[] data = new byte[30];
            uint bufsize = (uint)data.Length;
           int ret =  CoreInterface.GetEpsonEP0Cmd(0x92, data, ref bufsize, 0, 0x53);
            if (ret != 0)
            {
                this.Rear = data[3];
                this.Front = data[4];
            }
        }
	}
}
