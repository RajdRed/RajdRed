using RajdRed.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace RajdRed.Views
{
    /// <summary>
    /// Interaction logic for KlassNodView.xaml
    /// </summary>
    public partial class NodKlassView : UserControl
    {
        NodKlassViewModel NodKlassViewModel { get { return DataContext as NodKlassViewModel; } }
        public NodKlassView()
        {
            InitializeComponent();
            Loaded += (sender, eArgs) =>
            {
                if (NodKlassViewModel != null)
                {
                    NodKlassViewModel.SetView(this);
                }
            };
        }

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //NodKlassViewModel.CreateLinje();
            NodKlassViewModel.NodKlassModel.IsPressed = true;  
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (NodKlassViewModel.NodKlassModel.IsSet)
            {
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                Grid g = new Grid() { Width = mw.theCanvas.ActualWidth, Height = mw.theCanvas.ActualHeight, Background = Brushes.Black, Opacity = 0.2 };
                Canvas.SetLeft(g, 0);
                Canvas.SetTop(g, 0);
                
                NodSettings ns = new NodSettings(NodKlassViewModel, g);
                Point pt = this.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
                Canvas.SetLeft(ns, pt.X + 6 - ns.Width / 2);
                Canvas.SetTop(ns, pt.Y + 6 - ns.Height / 2);

                g.MouseDown += (sendr, eventArgs) =>
                {
                    mw.theCanvas.Children.Remove(g);
                    mw.theCanvas.Children.Remove(ns);
                };

                mw.theCanvas.Children.Add(g);
                mw.theCanvas.Children.Add(ns);
                
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NodKlassViewModel.NodKlassModel.IsPressed = false;
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
            if (NodKlassViewModel.NodKlassModel.IsPressed)
            {
                NodKlassViewModel.NodKlassModel.IsPressed = false;
                NodKlassViewModel.CreateLinje();
            }

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
