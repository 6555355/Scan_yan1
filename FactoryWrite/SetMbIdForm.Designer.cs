namespace FactoryWrite
{
    partial class SetMbIdForm
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
            this.mbIdSetting1 = new BYHXPrinterManager.Setting.MbIdSetting();
            this.buttonOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // mbIdSetting1
            // 
            this.mbIdSetting1.Dock = System.Windows.Forms.DockStyle.Top;
            this.mbIdSetting1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.mbIdSetting1.GrouperTitleStyle = null;
            this.mbIdSetting1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.mbIdSetting1.Location = new System.Drawing.Point(0, 0);
            this.mbIdSetting1.Name = "mbIdSetting1";
            this.mbIdSetting1.Size = new System.Drawing.Size(373, 274);
            this.mbIdSetting1.TabIndex = 0;
            // 
            // buttonOk
            // 
            this.buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOk.Location = new System.Drawing.Point(266, 280);
            this.buttonOk.Name = "buttonOk";
            this.buttonOk.Size = new System.Drawing.Size(75, 23);
            this.buttonOk.TabIndex = 1;
            this.buttonOk.Text = "OK";
            this.buttonOk.UseVisualStyleBackColor = true;
            // 
            // SetMbIdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 312);
            this.Controls.Add(this.buttonOk);
            this.Controls.Add(this.mbIdSetting1);
            this.Name = "SetMbIdForm";
            this.Text = "SetMbIdForm";
            this.Load += new System.EventHandler(this.SetMbIdForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private BYHXPrinterManager.Setting.MbIdSetting mbIdSetting1;
        private System.Windows.Forms.Button buttonOk;
    }
}