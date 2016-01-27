using RajdRed.Models;
using RajdRed.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace RajdRed.Views
{
    /// <summary>
    /// Interaction logic for KlassView.xaml
    /// </summary>
    public partial class KlassView : UserControl
    {
        public KlassViewModel KlassViewModel { get { return DataContext as KlassViewModel; } }
        public KlassView()
        {
            InitializeComponent();
            Loaded += (sender, eArgs) => {
                if (!KlassViewModel.KlassModel.OnField)
                {
                    CaptureMouse();
                    KlassViewModel.SetKlassView(this);
                }
            };
        }

        private void InnerGrid_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        public void InnerBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CaptureMouse();
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseCaptured)
            {
                Point p = e.GetPosition(Application.Current.MainWindow);
                SetValue(Canvas.LeftProperty, p.X - Width / 2);
                SetValue(Canvas.TopProperty, p.Y - Height / 2);
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();
            if(!this.KlassViewModel.KlassModel.IsSelected)
                this.KlassViewModel.SetAdornerLayer(this);
            
        }

        private void OuterBorder_MouseEnter(object sender, MouseEventArgs e)
        {

        }
    }
}
