using RajdRed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RajdRed.ViewModels
{
    class KlassNodViewModel
    {
        private KlassNodModel _klassNodModel;
        public KlassNodModel KlassNodModel
        {
            get { return _klassNodModel; }
            set { _klassNodModel = value; }
        }

        public KlassNodViewModel()
        {
            _klassNodModel = new KlassNodModel();
        }

        public KlassNodViewModel(KlassNodModel knm)
        {
            _klassNodModel = knm;
        }
    }
}
