using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RajdRed.Repositories
{
    class MainRepository
    {
        private KlassRepository _klassRepository = new KlassRepository();
        public KlassRepository KlassRepository
        {
            get { return _klassRepository; }
            set { _klassRepository = value; }
        }

        private CanvasNodRepository _canvasNodRepository = new CanvasNodRepository();
        public CanvasNodRepository CanvasNodRepository
        {
            get { return _canvasNodRepository; }
            set { _canvasNodRepository = value; }
        }

        private LinjeRepository _linjeRepository = new LinjeRepository();
        public LinjeRepository LinjeRepository
        {
            get { return _linjeRepository; }
            set { _linjeRepository = value; }
        }
        

        private CompositeCollection _collection = new CompositeCollection();
        public CompositeCollection Collection
        {
            get { return _collection; }
        }

        public MainRepository()
        {
            Collection.Add(new CollectionContainer() { Collection = KlassRepository });
            Collection.Add(new CollectionContainer() { Collection = CanvasNodRepository });
            Collection.Add(new CollectionContainer() { Collection = LinjeRepository });
        }
    }
}
