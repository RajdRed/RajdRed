using RajdRed.Models;
using RajdRed.Models.Base;
using RajdRed.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace RajdRed.Repositories
{
    public class MainRepository
    {
        private bool _hasSelected = false;
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


        public bool CheckIfHit(Point mouseDownPos, Point mouseUpPos)
        {
            /* kontroll för linjer innanför intersection */
            _hasSelected = LinjeRepository.CheckIfHit(mouseDownPos, mouseUpPos);

            /* kontroll för klasser innanför selection */
            _hasSelected = KlassRepository.CheckIfHit(mouseDownPos, mouseUpPos) || _hasSelected;

            /*Kontroll för canvasnoder inanför selection*/
            _hasSelected = NodCanvasRepository.CheckIfHit(mouseDownPos, mouseUpPos) || _hasSelected;

            return _hasSelected;
        }

        public void Select(RajdElement re)
        {
            if (re is KlassModel)
                _klassRepository.Select(re as KlassModel);
            else if (re is LinjeModel)
                _linjeRepository.Select(re as LinjeModel);
            else if (re is NodCanvasModel)
                _nodCanvasRepository.Select(re as NodCanvasModel);

            _hasSelected = true;
        }

        public bool HasSelected()
        {
            return _hasSelected;
        }

        public void DeselectAll()
        {
            if (_hasSelected)
            {
                _klassRepository.DeselectAllClasses();
                _linjeRepository.DeselectAllLines();
                _nodCanvasRepository.DeselectAllCanvasNodes();
            }
        }

        public void DeleteSelected()
        {
            if (_hasSelected)
            {
                _klassRepository.DeleteSelected();
                _linjeRepository.DeleteSelected();
                _nodCanvasRepository.DeleteSelected();

                _hasSelected = false;
            }
        }
    }
}
