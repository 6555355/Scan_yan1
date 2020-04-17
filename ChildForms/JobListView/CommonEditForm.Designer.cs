namespace BYHXPrinterManager.JobListView
{
    partial class CommonEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommonEditForm));
            this.label1 = new System.Windows.Forms.Label();
            this.m_textBoxInput = new System.Windows.Forms.TextBox();
            this.m_buttonOK = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // m_textBoxInput
            // 
            resources.ApplyResources(this.m_textBoxInput, "m_textBoxInput");
            this.m_textBoxInput.Name = "m_textBoxInput";
            // 
            // m_buttonOK
            // 
            resources.ApplyResources(this.m_buttonOK, "m_buttonOK");
            this.m_buttonOK.Name = "m_buttonOK";
            this.m_buttonOK.UseVisualStyleBackColor = true;
            this.m_buttonOK.Click += new System.EventHandler(this.m_buttonOK_Click);
            // 
            // button2
            // 
            resources.ApplyResources(this.button2, "button2");
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Name = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // CommonEditForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button2);
            this.Controls.Add(this.m_buttonOK);
            this.Controls.Add(this.m_textBoxInput);
            this.Controls.Add(this.label1);
            this.Name = "CommonEditForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox m_textBoxInput;
        private System.Windows.Forms.Button m_buttonOK;
        private System.Windows.Forms.Button button2;
    }
}