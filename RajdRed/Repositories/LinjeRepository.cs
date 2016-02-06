using RajdRed.Models;
using RajdRed.Models.Base;
using RajdRed.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RajdRed.Repositories
{
    public class LinjeRepository : ObservableCollection<LinjeViewModel>
    {
        private int _numberOfSelected = 0;
        MainRepository _mainRepository;
        public MainRepository MainRepository { get { return _mainRepository; } }

        public LinjeRepository(MainRepository mr)
        {
            _mainRepository = mr;
        }

        public LinjeViewModel AddNewLinje(NodModelBase n1, NodModelBase n2)
        {
            LinjeViewModel lvm = new LinjeViewModel(this, n1, n2);

            n1.LinjeModelList.Add(lvm.LinjeModel);
            n2.LinjeModelList.Add(lvm.LinjeModel);

            Add(lvm);

            return lvm;
        }

        public void DeselectAllLines()
        {
            if (HasSelected())
            {
                foreach (LinjeViewModel l in this)
                {
                    if (l.LinjeModel.IsSelected)
                    {
                        l.LinjeModel.IsSelected = false;
                    }
                }
            }
        }

        public void DeleteSelected()
        {
            int size = this.Count;
            List<LinjeViewModel> deleteEverythingInThisList = new List<LinjeViewModel>();

            for (int i = 0; i < size; i++)
                if (this[i].LinjeModel.IsSelected)
                    deleteEverythingInThisList.Add(this[i]);

            foreach (LinjeViewModel lvm in deleteEverythingInThisList)
                lvm.Delete();

            _numberOfSelected = 0;
        }

        public bool HasSelected()
        {
            return (_numberOfSelected != 0 ? true : false);
        }

        public void Select(LinjeModel l)
        {
            l.LinjeViewModel.Select();
        }

        public void Deselect(LinjeModel l)
        {
            l.LinjeViewModel.Deselect();
        }

        public void IncreaseSelected()
        {
            _numberOfSelected++;
        }

        public void DecreaseSelected()
        {
            _numberOfSelected--;
        }
    }
}
