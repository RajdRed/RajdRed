using RajdRed.ViewModels;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace RajdRed.Views
{
    /// <summary>
    /// Interaction logic for NodCanvasView.xaml
    /// </summary>
    public partial class NodCanvasView : UserControl
    {
        NodCanvasViewModel NodCanvasViewModel { get { return DataContext as NodCanvasViewModel; } }
        public NodCanvasView()
        {
            InitializeComponent();
            Loaded += (sender, eArgs) => {
                NodCanvasViewModel.SetNodCanvasView(this);
                CaptureMouse(); //Avkommenteras om/när man kan dra nod från klass
                
               
            };
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CaptureMouse();
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseCaptured && IsLoaded)
            {
                Point p = e.GetPosition(Application.Current.MainWindow);

                SetValue(Canvas.LeftProperty, p.X - Width / 2);
                SetValue(Canvas.TopProperty, p.Y - Height / 2);
            }
        }


        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();

            Point p = e.GetPosition(Application.Current.MainWindow);

            foreach (KlassViewModel k in NodCanvasViewModel.NodCanvasRepository.MainRepository.KlassRepository)
            {
                if (k.IsInArea(p))
                {
                    foreach (NodKlassViewModel n in k.NodKlassRepository)
                    {
                        if (n.IsInArea(p))
                        {
                            n.EatNodCanvas(NodCanvasViewModel);
                            break;
                        }
                    }

                    break;
                }
            }
        }
    }
}
