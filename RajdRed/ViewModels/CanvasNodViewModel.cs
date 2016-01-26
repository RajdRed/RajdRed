using RajdRed.Models;
using RajdRed.Repositories;
using RajdRed.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RajdRed.ViewModels
{
    public class CanvasNodViewModel
    {
        public CanvasNodView CanvasNodView { get; set; }
        public CanvasNodRepository CanvasNodRepository { get; set; }
        public CanvasNodModel CanvasNodModel { get; set; }

        public CanvasNodViewModel()
        {
            CanvasNodModel = new CanvasNodModel();
        }

        public CanvasNodViewModel(CanvasNodModel cnm)
        {
            CanvasNodModel = cnm;
        }

        public void SetCanvasNodView(CanvasNodView cnv)
        {
            CanvasNodView = cnv;
        }
    }
}
