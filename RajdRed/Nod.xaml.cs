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
        Bottom,
        Corner
    }

    public partial class Nod : UserControl
    {
        private Klass _klass = null;
        //private Linje _linje = null;
        private Shape _shape;
        private Point _p;
        private OnSide _onSide;

        public Nod() 
        {
            InitializeComponent();
            OuterGrid.Children.Add(_shape);
        }

        /*
        public Nod(Linje l) {
        
        }
         */

        public Nod(Klass k, OnSide os, Point p)
        {
            InitializeComponent();
            _onSide = os;
            _klass = k;

            _shape = new Ellipse() { 
                Width = 10, 
                Height = 10, 
                Stroke = Brushes.Black, 
                StrokeThickness = 1
            };

            this.OuterGrid.Children.Add(_shape);
            setMargin(p);
        }

        private void setMargin(Point p)
        {
            switch (_onSide) {
                case OnSide.Left:
                    this.Margin = new Thickness(0, p.Y, 0, 0);
                    this.HorizontalAlignment = HorizontalAlignment.Left;
                    this.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case OnSide.Right:
                    this.Margin = new Thickness(0, p.Y, 0, 0);
                    this.HorizontalAlignment = HorizontalAlignment.Right;
                    this.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case OnSide.Top:
                    this.Margin = new Thickness(p.X, 0, 0, 0);
                    this.HorizontalAlignment = HorizontalAlignment.Left;
                    this.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case OnSide.Bottom:
                    this.Margin = new Thickness(p.X, 0, 0, 0);
                    this.HorizontalAlignment = HorizontalAlignment.Left;
                    this.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
            }
        }

        public bool IsBindToKlass()
        {
            return (_klass != null ? true : false);
        }

        public bool IsBindToLinje()
        {
            //return (_linje != null ? true : false);
            return false;
        }

        public void TurnEmpty()
        {
            _shape = new Ellipse() { MinWidth = 15, MinHeight = 15, Stroke = Brushes.Black, StrokeThickness = 1 };
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

        public void TurnUnit(bool filled)
        {
            Polygon newpoly = new Polygon() { Stroke=Brushes.Black, StrokeThickness = 1 };
            if (filled)
                _shape.Fill = Brushes.Black;

            switch (_onSide)
            {
                case OnSide.Left:
                    newpoly.Points = new PointCollection() { 
                            _p, 
                            new Point(_p.X - 15, _p.Y), 
                            new Point(_p.X-7.5, _p.Y-7.5), 
                            new Point(_p.X-7.5, _p.Y+7.5) 
                        };
                    break;
                case OnSide.Right:
                    newpoly.Points = new PointCollection() { 
                            _p, 
                            new Point(_p.X + 15, _p.Y), 
                            new Point(_p.X + 7.5, _p.Y - 7.5), 
                            new Point(_p.X + 7.5, _p.Y + 7.5) 
                        };
                    break;
                case OnSide.Top:
                    newpoly.Points = new PointCollection() { 
                            _p, 
                            new Point(_p.X, _p.Y - 15), 
                            new Point(_p.X - 7.5, _p.Y - 7.5), 
                            new Point(_p.X + 7.5, _p.Y - 7.5) 
                        };
                    break;
                case OnSide.Bottom:
                    newpoly.Points = new PointCollection() { 
                            _p, 
                            new Point(_p.X, _p.Y + 15), 
                            new Point(_p.X - 7.5, _p.Y + 7.5), 
                            new Point(_p.X + 7.5, _p.Y + 7.5) 
                        };
                    break;
            }

            _shape = newpoly;
        }

        public OnSide GetOnSide()
        {
            return _onSide;
        }
    }
}
