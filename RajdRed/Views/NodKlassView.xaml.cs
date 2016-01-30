using RajdRed.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace RajdRed.Views
{
    /// <summary>
    /// Interaction logic for KlassNodView.xaml
    /// </summary>
    public partial class NodKlassView : UserControl
    {
        NodKlassViewModel NodKlassViewModel { get { return DataContext as NodKlassViewModel; } }
        public NodKlassView()
        {
            InitializeComponent();
            Loaded += (sender, eArgs) =>
            {
                if (NodKlassViewModel != null)
                {
                    NodKlassViewModel.SetView(this);
                }
            };
        }

        private void Path_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            NodKlassViewModel.CreateAndAttachCanvasNod(e.GetPosition(Application.Current.MainWindow));
        }
    }
}
