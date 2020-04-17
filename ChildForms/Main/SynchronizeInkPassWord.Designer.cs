namespace BYHXPrinterManager.Main
{
    partial class SynchronizeInkPassWord
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SynchronizeInkPassWord));
            this.labelPassWord = new System.Windows.Forms.Label();
            this.textBoxPassWord = new System.Windows.Forms.TextBox();
            this.buttonConfirm = new System.Windows.Forms.Button();
            this.labelTips = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelPassWord
            // 
            resources.ApplyResources(this.labelPassWord, "labelPassWord");
            this.labelPassWord.Name = "labelPassWord";
            // 
            // textBoxPassWord
            // 
            resources.ApplyResources(this.textBoxPassWord, "textBoxPassWord");
            this.textBoxPassWord.Name = "textBoxPassWord";
            // 
            // buttonConfirm
            // 
            resources.ApplyResources(this.buttonConfirm, "buttonConfirm");
            this.buttonConfirm.Name = "buttonConfirm";
            this.buttonConfirm.UseVisualStyleBackColor = true;
            this.buttonConfirm.Click += new System.EventHandler(this.buttonConfirm_Click);
            // 
            // labelTips
            // 
            resources.ApplyResources(this.labelTips, "labelTips");
            this.labelTips.ForeColor = System.Drawing.Color.Red;
            this.labelTips.Name = "labelTips";
            // 
            // SynchronizeInkPassWord
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelTips);
            this.Controls.Add(this.buttonConfirm);
            this.Controls.Add(this.textBoxPassWord);
            this.Controls.Add(this.labelPassWord);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SynchronizeInkPassWord";
            this.ShowIcon = false;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelPassWord;
        private System.Windows.Forms.TextBox textBoxPassWord;
        private System.Windows.Forms.Button buttonConfirm;
        private System.Windows.Forms.Label labelTips;
    }
}