using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;


namespace Dev4Arabs
{
	/// <summary>
	/// Summary description for OfficeMenus.
	/// </summary>
	[ToolboxBitmap (typeof(OfficeMenus), "Dev4Arabs.OfficeMenus.bmp")]
	public class OfficeMenus : System.ComponentModel.Component
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		static ImageList _imageList;

		// Collection that holds the pictures details
		static NameValueCollection picDetails = new NameValueCollection();

		public OfficeMenus(System.ComponentModel.IContainer container)
		{
			///
			/// Required for Windows.Forms Class Composition Designer support
			///
			container.Add(this);
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public OfficeMenus()
		{
			///
			/// Required for Windows.Forms Class Composition Designer support
			///
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion

		/// <summary>
		/// This method start the proccess of changing the
		/// menus look to that of Office 2003
		/// </summary>
		/// <param name="form"></param>
		public void Start(Form form)
		{
			try 
			{
				//************************************
				// Main menu
				//************************************
				
				// Get the main menu from the parent form
				System.Windows.Forms.MainMenu menu = form.Menu;

				// loop through all MenuItem controls, adding the event handlers
				foreach ( MenuItem mi in menu.MenuItems )
				{
					// Add MesaureItem event handler
					mi.MeasureItem += new System.Windows.Forms.MeasureItemEventHandler(mainMenuItem_MeasureItem);
					// Add DrawItem event handler
					mi.DrawItem += new System.Windows.Forms.DrawItemEventHandler(mainMenuItem_DrawItem);
					// Set the OwnerDraw property to true
					mi.OwnerDraw = true;
					
					// call InitMenuItem Method to apply changes to child menus
					InitMenuItem(mi);
				}

				//**************************************
				// Context menus
				//**************************************

				// Parent Form context menu
				ContextMenu cmenu = form.ContextMenu;
				if ( cmenu != null ) {
					InitMenuItem(cmenu);
				}

				// Now all context menus for all controls in the Form
				foreach ( Control c in form.Controls ) 
				{
					if ( c.ContextMenu != null )
						InitMenuItem(c.ContextMenu);
				}

			}
			catch {}
		}

		/// <summary>
		/// This method Ends the proccess of changing the
		/// menus look to that of Office 2003
		/// </summary>
		/// <param name="form"></param>
		public void End(Form form)
		{
			try 
			{
				//************************************
				// Main menu
				//************************************
				
				// Get the main menu from the parent form
				System.Windows.Forms.MainMenu menu = form.Menu;

				// loop through all MenuItem controls, Removing the event handlers
				foreach ( MenuItem mi in menu.MenuItems )
				{
					// Remove MesaureItem event handler
					mi.MeasureItem -= new System.Windows.Forms.MeasureItemEventHandler(mainMenuItem_MeasureItem);
					// Remove DrawItem event handler
					mi.DrawItem -= new System.Windows.Forms.DrawItemEventHandler(mainMenuItem_DrawItem);
					// Set the OwnerDraw property to false
					mi.OwnerDraw = false;
					
					// call UninitMenuItem Method to Remove changes from child menus
					UninitMenuItem(mi);
				}

				//**************************************
				// Context menus
				//**************************************

				// Parent Form context menu
				ContextMenu cmenu = form.ContextMenu;
				if ( cmenu != null ) 
				{
					UninitMenuItem(cmenu);
				}

				// Now all context menus for all controls in the Form
				foreach ( Control c in form.Controls ) 
				{
					if ( c.ContextMenu != null )
						UninitMenuItem(c.ContextMenu);
				}

			}
			catch {}
		}

		/// <summary>
		/// This method add event handlers to MesaureItem event and 
		/// DrawItem event, and changed the OwnerDraw property to true.
		/// after calling this method for a menu, the menu will look like 
		/// Office 2003 menus
		/// </summary>
		/// <param name="mi">The Menu to apply changes for</param>
		private void InitMenuItem(Menu mi)
		{
			foreach ( MenuItem m in mi.MenuItems )
			{
				// Add MesaureItem event handler
				m.MeasureItem += 
					new System.Windows.Forms.MeasureItemEventHandler(this.menuItem_MeasureItem);
				// Add DrawItem event handler
				m.DrawItem += 
					new System.Windows.Forms.DrawItemEventHandler(this.menuItem_DrawItem);
				// Set the OwnerDraw property to true
				m.OwnerDraw = true;

				// Recall the same method again to apply the changes
				// to the child menus
				InitMenuItem(m);
			}
		}

		/// <summary>
		/// This method Remove all changes to the menus
		/// </summary>
		/// <param name="mi">the menu too remove changes from</param>
		private void UninitMenuItem(Menu mi)
		{
			foreach ( MenuItem m in mi.MenuItems )
			{
				// Remove MesaureItem event handler
				m.MeasureItem -= 
					new System.Windows.Forms.MeasureItemEventHandler(this.menuItem_MeasureItem);
				// Remove DrawItem event handler
				m.DrawItem -= 
					new System.Windows.Forms.DrawItemEventHandler(this.menuItem_DrawItem);
				// Set the OwnerDraw property to false
				m.OwnerDraw = false;

				// Recall the same method again to Remove the changes
				// from the child menus
				UninitMenuItem(m);
			}
		}

		/// <summary>
		/// This is the event for mesauring the MenuItem size
		/// this will only be added for MenuItems and ContextMenus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
		{
			MenuItem mi = (MenuItem) sender;
			// if the item is a seperator
			if ( mi.Text == "-" ) {
				e.ItemHeight = 7;
			} else {
				// get the item's text size
				SizeF miSize = e.Graphics.MeasureString(mi.Text, Globals.menuFont);
				int scWidth = 0;
				// get the short cut width
				if ( mi.Shortcut != Shortcut.None ) {
					SizeF scSize = e.Graphics.MeasureString(mi.Shortcut.ToString(), Globals.menuFont);
					scWidth = Convert.ToInt32(scSize.Width);
				}
				// set the bounds
				int miHeight = Convert.ToInt32(miSize.Height) + 7;
				if (miHeight < 25) miHeight = Globals.MIN_MENU_HEIGHT;

				e.ItemHeight = miHeight;
				e.ItemWidth = Convert.ToInt32(miSize.Width) + scWidth + (Globals.PIC_AREA_SIZE * 2);

				Image img = OfficeMenus.GetItemPicture(mi);
				// check to see if the Item has a picture, if none, Ignore
				if ( img != null ) 
				{
					e.ItemHeight = Math.Max( img.Height,e.ItemHeight );//? Globals.MAX_PIC_SIZE : img.Width;
					e.ItemWidth = Math.Max(img.Width, Convert.ToInt32(miSize.Width))+ scWidth + (Globals.PIC_AREA_SIZE * 2);// ? Globals.MAX_PIC_SIZE : img.Height;
				}
			}
		}

		/// <summary>
		/// The event for drawing menuItems, this will be called for
		/// MenuItems and ContextMenus
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void menuItem_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{
			// Draw Menu Item
			MenuItemDrawing.DrawMenuItem(e, (MenuItem) sender);
		}

		/// <summary>
		/// This is the event for mesauring the MenuItem size
		/// this will only be added for Main MenuItems
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mainMenuItem_MeasureItem(object sender, System.Windows.Forms.MeasureItemEventArgs e)
		{
			MenuItem mi = (MenuItem) sender;
			SizeF miSize = e.Graphics.MeasureString(mi.Text, Globals.menuFont);
			e.ItemWidth = Convert.ToInt32(miSize.Width);
		}

		/// <summary>
		/// The event for drawing menuItems, this will only be called for
		/// Main MenuItems
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void mainMenuItem_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
		{
			// Draw the main menu item
			MainMenuItemDrawing.DrawMenuItem(e, (MenuItem) sender);
		}

		// ************************************
		// Public Methods
		// ************************************
		/// <summary>
		/// Adds a picture to a MenuItem
		/// </summary>
		/// <param name="mi">The menuItem to add picture to</param>
		/// <param name="index">The index of the Picture in the ImageList</param>
		public void AddPicture(MenuItem mi, int index)
		{
			picDetails.Add(mi.Handle.ToString(), index.ToString());
		}

		/// <summary>
		/// Gets a MenuItem's Picure
		/// </summary>
		/// <param name="mi">The MenuItem to get it's picture</param>
		/// <returns>A Bitmap object contains the picture
		/// null in case no picture for this MenuItem
		/// </returns>
		public static Bitmap GetItemPicture(MenuItem mi)
		{
			if ( _imageList == null )
				return null;

			string [] picIndex = picDetails.GetValues(mi.Handle.ToString());
			
			if ( picIndex == null )
				return null;
			else
				return (Bitmap)_imageList.Images[Convert.ToInt32(picIndex[0])];
		}

		//*************************************
		// Public Properties
		//*************************************
		/// <summary>
		/// The ImageList which will hold the MenuItems pictures
		/// </summary>
		public ImageList ImageList
		{
			get { return _imageList; }
			set { _imageList = value; }
		}

		public int MeasureItemHeight(MenuItem mi,System.Windows.Forms.MeasureItemEventArgs e)
		{
			// get the item's text size
			SizeF miSize = e.Graphics.MeasureString(mi.Text, Globals.menuFont);
			// set the bounds
			int miHeight = Convert.ToInt32(miSize.Height) + 7;
			if (miHeight < 25) 
				miHeight = Globals.MIN_MENU_HEIGHT;
			return miHeight;				
		}
	}
}
