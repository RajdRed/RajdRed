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
        public Nod StartNode { get; set; }
        public Nod EndNode { get; set; }

        private LineGeometry line = new LineGeometry();
        private Point start = new Point(0, 0);
        private Point end = new Point(0, 0);

        public double X1
        { set { start.X = value; } }
        public double Y1
        { set { start.Y = value; } }
        public double X2
        { set { end.X = value; } }
        public double Y2
        { set { end.Y = value; } }

        protected override Geometry DefiningGeometry
        {
            get
            {
                line.StartPoint = start;
                line.EndPoint = end;

                return line;
            }
        }

        public Linje(Nod n)
        {
            StartNode = n;
            EndNode = new Nod(n.Canvas, this, new Point(StartNode.PositionRelativeCanvas().X + 20-n.Width/2, StartNode.PositionRelativeCanvas().Y + 20-n.Height/2));

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
    }
}
