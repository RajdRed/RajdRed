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
    public class Linje 
    {
        Polyline polyLine = new Polyline();
        PointCollection polygonPoints = new PointCollection();
        //List<Point> points = new List<Point>(); 
        MainWindow mW;

        public Linje(MainWindow w, Point first_point)
        {
            mW = w;
            polygonPoints.Add(first_point);
        }
    }
}
