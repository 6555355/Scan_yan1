using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Dev4Arabs
{
	/// <summary>
	/// This Class Has methods to draw Main MenuItem
	/// </summary>
	public class MainMenuItemDrawing
	{
		/// <summary>
		/// The main method for drawing the main MenuItems
		/// </summary>
		public static void DrawMenuItem(System.Windows.Forms.DrawItemEventArgs e, MenuItem mi)
		{

			// Check the state of the menuItem
			if ( (e.State & DrawItemState.HotLight) == DrawItemState.HotLight ) {
				// Draw Hover rectangle
				DrawHoverRect(e, mi);
			} 
			else if ( (e.State & DrawItemState.Selected) == DrawItemState.Selected ) {
				// Draw selection rectangle
				DrawSelectionRect(e, mi);
			} else {
				// if no selection, just draw space
				Rectangle rect = new Rectangle(e.Bounds.X, 
					e.Bounds.Y, 
					e.Bounds.Width, 
					e.Bounds.Height -1);

				e.Graphics.FillRectangle(new SolidBrush(Globals.MainColor), rect);
				e.Graphics.DrawRectangle(new Pen(Globals.MainColor), rect);
			}
			
			// Create stringFormat object
			StringFormat sf = new StringFormat();

			// set the Alignment to center
			sf.LineAlignment = StringAlignment.Center;
			sf.Alignment = StringAlignment.Center;

			// Draw the text
			e.Graphics.DrawString(mi.Text, 
				Globals.menuFont, 
				new SolidBrush(Globals.TextColor), 
				e.Bounds, 
				sf);	
		}

		/// <summary>
		/// Draws the hover rectangle in case of a mouse is hovering 
		/// </summary>
		private static void DrawHoverRect(System.Windows.Forms.DrawItemEventArgs e, MenuItem mi)
		{
			// Create the hover rectangle
			Rectangle rect = new Rectangle(e.Bounds.X, 
				e.Bounds.Y + 1, 
				e.Bounds.Width, 
				e.Bounds.Height - 2);

			// Create the hover brush
			Brush b = new LinearGradientBrush(rect, 
				Color.White, 
				Globals.CheckBoxColor,
				90f, false);

			// Fill the rectangle
			e.Graphics.FillRectangle(b, rect);

			// Draw borders
			e.Graphics.DrawRectangle(new Pen(Color.Black), rect);
		}

		/// <summary>
		/// Draws a selection rectangle
		/// </summary>
		private static void DrawSelectionRect(System.Windows.Forms.DrawItemEventArgs e, MenuItem mi)
		{
			// Create the selection rectangle
			Rectangle rect = new Rectangle(e.Bounds.X, 
				e.Bounds.Y + 1, 
				e.Bounds.Width, 
				e.Bounds.Height - 2);

			// Create the selectino brush
			Brush b = new LinearGradientBrush(rect, 
				Globals.MenuBgColor, 
				Globals.MenuDarkColor2,
				90f, false);

			// fill the rectangle
			e.Graphics.FillRectangle(b, rect);

			// Draw borders
			e.Graphics.DrawRectangle(new Pen(Color.Black), rect);
		}
	}
}
