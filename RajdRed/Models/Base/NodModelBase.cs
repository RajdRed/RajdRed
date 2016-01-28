using RajdRed.Models.Adds;
using System.Windows.Media;

namespace RajdRed.Models.Base
{
    public abstract class NodModelBase : RajdElement
    {
        public NodTypesModel NodTypesModel = new NodTypesModel();
        private Geometry _geometry;
        public Geometry Geometry
        {
            get { return _geometry; }
            set
            {
                if (_geometry != value)
                    _geometry = value;

                OnPropertyChanged("Geometry");
            }
        }

        private SolidColorBrush _fill = new SolidColorBrush(Colors.Transparent);
        public SolidColorBrush Fill
        {
            get { return _fill; }
            set { _fill = value; }
        }
        
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

        public NodModelBase()
        {
            Geometry = NodTypesModel.Node;
        }
    }
}
