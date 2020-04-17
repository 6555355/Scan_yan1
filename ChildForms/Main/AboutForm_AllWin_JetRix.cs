/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Globalization;
using System.Threading;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;
using ICSharpCode.SharpZipLib.Core;
using PrinterStubC.Utility;
using SaveFileDialog = System.Windows.Forms.SaveFileDialog;
using ICSharpCode.SharpZipLib.Zip;
using PrinterStubC.Common;

namespace BYHXPrinterManager.Main
{
	/// <summary>
	/// Summary description for AboutForm.
	/// </summary>
	public class AboutForm_AllWin_JetRix : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button m_ButtonOK;
		private System.Windows.Forms.Button m_ButtonCopyInfo;
		private System.Windows.Forms.Button m_ButtonHelp;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.PictureBox m_PictureBoxLogo;
		private System.Windows.Forms.Label m_LabelCopyRight;
		private System.Windows.Forms.RichTextBox m_LabelVersion;
		private DividerPanel.DividerPanel dividerPanel1;
        private PictureBox _qaPass;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public AboutForm_AllWin_JetRix()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
#if !LIYUUSB
            m_LabelVersion.Text = GetPanelVersion() + GetBoardVersion() +
				                  GetPrintArea() + GetTimerInfo() + GetLangInfo();
#endif
            this.Text = ResString.GetProductName();
            //string iconpath = Application.StartupPath;
            //iconpath += "\\setup\\app.ico";
            //if (File.Exists(iconpath))
            //{
            //    m_PictureBoxLogo.Image = Image.FromFile(iconpath);
            //}
		    SetQaPass();
		}

        public AboutForm_AllWin_JetRix(string vtext)
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //

            //if (vtext.Trim()==string.Empty||vtext.Trim()== null)
            //    m_LabelVersion.Text =
            //                    GetPanelVersion() + GetBoardVersion() +
            //                    GetPrintArea() + GetTimerInfo() + GetLangInfo();
            //else
            //    m_LabelVersion.Text = vtext;
            m_LabelVersion.Text = GetPanelVersion() + GetBoardVersion() +
                                  GetPrintArea() + GetTimerInfo() + GetLangInfo();

#if LIYUUSB
			string iconpath= Application.StartupPath;
			iconpath += "\\setup\\app.png";
			if(File.Exists(iconpath))
			{
				this.m_PictureBoxLogo.Image = Image.FromFile(iconpath,false);// icon.ToBitmap();
			}

			m_ButtonHelp.Visible = false;
            m_LabelCopyRight.Text = ResString.GetResString("JHFCopyRightString");
            m_LabelCopyRight.Visible = false;
            this.m_LabelVersion.BorderStyle = BorderStyle.Fixed3D;
            //this.panel1.BackgroundImageLayout = ImageLayout.Stretch;
            this.panel1.BackgroundImage = null;
            this.panel1.BorderStyle = BorderStyle.None;
            this.m_ButtonCopyInfo.Visible = false;
