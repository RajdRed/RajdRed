using RajdRed.Models;
using RajdRed.Repositories;
using RajdRed.Views;
using System.Windows;
using System.Windows.Controls;

namespace RajdRed.ViewModels
{
    public class KlassViewModel
    {
        //AdornerLayer aLayer;
        
        public KlassModel KlassModel { get; set; }
        public KlassView KlassView { get; set; }
        public KlassRepository KlassRepository { get; set; }
        public NodKlassRepository NodKlassRepository { get; set; }

        public KlassViewModel(Point startPosition, KlassRepository kr)
        {
            KlassRepository = kr;
            KlassModel = new KlassModel(this, startPosition);
            NodKlassRepository = new NodKlassRepository(this);
        }

        public KlassViewModel(KlassModel km)
        {
            KlassModel = km;
        }

        public KlassViewModel(){}

        public void Delete()
        {
            foreach (NodKlassViewModel n in NodKlassRepository)
            {
                if (n.NodKlassModel.IsSet)
                {
                    KlassRepository.MainRepository.NodCanvasRepository.CreateFromNodModelBase(n.NodKlassModel);
                }
            }

            KlassRepository.Remove(this);
        }

        public void SetKlassView(KlassView kv)
        {
            KlassView = kv;
        }

        public Point PositionOnCanvas()
        {
            return new Point(Canvas.GetLeft(KlassView), Canvas.GetTop(KlassView));
        }

        public bool IsInArea(Point p)
        {
            Point ThisPosition = PositionOnCanvas();

            if ((p.X >= ThisPosition.X && p.Y >= ThisPosition.Y)
                && (p.X <= ThisPosition.X + KlassModel.Width && p.Y <= ThisPosition.Y + KlassModel.Height))
                return true;

            return false; 
        }

        public int OnSide(Point p)
        {
            double Top = -1;
            double Left = -1;
            double Right = Left + (this.KlassModel.Width);
            double Bottom = Top + (this.KlassModel.Height);


            //bool leftBoarder, topBoarder = 1, rightBoarder, bottomBoarder = false;
            while (true)
            {
                if (p.Y < Top)
                {
                    //topBoarder
                    return 1;

                }
                else if (p.X < Left)
                {
                    //leftBoarder
                    return 2;
                }
                else if (p.X > Left && ((p.Y > Top) && (p.Y < Bottom)))
                {
                    //rightBoarder
                    return 3;
                }
                else
                {
                    //bottomBoarder = true;
                    return 4;
                }

            }
        }
    }
}
