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

        private Nod[] _node = new Nod[2];
        private Line _line = null;
        private Klass[] _classes = new Klass[2];

        private bool _isSelected = false;

        public Linje(Nod n, Point p)
        {
            _node[0] = n;
            _node[1] = new Nod(this);
            _classes[0] = n.GetKlass();

            X1 = p.X;
            Y1 = p.Y;
            X2 = p.X+15;
            Y2 = p.Y+15;

            Stroke = Brushes.Black;
            StrokeThickness = 5;

            _classes[0].MainWindow().getCanvas().Children.Add(this);

            MouseDown += Linje_MouseDown;
            MouseMove += Linje_MouseMove;
            MouseUp += Linje_MouseUp;

        }

        void Linje_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isSelected = false;
        }

        void Linje_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isSelected)
            {
                X2 = e.GetPosition(_node[0].GetKlass().MainWindow().getCanvas()).X;
                Y2 = e.GetPosition(_node[0].GetKlass().MainWindow().getCanvas()).Y;
            }
        }

        private void Linje_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isSelected = true;
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
