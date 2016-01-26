using RajdRed.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RajdRed.ViewModels
{
    public class LinjeViewModel
    {
        public LinjeModel LinjeModel { get; set; }
        public LinjeViewModel()
        {
            LinjeModel = new LinjeModel();
        }
    }
}
