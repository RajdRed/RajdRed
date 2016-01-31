using RajdRed.Models.Adds;
using System.Windows.Media;

namespace RajdRed.Models.Base
{
    public abstract class NodModelBase : RajdElement
    {
        public LinjeModel LinjeModel { get; set; }

        private double _width = 10;
        public double Width
        {
            get { return _width; }
            set { _width = value; OnPropertyChanged("Width"); }
        }

        private double _height = 10;
        public double Height
        {
            get { return _height; }
            set { _height = value; OnPropertyChanged("Height"); }
        }

        public NodTypesModel NodTypesModel;
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
            NodTypesModel = new NodTypesModel(this);
            Geometry = NodTypesModel.Node;
        }
    }
}
