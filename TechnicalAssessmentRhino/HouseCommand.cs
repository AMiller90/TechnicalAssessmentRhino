using Rhino;
using Rhino.Commands;
using Rhino.Input;
using Rhino.Input.Custom;

using TechnicalAssessmentRhino.Models;
using TechnicalAssessmentRhino.Geometry;

namespace TechnicalAssessmentRhino
{
    public class HouseCommand : Command
    {
        public HouseCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static HouseCommand Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "House";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            var getWidth = new GetNumber();
            getWidth.SetCommandPrompt("Enter house width");
            getWidth.SetDefaultNumber(10);

            if (getWidth.Get() != GetResult.Number)
                return getWidth.CommandResult();

            double width = getWidth.Number();

            if(width <= 0)
            {
                RhinoApp.WriteLine("House width must be greater than zero.");
                return Result.Failure;
            }
            RhinoApp.WriteLine($"House width: {width}");

            var getdepth = new GetNumber();
            getdepth.SetCommandPrompt("Enter house depth");
            getdepth.SetDefaultNumber(8);

            if (getdepth.Get() != GetResult.Number)
                return getdepth.CommandResult();

            double depth = getdepth.Number();
            
            if (depth <= 0)
            {
                RhinoApp.WriteLine("House depth must be greater than zero.");
                return Result.Failure;
            }
            RhinoApp.WriteLine($"House depth: {depth}");

            var getHeight = new GetNumber();
            getHeight.SetCommandPrompt("Enter house height");
            getHeight.SetDefaultNumber(8);

            if (getHeight.Get() != GetResult.Number)
                return getHeight.CommandResult();

            double height = getHeight.Number();
            if (height <= 0)
            {
                RhinoApp.WriteLine("House height must be greater than zero.");
                return Result.Failure;
            }
            RhinoApp.WriteLine($"House height: {height}");

            var parameters = new HouseParameters(width, depth, height);

            RhinoApp.WriteLine(
                $"House dimensions: Width={parameters.Width}, " +
                $"Depth={parameters.Depth}, " +
                $"Height={parameters.Height}");

            HouseBuilder builder = new HouseBuilder(parameters);

            var body = builder.BuildBody();
            doc.Objects.AddBrep(body);
            doc.Views.Redraw();

            RhinoApp.WriteLine("House body created.");

            return Result.Success;
        }
    }
}
