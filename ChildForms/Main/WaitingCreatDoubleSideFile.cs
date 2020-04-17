using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BYHXPrinterManager.JobListView;

namespace BYHXPrinterManager.Main
{
    public partial class WaitingCreatDoubleSideFile : Form
    {
        private GenDoublePrintPrtWorker worker;
        public WaitingCreatDoubleSideFile(GenDoublePrintPrtWorker genDoublePrintPrtWorker)
        {
            InitializeComponent();

            worker = genDoublePrintPrtWorker;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            worker.Abort();
            this.Close();
        }

        private void WaitingCreatDoubleSideFile_Load(object sender, EventArgs e)
        {
            this.progressBar1.Value = 50;
        }
    }
}
