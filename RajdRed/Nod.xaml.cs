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
<<<<<<< HEAD
        private Shape _shape = new Ellipse() { MinWidth=10, MinHeight=10, Stroke=Brushes.Black, StrokeThickness=1 };
=======
        private Shape _shape;
>>>>>>> refs/remotes/origin/MasterNod
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

        /// <summary>
        /// Nodens konstruktor om den skapas bunden till en klass
        /// </summary>
        /// <param name="k"></param>
        /// <param name="os"></param>
        /// <param name="p"></param>
        public Nod(Klass k, OnSide os, Point p)
        {
            InitializeComponent();
            _onSide = os;
            _klass = k;
<<<<<<< HEAD
            OuterGrid.Children.Add(_shape);
=======

            _shape = new Ellipse() { 
                Width = 10, 
                Height = 10, 
                Stroke = Brushes.Black, 
                StrokeThickness = 1
            };

            this.OuterGrid.Children.Add(_shape);

            setPositionWithMargin(p);

            k.OuterGrid.Children.Add(this);
        }

        /// <summary>
        /// Sätter noden på rätt position runt en klass
        /// </summary>
        /// <param name="p"></param>
        private void setPositionWithMargin(Point p)
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
>>>>>>> refs/remotes/origin/MasterNod
        }

        /// <summary>
        /// Returnerar om noden är bunden till en klass
        /// </summary>
        /// <returns></returns>
        public bool IsBindToKlass()
        {
            return (_klass != null ? true : false);
        }

        /// <summary>
        /// Returnerar om noden är bunden till en extern linje
        /// </summary>
        /// <returns></returns>
        public bool IsBindToLinje()
        {
            //return (_linje != null ? true : false);
            return false;
        }

        /// <summary>
        /// Ändrar noden till en association
        /// </summary>
        public void TurnAssociation()
        {
<<<<<<< HEAD
            _shape = new Ellipse() { MinWidth = 10, MinHeight = 10, Stroke = Brushes.Black, StrokeThickness = 1 };
=======
            _shape = new Ellipse() { MinWidth = 15, MinHeight = 15, Stroke = Brushes.Black, StrokeThickness = 1 };
>>>>>>> refs/remotes/origin/MasterNod
        }

        /// <summary>
        /// Ändrar noden till ett arv
        /// </summary>
        public void TurnInheritance()
        {
            if (_onSide == OnSide.Bottom)
            {
                _shape = new Polygon()
                {
                    Points = new PointCollection() { 
                    _p, 
                    new Point(_p.X - 7.5, _p.Y + 15), 
                    new Point(_p.X + 7.5, _p.Y + 15) 
                }
                };
            }
            
        }

        /// <summary>
        /// Ändrar noden till ett aggregat eller komposition (om fylld)
        /// </summary>
        /// <param name="filled"></param>
        public void TurnAggregation(bool filled)
        {
            Polygon newpoly = new Polygon() { Stroke=Brushes.Black, StrokeThickness = 1 };
            if (filled)
                _shape.Fill = Brushes.Black;
            newpoly.Points = new PointCollection() { 
                            _p, 
                            new Point(_p.X - 15, _p.Y), 
                            new Point(_p.X-7.5, _p.Y-7.5), 
                            new Point(_p.X-7.5, _p.Y+7.5)
            };

            _shape = newpoly;
        }

        /// <summary>
        /// Returnerar _onSide
        /// </summary>
        /// <returns></returns>
        public OnSide GetOnSide()
        {
            return _onSide;
        }
    }
}
