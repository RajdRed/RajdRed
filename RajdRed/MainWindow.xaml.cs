using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using RajdRed.Models.Adds;
using RajdRed.Repositories;
using RajdRed.Views;
using RajdRed.ViewModels;
using RajdRed.Models;
using System.Collections.Generic;

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

            //AddNewCanvasNod returnerar den noden som skapas
            //_mainRepository.LinjeRepository.AddNewLinje(
            //        _mainRepository.NodCanvasRepository.AddNewCanvasNod(new Point(100, 100)).NodCanvasModel,
            //        _mainRepository.NodCanvasRepository.AddNewCanvasNod(new Point(200, 200)).NodCanvasModel
            //    );
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

			/* kontroll för intersection linjen */

			double Y1, Y2, X1, X2, M, M2;

			Y1 = mouseDownPos.Y;
			Y2 = mouseUpPos.Y;
			X1 = mouseDownPos.X;
			X2 = mouseUpPos.X;
			M = mouseDownPos.Y;
			M2 = mouseUpPos.Y;

			if (SelectionLinesHorizontal(X1, X2, Y1, M))			//Kollar övre linjen av selektionsrutan
				return;
			else if (SelectionLinesHorizontal(X1, X2, Y2, M2))		//Kollar undre linjen av selektionsrutan
				return;
			if (SelectionLinesVertical(X1, Y1, Y2))					//Kollar vänstra linjen av selektionsrutan
				return;
			else if (SelectionLinesVertical(X2, Y1, Y2))			//Kollar högra linjen av selektionsrutan
				return;			
			/************************************/

			foreach (KlassViewModel kvm in _mainRepository.KlassRepository)
			{
				Point leftTopCorner = new Point(kvm.KlassModel.PositionLeft, kvm.KlassModel.PositionTop);
				Point rightTopCorner = new Point(kvm.KlassModel.PositionLeft + kvm.KlassModel.Width, kvm.KlassModel.PositionTop);
				Point leftBotCorner = new Point(kvm.KlassModel.PositionLeft, kvm.KlassModel.PositionTop + kvm.KlassModel.Height);
				Point rightBotCorner = new Point(kvm.KlassModel.PositionLeft + kvm.KlassModel.Width, kvm.KlassModel.PositionTop + kvm.KlassModel.Height);

				if (rightTopCorner.X >= mouseDownPos.X && leftTopCorner.X <= mouseUpPos.X)
				{
					if (rightBotCorner.Y >= mouseDownPos.Y && leftTopCorner.Y <= mouseUpPos.Y)
					{
						kvm.KlassModel.IsSelected = true;
						anyOneSelected = true;
					}
				}
			}
		}

		private bool SelectionLinesHorizontal(double rak_X1, double rak_X2, double rak_Y1, double rak_M)
		{
			foreach (LinjeViewModel lvm in _mainRepository.LinjeRepository)
			{
				double sne_Y1, sne_Y2, sne_K, sne_X1, sne_X2, sne_M;

				if (lvm.LinjeModel.Nod1.PositionLeft > lvm.LinjeModel.Nod2.PositionLeft)
				{
					sne_X1 = lvm.LinjeModel.Nod2.PositionLeft + 5;
					sne_Y1 = lvm.LinjeModel.Nod2.PositionTop + 5;
					sne_Y2 = lvm.LinjeModel.Nod1.PositionTop + 5;
					sne_X2 = lvm.LinjeModel.Nod1.PositionLeft + 5;
				}

				else {
					sne_Y1 = lvm.LinjeModel.Nod1.PositionTop + 5;
					sne_X1 = lvm.LinjeModel.Nod1.PositionLeft + 5;
					sne_Y2 = lvm.LinjeModel.Nod2.PositionTop + 5;
					sne_X2 = lvm.LinjeModel.Nod2.PositionLeft + 5;
				}

				sne_K = (sne_Y2 - sne_Y1) / (sne_X2 - sne_X1);
				sne_M = sne_Y1 - sne_K * sne_X1;

				double intersectX, intersectY;
				intersectX = (rak_M - sne_M) / sne_K;
				intersectY = sne_K * intersectX + sne_M;

				if (rak_X1 <= intersectX && intersectX <= rak_X2)
				{
					if (sne_X1 <= intersectX && intersectX <= sne_X2) 
					{
						if (intersectY == rak_M)
						{
							lvm.LinjeModel.IsSelected = true;
							return true;
						}
					}
				}
			}

			return false;
		}

		private bool SelectionLinesVertical(double rak_X, double rak_Y1, double rak_Y2) 
		{
			foreach (LinjeViewModel lvm in _mainRepository.LinjeRepository)
			{
				double sne_Y1, sne_Y2, sne_K, sne_X1, sne_X2, sne_M;

				if (lvm.LinjeModel.Nod1.PositionLeft > lvm.LinjeModel.Nod2.PositionLeft)
				{
					sne_X1 = lvm.LinjeModel.Nod2.PositionLeft + 5;
					sne_Y1 = lvm.LinjeModel.Nod2.PositionTop + 5;
					sne_Y2 = lvm.LinjeModel.Nod1.PositionTop + 5;
					sne_X2 = lvm.LinjeModel.Nod1.PositionLeft + 5;
				}

				else
				{
					sne_Y1 = lvm.LinjeModel.Nod1.PositionTop + 5;
					sne_X1 = lvm.LinjeModel.Nod1.PositionLeft + 5;
					sne_Y2 = lvm.LinjeModel.Nod2.PositionTop + 5;
					sne_X2 = lvm.LinjeModel.Nod2.PositionLeft + 5;
				}

				sne_K = (sne_Y2 - sne_Y1) / (sne_X2 - sne_X1);
				sne_M = sne_Y1 - sne_K * sne_X1;


				double intersectX, intersectY;

				intersectX = rak_X;
				intersectY = sne_K * intersectX + sne_M;

				if (rak_Y1 <= intersectY && intersectY <= rak_Y2)
				{
					if (sne_Y1 > sne_Y2)	//Går mot nordost
					{
						if (sne_Y2 <= intersectY && intersectY <= sne_Y1)
						{
							lvm.LinjeModel.IsSelected = true;
							return true;
						}
					}

					else                     //Går mot sydost
					{
						if (sne_Y1 <= intersectY && intersectY <= sne_Y2)
						{
							lvm.LinjeModel.IsSelected = true;
							return true;
						}
					}
				}
			}

			return false;
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
    }
}