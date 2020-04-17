using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace PathChooser
{
    public class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;

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
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button1 = new System.Windows.Forms.Button();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(24, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(42, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "label1";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(12, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(428, 78);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "groupBox1";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(328, 24);
			this.button1.Name = "button1";
			this.button1.TabIndex = 2;
			this.button1.Text = "button1";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(96, 24);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(208, 21);
			this.textBox1.TabIndex = 1;
			this.textBox1.Text = "";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.panel1);
			this.groupBox2.Location = new System.Drawing.Point(12, 97);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(428, 327);
			this.groupBox2.TabIndex = 2;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "groupBox2";
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(3, 17);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(422, 307);
			this.panel1.TabIndex = 0;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(456, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(208, 416);
			this.label2.TabIndex = 3;
			this.label2.Text = "label2";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(672, 446);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Panel panel1;

		private byte[] realRet;
		public byte[] Passes
		{
			get{return realRet;}
		}

        private bool creatCheckBoxs(int count)
        {
			int margin = 0;
            if (count <= 0)
                return false;
            this.panel1.Controls.Clear();
            int ColCount = 0;
            string Maxtext = count.ToString();
            CheckBox CBsampale = new CheckBox();
            CBsampale.Text = Maxtext;

            ColCount = (int)(this.panel1.Width / (CBsampale.Width+ margin));
            int i = 0;
            int rowindex = 0;
            while (i < count)
            {
                for (int j = 0; j < ColCount && i < count; j++)
                {
                    CheckBox CB1 = new CheckBox();
                    CB1.Text = (i + 1).ToString();
                    CB1.CheckedChanged += new EventHandler(CB1_CheckedChanged);
                    CB1.Location = new Point(j * (CB1.Width + margin),rowindex * (CB1.Height + margin)); 
                    this.panel1.Controls.Add(CB1);
                    i++;
                }
                rowindex++;
            }
            return true;
        }

        void CB1_CheckedChanged(object sender, EventArgs e)
        {
			string text = string.Empty;
			int count = int.Parse(this.textBox1.Text);
            byte[] ret = new byte[count];
			int i = 0;
			for (int j = 0; j < count; j++)
			{
				ret[j] = ((CheckBox)this.panel1.Controls[j]).Checked ? (byte)1 :(byte)0;
			}

			byte[] hex = new byte[8];
			realRet = new byte[count /8];

			foreach(byte bt in ret)
			{
				i++;
				text += bt.ToString("X2") + " ";
				hex[(i-1)%8] = bt;
				if(i%8 == 0)
				{
					byte ihex = 0;
					string hexStr = string.Empty;
					for(int m = 0;m < 8; m++)
					{
						ihex += (byte)(hex[m]* Math.Pow(2,m%8));
					}
					realRet[i/8 -1]= ihex;
					text +=  "  " + ihex.ToString("X2").ToUpper() + "\n";
				}
			}

			this.label2.Text = text;
			this.label2.Invalidate();
        }

		private void button1_Click(object sender, System.EventArgs e)
		{
			int passNum = 0;
			try
			{
				passNum = int.Parse(this.textBox1.Text);
				this.creatCheckBoxs(passNum);
				this.CB1_CheckedChanged(null,new EventArgs());
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

    }
}
