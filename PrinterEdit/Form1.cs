using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Xml;


using BYHXPrinterManager;
using PrinterStubC.Utility;

namespace PrinterEdit
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class FormPrinterEdit : System.Windows.Forms.Form
	{
		
		private System.Windows.Forms.Button m_ButtonWorkFolder;
		private System.Windows.Forms.TextBox m_TextBoxWorkFolder;
		private System.Windows.Forms.Button m_ButtonDecrypt;
		private System.Windows.Forms.Button m_ButtonEncrypt;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        private Button button1;
        private TextBox textBoxOutPut;

		public FormPrinterEdit()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
			this.Text = ResString.GetProductName();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPrinterEdit));
            this.m_ButtonWorkFolder = new System.Windows.Forms.Button();
            this.m_TextBoxWorkFolder = new System.Windows.Forms.TextBox();
            this.m_ButtonDecrypt = new System.Windows.Forms.Button();
            this.m_ButtonEncrypt = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBoxOutPut = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // m_ButtonWorkFolder
            // 
            resources.ApplyResources(this.m_ButtonWorkFolder, "m_ButtonWorkFolder");
            this.m_ButtonWorkFolder.Name = "m_ButtonWorkFolder";
            this.m_ButtonWorkFolder.Click += new System.EventHandler(this.m_ButtonWorkFolder_Click);
            // 
            // m_TextBoxWorkFolder
            // 
            resources.ApplyResources(this.m_TextBoxWorkFolder, "m_TextBoxWorkFolder");
            this.m_TextBoxWorkFolder.Name = "m_TextBoxWorkFolder";
            // 
            // m_ButtonDecrypt
            // 
            resources.ApplyResources(this.m_ButtonDecrypt, "m_ButtonDecrypt");
            this.m_ButtonDecrypt.Name = "m_ButtonDecrypt";
            this.m_ButtonDecrypt.Click += new System.EventHandler(this.m_ButtonDecrypt_Click);
            // 
            // m_ButtonEncrypt
            // 
            resources.ApplyResources(this.m_ButtonEncrypt, "m_ButtonEncrypt");
            this.m_ButtonEncrypt.Name = "m_ButtonEncrypt";
            this.m_ButtonEncrypt.Click += new System.EventHandler(this.m_ButtonEncrypt_Click);
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBoxOutPut
            // 
            resources.ApplyResources(this.textBoxOutPut, "textBoxOutPut");
            this.textBoxOutPut.Name = "textBoxOutPut";
            this.textBoxOutPut.ReadOnly = true;
            // 
            // FormPrinterEdit
            // 
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBoxOutPut);
            this.Controls.Add(this.m_ButtonWorkFolder);
            this.Controls.Add(this.m_TextBoxWorkFolder);
            this.Controls.Add(this.m_ButtonDecrypt);
            this.Controls.Add(this.m_ButtonEncrypt);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPrinterEdit";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new FormPrinterEdit());
		}

		private void m_ButtonWorkFolder_Click(object sender, System.EventArgs e)
		{
			OpenFileDialog fileDialog = new OpenFileDialog();

			fileDialog.Multiselect = false;
			fileDialog.CheckFileExists = true;
			fileDialog.DefaultExt = ".bin";
			fileDialog.Filter = "Setting Files (*.bin)|*.bin";
			//fileDialog.InitialDirectory = m_allParam.Preference.WorkingFolder;

		    if (fileDialog.ShowDialog(this) == DialogResult.OK)
		    {
		        m_TextBoxWorkFolder.Text = fileDialog.FileName;
		        textBoxOutPut.Text = Path.ChangeExtension(m_TextBoxWorkFolder.Text, ".xml");
		    }
		}

		private void m_ButtonDecrypt_Click(object sender, System.EventArgs e)
		{
			Decrypt();
		}

		private void m_ButtonEncrypt_Click(object sender, System.EventArgs e)
		{
			Encrypt();
		}
		void Decrypt()
		{
			SPrinterProperty sp;
			if(!File.Exists(m_TextBoxWorkFolder.Text))
				return;
			string srcfilename = m_TextBoxWorkFolder.Text;
			string dstfilename = Path.ChangeExtension(m_TextBoxWorkFolder.Text,".xml");
			if(LoadFromEncrypt(srcfilename,out sp))
			{
				if(SaveToDecrypt(dstfilename,sp))
                    MessageBox.Show("Decryption is successful.");
                else
                    MessageBox.Show("Decryption fails.");
            }
			else
			{
                MessageBox.Show("File failed to load.");			    
			}
		}

	    private const uint NEW_PROPERTY_VERSION = 0xDC6DD21;
        void Encrypt()
		{
			SPrinterProperty sp;
			if(!File.Exists(m_TextBoxWorkFolder.Text))
				return;
			string dstfilename = m_TextBoxWorkFolder.Text;
			string srcfilename= Path.ChangeExtension(m_TextBoxWorkFolder.Text,".xml");
			if(LoadFromDecrypt(srcfilename,out sp))
			{
                // 从此之后编辑的配置文件全部为新版本 20150526 gzw
			    sp.Version = NEW_PROPERTY_VERSION;
				if (SaveToEncrypt(dstfilename,sp))
		            MessageBox.Show("Encryption success.");
		        else
                    MessageBox.Show("Encryption failed.");
		    }
		    else
		    {
                MessageBox.Show("File failed to load.");
            }
		}
		public bool LoadFromEncrypt(string fileName,out SPrinterProperty obj)
		{
			obj = new SPrinterProperty();
			try
			{
				if(!File.Exists(fileName))
					return false;
				int length = Marshal.SizeOf(typeof(SPrinterProperty));
				FileStream stream = File.OpenRead(fileName);
                //if(stream.Length != length)
                //{
                //    stream.Close();
                //    return false;
                //}
				byte [] buffer = new byte[length];
				int size = stream.Read(buffer,0,length);
				stream.Close();
				if(size != length)
					return false;
				IntPtr ptr = Marshal.AllocHGlobal(length);
				Marshal.Copy(buffer,0,ptr,length);
				obj = (SPrinterProperty)Marshal.PtrToStructure(ptr,typeof(SPrinterProperty));
				Marshal.FreeHGlobal(ptr);
			}
			catch(Exception e)
			{
				Debug.Assert(false,e.Message);
				return false;
			}

			return true;
		}

		public bool SaveToEncrypt(string fileName, SPrinterProperty obj)
		{
			try
			{
				if(File.Exists(fileName))
				{
					FileAttributes attr = File.GetAttributes(fileName);
					if((attr&FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
					{
						attr ^= FileAttributes.ReadOnly;
						File.SetAttributes(fileName,attr);
					}
				}
				int length = Marshal.SizeOf(typeof(SPrinterProperty));
				IntPtr ptr = Marshal.AllocHGlobal(length);
				byte [] buffer = new byte[length];
				Marshal.StructureToPtr(obj,ptr,false);
				Marshal.Copy(ptr,buffer,0,length);
				Marshal.FreeHGlobal(ptr);
				FileStream stream = File.OpenWrite(fileName);
				stream.Write(buffer,0,length);
				stream.Close();
			}
			catch(Exception e)
			{
				Debug.Assert(false,e.Message);
				return false;
			}
			return true;
		}




		

		public bool LoadFromDecrypt(string fileName,out SPrinterProperty obj)
		{

			obj = new SPrinterProperty();
			try
			{
				if(!File.Exists(fileName))
					return false;
				XmlDocument doc = new XmlDocument();
				doc.Load(fileName);
				XmlElement root = doc.DocumentElement;

				//XmlElement elem_i;
				//elem_i = DNetXmlSerializer.GetFirstChildByName(root,m_TagPrinterProperty);
				obj = (SPrinterProperty)PubFunc.SystemConvertFromXml(root.OuterXml,typeof(SPrinterProperty));
			}
			catch(Exception e)
			{
				Debug.Assert(false,e.Message);
				return false;
			}
			return true;
		}

		public bool SaveToDecrypt(string fileName, SPrinterProperty obj)
		{
			try
			{
				if(File.Exists(fileName))
				{
					FileAttributes attr = File.GetAttributes(fileName);
					if((attr&FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
					{
						attr ^= FileAttributes.ReadOnly;
						File.SetAttributes(fileName,attr);
					}
				}
				XmlDocument doc = new XmlDocument();
#if false
				XmlElement root = doc.CreateElement(m_TagPrinterProperty);
				doc.AppendChild(root);
#endif
				string xml = PubFunc.SystemConvertToXml(obj,obj.GetType());
				doc.InnerXml = xml;
				doc.Save(fileName);
			}
			catch(Exception e)
			{
				Debug.Assert(false,e.Message);
				return false;
			}
			return true;
		}

        private void button1_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBoxOutPut.Text))
                Process.Start(textBoxOutPut.Text);
            else
            {
                MessageBox.Show("The specified file does not exist, please try again later .");
            }
        }
		





	}
}
