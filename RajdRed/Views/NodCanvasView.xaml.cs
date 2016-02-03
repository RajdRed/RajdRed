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
        public NodCanvasView()
        {
            InitializeComponent();
            Loaded += (sender, eArgs) => {
                NodCanvasViewModel.SetNodCanvasView(this);
                if (!NodCanvasViewModel.NodCanvasModel.IsSet)
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
            NodCanvasViewModel.LookAndAttachCanvasNod(e.GetPosition(Application.Current.MainWindow));
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            ScaleTransform trans = new ScaleTransform();
            trans.CenterX = 5;
            trans.CenterY = 5;

            this.RenderTransform = trans;
            // if you use the same animation for X & Y you don't need anim1, anim2 
            DoubleAnimation anim = new DoubleAnimation(1, 1.5, TimeSpan.FromMilliseconds(200));
            trans.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
            trans.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            ScaleTransform trans = new ScaleTransform();
            trans.CenterX = 6;
            trans.CenterY = 6;
            this.RenderTransform = trans;
            // if you use the same animation for X & Y you don't need anim1, anim2 
            DoubleAnimation anim = new DoubleAnimation(1.5, 1, TimeSpan.FromMilliseconds(200));
            trans.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
            trans.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
        }
    }
}
