namespace BYHXPrinterManager.Main
{
    partial class DoubleSidePrintForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DoubleSidePrintForm));
            this.RotateSection = new System.Windows.Forms.GroupBox();
            this.graphicsPanel21 = new BYHXPrinterManager.Setting.GraphicsPanel2();
            this.numRightUp = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numLeftUp = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.xyShiftSection = new System.Windows.Forms.GroupBox();
            this.graphicsPanel1 = new BYHXPrinterManager.Setting.GraphicsPanel();
            this.buttondebug = new System.Windows.Forms.Button();
            this.numFb = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numLr = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numAdjustStep = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.numCursorPosY = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numCursorPosX = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.RotateSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRightUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLeftUp)).BeginInit();
            this.xyShiftSection.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFb)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustStep)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCursorPosY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCursorPosX)).BeginInit();
            this.SuspendLayout();
            // 
            // RotateSection
            // 
            resources.ApplyResources(this.RotateSection, "RotateSection");
            this.RotateSection.Controls.Add(this.graphicsPanel21);
            this.RotateSection.Controls.Add(this.numRightUp);
            this.RotateSection.Controls.Add(this.label2);
            this.RotateSection.Controls.Add(this.numLeftUp);
            this.RotateSection.Controls.Add(this.label1);
            this.RotateSection.Name = "RotateSection";
            this.RotateSection.TabStop = false;
            this.toolTip1.SetToolTip(this.RotateSection, resources.GetString("RotateSection.ToolTip"));
            // 
            // graphicsPanel21
            // 
            resources.ApplyResources(this.graphicsPanel21, "graphicsPanel21");
            this.graphicsPanel21.Name = "graphicsPanel21";
            this.toolTip1.SetToolTip(this.graphicsPanel21, resources.GetString("graphicsPanel21.ToolTip"));
            // 
            // numRightUp
            // 
            resources.ApplyResources(this.numRightUp, "numRightUp");
            this.numRightUp.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numRightUp.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numRightUp.Name = "numRightUp";
            this.toolTip1.SetToolTip(this.numRightUp, resources.GetString("numRightUp.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // numLeftUp
            // 
            resources.ApplyResources(this.numLeftUp, "numLeftUp");
            this.numLeftUp.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numLeftUp.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numLeftUp.Name = "numLeftUp";
            this.toolTip1.SetToolTip(this.numLeftUp, resources.GetString("numLeftUp.ToolTip"));
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            this.toolTip1.SetToolTip(this.label1, resources.GetString("label1.ToolTip"));
            // 
            // xyShiftSection
            // 
            resources.ApplyResources(this.xyShiftSection, "xyShiftSection");
            this.xyShiftSection.Controls.Add(this.graphicsPanel1);
            this.xyShiftSection.Controls.Add(this.buttondebug);
            this.xyShiftSection.Controls.Add(this.numFb);
            this.xyShiftSection.Controls.Add(this.label5);
            this.xyShiftSection.Controls.Add(this.numLr);
            this.xyShiftSection.Controls.Add(this.label6);
            this.xyShiftSection.Name = "xyShiftSection";
            this.xyShiftSection.TabStop = false;
            this.toolTip1.SetToolTip(this.xyShiftSection, resources.GetString("xyShiftSection.ToolTip"));
            // 
            // graphicsPanel1
            // 
            resources.ApplyResources(this.graphicsPanel1, "graphicsPanel1");
            this.graphicsPanel1.Name = "graphicsPanel1";
            this.toolTip1.SetToolTip(this.graphicsPanel1, resources.GetString("graphicsPanel1.ToolTip"));
            // 
            // buttondebug
            // 
            resources.ApplyResources(this.buttondebug, "buttondebug");
            this.buttondebug.Name = "buttondebug";
            this.toolTip1.SetToolTip(this.buttondebug, resources.GetString("buttondebug.ToolTip"));
            this.buttondebug.UseVisualStyleBackColor = true;
            this.buttondebug.Click += new System.EventHandler(this.buttondebug_Click);
            // 
            // numFb
            // 
            resources.ApplyResources(this.numFb, "numFb");
            this.numFb.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numFb.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numFb.Name = "numFb";
            this.toolTip1.SetToolTip(this.numFb, resources.GetString("numFb.ToolTip"));
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            this.toolTip1.SetToolTip(this.label5, resources.GetString("label5.ToolTip"));
            // 
            // numLr
            // 
            resources.ApplyResources(this.numLr, "numLr");
            this.numLr.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numLr.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numLr.Name = "numLr";
            this.toolTip1.SetToolTip(this.numLr, resources.GetString("numLr.ToolTip"));
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            this.toolTip1.SetToolTip(this.label6, resources.GetString("label6.ToolTip"));
            // 
            // numAdjustStep
            // 
            resources.ApplyResources(this.numAdjustStep, "numAdjustStep");
            this.numAdjustStep.DecimalPlaces = 2;
            this.numAdjustStep.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numAdjustStep.Name = "numAdjustStep";
            this.toolTip1.SetToolTip(this.numAdjustStep, resources.GetString("numAdjustStep.ToolTip"));
            this.numAdjustStep.Value = new decimal(new int[] {
            5,
            0,
            0,
            131072});
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            this.toolTip1.SetToolTip(this.label7, resources.GetString("label7.ToolTip"));
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.btnApply);
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // btnApply
            // 
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.Name = "btnApply";
            this.toolTip1.SetToolTip(this.btnApply, resources.GetString("btnApply.ToolTip"));
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.toolTip1.SetToolTip(this.btnClose, resources.GetString("btnClose.ToolTip"));
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.numCursorPosY);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.numCursorPosX);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.numAdjustStep);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Name = "panel2";
            this.toolTip1.SetToolTip(this.panel2, resources.GetString("panel2.ToolTip"));
            // 
            // numCursorPosY
            // 
            resources.ApplyResources(this.numCursorPosY, "numCursorPosY");
            this.numCursorPosY.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numCursorPosY.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numCursorPosY.Name = "numCursorPosY";
            this.toolTip1.SetToolTip(this.numCursorPosY, resources.GetString("numCursorPosY.ToolTip"));
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            this.toolTip1.SetToolTip(this.label3, resources.GetString("label3.ToolTip"));
            // 
            // numCursorPosX
            // 
            resources.ApplyResources(this.numCursorPosX, "numCursorPosX");
            this.numCursorPosX.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numCursorPosX.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numCursorPosX.Name = "numCursorPosX";
            this.toolTip1.SetToolTip(this.numCursorPosX, resources.GetString("numCursorPosX.ToolTip"));
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            this.toolTip1.SetToolTip(this.label4, resources.GetString("label4.ToolTip"));
            // 
            // DoubleSidePrintForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.xyShiftSection);
            this.Controls.Add(this.RotateSection);
            this.Controls.Add(this.panel2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DoubleSidePrintForm";
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.Load += new System.EventHandler(this.DoubleSidePrintForm_Load);
            this.RotateSection.ResumeLayout(false);
            this.RotateSection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRightUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLeftUp)).EndInit();
            this.xyShiftSection.ResumeLayout(false);
            this.xyShiftSection.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numFb)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numAdjustStep)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCursorPosY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCursorPosX)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox RotateSection;
        private System.Windows.Forms.GroupBox xyShiftSection;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numLeftUp;
        private System.Windows.Forms.NumericUpDown numRightUp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numAdjustStep;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numFb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numLr;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button buttondebug;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel2;
        private Setting.GraphicsPanel2 graphicsPanel21;
        private Setting.GraphicsPanel graphicsPanel1;
        private System.Windows.Forms.NumericUpDown numCursorPosY;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numCursorPosX;
        private System.Windows.Forms.Label label4;
    }
}