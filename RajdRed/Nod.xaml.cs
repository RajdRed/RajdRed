using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RajdRed
{
    /// <summary>
    /// Interaction logic for Nod.xaml
    /// </summary>
    /// 
    public enum OnSide
    {
        Left,
        Right,
        Top,
        Bottom
    }

    public partial class Nod : UserControl
    {
        private Klass _klass = null;
        //private Linje _linje = null;
        private Shape _shape = new Ellipse() { MinWidth=15, MinHeight=15, Stroke=Brushes.Black, StrokeThickness=1 };
        private int _numberOfBonds = 0;
        private Point _p;
        private OnSide _onSide;

        public Nod() 
        {
            InitializeComponent();
            OuterGrid.Children.Add(_shape);
        }

        public Nod(Klass k)
        {
            InitializeComponent();
            _klass = k;
            _numberOfBonds++;
        }

        public int NumberOfBonds()
        {
            return _numberOfBonds;
        }

        public Klass IsBindToKlass()
        {
            return _klass;
        }

        public void IsBindToLinje()
        {
            //return (_linje != null ? true : false);
        }

        public void TurnEmpty()
        {
            _shape = null;
        }

        public void TurnInheritance()
        {
            _shape = new Polygon() { 
                Points = new PointCollection() { 
                    _p, 
                    new Point(_p.X - 7.5, _p.Y + 15), 
                    new Point(_p.X + 7.5, _p.Y + 15) 
                } 
            };
        }

        public void TurnUnit()
        {
            switch (_onSide)
            {
                case OnSide.Left:
                    _shape = new Polyline() { 
                        Points = new PointCollection() { 
                            _p, 
                            new Point(_p.X - 15, _p.Y), 
                            new Point(_p.X-7.5, _p.Y-7.5), 
                            new Point(_p.X-7.5, _p.Y+7.5) 
                        } 
                    };
                    break;
                case OnSide.Right:
                    _shape = new Polyline()
                    {
                        Points = new PointCollection() { 
                            _p, 
                            new Point(_p.X + 15, _p.Y), 
                            new Point(_p.X + 7.5, _p.Y - 7.5), 
                            new Point(_p.X + 7.5, _p.Y + 7.5) 
                        }
                    };
                    break;
                case OnSide.Top:
                    _shape = new Polyline()
                    {
                        Points = new PointCollection() { 
                            _p, 
                            new Point(_p.X, _p.Y - 15), 
                            new Point(_p.X - 7.5, _p.Y - 7.5), 
                            new Point(_p.X + 7.5, _p.Y - 7.5) 
                        }
                    };
                    break;
                case OnSide.Bottom:
                    _shape = new Polyline()
                    {
                        Points = new PointCollection() { 
                            _p, 
                            new Point(_p.X, _p.Y + 15), 
                            new Point(_p.X - 7.5, _p.Y + 7.5), 
                            new Point(_p.X + 7.5, _p.Y + 7.5) 
                        }
                    };
                    break;
            }
        }

        public Point getPosition() {
            return _p;
        }
    }
}
