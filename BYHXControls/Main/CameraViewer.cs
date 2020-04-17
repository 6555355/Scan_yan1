using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using PrinterStubC.CInterface;

namespace BYHXPrinterManager.Main
{
    public partial class CameraViewer : UserControl
    {
        private const string LeftCameraConfigName = "LeftCamera.txt";
        private const string RightCameraConfigName = "RightCamera.txt";
        private const string LeftCameraName = "LeftCamera";
        private const string RightCameraName = "RightCamera";

        private string _leftCameraConfigPath;
        private string _rightCameraConfigPath;
        private Form _parentForm = null;

        public CameraViewer()
        {
            InitializeComponent();

            _leftCameraConfigPath = Path.Combine(Application.StartupPath, LeftCameraConfigName);
            _rightCameraConfigPath = Path.Combine(Application.StartupPath, RightCameraConfigName);
            toolStripButtonCameraClose.Enabled = false;

            bool isSupperUser = (PubFunc.GetUserPermission() == (int) (UserPermission.SupperUser));
            toolStripComboBox1.Visible = 
                toolStripButtonEndCari.Visible =
                toolStripButtonCariMode.Visible = isSupperUser;
            panelCaribrationLeft.Visible =
                panelCaribrationRight.Visible = false;
        }


        CameraSettings _cemraSettings = new CameraSettings();
        public CameraSettings Settings
        {
            get
            {
                return _cemraSettings;
            }
        }

        public CameraViewer(Form parentForm)
        {
            InitializeComponent();

            _leftCameraConfigPath = Path.Combine(Application.StartupPath, LeftCameraConfigName);
            _rightCameraConfigPath = Path.Combine(Application.StartupPath, RightCameraConfigName);
            _parentForm = parentForm;
        }

        private void CameraSettings_Load(object sender, EventArgs e)
        {
            if (PubFunc.IsInDesignMode())
                return;
            toolStripComboBox1.SelectedIndex = 0;
            LoadCemraSettings();
        }

