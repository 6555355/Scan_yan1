using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using PrinterStubC.CInterface;
using PrinterStubC.Utility;

namespace BYHXPrinterManager.Main
{
    public partial class CameraViewerSettings : Form
    {
        private const string LeftCameraConfigName = "LeftCamera.txt";
        private const string RightCameraConfigName = "RightCamera.txt";
        private const string LeftCameraName = "LeftCamera";
        private const string RightCameraName = "RightCamera";
        private string _leftCameraConfigPath;
        private string _rightCameraConfigPath;
        public CameraViewerSettings()
        {
            InitializeComponent();
            _leftCameraConfigPath = Path.Combine(Application.StartupPath, LeftCameraConfigName);
            _rightCameraConfigPath = Path.Combine(Application.StartupPath, RightCameraConfigName);
        }
        CameraSettings _cemraSettings = new CameraSettings();

        public CameraSettings Settings
        {
            get { return _cemraSettings; }

            set { _cemraSettings = value; }
        }

        private const string Spliter = "	";
        private string ReadCameraConfigValueFromFile(string filepath, string configName)
        {
            string ret = null;
            if (File.Exists(filepath))
            {
                StreamReader txtreader = new StreamReader(filepath, Encoding.Default, true);
                try
                {
                    string line = string.Empty;
                    line = txtreader.ReadLine();
                    while (!txtreader.EndOfStream)
                    {
                        if (!string.IsNullOrEmpty(line.Trim()))
                        {
                            string[] paras = line.Split(new string[] { Spliter }, StringSplitOptions.RemoveEmptyEntries);
                            if (paras.Length > 1 && paras[0].ToLower() == configName.ToLower())
                            {
                                ret = paras[1];
                                break;
                            }
                        }
                        line = txtreader.ReadLine();
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (txtreader != null)
                        txtreader.Close();
                }
            }
            return ret;
        }

        private void WriteCameraConfigValueToFile(string filepath, string configName, string configvalue)
        {
            if (File.Exists(filepath))
            {
                List<string> allLine = new List<string>();
                StreamReader txtreader = new StreamReader(filepath, Encoding.Default, true);
                try
                {
                    string line = string.Empty;
                    line = txtreader.ReadLine();
                    allLine.Add(line);
                    while (!txtreader.EndOfStream)
                    {
                        line = txtreader.ReadLine();
                        allLine.Add(line);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (txtreader != null)
                        txtreader.Close();
                }


                TextWriter txtwWriter = new StreamWriter(filepath, false);
                try
                {
                    string line = string.Empty;
                    for (int i = 0; i < allLine.Count; i++)
                    {
                        line = allLine[i];
                        if (!string.IsNullOrEmpty(line.Trim()))
                        {
                            string[] paras = line.Split(new string[] { Spliter }, StringSplitOptions.RemoveEmptyEntries);
                            if (paras.Length > 1 && paras[0].ToLower() == configName.ToLower())
                            {
                                line = string.Format("{0}{1}{2}", paras[0], Spliter, configvalue);
                            }
                        }
                        txtwWriter.WriteLine(line);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    if (txtwWriter != null)
                    {
                        txtwWriter.Flush();
                        txtwWriter.Close();
                    }
                }
            }
        }

        private void buttonApplyLeftCameraConfig_Click(object sender, EventArgs e)
        {
            string leftExposureTime = numleftExposureTime.Value.ToString();
            CameraCoreInterface.SetCameraParam(LeftCameraName, GX_FEATURE_ID.GX_FLOAT_EXPOSURE_TIME, (double)numleftExposureTime.Value);
            WriteCameraConfigValueToFile(_leftCameraConfigPath, "ExposureTime", leftExposureTime);
        }

        private void buttonApplyRightCameraConfig_Click(object sender, EventArgs e)
        {
            CameraCoreInterface.SetCameraParam(RightCameraName, GX_FEATURE_ID.GX_FLOAT_EXPOSURE_TIME, (double)numrightExposureTime.Value);
            WriteCameraConfigValueToFile(_rightCameraConfigPath, "ExposureTime", numrightExposureTime.Value.ToString());
        }

        private void LoadCameraBindXml()
        {
            string path = Path.Combine(Application.StartupPath, "camera_bind.xml");
            if (File.Exists(path))
            {
                SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
                doc.Load(path);
                XmlNodeList nodes = doc.SelectNodes("//Camera/_");
                for (int i = 0; i < nodes.Count; i++)
                {
                    var selectSingleNode = nodes[i].SelectSingleNode("Name");
                    if (selectSingleNode != null && selectSingleNode.InnerText == "LeftCamera")
                    {
                        var ip = nodes[i].SelectSingleNode("IPAddress");
                        for (int j = 0; j < comboBoxLeftIp.Items.Count; j++)
                        {
                            if (ip.InnerText.Contains(comboBoxLeftIp.Items[j].ToString()))
                            {
                                comboBoxLeftIp.SelectedIndex = j;
                                break;
                            }
                        }
                    }
                    if (selectSingleNode != null && selectSingleNode.InnerText == "RightCamera")
                    {
                        var ip = nodes[i].SelectSingleNode("IPAddress");
                        for (int j = 0; j < comboBoxRightIp.Items.Count; j++)
                        {
                            if (ip.InnerText.Contains(comboBoxRightIp.Items[j].ToString()))
                            {
                                comboBoxRightIp.SelectedIndex = j;
                                break;
                            }
                        }
                    }
                }
            }

        }

        private void SaveCameraBindXml()
        {
            string path = Path.Combine(Application.StartupPath, "camera_bind.xml");
            SelfcheckXmlDocument doc = new SelfcheckXmlDocument();
            if (File.Exists(path))
            {
                doc.Load(path);
                XmlNodeList nodes = doc.SelectNodes("//Camera/_");
                for (int i = 0; i < nodes.Count; i++)
                {
                    var selectSingleNode = nodes[i].SelectSingleNode("Name");
                    if (selectSingleNode != null && selectSingleNode.InnerText == "LeftCamera")
                    {
                        var ip = nodes[i].SelectSingleNode("IPAddress");
                        ip.InnerText = "\"" + comboBoxLeftIp.SelectedItem.ToString() + "\"";
                    }
                    if (selectSingleNode != null && selectSingleNode.InnerText == "RightCamera")
                    {
                        var ip = nodes[i].SelectSingleNode("IPAddress");
                        ip.InnerText = "\"" + comboBoxRightIp.SelectedItem.ToString() + "\"";
                    }
                }
            }
            else
            {
                XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "", "");
                doc.AppendChild(declaration);
                XmlElement root = doc.CreateElement("opencv_storage");
                XmlNode calibrationData = doc.CreateElement("calibrationData");
                calibrationData.InnerText = DateTime.Now.ToLongDateString();
                root.AppendChild(calibrationData);
                XmlNode camera = doc.CreateElement("Camera");
                root.AppendChild(camera);

                XmlNode left = doc.CreateElement("_");
                XmlNode leftName = doc.CreateElement("Name");
                leftName.InnerText = "LeftCamera";
                left.AppendChild(leftName);
                XmlNode leftIpAddress = doc.CreateElement("IPAddress");
                leftIpAddress.InnerText = comboBoxLeftIp.SelectedItem.ToString();
                left.AppendChild(leftIpAddress);
                camera.AppendChild(left);

                XmlNode right = doc.CreateElement("_");
                XmlNode rightName = doc.CreateElement("Name");
                rightName.InnerText = "RightCamera";
                right.AppendChild(rightName);
                XmlNode rightIpAddress = doc.CreateElement("IPAddress");
                rightIpAddress.InnerText = comboBoxRightIp.SelectedItem.ToString();
                right.AppendChild(rightIpAddress);
                camera.AppendChild(right);

                doc.AppendChild(root);
                doc.Save(path);
                XmlNodeList nodes = doc.SelectNodes("//Camera/_");
                for (int i = 0; i < nodes.Count; i++)
                {
                    var selectSingleNode = nodes[i].SelectSingleNode("Name");
                    if (selectSingleNode != null && selectSingleNode.InnerText == "LeftCamera")
                    {
                        var ip = nodes[i].SelectSingleNode("IPAddress");
                        ip.InnerText = "\"" + comboBoxLeftIp.SelectedItem.ToString() + "\"";
                    }
                    if (selectSingleNode != null && selectSingleNode.InnerText == "RightCamera")
                    {
                        var ip = nodes[i].SelectSingleNode("IPAddress");
                        ip.InnerText = "\"" + comboBoxRightIp.SelectedItem.ToString() + "\"";
                    }
                }
            }
            doc.Save(path);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            comboBoxLeftIp.Enabled = comboBoxRightIp.Enabled = buttonApplyCameraBind.Enabled = true;
        }

        private void buttonApplyCameraBind_Click(object sender, EventArgs e)
        {
            if (comboBoxLeftIp.SelectedIndex == comboBoxLeftIp.SelectedIndex)
                SaveCameraBindXml();
            comboBoxLeftIp.Enabled = comboBoxRightIp.Enabled = buttonApplyCameraBind.Enabled = false;
            button1.Enabled = true;
        }


        private void CameraViewerSettings_Load(object sender, EventArgs e)
        {
            string leftExposureTime = ReadCameraConfigValueFromFile(_leftCameraConfigPath, "ExposureTime");
            int exposureTime = 0;
            int.TryParse(leftExposureTime, out exposureTime);
            numleftExposureTime.Value = exposureTime;

            string rightExposureTime = ReadCameraConfigValueFromFile(_rightCameraConfigPath, "ExposureTime");
            int.TryParse(rightExposureTime, out exposureTime);
            numrightExposureTime.Value = exposureTime;

            //const int ipbufLen = 32;
            //byte[] camerainfoBuf = new byte[ipbufLen * 8];
            //int deviceNum = CameraCoreInterface.EnumCamera(camerainfoBuf);
            //comboBoxLeftIp.Items.Clear();
            //comboBoxRightIp.Items.Clear();
            //for (int i = 0; i < deviceNum; i++)
            //{
            //    string iptext = System.Text.ASCIIEncoding.Default.GetString(camerainfoBuf.Skip(i *ipbufLen).Take(ipbufLen).ToArray());
            //    comboBoxLeftIp.Items.Add(iptext);
            //    comboBoxRightIp.Items.Add(iptext);
            //}
            LoadCameraBindXml();

            UpdateCameraSettings();
        }

        private void buttonApplyCameraInstallSettings_Click(object sender, EventArgs e)
        {
            ApplyCameraSettings();
        }
        UILengthUnit _uiLengthUnit = UILengthUnit.Centimeter;
        private void UpdateCameraSettings()
        {
            numDelayCycleNum.Value = _cemraSettings.DistinguishDelayCycle;
            numLeftCameraPosX.Value = (decimal)UIPreference.ToDisplayLength(_uiLengthUnit, _cemraSettings.LeftCameraPosX);
            numRightCameraPosX.Value = (decimal)UIPreference.ToDisplayLength(_uiLengthUnit, _cemraSettings.RightCameraPosX);
            numLeftCameraPosY.Value = (decimal)UIPreference.ToDisplayLength(_uiLengthUnit, _cemraSettings.LeftCameraPosY);
            numRightCameraPosY.Value = (decimal)UIPreference.ToDisplayLength(_uiLengthUnit, _cemraSettings.RightCameraPosY);
            numMinDotCnt.Value = _cemraSettings.MinDotCount;
            numMaxError.Value = (decimal) _cemraSettings.MaxError;
        }

        private void ApplyCameraSettings()
        {
            _cemraSettings.DistinguishDelayCycle = (int) numDelayCycleNum.Value;
            _cemraSettings.LeftCameraPosX = UIPreference.ToInchLength(_uiLengthUnit, (float)numLeftCameraPosX.Value);
            _cemraSettings.RightCameraPosX = UIPreference.ToInchLength(_uiLengthUnit, (float)numRightCameraPosX.Value);
            _cemraSettings.LeftCameraPosY = UIPreference.ToInchLength(_uiLengthUnit, (float)numLeftCameraPosY.Value);
            _cemraSettings.RightCameraPosY = UIPreference.ToInchLength(_uiLengthUnit, (float)numRightCameraPosY.Value);
            _cemraSettings.MinDotCount = (int)numMinDotCnt.Value;
            _cemraSettings.MaxError = (double) numMaxError.Value;
        }
    }
}
