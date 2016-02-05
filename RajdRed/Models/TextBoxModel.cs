﻿using RajdRed.Models.Base;
using RajdRed.ViewModels;
using System.Windows;
using System.Windows.Media;
namespace RajdRed.Models
{
    public class TextBoxModel : RajdElement
    {
        TextBoxViewModel TextBoxViewModel { get; set; }
        private string _text;
        public string Text
        {
            get { return _text; }
            set { _text = value; OnPropertyChanged("Text"); }
        }

        private double _positionLeft;
        public double PositionLeft
        {
            get { return _positionLeft; }
            set { _positionLeft = value; OnPropertyChanged("PositionLeft"); }
        }

        private double _positionTop;
        public double PositionTop
        {
            get { return _positionTop; }
            set { _positionTop = value; OnPropertyChanged("PositionTop"); }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get { return _isSelected; }
            set { _isSelected = value; 
                OnPropertyChanged("IsSelected"); 
                OnPropertyChanged("Visible"); }
        }

        public Visibility Visible
        {
            get
            {
                if (IsSelected)
                    return Visibility.Visible;
                else
                    return Visibility.Hidden;
            }
        }

        public TextBoxModel(Point p, TextBoxViewModel tbvm)
        {
            PositionLeft = p.X;
            PositionTop = p.Y;
            TextBoxViewModel = tbvm;
        }
        
    }
}
