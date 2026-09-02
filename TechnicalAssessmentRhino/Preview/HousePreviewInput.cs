using DrawingColor = System.Drawing.Color;

using Rhino;
using Rhino.Input.Custom;

using TechnicalAssessmentRhino.Geometry;
using TechnicalAssessmentRhino.Models;

namespace TechnicalAssessmentRhino.Preview
{
    public class HousePreviewInput : GetPoint
    {
        private OptionDouble _widthOption;
        private OptionDouble _depthOption;
        private OptionDouble _heightOption;

        public HouseParameters Parameters { get; private set; }

        public HousePreviewInput(HouseParameters parameters)
        {
            Parameters = parameters;

            _widthOption = new OptionDouble(
                parameters.Width,
                0.001,
                double.MaxValue);

            _depthOption = new OptionDouble(
                parameters.Depth,
                0.001,
                double.MaxValue);

            _heightOption = new OptionDouble(
                parameters.Height,
                0.001,
                double.MaxValue);

            AddOptionDouble(
                "Width",
                ref _widthOption);

            AddOptionDouble(
                "Depth",
                ref _depthOption);

            AddOptionDouble(
                "Height",
                ref _heightOption);
        }

        protected override void OnDynamicDraw(GetPointDrawEventArgs e)
        {
            base.OnDynamicDraw(e);

            Parameters = new HouseParameters(
                _widthOption.CurrentValue,
                _depthOption.CurrentValue,
                _heightOption.CurrentValue);

            var builder = new HouseBuilder(Parameters);

            DrawPreview(e, builder);
        }

        private static void DrawPreview(
            GetPointDrawEventArgs e,
            HouseBuilder builder)
        {
            var body = builder.BuildBody();
            var roof = builder.BuildRoof();
            var door = builder.BuildDoor();
            var chimney = builder.BuildChimney();

            e.Display.DrawBrepWires(body, DrawingColor.Red);
            e.Display.DrawBrepWires(roof, DrawingColor.Red);
            e.Display.DrawBrepWires(door, DrawingColor.Red);
            e.Display.DrawBrepWires(chimney, DrawingColor.Red);
        }
    }
}