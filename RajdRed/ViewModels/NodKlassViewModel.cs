using RajdRed.Models;
using RajdRed.Repositories;
using RajdRed.Views;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

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
            nkm.NodKlassViewModel = this;

            NodKlassRepository = knp;
            KlassViewModel = kvm;
        }

        public NodKlassViewModel(){}

        private void KlassModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (NodKlassModel.IsSet)
            {
                Point p = GetPositionRelativeCanvas();
                NodKlassModel.PositionLeft = p.X;
                NodKlassModel.PositionTop = p.Y;
            }
        }

        public void SetView(NodKlassView kv) {
            NodKlassView = kv;
        }

        public void TurnToAssosiation()
        {
            NodKlassModel.Path = NodKlassModel.NodTypesModel.Association;
        }

        public void TurnToAggregation()
        {
            NodKlassModel.Path = NodKlassModel.NodTypesModel.Aggregation;
        }

        public void TurnToComposition()
        {
            NodKlassModel.Path = NodKlassModel.NodTypesModel.Composition;
        }

        public void TurnToGeneralization()
        {
            NodKlassModel.Path = NodKlassModel.NodTypesModel.Generalization;
        }

        public void TurnToNode()
        {
            NodKlassModel.Path = NodKlassModel.NodTypesModel.Node;
        }

        public bool Set()
        {
            if (!NodKlassModel.IsSet)
            {
                Point p = GetPositionRelativeCanvas();

                NodKlassModel.PositionLeft = p.X;
                NodKlassModel.PositionTop = p.Y;
                NodKlassModel.IsSet = true;

                TurnToAssosiation();

                KlassViewModel.KlassModel.PropertyChanged += new PropertyChangedEventHandler(KlassModel_PropertyChanged);

                return true;
            }

            return false;
        }

        public bool UnSet()
        {
            if (NodKlassModel.IsSet)
            {
                NodKlassModel.IsSet = NodKlassModel.IsSelected = false;
                NodKlassModel.Visible = Visibility.Hidden;
                NodKlassModel.LinjeModelList = new List<LinjeModel>();
                TurnToNode();

                KlassViewModel.KlassModel.PropertyChanged -= KlassModel_PropertyChanged;

                return true;
            }

            return false;
        }

        public void CreateLinje()
        {
            Point p = GetPositionRelativeCanvas();

            if (Set())
            {
                NodCanvasModel n = KlassViewModel.KlassRepository.MainRepository.NodCanvasRepository.AddNewCanvasNod(p).NodCanvasModel;
                LinjeViewModel l = KlassViewModel.KlassRepository.MainRepository.LinjeRepository.AddNewLinje(
                    NodKlassModel,
                    n
                );

                KlassViewModel.KlassRepository.MainRepository.Select(n);
            }
        }

        public void EatNod(NodCanvasViewModel ncvm)
        {
            Set();

            foreach (LinjeModel l in ncvm.NodCanvasModel.LinjeModelList)
            {
                l.ReplaceNod(ncvm.NodCanvasModel, this.NodKlassModel);
                NodKlassModel.LinjeModelList.Add(l);
            }

            KlassViewModel.KlassRepository.MainRepository.NodCanvasRepository.Remove(ncvm);
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

        public void LooseLinje(Point p)
        {
            if (NodKlassModel.IsSet)
            {
                NodCanvasViewModel ncvm = KlassViewModel.KlassRepository.MainRepository.NodCanvasRepository.AddNewCanvasNod(p);
                foreach (LinjeModel l in NodKlassModel.LinjeModelList)
                {
                    l.ReplaceNod(NodKlassModel, ncvm.NodCanvasModel);
                    ncvm.NodCanvasModel.LinjeModelList.Add(l);
                }

                UnSet();
            }
        }

        public void Show()
        {
            NodKlassModel.Visible = Visibility.Visible;
        }

        public void Hide()
        {
            NodKlassModel.Visible = Visibility.Hidden;
        }
    }
}
