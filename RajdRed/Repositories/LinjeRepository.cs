using RajdRed.Models;
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
        public LinjeRepository()
        {
            Add(new LinjeViewModel() {
                LinjeModel = new LinjeModel()
                {
                    X1 = 20,
                    Y1 = 20,
                    X2 = 400,
                    Y2 = 400
                }
            });
        }

        public void AddNewLinje()
        {

        }
    }
}
