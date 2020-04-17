namespace Write501
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.buttonXaar382 = new System.Windows.Forms.Button();
            this.buttonXaar501 = new System.Windows.Forms.Button();
            this.buttonKYOCERA = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonXaar382
            // 
            resources.ApplyResources(this.buttonXaar382, "buttonXaar382");
            this.buttonXaar382.Name = "buttonXaar382";
            this.buttonXaar382.UseVisualStyleBackColor = true;
            this.buttonXaar382.Click += new System.EventHandler(this.buttonXaar382_Click);
            // 
            // buttonXaar501
            // 
            resources.ApplyResources(this.buttonXaar501, "buttonXaar501");
            this.buttonXaar501.Name = "buttonXaar501";
            this.buttonXaar501.UseVisualStyleBackColor = true;
            this.buttonXaar501.Click += new System.EventHandler(this.buttonXaar501_Click);
            // 
            // buttonKYOCERA
            // 
            resources.ApplyResources(this.buttonKYOCERA, "buttonKYOCERA");
            this.buttonKYOCERA.Name = "buttonKYOCERA";
            this.buttonKYOCERA.UseVisualStyleBackColor = true;
            this.buttonKYOCERA.Click += new System.EventHandler(this.buttonKYOCERA_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonKYOCERA);
            this.Controls.Add(this.buttonXaar501);
            this.Controls.Add(this.buttonXaar382);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "MainForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonXaar382;
        private System.Windows.Forms.Button buttonXaar501;
        private System.Windows.Forms.Button buttonKYOCERA;
    }
}