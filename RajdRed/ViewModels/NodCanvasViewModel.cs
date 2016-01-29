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
        public NodCanvasView NodCanvasView { get; set; }
        public NodCanvasRepository NodCanvasRepository { get; set; }
        public NodCanvasModel NodCanvasModel { get; set; }

        public NodCanvasViewModel(NodCanvasModel ncm, NodCanvasRepository ncr)
        {
            NodCanvasModel = ncm;
            NodCanvasRepository = ncr;
        }

        public NodCanvasViewModel(){}

        public void SetNodCanvasView(NodCanvasView ncv)
        {
            NodCanvasView = ncv;
        }
    }
}
