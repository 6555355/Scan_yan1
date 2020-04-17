namespace BYHXPrinterManager.Setting
{
    partial class CalculateRollLengthForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CalculateRollLengthForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonCalculate = new System.Windows.Forms.Button();
            this.labelUnit4 = new System.Windows.Forms.Label();
            this.numCalculateRollLength = new System.Windows.Forms.NumericUpDown();
            this.labelCalculateRollLength = new System.Windows.Forms.Label();
            this.labelUnit3 = new System.Windows.Forms.Label();
            this.numDiameterRoll = new System.Windows.Forms.NumericUpDown();
            this.labelDiameterRoll = new System.Windows.Forms.Label();
            this.labelUnit2 = new System.Windows.Forms.Label();
            this.numDiameterCore = new System.Windows.Forms.NumericUpDown();
            this.labelDiameterCore = new System.Windows.Forms.Label();
            this.labelUnit1 = new System.Windows.Forms.Label();
            this.numMediaThickness = new System.Windows.Forms.NumericUpDown();
            this.labelMediaThickness = new System.Windows.Forms.Label();
            this.buttonApply = new System.Windows.Forms.Button();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCalculateRollLength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiameterRoll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiameterCore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMediaThickness)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.buttonCalculate);
            this.panel1.Controls.Add(this.labelUnit4);
            this.panel1.Controls.Add(this.numCalculateRollLength);
            this.panel1.Controls.Add(this.labelCalculateRollLength);
            this.panel1.Controls.Add(this.labelUnit3);
            this.panel1.Controls.Add(this.numDiameterRoll);
            this.panel1.Controls.Add(this.labelDiameterRoll);
            this.panel1.Controls.Add(this.labelUnit2);
            this.panel1.Controls.Add(this.numDiameterCore);
            this.panel1.Controls.Add(this.labelDiameterCore);
            this.panel1.Controls.Add(this.labelUnit1);
            this.panel1.Controls.Add(this.numMediaThickness);
            this.panel1.Controls.Add(this.labelMediaThickness);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // buttonCalculate
            // 
            resources.ApplyResources(this.buttonCalculate, "buttonCalculate");
            this.buttonCalculate.Name = "buttonCalculate";
            this.buttonCalculate.UseVisualStyleBackColor = true;
            this.buttonCalculate.Click += new System.EventHandler(this.buttonCalculate_Click);
            // 
            // labelUnit4
            // 
            resources.ApplyResources(this.labelUnit4, "labelUnit4");
            this.labelUnit4.Name = "labelUnit4";
            // 
            // numCalculateRollLength
            // 
            this.numCalculateRollLength.DecimalPlaces = 3;
            resources.ApplyResources(this.numCalculateRollLength, "numCalculateRollLength");
            this.numCalculateRollLength.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.numCalculateRollLength.Name = "numCalculateRollLength";
            this.numCalculateRollLength.ReadOnly = true;
            // 
            // labelCalculateRollLength
            // 
            resources.ApplyResources(this.labelCalculateRollLength, "labelCalculateRollLength");
            this.labelCalculateRollLength.Name = "labelCalculateRollLength";
            // 
            // labelUnit3
            // 
            resources.ApplyResources(this.labelUnit3, "labelUnit3");
            this.labelUnit3.Name = "labelUnit3";
            // 
            // numDiameterRoll
            // 
            this.numDiameterRoll.DecimalPlaces = 1;
            resources.ApplyResources(this.numDiameterRoll, "numDiameterRoll");
            this.numDiameterRoll.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.numDiameterRoll.Name = "numDiameterRoll";
            // 
            // labelDiameterRoll
            // 
            resources.ApplyResources(this.labelDiameterRoll, "labelDiameterRoll");
            this.labelDiameterRoll.Name = "labelDiameterRoll";
            // 
            // labelUnit2
            // 
            resources.ApplyResources(this.labelUnit2, "labelUnit2");
            this.labelUnit2.Name = "labelUnit2";
            // 
            // numDiameterCore
            // 
            this.numDiameterCore.DecimalPlaces = 1;
            resources.ApplyResources(this.numDiameterCore, "numDiameterCore");
            this.numDiameterCore.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.numDiameterCore.Name = "numDiameterCore";
            this.numDiameterCore.ValueChanged += new System.EventHandler(this.numDiameterCore_ValueChanged);
            // 
            // labelDiameterCore
            // 
            resources.ApplyResources(this.labelDiameterCore, "labelDiameterCore");
            this.labelDiameterCore.Name = "labelDiameterCore";
            // 
            // labelUnit1
            // 
            resources.ApplyResources(this.labelUnit1, "labelUnit1");
            this.labelUnit1.Name = "labelUnit1";
            // 
            // numMediaThickness
            // 
            this.numMediaThickness.DecimalPlaces = 2;
            resources.ApplyResources(this.numMediaThickness, "numMediaThickness");
            this.numMediaThickness.Maximum = new decimal(new int[] {
            276447232,
            23283,
            0,
            0});
            this.numMediaThickness.Name = "numMediaThickness";
            // 
            // labelMediaThickness
            // 
            resources.ApplyResources(this.labelMediaThickness, "labelMediaThickness");
            this.labelMediaThickness.Name = "labelMediaThickness";
            // 
            // buttonApply
            // 
            this.buttonApply.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.buttonApply, "buttonApply");
            this.buttonApply.Name = "buttonApply";
            this.buttonApply.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            this.buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            // 
            // CalculateRollLengthForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonApply);
            this.Controls.Add(this.panel1);
            this.Name = "CalculateRollLengthForm";
            this.Load += new System.EventHandler(this.CalculateRollLengthForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCalculateRollLength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiameterRoll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDiameterCore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMediaThickness)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonCalculate;
        private System.Windows.Forms.Label labelUnit4;
        private System.Windows.Forms.NumericUpDown numCalculateRollLength;
        private System.Windows.Forms.Label labelCalculateRollLength;
        private System.Windows.Forms.Label labelUnit3;
        private System.Windows.Forms.NumericUpDown numDiameterRoll;
        private System.Windows.Forms.Label labelDiameterRoll;
        private System.Windows.Forms.Label labelUnit2;
        private System.Windows.Forms.NumericUpDown numDiameterCore;
        private System.Windows.Forms.Label labelDiameterCore;
        private System.Windows.Forms.Label labelUnit1;
        private System.Windows.Forms.NumericUpDown numMediaThickness;
        private System.Windows.Forms.Label labelMediaThickness;
        private System.Windows.Forms.Button buttonApply;
        private System.Windows.Forms.Button buttonCancel;
    }
}