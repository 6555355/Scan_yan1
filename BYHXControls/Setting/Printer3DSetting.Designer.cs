namespace BYHXPrinterManager.Setting
{
    partial class Printer3DSetting
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
            this.components = new System.ComponentModel.Container();
            BYHXPrinterManager.Style style1 = new BYHXPrinterManager.Style();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Printer3DSetting));
            BYHXPrinterManager.Style style2 = new BYHXPrinterManager.Style();
            BYHXPrinterManager.Style style3 = new BYHXPrinterManager.Style();
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.cleanSetting3D1 = new BYHXPrinterManager.Setting.CleanSetting3D();
            this.m_Grouper3DPrint = new BYHXPrinterManager.GradientControls.Grouper();
            this.m_NumericUpDown_ZAdjustment = new System.Windows.Forms.NumericUpDown();
            this.m_NumericUpDown_3DSandSpeed = new System.Windows.Forms.NumericUpDown();
            this.m_Label_3DSandSpeed = new System.Windows.Forms.Label();
            this.m_NumericUpDown_3DFusionTime = new System.Windows.Forms.NumericUpDown();
            this.m_Label_3DFusionTime = new System.Windows.Forms.Label();
            this.m_NumericUpDown_3DSandDis = new System.Windows.Forms.NumericUpDown();
            this.m_Label_3DSandDis = new System.Windows.Forms.Label();
            this.m_NumericUpDown_3DZDownDis = new System.Windows.Forms.NumericUpDown();
            this.m_Label_3DZDownDis = new System.Windows.Forms.Label();
            this.m_Grouper3DPrint.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDown_ZAdjustment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDown_3DSandSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDown_3DFusionTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDown_3DSandDis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDown_3DZDownDis)).BeginInit();
            this.SuspendLayout();
            // 
            // cleanSetting3D1
            // 
            this.cleanSetting3D1.Divider = false;
            style1.Color1 = System.Drawing.SystemColors.Control;
            style1.Color2 = System.Drawing.SystemColors.Control;
            this.cleanSetting3D1.GradientColors = style1;
            this.cleanSetting3D1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.cleanSetting3D1.GrouperTitleStyle = null;
            resources.ApplyResources(this.cleanSetting3D1, "cleanSetting3D1");
            this.cleanSetting3D1.Name = "cleanSetting3D1";
            // 
            // m_Grouper3DPrint
            // 
            this.m_Grouper3DPrint.BackgroundGradientMode = BYHXPrinterManager.GradientControls.GroupBoxGradientMode.Vertical;
            this.m_Grouper3DPrint.BorderThickness = 1F;
            this.m_Grouper3DPrint.Controls.Add(this.m_NumericUpDown_ZAdjustment);
            this.m_Grouper3DPrint.Controls.Add(this.m_NumericUpDown_3DSandSpeed);
            this.m_Grouper3DPrint.Controls.Add(this.m_Label_3DSandSpeed);
            this.m_Grouper3DPrint.Controls.Add(this.m_NumericUpDown_3DFusionTime);
            this.m_Grouper3DPrint.Controls.Add(this.m_Label_3DFusionTime);
            this.m_Grouper3DPrint.Controls.Add(this.m_NumericUpDown_3DSandDis);
            this.m_Grouper3DPrint.Controls.Add(this.m_Label_3DSandDis);
            this.m_Grouper3DPrint.Controls.Add(this.m_NumericUpDown_3DZDownDis);
            this.m_Grouper3DPrint.Controls.Add(this.m_Label_3DZDownDis);
            style2.Color1 = System.Drawing.Color.LightBlue;
            style2.Color2 = System.Drawing.Color.SteelBlue;
            this.m_Grouper3DPrint.GradientColors = style2;
            this.m_Grouper3DPrint.GroupImage = null;
            resources.ApplyResources(this.m_Grouper3DPrint, "m_Grouper3DPrint");
            this.m_Grouper3DPrint.Name = "m_Grouper3DPrint";
            this.m_Grouper3DPrint.PaintGroupBox = false;
            this.m_Grouper3DPrint.RoundCorners = 10;
            this.m_Grouper3DPrint.ShadowColor = System.Drawing.Color.DarkGray;
            this.m_Grouper3DPrint.ShadowControl = false;
            this.m_Grouper3DPrint.ShadowThickness = 3;
            this.m_Grouper3DPrint.TabStop = false;
            style3.Color1 = System.Drawing.Color.LightBlue;
            style3.Color2 = System.Drawing.Color.SteelBlue;
            this.m_Grouper3DPrint.TitileGradientColors = style3;
            this.m_Grouper3DPrint.TitleStyle = BYHXPrinterManager.GradientControls.TitleStyles.XPStyle;
            this.m_Grouper3DPrint.TitleTextColor = System.Drawing.SystemColors.ActiveCaption;
            // 
            // m_NumericUpDown_ZAdjustment
            // 
            this.m_NumericUpDown_ZAdjustment.DecimalPlaces = 1;
            resources.ApplyResources(this.m_NumericUpDown_ZAdjustment, "m_NumericUpDown_ZAdjustment");
            this.m_NumericUpDown_ZAdjustment.Name = "m_NumericUpDown_ZAdjustment";
            // 
            // m_NumericUpDown_3DSandSpeed
            // 
            resources.ApplyResources(this.m_NumericUpDown_3DSandSpeed, "m_NumericUpDown_3DSandSpeed");
            this.m_NumericUpDown_3DSandSpeed.Maximum = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.m_NumericUpDown_3DSandSpeed.Name = "m_NumericUpDown_3DSandSpeed";
            // 
            // m_Label_3DSandSpeed
            // 
            resources.ApplyResources(this.m_Label_3DSandSpeed, "m_Label_3DSandSpeed");
            this.m_Label_3DSandSpeed.BackColor = System.Drawing.Color.Transparent;
            this.m_Label_3DSandSpeed.Name = "m_Label_3DSandSpeed";
            // 
            // m_NumericUpDown_3DFusionTime
            // 
            resources.ApplyResources(this.m_NumericUpDown_3DFusionTime, "m_NumericUpDown_3DFusionTime");
            this.m_NumericUpDown_3DFusionTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.m_NumericUpDown_3DFusionTime.Name = "m_NumericUpDown_3DFusionTime";
            // 
            // m_Label_3DFusionTime
            // 
            resources.ApplyResources(this.m_Label_3DFusionTime, "m_Label_3DFusionTime");
            this.m_Label_3DFusionTime.BackColor = System.Drawing.Color.Transparent;
            this.m_Label_3DFusionTime.Name = "m_Label_3DFusionTime";
            // 
            // m_NumericUpDown_3DSandDis
            // 
            resources.ApplyResources(this.m_NumericUpDown_3DSandDis, "m_NumericUpDown_3DSandDis");
            this.m_NumericUpDown_3DSandDis.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDown_3DSandDis.Name = "m_NumericUpDown_3DSandDis";
            // 
            // m_Label_3DSandDis
            // 
            resources.ApplyResources(this.m_Label_3DSandDis, "m_Label_3DSandDis");
            this.m_Label_3DSandDis.BackColor = System.Drawing.Color.Transparent;
            this.m_Label_3DSandDis.Name = "m_Label_3DSandDis";
            // 
            // m_NumericUpDown_3DZDownDis
            // 
            resources.ApplyResources(this.m_NumericUpDown_3DZDownDis, "m_NumericUpDown_3DZDownDis");
            this.m_NumericUpDown_3DZDownDis.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.m_NumericUpDown_3DZDownDis.Name = "m_NumericUpDown_3DZDownDis";
            // 
            // m_Label_3DZDownDis
            // 
            resources.ApplyResources(this.m_Label_3DZDownDis, "m_Label_3DZDownDis");
            this.m_Label_3DZDownDis.BackColor = System.Drawing.Color.Transparent;
            this.m_Label_3DZDownDis.Name = "m_Label_3DZDownDis";
            // 
            // Printer3DSetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cleanSetting3D1);
            this.Controls.Add(this.m_Grouper3DPrint);
            this.Name = "Printer3DSetting";
            this.m_Grouper3DPrint.ResumeLayout(false);
            this.m_Grouper3DPrint.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDown_ZAdjustment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDown_3DSandSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDown_3DFusionTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDown_3DSandDis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.m_NumericUpDown_3DZDownDis)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private GradientControls.Grouper m_Grouper3DPrint;
        private System.Windows.Forms.NumericUpDown m_NumericUpDown_3DSandSpeed;
        private System.Windows.Forms.Label m_Label_3DSandSpeed;
        private System.Windows.Forms.NumericUpDown m_NumericUpDown_3DFusionTime;
        private System.Windows.Forms.Label m_Label_3DFusionTime;
        private System.Windows.Forms.NumericUpDown m_NumericUpDown_3DSandDis;
        private System.Windows.Forms.Label m_Label_3DSandDis;
        private System.Windows.Forms.NumericUpDown m_NumericUpDown_3DZDownDis;
        private System.Windows.Forms.Label m_Label_3DZDownDis;
        private System.Windows.Forms.ToolTip m_ToolTip;
        private System.Windows.Forms.NumericUpDown m_NumericUpDown_ZAdjustment;
        private CleanSetting3D cleanSetting3D1;
    }
}
