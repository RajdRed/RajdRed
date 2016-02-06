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
        private int _numberOfSelected = 0;
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


        public int CheckIfHit(Point mouseDownPos, Point mouseUpPos)
        {
			List<NodModelBase> nmbList = new List<NodModelBase>();

            /* kontroll för klasser innanför selection */
            _numberOfSelected += KlassRepository.CheckIfHit(mouseDownPos, mouseUpPos, ref nmbList);

            /*Kontroll för canvasnoder inanför selection*/
            _numberOfSelected += NodCanvasRepository.CheckIfHit(mouseDownPos, mouseUpPos, ref nmbList);

            /*Kontroll för canvasnoder inanför selection*/
            _numberOfSelected += TextBoxRepository.CheckIfHit(mouseDownPos, mouseUpPos);

			SelectLinesOfNod(ref nmbList);

            return _numberOfSelected;
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

            _numberOfSelected++;
        }

        public void Deselect(RajdElement re)
        {
            if (re is KlassModel)
                _klassRepository.Deselect(re as KlassModel);
            else if (re is LinjeModel)
                _linjeRepository.Deselect(re as LinjeModel);
            else if (re is NodCanvasModel)
                _nodCanvasRepository.Deselect(re as NodCanvasModel);
            else if (re is TextBoxModel)
                _textBoxRepository.Deselect(re as TextBoxModel);

            _numberOfSelected--;
        }

        public bool HasSelected()
        {
            return (_numberOfSelected != 0 ? true : false);
        }

        public bool HasNoSelected()
        {
            return (_numberOfSelected == 0 ? true : false);
        }

        public void DeselectAll()
        {
            if (_numberOfSelected != 0)
            {
                _klassRepository.DeselectAllClasses();
                _linjeRepository.DeselectAllLines();
                _nodCanvasRepository.DeselectAllCanvasNodes();
                _textBoxRepository.DeselectAllTextBoxes();

                _numberOfSelected = 0;
            }
        }

        public void DeleteSelected()
        {
            if (_numberOfSelected != 0)
            {
                _klassRepository.DeleteSelected();
                _linjeRepository.DeleteSelected();
                _nodCanvasRepository.DeleteSelected();
                _textBoxRepository.DeleteSelected();

                _numberOfSelected = 0;
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
                        {
                            NodKlassModel n = li.Nod1 as NodKlassModel;
                            n.NodKlassViewModel.Select();
                        }
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
                        {
                            NodKlassModel n = li.Nod2 as NodKlassModel;
                            n.NodKlassViewModel.Select();
                        }
				}
			}
		}

        public void DeselectLinesOfNod(ref List<NodModelBase> nmbList)
        {
            List<LinjeModel> selectedLinjerList = new List<LinjeModel>();

            /* Deselekterar alla linjer som hör till noden */
            foreach (NodModelBase n in nmbList)
            {
                foreach (LinjeModel l in n.LinjeModelList)
                {
                    Deselect(l);
                    selectedLinjerList.Add(l);
                }
            }

            foreach (LinjeModel li in selectedLinjerList)
            {
                if (!li.Nod1.IsSelected && !li.Nod2.IsSelected)
                {
                    continue;
                }

                else if (li.Nod1.IsSelected)
                {
                    bool shouldNodBeSelected = false;

                    foreach (LinjeModel lm in li.Nod1.LinjeModelList)
                    {
                        if (lm.IsSelected)
                        {
                            shouldNodBeSelected = true;
                            break;
                        }
                    }

                    if (!shouldNodBeSelected)
                    {
                        if (li.Nod1 is NodCanvasModel)
                            Deselect(li.Nod1);
                        else
                        {
                            NodKlassModel n = li.Nod1 as NodKlassModel;
                            n.NodKlassViewModel.Deselect();
                        }
                    }
                }

                else
                {
                    bool shouldNodBeSelected = false;

                    foreach (LinjeModel lm in li.Nod2.LinjeModelList)
                    {
                        if (lm.IsSelected)
                        {
                            shouldNodBeSelected = true;
                            break;
                        }
                    }

                    if (!shouldNodBeSelected)
                        if (li.Nod2 is NodCanvasModel)
                            Deselect(li.Nod2);
                        else
                        {
                            NodKlassModel n = li.Nod2 as NodKlassModel;
                            n.NodKlassViewModel.Deselect();
                        }
                }
            }
        }
    }
}
