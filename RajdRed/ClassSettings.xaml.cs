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
    /// Interaction logic for ClassSettings.xaml
    /// </summary>
    public partial class ClassSettings : UserControl
    {
        private Klass _klass;
        public ClassSettings(Klass k)
        {
            InitializeComponent();
            _klass = k;
            ClassName.Text = _klass.ClassName.Content.ToString();
            Attributes.Text = _klass.Attributes.Text;
            Methods.Text = _klass.Methods.Text;

            //Lägger till vald färg
            ClassName.Background = (Brush)new BrushConverter().ConvertFrom(_klass.Colors[0]);
            Attributes.Background = (Brush)new BrushConverter().ConvertFrom(_klass.Colors[1]);
            Methods.Background = (Brush)new BrushConverter().ConvertFrom(_klass.Colors[0]);
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Är du säker?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _klass.Delete();
                _klass.CloseSettings(this);
            }
        }

        private void Btn_Abort_Click(object sender, RoutedEventArgs e)
        {
            _klass.CloseSettings(this);
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            _klass.Save(this);
            _klass.CloseSettings(this);
        }
    }
}
