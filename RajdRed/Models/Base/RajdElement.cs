using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RajdRed.Models.Base
{
    public abstract class RajdElement : BaseModel
    {
        public bool IsSelected { get; set; }
        public bool OnField { get; set; }
    }
}
