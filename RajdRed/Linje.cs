using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RajdRed
{
    public class Linje : Shape
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }
        public double X2 { get; set; }
        public double Y2 { get; set; }

        private Nod _startNode = new Nod();
        private Nod _endNode = new Nod();

        private bool _isSelected = false;

        public Linje(Nod n)
        {
            _startNode = n;
            _endNode = new Nod(this);

            X1 = _startNode.Position().X;
            Y1 = _startNode.Position().Y;
            X2 = _endNode.Position().X;
            Y2 = _endNode.Position().Y;

            Stroke = Brushes.Black;
            StrokeThickness = 5;
        }

        protected override Geometry DefiningGeometry
        {
            get
            {
                LineGeometry line = new LineGeometry(
                   new Point(X1, Y1), 
                    new Point(X2, Y2)
                );

                return line;
            }
        }
    }
}
