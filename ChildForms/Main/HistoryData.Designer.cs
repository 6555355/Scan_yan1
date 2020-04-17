namespace BYHXPrinterManager.Main
{
    partial class HistoryData
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTimeBegin = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dateTimeEnd = new System.Windows.Forms.DateTimePicker();
            this.lblEnd = new System.Windows.Forms.Label();
            this.lblBeginTime = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Col_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_JobName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_BeginTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_PrintTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Col_PrintInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTimeBegin);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.dateTimeEnd);
            this.groupBox1.Controls.Add(this.lblEnd);
            this.groupBox1.Controls.Add(this.lblBeginTime);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(784, 100);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "查询条件";
            // 
            // dateTimeBegin
            // 
            this.dateTimeBegin.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimeBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeBegin.Location = new System.Drawing.Point(83, 23);
            this.dateTimeBegin.Name = "dateTimeBegin";
            this.dateTimeBegin.Size = new System.Drawing.Size(150, 21);
            this.dateTimeBegin.TabIndex = 5;
            // 
            // btnSearch
            // 
            this.btnSearch.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSearch.Location = new System.Drawing.Point(428, 22);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dateTimeEnd
            // 
            this.dateTimeEnd.CustomFormat = "yyyy-MM-dd HH:mm:ss";
            this.dateTimeEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimeEnd.Location = new System.Drawing.Point(262, 23);
            this.dateTimeEnd.Name = "dateTimeEnd";
            this.dateTimeEnd.Size = new System.Drawing.Size(150, 21);
            this.dateTimeEnd.TabIndex = 6;
            // 
            // lblEnd
            // 
            this.lblEnd.AutoSize = true;
            this.lblEnd.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblEnd.Location = new System.Drawing.Point(239, 28);
            this.lblEnd.Name = "lblEnd";
            this.lblEnd.Size = new System.Drawing.Size(17, 12);
            this.lblEnd.TabIndex = 8;
            this.lblEnd.Text = "--";
            // 
            // lblBeginTime
            // 
            this.lblBeginTime.AutoSize = true;
            this.lblBeginTime.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblBeginTime.Location = new System.Drawing.Point(24, 27);
            this.lblBeginTime.Name = "lblBeginTime";
            this.lblBeginTime.Size = new System.Drawing.Size(53, 12);
            this.lblBeginTime.TabIndex = 7;
            this.lblBeginTime.Text = "打印时间";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Col_ID,
            this.Col_JobName,
            this.Col_BeginTime,
            this.Col_PrintTime,
            this.Col_PrintInfo});
            this.dataGridView1.Location = new System.Drawing.Point(6, 106);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(772, 556);
            this.dataGridView1.TabIndex = 1;
            // 
            // Col_ID
            // 
            this.Col_ID.DataPropertyName = "ID";
            this.Col_ID.FillWeight = 10F;
            this.Col_ID.HeaderText = "序号";
            this.Col_ID.MinimumWidth = 20;
            this.Col_ID.Name = "Col_ID";
            this.Col_ID.ReadOnly = true;
            this.Col_ID.Width = 60;
            // 
            // Col_JobName
            // 
            this.Col_JobName.DataPropertyName = "JobName";
            this.Col_JobName.FillWeight = 20F;
            this.Col_JobName.HeaderText = "任务名称";
            this.Col_JobName.MinimumWidth = 50;
            this.Col_JobName.Name = "Col_JobName";
            this.Col_JobName.ReadOnly = true;
            this.Col_JobName.Width = 120;
            // 
            // Col_BeginTime
            // 
            this.Col_BeginTime.DataPropertyName = "BeginTime";
            this.Col_BeginTime.FillWeight = 20F;
            this.Col_BeginTime.HeaderText = "启动时间";
            this.Col_BeginTime.MinimumWidth = 120;
            this.Col_BeginTime.Name = "Col_BeginTime";
            this.Col_BeginTime.ReadOnly = true;
            this.Col_BeginTime.Width = 120;
            // 
            // Col_PrintTime
            // 
            this.Col_PrintTime.DataPropertyName = "PrintTime";
            this.Col_PrintTime.FillWeight = 20F;
            this.Col_PrintTime.HeaderText = "打印时间";
            this.Col_PrintTime.MinimumWidth = 120;
            this.Col_PrintTime.Name = "Col_PrintTime";
            this.Col_PrintTime.ReadOnly = true;
            this.Col_PrintTime.Width = 120;
            // 
            // Col_PrintInfo
            // 
            this.Col_PrintInfo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Col_PrintInfo.DataPropertyName = "PrintInfo";
            this.Col_PrintInfo.FillWeight = 30F;
            this.Col_PrintInfo.HeaderText = "事件描述";
            this.Col_PrintInfo.MinimumWidth = 100;
            this.Col_PrintInfo.Name = "Col_PrintInfo";
            this.Col_PrintInfo.ReadOnly = true;
            // 
            // HistoryData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 662);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.Name = "HistoryData";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印记录";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTimeBegin;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DateTimePicker dateTimeEnd;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label lblBeginTime;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_JobName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_BeginTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_PrintTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Col_PrintInfo;
    }
}