using RajdRed.Models;
using RajdRed.ViewModels;
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
    }
}
