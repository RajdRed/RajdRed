using RajdRed.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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

        private void UserControl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
                NodKlassViewModel.CreateLinje(); 
        }

        private void UserControl_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("hej");
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            NodCanvasViewModel ncvm = e.Data.GetData(typeof(NodCanvasViewModel)) as NodCanvasViewModel;
            if (ncvm != null)
            {
                NodKlassViewModel.NodKlassModel.Path = NodKlassViewModel.NodKlassModel.NodTypesModel.Association;
            }

            e.Handled = true;
        }

        protected override void OnDragLeave(DragEventArgs e)
        {
            base.OnDragLeave(e);

            if (Background == Brushes.Blue)
                Background = Brushes.Transparent;

            e.Handled = true;
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);

            NodCanvasViewModel ncvm = e.Data.GetData(typeof(NodCanvasViewModel)) as NodCanvasViewModel;
            if (ncvm != null)
            {
                NodKlassViewModel.AttachLinje(ncvm);
            }

            e.Handled = true;
        }

              
    }
}
