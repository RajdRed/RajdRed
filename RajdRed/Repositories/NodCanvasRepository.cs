using RajdRed.Models;
using RajdRed.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace RajdRed.Repositories
{
    public class NodCanvasRepository : ObservableCollection<NodCanvasViewModel>
    {
        public NodCanvasRepository(){}

        public NodCanvasViewModel AddNewCanvasNod(Point p)
        {
            NodCanvasViewModel nkvm = new NodCanvasViewModel(new NodCanvasModel(p), this);
            Add(nkvm);

            return nkvm;
        }
    }
}
