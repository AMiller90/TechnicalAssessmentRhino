using Rhino.Geometry;
using TechnicalAssessmentRhino.Models;

namespace TechnicalAssessmentRhino.Geometry
{
    public class HouseBuilder
    {
        private readonly HouseParameters _parameters;

        public HouseBuilder(HouseParameters parameters)
        {
            _parameters = parameters;
        }

        public Brep BuildBody()
        {
            var box = new Box(Plane.WorldXY, new Interval(0, _parameters.Width), new Interval(0, _parameters.Depth), new Interval(0, _parameters.Height));
            return box.ToBrep();
        }

        public Brep BuildRoof()
        {
            double roofHeight = _parameters.Width * 0.4;
            double halfWidth = _parameters.Width / 2.0;
            double height = _parameters.Height;

            var points = new[]
            {
                new Point3d(0, 0, height), // Left Point
                new Point3d(_parameters.Width, 0, height), // Right Point
                new Point3d(halfWidth, 0, height + roofHeight), // Roof Peak
                new Point3d(0, 0, height) // Starting Point - close profile
            };

            // Create polyline from points. Connect them
            var roofProfile = new Polyline(points).ToNurbsCurve();
            roofProfile.Reverse();

            // Extrude based on depth - creates a prism
            var extrusion = Extrusion.Create(roofProfile, _parameters.Depth, true);
            return extrusion.ToBrep();
        }

        public Brep BuildDoor()
        {
            double doorWidth = _parameters.Width * 0.2;
            double doorHeight = _parameters.Height * 0.6;
            double doorDepth = _parameters.Width * 0.05;

            double doorX = (_parameters.Width - doorWidth) / 2;

            var doorBox = new Box(Plane.WorldXY, new Interval(doorX, doorX + doorWidth), new Interval(-doorDepth, 0), new Interval(0, doorHeight));
            return doorBox.ToBrep();
        }
    }
}
