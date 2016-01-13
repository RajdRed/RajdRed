using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RajdRed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
		private ArchiveMenu archiveMenu = new ArchiveMenu();
		private SettingsMenu settingsMenu = new SettingsMenu();
        private bool isArchiveMenuActive = false;
		private bool isSettingsMenuActive = false;
		private bool darkColorTheme = false;
		private List<Klass> _klassList = new List<Klass>();
		public RajdColors Colors = new RajdColors(RajdColorScheme.Light);
		
        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

		public void changeColors(bool dark)
		{
			if (!dark)
				Colors = RajdColorScheme.Dark;

			else
				Colors = RajdColorScheme.Light;
		} 

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Point pt = e.GetPosition(theCanvas);

            Klass klass = new Klass(this, pt);
			_klassList.Add(klass);

            klass.Klass_MouseDown(sender, e);
        }

        public void DeleteKlass(Klass klass)
        {
            theCanvas.Children.Remove(klass);
			_klassList.Remove(klass);
        }

        public Canvas getCanvas()
        {
            return theCanvas;
        }

        private void Ellipse_MinimizeWindow(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Ellipse_MaximizeWindow(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;

            else WindowState = WindowState.Maximized;
        }

        private void Ellipse_CloseWindow(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void WindowDragAndMove(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2 && WindowState == WindowState.Maximized)
                WindowState = WindowState.Normal;

            else if (e.ClickCount == 2 && WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;

            else DragMove();
        }

        private void Button_ArchiveMenu_MouseDown(object sender, MouseButtonEventArgs e)
        {
			if (!isArchiveMenuActive) {
				Canvas.SetLeft(archiveMenu, 0);
				Canvas.SetTop(archiveMenu, 100);
				theCanvas.Children.Add(archiveMenu);
				isArchiveMenuActive = true;

				ArchiveButtonGrid.SetCurrentValue(Control.BackgroundProperty, Colors.MenuButtonBg);
			}

			else {
				theCanvas.Children.Remove(archiveMenu);
				isArchiveMenuActive = false;
				ArchiveButtonGrid.SetCurrentValue(Control.BackgroundProperty, Brushes.Transparent);
			}
        }

		private void theCanvas_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (isArchiveMenuActive) {
				theCanvas.Children.Remove(archiveMenu);
				isArchiveMenuActive = false;
				ArchiveButtonGrid.SetCurrentValue(Control.BackgroundProperty, Brushes.Transparent);
			}

			if (isSettingsMenuActive)
			{
				theCanvas.Children.Remove(settingsMenu);
				isSettingsMenuActive = false;
				SettingsMenuGrid.SetCurrentValue(Control.BackgroundProperty, Brushes.Transparent);
			}
		}

		private void theCanvas_MouseMove(object sender, MouseEventArgs e)
		{
			if (isArchiveMenuActive) {
				Point pt = e.GetPosition(theCanvas);

				ArchiveButtonGrid.SetCurrentValue(Control.BackgroundProperty, Colors.MenuButtonBg);
			}

			if (isSettingsMenuActive)
			{
				Point pt = e.GetPosition(theCanvas);

				SettingsMenuGrid.SetCurrentValue(Control.BackgroundProperty, Colors.MenuButtonBg);
			}
		}

		private void Button_ArchiveMenu_MouseUp(object sender, MouseButtonEventArgs e)
		{
			Point pt = e.GetPosition(theCanvas);

			if (pt.X > 150 || pt.Y > 220) {
				theCanvas.Children.Remove(archiveMenu);
				isArchiveMenuActive = false;
				ArchiveButtonGrid.SetCurrentValue(Control.BackgroundProperty, Brushes.Transparent);
			}
		}

		private void Button_SettingsMenu_MouseDown(object sender, MouseButtonEventArgs e)
		{
			if (!isSettingsMenuActive)
			{
				Canvas.SetLeft(settingsMenu, theCanvas.ActualWidth - 150);
				Canvas.SetTop(settingsMenu, 100);
				theCanvas.Children.Add(settingsMenu);
				isSettingsMenuActive = true;

				SettingsMenuGrid.SetCurrentValue(Control.BackgroundProperty, Colors.MenuButtonBg);
			}

			else
			{
				theCanvas.Children.Remove(settingsMenu);
				isSettingsMenuActive = false;
				SettingsMenuGrid.SetCurrentValue(Control.BackgroundProperty, Brushes.Transparent);
			}
		}

		private void Button_SettingsMenu_MouseUp(object sender, MouseButtonEventArgs e)
		{
			Point pt = e.GetPosition(theCanvas);

			if (pt.X < theCanvas.ActualWidth - 150 || pt.Y > 220) {
				theCanvas.Children.Remove(settingsMenu);
				isSettingsMenuActive = false;
				SettingsMenuGrid.SetCurrentValue(Control.BackgroundProperty, Brushes.Transparent);
			}
		}

		public void ChangeColorTheme(bool dark)
		{
			if (dark) {
				this.darkColorTheme = true;
				var uri = new Uri("pack://application:,,,/img/createClassBg-Dark.png");
				var bitmap = new BitmapImage(uri);
				addClassButton.Source = bitmap;
			}

			else {
				this.darkColorTheme = false;
				var uri = new Uri("pack://application:,,,/img/createClassBg.png");
				var bitmap = new BitmapImage(uri);
				addClassButton.Source = bitmap;
			}

			theCanvas.Background = Colors.TheCanvasBg;
			menuBot.Background = Colors.MenuBotBg;
			menuTopRight.Background = Colors.KlassNameBg;
			menuTopLeft.Fill = Colors.KlassNameBg;
			SettingsMenuGrid.SetCurrentValue(Control.BackgroundProperty, Colors.MenuButtonBg);

			foreach (Klass k in _klassList)
				k.setKlassColors();
		}
    }
}