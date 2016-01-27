using RajdRed.Models;
using RajdRed.Models.Base;

namespace RajdRed.ViewModels
{
    public class LinjeViewModel
    {
        public LinjeModel LinjeModel { get; set; }
        public LinjeViewModel(NodModelBase n1, NodModelBase n2)
        {
            LinjeModel = new LinjeModel(n1, n2);
        }

        public LinjeViewModel()
        {}
    }
}
