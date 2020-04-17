using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BYHXPrinterManager.Main
{
    public class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            this.mXmlReader.ReadStatusChanged += new MyXmlReader.OnReadStatusChanged(mXmlReader_ReadStatusChanged);
        }

        private MyXmlReader mXmlReader = new MyXmlReader();
        private const int MAP_1_MARK = 0xDDDD;
        private const int MAP_2_MARK = 0xEEEE;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private MainMenu menuStrip1;
        private RichTextBox richTextBox1;
        private Label label1;
        private TextBox textBox1;
        private ProgressBar progressBar1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem menuItem2;
		private System.Windows.Forms.MenuItem menuItem3;
        private const int MAP_HEADER_SIZE = 10;

        private void InitializeComponent()
        {
			this.button1 = new System.Windows.Forms.Button();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
			this.menuStrip1 = new System.Windows.Forms.MainMenu();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.menuItem2 = new System.Windows.Forms.MenuItem();
			this.menuItem3 = new System.Windows.Forms.MenuItem();
			this.SuspendLayout();
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(317, 71);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(65, 27);
			this.button1.TabIndex = 0;
			this.button1.Text = "Open";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.menuItem1});
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.richTextBox1.Location = new System.Drawing.Point(0, 128);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(394, 115);
			this.richTextBox1.TabIndex = 4;
			this.richTextBox1.Text = "";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 47);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(66, 17);
			this.label1.TabIndex = 5;
			this.label1.Text = "File Path:";
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(81, 44);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(301, 21);
			this.textBox1.TabIndex = 6;
			this.textBox1.Text = "";
			// 
			// progressBar1
			// 
			this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.progressBar1.Location = new System.Drawing.Point(0, 243);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(394, 23);
			this.progressBar1.TabIndex = 7;
			// 
			// menuItem1
			// 
			this.menuItem1.Index = 0;
			this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					  this.menuItem2,
																					  this.menuItem3});
			this.menuItem1.Text = "&File";
			// 
			// menuItem2
			// 
			this.menuItem2.Index = 0;
			this.menuItem2.Text = "&Open";
			this.menuItem2.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// menuItem3
			// 
			this.menuItem3.Index = 1;
			this.menuItem3.Text = "&Exit";
			this.menuItem3.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// Form2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(394, 266);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.richTextBox1);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.progressBar1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "Form2";
			this.ResumeLayout(false);

		}


        private const string strRead_start = "开始写入内存";
        private const string strRead_end = "写入完成";
        private const string strRead_Commond = "正在写入第{0}个类型为{1}的AREA...";

        unsafe private void button1_Click(object sender, EventArgs e)
        {
            bool sucess = this.OpenFile();
            if (sucess == true)
            {
                MessageBox.Show("Write Headboard has Sucessed!");
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bool sucess = this.OpenFile();
            if (sucess == true)
            {
                MessageBox.Show("Write Headboard has Sucessed!");
            }
        }

        void mXmlReader_ReadStatusChanged(object sender, string message)
        {
            UpdateText(message);
        }

        unsafe private bool OpenFile()
        {
            this.openFileDialog1.FileName = Application.StartupPath;
            this.openFileDialog1.Filter = "Job Files (*.xml)|*.xml|All Files (*.*)|*.*";
            if (this.openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = this.openFileDialog1.FileName;
                this.textBox1.Update();
                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    string mFileName = this.openFileDialog1.FileName + ".txt";
                    mXmlReader.ReadXml(this.openFileDialog1.FileName);
                    int i = 0;
                    UpdateText(strRead_start);
                    foreach (AREA area in mXmlReader.AREAsFromXml1)
                    {
                        i++;
                        byte[] areaDatas = mXmlReader.GetAREABuffer(area);
                        UpdateText(string.Format(strRead_Commond, i.ToString(), area.type.ToString()));
                        CoreInterface.WriteHBEEprom(areaDatas, area.AreaHeader.Size, area.ADDR);
                        this.progressBar1.Value = i * 100 / mXmlReader.AREAsFromXml1.Count;
                    }
                    this.progressBar1.Value = 0;
                    UpdateText(strRead_end);

                    FileStream fileStream = new FileStream(mFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite); ;
                    BinaryWriter bw = new BinaryWriter(fileStream);

                    foreach (AREA area in mXmlReader.AREAsFromXml1)
                    {
                        byte[] areaDatas = mXmlReader.GetAREABuffer(area);
                        bw.Seek(area.ADDR, 0);
                        bw.Write(areaDatas);
                    }

                    bw.Flush();
                    bw.Close();
                    this.Cursor = Cursors.Default;
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    this.Cursor = Cursors.Default;
                    return false;
                }
            }
            return false;
        }

        private void UpdateText(string mes)
        {
            this.richTextBox1.AppendText(mes + "\n");
            this.richTextBox1.Update();
            this.richTextBox1.ScrollToCaret();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
