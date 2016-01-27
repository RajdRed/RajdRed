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
using System.Windows.Shapes;
 
 namespace RajdRed
 {
	/// <summary>
 	/// Interaction logic for HelpWindow.xaml
 	/// </summary>
	public partial class HelpWindow : Window
 	{
		public HelpWindow()
 		{
 			InitializeComponent();
 		}
 
 		private void setColors(object sender, RoutedEventArgs e)
 		{
 			MainWindow mw = (MainWindow)Application.Current.MainWindow;
 			bool dark = mw.getDarkMode();
 
 			if (dark)
 			{
 				menuTopLeft.Fill = (Brush)new BrushConverter().ConvertFrom("#151515");
 				menuBtnsBg.SetCurrentValue(Control.BackgroundProperty, (Brush)new BrushConverter().ConvertFrom("#151515"));
 				menuBot.SetCurrentValue(Control.BackgroundProperty, (Brush)new BrushConverter().ConvertFrom("#333"));
 			}
 
 			else
 			{
 				menuTopLeft.Fill = (Brush)new BrushConverter().ConvertFrom("#222931");
 				menuBtnsBg.SetCurrentValue(Control.BackgroundProperty, (Brush)new BrushConverter().ConvertFrom("#222931"));
 				menuBot.SetCurrentValue(Control.BackgroundProperty, (Brush)new BrushConverter().ConvertFrom("#4f5b6d"));
 			}
 		}
 
 		private void Ellipse_CloseWindow(object sender, MouseButtonEventArgs e)
 		{
 			this.Close();
 		}
 
 		private void WindowDragAndMove(object sender, MouseButtonEventArgs e)
 		{
 			DragMove();
 		}
 	}
 }