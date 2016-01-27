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
    public class NodCanvasViewModel
    {
        public CanvasNodView CanvasNodView { get; set; }
        public NodCanvasRepository NodCanvasRepository { get; set; }
        public NodCanvasModel NodCanvasModel { get; set; }

        public NodCanvasViewModel()
        {
            NodCanvasModel = new NodCanvasModel();
        }

        public NodCanvasViewModel(NodCanvasModel cnm)
        {
            NodCanvasModel = cnm;
        }

        public void SetCanvasNodView(CanvasNodView cnv)
        {
            CanvasNodView = cnv;
        }
    }
}
