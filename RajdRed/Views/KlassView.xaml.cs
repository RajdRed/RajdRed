using RajdRed.Models;
using RajdRed.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

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
            Loaded += (sender, eArgs) => {
                if (!KlassViewModel.KlassModel.OnField)
                {
                    CaptureMouse();
                    KlassViewModel.SetKlassView(this);
					_posOnUserControlOnHit = new Point(Width / 2, Height / 2);
					KlassViewModel.KlassModel.PositionLeft = KlassViewModel.KlassModel.PositionLeft - (Width / 2);
					KlassViewModel.KlassModel.PositionTop = KlassViewModel.KlassModel.PositionTop - (Height / 2);

                    KlassViewModel.SetAdornerLayer();
                }
            };
        }

        private void InnerGrid_MouseEnter(object sender, MouseEventArgs e)
        {
        }

        public void InnerBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
			CaptureMouse();
			_posOnUserControlOnHit = Mouse.GetPosition(this);
			KlassViewModel.KlassModel.IsSelected = true;
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsMouseCaptured && KlassViewModel.KlassModel.IsSelected)
            {
                Point p = e.GetPosition(Application.Current.MainWindow);
				SetValue(Canvas.LeftProperty, p.X - _posOnUserControlOnHit.X);
                SetValue(Canvas.TopProperty, p.Y - _posOnUserControlOnHit.Y);

				if (KlassViewModel.KlassModel.OnField && ((p.Y - _posOnUserControlOnHit.Y) <= 100.5))
					Canvas.SetTop(this, 100.6);

				if ((p.X - _posOnUserControlOnHit.X) <= 0.5)
					Canvas.SetLeft(this, 0.6);
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

			KlassViewModel.KlassModel.IsSelected = false;
        }

        private void OuterBorder_MouseEnter(object sender, MouseEventArgs e)
        {
        }

		private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
		{
			MainWindow mw = (MainWindow)Application.Current.MainWindow;

			Grid g = new Grid() { Width = mw.theCanvas.ActualWidth, Height = mw.theCanvas.ActualHeight, Background = Brushes.Black, Opacity = 0.2 };
			Canvas.SetLeft(g, 0);
			Canvas.SetTop(g, 0);

			ClassSettings cs = new ClassSettings(KlassViewModel, g);
			g.MouseDown += (sendr, eventArgs) => { CloseSettings(cs, g); };	//Skapar ett mouseDown-event för Grid g som anropar CloseSettings

			double x = (KlassViewModel.KlassModel.PositionLeft + _posOnUserControlOnHit.X - cs.Width / 2.33);
			double y = (KlassViewModel.KlassModel.PositionTop + _posOnUserControlOnHit.Y - cs.Height / 2);

			if (cs.Width + x > mw.ActualWidth)
				Canvas.SetLeft(cs, x - (x + cs.Width - mw.ActualWidth));
			else if (x < 0)
				Canvas.SetLeft(cs, x - x);
			else
				Canvas.SetLeft(cs, x);

			if (cs.Height + y > mw.ActualHeight)
				Canvas.SetTop(cs, y - (y + cs.Height - mw.ActualHeight));
			else if (y < 0)
				Canvas.SetTop(cs, y - y);
			else
				Canvas.SetTop(cs, y);

			mw.theCanvas.Children.Add(g);
			mw.theCanvas.Children.Add(cs);
		}

		public void CloseSettings(ClassSettings cs, Grid g)
        {
			MainWindow mw = (MainWindow)Application.Current.MainWindow;
            mw.theCanvas.Children.Remove(cs);
			mw.theCanvas.Children.Remove(g);
        }
    }
}