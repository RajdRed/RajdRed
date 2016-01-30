using RajdRed.Models;
using RajdRed.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace RajdRed.Repositories
{
    public class KlassRepository : ObservableCollection<KlassViewModel>
    {

        private MainRepository _mainRepository;
        public MainRepository MainRepository { get { return _mainRepository; } }
        
        public KlassRepository(MainRepository mr)
        {
            _mainRepository = mr;
        }
        public KlassViewModel AddNewKlass(Point startPosition)
        {
            KlassViewModel kvm = new KlassViewModel(startPosition, this);
            Add(kvm);
            return kvm;
        }
    }
}
