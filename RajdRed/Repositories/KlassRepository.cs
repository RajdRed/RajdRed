using RajdRed.Models;
using RajdRed.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace RajdRed.Repositories
{
    public class KlassRepository : ObservableCollection<KlassViewModel>
    {
        private bool _hasSelected = false;

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

        public bool CheckIfHit(Point mouseDownPos, Point mouseUpPos)
        {
            foreach (KlassViewModel kvm in this)
            {
                Point leftTopCorner = new Point(kvm.KlassModel.PositionLeft, kvm.KlassModel.PositionTop);
                Point rightTopCorner = new Point(kvm.KlassModel.PositionLeft + kvm.KlassModel.Width, kvm.KlassModel.PositionTop);
                Point leftBotCorner = new Point(kvm.KlassModel.PositionLeft, kvm.KlassModel.PositionTop + kvm.KlassModel.Height);
                Point rightBotCorner = new Point(kvm.KlassModel.PositionLeft + kvm.KlassModel.Width, kvm.KlassModel.PositionTop + kvm.KlassModel.Height);

                if (rightTopCorner.X >= mouseDownPos.X && leftTopCorner.X <= mouseUpPos.X)
                {
                    if (rightBotCorner.Y >= mouseDownPos.Y && leftTopCorner.Y <= mouseUpPos.Y)
                    {
                        _hasSelected = kvm.KlassModel.IsSelected = true;
                    }
                }
            }

            return _hasSelected;
        }

        public void DeselectAllClasses()
        {
            if (_hasSelected)
            {
                foreach (KlassViewModel k in this)
                {
                    if (k.KlassModel.IsSelected)
                    {
                        k.KlassModel.IsSelected = false;
                    }
                }
            }
        }

        public void DeleteSelected()
        {
            int size = this.Count;
            List<KlassViewModel> deleteEverythingInThisList = new List<KlassViewModel>();

            for (int i = 0; i < size; i++)
                if (this[i].KlassModel.IsSelected)
                    deleteEverythingInThisList.Add(this[i]);

            foreach (KlassViewModel kvm in deleteEverythingInThisList)
                kvm.Delete();

            _hasSelected = false;
        }

        public void Select(KlassModel k)
        {
            _hasSelected = k.IsSelected = true;
        }
    }
}
