using System.Windows.Media;

namespace RajdRed.Models.Base
{
    public abstract class NodModelBase :RajdElement
    {
        private Geometry _geometry = Adds.NodTypesModel.Association();
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
    }
}
