using System;
using System.Drawing;

namespace Dev4Arabs
{
	/// <summary>
	/// Define the global values for colors and fonts
	/// </summary>
	public class Globals
	{
		public static int PIC_AREA_SIZE = 24;//img.width + 8
		public static int MIN_MENU_HEIGHT = 22;
		public static Font menuFont = System.Windows.Forms.SystemInformation.MenuFont; 
		public static Color CheckBoxColor = Color.FromArgb(255, 192, 111);
		public static Color DarkCheckBoxColor = Color.FromArgb(254, 128, 62);
		public static Color SelectionColor = Color.FromArgb(255,238,194);
		public static Color TextColor = Color.FromKnownColor(KnownColor.MenuText);
		public static Color TextDisabledColor = Color.FromKnownColor(KnownColor.GrayText);
		public static Color MenuBgColor = Color.White;
		public static Color MainColor = Color.FromKnownColor(KnownColor.Control);
		public static Color MenuDarkColor = Color.FromKnownColor(KnownColor.ActiveCaption);
		public static Color MenuDarkColor2 = Color.FromArgb(0x65,0x93,0xb7);//Color.FromArgb(110, Color.FromKnownColor(KnownColor.ActiveCaption));
		public static Color MenuLightColor = Color.FromKnownColor(KnownColor.InactiveCaption);
		public static Color MenuLightColor2 = Color.FromArgb(0x48,0x74,0x97);// Color.FromArgb(50, Color.FromKnownColor(KnownColor.InactiveCaption));
		public const int MAX_PIC_SIZE = 128;
	}
}
