using RajdRed.Models;
using RajdRed.Models.Base;
using RajdRed.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RajdRed.Repositories
{
    public class LinjeRepository : ObservableCollection<LinjeViewModel>
    {
        public LinjeRepository(){}

        public void AddNewLinje(NodModelBase n1, NodModelBase n2)
        {
            Add(new LinjeViewModel()
            {
                LinjeModel = new LinjeModel(n1, n2)
                {
                    Nod1 = n1,
                    Nod2 = n2
                }
            });
        }
    }
}
