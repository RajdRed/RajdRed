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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition(theCanvas);

            Klass klass = new Klass(this, "Ny klass*");

            Canvas.SetLeft(klass, pt.X - 50);
            Canvas.SetTop(klass, pt.Y - 10);
            klass.Klass_MouseDown(sender, e);
        }

        public void DeleteKlass(UIElement ui)
        {
            theCanvas.Children.Remove(ui);
        }

        public Canvas getCanvas()
        {
            return theCanvas;
        }
    }
}