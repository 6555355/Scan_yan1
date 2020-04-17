namespace BYHXPrinterManager.Main
{
    partial class ManualCleanForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ManualCleanForm));
            this.m_ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.m_PanelButtom = new System.Windows.Forms.Panel();
            this.m_ButtonExit = new System.Windows.Forms.Button();
            this.m_PanelTop = new System.Windows.Forms.Panel();
            this.pqCleanSetting1 = new BYHXPrinterManager.Setting.PQCleanSetting();
            this.hlcCleanSetting1 = new BYHXPrinterManager.Setting.HLCCleanSetting();
            this.gmaCleanSetting1 = new BYHXPrinterManager.Setting.GmaCleanSetting();
            this.docanManualCleanSetting1 = new BYHXPrinterManager.Setting.DOCANManualCleanSetting();
            this.colorjetManualCleanSettingA1 = new BYHXPrinterManager.Setting.ColorjetManualCleanSettingA();
            this.manualCleanSetting = new BYHXPrinterManager.Setting.ManualCleanSetting();
            this.colorjetManualCleanSettingS = new BYHXPrinterManager.Setting.ColorjetManualCleanSetting2();
            this.tableLayoutPanel1.SuspendLayout();
            this.m_PanelButtom.SuspendLayout();
            this.m_PanelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.m_PanelButtom, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.m_PanelTop, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // m_PanelButtom
            // 
            this.m_PanelButtom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_PanelButtom.Controls.Add(this.m_ButtonExit);
            resources.ApplyResources(this.m_PanelButtom, "m_PanelButtom");
            this.m_PanelButtom.Name = "m_PanelButtom";
            // 
            // m_ButtonExit
            // 
            this.m_ButtonExit.DialogResult = System.Windows.Forms.DialogResult.OK;
            resources.ApplyResources(this.m_ButtonExit, "m_ButtonExit");
            this.m_ButtonExit.Name = "m_ButtonExit";
            this.m_ButtonExit.UseVisualStyleBackColor = true;
            this.m_ButtonExit.Click += new System.EventHandler(this.m_ButtonExit_Click);
            // 
            // m_PanelTop
            // 
            this.m_PanelTop.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.m_PanelTop.Controls.Add(this.pqCleanSetting1);
            this.m_PanelTop.Controls.Add(this.hlcCleanSetting1);
            this.m_PanelTop.Controls.Add(this.gmaCleanSetting1);
            this.m_PanelTop.Controls.Add(this.docanManualCleanSetting1);
            this.m_PanelTop.Controls.Add(this.colorjetManualCleanSettingA1);
            this.m_PanelTop.Controls.Add(this.manualCleanSetting);
            this.m_PanelTop.Controls.Add(this.colorjetManualCleanSettingS);
            resources.ApplyResources(this.m_PanelTop, "m_PanelTop");
            this.m_PanelTop.Name = "m_PanelTop";
            // 
            // pqCleanSetting1
            // 
            resources.ApplyResources(this.pqCleanSetting1, "pqCleanSetting1");
            this.pqCleanSetting1.Name = "pqCleanSetting1";
            // 
            // hlcCleanSetting1
            // 
            resources.ApplyResources(this.hlcCleanSetting1, "hlcCleanSetting1");
            this.hlcCleanSetting1.Name = "hlcCleanSetting1";
            // 
            // gmaCleanSetting1
            // 
            resources.ApplyResources(this.gmaCleanSetting1, "gmaCleanSetting1");
            this.gmaCleanSetting1.Name = "gmaCleanSetting1";
            // 
            // docanManualCleanSetting1
            // 
            resources.ApplyResources(this.docanManualCleanSetting1, "docanManualCleanSetting1");
            this.docanManualCleanSetting1.Name = "docanManualCleanSetting1";
            // 
            // colorjetManualCleanSettingA1
            // 
            resources.ApplyResources(this.colorjetManualCleanSettingA1, "colorjetManualCleanSettingA1");
            this.colorjetManualCleanSettingA1.Name = "colorjetManualCleanSettingA1";
            // 
            // manualCleanSetting
            // 
            resources.ApplyResources(this.manualCleanSetting, "manualCleanSetting");
            this.manualCleanSetting.Name = "manualCleanSetting";
            // 
            // colorjetManualCleanSettingS
            // 
            resources.ApplyResources(this.colorjetManualCleanSettingS, "colorjetManualCleanSettingS");
            this.colorjetManualCleanSettingS.Name = "colorjetManualCleanSettingS";
            // 
            // ManualCleanForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "ManualCleanForm";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.m_PanelButtom.ResumeLayout(false);
            this.m_PanelTop.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ToolTip m_ToolTip;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel m_PanelButtom;
        private System.Windows.Forms.Button m_ButtonExit;
        private System.Windows.Forms.Panel m_PanelTop;
        private Setting.ManualCleanSetting manualCleanSetting;
        private Setting.ColorjetManualCleanSetting2 colorjetManualCleanSettingS;
        private Setting.ColorjetManualCleanSettingA colorjetManualCleanSettingA1;
        private Setting.DOCANManualCleanSetting docanManualCleanSetting1;
        private Setting.GmaCleanSetting gmaCleanSetting1;
        private Setting.HLCCleanSetting hlcCleanSetting1;
        private Setting.PQCleanSetting pqCleanSetting1;
    }
}