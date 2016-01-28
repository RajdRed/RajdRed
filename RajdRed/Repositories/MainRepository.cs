using RajdRed.Models;
using RajdRed.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace RajdRed.Repositories
{
    public class MainRepository
    {
        private KlassRepository _klassRepository = new KlassRepository();
        public KlassRepository KlassRepository
        {
            get { return _klassRepository; }
            set { _klassRepository = value; }
        }

        private NodCanvasRepository _canvasNodRepository = new NodCanvasRepository();
        public NodCanvasRepository CanvasNodRepository
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

            //CanvasNodModel cnm1 = new Models.CanvasNodModel() 
            //    { 
            //        PositionTop = 100, 
            //        PositionLeft = 100 
            //    };
            //CanvasNodViewModel cnvm1 = new CanvasNodViewModel() {
            //    CanvasNodModel = cnm1
            //};

            //CanvasNodModel cnm2 = new Models.CanvasNodModel()
            //    {
            //        PositionTop = 300,
            //        PositionLeft = 300
            //    };
            //CanvasNodViewModel cnvm2 = new CanvasNodViewModel()
            //{
            //    CanvasNodModel = cnm2
            //};

            //CanvasNodRepository.Add(cnvm1);
            //CanvasNodRepository.Add(cnvm2);
            //LinjeRepository.AddNewLinje(cnm1, cnm2);
        }
    }
}
