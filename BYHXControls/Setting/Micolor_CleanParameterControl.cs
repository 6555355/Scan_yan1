// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Micolor_CleanParameterControl.cs" company="">
//   
// </copyright>
// <summary>
//   The micolor_ clean parameter control.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using PrinterStubC.Utility;

namespace EpsonControlLibrary
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;
	using BYHXPrinterManager;

    /// <summary>
    /// The micolor_ clean parameter control.
    /// </summary>
    public class Micolor_CleanParameterControl : BYHXPrinterManager.Setting.BYHXUserControl
    {
        #region Constants and Fields

        /// <summary>
        /// The m clean parameter.
        /// </summary>
        private CleanparaEpsonMicolor mCleanParameter;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Micolor_CleanParameterControl));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numCarriage_X_WipePos_1_Start = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.numCarriage_X_WipePos_1_End = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.numCarriage_X_WipePos_Start = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numCarriage_X_FlashPos = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numCarriage_X_ReleasePos = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numCarriage_X_WipePos_End = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numCarriage_X_SuckPos = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.Section4 = new EpsonControlLibrary.MICOLOR_CleanSection();
            this.Section3 = new EpsonControlLibrary.MICOLOR_CleanSection();
            this.Section2 = new EpsonControlLibrary.MICOLOR_CleanSection();
            this.Section1 = new EpsonControlLibrary.MICOLOR_CleanSection();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.numWiper_Y_HideToWipeDistance_1 = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numSpeed_Interval4 = new System.Windows.Forms.NumericUpDown();
            this.numSpeed_Interval3 = new System.Windows.Forms.NumericUpDown();
            this.numSpeed_Interval2 = new System.Windows.Forms.NumericUpDown();
            this.numSpeed_Interval1 = new System.Windows.Forms.NumericUpDown();
            this.numWiper_Y_HideToWipeDistance = new System.Windows.Forms.NumericUpDown();
            this.label13 = new System.Windows.Forms.Label();
            this.numWiper_Y_SuckToHideDistance = new System.Windows.Forms.NumericUpDown();
            this.label14 = new System.Windows.Forms.Label();
            this.numRotateDir = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.numsectionCount = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.numWiper_Y_WipeToSuckDistance = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.numCarriage_X_Wipe_Speed = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBoxCleanMode = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonRead = new System.Windows.Forms.Button();
            this.buttonSet = new System.Windows.Forms.Button();
            this.toolStrip2 = new System.Windows.Forms.Panel();
            this.openToolStripButton = new System.Windows.Forms.Button();
            this.saveToolStripButton = new System.Windows.Forms.Button();
            this.toolStripButton1 = new System.Windows.Forms.Button();
            this.toolStripButton2 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new EpsonControlLibrary.MicolorAutoSpraySetting();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCarriage_X_WipePos_1_Start)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCarriage_X_WipePos_1_End)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCarriage_X_WipePos_Start)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCarriage_X_FlashPos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCarriage_X_ReleasePos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCarriage_X_WipePos_End)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCarriage_X_SuckPos)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWiper_Y_HideToWipeDistance_1)).BeginInit();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed_Interval4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed_Interval3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed_Interval2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed_Interval1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWiper_Y_HideToWipeDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWiper_Y_SuckToHideDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRotateDir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numsectionCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWiper_Y_WipeToSuckDistance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCarriage_X_Wipe_Speed)).BeginInit();
            this.panel1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.numCarriage_X_WipePos_1_Start);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.numCarriage_X_WipePos_1_End);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.numCarriage_X_WipePos_Start);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.numCarriage_X_FlashPos);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numCarriage_X_ReleasePos);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numCarriage_X_WipePos_End);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.numCarriage_X_SuckPos);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // numCarriage_X_WipePos_1_Start
            // 
            resources.ApplyResources(this.numCarriage_X_WipePos_1_Start, "numCarriage_X_WipePos_1_Start");
            this.numCarriage_X_WipePos_1_Start.Name = "numCarriage_X_WipePos_1_Start";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // numCarriage_X_WipePos_1_End
            // 
            resources.ApplyResources(this.numCarriage_X_WipePos_1_End, "numCarriage_X_WipePos_1_End");
            this.numCarriage_X_WipePos_1_End.Name = "numCarriage_X_WipePos_1_End";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // numCarriage_X_WipePos_Start
            // 
            resources.ApplyResources(this.numCarriage_X_WipePos_Start, "numCarriage_X_WipePos_Start");
            this.numCarriage_X_WipePos_Start.Name = "numCarriage_X_WipePos_Start";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // numCarriage_X_FlashPos
            // 
            resources.ApplyResources(this.numCarriage_X_FlashPos, "numCarriage_X_FlashPos");
            this.numCarriage_X_FlashPos.Name = "numCarriage_X_FlashPos";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // numCarriage_X_ReleasePos
            // 
            resources.ApplyResources(this.numCarriage_X_ReleasePos, "numCarriage_X_ReleasePos");
            this.numCarriage_X_ReleasePos.Name = "numCarriage_X_ReleasePos";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // numCarriage_X_WipePos_End
            // 
            resources.ApplyResources(this.numCarriage_X_WipePos_End, "numCarriage_X_WipePos_End");
            this.numCarriage_X_WipePos_End.Name = "numCarriage_X_WipePos_End";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // numCarriage_X_SuckPos
            // 
            resources.ApplyResources(this.numCarriage_X_SuckPos, "numCarriage_X_SuckPos");
            this.numCarriage_X_SuckPos.Name = "numCarriage_X_SuckPos";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.Section4);
            this.groupBox2.Controls.Add(this.Section3);
            this.groupBox2.Controls.Add(this.Section2);
            this.groupBox2.Controls.Add(this.Section1);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // Section4
            // 
            resources.ApplyResources(this.Section4, "Section4");
            this.Section4.Name = "Section4";
            // 
            // Section3
            // 
            resources.ApplyResources(this.Section3, "Section3");
            this.Section3.Name = "Section3";
            // 
            // Section2
            // 
            resources.ApplyResources(this.Section2, "Section2");
            this.Section2.Name = "Section2";
            // 
            // Section1
            // 
            resources.ApplyResources(this.Section1, "Section1");
            this.Section1.Name = "Section1";
            // 
            // groupBox3
            // 
            resources.ApplyResources(this.groupBox3, "groupBox3");
            this.groupBox3.Controls.Add(this.numWiper_Y_HideToWipeDistance_1);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.groupBox4);
            this.groupBox3.Controls.Add(this.numWiper_Y_HideToWipeDistance);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.numWiper_Y_SuckToHideDistance);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.numRotateDir);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.numsectionCount);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.numWiper_Y_WipeToSuckDistance);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Controls.Add(this.numCarriage_X_Wipe_Speed);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.TabStop = false;
            // 
            // numWiper_Y_HideToWipeDistance_1
            // 
            resources.ApplyResources(this.numWiper_Y_HideToWipeDistance_1, "numWiper_Y_HideToWipeDistance_1");
            this.numWiper_Y_HideToWipeDistance_1.Name = "numWiper_Y_HideToWipeDistance_1";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // groupBox4
            // 
            resources.ApplyResources(this.groupBox4, "groupBox4");
            this.groupBox4.Controls.Add(this.numSpeed_Interval4);
            this.groupBox4.Controls.Add(this.numSpeed_Interval3);
            this.groupBox4.Controls.Add(this.numSpeed_Interval2);
            this.groupBox4.Controls.Add(this.numSpeed_Interval1);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.TabStop = false;
            // 
            // numSpeed_Interval4
            // 
            resources.ApplyResources(this.numSpeed_Interval4, "numSpeed_Interval4");
            this.numSpeed_Interval4.Name = "numSpeed_Interval4";
            // 
            // numSpeed_Interval3
            // 
            resources.ApplyResources(this.numSpeed_Interval3, "numSpeed_Interval3");
            this.numSpeed_Interval3.Name = "numSpeed_Interval3";
            // 
            // numSpeed_Interval2
            // 
            resources.ApplyResources(this.numSpeed_Interval2, "numSpeed_Interval2");
            this.numSpeed_Interval2.Name = "numSpeed_Interval2";
            // 
            // numSpeed_Interval1
            // 
            resources.ApplyResources(this.numSpeed_Interval1, "numSpeed_Interval1");
            this.numSpeed_Interval1.Name = "numSpeed_Interval1";
            // 
            // numWiper_Y_HideToWipeDistance
            // 
            resources.ApplyResources(this.numWiper_Y_HideToWipeDistance, "numWiper_Y_HideToWipeDistance");
            this.numWiper_Y_HideToWipeDistance.Name = "numWiper_Y_HideToWipeDistance";
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.Name = "label13";
            // 
            // numWiper_Y_SuckToHideDistance
            // 
            resources.ApplyResources(this.numWiper_Y_SuckToHideDistance, "numWiper_Y_SuckToHideDistance");
            this.numWiper_Y_SuckToHideDistance.Name = "numWiper_Y_SuckToHideDistance";
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.Name = "label14";
            // 
            // numRotateDir
            // 
            resources.ApplyResources(this.numRotateDir, "numRotateDir");
            this.numRotateDir.Name = "numRotateDir";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.Name = "label15";
            // 
            // numsectionCount
            // 
            resources.ApplyResources(this.numsectionCount, "numsectionCount");
            this.numsectionCount.Name = "numsectionCount";
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.Name = "label16";
            // 
            // numWiper_Y_WipeToSuckDistance
            // 
            resources.ApplyResources(this.numWiper_Y_WipeToSuckDistance, "numWiper_Y_WipeToSuckDistance");
            this.numWiper_Y_WipeToSuckDistance.Name = "numWiper_Y_WipeToSuckDistance";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.Name = "label17";
            // 
            // numCarriage_X_Wipe_Speed
            // 
            resources.ApplyResources(this.numCarriage_X_Wipe_Speed, "numCarriage_X_Wipe_Speed");
            this.numCarriage_X_Wipe_Speed.Name = "numCarriage_X_Wipe_Speed";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.Name = "label18";
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.comboBoxCleanMode);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.buttonRead);
            this.panel1.Controls.Add(this.buttonSet);
            this.panel1.Name = "panel1";
            // 
            // comboBoxCleanMode
            // 
            resources.ApplyResources(this.comboBoxCleanMode, "comboBoxCleanMode");
            this.comboBoxCleanMode.Name = "comboBoxCleanMode";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // buttonRead
            // 
            resources.ApplyResources(this.buttonRead, "buttonRead");
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
            // 
            // buttonSet
            // 
            resources.ApplyResources(this.buttonSet, "buttonSet");
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // toolStrip2
            // 
            resources.ApplyResources(this.toolStrip2, "toolStrip2");
            this.toolStrip2.Controls.Add(this.openToolStripButton);
            this.toolStrip2.Controls.Add(this.saveToolStripButton);
            this.toolStrip2.Name = "toolStrip2";
            // 
            // openToolStripButton
            // 
            resources.ApplyResources(this.openToolStripButton, "openToolStripButton");
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Click += new System.EventHandler(this.openToolStripButton_Click);
            // 
            // saveToolStripButton
            // 
            resources.ApplyResources(this.saveToolStripButton, "saveToolStripButton");
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Click += new System.EventHandler(this.saveToolStripButton_Click);
            // 
            // toolStripButton1
            // 
            resources.ApplyResources(this.toolStripButton1, "toolStripButton1");
            this.toolStripButton1.Name = "toolStripButton1";
            // 
            // toolStripButton2
            // 
            resources.ApplyResources(this.toolStripButton2, "toolStripButton2");
            this.toolStripButton2.Name = "toolStripButton2";
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.toolStrip2);
            this.panel2.Name = "panel2";
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // Micolor_CleanParameterControl
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Micolor_CleanParameterControl";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numCarriage_X_WipePos_1_Start)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCarriage_X_WipePos_1_End)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCarriage_X_WipePos_Start)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCarriage_X_FlashPos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCarriage_X_ReleasePos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCarriage_X_WipePos_End)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCarriage_X_SuckPos)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWiper_Y_HideToWipeDistance_1)).EndInit();
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed_Interval4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed_Interval3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed_Interval2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSpeed_Interval1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWiper_Y_HideToWipeDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWiper_Y_SuckToHideDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRotateDir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numsectionCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numWiper_Y_WipeToSuckDistance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numCarriage_X_Wipe_Speed)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

		}

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numCarriage_X_SuckPos;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numCarriage_X_WipePos_Start;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numCarriage_X_FlashPos;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numCarriage_X_ReleasePos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numCarriage_X_WipePos_End;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.NumericUpDown numWiper_Y_HideToWipeDistance;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numWiper_Y_SuckToHideDistance;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.NumericUpDown numRotateDir;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown numsectionCount;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.NumericUpDown numWiper_Y_WipeToSuckDistance;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.NumericUpDown numCarriage_X_Wipe_Speed;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown numSpeed_Interval4;
        private System.Windows.Forms.NumericUpDown numSpeed_Interval3;
        private System.Windows.Forms.NumericUpDown numSpeed_Interval2;
        private System.Windows.Forms.NumericUpDown numSpeed_Interval1;
        private MICOLOR_CleanSection Section1;
        private MICOLOR_CleanSection Section3;
        private MICOLOR_CleanSection Section2;
        private MICOLOR_CleanSection Section4;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonRead;
        private System.Windows.Forms.Button buttonSet;
        private System.Windows.Forms.ComboBox comboBoxCleanMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel toolStrip2;
        private System.Windows.Forms.Button openToolStripButton;
        private System.Windows.Forms.Button saveToolStripButton;
        private MicolorAutoSpraySetting toolStrip1;
        private System.Windows.Forms.Button toolStripButton1;
        private System.Windows.Forms.Button toolStripButton2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown numCarriage_X_WipePos_1_Start;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numCarriage_X_WipePos_1_End;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown numWiper_Y_HideToWipeDistance_1;
        private System.Windows.Forms.Label label8;
        
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Micolor_CleanParameterControl"/> class.
        /// </summary>
        public Micolor_CleanParameterControl()
        {
            this.InitializeComponent();

            PubFunc.SetNumricMaxAndMin(this,true);


            this.comboBoxCleanMode.Items.Clear();
            foreach (EpsonAutoCleanWay value in Enum.GetValues(typeof(EpsonAutoCleanWay)))
            {
				string txt = ResString.GetEnumDisplayName(value.GetType(),value);
                this.comboBoxCleanMode.Items.Add(txt);
            }
            this.comboBoxCleanMode.SelectedIndex = 0;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// The get clean parameter.
        /// </summary>
        /// <returns>
        /// </returns>
        public CleanparaEpsonMicolor getCleanParameter()
        {
            if (PubFunc.IsInDesignMode())
            {
                return this.mCleanParameter;
            }
            this.mCleanParameter = new CleanparaEpsonMicolor();
            this.mCleanParameter.Carriage_X_Wipe_Speed = (byte)this.numCarriage_X_Wipe_Speed.Value;
            if (this.mCleanParameter.Speed_Interval == null)
                this.mCleanParameter.Speed_Interval = new byte[4];
            this.mCleanParameter.Speed_Interval[0] = (byte)this.numSpeed_Interval1.Value;
            this.mCleanParameter.Speed_Interval[1] = (byte)this.numSpeed_Interval2.Value;
            this.mCleanParameter.Speed_Interval[2] = (byte)this.numSpeed_Interval3.Value;
            this.mCleanParameter.Speed_Interval[3] = (byte)this.numSpeed_Interval4.Value;
            this.mCleanParameter.RotateDir = (byte)this.numRotateDir.Value;
            this.mCleanParameter.Wiper_Y_HideToWipeDistance = (ushort)this.numWiper_Y_HideToWipeDistance.Value;
            this.mCleanParameter.Wiper_Y_WipeToSuckDistance = (ushort)this.numWiper_Y_WipeToSuckDistance.Value;
            this.mCleanParameter.Wiper_Y_SuckToHideDistance = (ushort)this.numWiper_Y_SuckToHideDistance.Value;
            this.mCleanParameter.sectionCount = (byte)this.numsectionCount.Value;
            this.mCleanParameter.Carriage_X_SuckPos = (short)this.numCarriage_X_SuckPos.Value;
            this.mCleanParameter.Carriage_X_ReleasePos = (ushort)this.numCarriage_X_ReleasePos.Value;
            this.mCleanParameter.Carriage_X_WipePos_Start = (ushort)this.numCarriage_X_WipePos_Start.Value;
            this.mCleanParameter.Carriage_X_WipePos_End = (ushort)this.numCarriage_X_WipePos_End.Value;
            this.mCleanParameter.Carriage_X_FlashPos = (ushort)this.numCarriage_X_FlashPos.Value;
            if(this.mCleanParameter.sections == null)
                this.mCleanParameter.sections = new CLEANSECTION_EPSON_MICOLOR[4];
            this.mCleanParameter.sections[0] = this.Section1.getCleanSection();
            this.mCleanParameter.sections[1] = this.Section2.getCleanSection();
            this.mCleanParameter.sections[2] = this.Section3.getCleanSection();
            this.mCleanParameter.sections[3] = this.Section4.getCleanSection();

            this.mCleanParameter.Wiper_Y_HideToWipeDistance_1 = (ushort)this.numWiper_Y_HideToWipeDistance_1.Value;
            this.mCleanParameter.Carriage_X_WipePos_1_Start = (ushort) this.numCarriage_X_WipePos_1_Start.Value;
            this.mCleanParameter.Carriage_X_WipePos_1_End = (ushort) this.numCarriage_X_WipePos_1_End.Value;
            return this.mCleanParameter;
        }

        /// <summary>
        /// The set clean parameter.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        public void setCleanParameter(CleanparaEpsonMicolor value)
        {
            if (PubFunc.IsInDesignMode())
            {
                return;
            }

            this.mCleanParameter = value;
            this.numCarriage_X_Wipe_Speed.Value = this.mCleanParameter.Carriage_X_Wipe_Speed;
            this.numSpeed_Interval1.Value = this.mCleanParameter.Speed_Interval[0];
            this.numSpeed_Interval2.Value = this.mCleanParameter.Speed_Interval[1];
            this.numSpeed_Interval3.Value = this.mCleanParameter.Speed_Interval[2];
            this.numSpeed_Interval4.Value = this.mCleanParameter.Speed_Interval[3];
            this.numRotateDir.Value = this.mCleanParameter.RotateDir;
            this.numWiper_Y_HideToWipeDistance.Value = this.mCleanParameter.Wiper_Y_HideToWipeDistance;
            this.numWiper_Y_WipeToSuckDistance.Value = this.mCleanParameter.Wiper_Y_WipeToSuckDistance;
            this.numWiper_Y_SuckToHideDistance.Value = this.mCleanParameter.Wiper_Y_SuckToHideDistance;
            this.numsectionCount.Value = this.mCleanParameter.sectionCount;
            this.numCarriage_X_SuckPos.Value = this.mCleanParameter.Carriage_X_SuckPos;
            this.numCarriage_X_ReleasePos.Value = this.mCleanParameter.Carriage_X_ReleasePos;
            this.numCarriage_X_WipePos_Start.Value = this.mCleanParameter.Carriage_X_WipePos_Start;
            this.numCarriage_X_WipePos_End.Value = this.mCleanParameter.Carriage_X_WipePos_End;
            this.numCarriage_X_FlashPos.Value = this.mCleanParameter.Carriage_X_FlashPos;
            this.Section1.setCleanSection(this.mCleanParameter.sections[0]);
            this.Section2.setCleanSection(this.mCleanParameter.sections[1]);
            this.Section3.setCleanSection(this.mCleanParameter.sections[2]);
            this.Section4.setCleanSection(this.mCleanParameter.sections[3]);
           this.numWiper_Y_HideToWipeDistance_1.Value = this.mCleanParameter.Wiper_Y_HideToWipeDistance_1;
           this.numCarriage_X_WipePos_1_Start.Value= this.mCleanParameter.Carriage_X_WipePos_1_Start;
            this.numCarriage_X_WipePos_1_End.Value = this.mCleanParameter.Carriage_X_WipePos_1_End;
        }

        #endregion

        #region Implemented Interfaces

        #region ISerialize

        /// <summary>
        /// The from xml.
        /// </summary>
        /// <param name="xmlstring">
        /// The xmlstring.
        /// </param>
        public void FromXML(string xmlstring)
        {
            this.mCleanParameter =
                (CleanparaEpsonMicolor)PubFunc.SystemConvertFromXml(xmlstring, typeof(CleanparaEpsonMicolor));
            this.setCleanParameter(this.mCleanParameter);
        }

        /// <summary>
        /// The to xml.
        /// </summary>
        /// <returns>
        /// The to xml.
        /// </returns>
        public string ToXML()
        {
            this.mCleanParameter = this.getCleanParameter();
            return PubFunc.SystemConvertToXml(this.mCleanParameter, typeof(CleanparaEpsonMicolor));
        }

        #endregion

        #endregion

        /// <summary>
        /// The button read_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void buttonRead_Click(object sender, EventArgs e)
        {
            byte[] vals = this.ReadMICCleanParameter();
			this.mCleanParameter = (CleanparaEpsonMicolor)PubFunc.BytesToStruct(vals, typeof(CleanparaEpsonMicolor));
//			this.mCleanParameter = (CleanparaEpsonMicolor)PubFunc.BytesToStruct(vals,typeof(CleanparaEpsonMicolor), 93);
            this.setCleanParameter(this.mCleanParameter);
        }

        /// <summary>
        /// The button set_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void buttonSet_Click(object sender, EventArgs e)
        {
            this.SetMICCleanParameter();

        }

        public EpsonAutoCleanWay CleanWay
        {
            get
            {
                return (EpsonAutoCleanWay)this.comboBoxCleanMode.SelectedIndex;
            }
            set
            {
                this.comboBoxCleanMode.SelectedIndex = (int)value;
            }
        }


        /// <summary>
        /// The open tool strip button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Setting Files (*.xml)|*.xml";
            ofd.FileName = Application.StartupPath;
            DialogResult dr = ofd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                fileName = ofd.FileName;
            }
            else
            {
                return;
            }

            SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
            try
            {
                doc.Load(fileName);
                XmlElement root = doc.DocumentElement;

                // adParamT_Epson2HeadV1
                this.FromXML(root.ChildNodes[0].OuterXml);

            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message);
            }
        }

        /// <summary>
        /// The save tool strip button_ click.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            string fileName = string.Empty;
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Setting Files (*.xml)|*.xml";
            sfd.FileName = Application.StartupPath;
            DialogResult dr = sfd.ShowDialog();
            if (dr == DialogResult.OK)
            {
                fileName = sfd.FileName;
            }
            else
            {
                return;
            }

            SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
            try
            {
                XmlElement root = doc.CreateElement("MICP");
                doc.AppendChild(root);
                string xml = string.Empty;

                // adParamT_Epson2HeadV1
                xml += this.ToXML();

                root.InnerXml = xml;
                doc.Save(fileName);
            }
            catch (Exception ex)
            {
                Debug.Assert(false, ex.Message + ex.StackTrace);
            }
        }




        /// <summary>
        /// 2。读相应的Parameter.
        /// 通过USB EP0。方向是IN。
        /// reqCode是0x7F。value是3。EP0是要读的参数。
        /// 增加Index值，index为0 是allwin清洗参数。index为1是MIC清洗参数。
        /// </summary>
        private byte[] ReadMICCleanParameter()
        {
            byte[] ret = new byte[93];//[Marshal.SizeOf(typeof(CleanparaEpsonMicolor))];
            byte[] subFirst = new byte[64];
			uint bufsize = (uint)subFirst.Length;
            ushort index = (ushort)((this.comboBoxCleanMode.SelectedIndex << 8) + 1);
            if (CoreInterface.GetEpsonEP0Cmd(0x7F,subFirst,ref bufsize,  (ushort)3,(ushort)index) == 0)
            {
                MessageBox.Show("读MIC清洗Parameter失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Buffer.BlockCopy(subFirst, 0, ret, 0, subFirst.Length);
            byte[] subSec = new byte[ret.Length - subFirst.Length];
            index = (ushort)((this.comboBoxCleanMode.SelectedIndex << 8) + 2);
			bufsize = (uint)subSec.Length;
            if (CoreInterface.GetEpsonEP0Cmd(0x7F,subSec,  ref bufsize, (ushort)3,(ushort)index) == 0)
            {
                MessageBox.Show("读MIC清洗Parameter失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            Buffer.BlockCopy(subSec, 0, ret, subFirst.Length, subSec.Length);
            return ret;
        }





        /// <summary>
        /// 1。设置Parameter
        /// 通过USB EP0。方向是OUT。
        /// reqCode是0x7F。value是2，setuplength是长度，一次操作的长度不能超过64Byte。
        /// 写失败会报STATUS_FTA_EEPROM_WRITE.
        /// 增加Index值，index为0 是allwin清洗参数。index为1是MIC清洗参数。
        /// </summary>
        /// <returns>
        /// The set MIC clean parameter.
        /// </returns>
        private bool SetMICCleanParameter()
        {
            byte[] subval = PubFunc.StructToBytes(this.getCleanParameter());

            byte[] Buf = new byte[64];
			uint bufsize = (uint)Buf.Length;
            Buffer.BlockCopy(subval, 0, Buf, 0, Buf.Length);
            ushort index = (ushort)((this.comboBoxCleanMode.SelectedIndex << 8) + 1);
            if (CoreInterface.SetEpsonEP0Cmd(0x7F, Buf,ref bufsize,  2, index)==0)
            {
                MessageBox.Show("设置MIC清洗Parameter失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            byte[] Buf1 = new byte[subval.Length - 64];
            Buffer.BlockCopy(subval, Buf.Length, Buf1, 0, Buf1.Length);
            index = (ushort)((this.comboBoxCleanMode.SelectedIndex << 8) + 2);
            bufsize = (uint)Buf1.Length;
			if (CoreInterface.SetEpsonEP0Cmd(0x7F, Buf1,ref bufsize,  2, index)==0)
			{
                MessageBox.Show("设置MIC清洗Parameter失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return true;
        }
    }
}