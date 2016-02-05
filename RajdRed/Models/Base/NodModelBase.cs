﻿using RajdRed.Models.Adds;
using System.Windows.Media;
using System.Windows.Shapes;
using System;

namespace RajdRed.Models.Base
{
    public class NodModelBase : RajdElement
    {
        public LinjeModel LinjeModel { get; set; }

        private bool _converted = false;
        public bool Converted
        {
            get { return _converted; }
            set { _converted = value; OnPropertyChanged("Converted"); }
        }
        

        private double _width;
        public virtual double Width
        {
            get { return _width; }
            set { _width = value; OnPropertyChanged("Width"); }
        }

        private double _height;
        public virtual double Height
        {
            get { return _height; }
            set { _height = value; OnPropertyChanged("Height"); }
        }



        public NodTypesModel NodTypesModel;
        
        
        private Path _path;
        public Path Path
        {
            get { return _path; }
            set
            {
                if (_path != value)
                    _path = value;

                OnPropertyChanged("Path");
            }
        }
        
        private double _positionLeft;
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

        private double _positionTop;
        public double PositionTop
        {
            get { return _positionTop; }
            set 
            { 
                _positionTop = value; 
                OnPropertyChanged("PositionTop");
            }
        }

        public NodModelBase()
        {
            NodTypesModel = new NodTypesModel();
        }
    }
}
