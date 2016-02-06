using RajdRed.Models;
using RajdRed.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace RajdRed.Repositories
{
    public class TextBoxRepository : ObservableCollection<TextBoxViewModel>
    {
        private bool _hasSelected = false;
        public MainRepository MainRepository { get; set; }
        public TextBoxRepository(MainRepository mr)
        {
            MainRepository = mr;
        }

        public TextBoxViewModel AddNewTextBox(Point p)
        {
            TextBoxViewModel tbvm = new TextBoxViewModel(p, this);
            Add(tbvm);

            return tbvm;
        }

        public void Select(TextBoxModel t)
        {
            _hasSelected = t.IsSelected = true;
        }

        public void DeselectAllTextBoxes()
        {
            foreach (TextBoxViewModel t in this)
                t.TextBoxModel.IsSelected = false;
        }

        public bool CheckIfHit(Point mouseDownPos, Point mouseUpPos)
        {
            foreach (TextBoxViewModel tbvm in this)
            {
                Point leftTopCorner = new Point(tbvm.TextBoxModel.PositionLeft, tbvm.TextBoxModel.PositionTop);
                Point rightTopCorner = new Point(tbvm.TextBoxModel.PositionLeft + tbvm.TextBoxView.ActualWidth, tbvm.TextBoxModel.PositionTop);
                Point leftBotCorner = new Point(tbvm.TextBoxModel.PositionLeft, tbvm.TextBoxModel.PositionTop + tbvm.TextBoxView.ActualHeight);
                Point rightBotCorner = new Point(tbvm.TextBoxModel.PositionLeft + tbvm.TextBoxView.ActualWidth, tbvm.TextBoxModel.PositionTop + tbvm.TextBoxView.ActualHeight);
                if (rightTopCorner.X >= mouseDownPos.X && leftTopCorner.X <= mouseUpPos.X)
                {
                    if (rightBotCorner.Y >= mouseDownPos.Y && leftTopCorner.Y <= mouseUpPos.Y)
                    {
                        _hasSelected = tbvm.TextBoxModel.IsSelected = true;
                        
                    }
                }
            }
            return _hasSelected;
        }
        public void DeleteSelected()
        {
            int size = this.Count;
            List<TextBoxViewModel> deleteEverythingInThisList = new List<TextBoxViewModel>();

            for (int i = 0; i < size; i++)
                if (this[i].TextBoxModel.IsSelected)
                    deleteEverythingInThisList.Add(this[i]);

            foreach (TextBoxViewModel tbvm in deleteEverythingInThisList)
                tbvm.Delete();

            _hasSelected = false;
        }
    }
}
