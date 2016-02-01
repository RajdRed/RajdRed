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
        private Point _posOnUserControlOnHit;

        public KlassView()
        {
            InitializeComponent();
            Loaded += (sender, eArgs) =>
            {
                if (!KlassViewModel.KlassModel.OnField)
                {
                    CaptureMouse();
                    KlassViewModel.SetKlassView(this);
                    _posOnUserControlOnHit = new Point(ActualWidth / 2, ActualHeight / 2);
                    KlassViewModel.KlassModel.PositionLeft = KlassViewModel.KlassModel.PositionLeft - (ActualWidth / 2);
                    KlassViewModel.KlassModel.PositionTop = KlassViewModel.KlassModel.PositionTop - (ActualHeight / 2);

                    //KlassViewModel.SetAdornerLayer();
                    
                }
            };
        }

        public void Innerborder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            
            if (!KlassViewModel.KlassModel.IsSelected && (Keyboard.IsKeyDown(Key.RightCtrl) || (Keyboard.IsKeyDown(Key.LeftCtrl))))
            {
                KlassViewModel.KlassModel.IsSelected = true;
                CaptureMouse();
                _posOnUserControlOnHit = Mouse.GetPosition(this);
            }
            else if (KlassViewModel.KlassModel.IsSelected && (Keyboard.IsKeyDown(Key.RightCtrl) || (Keyboard.IsKeyDown(Key.LeftCtrl))))
                KlassViewModel.KlassModel.IsSelected = false;
            else if (KlassViewModel.KlassModel.IsSelected && !(Keyboard.IsKeyDown(Key.RightCtrl) || (Keyboard.IsKeyDown(Key.LeftCtrl))))
            {
                CaptureMouse();
                _posOnUserControlOnHit = Mouse.GetPosition(this);
            }
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseCaptured && KlassViewModel.KlassModel.IsSelected)
            {
                Point p = e.GetPosition(Application.Current.MainWindow);

                if (!(KlassViewModel.KlassModel.OnField && ((p.Y - _posOnUserControlOnHit.Y) <= 100.5)))
                    SetValue(Canvas.TopProperty, p.Y - _posOnUserControlOnHit.Y);

                if (!((p.X - _posOnUserControlOnHit.X) <= 0.5))
                    SetValue(Canvas.LeftProperty, p.X - _posOnUserControlOnHit.X);
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();

            Point p = e.GetPosition(Application.Current.MainWindow);
            Point posOfUserControlOnCanvas = new Point();
            posOfUserControlOnCanvas.X = p.X - _posOnUserControlOnHit.X;
            posOfUserControlOnCanvas.Y = p.Y - _posOnUserControlOnHit.Y;

            if (posOfUserControlOnCanvas.Y <= 100 && !KlassViewModel.KlassModel.OnField)
                KlassViewModel.Delete();
            else
                KlassViewModel.KlassModel.OnField = true;

        }
        
    }
}
