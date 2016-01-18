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
using System.IO;
using System.IO.Packaging;
using System.Windows.Xps.Packaging;
using System.Windows.Xps;

namespace RajdRed
{
    /// <summary>
    /// Interaction logic for ArchiveMenu.xaml
    /// </summary>
    public partial class ArchiveMenu : UserControl
    {
        public ArchiveMenu()
        {
            InitializeComponent();
        }

		private void exitButton_click(object sender, RoutedEventArgs e)
		{
			Application.Current.Shutdown();
		}

		private void newButton_click(object sender, RoutedEventArgs e)
		{
		}

		private void saveButton_click(object sender, RoutedEventArgs e)
		{
			MainWindow mw = (MainWindow)Application.Current.MainWindow;
			Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
			dlg.FileName = mw.TitleTextBox.Text; // Default file name
			dlg.DefaultExt = ".pdf"; // Default file extension
			dlg.Filter = "PDF documents (.pdf)|*.pdf"; // Filter files by extension

			// Show save file dialog box
			Nullable<bool> result = dlg.ShowDialog();

			// Process save file dialog box results
			if (result == true)
			{
				// Save document
				string filename = dlg.FileName;
				writeFile(filename);
			}
		}

		private void writeFile(string filename)
		{
			MainWindow mw = (MainWindow)Application.Current.MainWindow;

			mw.theCanvas.Children.Remove(this);
			mw.isArchiveMenuActive = false;
			mw.archiveMenuBtn.SetCurrentValue(Control.BackgroundProperty, Brushes.Transparent);

			MemoryStream lMemoryStream = new MemoryStream();
			Package package = Package.Open(lMemoryStream, FileMode.Create);
			XpsDocument doc = new XpsDocument(package);
			XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
			writer.Write(mw.theCanvas);
			doc.Close();
			package.Close();

			var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(lMemoryStream);
			PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, filename, 0);
		}
    }
}
