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

        private NodCanvasRepository _nodCanvasRepository = new NodCanvasRepository();
        public NodCanvasRepository NodCanvasRepository
        {
            get { return _nodCanvasRepository; }
            set { _nodCanvasRepository = value; }
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
            Collection.Add(new CollectionContainer() { Collection = NodCanvasRepository });
            Collection.Add(new CollectionContainer() { Collection = LinjeRepository });

            //NodCanvasModel cnm1 = new Models.NodCanvasModel()
            //    {
            //        PositionTop = 100,
            //        PositionLeft = 100
            //    };
            //NodCanvasViewModel cnvm1 = new NodCanvasViewModel()
            //{
            //    NodCanvasModel = cnm1
            //};

            //NodCanvasModel cnm2 = new Models.NodCanvasModel()
            //    {
            //        PositionTop = 300,
            //        PositionLeft = 300
            //    };
            //NodCanvasViewModel cnvm2 = new NodCanvasViewModel()
            //{
            //    NodCanvasModel = cnm2
            //};

            //NodCanvasRepository.Add(cnvm1);
            //NodCanvasRepository.Add(cnvm2);
            //LinjeRepository.AddNewLinje(cnm1, cnm2);
        }
    }
}
