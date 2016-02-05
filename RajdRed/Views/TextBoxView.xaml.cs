using RajdRed.ViewModels;
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

namespace RajdRed.Views
{
    /// <summary>
    /// Interaction logic for TextBoxView.xaml
    /// </summary>
    public partial class TextBoxView : UserControl
    {
        TextBoxViewModel TextBoxViewModel { get { return DataContext as TextBoxViewModel; } }
        public TextBoxView()
        {
            InitializeComponent();
            Loaded += (sender, args) =>
            {
                TextBoxViewModel.SetView(this);
            };
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.SizeAll;
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBoxViewModel.TextBoxRepository.MainRepository.Select(TextBoxViewModel.TextBoxModel);
        }
    }
}
