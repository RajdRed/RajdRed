using RajdRed.Models;
using RajdRed.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace RajdRed.Repositories
{
    public class CanvasNodRepository : ObservableCollection<CanvasNodViewModel>
    {
        public CanvasNodRepository()
        {
            Add(new CanvasNodViewModel()
                {
                    CanvasNodModel = new CanvasNodModel()
                    {
                        PositionLeft = 200,
                        PositionTop = 200,
                        OnField = true
                    },
                    CanvasNodRepository = this
                }
            );
        }

        public void AddNewCanvasNod(Point p)
        {
            Add(new CanvasNodViewModel() {
                CanvasNodModel = new CanvasNodModel()
                {
                    PositionLeft = p.X,
                    PositionTop = p.Y
                },
                CanvasNodRepository = this
            });
        }
    }
}
