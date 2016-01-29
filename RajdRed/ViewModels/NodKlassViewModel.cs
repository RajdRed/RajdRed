using RajdRed.Models;
using RajdRed.Repositories;
using RajdRed.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

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

        public NodKlassViewModel(NodKlassModel nkm, KlassViewModel kvm, NodKlassRepository knp)
        {
            NodKlassModel = nkm;
            NodKlassRepository = knp;
            KlassViewModel = kvm;
        }

        public NodKlassViewModel(){}

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
    }
}
