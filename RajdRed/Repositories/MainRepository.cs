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

        private TextBoxRepository _textBoxRepository;
        public TextBoxRepository TextBoxRepository
        {
            get { return _textBoxRepository; }
        }
        

        public MainRepository(MainWindow mw)
        {
            _mainWindow = mw;
            _klassRepository = new KlassRepository(this);
            _linjeRepository = new LinjeRepository(this);
            _nodCanvasRepository = new NodCanvasRepository(this);
            _textBoxRepository = new TextBoxRepository(this);
        }


        public bool CheckIfHit(Point mouseDownPos, Point mouseUpPos)
        {
			List<NodModelBase> nmbList = new List<NodModelBase>();

            /* kontroll för klasser innanför selection */
            _hasSelected = KlassRepository.CheckIfHit(mouseDownPos, mouseUpPos, ref nmbList);

            /*Kontroll för canvasnoder inanför selection*/
            _hasSelected = NodCanvasRepository.CheckIfHit(mouseDownPos, mouseUpPos, ref nmbList) || _hasSelected;

			SelectLinesOfNod(ref nmbList);

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
            else if (re is TextBoxModel)
                _textBoxRepository.Select(re as TextBoxModel);

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
                _textBoxRepository.DeselectAllTextBoxes();

                _hasSelected = false;
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

		public void SelectLinesOfNod(ref List<NodModelBase> nmbList) 
		{
			List<LinjeModel> selectedLinjerList = new List<LinjeModel>();

			/* selektera alla linjer som hör till noden */
			foreach (NodModelBase n in nmbList)
			{
				foreach (LinjeModel l in n.LinjeModelList)
				{
					Select(l);
					selectedLinjerList.Add(l);
				}
			}

			foreach (LinjeModel li in selectedLinjerList)
			{
				if (li.Nod1.IsSelected && li.Nod2.IsSelected)
				{
					continue;
				}

				else if (!li.Nod1.IsSelected)
				{
					bool shouldNodBeSelected = true;

					foreach (LinjeModel lm in li.Nod1.LinjeModelList)
					{
						if (!lm.IsSelected)
						{
							shouldNodBeSelected = false;
							break;
						}
					}

					if (shouldNodBeSelected)
					{
						if (li.Nod1 is NodCanvasModel)
							Select(li.Nod1);
						else
							li.Nod1.IsSelected = true;
					}
				}

				else
				{
					bool shouldNodBeSelected = true;

					foreach (LinjeModel lm in li.Nod2.LinjeModelList)
					{
						if (!lm.IsSelected)
						{
							shouldNodBeSelected = false;
							break;
						}
					}

					if (shouldNodBeSelected)
						if (li.Nod2 is NodCanvasModel)
							Select(li.Nod2);
						else
							li.Nod2.IsSelected = true;
				}
			}
		}
    }
}
