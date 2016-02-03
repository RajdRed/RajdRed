using RajdRed.Models;
using RajdRed.Models.Base;
using RajdRed.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RajdRed.Repositories
{
    public class LinjeRepository : ObservableCollection<LinjeViewModel>
    {
        MainRepository _mainRepository;
        public MainRepository MainRepository { get { return _mainRepository; } }

        public LinjeRepository(MainRepository mr)
        {
            _mainRepository = mr;
        }

        public LinjeViewModel AddNewLinje(NodModelBase n1, NodModelBase n2)
        {
            LinjeModel lm = new LinjeModel(n1, n2) { Nod1 = n1, Nod2 = n2 };
            LinjeViewModel lvm = new LinjeViewModel() { LinjeModel = lm };

            n1.LinjeModel = lm;
            n2.LinjeModel = lm;

            Add(lvm);

            return lvm;
        }

		public void CheckIfHit(Point mouseDownPos, Point mouseUpPos)
		{
			double Y1, Y2, X1, X2, M, Y12, Y22;
			Y1 = mouseDownPos.Y;
			Y2 = mouseUpPos.Y;
			X1 = mouseDownPos.X;
			X2 = mouseUpPos.X;
			M = mouseDownPos.Y;
			Y12 = mouseUpPos.Y;
			Y22 = mouseDownPos.Y;

			if (SelectionLinesHorizontal(X1, X2, Y1, Y2, M))
				return;
			else if (SelectionLinesVertical(X1, Y1, Y2))
				return;
			else if (SelectionLinesHorizontal(X1, X2, Y12, Y22, M))
				return;
			else if (SelectionLinesVertical(X1, Y12, Y22))
				return;	
		}

		private bool SelectionLinesHorizontal(double rak_X1, double rak_X2, double rak_Y1, double rak_Y2, double rak_M)
		{
			foreach (LinjeViewModel lvm in this)
			{
				double sne_Y1, sne_Y2, sne_K, sne_X1, sne_X2, sne_M;

				sne_Y1 = lvm.LinjeModel.Nod1.PositionTop + 5;
				sne_X1 = lvm.LinjeModel.Nod1.PositionLeft + 5;
				sne_Y2 = lvm.LinjeModel.Nod2.PositionTop + 5;
				sne_X2 = lvm.LinjeModel.Nod2.PositionLeft + 5;
				sne_K = (sne_Y2 - sne_Y1) / (sne_X2 - sne_X1);
				sne_M = sne_Y1 - sne_K * sne_X1;

				double intersectX, intersectY;

				intersectX = (rak_M - sne_M) / sne_K;
				intersectY = sne_K * intersectX + sne_M;

				if ((rak_X1 <= intersectX && intersectX <= rak_X2) && (sne_X1 <= intersectX && sne_X2 <= intersectX))
				{
					if (intersectY == rak_Y1)
					{
						lvm.LinjeModel.IsSelected = true;
						return true;
					}
				}
			}

			return false;
		}

		private bool SelectionLinesVertical(double rak_X, double rak_Y1, double rak_Y2)
		{
			foreach (LinjeViewModel lvm in this)
			{
				double sne_Y1, sne_Y2, sne_K, sne_X1, sne_X2, sne_M;

				sne_Y1 = lvm.LinjeModel.Nod1.PositionTop + 5;
				sne_X1 = lvm.LinjeModel.Nod1.PositionLeft + 5;
				sne_Y2 = lvm.LinjeModel.Nod2.PositionTop + 5;
				sne_X2 = lvm.LinjeModel.Nod2.PositionLeft + 5;
				sne_K = (sne_Y2 - sne_Y1) / (sne_X2 - sne_X1);
				sne_M = sne_Y1 - sne_K * sne_X1;

				double intersectX, intersectY;

				intersectX = rak_X;
				intersectY = sne_K * rak_X + sne_M;

				if ((rak_Y1 <= intersectY && intersectY <= rak_Y2) && (sne_X1 <= intersectX && intersectX <= sne_X2))
				{
					lvm.LinjeModel.IsSelected = true;
					return true;
				}
			}

			return false;
		}
    }
}
