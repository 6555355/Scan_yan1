// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MICOLOR_CleanSection.cs" company="">
//   
// </copyright>
// <summary>
//   The micolo r_ clean section.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace EpsonControlLibrary
{
    using System;
    using System.Windows.Forms;
	using BYHXPrinterManager;
    /// <summary>
    /// The micolo r_ clean section.
    /// </summary>
    public class MICOLOR_CleanSection : UserControl
    {
        #region Constants and Fields

        /// <summary>
        /// The mmicolor_cleansection.
        /// </summary>
        private CLEANSECTION_EPSON_MICOLOR mmicolor_cleansection;

        #endregion

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MICOLOR_CleanSection));
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDownMoveSpeed = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDownSuckSpeed = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numericUpDownOption = new System.Windows.Forms.NumericUpDown();
            this.comboBoxtype = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.numericUpDownflashCycle = new System.Windows.Forms.NumericUpDown();
            this.label22 = new System.Windows.Forms.Label();
            this.numericUpDownFlashTime = new System.Windows.Forms.NumericUpDown();
            this.label23 = new System.Windows.Forms.Label();
            this.numericUpDownFlashIdleInCycle = new System.Windows.Forms.NumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.numericUpDownsuckInkTime = new System.Windows.Forms.NumericUpDown();
            this.label25 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.numericUpDownReleaseTime = new System.Windows.Forms.NumericUpDown();
            this.label27 = new System.Windows.Forms.Label();
            this.numericUpDownStayTime = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownLoopTimes = new System.Windows.Forms.NumericUpDown();
            this.label28 = new System.Windows.Forms.Label();
            this.label29 = new System.Windows.Forms.Label();
            this.numericUpDownFlashFreqInterval = new System.Windows.Forms.NumericUpDown();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMoveSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSuckSpeed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownflashCycle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFlashTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFlashIdleInCycle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownsuckInkTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownReleaseTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStayTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLoopTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFlashFreqInterval)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox5
            // 
            this.groupBox5.AccessibleDescription = null;
            this.groupBox5.AccessibleName = null;
            resources.ApplyResources(this.groupBox5, "groupBox5");
            this.groupBox5.BackgroundImage = null;
            this.groupBox5.Controls.Add(this.label1);
            this.groupBox5.Controls.Add(this.numericUpDownMoveSpeed);
            this.groupBox5.Controls.Add(this.label2);
            this.groupBox5.Controls.Add(this.numericUpDownSuckSpeed);
            this.groupBox5.Controls.Add(this.label3);
            this.groupBox5.Controls.Add(this.numericUpDownOption);
            this.groupBox5.Controls.Add(this.comboBoxtype);
            this.groupBox5.Controls.Add(this.label21);
            this.groupBox5.Controls.Add(this.numericUpDownflashCycle);
            this.groupBox5.Controls.Add(this.label22);
            this.groupBox5.Controls.Add(this.numericUpDownFlashTime);
            this.groupBox5.Controls.Add(this.label23);
            this.groupBox5.Controls.Add(this.numericUpDownFlashIdleInCycle);
            this.groupBox5.Controls.Add(this.label24);
            this.groupBox5.Controls.Add(this.numericUpDownsuckInkTime);
            this.groupBox5.Controls.Add(this.label25);
            this.groupBox5.Controls.Add(this.label26);
            this.groupBox5.Controls.Add(this.numericUpDownReleaseTime);
            this.groupBox5.Controls.Add(this.label27);
            this.groupBox5.Controls.Add(this.numericUpDownStayTime);
            this.groupBox5.Controls.Add(this.numericUpDownLoopTimes);
            this.groupBox5.Controls.Add(this.label28);
            this.groupBox5.Controls.Add(this.label29);
            this.groupBox5.Controls.Add(this.numericUpDownFlashFreqInterval);
            this.groupBox5.Font = null;
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.TabStop = false;
            // 
            // label1
            // 
            this.label1.AccessibleDescription = null;
            this.label1.AccessibleName = null;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Font = null;
            this.label1.Name = "label1";
            // 
            // numericUpDownMoveSpeed
            // 
            this.numericUpDownMoveSpeed.AccessibleDescription = null;
            this.numericUpDownMoveSpeed.AccessibleName = null;
            resources.ApplyResources(this.numericUpDownMoveSpeed, "numericUpDownMoveSpeed");
            this.numericUpDownMoveSpeed.Font = null;
            this.numericUpDownMoveSpeed.Name = "numericUpDownMoveSpeed";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Font = null;
            this.label2.Name = "label2";
            // 
            // numericUpDownSuckSpeed
            // 
            this.numericUpDownSuckSpeed.AccessibleDescription = null;
            this.numericUpDownSuckSpeed.AccessibleName = null;
            resources.ApplyResources(this.numericUpDownSuckSpeed, "numericUpDownSuckSpeed");
            this.numericUpDownSuckSpeed.Font = null;
            this.numericUpDownSuckSpeed.Name = "numericUpDownSuckSpeed";
            // 
            // label3
            // 
            this.label3.AccessibleDescription = null;
            this.label3.AccessibleName = null;
            resources.ApplyResources(this.label3, "label3");
            this.label3.Font = null;
            this.label3.Name = "label3";
            // 
            // numericUpDownOption
            // 
            this.numericUpDownOption.AccessibleDescription = null;
            this.numericUpDownOption.AccessibleName = null;
            resources.ApplyResources(this.numericUpDownOption, "numericUpDownOption");
            this.numericUpDownOption.Font = null;
            this.numericUpDownOption.Name = "numericUpDownOption";
            // 
            // comboBoxtype
            // 
            this.comboBoxtype.AccessibleDescription = null;
            this.comboBoxtype.AccessibleName = null;
            resources.ApplyResources(this.comboBoxtype, "comboBoxtype");
            this.comboBoxtype.BackgroundImage = null;
            this.comboBoxtype.Font = null;
            this.comboBoxtype.Name = "comboBoxtype";
            // 
            // label21
            // 
            this.label21.AccessibleDescription = null;
            this.label21.AccessibleName = null;
            resources.ApplyResources(this.label21, "label21");
            this.label21.Font = null;
            this.label21.Name = "label21";
            // 
            // numericUpDownflashCycle
            // 
            this.numericUpDownflashCycle.AccessibleDescription = null;
            this.numericUpDownflashCycle.AccessibleName = null;
            resources.ApplyResources(this.numericUpDownflashCycle, "numericUpDownflashCycle");
            this.numericUpDownflashCycle.Font = null;
            this.numericUpDownflashCycle.Name = "numericUpDownflashCycle";
            // 
            // label22
            // 
            this.label22.AccessibleDescription = null;
            this.label22.AccessibleName = null;
            resources.ApplyResources(this.label22, "label22");
            this.label22.Font = null;
            this.label22.Name = "label22";
            // 
            // numericUpDownFlashTime
            // 
            this.numericUpDownFlashTime.AccessibleDescription = null;
            this.numericUpDownFlashTime.AccessibleName = null;
            resources.ApplyResources(this.numericUpDownFlashTime, "numericUpDownFlashTime");
            this.numericUpDownFlashTime.Font = null;
            this.numericUpDownFlashTime.Name = "numericUpDownFlashTime";
            // 
            // label23
            // 
            this.label23.AccessibleDescription = null;
            this.label23.AccessibleName = null;
            resources.ApplyResources(this.label23, "label23");
            this.label23.Font = null;
            this.label23.Name = "label23";
            // 
            // numericUpDownFlashIdleInCycle
            // 
            this.numericUpDownFlashIdleInCycle.AccessibleDescription = null;
            this.numericUpDownFlashIdleInCycle.AccessibleName = null;
            resources.ApplyResources(this.numericUpDownFlashIdleInCycle, "numericUpDownFlashIdleInCycle");
            this.numericUpDownFlashIdleInCycle.Font = null;
            this.numericUpDownFlashIdleInCycle.Name = "numericUpDownFlashIdleInCycle";
            // 
            // label24
            // 
            this.label24.AccessibleDescription = null;
            this.label24.AccessibleName = null;
            resources.ApplyResources(this.label24, "label24");
            this.label24.Font = null;
            this.label24.Name = "label24";
            // 
            // numericUpDownsuckInkTime
            // 
            this.numericUpDownsuckInkTime.AccessibleDescription = null;
            this.numericUpDownsuckInkTime.AccessibleName = null;
            resources.ApplyResources(this.numericUpDownsuckInkTime, "numericUpDownsuckInkTime");
            this.numericUpDownsuckInkTime.Font = null;
            this.numericUpDownsuckInkTime.Name = "numericUpDownsuckInkTime";
            // 
            // label25
            // 
            this.label25.AccessibleDescription = null;
            this.label25.AccessibleName = null;
            resources.ApplyResources(this.label25, "label25");
            this.label25.Font = null;
            this.label25.Name = "label25";
            // 
            // label26
            // 
            this.label26.AccessibleDescription = null;
            this.label26.AccessibleName = null;
            resources.ApplyResources(this.label26, "label26");
            this.label26.Font = null;
            this.label26.Name = "label26";
            // 
            // numericUpDownReleaseTime
            // 
            this.numericUpDownReleaseTime.AccessibleDescription = null;
            this.numericUpDownReleaseTime.AccessibleName = null;
            resources.ApplyResources(this.numericUpDownReleaseTime, "numericUpDownReleaseTime");
            this.numericUpDownReleaseTime.Font = null;
            this.numericUpDownReleaseTime.Name = "numericUpDownReleaseTime";
            // 
            // label27
            // 
            this.label27.AccessibleDescription = null;
            this.label27.AccessibleName = null;
            resources.ApplyResources(this.label27, "label27");
            this.label27.Font = null;
            this.label27.Name = "label27";
            // 
            // numericUpDownStayTime
            // 
            this.numericUpDownStayTime.AccessibleDescription = null;
            this.numericUpDownStayTime.AccessibleName = null;
            resources.ApplyResources(this.numericUpDownStayTime, "numericUpDownStayTime");
            this.numericUpDownStayTime.Font = null;
            this.numericUpDownStayTime.Name = "numericUpDownStayTime";
            // 
            // numericUpDownLoopTimes
            // 
            this.numericUpDownLoopTimes.AccessibleDescription = null;
            this.numericUpDownLoopTimes.AccessibleName = null;
            resources.ApplyResources(this.numericUpDownLoopTimes, "numericUpDownLoopTimes");
            this.numericUpDownLoopTimes.Font = null;
            this.numericUpDownLoopTimes.Name = "numericUpDownLoopTimes";
            // 
            // label28
            // 
            this.label28.AccessibleDescription = null;
            this.label28.AccessibleName = null;
            resources.ApplyResources(this.label28, "label28");
            this.label28.Font = null;
            this.label28.Name = "label28";
            // 
            // label29
            // 
            this.label29.AccessibleDescription = null;
            this.label29.AccessibleName = null;
            resources.ApplyResources(this.label29, "label29");
            this.label29.Font = null;
            this.label29.Name = "label29";
            // 
            // numericUpDownFlashFreqInterval
            // 
            this.numericUpDownFlashFreqInterval.AccessibleDescription = null;
            this.numericUpDownFlashFreqInterval.AccessibleName = null;
            resources.ApplyResources(this.numericUpDownFlashFreqInterval, "numericUpDownFlashFreqInterval");
            this.numericUpDownFlashFreqInterval.Font = null;
            this.numericUpDownFlashFreqInterval.Name = "numericUpDownFlashFreqInterval";
            // 
            // MICOLOR_CleanSection
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.Controls.Add(this.groupBox5);
            this.Font = null;
            this.Name = "MICOLOR_CleanSection";
            this.Load += new System.EventHandler(this.MICOLOR_CleanSection_Load);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMoveSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownSuckSpeed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownOption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownflashCycle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFlashTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFlashIdleInCycle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownsuckInkTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownReleaseTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownStayTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownLoopTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownFlashFreqInterval)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.NumericUpDown numericUpDownflashCycle;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.NumericUpDown numericUpDownFlashTime;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.NumericUpDown numericUpDownFlashIdleInCycle;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.NumericUpDown numericUpDownsuckInkTime;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.NumericUpDown numericUpDownReleaseTime;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.NumericUpDown numericUpDownStayTime;
        private System.Windows.Forms.NumericUpDown numericUpDownLoopTimes;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.NumericUpDown numericUpDownFlashFreqInterval;
        private System.Windows.Forms.ComboBox comboBoxtype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDownMoveSpeed;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDownSuckSpeed;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numericUpDownOption;


        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MICOLOR_CleanSection"/> class.
        /// </summary>
        public MICOLOR_CleanSection()
        {
            this.InitializeComponent();

            foreach (MicolorCleanType mct in Enum.GetValues(typeof(MicolorCleanType)))
            {
                this.comboBoxtype.Items.Add(mct.ToString());
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The get clean section.
        /// </summary>
        /// <returns>
        /// </returns>
        public CLEANSECTION_EPSON_MICOLOR getCleanSection()
        {
            if (PubFunc.IsInDesignMode())
            {
                return this.mmicolor_cleansection;
            }

            this.mmicolor_cleansection.type = (MicolorCleanType)this.comboBoxtype.SelectedIndex;
            this.mmicolor_cleansection.LoopTimes = (byte)this.numericUpDownLoopTimes.Value;
            this.mmicolor_cleansection.SuckInkTime = (ushort)this.numericUpDownsuckInkTime.Value;
            this.mmicolor_cleansection.StayTime = (ushort)this.numericUpDownStayTime.Value;
            this.mmicolor_cleansection.ReleaseTime = (ushort)this.numericUpDownReleaseTime.Value;
            this.mmicolor_cleansection.FlashFreqInterval = (ushort)this.numericUpDownFlashFreqInterval.Value;
            this.mmicolor_cleansection.FlashTime = (byte)this.numericUpDownFlashTime.Value;
            this.mmicolor_cleansection.FlashCycle = (byte)this.numericUpDownflashCycle.Value;
            this.mmicolor_cleansection.FlashIdleInCycle = (byte)this.numericUpDownFlashIdleInCycle.Value;
            this.mmicolor_cleansection.SuckSpeed = (byte)this.numericUpDownSuckSpeed.Value;
            this.mmicolor_cleansection.MoveSpeed = (byte)this.numericUpDownMoveSpeed.Value;
            this.mmicolor_cleansection.Option = (byte)this.numericUpDownOption.Value;
            return this.mmicolor_cleansection;
        }

        /// <summary>
        /// The set clean section.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void setCleanSection(CLEANSECTION_EPSON_MICOLOR value)
        {
            this.mmicolor_cleansection = value;
            if (PubFunc.IsInDesignMode())
            {
                return;
            }

            this.comboBoxtype.SelectedIndex = (int)this.mmicolor_cleansection.type;
            this.numericUpDownLoopTimes.Value = this.mmicolor_cleansection.LoopTimes;
            this.numericUpDownsuckInkTime.Value = this.mmicolor_cleansection.SuckInkTime;
            this.numericUpDownStayTime.Value = this.mmicolor_cleansection.StayTime;
            this.numericUpDownReleaseTime.Value = this.mmicolor_cleansection.ReleaseTime;
            this.numericUpDownFlashFreqInterval.Value = this.mmicolor_cleansection.FlashFreqInterval;
            this.numericUpDownFlashTime.Value = this.mmicolor_cleansection.FlashTime;
            this.numericUpDownflashCycle.Value = this.mmicolor_cleansection.FlashCycle;
            this.numericUpDownFlashIdleInCycle.Value = this.mmicolor_cleansection.FlashIdleInCycle;
            this.numericUpDownSuckSpeed.Value = this.mmicolor_cleansection.SuckSpeed;
            this.numericUpDownMoveSpeed.Value = this.mmicolor_cleansection.MoveSpeed;
            this.numericUpDownOption.Value = this.mmicolor_cleansection.Option;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The micolo r_ clean section_ load.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void MICOLOR_CleanSection_Load(object sender, EventArgs e)
        {
            this.groupBox5.Text = this.Name;
        }

        #endregion
    }
}