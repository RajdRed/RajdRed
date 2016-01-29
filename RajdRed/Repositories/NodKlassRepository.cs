using RajdRed.Models;
using RajdRed.ViewModels;
using System.Collections.ObjectModel;

namespace RajdRed.Repositories
{
    public class NodKlassRepository : ObservableCollection<NodKlassViewModel>
    {
        public NodKlassRepository(KlassViewModel kvm)
        {
            for (int i = 1; i <= 4; i++)
                Add(new NodKlassViewModel(new NodKlassModel() { GridColumn = 0, GridRow = i }, kvm, this));

            for (int i = 1; i <= 4; i++)
                Add(new NodKlassViewModel(new NodKlassModel() { GridColumn = 5, GridRow = i }, kvm, this));

            for (int i = 1; i <= 4; i++)
                Add(new NodKlassViewModel(new NodKlassModel() { GridColumn = i, GridRow = 0 }, kvm, this));

            for (int i = 1; i <= 4; i++)
                Add(new NodKlassViewModel(new NodKlassModel() { GridColumn = i, GridRow = 5 }, kvm, this));
        }
    }
}
