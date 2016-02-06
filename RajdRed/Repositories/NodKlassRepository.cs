using RajdRed.Models;
using RajdRed.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace RajdRed.Repositories
{
    public class NodKlassRepository : ObservableCollection<NodKlassViewModel>
    {
		private int _numberOfSelected = 0;

        public NodKlassRepository(KlassViewModel kvm)
        {
            //left
            for (int i = 1; i <= 4; i++)
                Add(new NodKlassViewModel(new NodKlassModel() {
                    Row = i,
                    Column = 0,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center
                }, kvm, this));

            //right
            for (int i = 1; i <= 4; i++)
                Add(new NodKlassViewModel(new NodKlassModel()
                {
                    Row = i,
                    Column = 5,
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center
                }, kvm, this));

            //top
            for (int i = 1; i <= 4; i++)
                Add(new NodKlassViewModel(new NodKlassModel()
                {
                    Row = 0,
                    Column = i,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top
                }, kvm, this));

            //bottom
            for (int i = 1; i <= 4; i++)
                Add(new NodKlassViewModel(new NodKlassModel()
                {
                    Row = 5,
                    Column = i,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Bottom
                }, kvm, this));
        }

		public void Select(NodKlassModel n)
		{
            n.NodKlassViewModel.Select();
		}

        public void Deselect(NodKlassModel n)
        {
            n.NodKlassViewModel.Deselect();
        }

        public void IncreaseSelected()
        {
            _numberOfSelected++;
        }

        public void DecreaseSelected()
        {
            _numberOfSelected--;
        }
    }
}
