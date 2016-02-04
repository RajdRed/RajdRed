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
    }
}
