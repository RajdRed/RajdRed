using RajdRed.Models;
using RajdRed.Repositories;
using RajdRed.ViewModells.Add;
using RajdRed.ViewModels.Commands;
using RajdRed.Views;
using System.Windows.Controls;
using System.Windows.Documents;

namespace RajdRed.ViewModels
{
    public class KlassViewModel
    {
        AdornerLayer aLayer;
        
        public KlassModel KlassModel { get; set; }
        public KlassView KlassView { get; set; }
        public KlassRepository KlassRepository { get; set; }
        public NodKlassRepository NodKlassRepository { get; set; }

        public KlassViewModel(KlassModel km, KlassRepository kr)
        {
            KlassModel = km;
            KlassRepository = kr;      
        }

        public KlassViewModel(KlassModel km)
        {
            KlassModel = km;
        }

        public KlassViewModel(){}

        public void Delete()
        {
            
            KlassRepository.Remove(this);
        }

        public void SetKlassView(KlassView kv)
        {
            KlassView = kv;
        }

        public void SetAdornerLayer()
        {
            aLayer = AdornerLayer.GetAdornerLayer(this.KlassView);
            aLayer.Add(new ResizingAdorner(this.KlassView));
        }
    }
}
