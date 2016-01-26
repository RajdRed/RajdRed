using RajdRed.Models.Base;
using RajdRed.Views;
using System.Windows.Controls;

namespace RajdRed.Models
{
    public class KlassModel : RajdElement
    {
        public KlassView KlassView;
        private string _header;
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged("Header");
            }
        }

        private string _attributes;
        public string Attributes
        {
            get { return _attributes; }
            set
            {
                _attributes = value;
                OnPropertyChanged("Attributes");
            }
        }

        private string _methods;
        public string Methods
        {
            get { return _methods; }
            set
            {
                _methods = value;
                OnPropertyChanged("Methods");
            }
        }

        private double _positionLeft = 0;
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

        private double _positionTop = 0;
        public double PositionTop
        {
            get { return _positionTop; }
            set
            {
                if (_positionTop != value)
                    _positionTop = value;

                OnPropertyChanged("PositionTop");
            }
        }        
    }
}