#endif
            this.Text = ResString.GetProductName() + vtext;
            //string iconpath = Application.StartupPath;
            //iconpath += "\\setup\\app.ico";
            //if (File.Exists(iconpath))
            //{
            //    m_PictureBoxLogo.Image = Image.FromFile(iconpath);
            //}
            SetQaPass();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm_AllWin_JetRix));
            this.panel1 = new System.Windows.Forms.Panel();
            this.m_PictureBoxLogo = new System.Windows.Forms.PictureBox();
            this.m_LabelCopyRight = new System.Windows.Forms.Label();
            this.m_LabelVersion = new System.Windows.Forms.RichTextBox();
            this.dividerPanel1 = new DividerPanel.DividerPanel();
            this.m_ButtonCopyInfo = new System.Windows.Forms.Button();
            this.m_ButtonHelp = new System.Windows.Forms.Button();
            this.m_ButtonOK = new System.Windows.Forms.Button();
            this._qaPass = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBoxLogo)).BeginInit();
            this.dividerPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._qaPass)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.m_PictureBoxLogo);
            this.panel1.Name = "panel1";
            // 
            // m_PictureBoxLogo
            // 
            resources.ApplyResources(this.m_PictureBoxLogo, "m_PictureBoxLogo");
            this.m_PictureBoxLogo.Name = "m_PictureBoxLogo";
            this.m_PictureBoxLogo.TabStop = false;
            // 
            // m_LabelCopyRight
            // 
            this.m_LabelCopyRight.FlatStyle = System.Windows.Forms.FlatStyle.System;
            resources.ApplyResources(this.m_LabelCopyRight, "m_LabelCopyRight");
            this.m_LabelCopyRight.Name = "m_LabelCopyRight";
            // 
            // m_LabelVersion
            // 
            this.m_LabelVersion.BackColor = System.Drawing.SystemColors.Control;
            resources.ApplyResources(this.m_LabelVersion, "m_LabelVersion");
            this.m_LabelVersion.Name = "m_LabelVersion";
            this.m_LabelVersion.ReadOnly = true;
            // 
            // dividerPanel1
            // 
            this.dividerPanel1.AllowDrop = true;
            this.dividerPanel1.BorderSide = System.Windows.Forms.Border3DSide.Top;
            this.dividerPanel1.Controls.Add(this.m_ButtonCopyInfo);
            this.dividerPanel1.Controls.Add(this.m_ButtonHelp);
            this.dividerPanel1.Controls.Add(this.m_ButtonOK);
            resources.ApplyResources(this.dividerPanel1, "dividerPanel1");
            this.dividerPanel1.Name = "dividerPanel1";
            // 
            // m_ButtonCopyInfo
            // 
            resources.ApplyResources(this.m_ButtonCopyInfo, "m_ButtonCopyInfo");
            this.m_ButtonCopyInfo.Name = "m_ButtonCopyInfo";
            this.m_ButtonCopyInfo.Click += new System.EventHandler(this.m_ButtonCopyInfo_Click);
            // 
            // m_ButtonHelp
            // 
            resources.ApplyResources(this.m_ButtonHelp, "m_ButtonHelp");
            this.m_ButtonHelp.Name = "m_ButtonHelp";
            this.m_ButtonHelp.Click += new System.EventHandler(this.m_ButtonHelp_Click);
            // 
            // m_ButtonOK
            // 
            this.m_ButtonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.m_ButtonOK, "m_ButtonOK");
            this.m_ButtonOK.Name = "m_ButtonOK";
            // 
            // _qaPass
            // 
            resources.ApplyResources(this._qaPass, "_qaPass");
            this._qaPass.Name = "_qaPass";
            this._qaPass.TabStop = false;
            // 
            // AboutForm_AllWin_JetRix
            // 
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this._qaPass);
            this.Controls.Add(this.dividerPanel1);
            this.Controls.Add(this.m_LabelVersion);
            this.Controls.Add(this.m_LabelCopyRight);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutForm_AllWin_JetRix";
            this.ShowInTaskbar = false;
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.m_PictureBoxLogo)).EndInit();
            this.dividerPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._qaPass)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		
		private string VersionToString(uint nVersion)
		{   
			
			SFWVersion fwv= new SFWVersion(nVersion);
			string sVersion = //fwv.m_nHWVersion.ToString()+"."+
				    fwv.m_nMainVersion.ToString()
					+ "."+fwv.m_nSubVersion.ToString()
					+ "."+fwv.m_nBuildVersion.ToString()
                    + "." + fwv.m_nHWVersion.ToString();
			return sVersion;
		}
		private string GetPrintArea()
		{
			string ret ="";
#if !LIYUUSB
			double area = 0;
			if( CoreInterface.GetPrintArea(ref area) != 0)
			{
				double m2 = area* 0.0254f* 0.0254f;
				ret +=  "Area:"+ m2.ToString("F6") + "m2\n";
			}
#endif
			return ret;
		}
		private string GetBoardVersion()
		{
			SBoardInfo sBoardInfo = new SBoardInfo();
			if( CoreInterface.GetBoardInfo(0,ref sBoardInfo) != 0)
			{
			    //FW Version Get
                string fwversion = string.Empty;
			    int nMaxNum = (sBoardInfo.m_nPkgVersion & 0xFF00) >> 8;
			    for (int nIndex = 1; nIndex <= nMaxNum; nIndex++)
			    {
			        if (nIndex == 1)
			        {
			            byte[] bytefwversion = new byte[16];
			            if (EpsonLCD.GetFWVersionInfo(ref bytefwversion))
			            {
			                int name_term = 4;
			                while (bytefwversion[name_term] != 0) name_term++;
			                fwversion = "FW version:" + (uint) BitConverter.ToInt32(bytefwversion, 0) + " " +
			                            System.Text.Encoding.ASCII.GetString(bytefwversion, 4, name_term - 4) + " " + "\n";
			            }
			        }
			    }
                string mbversion = "MB version:" + VersionToString(sBoardInfo.m_nBoradVersion)+" " + sBoardInfo.sProduceDateTime + " "+sBoardInfo.m_nBoardManufatureID.ToString("X4") + sBoardInfo.m_nBoardProductID.ToString("X4")+"\n";
				string mtversion = "MT version:" + VersionToString(sBoardInfo.m_nMTBoradVersion)+" " + sBoardInfo.sMTProduceDateTime +" " +"\n";
				string hbversion = "HB version:" + VersionToString(sBoardInfo.m_nHBBoardVersion)+" " + sBoardInfo.sReserveProduceDateTime +" " +"\n";
				string idversion = "ID :" + sBoardInfo.m_nBoardSerialNum.ToString() +" " +"\n";
                return fwversion + mbversion + mtversion + hbversion + idversion;
			}
			else
				return "";
		}
		private string GetTimerInfo()
		{
#if LIYUUSB
			int nLimitTime= 0; 
			int nDuration = 0;
            int nLang = 0;
			if( CoreInterface.GetPasswdInfo(ref nLimitTime,ref nDuration,ref nLang) != 0)
			{
				string Limit = nLimitTime.ToString() + "(Hours)";
				if(nLimitTime == 0)
					Limit = "Permanent";
				string mbversion = 
					   "Limit time:"   + Limit  + "\n"
					 + "Elapsed time:" + nDuration.ToString() + "(Hours)" + "\n"
                +DoWithLangInfo(nLang);
				return mbversion;
			}
			else
                return "";
#else
			SPwdInfo pwdinfo = new SPwdInfo();
			string ret = "";
			if( CoreInterface.GetPWDInfo(ref pwdinfo) != 0)
			{
				string Limit = pwdinfo.nLimitTime.ToString() + "(Hours)";
				if(pwdinfo.nLimitTime == 0)
					Limit = "Permanent";
				string LimitInk = pwdinfo.nLimitInk[0].ToString() + "(L)";
				if(pwdinfo.nLimitInk[0] == 0)
					LimitInk = "Not Limit Ink";

				//string usedInk = 0;
				string mbversion = 
					"Limit time:"   + Limit  + "\n"
					+ "Elapsed time:" + pwdinfo.nDuration.ToString() + "(Hours)" + "\n"
					+DoWithLangInfo(pwdinfo.nLang)
					+ "Limit Ink:" + LimitInk  + "\n";

				SPrinterProperty PrinterProperty=new SPrinterProperty();
				CoreInterface.GetSPrinterProperty(ref PrinterProperty);
                int colornum = CoreConst.OLD_MAX_COLOR_NUM;//PrinterProperty.GetRealColorNum();
                for (int i = 0; i < colornum && i < pwdinfo.nDurationInk.Length; i++)
				{
				    ColorEnum_Short ink = ColorEnum_Short.N;
                    if(Enum.IsDefined(typeof(ColorEnum_Short),PrinterProperty.eColorOrder[i]))
				    {
				        ink = (ColorEnum_Short) PrinterProperty.eColorOrder[i];
				    }
                    mbversion += string.Format("Ink{0}[",i+1)+ink.ToString() + "] Printed Ink:" 
						+ pwdinfo.nDurationInk[i].ToString("0.0") + "(L)" + "\n";
                }
				ret += mbversion;
			}
#if UV_LIMIT
			SPwdInfo_UV pwdinfo_uv = new SPwdInfo_UV();
			if( CoreInterface.GetPWDInfo_UV(ref pwdinfo_uv) != 0)
			{
				string mbversion = ""; 
					mbversion+=" Left UV: " 
					+"Limit:"+ pwdinfo_uv.nLimitUV[0].ToString() + "(Hours)" 
					+"Duration:"+ (pwdinfo_uv.nDurationUV[0]/3600f).ToString("F3") + "(Hours)" + "\n";
					mbversion+=" Right UV: " 
					+"Limit:"+ pwdinfo_uv.nLimitUV[1].ToString() + "(Hours)"
                    + "Duration:" + (pwdinfo_uv.nDurationUV[1]/3600f).ToString("F3") + "(Hours)" + "\n";
				ret += mbversion;
			}
#endif
			return  ret;
#endif
		}
        private string DoWithLangInfo(int nLimitTime)
        {
            string lang = "";
            ///
            try
            {
                int lcid = 0;
                switch (nLimitTime)
                {
                    case 0:
                        lcid = 0x0004;
                        break;
                    case 1:
                        lcid = 0x7c04;
                        break;
                    case 3:
                        lcid = 0x0409;
                        break;
                }
                if (lcid != 0)
                {
                    CultureInfo cInfo = new CultureInfo(lcid);
                    if (Thread.CurrentThread.CurrentUICulture.LCID == 0x0409)
                        lang = cInfo.EnglishName;
                    else
                        lang = cInfo.NativeName;
                }
                else
                    lang = "Unkown:" + nLimitTime.ToString();

            }
            catch (Exception)
            {
                lang = "Unkown:" + nLimitTime.ToString();
            }
            string mbversion =
                "Language:" + lang + "\n";
            return mbversion;
        }
       
		private string GetLangInfo()
		{
#if false//!LIYUUSB
			int nLimitTime= 0; 
			if( CoreInterface.GetLangInfo(ref nLimitTime) != 0)
			{
               return DoWithLangInfo(nLimitTime);
			}
			else
#endif
            return "";
		}
		private string GetPanelVersion()
		{
            string prefix = "SW version:";
            string ModifyTime = PubFunc.GetCompileTime().ToString("MM/dd/yyyy HHmm");

			System.Version ver = Assembly.GetEntryAssembly().GetName().Version;
			string appVersion =  ver.Major.ToString() + "."+ ver.Minor.ToString() + "."
				//+ ver.Build.ToString() + "."
				+ ver.Revision.ToString() + "  ";
		    string exeBitLen = string.Empty;
            if (IntPtr.Size == 4)
            {
                exeBitLen = " 32bit";
            }
            else if (IntPtr.Size == 8)
            {
                exeBitLen = " 64bit";
            }
            else
            {
                //未来肯定有
            }
			return prefix + appVersion + ModifyTime + exeBitLen+"\n";
		}
		private void m_ButtonHelp_Click(object sender, System.EventArgs e)
		{
//			Process process = Process.GetCurrentProcess();
//			DirectoryInfo dirInfo = Directory.GetParent(process.MainModule.FileName);
//			return dirInfo.FullName;
			string helpFileNutrual = Application.StartupPath;
			string helpFile = helpFileNutrual + 
				Path.DirectorySeparatorChar +
				Thread.CurrentThread.CurrentUICulture.Name + 
				Path.DirectorySeparatorChar + 
				"Help.pdf";
			helpFileNutrual += Path.DirectorySeparatorChar + "Manual.pdf";
			

			if(File.Exists(helpFile))
            {
                Process.Start(helpFile, ".pdf"); 
                //if(!StartReadFileProcess(helpFile,".pdf"))
                //{
                //    string msgInfo = ResString.GetEnumDisplayName(typeof(UIError),UIError.NoAcrobat);
                //    MessageBox.Show(msgInfo,ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Information);
                //    return;
                //}
			}
			else if(File.Exists(helpFileNutrual))
            {
                Process.Start(helpFileNutrual, ".pdf"); 
                //if(!StartReadFileProcess(helpFileNutrual,".pdf"))
                //{
                //    string msgInfo = ResString.GetEnumDisplayName(typeof(UIError),UIError.NoAcrobat);
                //    MessageBox.Show(msgInfo,ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Information);
                //    return;
                //}
			}
			else
			{
				string msgInfo = ResString.GetEnumDisplayName(typeof(UIError),UIError.NoHelpFile);
				MessageBox.Show(msgInfo,ResString.GetProductName(),MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}
			return ;	
		}
		private RegistryKey GetRegistryKey(RegistryKey regKey,string [] names)
		{
			if(regKey == null || names == null || names.Length == 0)
				return null;

			RegistryKey resultKey = regKey.OpenSubKey(names[0]);
			if(resultKey == null)
				return null;

			for(int i=1;i<names.Length;i++)
			{
				resultKey = resultKey.OpenSubKey(names[i]);
				if(resultKey == null)
					break;
			}

			return resultKey;
		}

		private string RemoveChars(string fileName)
		{
			if(fileName == null)
				return null;
			int index = 0;
			while(index >= 0)
			{
				index = fileName.IndexOf('"',index);
				if(index >= 0)
					fileName = fileName.Remove(index,1);
			}
			index = fileName.IndexOf("%1",0);
			if(index >= 0)
			{
				fileName = fileName.Substring(0,index);
			}

			return fileName;
		}
		private bool StartReadFileProcess(string fileName,string fileType)
		{
			RegistryKey regKey = Registry.LocalMachine;
			regKey = GetRegistryKey(regKey,new string[]{"SOFTWARE","Classes"});
			RegistryKey procKey = GetRegistryKey(regKey,new string[]{fileType});
			if(procKey != null)
			{
				string name = (string)procKey.GetValue("");
				if(name != null)
				{
					RegistryKey reader = GetRegistryKey(regKey,new string[]{name,"shell","open","command"});
					if(reader != null)
					{
						string procFile = RemoveChars((string)reader.GetValue(""));
						if(procFile != null)
						{
							if(File.Exists(procFile))
							{
								Process pp = new Process();
								pp.StartInfo.Arguments = string.Format("\"{0}\"",fileName);
								pp.StartInfo.FileName = procFile;
								pp.Start();
								return true;
							}
							else
							{
								return false;
							}
						}
					}
				}
			}
			return false;
		}

		private int SaveVersionInfo(string sFileName)
		{
			int ret = 0;
            FileStream fs = null; ;
			try 
			{
                //只保存关于窗口中的信息
                //fs = new FileStream(sFileName, FileMode.Create);
                //if(fs != null && m_LabelVersion.Text != null && m_LabelVersion.Text.Length != 0)
                //{
                //    fs.Write(System.Text.Encoding.ASCII.GetBytes(m_LabelVersion.Text), 0, m_LabelVersion.Text.Length);
                //    ret = 1;
                //}

                //修改于2018-5-10 压缩保存相关信息
                using (FileStream fsOut = File.Create(sFileName))
                {
                    using (ZipOutputStream zipStream = new ZipOutputStream(fsOut))
                    {
                        bool bPowerOn = CoreInterface.GetBoardStatus() != JetStatusEnum.PowerOff;
                        HEAD_BOARD_TYPE headBoardType = (HEAD_BOARD_TYPE)CoreInterface.get_HeadBoardType(bPowerOn);
                        string info = m_LabelVersion.Text + 
                            "HEAD_BOARD_TYPE: " + headBoardType + "\n" +
                            "BOARD_SYSTEM: " + (CoreInterface.IsS_system() ? "S" : "A+" + "\n"+
                            "PRINT_HEAD_TYPE: " + Text.Replace(ResString.GetProductName(), "") + "\n");


                        byte[] aboutStr = Encoding.Default.GetBytes(info);
                        ZipEntry aboutEntry = new ZipEntry("About.txt") {DateTime = DateTime.Now};
                        zipStream.PutNextEntry(aboutEntry);
                        zipStream.Write(aboutStr, 0, aboutStr.Length);
                        zipStream.CloseEntry();

                        string printfile = Path.Combine(Application.StartupPath, "Print.log");
                        Zip(printfile, zipStream);

                        string logfile = Path.Combine(Application.StartupPath, "log.txt");
                        Zip(logfile, zipStream);

                        string usersettingfile = Path.Combine(Application.StartupPath, "UserSetting.ini");
                        Zip(usersettingfile, zipStream);

                        if (GlobalSetting.Instance.VendorProduct != null && GlobalSetting.Instance.VendorProduct.Length >= 8)
                        {
                            string folderName = Path.Combine(Application.StartupPath,
                                GlobalSetting.Instance.VendorProduct.Substring(0, 4),
                                GlobalSetting.Instance.VendorProduct.Substring(4, 4));
                            CompressFolder(folderName, zipStream);

                            string factorywritefile = Application.StartupPath +
                                 Path.DirectorySeparatorChar + "PrinterProductList_"
                                 + GlobalSetting.Instance.VendorProduct.Substring(4, 4) + ".xml";
                            Zip(factorywritefile, zipStream);

                        }



                        zipStream.IsStreamOwner = false;
                        zipStream.Finish();
                        zipStream.Close();
                        ret = 1;
                    }
                }
			}
			catch{}
			finally 
			{
				if (fs != null) 
				{
					fs.Close();
				}
			}
			return ret;
		}
        private void CompressFolder(string path, ZipOutputStream zipStream)
        {
            if (!Directory.Exists(path))
                return;
            string[] files = Directory.GetFiles(path);

            foreach (string filename in files)
            {

                FileInfo fi = new FileInfo(filename);

                string entryName = Path.Combine(GlobalSetting.Instance.VendorProduct.Substring(0, 4),
                                GlobalSetting.Instance.VendorProduct.Substring(4, 4),
                                fi.Name);

                ZipEntry newEntry = new ZipEntry(entryName) {DateTime = fi.LastWriteTime};
                zipStream.PutNextEntry(newEntry);
                byte[] buffer = new byte[4096];
                using (FileStream fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    StreamUtils.Copy(fs, zipStream, buffer);
                }
                zipStream.CloseEntry();
            }
        }
        private void Zip(string filename, ZipOutputStream zipStream)
	    {
            if (File.Exists(filename))
            {
                byte[] buffer = new byte[4096];
                FileInfo fi = new FileInfo(filename);
                ZipEntry entry = new ZipEntry(fi.Name)
                {
                    DateTime = fi.LastWriteTime,
                    //Size = fi.Length
                };
                zipStream.PutNextEntry(entry);
                using (FileStream fs = new FileStream(fi.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    StreamUtils.Copy(fs, zipStream, buffer);
                }
                zipStream.CloseEntry();
            }
	    }


		private void m_ButtonCopyInfo_Click(object sender, System.EventArgs e)
		{
			SaveFileDialog fileDialog = new SaveFileDialog();
			
			fileDialog.OverwritePrompt = true;
			fileDialog.DefaultExt = ".zip";
            fileDialog.Filter = @"Text Files (*.zip)|*.zip";//ResString.GetEnumDisplayName(typeof(FileFilter),FileFilter.Txt);
			fileDialog.InitialDirectory = Application.StartupPath;

			if(fileDialog.ShowDialog(this) == DialogResult.OK)
			{
			    if (SaveVersionInfo(fileDialog.FileName) == 0)
			    {
			        MessageBox.Show(this, ResString.GetEnumDisplayName(typeof(UIError), UIError.SaveAboutFail),
			            ResString.GetProductName());
			    }
			    else
                {
                    MessageBox.Show(this, ResString.GetEnumDisplayName(typeof(UISuccess), UISuccess.SaveAboutSuccess),
                        ResString.GetProductName());
			    }
			}

        }

        public string CacheVersionText()
        {
            bool allsucces = true;
            string mVersionText = string.Empty;
            string panelversion = GetPanelVersion();
            string bv = GetBoardVersion();
            string timeinfo = GetTimerInfo();
            //string langinfo = GetLangInfo();
            if (
                panelversion.Trim()==string.Empty
				||panelversion.Trim()== null
				||bv.Trim()==string.Empty
				||bv.Trim()== null
				||timeinfo.Trim()==string.Empty
				||timeinfo.Trim()== null
                //|| string.IsNullOrEmpty(langinfo.Trim())
                )
                allsucces = false;
            if (allsucces)
                mVersionText =
                     GetPanelVersion() + GetBoardVersion()
                    + GetTimerInfo();
                   // + GetLangInfo();
            else
                mVersionText = string.Empty;
            return mVersionText;
		}

	    private void SetQaPass()
	    {
            string namePath = Application.StartupPath + "\\setup\\Vender.xml";
            _qaPass.Visible = Authentication.HasAuthenticationed(namePath, PubFunc.GetCompileTime());
	    }
    }
}
