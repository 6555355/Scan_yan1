namespace BYHXPrinterManager.Setting
{
    partial class CloseColorNozzle_New
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CloseColorNozzle_New));
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnOverLapCheck = new System.Windows.Forms.Button();
            this.comboBox_model = new System.Windows.Forms.ComboBox();
            this.label_Model = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            resources.ApplyResources(this.flowLayoutPanel1, "flowLayoutPanel1");
            this.flowLayoutPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnOverLapCheck
            // 
            resources.ApplyResources(this.btnOverLapCheck, "btnOverLapCheck");
            this.btnOverLapCheck.Name = "btnOverLapCheck";
            this.btnOverLapCheck.UseVisualStyleBackColor = true;
            this.btnOverLapCheck.Click += new System.EventHandler(this.btnOverLapCheck_Click);
            // 
            // comboBox_model
            // 
            resources.ApplyResources(this.comboBox_model, "comboBox_model");
            this.comboBox_model.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_model.FormattingEnabled = true;
            this.comboBox_model.Name = "comboBox_model";
            this.comboBox_model.SelectedIndexChanged += new System.EventHandler(this.comboBox_model_SelectedIndexChanged);
            // 
            // label_Model
            // 
            resources.ApplyResources(this.label_Model, "label_Model");
            this.label_Model.Name = "label_Model";
            // 
            // CloseColorNozzle_New
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label_Model);
            this.Controls.Add(this.comboBox_model);
            this.Controls.Add(this.btnOverLapCheck);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "CloseColorNozzle_New";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnOverLapCheck;
        private System.Windows.Forms.ComboBox comboBox_model;
        private System.Windows.Forms.Label label_Model;
    }
}
