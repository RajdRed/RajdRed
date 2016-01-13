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
    /// Interaction logic for NodSettings.xaml
    /// </summary>

    public partial class NodSettings : UserControl
    {
        private Klass _klass;

        public NodSettings(Klass k)
        {
            InitializeComponent();
            _klass = k;

            Polygon polygon = new Polygon() { 
                VerticalAlignment = VerticalAlignment.Top, 
                HorizontalAlignment=HorizontalAlignment.Center 
            };

            Grid.SetColumn(polygon, 1);
            Grid.SetRow(polygon, 2);

            OuterGrid.Children.Add(polygon);
        }
    }
}
