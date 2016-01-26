using RajdRed.Models.Adds;
using RajdRed.Models.Base;

namespace RajdRed.Models
{
    class KlassNodModel : NodModelBase
    {
        private int[,] _positionOnKlass;
        public int[,] PositionOnKlass
        {
            get { return _positionOnKlass; }
            set { _positionOnKlass = value; }
        }

        public void TurnToAssociation()
        {
            Geometry = NodTypesModel.Association();
        }
    }
}
