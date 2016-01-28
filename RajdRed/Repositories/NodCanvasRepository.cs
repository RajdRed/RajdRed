using RajdRed.Models;
using RajdRed.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace RajdRed.Repositories
{
    public class NodCanvasRepository : ObservableCollection<NodCanvasViewModel>
    {
        public NodCanvasRepository(){}

        public void AddNewCanvasNod(Point p)
        {
            Add(new NodCanvasViewModel() {
                NodCanvasModel = new NodCanvasModel()
                {
                    PositionLeft = p.X,
                    PositionTop = p.Y
                },
                NodCanvasRepository = this
            });
        }
    }
}
