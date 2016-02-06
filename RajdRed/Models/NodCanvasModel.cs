using RajdRed.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RajdRed.Models
{
    public class NodCanvasModel : NodModelBase
    {
        private double _width = NodModelBase.MinSize;
        public override double Width
        {
            get { return _width; }
            set { _width = value; OnPropertyChanged("Width"); }
        }

        private double _height = NodModelBase.MinSize;
        public override double Height
        {
            get { return _height; }
            set { _height = value; OnPropertyChanged("Height"); }
        }

        public NodCanvasModel(){}
        public NodCanvasModel(Point p)
        {
            PositionLeft = p.X;
            PositionTop = p.Y;
            Path = NodTypesModel.Association;

            ZIndex = 99;
        }

        void SetLinje(LinjeModel lm)
        {
            LinjeModelList.Add(lm);
        }

        public static NodCanvasModel CopyNod(NodKlassModel n)
        {
            NodCanvasModel ncm = new NodCanvasModel()
            {
                Height = n.Height,
                Width = n.Width,
                IsSelected = n.IsSelected,
                LinjeModelList = n.LinjeModelList,
                OnField = n.OnField,
                Path = n.Path,
                NodTypesModel = n.NodTypesModel,
                PositionLeft = n.PositionLeft,
                PositionTop = n.PositionTop
            };

            foreach (LinjeModel l in n.LinjeModelList)
            {
                if (l.Nod1 == n)
                    l.Nod1 = ncm;
                else
                    l.Nod2 = ncm;

                l.SetOnPropertyChanged();
            }

            return ncm;
        }
    }
}
