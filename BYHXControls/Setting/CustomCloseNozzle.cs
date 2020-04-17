using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace BYHXPrinterManager.Setting
{
    public partial class CustomCloseNozzle : BYHXUserControl
    {
        string filePath = Application.StartupPath + "\\CustomCloseNozzle.txt";


        public CustomCloseNozzle()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string info = cbxColorName.Text + ","
                + numGroup.Value.ToString() + ","
                + numInterleave.Value.ToString() + ","
                + numRow.Value.ToString() + ","
                + numLine.Value.ToString();

            if (listBox1.Items.IndexOf(info) < 0)
            {
                listBox1.Items.Add(info);
            }
            else 
            {
                string message = ResString.GetResString("ColseNozzle_AlreadyExists");
                MessageBox.Show(message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0) return;

            listBox1.Items.RemoveAt(listBox1.SelectedIndex);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //if (this.checkBoxClose.Checked == false)
            //{
            //    if (File.Exists(filePath))
            //    {
            //        File.Delete(filePath);
            //    }
            //}
            //else
            {
                Save();
                MessageBox.Show("Save Successful!",ResString.GetProductName());
            }
        }

        public void Save()
        {
            CoreInterface.ClearList();

            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                string info = listBox1.GetItemText(listBox1.Items[i]);
                string[] strArray = info.Split(new char[] { ',' });
                if (strArray.Length < 5) return;

                DynamicData dynamicdata = new DynamicData();
                dynamicdata.curcolorindex = (byte)NewLayoutFun.GetColorID(strArray[0]); //getColorIdx(strArray[0]);
                dynamicdata.curgroupindex = (byte)ConvertStringtoDecimal(strArray[1]);
                dynamicdata.curinterleaveindex = (byte)ConvertStringtoDecimal(strArray[2]);
                dynamicdata.row = (int)ConvertStringtoDecimal(strArray[3]);
                dynamicdata.line = (int)ConvertStringtoDecimal(strArray[4]);

                CoreInterface.AddDynamicListData(dynamicdata);
            }

            FileStream fs = new FileStream(filePath, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);
            //开始写入
            for (int i = 0; i < listBox1.Items.Count; i++)
            {
                sw.WriteLine(listBox1.GetItemText(listBox1.Items[i]));
            }
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }

        public void OnPrinterSettingChange(SPrinterSetting ss)
        {
            int colorNum = CoreInterface.GetLayoutColorNum();

            cbxColorName.Items.Clear();
            for (int i = 0; i < colorNum; i++)
            {
                int colorID = CoreInterface.GetLayoutColorID(i);
                string cmode = NewLayoutFun.GetColorName(colorID);
                cbxColorName.Items.Add(cmode);
            }

            if (cbxColorName.Items.Count > 0)
            {
            cbxColorName.SelectedIndex = 0;
            }

            if (File.Exists(filePath))
            {
                listBox1.Items.Clear();
                //this.checkBoxClose.Checked = true;
                StreamReader sr = new StreamReader(filePath, Encoding.Default);
                string info = "";
                while (!sr.EndOfStream)
                {
                    info = sr.ReadLine();

                    //check
                    if (info.Trim() == "")
                    {
                        continue;
                    }

                    string[] temp = info.Split(new char[] { ',' });
                    if (temp.Length != 5)
                    {
                        continue;
                    }

                    listBox1.Items.Add(info);
                }
                sr.Close();
            }
        }

        private void CustomCloseNozzle_Load(object sender, EventArgs e)
        {
            this.checkBoxClose.Checked = false;
            if (cbxColorName.Items.Count > 0)
                cbxColorName.SelectedIndex = 0;

            if (File.Exists(filePath))
            {
                listBox1.Items.Clear();
                this.checkBoxClose.Checked = true;
                StreamReader sr = new StreamReader(filePath, Encoding.Default);
                string info = "";
                while (!sr.EndOfStream)
                {
                    info = sr.ReadLine();
                    
                    //check
                    if (info.Trim() == "")
                    {
                        continue;
                    }

                    string[] temp = info.Split(new char[]{','});
                    if (temp.Length != 5)
                    {
                        continue;
                    }

                    listBox1.Items.Add(info);
                }
                sr.Close();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string info = listBox1.GetItemText(listBox1.SelectedItem);
            string[] strArray = info.Split(new char[]{','});
            if (strArray.Length < 5) return;

            cbxColorName.Text = strArray[0];
            numGroup.Value = ConvertStringtoDecimal(strArray[1]);
            numInterleave.Value = ConvertStringtoDecimal(strArray[2]);
            numRow.Value = ConvertStringtoDecimal(strArray[3]);
            numLine.Value = ConvertStringtoDecimal(strArray[4]);
        }

        private Decimal ConvertStringtoDecimal(string value)
        {
            Decimal result = 0;
            try
            {
                result = Convert.ToDecimal(value.Trim());
            }
            catch{}
            return result;
        }

        private byte getColorIdx(string colorName)
        {
            int info = 0;

            switch (colorName.Trim().ToUpper())
            {
                case "Y":
                    info = 0;
                    break;
                case "M":
                    info = 1;
                    break;
                case "C":
                    info = 2;
                    break;
                case "K":
                    info = 3;
                    break;
                case "S1":
                    info = 4;
                    break;
                case "S2":
                    info = 5;
                    break;
                case "S3":
                    info = 6;
                    break;
                case "S4":
                    info = 7;
                    break;
            }
            return (byte)info;
        }

        private void checkBoxClose_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.checkBoxClose.Checked == true)
            //{
            //    this.groupBox1.Enabled = true;
            //}
            //else
            //{
            //    this.groupBox1.Enabled = false;
            //}
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            string info = ResString.GetResString("ColseNozzle_ClearAll_Question");

            if (MessageBox.Show(info, ResString.GetProductName(), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                listBox1.Items.Clear();
            }
        }
    }
}
