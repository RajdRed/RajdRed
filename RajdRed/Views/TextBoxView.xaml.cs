using RajdRed.ViewModels;
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

namespace RajdRed.Views
{
    /// <summary>
    /// Interaction logic for TextBoxView.xaml
    /// </summary>
    public partial class TextBoxView : UserControl
    {
        TextBoxViewModel TextBoxViewModel { get { return DataContext as TextBoxViewModel; } }
        private Point _posOnUserControlOnHit;
        public TextBoxView()
        {
            InitializeComponent();
            Loaded += (sender, args) =>
            {
                TextBoxViewModel.SetView(this);
            };
        }

        private void TextBoxBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CaptureMouse();
            _posOnUserControlOnHit = Mouse.GetPosition(this);
            e.Handled = true;
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseCaptured)
            {
                Point p = e.GetPosition(Application.Current.MainWindow);

                SetValue(Canvas.LeftProperty, p.X - _posOnUserControlOnHit.X);
                SetValue(Canvas.TopProperty, p.Y - _posOnUserControlOnHit.Y);
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();
        }

    }
}
