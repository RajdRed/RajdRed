using RajdRed.Models;
using RajdRed.Repositories;
using RajdRed.Views;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace RajdRed.ViewModels
{
    public class NodKlassViewModel
    {
        public NodKlassView NodKlassView { get; set; }
        public KlassViewModel KlassViewModel { get; set; }
        public NodKlassRepository NodKlassRepository { get; set; }

        private NodKlassModel _nodKlassModel;
        public NodKlassModel NodKlassModel
        {
            get { return _nodKlassModel; }
            set { _nodKlassModel = value; }
        }

        public NodKlassViewModel(NodKlassModel nkm, KlassViewModel kvm ,NodKlassRepository knp)
        {
            NodKlassModel = nkm;
            NodKlassRepository = knp;
            KlassViewModel = kvm;

            KlassViewModel.KlassModel.PropertyChanged += new PropertyChangedEventHandler(KlassModel_PropertyChanged);
        }

        public NodKlassViewModel(){}

        private void KlassModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (NodKlassModel.IsSet)
            {
                if (e.PropertyName == "PositionLeft" || e.PropertyName == "PositionTop")
                {
                    double x = Canvas.GetLeft(KlassViewModel.KlassView);
                    double y = Canvas.GetTop(KlassViewModel.KlassView);
                    NodKlassModel.PositionLeft = (x + KlassViewModel.KlassView.ActualWidth * NodKlassModel.RPositionLeft) - NodKlassModel.Width / 2;
                    NodKlassModel.PositionTop = (y + KlassViewModel.KlassView.ActualHeight * NodKlassModel.RPositionTop) - NodKlassModel.Height / 2;
                }
            }
        }

        public void SetView(NodKlassView kv) {
            NodKlassView = kv;
        }

        public void TurnToAssosiation()
        {
            NodKlassModel.Geometry = NodKlassModel.NodTypesModel.Association;
        }

        public void TurnToAggregation(bool filled)
        {
            NodKlassModel.Geometry = NodKlassModel.NodTypesModel.Aggregation;
        }

        public void TurnToGeneralization()
        {
            NodKlassModel.Geometry = NodKlassModel.NodTypesModel.Generalization;
        }

        public void TurnToNode()
        {
            NodKlassModel.Geometry = NodKlassModel.NodTypesModel.Node;
        }

        public bool Set(Point p)
        {
            if (!NodKlassModel.IsSet)
            {
                NodKlassModel.PositionLeft = p.X - NodKlassView.Width / 2;
                NodKlassModel.PositionTop = p.Y - NodKlassView.Height / 2;
                NodKlassModel.RPositionLeft = (p.X - Canvas.GetLeft(KlassViewModel.KlassView)) / KlassViewModel.KlassView.ActualWidth;
                NodKlassModel.RPositionTop = (p.Y - Canvas.GetTop(KlassViewModel.KlassView)) / KlassViewModel.KlassView.ActualHeight;
                NodKlassModel.IsSet = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CreateAndAttachCanvasNod(Point p)
        {
            if (Set(p))
            {
                KlassViewModel.KlassRepository.MainRepository.LinjeRepository.AddNewLinje(
                    NodKlassModel,
                    KlassViewModel.KlassRepository.MainRepository.NodCanvasRepository.AddNewCanvasNod(p).NodCanvasModel
                );
            }
        }
    }
}
