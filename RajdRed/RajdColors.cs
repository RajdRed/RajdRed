using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace RajdRed
{
	public class RajdColors
	{
		public Brush KlassNameBg;
        public Brush KlassAttributesBg;
        public Brush KlassMethodsBg;
        public Brush TheCanvasBg;
        public Brush MenuBotBg;
        public Brush MenuButtonBg;

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
		}
	}
}
