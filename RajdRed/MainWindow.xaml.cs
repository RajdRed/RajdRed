using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using RajdRed.Models.Adds;
using RajdRed.Repositories;
using RajdRed.ViewModels;
using System.Collections.Generic;
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
        public bool isArchiveMenuActive = false;
		public bool isSettingsMenuActive = false;
		public RajdColors Colors = new RajdColors(RajdColorScheme.Light);
		private bool darkMode = false;
		private Point mouseDownPos;
		public bool anyOneSelected = false;

        public MainRepository _mainRepository;
		
        public MainWindow()
        {
            InitializeComponent();
            _mainRepository = new MainRepository(this);
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
			deselectAllClasses();
            _mainRepository.KlassRepository.AddNewKlass(e.GetPosition(Application.Current.MainWindow));
            anyOneSelected = true;
        }

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
				if (isSettingsMenuActive)
				{
					theCanvas.Children.Remove(settingsMenu);
					isSettingsMenuActive = false;
					settingsMenuBtn.SetCurrentValue(Control.BackgroundProperty, Brushes.Transparent);
				}

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
			//Keyboard.ClearFocus();

			deselectAllClasses();	

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

		private void theCanvas_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
		{
			/************  För selectionverktyget  ***************/

			mouseDownPos = e.GetPosition(theCanvas);

			if (mouseDownPos.Y > 100 && Mouse.Captured == null)
			{
				theCanvas.CaptureMouse();

				Canvas.SetLeft(selectionBox, mouseDownPos.X);
				Canvas.SetTop(selectionBox, mouseDownPos.Y);
				selectionBox.Width = 0;
				selectionBox.Height = 0;

				selectionBox.Visibility = Visibility.Visible;

				theCanvas.MouseMove += (sendr, eventArgs) => {
					Point mousePos = e.GetPosition(theCanvas);

					if (mouseDownPos.X < mousePos.X)
					{
						Canvas.SetLeft(selectionBox, mouseDownPos.X);
						selectionBox.Width = mousePos.X - mouseDownPos.X;
					}
					else
					{
						Canvas.SetLeft(selectionBox, mousePos.X);
						selectionBox.Width = mouseDownPos.X - mousePos.X;
					}

					if (mouseDownPos.Y < mousePos.Y)
					{
						Canvas.SetTop(selectionBox, mouseDownPos.Y);
						selectionBox.Height = mousePos.Y - mouseDownPos.Y;
					}
					else
					{
						Canvas.SetTop(selectionBox, mousePos.Y);
						selectionBox.Height = mouseDownPos.Y - mousePos.Y;
					}
				};
			}
		}

		private void theCanvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
		{
			theCanvas.ReleaseMouseCapture();
			selectionBox.Visibility = Visibility.Collapsed;

			Point mouseUpPos = e.GetPosition(theCanvas);

			/*Musen har släppts - Kolla om det är finns några element innanför mouseUpPos och mouseDownPos*/
			if (mouseDownPos.X > mouseUpPos.X)
			{
				double temp = mouseDownPos.X;
				mouseDownPos.X = mouseUpPos.X;
				mouseUpPos.X = temp;
			}

			if (mouseDownPos.Y > mouseUpPos.Y)
			{
				double temp = mouseDownPos.Y;
				mouseDownPos.Y = mouseUpPos.Y;
				mouseUpPos.Y = temp;
			}


            //Checks if intersect with RajdElements on Canvas
            anyOneSelected = _mainRepository.CheckIfHit(mouseDownPos, mouseUpPos);

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
				if (isArchiveMenuActive)
				{
					theCanvas.Children.Remove(archiveMenu);
					isArchiveMenuActive = false;
					archiveMenuBtn.SetCurrentValue(Control.BackgroundProperty, Brushes.Transparent);
				}

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

			if ((pt.X < theCanvas.ActualWidth - 150 || pt.Y > 190) || (pt.X < theCanvas.ActualWidth - 78 && pt.Y < theCanvas.ActualWidth - 96)) 
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
				var uri = new Uri("pack://application:,,,/Images/createClassBg-Dark.png");
				var bitmap = new BitmapImage(uri);
				addClassButton.Source = bitmap;	
			}

			else 
			{
				var uri = new Uri("pack://application:,,,/Images/createClassBg.png");
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
			ThicknessAnimation animate = new ThicknessAnimation(new Thickness(0), TimeSpan.FromSeconds(0.1));
			addClassButton.BeginAnimation(Canvas.MarginProperty, animate);
		}

		private void addClassButton_MouseLeave(object sender, MouseEventArgs e)
		{
			ThicknessAnimation animate = new ThicknessAnimation(new Thickness(2), TimeSpan.FromSeconds(0.1));
			addClassButton.BeginAnimation(Canvas.MarginProperty, animate);
		}

		public void deselectAllClasses()
		{
			foreach (KlassViewModel k in _mainRepository.KlassRepository)
			{
				if (k.KlassModel.IsSelected)
				{
					k.KlassModel.IsSelected = false;
					k.KlassView.ReleaseMouseCapture();
				}
			}

			anyOneSelected = false;
		}

		private void RajdRedMainWindow_KeyDown(object sender, KeyEventArgs k)
		{
			if (k.Key == Key.Delete || k.Key == Key.Back )
			{
				if (anyOneSelected)
				{
					int size = _mainRepository.KlassRepository.Count;
					List<KlassViewModel> deleteEverythingInThisList = new List<KlassViewModel>();

					for (int i = 0; i < size; i++)
						if (_mainRepository.KlassRepository[i].KlassModel.IsSelected)
							deleteEverythingInThisList.Add(_mainRepository.KlassRepository[i]);

					foreach (KlassViewModel kvm in deleteEverythingInThisList)
						kvm.Delete();

					anyOneSelected = false;
				}
			}
		}

        private void Line_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Line line = sender as Line;
            LinjeViewModel l = line.DataContext as LinjeViewModel;
            l.Split(e.GetPosition(this));
        }
    }
}