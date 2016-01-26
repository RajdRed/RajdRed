using System.Windows;
using System.Windows.Media;

namespace RajdRed.Models.Adds
{
    class NodTypesModel
    {
        public static Geometry Association()
        {
            PathGeometry geometry = new PathGeometry();
            PathFigure figure = new PathFigure();
            figure.StartPoint = new Point(2, 5);
            figure.Segments.Add(new PolyLineSegment()
            {
                Points = new PointCollection()
                {
                   new Point(7,5),
                   new Point(5,2),
                   new Point(5,7)
                }
            });

            geometry.Figures.Add(figure);
            return geometry;
        }

        public static Geometry Aggregation()
        {
            return new RectangleGeometry(new Rect(new Size(10, 10)));
        }
    }
}
