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
	/// Interaction logic for SettingsMenu.xaml
	/// </summary>
	public partial class SettingsMenu : UserControl
	{
		public SettingsMenu()
		{
			InitializeComponent();
		}

		private void darkModeButton_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mw = (MainWindow)Application.Current.MainWindow;

			if (txtBlockColor.Text == "Dark Mode") {
				mw.changeColors(false);
				mw.ChangeColorTheme(true);
				txtBlockColor.Text = "Light Mode";
			}

			else {
				mw.changeColors(true);
				mw.ChangeColorTheme(false);
				txtBlockColor.Text = "Dark Mode";
			}
		}

		private void AboutProgram_Click(object sender, RoutedEventArgs e)
		{
			MainWindow mw = (MainWindow)Application.Current.MainWindow;
			AboutWindow aboutWindow = new AboutWindow();
			aboutWindow.Owner = mw;
			aboutWindow.Show();

			mw.theCanvas.Children.Remove(this);
			mw.isSettingsMenuActive = false;
			mw.settingsMenuBtn.SetCurrentValue(Control.BackgroundProperty, Brushes.Transparent);
		}
	}
}
