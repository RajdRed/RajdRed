﻿using RajdRed.Models.Base;
using RajdRed.ViewModels;
using RajdRed.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RajdRed.Models
{
    public class KlassModel : RajdElement
    {
<<<<<<< HEAD
        public KlassViewModel KlassViewModel { get; set; }
=======
        public KlassViewModel _klassViewModel { get; set; }
>>>>>>> refs/remotes/origin/SelectFix-SnartÄrViFramme-Jocke
        public static double MinSize = 110;

        private bool _resize = false;
        public bool Resize
        {
            get
            {
                return _resize;
            }
            set
            {
                _resize = value;
                OnPropertyChanged("Resize");
            }
        }

        public override bool IsSelected
        {
            get
            {
                return base.IsSelected;
            }
            set
            {
                base.IsSelected = value;
            }
        }

        private string _header;
        public string Header
        {
            get { return _header; }
            set
            {
                _header = value;
                OnPropertyChanged("Header");
            }
        }

        private string _attributes;
        public string Attributes
        {
            get { return _attributes; }
            set
            {
                _attributes = value;
                OnPropertyChanged("Attributes");
            }
        }

        private string _methods;
        public string Methods
        {
            get { return _methods; }
            set
            {
                _methods = value;
                OnPropertyChanged("Methods");
            }
        }

        private double _positionLeft = 0;
        public double PositionLeft
        {
            get { return _positionLeft; }
            set
            {
                if (_positionLeft != value)
                    _positionLeft = value;

                OnPropertyChanged("PositionLeft");
            }
        }

        private double _positionTop = 0;
        public double PositionTop
        {
            get { return _positionTop; }
            set
            {
                if (_positionTop != value)
                    _positionTop = value;

                OnPropertyChanged("PositionTop");
            }
        }

        public Visibility Selected
        {
            get
            {
                if (IsSelected)
                    return Visibility.Visible;
                else
                    return Visibility.Hidden;
            }
        }

        private double _height = MinSize;
        public double Height
        {
            get { return _height; }
            set 
            {
                _height = value; 
                OnPropertyChanged("Height"); 
            }
        }

        private double _width = MinSize;
        public double Width
        {
            get { return _width; }
            set 
            {
                _width = value;// _klassViewModel.KlassView.ActualWidth; 
                OnPropertyChanged("Width"); 
            }
        }

        public KlassModel(KlassViewModel kvm, Point startPosition)
        {
            KlassViewModel = kvm;
            Header = "New Class *";
            PositionLeft = startPosition.X;
            PositionTop = startPosition.Y;
            IsSelected = true;
		}
    }
}
