using RajdRed.Models.Adds;
using RajdRed.Models.Base;
using RajdRed.ViewModels;
using System.Windows;

namespace RajdRed.Models
{
    public class NodKlassModel : NodModelBase
    {
        public bool IsPressed = false;
        public int Row { get; set; }
        public int Column { get; set; }

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

        public double Angle
        {
            get 
            {
                if (_horizontalAlignment == HorizontalAlignment.Left)
                {
                    return 90;
                }
                else if (_horizontalAlignment == HorizontalAlignment.Right)
                {
                    return 270;
                }
                else if (_verticalAlignment == VerticalAlignment.Top)
                {
                    return 180;
                }

                return 0;
            }
        }
        

        public NodKlassModel()
        {
            Path = NodTypesModel.Node;
        }
        
    }
}
