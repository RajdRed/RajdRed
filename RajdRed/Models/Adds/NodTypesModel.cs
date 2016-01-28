using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace RajdRed.Models.Adds
{
    class NodTypesModel
    {
        public GeometryGroup Node = new GeometryGroup()
        {
            Children = new GeometryCollection(){
                new LineGeometry(new Point(5, 1), new Point(5, 9)),
                new LineGeometry(new Point(1, 5), new Point(9, 5))
                }
        };

        public EllipseGeometry Association = new EllipseGeometry(new Point(5, 5), 5, 5);

        public RectangleGeometry Aggregation = new RectangleGeometry(new Rect(new Size(5, 5)), 0, 0, new RotateTransform(45));

        public PathGeometry Generalization = new PathGeometry(
            new List<PathFigure>() 
            { 
                new PathFigure(new Point(4.5, 1), 
                new List<PathSegment>()
                {
                    new LineSegment(new Point(4.5, 1), true),
                    new LineSegment(new Point(9, 9), true),
                    new LineSegment(new Point(1, 9), true)
                }, 
                true) 
            });
    }
}
