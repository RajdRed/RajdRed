using RajdRed.Models;
using RajdRed.Models.Base;
using RajdRed.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace RajdRed.Repositories
{
    public class NodCanvasRepository : ObservableCollection<NodCanvasViewModel>
    {
        private bool _hasSelected = false;
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
                        _hasSelected = ncm.NodCanvasModel.IsSelected = true;
                    }
                }
            }

            return _hasSelected;
        }

        public void DeselectAllCanvasNodes()
        {
            if (_hasSelected)
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

        public void DeleteSelected()
        {
            int size = this.Count;
            List<NodCanvasViewModel> deleteEverythingInThisList = new List<NodCanvasViewModel>();

            for (int i = 0; i < size; i++)
                if (this[i].NodCanvasModel.IsSelected)
                    deleteEverythingInThisList.Add(this[i]);

            foreach (NodCanvasViewModel ncvm in deleteEverythingInThisList)
                ncvm.Delete();

            _hasSelected = false;
        }

        public void Select(NodCanvasModel n)
        {
            _hasSelected = n.IsSelected = true;
        }
    }
}
