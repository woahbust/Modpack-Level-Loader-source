using System;

namespace Modpack.Utils.ModOptions
{
	public class Button
	{
		public void Click()
		{
			this.clicked = true;
		}

		public bool clicked { get; set; }

		public static implicit operator bool(Button b)
		{
			return b.clicked;
		}
	}
}
