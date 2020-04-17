using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BYHXPrinterManager;

namespace BYHXPrinterManager.Main
{
    public partial class HistoryData : Form
    {
        public HistoryData()
        {
            InitializeComponent();
            dateTimeBegin.Value = DateTime.Now.AddDays(-2);

            DataBand();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DataBand();
        }

        private void DataBand()
        {
            DataBase DB = new DataBase();
            //select strftime('%Y-%m-%d %H:%M:%S',Time) as Time, ROUND(Area,5) as Area from AreaData
            string sql = "select ID, JobName, strftime('%Y-%m-%d %H:%M:%S',BeginTime) as BeginTime, strftime('%Y-%m-%d %H:%M:%S',Time) as PrintTime, PrintInfo from AreaData ";
            sql += " where Time > '" + dateTimeBegin.Value.ToString("yyyy-MM-dd HH:mm:ss") + "' and Time < '" + dateTimeEnd.Value.ToString("yyyy-MM-dd HH:mm:ss") + "'";
            try
            {
                if (DB.OpenDB())
                {
                    DataTable dt = DB.SelectData(sql);

                    if (dt == null || dt.Rows.Count == 0)
                    {
                        MessageBox.Show("没有查到打印记录");
                    }
                    else
                    {
                        dataGridView1.DataSource = dt;
                        dataGridView1.Refresh();
                    }

                }
                else
                {
                    MessageBox.Show("没有查到打印记录");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
