using RajdRed.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

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

		private void UserControl_MouseEnter(object sender, MouseEventArgs e)
		{
			ScaleTransform trans = new ScaleTransform();
			trans.CenterX = 5;
			trans.CenterY = 5;

			this.RenderTransform = trans;
			// if you use the same animation for X & Y you don't need anim1, anim2 
			DoubleAnimation anim = new DoubleAnimation(1, 1.5, TimeSpan.FromMilliseconds(200));
			trans.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
			trans.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
		}

		private void UserControl_MouseLeave(object sender, MouseEventArgs e)
		{
			ScaleTransform trans = new ScaleTransform();
			trans.CenterX = 6;
			trans.CenterY = 6;
			this.RenderTransform = trans;
			// if you use the same animation for X & Y you don't need anim1, anim2 
			DoubleAnimation anim = new DoubleAnimation(1.5, 1, TimeSpan.FromMilliseconds(200));
			trans.BeginAnimation(ScaleTransform.ScaleXProperty, anim);
			trans.BeginAnimation(ScaleTransform.ScaleYProperty, anim);
		}              
    }
}
