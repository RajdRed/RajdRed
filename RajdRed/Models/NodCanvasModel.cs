using RajdRed.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RajdRed.Models
{
    public class NodCanvasModel : NodModelBase
    {
        public NodCanvasModel(Point p)
        {
            PositionLeft = p.X;
            PositionTop = p.Y;
            Path = NodTypesModel.Association;
        }
        void SetLinje(LinjeModel lm)
        {
            LinjeModel = lm;
        }
    }
}
