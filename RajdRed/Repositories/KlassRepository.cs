using RajdRed.Models;
using RajdRed.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

namespace RajdRed.Repositories
{
    public class KlassRepository : ObservableCollection<KlassViewModel>
    {
        public KlassRepository(){}
        public void AddNewKlass(Point startPosition)
        {
<<<<<<< HEAD
            Add(new KlassViewModel(new KlassModel()
                    {
                        Header = "Ny Klass *",
                        PositionLeft = startPosition.X,
                        PositionTop = startPosition.Y
                    }, this)
                );
=======
            Add(new KlassViewModel()
            {
                KlassModel = new KlassModel()
                {
                    Header = "Ny Klass *",
                    PositionLeft = startPosition.X,
                    PositionTop = startPosition.Y,
					IsSelected = true
                },
                KlassRepository = this
            });
>>>>>>> refs/remotes/origin/BugFixMVVM-Jocke
        }
    }
}
