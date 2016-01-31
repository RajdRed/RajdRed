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

        private void Path_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NodKlassViewModel.CreateLinje();
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            base.OnDragEnter(e);

            NodCanvasViewModel ncvm = e.Data.GetData(typeof(NodCanvasViewModel)) as NodCanvasViewModel;
            if (ncvm != null)
            {
                Background = Brushes.Blue;
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
