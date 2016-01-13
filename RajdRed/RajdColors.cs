using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RajdRed
{
	public class RajdColors
	{
		public string KlassNameBg = "";
		public string KlassAttributesBg = "";
		public string KlassMethodsBg = "";
		public string TheCanvasBg = "";
		public string MenuBotBg = "";
		public string MenuButtonBg = "";
		public string ClassSettingsButtons = "";

		public RajdColors() {
 			
		}

		public RajdColors(RajdColors r)
		{
			KlassNameBg = r.KlassNameBg;
			KlassAttributesBg = r.KlassAttributesBg;
			KlassMethodsBg = r.KlassMethodsBg;
			TheCanvasBg = r.TheCanvasBg;
			MenuBotBg = r.MenuBotBg;
			MenuButtonBg = r.MenuButtonBg;
			ClassSettingsButtons = r.ClassSettingsButtons;
		}
	}
}
