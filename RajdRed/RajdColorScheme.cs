using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RajdRed
{
	public static class RajdColorScheme
	{
		public static RajdColors Dark = new RajdColors() {
			KlassNameBg = "#151515",
			KlassAttributesBg = "#1d1d1d",
			KlassMethodsBg = "#151515",
			TheCanvasBg = "#333",
			MenuBotBg = "#222",
			MenuButtonBg = "#191919",
		};

		public static RajdColors Light = new RajdColors()
		{
			KlassNameBg = "#222931",
			KlassAttributesBg = "#323a45",
			KlassMethodsBg = "#222931",
			TheCanvasBg = "#EAEDF2",
			MenuBotBg = "#4f5b6d",
			MenuButtonBg = "#323a45",
		};
	}
}
