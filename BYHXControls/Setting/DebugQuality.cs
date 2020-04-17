using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BYHXPrinterManager.Setting
{
    public partial class DebugQuality : UserControl
    {
        private int m_ColorNumAndColumnNum = 0;
        private int m_ColorNum = 0;
        public int m_HorColumnNum = 1;

        private System.Windows.Forms.Label[] m_LabelVerHeadIndex;
        private System.Windows.Forms.NumericUpDown[] m_TextBoxVer;

        public DebugQuality()
        {
            InitializeComponent();
        }

        public void OnPrinterPropertyChange(SPrinterProperty sp)
        {
            m_HorColumnNum = CoreInterface.GetMaxColumnNum();
            if (m_HorColumnNum < 1) m_HorColumnNum = 1;
            m_ColorNum = CoreInterface.GetLayoutColorNum();
            m_ColorNumAndColumnNum = m_ColorNum * m_HorColumnNum;

            this.m_LabelVerHeadIndex = new Label[m_ColorNumAndColumnNum];
            this.m_TextBoxVer = new NumericUpDown[m_ColorNumAndColumnNum];
            for (int i = 0; i < m_ColorNumAndColumnNum; i++)
            {
                this.m_LabelVerHeadIndex[i] = new Label();
                this.m_TextBoxVer[i] = new NumericUpDown();
            }

            BuildVerticalGroup();

        }

        public void OnPrinterSettingChange(AllParam allParam)
        {
            for (int i = 0; i < m_ColorNumAndColumnNum; i++)
            {
                this.m_TextBoxVer[i].Value = (decimal)allParam.PrintQualityUI.VerticalOffset[i];
            }
        }

        public void OnGetPrinterSetting(ref AllParam allParam)
        {
            allParam.PrintQualityUI.VerticalOffset = new float[CoreConst.MAX_HEAD_NUM * 2];
            for (int i = 0; i < m_ColorNumAndColumnNum; i++)
            {
                allParam.PrintQualityUI.VerticalOffset[i] = (float)this.m_TextBoxVer[i].Value;
            }
        }

        private void BuildVerticalGroup()
        {
            int start_x = this.m_TextBoxVerSample.Left;
            int start_y = this.m_TextBoxVerSample.Top;
            int width_GroupOld = m_GroupBoxVer.Width;
            int width_GroupVer = start_x;
            int width_con = this.m_TextBoxVerSample.Width;
            int space_x = 70;
            for (int i = 0; i < m_ColorNumAndColumnNum; i++)
            {
                int hg = i / m_ColorNum;
                Label label = this.m_LabelVerHeadIndex[i];
                ControlClone.LabelClone(label, this.m_LabelVerSample);

                label.Location = new Point(start_x + space_x * i, this.m_LabelVerSample.Top);
                label.Width = width_con;
                int colorID = CoreInterface.GetLayoutColorID(i % m_ColorNum);
                if (m_HorColumnNum != 1)
                {
                    label.Text = hg.ToString() + " (" + NewLayoutFun.GetColorName(colorID) + ")";
                }
                else
                {
                    label.Text = " (" + NewLayoutFun.GetColorName(colorID) + ")";
                }
                label.Visible = true;
                m_GroupBoxVer.Controls.Add(this.m_LabelVerHeadIndex[i]);

                NumericUpDown textBox = this.m_TextBoxVer[i];
                ControlClone.NumericUpDownClone(textBox, this.m_TextBoxVerSample);

                textBox.Location = new Point(start_x + space_x * i, this.m_TextBoxVerSample.Top);
                textBox.Width = width_con;
                textBox.Value = 0;
                textBox.Visible = true;
                m_GroupBoxVer.Controls.Add(this.m_TextBoxVer[i]);

                width_GroupVer += space_x;
            }

            if (width_GroupOld < width_GroupVer)
                m_GroupBoxVer.Width = width_GroupVer;

            m_GroupBoxVer.ResumeLayout(false);
        }

    }
}
