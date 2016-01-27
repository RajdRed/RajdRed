using RajdRed.Models;
using RajdRed.Repositories;
using RajdRed.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RajdRed.ViewModels
{
    public class NodKlassViewModel
    {
        public KlassNodView KlassNodView { get; set; }
        public KlassViewModel KlassViewModel { get; set; }
        public NodKlassRepository NodKlassRepository { get; set; }

        private NodKlassModel _nodKlassModel;
        public NodKlassModel NodKlassModel
        {
            get { return _nodKlassModel; }
            set { _nodKlassModel = value; }
        }

        public NodKlassViewModel(NodKlassModel nkm, NodKlassRepository knp)
        {
            NodKlassModel = nkm;
            NodKlassRepository = knp;
        }

        public NodKlassViewModel(){}

        public void SetView(KlassNodView kv) {
            KlassNodView = kv;
        }
    }
}
