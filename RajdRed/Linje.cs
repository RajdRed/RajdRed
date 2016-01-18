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
        public Nod StartNod { get; set; }
        public Nod EndNod { get; set; }

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

        public Linje(Nod sn, Nod en)
        {
            StartNod = sn;
            EndNod = en;

            X1 = StartNod.PositionRelativeCanvas().X;
            Y1 = StartNod.PositionRelativeCanvas().Y;
            X2 = EndNod.PositionRelativeCanvas().X;
            Y2 = EndNod.PositionRelativeCanvas().Y;

            Stroke = Brushes.Black;
            StrokeThickness = 2;

            Canvas.SetZIndex(this, 2);

            MouseEnter += Linje_MouseEnter;
            MouseLeave += Linje_MouseLeave;
        }

        void Linje_MouseLeave(object sender, MouseEventArgs e)
        {
            StartNod.OuterEllipse.Visibility = Visibility.Hidden;
            EndNod.OuterEllipse.Visibility = Visibility.Hidden;
        }

        void Linje_MouseEnter(object sender, MouseEventArgs e)
        {
            StartNod.OuterEllipse.Visibility = Visibility.Visible;
            EndNod.OuterEllipse.Visibility = Visibility.Visible;
        }

        public void UpdatePosition(Nod node, Point p)
        {
            if (node == StartNod)
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
