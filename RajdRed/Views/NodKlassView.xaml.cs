using RajdRed.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace RajdRed.Views
{
    /// <summary>
    /// Interaction logic for KlassNodView.xaml
    /// </summary>
    public partial class KlassNodView : UserControl
    {
        NodKlassViewModel NodKlassViewModel { get { return DataContext as NodKlassViewModel; } }
        public KlassNodView()
        {
            InitializeComponent();
            Loaded += (sender, eArgs) =>
            {
                NodKlassViewModel.SetView(this);
            };
        }

        private void UserControl_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
        }
    }
}
