using RajdRed.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace RajdRed
{
    /// <summary>
    /// Interaction logic for ClassSettings.xaml
    /// </summary>
    public partial class ClassSettings : UserControl
    {
		private KlassViewModel _kvm;
		private Grid _backgroundGrid;

        public ClassSettings(KlassViewModel kvm, Grid g)
        {
            InitializeComponent();
			_kvm = kvm;
			_backgroundGrid = g;
			DataContext = _kvm.KlassModel;
		}

        //public ClassSettings(KlassView k, Grid g)
        //{
        //    InitializeComponent();
        //    _klass = k;
        //    _backgroundGrid = g;

        //    Canvas.SetZIndex(this, 4);

        //    ClassName.Text = _klass.ClassName.Content.ToString();
        //    Attributes.Text = _klass.Attributes.Text;
        //    Methods.Text = _klass.Methods.Text;

        //    ClassName.Background = _klass.GetMainWindow().Colors.KlassNameBg;
        //    Attributes.Background = _klass.GetMainWindow().Colors.KlassAttributesBg;
        //    Methods.Background = _klass.GetMainWindow().Colors.KlassMethodsBg;

        //    drawNodes();
        //}

        private void drawNodes()
        {
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Är du säker på att du vill ta bort \"" + ClassName.Text + "\"", "Konfirmera borttagning", System.Windows.MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
				_kvm.Delete();
				_kvm.KlassView.CloseSettings(this, _backgroundGrid);
            }
        }

        public void Btn_Abort_Click(object sender, RoutedEventArgs e)
        {
			_kvm.KlassView.CloseSettings(this, _backgroundGrid);
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
			BindingExpression classNameBE = BindingOperations.GetBindingExpression(ClassName, TextBox.TextProperty);
			BindingExpression attributesBE = BindingOperations.GetBindingExpression(Attributes, TextBox.TextProperty);
			BindingExpression methodsBE = BindingOperations.GetBindingExpression(Methods, TextBox.TextProperty);

			classNameBE.UpdateSource();
			attributesBE.UpdateSource();
			methodsBE.UpdateSource();

			_kvm.KlassView.CloseSettings(this, _backgroundGrid);
        }

        private void ClassSettings_Loaded(object sender, RoutedEventArgs e)
        {
            Random rnd = new Random();
            ScaleTransform sct = new ScaleTransform(0, 0);
            gridAnimate.RenderTransformOrigin = new Point(0.5, 0.5);
            gridAnimate.RenderTransform = sct;
            DoubleAnimation da = new DoubleAnimation(1, new Duration(TimeSpan.FromSeconds(0.17)));
            sct.BeginAnimation(ScaleTransform.ScaleXProperty, da);
            sct.BeginAnimation(ScaleTransform.ScaleYProperty, da);
        }
    }
}
