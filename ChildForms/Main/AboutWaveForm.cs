using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Main
{
    public partial class AboutWaveForm : Form
    {
        private ArrayList m_WaveformList;
        private AllParam m_Param;
        private int m_ColorNum = 0;
        public AboutWaveForm(AllParam param)
        {
            this.m_Param = param;
            InitializeComponent();
            m_WaveformList = new ArrayList(256);
            InitContentAtUI();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DownLoadXml();
        }

        private void DownLoadXml()
        {
            FileDialog dialog = new OpenFileDialog();
            dialog.Filter = @"Job Files (*.xml)|*.xml|All Files (*.*)|*.*"; 
            dialog.ShowDialog();
            string path = dialog.FileName;
            MyXmlReader xmlReader = new MyXmlReader();
            xmlReader.ReadXml(path);
            foreach (AREA area in xmlReader.AREAsFromXml1)
            {
                if (area.type == AREA_Type.map3||area.type == AREA_Type.map4)
                {
                    byte[] sendData= xmlReader.GetWaveformAreaBuffer(area);
                    byte[] val = { 0x01, 0x1B, 0x86, 0x80, 0x1B };
                    int ret = CoreInterface.DownInkCurve(sendData, sendData.Length);
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            this.m_WaveformList.Clear();
            this.comb_Index.Items.Clear();
            this.comb_Index.Update();
            uint uLen = 64;
            byte[] buffer = new byte[uLen];
            bool bSucessed = true;
            for (int i = 1; i < 0xff; i++)
            {
                if(CoreInterface.GetEpsonEP0Cmd(0x78, buffer, ref uLen, 1, (ushort)i)==0)
                {
                    bSucessed = false;
                    break;
                }
                else
                {
                    if (buffer[2]==0)
                    {
                        break;
                    }
                    byte[] structData = new byte[Marshal.SizeOf(typeof(MyStruct))];
                    InkVoltageCurve myStruct = new InkVoltageCurve();
                    Array.Copy(buffer, 2, structData, 0, structData.Length);
                    myStruct = BytesToStruct(structData, typeof(InkVoltageCurve));
                    m_WaveformList.Add(myStruct);
                    this.cmb_WaveformIndex.Items.Add(string.Format("{0:D}", (byte)myStruct.Index));
                }
            }
            if (m_WaveformList.Count < 1)
            {
                MessageBox.Show("Get waveform faile!");
            }
        }
        public InkVoltageCurve BytesToStruct(Byte[] bytes, Type strcutType)
        {
            Int32 size = Marshal.SizeOf(strcutType);
            IntPtr buffer = Marshal.AllocHGlobal(size);
            Marshal.WriteByte(buffer,0);
            try
            {
                Marshal.Copy(bytes, 0, buffer, size);
                InkVoltageCurve s = (InkVoltageCurve)Marshal.PtrToStructure(buffer, strcutType);
                return s;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        private void comb_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (object o in m_WaveformList)
            {
                Type t = o.GetType();
                if ("InkVoltageCurve" == t.Name)
                {
                    InkVoltageCurve myStruct = o as InkVoltageCurve;
                    if (null != myStruct)
                    {
                        if (this.cmb_WaveformIndex.Text ==string.Format("{0:D}", (byte)myStruct.Index))
                        {
                            this.cmb_Ink.SelectedIndex =
                                this.cmb_Ink.FindString(Enum.GetName(typeof(INK),(INK)myStruct.Inktype));
                            this.cmb_Speed.SelectedIndex =
                                 this.cmb_Speed.FindString(Enum.GetName(typeof(SPEED),(SPEED)myStruct.Inktype));
                            this.lstbx_HeadType.Items.Clear();
                            for (int i = 0; i < myStruct.HeadList.Length; i++)
                            {
                                
                                int value = myStruct.HeadList[i];//注意
                                if (value > 0)
                                {
                                    value -= 30;
                                }
                                string type = Enum.GetName(typeof (PrinterHeadEnum), (PrinterHeadEnum) value);
                                if (value != 0 && null != type)
                                {
                                    this.lstbx_HeadType.Items.Add(type);
                                }
                            }   
                        }
                        
                    }
                }
            }
        }

        private void InitContentAtUI()
        {
            this.cmb_Ink.Items.AddRange(Enum.GetNames(typeof(INK)));   
            this.cmb_Speed.Items.AddRange(Enum.GetNames(typeof(SPEED)));
        }
        
    }
}
