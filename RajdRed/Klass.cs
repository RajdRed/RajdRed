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

        private Klass _shapeSelected = null;
        private Point _posOfMouseOnHit;
        private Point _posOfShapeOnHit;

        public Klass(MainWindow w, string name)
        {
            //Attribut
            Width = 100;
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

            //Slutligen lägger till detta objektet till canvasen
            canvas.Children.Add(this);
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
            }
        }

        private void Klass_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();

            if (_shapeSelected == null)
                return;

            Point pt = e.GetPosition(canvas);
            Point posOnCanvas = pt - _posOfMouseOnHit + _posOfShapeOnHit;

            if (posOnCanvas.Y <= 100 && !onField)
                MainWindow.DeleteKlass(this);

            else onField = true;

            _shapeSelected = null;
        }
    }
}