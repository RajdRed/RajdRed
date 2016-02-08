using RajdRed.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

                GotKeyboardFocus += (s,e) =>
                {
                    TextBoxViewModel.Select();
                };

                LostKeyboardFocus += (s, e) =>
                {
                    TextBoxViewModel.Deselect();
                };
            };
        }

        private void TextBoxBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!(Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                TextBoxViewModel.TextBoxRepository.MainRepository.DeselectAll();
            }

            CaptureMouse();
            _posOnUserControlOnHit = Mouse.GetPosition(this);
            e.Handled = true;
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseCaptured)
            {
                Point p = e.GetPosition(Application.Current.MainWindow);

				if (!((p.Y - _posOnUserControlOnHit.Y) <= 100.5))
					SetValue(Canvas.TopProperty, p.Y - _posOnUserControlOnHit.Y);

				if (!((p.X - _posOnUserControlOnHit.X) <= 0.5))
					SetValue(Canvas.LeftProperty, p.X - _posOnUserControlOnHit.X);
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();
        }

    }
}
