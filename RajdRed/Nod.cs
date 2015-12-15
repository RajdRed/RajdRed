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
    class Nod : Grid
    {
        private Canvas canvas;
        private Klass klass;
        ContextMenu menu = new ContextMenu();

        public Nod(Canvas c, Klass k)
        {
            //Attribut
            Height = 20;
            Width = 20;
            canvas = c;
            klass = k;

            createCircle();
            
            
            //Contextmeny
            
            
            MenuItem item1 = new MenuItem();
            MenuItem item2 = new MenuItem();
            MenuItem item3 = new MenuItem();
            MenuItem item4 = new MenuItem();

            item1.Header = "Association";
            item1.Click += MenuCircle_Click;
            menu.Items.Add(item1);
            item2.Header = "Aggregat";
            item2.Click += MenuDiamond_Click;
            menu.Items.Add(item2);
            item3.Header = "Komposition";
            item3.Click += MenuFilledDiamond_Click;
            menu.Items.Add(item3);
            item4.Header = "Generalisering";
            item4.Click += MenuTriangle_Click;
            menu.Items.Add(item4);

            
            this.MouseRightButtonDown += Nod_Click;
            


            canvas.Children.Add(this);
            
        }

        private void createCircle()
        {
            Ellipse el = new Ellipse();
            el.Fill = Brushes.Black;
            el.Height = 10;
            el.Width = 10;
            this.Children.Clear();
            this.Children.Add(el);
        }

        private void createTriangle()
        {
            Polygon triangle = new Polygon();
            triangle.Stroke = Brushes.Black;
            triangle.Fill = (Brush)new BrushConverter().ConvertFrom("#EAEDF2");
            triangle.StrokeThickness = 2;

            Point p1 = new Point(9, 0);
            Point p2 = new Point(18, 18);
            Point p3 = new Point(0, 18);
            PointCollection pc = new PointCollection();
            pc.Add(p1);
            pc.Add(p2);
            pc.Add(p3);

            triangle.Points = pc;
            triangle.HorizontalAlignment = HorizontalAlignment.Center;
            triangle.VerticalAlignment = VerticalAlignment.Center;

            this.Children.Clear();
            this.Children.Add(triangle);
        }

        private void createDiamond(bool filled)
        {
            Polygon diamond = new Polygon();
            diamond.Stroke = Brushes.Black;
            if (filled)
            {
                diamond.Fill = Brushes.Black;
            }
            else
            {
                diamond.Fill = (Brush)new BrushConverter().ConvertFrom("#EAEDF2");
            }
           
            diamond.StrokeThickness = 2;

            Point p1 = new Point(9, 0);
            Point p2 = new Point(18, 9);
            Point p3 = new Point(9, 18);
            Point p4 = new Point(0, 9);
            PointCollection pc = new PointCollection();
            pc.Add(p1);
            pc.Add(p2);
            pc.Add(p3);
            pc.Add(p4);

            diamond.Points = pc;
            diamond.HorizontalAlignment = HorizontalAlignment.Center;
            diamond.VerticalAlignment = VerticalAlignment.Center;

            this.Children.Clear();
            this.Children.Add(diamond);
        }

        private void MenuCircle_Click(object sender, RoutedEventArgs e)
        {
            createCircle();
        }
        private void MenuTriangle_Click(object sender, RoutedEventArgs e)
        {
            createTriangle();
        }
        private void MenuDiamond_Click(object sender, RoutedEventArgs e)
        {
            createDiamond(false);
        }
        private void MenuFilledDiamond_Click(object sender, RoutedEventArgs e)
        {
            createDiamond(true);
        }

        private void Nod_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Nod_Click(object sender, MouseButtonEventArgs e)
        {
            menu.IsOpen = true;
        }

    }
}
