using RajdRed.Models;
using RajdRed.Models.Base;
using RajdRed.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace RajdRed.Repositories
{
    public class NodCanvasRepository : ObservableCollection<NodCanvasViewModel>
    {
        MainRepository _mainRepository;
        public MainRepository MainRepository { get { return _mainRepository; } }
        public NodCanvasRepository(MainRepository mr)
        {
            _mainRepository = mr;
        }

        public NodCanvasViewModel AddNewCanvasNod(Point p)
        {
            NodCanvasViewModel nkvm = new NodCanvasViewModel(new NodCanvasModel(p), this);
            Add(nkvm);

            return nkvm;
        }

        public NodCanvasViewModel CreateFromNodModelBase(NodKlassModel n)
        {
            NodCanvasViewModel nkvm = NodCanvasViewModel.CopyNodKlassToNew(n, this);
            Add(nkvm);

            return nkvm;
        }
    }
}
