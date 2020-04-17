using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using BYHXPrinterManager;

namespace EpsonControlLibrary
{
    public class MicolorAutoSpraySetting : UserControl
    {
    	        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MicolorAutoSpraySetting));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBoxAutoSpray = new System.Windows.Forms.CheckBox();
            this.numpassNum = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numSprayInterval = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numSprayWidth = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonRead = new System.Windows.Forms.Button();
            this.buttonSet = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numpassNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSprayInterval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSprayWidth)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.AccessibleDescription = null;
            this.groupBox1.AccessibleName = null;
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.BackgroundImage = null;
            this.groupBox1.Controls.Add(this.checkBoxAutoSpray);
            this.groupBox1.Controls.Add(this.numpassNum);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numSprayInterval);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.numSprayWidth);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.buttonRead);
            this.groupBox1.Controls.Add(this.buttonSet);
            this.groupBox1.Font = null;
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // checkBoxAutoSpray
            // 
            this.checkBoxAutoSpray.AccessibleDescription = null;
            this.checkBoxAutoSpray.AccessibleName = null;
            resources.ApplyResources(this.checkBoxAutoSpray, "checkBoxAutoSpray");
            this.checkBoxAutoSpray.BackgroundImage = null;
            this.checkBoxAutoSpray.Font = null;
            this.checkBoxAutoSpray.Name = "checkBoxAutoSpray";
            // 
            // numpassNum
            // 
            this.numpassNum.AccessibleDescription = null;
            this.numpassNum.AccessibleName = null;
            resources.ApplyResources(this.numpassNum, "numpassNum");
            this.numpassNum.Font = null;
            this.numpassNum.Name = "numpassNum";
            // 
            // label5
            // 
            this.label5.AccessibleDescription = null;
            this.label5.AccessibleName = null;
            resources.ApplyResources(this.label5, "label5");
            this.label5.Font = null;
            this.label5.Name = "label5";
            // 
            // numSprayInterval
            // 
            this.numSprayInterval.AccessibleDescription = null;
            this.numSprayInterval.AccessibleName = null;
            resources.ApplyResources(this.numSprayInterval, "numSprayInterval");
            this.numSprayInterval.Font = null;
            this.numSprayInterval.Name = "numSprayInterval";
            // 
            // label4
            // 
            this.label4.AccessibleDescription = null;
            this.label4.AccessibleName = null;
            resources.ApplyResources(this.label4, "label4");
            this.label4.Font = null;
            this.label4.Name = "label4";
            // 
            // numSprayWidth
            // 
            this.numSprayWidth.AccessibleDescription = null;
            this.numSprayWidth.AccessibleName = null;
            resources.ApplyResources(this.numSprayWidth, "numSprayWidth");
            this.numSprayWidth.Font = null;
            this.numSprayWidth.Name = "numSprayWidth";
            // 
            // label2
            // 
            this.label2.AccessibleDescription = null;
            this.label2.AccessibleName = null;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Font = null;
            this.label2.Name = "label2";
            // 
            // buttonRead
            // 
            this.buttonRead.AccessibleDescription = null;
            this.buttonRead.AccessibleName = null;
            resources.ApplyResources(this.buttonRead, "buttonRead");
            this.buttonRead.BackgroundImage = null;
            this.buttonRead.Font = null;
            this.buttonRead.Name = "buttonRead";
            this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
            // 
            // buttonSet
            // 
            this.buttonSet.AccessibleDescription = null;
            this.buttonSet.AccessibleName = null;
            resources.ApplyResources(this.buttonSet, "buttonSet");
            this.buttonSet.BackgroundImage = null;
            this.buttonSet.Font = null;
            this.buttonSet.Name = "buttonSet";
            this.buttonSet.Click += new System.EventHandler(this.buttonSet_Click);
            // 
            // MicolorAutoSpraySetting
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.BackgroundImage = null;
            this.Controls.Add(this.groupBox1);
            this.Font = null;
            this.Name = "MicolorAutoSpraySetting";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numpassNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSprayInterval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSprayWidth)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button buttonRead;
        private System.Windows.Forms.Button buttonSet;
        private System.Windows.Forms.CheckBox checkBoxAutoSpray;
        private System.Windows.Forms.NumericUpDown numpassNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown numSprayInterval;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numSprayWidth;
        private System.Windows.Forms.Label label2;
        
        public MicolorAutoSpraySetting()
        {
            InitializeComponent();
        }

        private void buttonRead_Click(object sender, EventArgs e)
        {
            byte[] subval = new byte[9];
			uint bufsize = (uint)subval.Length;
            if (CoreInterface.GetEpsonEP0Cmd( 0x7d,subval,ref bufsize, 2, 0) == 0)
            {
                MessageBox.Show("获取MIC清洗闪喷参数失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                this.checkBoxAutoSpray.Checked = subval[0] == 1;
                this.numSprayWidth.Value = BitConverter.ToInt16(subval, 1);
                this.numSprayInterval.Value = BitConverter.ToInt32(subval, 3);
                this.numpassNum.Value = BitConverter.ToInt16(subval, 7);
            }
        }

        private void buttonSet_Click(object sender, EventArgs e)
        {
            byte[] subval = new byte[9];
			uint bufsize =(uint)subval.Length;
            subval[0]=(byte)(this.checkBoxAutoSpray.Checked?1:0);
            Buffer.BlockCopy(BitConverter.GetBytes((Int16)this.numSprayWidth.Value),0,subval,1,2);
            Buffer.BlockCopy(BitConverter.GetBytes((int)this.numSprayInterval.Value), 0, subval, 3, 4);
            Buffer.BlockCopy(BitConverter.GetBytes((Int16)this.numpassNum.Value), 0, subval, 7, 2);
            if (CoreInterface.SetEpsonEP0Cmd(0x7d,subval,ref bufsize,   2, 0) == 0)
            {
                MessageBox.Show("设置MIC清洗闪喷参数失败！", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
