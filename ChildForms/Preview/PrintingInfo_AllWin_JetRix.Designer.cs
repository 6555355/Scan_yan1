using System.Drawing;

namespace BYHXPrinterManager.Preview
{
    partial class PrintingInfo_AllWin_JetRix
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintingInfo_AllWin_JetRix));
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.m_LabelPrintingJobPath = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.m_LabelPrintingJobInfo = new System.Windows.Forms.Label();
            this.m_PrintPreview = new BYHXPrinterManager.Preview.PrintingPreview();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 304);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(563, 100);
            this.panel1.TabIndex = 0;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.panel4.Controls.Add(this.m_LabelPrintingJobPath);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(187, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(155, 100);
            this.panel4.TabIndex = 2;
            // 
            // m_LabelPrintingJobPath
            // 
            this.m_LabelPrintingJobPath.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelPrintingJobPath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_LabelPrintingJobPath.Location = new System.Drawing.Point(0, 0);
            this.m_LabelPrintingJobPath.Name = "m_LabelPrintingJobPath";
            this.m_LabelPrintingJobPath.Size = new System.Drawing.Size(155, 100);
            this.m_LabelPrintingJobPath.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(342, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(221, 100);
            this.panel3.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(2, 57);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(217, 41);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(221)))), ((int)(((byte)(221)))));
            this.panel2.Controls.Add(this.m_LabelPrintingJobInfo);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(187, 100);
            this.panel2.TabIndex = 0;
            // 
            // m_LabelPrintingJobInfo
            // 
            this.m_LabelPrintingJobInfo.BackColor = System.Drawing.Color.Transparent;
            this.m_LabelPrintingJobInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_LabelPrintingJobInfo.Location = new System.Drawing.Point(0, 0);
            this.m_LabelPrintingJobInfo.Name = "m_LabelPrintingJobInfo";
            this.m_LabelPrintingJobInfo.Size = new System.Drawing.Size(187, 100);
            this.m_LabelPrintingJobInfo.TabIndex = 0;
            // 
            // m_PrintPreview
            // 
            this.m_PrintPreview.BackColor = System.Drawing.Color.White;
            this.m_PrintPreview.Divider = false;
            this.m_PrintPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.m_PrintPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            style1.Color1 = Color.White;
            style1.Color2 = Color.White;
            this.m_PrintPreview.GradientColors = style1;
            this.m_PrintPreview.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.m_PrintPreview.Location = new System.Drawing.Point(0, 0);
            this.m_PrintPreview.Name = "m_PrintPreview";
            this.m_PrintPreview.Rotate = System.Drawing.RotateFlipType.RotateNoneFlipNone;
            this.m_PrintPreview.Size = new System.Drawing.Size(563, 304);
            this.m_PrintPreview.TabIndex = 1;
            // 
            // PrintingInfo_AllWin_JetRix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.m_PrintPreview);
            this.Controls.Add(this.panel1);
            this.Name = "PrintingInfo_AllWin_JetRix";
            this.Size = new System.Drawing.Size(563, 404);
            this.panel1.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private PrintingPreview m_PrintPreview;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label m_LabelPrintingJobPath;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label m_LabelPrintingJobInfo;



    }
}
