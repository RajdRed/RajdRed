using RajdRed.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RajdRed.Models
{
    public class NodCanvasModel : NodModelBase
    {
        public LinjeModel LinjeModel { get; set; }

        void SetLinje(LinjeModel lm)
        {
            LinjeModel = lm;
        }
    }
}
