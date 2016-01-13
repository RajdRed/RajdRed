using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RajdRed
{
	public static class RajdColorScheme
	{
		public static RajdColors Dark = new RajdColors() {
            KlassNameBg = Convert("#151515"),
            KlassAttributesBg = Convert("#1d1d1d"),
            KlassMethodsBg = Convert("#151515"),
            TheCanvasBg = Convert("#333"),
            MenuBotBg = Convert("#222"),
            MenuButtonBg = Convert("#191919")
		};

		public static RajdColors Light = new RajdColors()
		{
            KlassNameBg = Convert("#222931"),
            KlassAttributesBg = Convert("#323a45"),
			KlassMethodsBg = Convert("#222931"),
			TheCanvasBg = Convert("#EAEDF2"),
			MenuBotBg = Convert("#4f5b6d"),
			MenuButtonBg = Convert("#323a45")
		};

        public static Brush Convert(string colorcode)
        {
            return (Brush)new BrushConverter().ConvertFrom(colorcode);
        }
	}
}
