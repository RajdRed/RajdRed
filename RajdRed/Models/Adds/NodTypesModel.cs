using RajdRed.Models.Base;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RajdRed.Models.Adds
{
    public class NodTypesModel : RajdElement
    {
        private Path _node = new Path()
        {
            Data = new GeometryGroup()
            {
                Children = new GeometryCollection()
                {
                    new LineGeometry(new Point(5, 1), new Point(5, 9)),
                    new LineGeometry(new Point(1, 5), new Point(9, 5))
                }
            },

            Stroke = Brushes.RoyalBlue,
            StrokeThickness = 0.75,

        };
        public Path Node
        {
            get{return _node;}
            set{}
        }

        private Path _association = new Path()
        {
            Data = new EllipseGeometry(new Point(5, 5), 4.5, 4.5),
            Stroke = Brushes.Red,
            StrokeThickness = 0.75
            //Fill = Brushes.Black
        };
        public Path Association
        {
            get { return _association; }
            set { }
        }

        private Path _aggregation = new Path()
        {
            Data = new PathGeometry(
            new List<PathFigure>() 
            { 
                new PathFigure(new Point(5, 1), 
                new List<PathSegment>()
                {
                    new LineSegment(new Point(5, 1), true),
                    new LineSegment(new Point(9, 5), true),
                    new LineSegment(new Point(5, 9), true),
                    new LineSegment(new Point(1, 5), true)
                }, 
                true) 
            }),
            StrokeThickness = 0.75,
            Stroke = Brushes.Magenta,
            Fill = Brushes.Black //(Brush)new BrushConverter().ConvertFrom("#EAEDF2")
        };
        public Path Aggregation
        {
            get { return _aggregation; }
            set { }
        }

        private Path _composition = new Path()
        {
            Data = new PathGeometry(
            new List<PathFigure>() 
            { 
                new PathFigure(new Point(5, 1), 
                new List<PathSegment>()
                {
                    new LineSegment(new Point(5, 1), true),
                    new LineSegment(new Point(9, 5), true),
                    new LineSegment(new Point(5, 9), true),
                    new LineSegment(new Point(1, 5), true)
                }, 
                true) 
            }),
            Fill = Brushes.Black,
            StrokeThickness = 0.75,
            Stroke = Brushes.Black
        };
        public Path Composition
        {
            get { return _composition; }
            set { }
        }


        private Path _generalization = new Path()
        {
            Data = new PathGeometry(
            new List<PathFigure>() 
            { 
                new PathFigure(new Point(5, 2), 
                new List<PathSegment>()
                {
                    new LineSegment(new Point(5, 2), true),
                    new LineSegment(new Point(9, 9), true),
                    new LineSegment(new Point(1, 9), true)
                }, 
                true) 
            }),
            Stroke = Brushes.SeaGreen,
            StrokeThickness = 0.75,
            Fill = Brushes.Black //(Brush)new BrushConverter().ConvertFrom("#EAEDF2")
        };
        public Path Generalization
        {
            get { return _generalization; }
            set { }
        }
    }
}
