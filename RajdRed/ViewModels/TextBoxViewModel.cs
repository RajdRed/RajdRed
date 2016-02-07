using RajdRed.Models;
using RajdRed.Repositories;
using RajdRed.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace RajdRed.ViewModels
{
    public class TextBoxViewModel
    {
        public TextBoxModel TextBoxModel { get; set; }
        public TextBoxRepository TextBoxRepository { get; set; }
        public TextBoxView TextBoxView { get; set; }
        public TextBoxViewModel(Point p, TextBoxRepository tbr)
        {
            TextBoxModel = new TextBoxModel(p, this)
            {
                Text = "New text *"
            };
            TextBoxRepository = tbr;
        }

        public void SetView(TextBoxView t)
        {
            TextBoxView = t;
        }

        public void Select()
        {
            if (!IsSelected())
            {
                TextBoxModel.IsSelected = true;
                TextBoxRepository.IncreaseSelected();
                SetPositionRelativeToView();
            }
        }

        public void Deselect()
        {
            if (IsSelected())
            {
                TextBoxModel.IsSelected = true;
                TextBoxRepository.DecreaseSelected();
            }
        }

        public void Delete()
        {
            Deselect();
            TextBoxRepository.Remove(this);
        }

        public bool IsSelected()
        {
            return (TextBoxModel.IsSelected ? true : false);
        }

        public void SetPositionRelativeToView()
        {
            if (TextBoxView != null)
                TextBoxModel.PositionRelative = Mouse.GetPosition(TextBoxView);
            else
                TextBoxModel.PositionRelative = new Point(KlassModel.MinSize / 2, KlassModel.MinSize / 2);
        }

        public Point GetPositionRelativeToView()
        {
            return TextBoxModel.PositionRelative;
        }

        public void Move(Point p)
        {
            TextBoxModel.PositionLeft = p.X - GetPositionRelativeToView().X;
            TextBoxModel.PositionTop = p.Y - GetPositionRelativeToView().Y;
        }
    }
}
