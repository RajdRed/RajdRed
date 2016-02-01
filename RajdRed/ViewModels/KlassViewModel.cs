using RajdRed.Models;
using RajdRed.Repositories;
using RajdRed.ViewModells.Add;
using RajdRed.ViewModels.Commands;
using RajdRed.Views;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace RajdRed.ViewModels
{
    public class KlassViewModel
    {
        AdornerLayer aLayer;
        
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
    }
}
