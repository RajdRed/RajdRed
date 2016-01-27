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
        }
    }
}
