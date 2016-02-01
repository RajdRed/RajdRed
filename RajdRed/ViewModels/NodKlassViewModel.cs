using RajdRed.Models;
using RajdRed.Repositories;
using RajdRed.ViewModels.Base;
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
                    Point p = GetPositionRelativeCanvas();
                    NodKlassModel.PositionLeft = p.X;
                    NodKlassModel.PositionTop = p.Y;
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

        public bool Set()
        {
            if (!NodKlassModel.IsSet)
            {
                Point p = GetPositionRelativeCanvas();

                NodKlassModel.PositionLeft = p.X;
                NodKlassModel.PositionTop = p.Y;
                NodKlassModel.IsSet = true;

                return true;
            }

            return false;
        }

        public void CreateLinje()
        {
            Point p = GetPositionRelativeCanvas();

            if (Set())
            {
                KlassViewModel.KlassRepository.MainRepository.LinjeRepository.AddNewLinje(
                    NodKlassModel,
                    KlassViewModel.KlassRepository.MainRepository.NodCanvasRepository.AddNewCanvasNod(p).NodCanvasModel
                );
            }
        }

        public void EatNodCanvas(NodCanvasViewModel ncvm)
        {
            if (Set())
            {
                LinjeModel oldLine = ncvm.NodCanvasModel.LinjeModel;
                LinjeViewModel newLine = KlassViewModel.KlassRepository.MainRepository.LinjeRepository.AddNewLinje(
                        ncvm.NodCanvasModel,
                        this.NodKlassModel
                    );

                Point newNodPos = NodViewModelBase.CenterBetweenNodes(oldLine.Nod1, oldLine.Nod2);

                ncvm.NodCanvasModel.PositionLeft = newNodPos.X;
                ncvm.NodCanvasModel.PositionTop = newNodPos.Y;

            };
        }

        public Point GetPositionRelativeCanvas()
        {
            return NodKlassView.TransformToAncestor(Application.Current.MainWindow).Transform(new Point(0, 0));
        }

        public bool IsInArea(Point p)
        {
            Point ThisPosition = GetPositionRelativeCanvas();

            if ((p.X >= ThisPosition.X && p.Y >= ThisPosition.Y)
                && (p.X <= ThisPosition.X + NodKlassModel.Width && p.Y <= ThisPosition.Y + NodKlassModel.Height))
                return true;

            return false;
        }

    }
}
