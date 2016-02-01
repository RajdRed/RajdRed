using RajdRed.Models.Base;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace RajdRed.Models.Adds
{
    public class NodTypesModel : RajdElement
    {
        private NodModelBase _nodBaseModel { get; set; }
        public Path Node = new Path()
        {
            Data = new GeometryGroup()
            {
                Children = new GeometryCollection()
                {
                    new LineGeometry(new Point(5, 1), new Point(5, 9)),
                    new LineGeometry(new Point(1, 5), new Point(9, 5))
                }
            },
            Stroke = Brushes.Black,
            StrokeThickness = 0.8,


        };

        public Path Association = new Path()
        {
            Data = new EllipseGeometry(new Point(5, 5), 4.5, 4.5),
            Fill = Brushes.Black
        };

        public Path Aggregation = new Path()
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
            Stroke = Brushes.Black,
            Fill = Brushes.White
        };

        public Path Composition = new Path()
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


        public Path Generalization = new Path()
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
            Stroke = Brushes.Black,
            StrokeThickness = 0.75,
            Fill = Brushes.White
        };

        public NodTypesModel(NodModelBase nmb)
        {
            _nodBaseModel = nmb;
        }
    }
}
