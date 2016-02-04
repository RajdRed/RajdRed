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
        public LinjeViewModel(LinjeRepository lr, NodModelBase n1, NodModelBase n2)
        {
            LinjeRepository = lr;
            LinjeModel = new LinjeModel(this, n1, n2);
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

        public void Delete()
        {
            LinjeRepository.Remove(this);
        }
    }
}
