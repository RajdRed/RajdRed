using RajdRed.Models;
using RajdRed.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace RajdRed.Repositories
{
    public class NodCanvasRepository : ObservableCollection<NodCanvasViewModel>
    {
        public NodCanvasRepository()
        {
            Add(new NodCanvasViewModel()
                {
                    NodCanvasModel = new NodCanvasModel()
                    {
                        PositionLeft = 200,
                        PositionTop = 200,
                        OnField = true
                    },
                    NodCanvasRepository = this
                }
            );
        }

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
