using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using RajdRed.Models.Adds;
using RajdRed.Repositories;
using RajdRed.Models;
using RajdRed.ViewModels;

namespace RajdRed
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
		private ArchiveMenu archiveMenu = new ArchiveMenu();
		private SettingsMenu settingsMenu = new SettingsMenu();
        public bool isArchiveMenuActive = false;
		public bool isSettingsMenuActive = false;
		public RajdColors Colors = new RajdColors(RajdColorScheme.Light);
		private bool darkMode = false;

        MainRepository _mainRepository = new MainRepository();
		
        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
            DataContext = _mainRepository;
        }

		public void changeColors(bool dark)
		{
			if (!dark)
			{
				Colors = RajdColorScheme.Dark;
				darkMode = true;
			}

			else
			{
				Colors = RajdColorScheme.Light;
				darkMode = false;
			}
		} 

        private void Button_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            _mainRepository.KlassRepository.AddNewKlass(e.GetPosition(Application.Current.MainWindow));
        }

        //public void DeleteKlass(KlassView klass)
        //{
        //    theCanvas.Children.Remove(klass);
        //    _klassList.Remove(klass);
        //}

		public bool getDarkMode()
		{
			return darkMode;
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
			if (!isArchiveMenuActive) 
			{
				Canvas.SetLeft(archiveMenu, 0);
				Canvas.SetTop(archiveMenu, 100);
				theCanvas.Children.Add(archiveMenu);
				isArchiveMenuActive = true;

				archiveMenuBtn.SetCurrentValue(Control.BackgroundProperty, Colors.MenuButtonBg);
			}

			else 
			{
				theCanvas.Children.Remove(archiveMenu);
				isArchiveMenuActive = false;
				archiveMenuBtn.SetCurrentValue(Control.BackgroundProperty, Brushes.Transparent);
			}
        }

		private void theCanvas_MouseDown(object sender, MouseButtonEventArgs e)
		{
			Keyboard.ClearFocus();

			if (isArchiveMenuActive) 
			{
				theCanvas.Children.Remove(archiveMenu);
				isArchiveMenuActive = false;
				archiveMenuBtn.SetCurrentValue(Control.BackgroundProperty, Brushes.Transparent);
			}

			if (isSettingsMenuActive) 
			{
				theCanvas.Children.Remove(settingsMenu);
				isSettingsMenuActive = false;
				settingsMenuBtn.SetCurrentValue(Control.BackgroundProperty, Brushes.Transparent);
			}
		}

		private void Button_ArchiveMenu_MouseUp(object sender, MouseButtonEventArgs e)
		{
			Point pt = e.GetPosition(theCanvas);

			if ( (pt.X > 150 || pt.Y > 190) || (pt.X > 78 && pt.Y < 96)) 
			{
				theCanvas.Children.Remove(archiveMenu);
				isArchiveMenuActive = false;
				archiveMenuBtn.SetCurrentValue(Control.BackgroundProperty, Brushes.Transparent);
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

				settingsMenuBtn.SetCurrentValue(Control.BackgroundProperty, Colors.MenuButtonBg);
			}

			else 
			{
				theCanvas.Children.Remove(settingsMenu);
				isSettingsMenuActive = false;
				settingsMenuBtn.SetCurrentValue(Control.BackgroundProperty, Brushes.Transparent);
			}
		}

		private void Button_SettingsMenu_MouseUp(object sender, MouseButtonEventArgs e)
		{
			Point pt = e.GetPosition(theCanvas);

			if ((pt.X < theCanvas.ActualWidth - 150 || pt.Y > 220) || (pt.X < theCanvas.ActualWidth - 78 && pt.Y < theCanvas.ActualWidth - 96)) 
			{
				theCanvas.Children.Remove(settingsMenu);
				isSettingsMenuActive = false;
				settingsMenuBtn.SetCurrentValue(Control.BackgroundProperty, Brushes.Transparent);
			}
		}

		public void ChangeColorTheme(bool dark)
		{
			if (dark) 
			{
				var uri = new Uri("pack://application:,,,/img/createClassBg-Dark.png");
				var bitmap = new BitmapImage(uri);
				addClassButton.Source = bitmap;	
			}

			else 
			{
				var uri = new Uri("pack://application:,,,/img/createClassBg.png");
				var bitmap = new BitmapImage(uri);
				addClassButton.Source = bitmap;
			}

			theCanvas.Background = Colors.TheCanvasBg;
			menuBot.Background = Colors.MenuBotBg;
			menuTopRight.Background = Colors.KlassNameBg;
			menuTopLeft.Fill = Colors.KlassNameBg;
			titleBorder.Background = Colors.TheCanvasBg;
			titleBorder.BorderBrush = Colors.TheCanvasBg;
			TitleTextBox.SetCurrentValue(Control.ForegroundProperty, Colors.TitleText);

			if (isArchiveMenuActive)
				archiveMenuBtn.SetCurrentValue(Control.BackgroundProperty, Colors);

			if (isSettingsMenuActive)
				settingsMenuBtn.SetCurrentValue(Control.BackgroundProperty, Colors.MenuButtonBg);

            //foreach (KlassView k in _klassList)
            //    k.setKlassColors();
		}

		private void addClassButton_MouseEnter(object sender, MouseEventArgs e)
		{
			ThicknessAnimation animate = new ThicknessAnimation(new Thickness(0), TimeSpan.FromSeconds(0.2));
			addClassButton.BeginAnimation(Canvas.MarginProperty, animate);
		}

		private void addClassButton_MouseLeave(object sender, MouseEventArgs e)
		{
			ThicknessAnimation animate = new ThicknessAnimation(new Thickness(5), TimeSpan.FromSeconds(0.2));
			addClassButton.BeginAnimation(Canvas.MarginProperty, animate);
		}
    }
}