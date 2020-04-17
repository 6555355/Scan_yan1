/* 
	版权所有 2006－2007，北京博源恒芯科技有限公司。保留所有权利。
	只有被北京博源恒芯科技有限公司授权的单位才能更改抄写和传播。
	CopyRight 2006-2007, Beijing BYHX Technology Co., Ltd. All Rights reserved.
	All information contained in this file is the confindential property of Beijing BYHX Technology Co., Ltd.
	This file is distributed under license and may not be copied,
	modified or distributed except as expressly authorized by BYHX Technology Co., Ltd.
*/
using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
namespace BYHXPrinterManager
{
	/// <summary>
	/// Summary description for Interface.
	/// </summary>

	public interface IPrinterChange
	{
		void OnErrorCodeChanged(int code);
		void OnPrinterStatusChanged(JetStatusEnum status);
		void OnPrinterPropertyChange( SPrinterProperty sp);
		void OnPrinterSettingChange( SPrinterSetting ss);
		void OnPreferenceChange( UIPreference up);
		void OnSwitchPreview();
		bool LoadJobList();
		bool SaveJobList();
		AllParam GetAllParam();
		void NotifyUIParamChanged();
		void NotifyUICalibrationExit(bool bSave, bool bClose);
		void NotifyUIKeyDownAndUp(Keys keyData,bool bKeydown);
		CLockQueue GetLockQueue();
	}
	public class IAbort
	{
		private bool m_Abort;
		public bool Abort
		{
			get{return m_Abort;}
			set{m_Abort = value;}
		}
	}
	public class ControlClone
	{
		public static void NumericUpDownClone(NumericUpDown numericUpDown,NumericUpDown controlsource)
		{
			numericUpDown.AccessibleDefaultActionDescription = controlsource.AccessibleDescription;

			numericUpDown.AccessibleName = controlsource.AccessibleName;
			numericUpDown.Anchor = controlsource.Anchor;
			numericUpDown.DecimalPlaces = controlsource.DecimalPlaces;
			numericUpDown.Dock = controlsource.Dock;
			numericUpDown.Enabled = controlsource.Enabled;
			numericUpDown.Font = controlsource.Font;
			numericUpDown.ImeMode = controlsource.ImeMode;

			numericUpDown.Minimum = controlsource.Minimum;
			numericUpDown.Maximum = controlsource.Maximum;
			numericUpDown.DecimalPlaces = controlsource.DecimalPlaces;
			numericUpDown.Increment = controlsource.Increment;

			numericUpDown.RightToLeft = controlsource.RightToLeft;
			numericUpDown.Size = controlsource.Size;
			numericUpDown.TextAlign = controlsource.TextAlign;
			numericUpDown.ThousandsSeparator = controlsource.ThousandsSeparator;
			numericUpDown.UpDownAlign = controlsource.UpDownAlign;
			numericUpDown.ReadOnly = controlsource.ReadOnly;
			numericUpDown.Visible = controlsource.Visible;
		}

		public static void TextBoxClone(TextBox textBox, TextBox controlsource)
		{
			textBox.AccessibleDescription = controlsource.AccessibleDescription;
			textBox.AccessibleName = controlsource.AccessibleName;
			textBox.Anchor = controlsource.Anchor;
			textBox.AutoSize = controlsource.AutoSize;
			textBox.BackgroundImage = controlsource.BackgroundImage;
			textBox.Dock = controlsource.Dock;
			textBox.Enabled = controlsource.Enabled;
			textBox.Font = controlsource.Font;
			textBox.ImeMode = controlsource.ImeMode;
			textBox.MaxLength = controlsource.MaxLength;
			textBox.Multiline = controlsource.Multiline;
			textBox.PasswordChar = controlsource.PasswordChar;
			textBox.ReadOnly = controlsource.ReadOnly;
			textBox.RightToLeft = controlsource.RightToLeft;
			textBox.ScrollBars = controlsource.ScrollBars;
			textBox.Size = controlsource.Size;
			textBox.TextAlign = controlsource.TextAlign;
			textBox.WordWrap = controlsource.WordWrap;
		}

