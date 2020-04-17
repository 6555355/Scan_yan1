using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Dev4Arabs
{
	/// <summary>
	/// This class has the methods to draw the MenuItems
	/// </summary>
	public class MenuItemDrawing
	{

		/// <summary>
		/// The main method that will draw the menu item
		/// </summary>
		public static void DrawMenuItem(System.Windows.Forms.DrawItemEventArgs e, MenuItem mi)
		{
			// check to see if the menu item is selected
			if ( (e.State & DrawItemState.Selected) == DrawItemState.Selected ) {
				// Draw selection rectangle
				DrawSelectionRect(e, mi);	
			} else {
				// if no selection, just draw white space
				e.Graphics.FillRectangle(new SolidBrush(Globals.MenuBgColor), e.Bounds);
				// Draw the picture area
				DrawPictureArea(e, mi);
			}

			// Draw check box if the menu item is checked
			if ( (e.State & DrawItemState.Checked) == DrawItemState.Checked ) {
				DrawCheckBox(e, mi);
			}

			// Draw the menuitem text
			DrawMenuText(e, mi);

			// Draw the item's picture
			DrawItemPicture(e, mi);

		}

		/// <summary>
		/// This method draws the menu's Text
		/// </summary>
		private static void DrawMenuText(System.Windows.Forms.DrawItemEventArgs e, MenuItem mi)
		{
			Brush textBrush = new SolidBrush(Globals.TextColor);

			// Draw the menu text
			// if the menu item is a seperator
			if ( mi.Text == "-" ) {
				// draw seperator line
				e.Graphics.DrawLine(new Pen(Globals.MenuLightColor), e.Bounds.X + Globals.PIC_AREA_SIZE + 3, e.Bounds.Y + 2, e.Bounds.Width, e.Bounds.Y + 2);
			} else {
				// create StringFormat object and set the alignment to center
				StringFormat sf = new StringFormat();
				sf.LineAlignment = StringAlignment.Center;

				// create the rectangle that will hold the text
				RectangleF rect = new Rectangle(Globals.PIC_AREA_SIZE + 2, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

				string miText = mi.Text.Replace("&","");

				// check the menuitem status
				if ( mi.Enabled )
					textBrush = new SolidBrush(Globals.TextColor);	
				else
					textBrush = new SolidBrush(Globals.TextDisabledColor);	
				
				// Draw the text
				e.Graphics.DrawString(miText, Globals.menuFont, textBrush, rect, sf);

				// Draw the shortcut text
				DrawShortCutText(e, mi);
			}
		}

		/// <summary>
		/// This method draws the shortcut text for a MenuItem
		/// </summary>
		private static void DrawShortCutText(System.Windows.Forms.DrawItemEventArgs e, MenuItem mi)
		{
			// check to see if there is a short cut for this item
			if ( mi.Shortcut != Shortcut.None && mi.ShowShortcut == true)
			{
				// get the shortcut text size
				SizeF scSize = 
					e.Graphics.MeasureString(mi.Shortcut.ToString(), 
					Globals.menuFont);

				// Create the text rectangle
				Rectangle rect = 
					new Rectangle(e.Bounds.Width - Convert.ToInt32(scSize.Width) - Globals.PIC_AREA_SIZE,
					e.Bounds.Y,
					Convert.ToInt32(scSize.Width) + 5,
					e.Bounds.Height);

				// set it to right-to-left, and center it
				StringFormat sf = new StringFormat();
				sf.FormatFlags = StringFormatFlags.DirectionRightToLeft;
				sf.LineAlignment = StringAlignment.Center;

				// draw the text
				if ( mi.Enabled )
					e.Graphics.DrawString(mi.Shortcut.ToString(), 
						Globals.menuFont, 
						new SolidBrush(Globals.TextColor), 
						rect, 
						sf);
				else {	
					// if menuItem is disabled
					e.Graphics.DrawString(mi.Shortcut.ToString(), 
						Globals.menuFont, 
						new SolidBrush(Globals.TextDisabledColor), 
						rect, 
						sf);
				}
			}
		}

		/// <summary>
		/// This method draws the picturebox area
		/// </summary>
		private static void DrawPictureArea(System.Windows.Forms.DrawItemEventArgs e, MenuItem mi)
		{
			// the picture area rectangle
			Rectangle rect = new Rectangle(e.Bounds.X - 1, 
				e.Bounds.Y, 
				Globals.PIC_AREA_SIZE, 
				e.Bounds.Height);

			// Create Gradient brush, using system colors
			Brush b = new LinearGradientBrush(rect, 
				Globals.MenuDarkColor2, 
				Globals.MenuLightColor2,
				180f, 
				false);

			// Draw the rect
			e.Graphics.FillRectangle(b, rect);
		}

		/// <summary>
		/// This method Draws the picture associated with a MenuItem
		/// </summary>
		private static void DrawItemPicture(System.Windows.Forms.DrawItemEventArgs e, MenuItem mi)
		{
			// Get the Item's picture
			Image img = OfficeMenus.GetItemPicture(mi);

			// check to see if the Item has a picture, if none, Ignore
			if ( img != null ) 
			{
				// if the size exceeds the maximum picture's size, fix it
				int width = img.Width > Globals.MAX_PIC_SIZE ? Globals.MAX_PIC_SIZE : img.Width;
				int height = img.Height > Globals.MAX_PIC_SIZE ? Globals.MAX_PIC_SIZE : img.Height;
				
				// set the picture coordinates
				int x = e.Bounds.X + 2;
				int y = e.Bounds.Y + ((e.Bounds.Height - height) / 2);
				
				// create the picture rectangle
				Rectangle rect = new Rectangle(x, y, width, height);
				
				// Now check the items state, if enabled just draw the picture
				// if not enabled, make a water mark and draw it.
				if ( mi.Enabled ) {
					// draw the image
					e.Graphics.DrawImage(img, x, y, width, height);
				} else {
					// make water mark of the picture
					ColorMatrix myColorMatrix = new ColorMatrix();
					myColorMatrix.Matrix00 = 1.00f; // Red
					myColorMatrix.Matrix11 = 1.00f; // Green
					myColorMatrix.Matrix22 = 1.00f; // Blue
					myColorMatrix.Matrix33 = 1.30f; // alpha
					myColorMatrix.Matrix44 = 1.00f; // w

					// Create an ImageAttributes object and set the color matrix.
					ImageAttributes imageAttr = new ImageAttributes();
					imageAttr.SetColorMatrix(myColorMatrix);

					// draw the image
					e.Graphics.DrawImage(img,
						rect,
						0, 
						0, 
						width, 
						height, 
						GraphicsUnit.Pixel, 
						imageAttr);
				}
			}
		}

		/// <summary>
		/// This method draws the selection rectangle
		/// </summary>
		private static void DrawSelectionRect(System.Windows.Forms.DrawItemEventArgs e, MenuItem mi)
		{
			// if the item is not enabled, then do not draw the selection rect
			if ( mi.Enabled ) 
			{
				// fill selection rectangle
				e.Graphics.FillRectangle(new SolidBrush(Globals.SelectionColor), 
					e.Bounds);

				// Draw borders
				e.Graphics.DrawRectangle(new Pen(Globals.MenuDarkColor), 
					e.Bounds.X, 
					e.Bounds.Y, 
					e.Bounds.Width - 1, 
					e.Bounds.Height - 1);
			}
		}

		/// <summary>
		/// This method draws a CheckBox for a MenuItem
		/// </summary>
		private static void DrawCheckBox(System.Windows.Forms.DrawItemEventArgs e, MenuItem mi)
		{
			// Define the CheckBox size
			int cbSize = Globals.PIC_AREA_SIZE - 5;

			// set the smoothing mode to anti alias
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

			// the main rectangle
			Rectangle rect = new Rectangle(e.Bounds.X + 1, 
				e.Bounds.Y + ((e.Bounds.Height - cbSize) / 2), 
				cbSize, 
				cbSize);

			// construct the drawing pen
			Pen pen = new Pen(Color.Black,1.7f);

			// fill the rectangle
			if ( (e.State & DrawItemState.Selected) == DrawItemState.Selected )
				e.Graphics.FillRectangle(new SolidBrush(Globals.DarkCheckBoxColor), rect);
			else
				e.Graphics.FillRectangle(new SolidBrush(Globals.CheckBoxColor), rect);

			// draw borders
			e.Graphics.DrawRectangle(new Pen(Globals.MenuDarkColor), rect);
			
			// Check to see if the menuItem has a picture
			// if Yes, Do not draw the check mark; else, Draw it
			Bitmap img = OfficeMenus.GetItemPicture(mi);

			if ( img == null ) {
				// Draw the check mark
				e.Graphics.DrawLine(pen, e.Bounds.X + 7, 
					e.Bounds.Y + 10, 
					e.Bounds.X + 10, 
					e.Bounds.Y + 14);

				e.Graphics.DrawLine(pen, 
					e.Bounds.X + 10, 
					e.Bounds.Y + 14, 
					e.Bounds.X + 15, 
					e.Bounds.Y + 9);
			}
		}
	}
}
