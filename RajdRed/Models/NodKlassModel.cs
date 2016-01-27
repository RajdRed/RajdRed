using RajdRed.Models.Adds;
using RajdRed.Models.Base;

namespace RajdRed.Models
{
    public class NodKlassModel : NodModelBase
    {
        private int _gridRow;
        public int GridRow
        {
            get { return _gridRow; }
            set { _gridRow = value; OnPropertyChanged("GridRow"); }
        }

        private int _gridColumn;
        public int GridColumn
        {
            get { return _gridColumn; }
            set { _gridColumn = value; OnPropertyChanged("GridColumn"); }
        }
        

        public void TurnToAssociation()
        {
            Geometry = NodTypesModel.Association();
        }
    }
}
