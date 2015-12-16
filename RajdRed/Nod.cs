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
    public class Nod : Grid
    {
        private Klass klass;
        private ContextMenu menu = new ContextMenu();
        private bool alwaysVisible = false;


        public Nod(Klass k)
        {
            //Attribut
            Height = 15;
            Width = 15;
            this.Visibility = Visibility.Hidden;
            klass = k;

            createAssociation();
            
            //Contextmeny
            MenuItem item1 = new MenuItem();
            MenuItem item2 = new MenuItem();
            MenuItem item3 = new MenuItem();
            MenuItem item4 = new MenuItem();

            item1.Header = "Association";
            item1.Click += MenuAssociation_Click;
            menu.Items.Add(item1);
            item2.Header = "Aggregat";
            item2.Click += MenuAggregation_Click;
            menu.Items.Add(item2);
            item3.Header = "Komposition";
            item3.Click += MenuComposition_Click;
            menu.Items.Add(item3);
            item4.Header = "Generalisering";
            item4.Click += MenuGeneralization_Click;
            menu.Items.Add(item4);


            this.MouseLeftButtonDown += Nod_DoubleClick;
            this.MouseEnter += Nod_MouseEnter;
            this.MouseLeave += Nod_MouseLeave;
            k.MouseEnter += Klass_MouseEnter;
            k.MouseLeave += Klass_MouseLeave;
   
        }

        private void createAssociation()
        {
            Ellipse el = new Ellipse();
            el.Fill = Brushes.Black;
            el.Height = 10;
            el.Width = 10;
            alwaysVisible = false;
            this.Children.Clear();
            this.Children.Add(el);
        }

        private void createGeneralization()
        {
            Polygon triangle = new Polygon();
            triangle.Height = 10;
            triangle.Width = 10;
            triangle.Stroke = Brushes.Black;
            triangle.Fill = (Brush)new BrushConverter().ConvertFrom("#EAEDF2");
            triangle.StrokeThickness = 2;

            Point p1 = new Point(5, 1);
            Point p2 = new Point(9, 9);
            Point p3 = new Point(1, 9);
            PointCollection pc = new PointCollection();
            pc.Add(p1);
            pc.Add(p2);
            pc.Add(p3);

            triangle.Points = pc;
            triangle.HorizontalAlignment = HorizontalAlignment.Center;
            triangle.VerticalAlignment = VerticalAlignment.Center;

            alwaysVisible = true;
            this.Children.Clear();
            this.Children.Add(triangle);
        }

        private void createAggregation(bool filled)
        {
            Polygon diamond = new Polygon();
            diamond.Height = 10;
            diamond.Width = 10;
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

            Point p1 = new Point(5, 1);
            Point p2 = new Point(9, 5);
            Point p3 = new Point(5, 9);
            Point p4 = new Point(1, 5);
            PointCollection pc = new PointCollection();
            pc.Add(p1);
            pc.Add(p2);
            pc.Add(p3);
            pc.Add(p4);

            diamond.Points = pc;
            diamond.HorizontalAlignment = HorizontalAlignment.Center;
            diamond.VerticalAlignment = VerticalAlignment.Center;

            alwaysVisible = true;
            this.Children.Clear();
            this.Children.Add(diamond);
        }

        private void MenuAssociation_Click(object sender, RoutedEventArgs e)
        {
            createAssociation();
        }
        private void MenuGeneralization_Click(object sender, RoutedEventArgs e)
        {
            createGeneralization();
        }
        private void MenuAggregation_Click(object sender, RoutedEventArgs e)
        {
            createAggregation(false);
        }
        private void MenuComposition_Click(object sender, RoutedEventArgs e)
        {
            createAggregation(true);
        }


        private void Nod_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                menu.IsOpen = true;
            }

        }

        private void Klass_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Visibility = Visibility.Visible;
        }

        private void Klass_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!alwaysVisible)
            {
                TheEnclosingMethod();
            }
              
        }

        private void Nod_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!alwaysVisible)
            {
                this.Visibility = Visibility.Visible;
                var el = this.Children.OfType<Ellipse>().FirstOrDefault();
                el.Height = 15;
                el.Width = 15;
            }
        }

        private void Nod_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!alwaysVisible)
            {
                var el = this.Children.OfType<Ellipse>().FirstOrDefault();
                el.Height = 10;
                el.Width = 10;
                TheEnclosingMethod();
            }
        }

        public async void TheEnclosingMethod()
        {
            await Task.Delay(3000);
            if (!(klass.IsMouseOver || this.IsMouseOver))
            {
                if (!alwaysVisible)
                {
                    this.Visibility = Visibility.Hidden;
                }
                
            }
            
        }
    }
}
