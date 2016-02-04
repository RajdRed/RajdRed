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

        public bool CheckIfHit(Point mouseDownPos, Point mouseUpPos)
        {
            bool anyOneSelected = false;
            foreach (NodCanvasViewModel ncm in this)
            {
                Point leftTopCorner = new Point(ncm.NodCanvasModel.PositionLeft, ncm.NodCanvasModel.PositionTop);
                Point rightTopCorner = new Point(ncm.NodCanvasModel.PositionLeft + ncm.NodCanvasModel.Width, ncm.NodCanvasModel.PositionTop);
                Point leftBotCorner = new Point(ncm.NodCanvasModel.PositionLeft, ncm.NodCanvasModel.PositionTop + ncm.NodCanvasModel.Height);
                Point rightBotCorner = new Point(ncm.NodCanvasModel.PositionLeft + ncm.NodCanvasModel.Width, ncm.NodCanvasModel.PositionTop + ncm.NodCanvasModel.Height);

                if (rightTopCorner.X >= mouseDownPos.X && leftTopCorner.X <= mouseUpPos.X)
                {
                    if (rightBotCorner.Y >= mouseDownPos.Y && leftTopCorner.Y <= mouseUpPos.Y)
                    {
                        ncm.NodCanvasModel.IsSelected = true;
                        anyOneSelected = true;
                    }
                }
            }

            return anyOneSelected;
        }

        public void DeselectAllCanvasNodes()
        {
            foreach (NodCanvasViewModel n in this)
            {
                if (n.NodCanvasModel.IsSelected)
                {
                    n.NodCanvasModel.IsSelected = false;
                }
            }
        }
    }
}
