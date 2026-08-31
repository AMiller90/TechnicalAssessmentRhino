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
    }
}
