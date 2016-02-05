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
             NodKlassViewModel.NodKlassModel.IsPressed = true;  
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (NodKlassViewModel.NodKlassModel.IsSet)
            {
                MainWindow mw = (MainWindow)Application.Current.MainWindow;
                NodSettings ns = new NodSettings(NodKlassViewModel);
                Point pt = e.GetPosition(Application.Current.MainWindow);

                Canvas.SetLeft(ns, pt.X - ns.Width / 2);
                Canvas.SetTop(ns, pt.Y - ns.Height / 2);

                mw.theCanvas.Children.Add(ns);
            }
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            NodKlassViewModel.NodKlassModel.IsPressed = false;
        }

		private void UserControl_MouseEnter(object sender, MouseEventArgs e)
		{
		}

		private void UserControl_MouseLeave(object sender, MouseEventArgs e)
		{
            if (NodKlassViewModel.NodKlassModel.IsPressed)
            {
                if (!NodKlassViewModel.NodKlassModel.IsSet)
                {
                    //Skapa ny linje
                    NodKlassViewModel.CreateLinje();
                }
                else
                {
                    //Lossa linje
                    NodKlassViewModel.LooseLinje(e.GetPosition(Application.Current.MainWindow));
                }

                NodKlassViewModel.NodKlassModel.IsPressed = false;
            }
		}              
    }
}
