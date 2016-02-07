using RajdRed.ViewModels;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace RajdRed.Views
{
    /// <summary>
    /// Interaction logic for NodCanvasView.xaml
    /// </summary>
    public partial class NodCanvasView : UserControl
    {
        NodCanvasViewModel NodCanvasViewModel { get { return DataContext as NodCanvasViewModel; } }
        Point _posOnUserControlOnHit;
        Point _startDragPosition;
        public NodCanvasView()
        {
            InitializeComponent();
            Loaded += (sender, eArgs) => {
                NodCanvasViewModel.SetNodCanvasView(this);
                if (!NodCanvasViewModel.NodCanvasModel.IsSet)
                {
                    CaptureMouse(); //Avkommenteras om/när man kan dra nod från klass   
                    Point p = Mouse.GetPosition(Application.Current.MainWindow);
                    _startDragPosition = new Point(p.X-NodCanvasViewModel.NodCanvasModel.Width, p.Y-NodCanvasViewModel.NodCanvasModel.Height);
					_posOnUserControlOnHit = Mouse.GetPosition(this);

                    Dispatcher.Invoke(new Action(() =>
                    {
                        NodCanvasViewModel.NodCanvasRepository.MainRepository.KlassRepository.ShowAllKlassNodes();
                    }));
                }

                eArgs.Handled = true;
            };
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CaptureMouse();
            _startDragPosition = _posOnUserControlOnHit = Mouse.GetPosition(this);

            Dispatcher.Invoke(new Action(() => {
                NodCanvasViewModel.NodCanvasRepository.MainRepository.KlassRepository.ShowAllKlassNodes();
            }));

            e.Handled = true;
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseCaptured && IsLoaded)
            {
                Point p = e.GetPosition(Application.Current.MainWindow);

               if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    double dx = (p.X - _startDragPosition.X) * (p.X - _startDragPosition.X);
                    double dy = (p.Y - _startDragPosition.Y) * (p.Y - _startDragPosition.Y);

                    if (dx >= dy) {
                        SetValue(Canvas.LeftProperty, p.X - _posOnUserControlOnHit.X);
                        SetValue(Canvas.TopProperty, _startDragPosition.Y - _posOnUserControlOnHit.Y);
                    }
                    else
                    {
                        SetValue(Canvas.LeftProperty, _startDragPosition.X - _posOnUserControlOnHit.X);
                        SetValue(Canvas.TopProperty, p.Y - _posOnUserControlOnHit.Y);
                    }
                }

                else
                {
					if (!((p.Y - _posOnUserControlOnHit.Y) <= 100.5))
						SetValue(Canvas.TopProperty, p.Y - _posOnUserControlOnHit.Y);

					if (!((p.X - _posOnUserControlOnHit.X) <= 0.5))
						SetValue(Canvas.LeftProperty, p.X - _posOnUserControlOnHit.X);
                }
            }
        }


        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();
            NodCanvasViewModel.LookAndAttachCanvasNod(e.GetPosition(Application.Current.MainWindow));

            Dispatcher.Invoke(new Action(() =>
            {
                NodCanvasViewModel.NodCanvasRepository.MainRepository.KlassRepository.HideAllKlassNodes();
            }));
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
        
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
           
        }
    }
}
