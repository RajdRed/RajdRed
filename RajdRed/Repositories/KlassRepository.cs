using RajdRed.Models;
using RajdRed.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace RajdRed.Repositories
{
    public class KlassRepository : ObservableCollection<KlassViewModel>
    {
        public KlassRepository(){}
        public KlassViewModel AddNewKlass(Point startPosition)
        {
            KlassViewModel kvm = new KlassViewModel(new KlassModel()
                    {
                        Header = "Ny Klass *",
                        PositionLeft = startPosition.X,
                        PositionTop = startPosition.Y,
                        IsSelected = true
                    }, this);
            Add(kvm);

            return kvm;
        }
    }
}
