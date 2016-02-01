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
using RajdRed.Models.Adds;
using RajdRed.ViewModels;

namespace RajdRed
{
    /// <summary>
    /// Interaction logic for NodSettings.xaml
    /// </summary>
    public partial class NodSettings : UserControl
    {
        NodTypesModel ntm = new NodTypesModel();
        NodKlassViewModel NodKlassViewModel;
        MainWindow mw = (MainWindow)Application.Current.MainWindow;
        public NodSettings(NodKlassViewModel nkvm)
        {
            InitializeComponent();
            DataContext = ntm;
            NodKlassViewModel = nkvm;
        }

        private void Association_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NodKlassViewModel.NodKlassModel.Path = ntm.Association;
            mw.theCanvas.Children.Remove(this);
        }

        private void Aggregation_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NodKlassViewModel.NodKlassModel.Path = ntm.Aggregation;
            mw.theCanvas.Children.Remove(this);
        }

        private void Composition_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NodKlassViewModel.NodKlassModel.Path = ntm.Node;
            mw.theCanvas.Children.Remove(this);
        }

        private void Generalization_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            NodKlassViewModel.NodKlassModel.Path = ntm.Generalization;
            mw.theCanvas.Children.Remove(this);
        }
    }
}
