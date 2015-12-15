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
        private Point m_start;
        private Vector m_startOffset;
        private Canvas canvas;

        public Klass(MainWindow w, string name)
        {
            //Attribut
            Width = 100;
            MinHeight = 100;
            MainWindow = w;
            canvas = MainWindow.getCanvas();

            Grid grid = new Grid();
            grid.RenderTransform = tt;
            for (int i = 0; i < 3; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }

            grid.RowDefinitions[0].Height = new GridLength(20, GridUnitType.Star);
            grid.RowDefinitions[1].Height = new GridLength(40, GridUnitType.Star);
            grid.RowDefinitions[2].Height = new GridLength(40, GridUnitType.Star);

            Border borderHeader = new Border() { 
                CornerRadius=new CornerRadius(2,2,0,0), 
                Background = (Brush)new BrushConverter().ConvertFrom("#404d5c") 
            };
            Border borderAttributes = new Border() {
                Background = (Brush)new BrushConverter().ConvertFrom("#768ca5") 
            };
            Border borderMethods = new Border() { 
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

        private void createNod()
        {
            Point pt = new Point();
            pt.X = Canvas.GetLeft(this);
            pt.Y = Canvas.GetTop(this);
            

            

            Nod n = new Nod(canvas, this);
            Canvas.SetLeft(n, pt.X-15);
            Canvas.SetTop(n, pt.Y);
        }

        public void Klass_MouseDown(object sender, MouseButtonEventArgs e)
        {
            m_start = e.GetPosition(canvas);
            m_startOffset = new Vector(tt.X, tt.Y);
            CaptureMouse();
            createNod();
        }

        private void Klass_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseCaptured)
            {
                Vector offset = Point.Subtract(e.GetPosition(canvas), m_start);

                tt.X = m_startOffset.X + offset.X;
                tt.Y = m_startOffset.Y + offset.Y;
            }
        }

        private void Klass_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();

            Point posOnCanvas = e.GetPosition(canvas);
            posOnCanvas.Y = posOnCanvas.Y - m_startOffset.Y - 10;

            if (posOnCanvas.Y < 0)
                MainWindow.DeleteKlass(this);

            
        }
    }
}