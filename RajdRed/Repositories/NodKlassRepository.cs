using RajdRed.Models;
using RajdRed.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace RajdRed.Repositories
{
    public class NodKlassRepository : ObservableCollection<NodKlassViewModel>
    {
        public NodKlassRepository(KlassViewModel kvm)
        {
            //left
            for (int i = 1; i <= 4; i++)
                Add(new NodKlassViewModel(new NodKlassModel() {
                    Row = i,
                    Column = 0
                }, kvm, this));

            //right
            for (int i = 1; i <= 4; i++)
                Add(new NodKlassViewModel(new NodKlassModel()
                {
                    Row = i,
                    Column = 5
                }, kvm, this));

            //top
            for (int i = 1; i <= 4; i++)
                Add(new NodKlassViewModel(new NodKlassModel()
                {
                    Row = 0,
                    Column = i
                }, kvm, this));

            //bottom
            for (int i = 1; i <= 4; i++)
                Add(new NodKlassViewModel(new NodKlassModel()
                {
                    Row = 5,
                    Column = i
                }, kvm, this));
        }
    }
}
