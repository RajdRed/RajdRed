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
                    SetValue(Grid.ColumnProperty, NodKlassViewModel.NodKlassModel.GridColumn);
                    SetValue(Grid.RowProperty, NodKlassViewModel.NodKlassModel.GridRow);
                }
            };
        }
    }
}
