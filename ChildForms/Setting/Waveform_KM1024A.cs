using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace BYHXPrinterManager.Setting
{
    public partial class Waveform_KM1024A : ByhxBaseChildForm
    {
        private int HeadNumPerBoard = 8;
        public Waveform_KM1024A()
        {
            InitializeComponent();
        }

        public void InitHeadNum()
        {
            comboBox_HeadBoardIndex.Items.Clear();
            if (CoreInterface.IsS_system())
            {
                USER_SET_INFORMATION info = new USER_SET_INFORMATION();
                CoreInterface.GetUserSetInfo(ref info);
                for (int i = 0; i < info.HeadBoardNum; i++)
                {
                    comboBox_HeadBoardIndex.Items.Add("Head Board" + (i + 1));
                }
            }
            else
            {
                comboBox_HeadBoardIndex.Items.Add("Head Board1");
            }
            for (int i = 0; i < HeadNumPerBoard; i++)
            {
                comboBox_HeadSelected.Items.Add("Head" + (i + 1));
            }
        }

        private void button_Open_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog
                {
                    Filter = @"waveform file|*.wav"
                };
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    textBox_FileName.Text = openFileDialog.FileName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button_Download_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox_FileName.Text))
            {
                if (comboBox_HeadSelected.SelectedIndex == -1 || comboBox_HeadBoardIndex.SelectedIndex == -1)
                {
                    MessageBox.Show(ResString.GetResString("InputEmpty"));
                }
                else
                {
                    List<byte> data = new List<byte>();
                    using (
                        FileStream fileStream = new FileStream(textBox_FileName.Text, FileMode.Open, FileAccess.Read,
                            FileShare.Read))
                    {
                        using (BinaryReader binaryReader = new BinaryReader(fileStream))
                        {
                            data.AddRange(binaryReader.ReadBytes((int)binaryReader.BaseStream.Length));
                        }
                    }
                    MessageBox.Show(DownloadWaveForm(data.ToArray())
                        ? ResString.GetResString("UISuccess_Success")
                        : ResString.GetResString("UIError_Error"));
                }
            }
            else
            {
                MessageBox.Show(ResString.GetResString("FiledoesnotExist"));
            }
        }

        private void button_DownloadAll_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox_FileName.Text))
            {
                if (comboBox_HeadSelected.Items.Count == 0 || comboBox_HeadBoardIndex.Items.Count == 0)
                {
                    MessageBox.Show(ResString.GetResString("InputEmpty"));
                }
                else
                {
                    List<byte> data = new List<byte>();
                    using (
                        FileStream fileStream = new FileStream(textBox_FileName.Text, FileMode.Open, FileAccess.Read,
                            FileShare.Read))
                    {
                        using (BinaryReader binaryReader = new BinaryReader(fileStream))
                        {
                            data.AddRange(binaryReader.ReadBytes((int)binaryReader.BaseStream.Length));
                        }
                    }
                    MessageBox.Show(DownloadWaveFormAll(data.ToArray())
                        ? ResString.GetResString("UISuccess_Success")
                        : ResString.GetResString("UIError_Error"));
                }
            }
            else
            {
                MessageBox.Show(ResString.GetResString("FiledoesnotExist"));
            }
        }

        private bool DownloadWaveForm(byte[] info)
        {
            byte index = (byte)(comboBox_HeadBoardIndex.SelectedIndex * HeadNumPerBoard + comboBox_HeadSelected.SelectedIndex);
            int nLen = 21 + info.Length;
            byte[] val = new byte[nLen];
            val[0] = 0x46;
            val[1] = 0x00;
            val[2] = index;
            val[19] = 1;
            Array.Copy(info, 0, val, 21, info.Length);
            return CoreInterface.Down382WaveForm(val, nLen, 0) != 0;
        }

        private bool DownloadWaveFormAll(byte[] info)
        {
            for (int i = 0; i < comboBox_HeadBoardIndex.Items.Count * HeadNumPerBoard; i++)
            {
                int nLen = 21 + info.Length;
                byte[] val = new byte[nLen];
                val[0] = 0x46;
                val[1] = 0x00;
                val[2] = (byte)i;
                val[19] = 1;
                Array.Copy(info, 0, val, 21, info.Length);
                if (CoreInterface.Down382WaveForm(val, nLen, 0) == 0)
                {
                    return false;
                }
                Thread.Sleep(500);
            }
            return true;
        }
    }
}
