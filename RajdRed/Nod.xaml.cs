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
    /// Vilken sida av en klass
    /// </summary>
    public enum OnSide
    {
        Left,
        Right,
        Top,
        Bottom,
        Corner
    }


    public struct NodTypes
    {
        private Nod _n;
        public Ellipse Assosiation;
        public Polygon Generalization;
        public Polygon Aggregation;
        public Polygon Node;

        public NodTypes(Nod n)
        {
            _n = n;
            Assosiation = new Ellipse() 
            {
                Stroke = Brushes.Black, 
                StrokeThickness = 1, 
                Fill = Brushes.Transparent
            };

            Generalization = new Polygon()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                Points = new PointCollection() { 
                        new Point(_n.Width/2, 0), 
                        new Point(_n.Width, _n.Height), 
                        new Point(0, _n.Height) 
                    }
            };

            Aggregation = new Polygon()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 1,
                Points = new PointCollection()
                {
                    new Point(_n.Width/2, 0),
                    new Point(_n.Width, _n.Height/2),
                    new Point(_n.Width/2, _n.Height),
                    new Point(0, _n.Height/2)
                }
            };

            Node = new Polygon()
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
        }
        
    }

    public partial class Nod : UserControl
    {
        public Klass Klass { get; set; }
        public Linje Linje { get; set; }
        private Shape _shape;
        private MainWindow _mainWindow;
        private OnSide _onSide;
        private Point _nodPos;
        private bool _isSelected;
        public Canvas Canvas { get; set; }
        private Nod _siblingNod = null;
        private NodTypes _nodTypes;

        /// <summary>
        /// Baskonstruktor. 
        /// </summary>
        /// <param name="c"></param>
        public Nod(MainWindow m) 
        {
            InitializeComponent();
            _nodTypes = new NodTypes(this);
            _shape = _nodTypes.Node;
            _shape.DataContext = _shape;

            _mainWindow = m;
            Canvas = m.GetCanvas();

            Canvas.SetZIndex(this, 3);

            Stackpanel.Children.Add(_shape);
        }


        /// <summary>
        /// Kopieringskonstruktor. Från fristående nod till klassbunden nod
        /// </summary>
        /// <param name="c"></param>
        /// <param name="l"></param>
        /// <param name="p"></param>
        public Nod(Nod n, bool ass) : this(n._mainWindow)
        {
            _onSide = n._onSide;
            _nodPos = n._nodPos;

            Klass = n.Klass;
            Klass._noder[Klass._noder.IndexOf(n)] = this;


            if (ass)
            {
                TurnToAssociation();
                Klass.SetNodOnKlass(this, true);
            }
            else
            {
                Klass.LooseNod(this);
            }

            SetPositionWithMargin();
        }

        /// <summary>
        /// Konstruktor om den från början är bunden till en klass
        /// </summary>
        /// <param name="k"></param>
        /// <param name="os"></param>
        /// <param name="p"></param>
        public Nod(Klass k, OnSide os, Point p) : this(k.GetMainWindow())
        {
            _onSide = os;
            Klass = k;

            PositionOfNod(p);
            SetPositionWithMargin();
        }

        public Point PositionRelativeCanvas()
        {
            if (IsBindToKlass())
            {
                if (_onSide == OnSide.Left || _onSide == OnSide.Top) {
                    return new Point(
                        Canvas.GetLeft(Klass) + (_nodPos.X * Klass.ActualWidth) + Width/2,
                        Canvas.GetTop(Klass) + (_nodPos.Y * Klass.ActualHeight) + Height/2
                        );
                }
                else if (_onSide == OnSide.Right) {
                    return new Point(
                        Canvas.GetLeft(Klass) + (_nodPos.X * Klass.ActualWidth) - Width / 2,
                        Canvas.GetTop(Klass) + (_nodPos.Y * Klass.ActualHeight) + Height / 2
                        );
                }
                else {
                    return new Point(
                        Canvas.GetLeft(Klass) + (_nodPos.X * Klass.ActualWidth) + Width / 2,
                        Canvas.GetTop(Klass) + (_nodPos.Y * Klass.ActualHeight) - Height / 2
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
            _nodPos.X = p.X / Klass.MinWidth;
            _nodPos.Y = p.Y / Klass.MinHeight;
        }

         //<summary>
         //Sätter noden på rätt position runt en klass
         //</summary>
         //<param name="p"></param>
        public void SetPositionWithMargin()
        {
            switch (_onSide) {
                case OnSide.Left:
                    this.Margin = new Thickness(0, _nodPos.Y * Klass.ActualHeight, 0, 0);
                    this.HorizontalAlignment = HorizontalAlignment.Left;
                    this.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case OnSide.Right:
                    this.Margin = new Thickness(0, _nodPos.Y * Klass.ActualHeight, 0, 0);
                    this.HorizontalAlignment = HorizontalAlignment.Right;
                    this.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case OnSide.Top:
                    this.Margin = new Thickness(_nodPos.X * Klass.ActualWidth, 0, 0, 0);
                    this.HorizontalAlignment = HorizontalAlignment.Left;
                    this.VerticalAlignment = VerticalAlignment.Top;
                    break;
                case OnSide.Bottom:
                    this.Margin = new Thickness(_nodPos.X * Klass.ActualWidth, 0, 0, 0);
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
            return (Klass != null ? true : false);
        }

        /// <summary>
        /// Returnerar om noden är bunden till en extern linje
        /// </summary>
        /// <returns></returns>
        public bool IsBindToLinje()
        {
            return (Linje != null ? true : false);
        }

        /// <summary>
        /// Ändrar noden till en association
        /// </summary>
        public void TurnToAssociation()
        {
            _shape = _nodTypes.Assosiation;
        }

        /// <summary>
        /// Ändrar noden till ett arv
        /// </summary>
        public void TurnToGeneralization()
        {
            _shape = _nodTypes.Generalization;
        }

        /// <summary>
        /// Ändrar noden till ett aggregat eller komposition (om fylld)
        /// </summary>
        /// <param name="filled"></param>
        public void TurnToAggregation(bool filled)
        {
            _shape = _nodTypes.Aggregation;

            if (filled)
            {
                _shape.Fill = Brushes.Black;
            }
        }

        public void TurnToNode()
        {
            _shape = _nodTypes.Node;
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
                Linje = new Linje(_siblingNod, this);
                _siblingNod.Linje = Linje;
                resetNodFromKlass();
                
                CaptureMouse();
                _isSelected = true;

                Canvas.Children.Add(Linje);
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

        private void Nod_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isSelected && IsMouseCaptured)
            {
                Linje.UpdatePosition(this, e.GetPosition(Canvas));
                Canvas.SetLeft(this, e.GetPosition(Canvas).X - Width/2);
                Canvas.SetTop(this, e.GetPosition(Canvas).Y - Height/2);
            }
        }

        private void Nod_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _siblingNod.Klass.NodeGrid.Visibility = Visibility.Hidden;
            _isSelected = false;
            ReleaseMouseCapture();
            _mainWindow.ShowAllNodes(false);

        }

        public void UpdateLinjePosition()
        {
            if (IsBindToLinje())
            {
                Linje.UpdatePosition(this, PositionRelativeCanvas());
            }
        }

        public void resetNodFromKlass()
        {
            TurnToAssociation();
            _onSide = 0;
            _nodPos.X = 0;
            _nodPos.Y = 0;
            this.Margin = new Thickness(0);
            Klass.LooseNodFromKlass(this);
            Klass = null;
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
                            Klass = n.Klass;
                            _nodPos = n._nodPos;
                            _onSide = n._onSide;
                            
                        }
                       
                    }
                }
            }
        }

    }
}
