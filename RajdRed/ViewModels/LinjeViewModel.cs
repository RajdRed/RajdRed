using RajdRed.Models;
using RajdRed.Models.Base;
using RajdRed.Repositories;
using System.Windows;

namespace RajdRed.ViewModels
{
    public class LinjeViewModel
    {
        public LinjeModel LinjeModel { get; set; }
        public LinjeRepository LinjeRepository { get; set; }
        public LinjeViewModel(LinjeRepository lr)
        {
            LinjeRepository = lr;
        }

        public LinjeViewModel()
        {}

        public void Split(Point p)
        {
            NodCanvasViewModel ncvm = LinjeRepository.MainRepository.NodCanvasRepository.AddNewCanvasNod(p);
            LinjeRepository.AddNewLinje(LinjeModel.Nod1, ncvm.NodCanvasModel);
            LinjeRepository.AddNewLinje(LinjeModel.Nod2, ncvm.NodCanvasModel);

            LinjeRepository.Remove(this);
        }
    }
}
