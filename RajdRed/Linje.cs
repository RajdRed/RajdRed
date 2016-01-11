using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;

namespace RajdRed
{
    public class Linje : FrameworkElement
    {
        Polyline polyLine = new Polyline();
        PointCollection polygonPoints = new PointCollection();
        List<Nod> nodes = new List<Nod>(); 
        MainWindow mW;


        public Linje(MainWindow w, Point first_point, Nod startNod)
        {
            mW = w;
            nodes.Add(startNod);
            polygonPoints.Add(first_point);
            MouseDown += Linje_MouseDown;
        }

        void Linje_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }
    }
}
