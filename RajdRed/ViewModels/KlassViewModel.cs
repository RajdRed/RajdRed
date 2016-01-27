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
        public KlassViewModel()
        {
            
        }

        public KlassViewModel(KlassModel km)
        {
            KlassModel = km;
        }

        public void Delete()
        {
            
            KlassRepository.Remove(this);
        }

        public void SetKlassView(KlassView kv)
        {
            KlassView = kv;
        }
        public void SetAdornerLayer(KlassView k)
        {
            aLayer = AdornerLayer.GetAdornerLayer(k);
            aLayer.Add(new ResizingAdorner(k));
            this.KlassModel.IsSelected = true;
        }
    }
}