        private void buttonCameraStart_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(LeftCameraName) && !string.IsNullOrEmpty(RightCameraName))
            {
                int isCariMode = toolStripButtonCariMode.Checked ? 1 : 0;
                if (
                    CameraCoreInterface.CameraStart(pictureBoxLeft.Handle, pictureBoxRight.Handle, pictureBoxLeft.Width,
                        pictureBoxLeft.Height, isCariMode) != 0)
                {
                    //CameraCoreInterface.ImportConfigs(_leftCameraConfigPath, LeftCameraName);
                    //CameraCoreInterface.ImportConfigs(_rightCameraConfigPath, RightCameraName);
                }
                toolStripButtonCameraClose.Enabled = true;
                toolStripButtonCameraStart.Enabled = false;
            }
            else
            {
                MessageBox.Show("IP地址不能为空!");
            }
        }

        private void buttonCameraClose_Click(object sender, EventArgs e)
        {
            CameraCoreInterface.CameraClose();
            toolStripButtonCameraClose.Enabled = false;
            toolStripButtonCameraStart.Enabled = true;
        }

        private void toolStripButtonSetting_Click(object sender, EventArgs e)
        {
            CameraViewerSettings viewerSettings = new CameraViewerSettings();
            viewerSettings.StartPosition = FormStartPosition.CenterParent;
            viewerSettings.Settings = _cemraSettings;
            viewerSettings.ShowDialog(_parentForm);
            _cemraSettings = viewerSettings.Settings;
            if (toolStripComboBox1.SelectedIndex > 0)
            {
                CameraCoreInterface.SetCameraCaribrationParam(LeftCameraName, (int)numLeftHigh.Value, (int)numLeftLow.Value, _cemraSettings.MinDotCount, _cemraSettings.MaxError);
                CameraCoreInterface.SetCameraCaribrationParam(RightCameraName, (int)numRightHigh.Value, (int)numRightLow.Value, _cemraSettings.MinDotCount, _cemraSettings.MaxError);
            }
        }

        private void numLeftLow_ValueChanged(object sender, EventArgs e)
        {
            CameraCoreInterface.SetCameraCaribrationParam(LeftCameraName, (int)numLeftHigh.Value, (int)numLeftLow.Value, _cemraSettings.MinDotCount, _cemraSettings.MaxError);
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            CameraCoreInterface.SetCameraCaribrationParam(RightCameraName, (int)numRightHigh.Value, (int)numRightLow.Value, _cemraSettings.MinDotCount, _cemraSettings.MaxError);
        }

        private void toolStripButtonStartCari_Click(object sender, EventArgs e)
        {
        }

        private void toolStripButtonEndCari_Click(object sender, EventArgs e)
        {
            int step = toolStripComboBox1.SelectedIndex;
            if (step < 3)
            {
                toolStripComboBox1.SelectedIndex++;
                step++;
            }
            if (step == 3)
            {
                toolStripComboBox1.SelectedIndex=0;
            }
            switch (step)
            {
                case 0:
                {
                    break;
                }
                case 1:
                {
                    panelCaribrationLeft.Enabled = panelCaribrationRight.Enabled = true;
                    numLeftHigh.Enabled = numRightHigh.Enabled = true;
                    numLeftLow.Enabled = numRightLow.Enabled = true;
                    CameraCoreInterface.SetCameraCaribrationStep(2);
                    CameraCoreInterface.SetCameraCaribrationParam(LeftCameraName, (int)numLeftHigh.Value, (int)numLeftLow.Value, _cemraSettings.MinDotCount, _cemraSettings.MaxError);
                    CameraCoreInterface.SetCameraCaribrationParam(RightCameraName, (int)numRightHigh.Value, (int)numRightLow.Value, _cemraSettings.MinDotCount, _cemraSettings.MaxError);
                    break;
                }
                case 2:
                {
                    numLeftHigh.Enabled = numRightHigh.Enabled = numLeftLow.Enabled = numRightLow.Enabled = false;
                    CameraCoreInterface.SetCameraCaribrationStep(3);
                    CameraCoreInterface.SetCameraCaribrationParam(LeftCameraName, (int)numLeftHigh.Value, (int)numLeftLow.Value, _cemraSettings.MinDotCount, _cemraSettings.MaxError);
                    CameraCoreInterface.SetCameraCaribrationParam(RightCameraName, (int)numRightHigh.Value, (int)numRightLow.Value, _cemraSettings.MinDotCount, _cemraSettings.MaxError);
                    break;
                }
                case 3:
                {
                    CameraCoreInterface.SetCameraCaribrationStep(0);
                    panelCaribrationLeft.Enabled = panelCaribrationRight.Enabled = false;
                    toolStripButtonCariMode.Checked = false;
                    toolStripButtonEndCari.Enabled = false;
                    break;
                }
            }
        }

        private void toolStripButtonCariMode_CheckedChanged(object sender, EventArgs e)
        {
            panelCaribrationLeft.Visible = panelCaribrationRight.Visible = toolStripButtonCariMode.Checked;
            toolStripButtonEndCari.Enabled = toolStripButtonCariMode.Checked;
        }

        public void UpdateDistinguishCnt(int cnt,int hitCnt)
        {
            toolStripDistinguishCnt.Text = string.Format("识别次数[{1}/{0}]", cnt + 1, hitCnt);
        }

        public CameraSettings LoadCemraSettings()
        {
            string path = System.IO.Path.Combine(Application.StartupPath, "CameraSettings.xml");
            if (File.Exists(path))
            {
                TextReader xmlrReader = new StreamReader(path, Encoding.Default);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(CameraSettings));
                _cemraSettings = (CameraSettings)xmlSerializer.Deserialize(xmlrReader);
                xmlrReader.Close();
            }
            return _cemraSettings;
        }

        public void SaveCemraSettings()
        {
            string path = System.IO.Path.Combine(Application.StartupPath, "CameraSettings.xml");
            XmlWriter xmlWriter = new XmlTextWriter(path, Encoding.Default);
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(CameraSettings));
            xmlSerializer.Serialize(xmlWriter, _cemraSettings);
            xmlWriter.Flush();
            xmlWriter.Close();
        }
    }
    [Serializable]
    public struct CameraSettings
    {
        /// <summary>
        /// 左相机位置,inch
        /// </summary>
        public float LeftCameraPosX;

        /// <summary>
        /// 左相机位置,inch
        /// </summary>
        public float LeftCameraPosY;

        /// <summary>
        /// 右相机位置,inch
        /// </summary>
        public float RightCameraPosX;
        /// <summary>
        /// 右相机位置,inch
        /// </summary>
        public float RightCameraPosY;

        /// <summary>
        /// 相机识别延迟周期,单位是相机拍摄周期
        /// </summary>
        public int DistinguishDelayCycle;

        /// <summary>
        /// 单个轮廓包括的最少点数
        /// </summary>
        public int MinDotCount;

        /// <summary>
        /// 单个轮廓内所有点到圆心距离允许的最大平均误差
        /// </summary>
        public double MaxError; 
    }
}
