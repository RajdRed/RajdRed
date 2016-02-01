using RajdRed.ViewModels;
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
            //if (IsMouseCaptured && IsLoaded)
            //{
            //    Point p = e.GetPosition(Application.Current.MainWindow);

            //    SetValue(Canvas.LeftProperty, p.X - Width / 2);
            //    SetValue(Canvas.TopProperty, p.Y - Height / 2);

            //    DragDrop.DoDragDrop(this, NodCanvasViewModel, DragDropEffects.None);
            //}

            //try
            //{
            //    if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            //    {
            //        Point position = e.GetPosition(null);

            //        Dispatcher.BeginInvoke(DispatcherPriority.Render, new ParameterizedThreadStart(DragDrop.DoDragDrop), this);
            //    }
            //}
            //finally { }
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs e)
        {
            base.OnGiveFeedback(e);

            

            e.Handled = true;
        }



        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ReleaseMouseCapture();
        }
    }
}
