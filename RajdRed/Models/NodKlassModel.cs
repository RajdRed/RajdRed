using RajdRed.Models.Adds;
using RajdRed.Models.Base;
using RajdRed.ViewModels;
using System.Windows;

namespace RajdRed.Models
{
    public class NodKlassModel : NodModelBase
    {
        public bool IsSet = false;
        public int Row { get; set; }
        public int Column { get; set; }

        public double RPositionLeft { get; set; }
        public double RPositionTop { get; set; }

        private HorizontalAlignment _horizontalAlignment;
        public HorizontalAlignment HorizontalAlignment
        {
            get { return _horizontalAlignment; }
            set 
            { 
                _horizontalAlignment = value;
                OnPropertyChanged("HorizontalAlignment");
            }
        }

        private VerticalAlignment _verticalAlignment;
        public VerticalAlignment VerticalAlignment
        {
            get { return _verticalAlignment; }
            set 
            {
                _verticalAlignment = value;
                OnPropertyChanged("VerticalAlignment");
            }
        }
        
        
    }
}
