namespace BYHXPrinterManager.Setting
{
    partial class CustomCloseNozzle
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomCloseNozzle));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.numInterleave = new System.Windows.Forms.NumericUpDown();
            this.numGroup = new System.Windows.Forms.NumericUpDown();
            this.lblInterleave = new System.Windows.Forms.Label();
            this.lblGroup = new System.Windows.Forms.Label();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.numLine = new System.Windows.Forms.NumericUpDown();
            this.numRow = new System.Windows.Forms.NumericUpDown();
            this.lblLine = new System.Windows.Forms.Label();
            this.lblRow = new System.Windows.Forms.Label();
            this.lblColorName = new System.Windows.Forms.Label();
            this.cbxColorName = new System.Windows.Forms.ComboBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.checkBoxClose = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInterleave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLine)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRow)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.listBox1);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnClear);
            this.groupBox2.Controls.Add(this.numInterleave);
            this.groupBox2.Controls.Add(this.numGroup);
            this.groupBox2.Controls.Add(this.lblInterleave);
            this.groupBox2.Controls.Add(this.lblGroup);
            this.groupBox2.Controls.Add(this.btnDelete);
            this.groupBox2.Controls.Add(this.btnAdd);
            this.groupBox2.Controls.Add(this.numLine);
            this.groupBox2.Controls.Add(this.numRow);
            this.groupBox2.Controls.Add(this.lblLine);
            this.groupBox2.Controls.Add(this.lblRow);
            this.groupBox2.Controls.Add(this.lblColorName);
            this.groupBox2.Controls.Add(this.cbxColorName);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // btnClear
            // 
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.Name = "btnClear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // numInterleave
            // 
            resources.ApplyResources(this.numInterleave, "numInterleave");
            this.numInterleave.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numInterleave.Name = "numInterleave";
            // 
            // numGroup
            // 
            resources.ApplyResources(this.numGroup, "numGroup");
            this.numGroup.Maximum = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.numGroup.Name = "numGroup";
            // 
            // lblInterleave
            // 
            resources.ApplyResources(this.lblInterleave, "lblInterleave");
            this.lblInterleave.Name = "lblInterleave";
            // 
            // lblGroup
            // 
            resources.ApplyResources(this.lblGroup, "lblGroup");
            this.lblGroup.Name = "lblGroup";
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // numLine
            // 
            resources.ApplyResources(this.numLine, "numLine");
            this.numLine.Name = "numLine";
            // 
            // numRow
            // 
            resources.ApplyResources(this.numRow, "numRow");
            this.numRow.Name = "numRow";
            // 
            // lblLine
            // 
            resources.ApplyResources(this.lblLine, "lblLine");
            this.lblLine.Name = "lblLine";
            // 
            // lblRow
            // 
            resources.ApplyResources(this.lblRow, "lblRow");
            this.lblRow.Name = "lblRow";
            // 
            // lblColorName
            // 
            resources.ApplyResources(this.lblColorName, "lblColorName");
            this.lblColorName.Name = "lblColorName";
            // 
            // cbxColorName
            // 
            this.cbxColorName.FormattingEnabled = true;
            this.cbxColorName.Items.AddRange(new object[] {
            resources.GetString("cbxColorName.Items"),
            resources.GetString("cbxColorName.Items1"),
            resources.GetString("cbxColorName.Items2"),
            resources.GetString("cbxColorName.Items3")});
            resources.ApplyResources(this.cbxColorName, "cbxColorName");
            this.cbxColorName.Name = "cbxColorName";
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            resources.ApplyResources(this.listBox1, "listBox1");
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectedIndexChanged += new System.EventHandler(this.listBox1_SelectedIndexChanged);
            // 
            // checkBoxClose
            // 
            resources.ApplyResources(this.checkBoxClose, "checkBoxClose");
            this.checkBoxClose.Name = "checkBoxClose";
            this.checkBoxClose.UseVisualStyleBackColor = true;
            this.checkBoxClose.CheckedChanged += new System.EventHandler(this.checkBoxClose_CheckedChanged);
            // 
            // CustomCloseNozzle
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.checkBoxClose);
            this.Controls.Add(this.groupBox1);
            this.Name = "CustomCloseNozzle";
            this.Load += new System.EventHandler(this.CustomCloseNozzle_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numInterleave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLine)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRow)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblColorName;
        private System.Windows.Forms.ComboBox cbxColorName;
        private System.Windows.Forms.NumericUpDown numLine;
        private System.Windows.Forms.NumericUpDown numRow;
        private System.Windows.Forms.Label lblLine;
        private System.Windows.Forms.Label lblRow;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.NumericUpDown numInterleave;
        private System.Windows.Forms.NumericUpDown numGroup;
        private System.Windows.Forms.Label lblInterleave;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.CheckBox checkBoxClose;
        private System.Windows.Forms.Button btnClear;
    }
}
