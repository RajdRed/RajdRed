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

        public bool CheckIfHit(Point mouseDownPos, Point mouseUpPos)
        {
            bool anyOneSelected = false;

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
                        kvm.KlassModel.IsSelected = true;
                        anyOneSelected = true;
                    }
                }
            }

            return anyOneSelected;
        }
    }
}
