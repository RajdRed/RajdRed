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
            LinjeModel lm = new LinjeModel(n1, n2) { Nod1 = n1, Nod2 = n2 };
            LinjeViewModel lvm = new LinjeViewModel(this) { LinjeModel = lm };

            n1.LinjeModelList.Add(lm);
            n2.LinjeModelList.Add(lm);

            Add(lvm);

            return lvm;
        }
    }
}