		public static void LabelClone(Label label, Label controlsource)
		{
			label.AccessibleDescription = controlsource.AccessibleDescription;
			label.AccessibleName = controlsource.AccessibleName;
			label.Anchor = controlsource.Anchor;
			label.AutoSize = controlsource.AutoSize;
			label.Dock = controlsource.Dock;
			label.Enabled = controlsource.Enabled;
			label.Font = controlsource.Font;
			label.Image = controlsource.Image;
			label.ImageAlign = controlsource.ImageAlign;
			label.ImageIndex = controlsource.ImageIndex;
			label.ImeMode = controlsource.ImeMode;
			label.FlatStyle = controlsource.FlatStyle;
			label.BackColor = controlsource.BackColor;
			label.Location = controlsource.Location;
			label.RightToLeft = controlsource.RightToLeft;
			label.Size = controlsource.Size;

			label.TabIndex = 0;
			label.Text = controlsource.Text;
			label.TextAlign = controlsource.TextAlign;
			label.Visible = controlsource.Visible;
		}

    
		public static void GroupBoxClone(GroupBox groupBox, GroupBox controlsource)
		{
			groupBox.AccessibleDescription = controlsource.AccessibleDescription;
			groupBox.AccessibleName = controlsource.AccessibleName;
			groupBox.Anchor = controlsource.Anchor;
			groupBox.BackgroundImage = controlsource.BackgroundImage;
			groupBox.Dock = controlsource.Dock;
			groupBox.Enabled = controlsource.Enabled;
			groupBox.Font = controlsource.Font;
			groupBox.ImeMode = controlsource.ImeMode;
			groupBox.Location = controlsource.Location;
			groupBox.RightToLeft = controlsource.RightToLeft;
			groupBox.Size = controlsource.Size;
			groupBox.TabStop = controlsource.TabStop;
		}
		public static void CheckBoxClone(CheckBox groupBox, CheckBox controlsource)
		{
			groupBox.Location = controlsource.Location;
			groupBox.Size = controlsource.Size;
			groupBox.Text = controlsource.Text;
			groupBox.FlatStyle = controlsource.FlatStyle;
			groupBox.BackColor = controlsource.BackColor;
		}

		public static void RadioButtonClone(RadioButton radio,RadioButton controlsource)
		{
			radio.AccessibleDescription = controlsource.AccessibleDescription;
			radio.AccessibleName = controlsource.AccessibleName;
			radio.Anchor = controlsource.Anchor;
			radio.Appearance = controlsource.Appearance;
			radio.BackgroundImage = controlsource.BackgroundImage;
			radio.CheckAlign = controlsource.CheckAlign;
			radio.Dock = controlsource.Dock;
			radio.FlatStyle = controlsource.FlatStyle;
			radio.BackColor = controlsource.BackColor;
			radio.Font = controlsource.Font;
			radio.Image = controlsource.Image;
			radio.ImageAlign = controlsource.ImageAlign;
			radio.ImageIndex = controlsource.ImageIndex;
			radio.ImeMode = controlsource.ImeMode;
			radio.RightToLeft = controlsource.RightToLeft;
			radio.TextAlign = controlsource.TextAlign;
			radio.Enabled = controlsource.Enabled;
		}

        public static void ButtonClone(Button label, Button controlsource)
        {
            label.AccessibleDescription = controlsource.AccessibleDescription;
            label.AccessibleName = controlsource.AccessibleName;
            label.Anchor = controlsource.Anchor;
            label.AutoSize = controlsource.AutoSize;
            label.Dock = controlsource.Dock;
            label.Enabled = controlsource.Enabled;
            label.Font = controlsource.Font;
            label.Image = controlsource.Image;
            label.ImageAlign = controlsource.ImageAlign;
            label.ImageIndex = controlsource.ImageIndex;
            label.ImeMode = controlsource.ImeMode;
            label.FlatStyle = controlsource.FlatStyle;
            label.BackColor = controlsource.BackColor;
            label.Location = controlsource.Location;
            label.RightToLeft = controlsource.RightToLeft;
            label.Size = controlsource.Size;

            label.TabIndex = 0;
            label.Text = controlsource.Text;
            label.TextAlign = controlsource.TextAlign;
            label.Visible = controlsource.Visible;
        }

	}

}