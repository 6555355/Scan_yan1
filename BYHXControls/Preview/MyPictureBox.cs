using System;
using System.Windows.Forms;

namespace BYHXPrinterManager.Preview
{
	/// <summary>
	/// MyPictureBox 的摘要说明。
	/// </summary>
	public class MyPictureBox:System.Windows.Forms.PictureBox
	{
		public MyPictureBox():base()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		
		protected override bool IsInputKey(System.Windows.Forms.Keys keyData)
		{
			if(keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Left || keyData == Keys.Right)
				return true;
			else
				return base.IsInputKey (keyData);
		}

		protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
		{
			if(this.Focused)
			{
				base.OnMouseDown (e);
			}
			else
			{
				this.Focus();
			}
		}
	}
}
