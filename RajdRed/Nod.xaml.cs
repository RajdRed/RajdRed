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
        private MainWindow _mainWindow;
        private Klass _klass = null;
        private Linje _linje = null;
        private Nod _siblingNod = null;
        private Shape _shape = null;
        private OnSide _onSide;
        private Point _nodPos;
        private bool _isSelected;
        public Canvas Canvas { get; set; }

        public Nod(MainWindow m) 
        {
            InitializeComponent();
            _mainWindow = m;
            Canvas = m.GetCanvas();
            Canvas.SetZIndex(this, 3);
        }


        /// <summary>
        /// Konstruktor med linje
        /// </summary>
        /// <param name="c"></param>
        /// <param name="l"></param>
        /// <param name="p"></param>
        public Nod(Nod n, bool ass) : this(n._mainWindow)
        {
            _onSide = n._onSide;
            _nodPos = n._nodPos;
            _klass = n._klass;
            _klass._noder[_klass._noder.IndexOf(n)] = this;

            _klass.NodeSetGrid.Children.Add(this);
            if (ass)
            {
                TurnToAssociation();
            }
            else
            {
                _klass.LooseNod(this);
                TurnToNode();
            }
            SetPositionWithMargin();
        }

        /// <summary>
        /// Nodens konstruktor om den skapas bunden till en klass
        /// </summary>
        /// <param name="k"></param>
        /// <param name="os"></param>
        /// <param name="p"></param>
        public Nod(Klass k, OnSide os, Point p) : this(k.GetMainWindow())
        {
            _onSide = os;
            _klass = k;

            TurnToNode();
            PositionOfNod(p);
            SetPositionWithMargin();
        }

        /// <summary>
        /// Position relativt Canvas:en
        /// </summary>
        /// <returns></returns>
        public Point Position()
        {
            return new Point();
        }

        public Point PositionRelativeCanvas()
        {
            if (IsBindToKlass())
            {
                if (_onSide == OnSide.Left || _onSide == OnSide.Top) {
                    return new Point(
                        Canvas.GetLeft(_klass) + (_nodPos.X * _klass.ActualWidth) + Width/2,
                        Canvas.GetTop(_klass) + (_nodPos.Y * _klass.ActualHeight) + Height/2
                        );
                }
                else if (_onSide == OnSide.Right) {
                    return new Point(
                        Canvas.GetLeft(_klass) + (_nodPos.X * _klass.ActualWidth) - Width / 2,
                        Canvas.GetTop(_klass) + (_nodPos.Y * _klass.ActualHeight) + Height / 2
                        );
                }
                else {
                    return new Point(
                        Canvas.GetLeft(_klass) + (_nodPos.X * _klass.ActualWidth) + Width / 2,
                        Canvas.GetTop(_klass) + (_nodPos.Y * _klass.ActualHeight) - Height / 2
                        );
                }
            }
            else
            {
                return new Point(
                    Canvas.GetLeft(this),
                    Canvas.GetTop(this)
                    );
            }
        }

        public void PositionOfNod(Point p)
        {
            _nodPos.X = p.X / _klass.MinWidth;
            _nodPos.Y = p.Y / _klass.MinHeight;
        }

         //<summary>
         //Sätter noden på rätt position runt en klass
         //</summary>
         //<param name="p"></param>
        public void SetPositionWithMargin()
        {
            switch (_onSide) {
                case OnSide.Left:
                    this.Margin = new Thickness(0, _nodPos.Y * _klass.ActualHeight, 0, 0);
                    this.HorizontalAlignment = HorizontalAlignment.Left;
                    this.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case OnSide.Right:
                    this.Margin = new Thickness(0, _nodPos.Y * _klass.ActualHeight, 0, 0);
                    this.HorizontalAlignment = HorizontalAlignment.Right;
                    this.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case OnSide.Top:
                    this.Margin = new Thickness(_nodPos.X * _klass.ActualWidth, 0, 0, 0);
                    this.HorizontalAlignment = HorizontalAlignment.Left;
                    this.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case OnSide.Bottom:
                    this.Margin = new Thickness(_nodPos.X * _klass.ActualWidth, 0, 0, 0);
                    this.HorizontalAlignment = HorizontalAlignment.Left;
                    this.VerticalAlignment = VerticalAlignment.Bottom;
                    break;
            }
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
            return (_linje != null ? true : false);
        }

        /// <summary>
        /// Ändrar noden till en association
        /// </summary>
        public void TurnToAssociation()
        {
            if (_shape != null)
            {
                OuterGrid.Children.Remove(_shape);
            }
            
            _shape = new Ellipse() {
                Stroke = Brushes.Black, 
                StrokeThickness = 1, 
                Fill = Brushes.Transparent
            };
            OuterGrid.Children.Add(_shape);
        }

        /// <summary>
        /// Ändrar noden till ett arv
        /// </summary>
        public void TurnToGeneralization()
        {
            if (_onSide == OnSide.Bottom)
            {
                if (_shape != null)
                {
                    OuterGrid.Children.Remove(_shape);
                }
                _shape = new Polygon()
                {
                    Stroke = Brushes.Black,
                    StrokeThickness = 1,
                    Points = new PointCollection() { 
                    new Point(this.Width/2, 0), 
                    new Point(this.Width, this.Height), 
                    new Point(0, this.Height) 
                    }
                };
                OuterGrid.Children.Add(_shape);
            }
            
        }

        /// <summary>
        /// Ändrar noden till ett aggregat eller komposition (om fylld)
        /// </summary>
        /// <param name="filled"></param>
        public void TurnToAggregation(bool filled)
        {
            if (_shape != null)
            {
                OuterGrid.Children.Remove(_shape);
            }
            _shape = new Polygon() { 
                Stroke=Brushes.Black, 
                StrokeThickness = 1,
                Points = new PointCollection()
                {
                    new Point(this.Width/2, 0),
                    new Point(this.Width, this.Height/2),
                    new Point(this.Width/2, this.Height),
                    new Point(0, this.Height/2)
                }
            };

            if (filled)
            {
                _shape.Fill = Brushes.Black;
            }
            OuterGrid.Children.Add(_shape);

        }

        public void TurnToNode()
        {
            if (_shape != null)
            {
                OuterGrid.Children.Remove(_shape);
            }
            _shape = new Polygon()
            {
                Points = new PointCollection() {
                    new Point(2,5),
                    new Point(8,5),
                    new Point(5,5),
                    new Point(5,2),
                    new Point(5,8)
                },
                StrokeThickness = 1,
                Stroke = Brushes.Gray
            };
            OuterGrid.Children.Add(_shape);
        }

        /// <summary>
        /// Returnerar _onSide
        /// </summary>
        /// <returns></returns>
        public OnSide GetOnSide()
        {
            return _onSide;
        }

        private void Nod_MouseEnter(object sender, MouseEventArgs e)
        {
            OuterEllipse.Visibility = Visibility.Visible;
        }

        private void Nod_MouseLeave(object sender, MouseEventArgs e)
        {
            OuterEllipse.Visibility = Visibility.Hidden;
        }

        private void Nod_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!IsBindToLinje() && IsBindToKlass())
            {
                _siblingNod = new Nod(this, true);
                _linje = new Linje(_siblingNod, this);
                _siblingNod.BindLinje(_linje);

                resetNodFromKlass();
                
                CaptureMouse();
                _isSelected = true;

                Canvas.Children.Add(_linje);
            }
            else if (!IsBindToKlass() && IsBindToLinje())
            {
                CaptureMouse();
                _isSelected = true;
            } 
            else if (IsBindToKlass() && IsBindToLinje()) 
            {
                _siblingNod = new Nod(this, false);
                resetNodFromKlass();
                CaptureMouse();
                _isSelected = true;
            }
            _mainWindow.ShowAllNodes(true);
        }

        public Klass GetKlass()
        {
            return _klass;
        }

        private void Nod_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isSelected && IsMouseCaptured)
            {
                _linje.UpdatePosition(this, e.GetPosition(Canvas));
                Canvas.SetLeft(this, e.GetPosition(Canvas).X - Width/2);
                Canvas.SetTop(this, e.GetPosition(Canvas).Y - Height/2);
            }
        }

        private void Nod_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isSelected = false;
            ReleaseMouseCapture();
            _mainWindow.ShowAllNodes(false);

        }

        public void UpdateLinjePosition()
        {
            if (IsBindToLinje())
            {
                _linje.UpdatePosition(this, PositionRelativeCanvas());
            }
        }

        public void BindLinje(Linje l)
        {
            _linje = l;
        }

        public void resetNodFromKlass()
        {
            TurnToAssociation();
            _onSide = 0;
            _nodPos.X = 0;
            _nodPos.Y = 0;
            this.Margin = new Thickness(0);
            _klass.LooseNodFromKlass(this);
            _klass = null;
        }

        private void setExternNodToKlass()
        {
            Point pt = new Point(Canvas.GetLeft(this), Canvas.GetTop(this));
            foreach (var k in _mainWindow._klassList)
            {
                foreach (var n in k._noder)
                {
                    //Kollar om noden man släpper är innanför någon nod på någon klass (alltså fett många noder)
                    if (((pt.X > n.PositionRelativeCanvas().X && pt.Y > n.PositionRelativeCanvas().Y) &&
                        (pt.X < n.PositionRelativeCanvas().X + n.Width && pt.Y < n.PositionRelativeCanvas().Y + n.Height))
                        || ((pt.X + Width > n.PositionRelativeCanvas().X && pt.Y > n.PositionRelativeCanvas().Y) &&
                        (pt.X + Width < n.PositionRelativeCanvas().X + n.Width && pt.Y < n.PositionRelativeCanvas().Y + n.Height))
                        || ((pt.X > n.PositionRelativeCanvas().X && pt.Y + Height > n.PositionRelativeCanvas().Y) &&
                        (pt.X < n.PositionRelativeCanvas().X + n.Width && pt.Y + Height < n.PositionRelativeCanvas().Y + n.Height))
                        || ((pt.X + Width > n.PositionRelativeCanvas().X && pt.Y + Height > n.PositionRelativeCanvas().Y) &&
                        (pt.X + Width < n.PositionRelativeCanvas().X + n.Width && pt.Y + Height < n.PositionRelativeCanvas().Y + n.Height)))
                    {
                        if (n.IsBindToLinje() == false)
                        {
                            _klass = n._klass;
                            _nodPos = n._nodPos;
                            _onSide = n._onSide;
                            
                        }
                       
                    }
                }
            }
        }

    }
}
