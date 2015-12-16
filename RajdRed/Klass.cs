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
using System.Windows.Media.Animation;

namespace RajdRed
{
    public class Klass : Grid
    {
        private TranslateTransform tt = new TranslateTransform();
        private MainWindow _mainWindow;
        public Label ClassName = new Label() { Foreground = Brushes.White };
        public TextBlock Attributes = new TextBlock() { Background = Brushes.Transparent, Foreground = Brushes.White, Padding = new Thickness(5)};
        public TextBlock Methods = new TextBlock() { Background = Brushes.Transparent, Foreground = Brushes.White, Padding = new Thickness(5) };
        private Canvas canvas;
        private bool onField;
        public List<string> Colors = new List<string>() { "#222931", "#323a45" };

        private Klass _shapeSelected = null;
        private Point _posOfMouseOnHit;
        private Point _posOfShapeOnHit;

        public List<Nod> Nodes = new List<Nod>();

        public Klass(MainWindow w, string name)
        {
            //Attribut
            MinWidth = 100;
            MinHeight = 100;
            _mainWindow = w;
            canvas = w.getCanvas();
            onField = false;

            Grid grid = new Grid() { Cursor = Cursors.Hand };
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
                Background = (Brush)new BrushConverter().ConvertFrom(Colors[0])
            };
            Border borderAttributes = new Border()
            {
                Background = (Brush)new BrushConverter().ConvertFrom(Colors[1])
            };
            Border borderMethods = new Border()
            {
                CornerRadius = new CornerRadius(0, 0, 2, 2),
                Background = (Brush)new BrushConverter().ConvertFrom(Colors[0])
            };

            //Label
            ClassName.FontWeight = FontWeights.Bold;
            ClassName.HorizontalAlignment = HorizontalAlignment.Center;
            ClassName.Content = name;

            Grid grid_header = new Grid();
            grid_header.SetValue(Grid.RowProperty, 0);
            grid_header.Children.Add(borderHeader);
            grid_header.Children.Add(ClassName);
            grid.Children.Add(grid_header);

            Grid grid_attributes = new Grid();
            grid_attributes.SetValue(Grid.RowProperty, 1);
            grid_attributes.Children.Add(borderAttributes);
            grid_attributes.Children.Add(Attributes);
            grid.Children.Add(grid_attributes);

            Grid grid_methods = new Grid();
            grid_methods.SetValue(Grid.RowProperty, 2);
            grid_methods.Children.Add(borderMethods);
            grid_methods.Children.Add(Methods);
            grid.Children.Add(grid_methods);

            grid.Background = Brushes.Transparent;
            Children.Add(grid);

            //Events
            this.MouseDown += Klass_MouseDown;
            this.MouseMove += Klass_MouseMove;
            this.MouseUp += Klass_MouseUp;

            //Slutligen lägger till detta objektet till canvasen
            canvas.Children.Add(this);
           
        }

        private void createNod()
        {
            for (int i = 0; i < 6; ++i)
            {
                Nod n = new Nod(this, i);
                Nodes.Add(n);
                canvas.Children.Add(n);
            }

            /*Point pt = new Point();
            pt.X = Canvas.GetLeft(this);
            pt.Y = Canvas.GetTop(this);
            
            Nod n = new Nod(canvas, this);
            Canvas.SetLeft(n, pt.X-15);
            Canvas.SetTop(n, pt.Y);*/
        }

        public void Klass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                Grid g = new Grid() { Width = canvas.ActualWidth, Height = canvas.ActualHeight, Background=Brushes.Black, Opacity=0.2 };
                Canvas.SetLeft(g, 0);
                Canvas.SetTop(g, 0);

                ClassSettings cs = new ClassSettings(this, g);
                Point posOnCanvas = e.GetPosition(canvas) - _posOfMouseOnHit + _posOfShapeOnHit;
                double x = (posOnCanvas.X+ActualWidth/2)-cs.Width/2;
                double y = (posOnCanvas.Y+ActualHeight/2)-cs.Height/2;

                if (cs.Width + x > _mainWindow.ActualWidth)
                    Canvas.SetLeft(cs, x - (x + cs.Width - _mainWindow.ActualWidth));
                else if (x < 0)
                    Canvas.SetLeft(cs, x-x);
                else
                    Canvas.SetLeft(cs, x);

                if (cs.Height + y > _mainWindow.ActualHeight)
                    Canvas.SetTop(cs, y - (y + cs.Height - _mainWindow.ActualHeight));
                else if (y < 0)
                    Canvas.SetTop(cs, y-y);
                else
                    Canvas.SetTop(cs, y);

                canvas.Children.Add(g);
                canvas.Children.Add(cs);
            }
            else
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
                Delete();
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

        public void Delete()
        {
            _mainWindow.DeleteKlass(this);
        }

        public void CloseSettings(ClassSettings cs, Grid g)
        {
            canvas.Children.Remove(cs);
            canvas.Children.Remove(g);
        }

        public void Save(ClassSettings cs)
        {
            ClassName.Content = cs.ClassName.Text;
            Attributes.Text = cs.Attributes.Text;
            Methods.Text = cs.Methods.Text;
        }
    }
}