using System;
using System.Windows.Forms;

namespace BYHXPrinterManager.Preview
{
	/// <summary>
	/// MyPictureBox ��ժҪ˵����
	/// </summary>
	public class MyPictureBox:System.Windows.Forms.PictureBox
	{
		public MyPictureBox():base()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
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
