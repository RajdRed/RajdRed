using RajdRed.Models;
using RajdRed.Models.Base;
using RajdRed.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace RajdRed.Repositories
{
    public class KlassRepository : ObservableCollection<KlassViewModel>
    {
        private int _numberOfSelected = 0;

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

		public int CheckIfHit(Point mouseDownPos, Point mouseUpPos, ref List<NodModelBase> nodList)
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
                        kvm.KlassModel.IsSelected = true;
                        _numberOfSelected++;

						foreach (NodKlassViewModel nod in kvm.NodKlassRepository)
						{
							if (nod.NodKlassModel.IsSet)
							{
								nod.NodKlassModel.IsSelected = true;
								nodList.Add(nod.NodKlassModel);
							}
						}
                    }
                }
            }

            return _numberOfSelected;
        }

        public void DeselectAllClasses()
        {
            if (HasSelected())
            {
                foreach (KlassViewModel k in this)
                {
                    if (k.KlassModel.IsSelected)
                    {
                        k.Deselect();
                    }

					foreach (NodKlassViewModel nvkm in k.NodKlassRepository)
					{
                        if (nvkm.NodKlassModel.IsSet)
                            nvkm.Deselect();
					}
                }
            }

            _numberOfSelected = 0;
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

            _numberOfSelected = 0;
        }        

        public void ShowAllKlassNodes()
        {
            foreach (KlassViewModel k in this)
                k.ShowNodes();
        }

        public void HideAllKlassNodes()
        {
            foreach (KlassViewModel k in this)
                k.HideNodes();
        }

        public KlassViewModel GetKlassByPoint(Point p)
        {
            foreach (KlassViewModel k in this) {
                if (k.KlassModel.PositionLeft <= p.X && k.KlassModel.PositionTop <= p.Y
                    && (k.KlassModel.PositionLeft + k.KlassModel.Width) >= p.X
                    && (k.KlassModel.PositionTop + k.KlassModel.Height) >= p.Y)
                {
                    return k;
                }
            }

            return null;
        }

        public bool HasSelected()
        {
            return (_numberOfSelected != 0 ? true : false);
        }

        public void IncreaseSelected()
        {
            _numberOfSelected++;
        }

        public void DecreaseSelected()
        {
            _numberOfSelected--;
        }

        public void Select(KlassModel k)
        {
            List<NodModelBase> nodList = new List<NodModelBase>();

            foreach (NodKlassViewModel n in k.KlassViewModel.NodKlassRepository)
            {
                if (n.IsSet())
                {
                    n.Select();
                    nodList.Add(n.NodKlassModel);
                }
            }

            _mainRepository.SelectLinesOfNod(ref nodList);
            k.KlassViewModel.Select();
        }

        public void Deselect(KlassModel k)
        {
            List<NodModelBase> nodList = new List<NodModelBase>();

            foreach (NodKlassViewModel n in k.KlassViewModel.NodKlassRepository)
            {
                if (n.IsSet())
                {
                    n.Deselect();
                    nodList.Add(n.NodKlassModel);
                }
            }

            _mainRepository.DeselectLinesOfNod(ref nodList);
            k.KlassViewModel.Deselect();
        }
    }
}
