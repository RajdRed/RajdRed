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

        public Nod StartNode { get; set; }
        public Nod EndNode { get; set; }

        private bool _isSelected = false;

        public Linje(Nod n)
        {
            StartNode = n;
            EndNode = new Nod(n.Canvas, this, new Point(StartNode.PositionRelativeCanvas().X + 20, StartNode.PositionRelativeCanvas().Y + 20));

            X1 = StartNode.PositionRelativeCanvas().X;
            Y1 = StartNode.PositionRelativeCanvas().Y;
            X2 = StartNode.PositionRelativeCanvas().X+20;
            Y2 = StartNode.PositionRelativeCanvas().Y+20;

            Stroke = Brushes.Black;
            StrokeThickness = 2;
        }

        public void UpdatePosition(Nod node, Point p)
        {
            if (node == StartNode)
            {
                X1 = p.X;
                Y1 = p.Y;
            }
            else
            {
                X2 = p.X;
                Y2 = p.Y;
            }
            InvalidateVisual();
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
