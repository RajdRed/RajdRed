using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows;

namespace RajdRed
{
    public class Klass : Grid
    {
        private TranslateTransform tt = new TranslateTransform();
        private MainWindow MainWindow;
        private Label header;
        private Canvas canvas;
        private bool onField;
        private Point[] posPoint = new Point[8] { new Point(20, 97.5), new Point(65, 97.5), 
                                                  new Point(97.5, 65), new Point(97.5, 20), 
                                                  new Point(20, -12.5), new Point(65, -12.5),
                                                  new Point(-12.5, 20), new Point(-12.5, 65)};

        private Klass _shapeSelected = null;
        private Point _posOfMouseOnHit;
        private Point _posOfShapeOnHit;
        public List<Nod> Noder = new List<Nod>(); 

        public Klass(MainWindow w, string name)
        {
            //Attribut
            MinWidth = 100;
            MinHeight = 100;
            MainWindow = w;
            canvas = MainWindow.getCanvas();
            onField = false;

            Grid grid = new Grid();
            grid.RenderTransform = tt;
            for (int i = 0; i < 3; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            grid.RowDefinitions[0].Height = new GridLength(20, GridUnitType.Star);
            grid.RowDefinitions[1].Height = new GridLength(40, GridUnitType.Star);
            grid.RowDefinitions[2].Height = new GridLength(40, GridUnitType.Star);

            Border borderHeader = new Border()
            {
                CornerRadius = new CornerRadius(2, 2, 0, 0),
                Background = (Brush)new BrushConverter().ConvertFrom("#404d5c")
            };
            Border borderAttributes = new Border()
            {
                Background = (Brush)new BrushConverter().ConvertFrom("#768ca5")
            };
            Border borderMethods = new Border()
            {
                CornerRadius = new CornerRadius(0, 0, 2, 2),
                Background = (Brush)new BrushConverter().ConvertFrom("#404d5c")
            };

            //Label
            header = new Label() { Foreground = Brushes.White };
            header.FontWeight = FontWeights.Bold;
            header.HorizontalAlignment = HorizontalAlignment.Center;
            header.Content = name;

            Grid grid_header = new Grid();
            grid_header.SetValue(Grid.RowProperty, 0);
            grid_header.Children.Add(borderHeader);
            grid_header.Children.Add(header);
            grid.Children.Add(grid_header);

            Grid grid_attributes = new Grid();
            grid_attributes.SetValue(Grid.RowProperty, 1);
            grid_attributes.Children.Add(borderAttributes);
            grid.Children.Add(grid_attributes);

            Grid grid_methods = new Grid();
            grid_methods.SetValue(Grid.RowProperty, 2);
            grid_methods.Children.Add(borderMethods);
            grid.Children.Add(grid_methods);

            grid.Background = Brushes.Transparent;
            Children.Add(grid);

            //Events
            this.MouseDown += Klass_MouseDown;
            this.MouseMove += Klass_MouseMove;
            this.MouseUp += Klass_MouseUp;
            this.SizeChanged += Klass_SizeChanged;

            //Slutligen lägger till detta objektet till canvasen
            canvas.Children.Add(this);
            
           
        }

        void Klass_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double ah = this.ActualHeight;
            double aw = this.ActualWidth;
          
            posPoint[0] = new Point((aw - (aw / 10 * 8)), ah - 2.5);
            posPoint[1] = new Point((aw - (aw / 10 * 3.5)), ah - 2.5);
            posPoint[2] = new Point((aw - 2.5), (ah - (ah / 10 * 3.5)));
            posPoint[3] = new Point((aw - 2.5), (ah - (ah / 10 * 8)));
            posPoint[4] = new Point((aw - (aw / 10 * 3.5)), -12.5);
            posPoint[5] = new Point((aw - (aw / 10 * 8)), -12.5);
            posPoint[6] = new Point(-12.5, (ah - (ah / 10 * 8)));
            posPoint[7] = new Point(-12.5, (ah - (ah / 10 * 3.5)));
            
        }

        private void createNod()
        {
            
            for (int i = 0; i < 8; ++i)
            {
                Nod n = new Nod(this);
                Point pt = new Point();
                pt.X = Canvas.GetLeft(this);
                pt.Y = Canvas.GetTop(this);

                Canvas.SetLeft(n, pt.X + posPoint[i].X);
                Canvas.SetTop(n, pt.Y + posPoint[i].Y);
                Noder.Add(n);
                canvas.Children.Add(n);
            }
        }

        private void changePosOfNod()
        {
            int i = 0;
            foreach (Nod n in Noder)
            {
                canvas.Children.Remove(n);
                canvas.Children.Add(n);
                Point pt = new Point();
                pt.X = Canvas.GetLeft(this);
                pt.Y = Canvas.GetTop(this);

                Canvas.SetLeft(n, pt.X + posPoint[i].X);
                Canvas.SetTop(n, pt.Y + posPoint[i].Y);
                i++;
            }
            
        }

        public void Klass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CaptureMouse();


            Point pt = e.GetPosition(canvas);

            _shapeSelected = this;
            canvas.Children.Remove(_shapeSelected);
            canvas.Children.Add(_shapeSelected);
            _posOfMouseOnHit = pt;
            _posOfShapeOnHit.X = Canvas.GetLeft(_shapeSelected);
            _posOfShapeOnHit.Y = Canvas.GetTop(_shapeSelected);

        }

        public void Klass_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseCaptured && _shapeSelected != null)
            {
                Point pt = e.GetPosition(canvas);
                Canvas.SetLeft(_shapeSelected, (pt.X - _posOfMouseOnHit.X) + _posOfShapeOnHit.X);
                Canvas.SetTop(_shapeSelected, (pt.Y - _posOfMouseOnHit.Y) + _posOfShapeOnHit.Y);

                Point posOnCanvas = pt - _posOfMouseOnHit + _posOfShapeOnHit;

                if (onField && posOnCanvas.Y <= 100)
                    Canvas.SetTop(_shapeSelected, 100.1);

                if (posOnCanvas.X <= 0)
                    Canvas.SetLeft(_shapeSelected, 0.1);
                if (onField)
                {
                    changePosOfNod();
                }
                
            }
        }

        private void Klass_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();

            if (_shapeSelected == null)
            {
                return;
            }
               

            Point pt = e.GetPosition(canvas);
            Point posOnCanvas = pt - _posOfMouseOnHit + _posOfShapeOnHit;

            if (posOnCanvas.Y <= 100 && !onField)
            {
                MainWindow.DeleteKlass(this);
            }
            else
            {
                if (!onField)
                {
                    createNod();
                }
                onField = true;
            }

            
            _shapeSelected = null;

        }
    }
}