using RajdRed.Models;
using RajdRed.Models.Base;
using RajdRed.Repositories;
using RajdRed.ViewModels.Base;
using RajdRed.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public static NodCanvasViewModel CopyNodKlassToNew(NodKlassModel nkm, NodCanvasRepository ncr)
        {
            NodCanvasModel newNcm = NodCanvasModel.CopyNod(nkm);
            NodCanvasViewModel newNcvm = new NodCanvasViewModel()
            {
                NodCanvasRepository = ncr,
                NodCanvasModel = newNcm
            };

            return newNcvm;
        }

        public void SetNodCanvasView(NodCanvasView ncv)
        {
            NodCanvasView = ncv;
        }

        public void EatNod(NodCanvasViewModel ncvm)
        {
            foreach (LinjeModel l in ncvm.NodCanvasModel.LinjeModelList)
            {
                l.ReplaceNod(ncvm.NodCanvasModel, this.NodCanvasModel);
                NodCanvasModel.LinjeModelList.Add(l);
            }

            NodCanvasRepository.Remove(ncvm);
        }

        public bool IsInArea(Point p)
        {
            if ((p.X >= NodCanvasModel.PositionLeft && p.Y >= NodCanvasModel.PositionTop)
                && (p.X <= NodCanvasModel.PositionLeft + NodCanvasModel.Width && p.Y <= NodCanvasModel.PositionTop + NodCanvasModel.Height))
                return true;

            return false;
        }

        public bool LookAndAttachCanvasNod(Point p)
        {
            if (tryAttachInKlasses(p))
            {
                return true;
            }
            else if (tryAttachOnCanvasNods(p))
            {
                return true;
            }

            return false;
        }

        private bool tryAttachInKlasses(Point p)
        {
            foreach (KlassViewModel k in NodCanvasRepository.MainRepository.KlassRepository)
            {
                if (k.IsInArea(p))
                {
                    foreach (NodKlassViewModel n in k.NodKlassRepository)
                    {
                        if (n.IsInArea(p))
                        {
                            n.EatNod(this);
                            return true;
                        }
                    }

                    return false;
                }
            }

            return false;
        }

        private bool tryAttachOnCanvasNods(Point p)
        {
            foreach (NodCanvasViewModel n in NodCanvasRepository) 
            {
                if (n != this) 
                {
                    if (n.IsInArea(p))
                    {
                        EatNod(n);
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
