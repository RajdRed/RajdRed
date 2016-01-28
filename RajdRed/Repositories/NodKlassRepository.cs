using RajdRed.Models;
using RajdRed.ViewModels;
using System.Collections.ObjectModel;

namespace RajdRed.Repositories
{
    public class NodKlassRepository : ObservableCollection<NodKlassViewModel>
    {
        public NodKlassRepository(KlassViewModel kvm)
        {
            for (int i = 0; i < 2; i++)
                Add(new NodKlassViewModel(new NodKlassModel()
                    {
                        GridColumn = 2,
                        GridRow = 0
                    }, kvm, this)
                );
        }
    }
}
