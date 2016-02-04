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
        private MainWindow _mainWindow;
        public MainWindow MainWindow
        {
            get { return _mainWindow; }
        }
        
        private KlassRepository _klassRepository;
        public KlassRepository KlassRepository
        {
            get { return _klassRepository; }
        }

        private NodCanvasRepository _nodCanvasRepository;
        public NodCanvasRepository NodCanvasRepository
        {
            get { return _nodCanvasRepository; }
        }

        private LinjeRepository _linjeRepository;
        public LinjeRepository LinjeRepository
        {
            get { return _linjeRepository; }
        }

        public MainRepository(MainWindow mw)
        {
            _mainWindow = mw;
            _klassRepository = new KlassRepository(this);
            _linjeRepository = new LinjeRepository(this);
            _nodCanvasRepository = new NodCanvasRepository(this);
        }
    }
}
