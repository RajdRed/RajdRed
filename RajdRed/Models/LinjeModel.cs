using RajdRed.Models.Base;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RajdRed.Models
{
    public class LinjeModel : RajdElement
    {
        private NodModelBase _nod1;

        public NodModelBase Nod1
        {
            get { return _nod1; }
            set 
            { 
                _nod1 = value;
            }
        }
        

        private double _x1;
        public double X1
        {
            get { return _x1; }
            set { _x1 = value; OnPropertyChanged("X1");  }
        }

        private double _y1;
        public double Y1
        {
            get { return _y1; }
            set { _y1 = value; OnPropertyChanged("Y1"); }
        }

        private double _x2;
        public double X2
        {
            get { return _x2; }
            set { _x2 = value; OnPropertyChanged("X2"); }
        }

        private double _y2;
        public double Y2
        {
            get { return _y2; }
            set { _y2 = value; OnPropertyChanged("Y2"); }
        }
        
        
        
    }
}
