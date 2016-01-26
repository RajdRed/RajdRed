using RajdRed.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RajdRed.Models
{
    public class CanvasNodModel : NodModelBase
    {
        private double _positionLeft;
        public double PositionLeft
        {
            get { return _positionLeft; }
            set
            {
                if (_positionLeft != value)
                    _positionLeft = value;

                OnPropertyChanged("PositionLeft");
            }
        }

        private double _positionTop;
        public double PositionTop
        {
            get { return _positionTop; }
            set { _positionTop = value; OnPropertyChanged("PositionTop"); }
        }
    }
}
