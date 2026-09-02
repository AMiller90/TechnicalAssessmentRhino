using Rhino;
using Rhino.Commands;
using Rhino.Input;
using Rhino.Input.Custom;

using TechnicalAssessmentRhino.Geometry;
using TechnicalAssessmentRhino.Models;
using TechnicalAssessmentRhino.Preview;

namespace TechnicalAssessmentRhino
{
    public class HouseCommand : Command
    {
        public HouseCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a reference in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static HouseCommand Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "House";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            var parameters = new HouseParameters(10, 8, 8);

            var preview = new HousePreviewInput(parameters);

            preview.SetCommandPrompt("Adjust the house or click to finish.");

            while (true)
            {
                var result = preview.Get();

                if (result == GetResult.Option)
                    continue;

                if (result != GetResult.Point)
                    return preview.CommandResult();

                break;
            }

            parameters = preview.Parameters;

            var builder = new HouseBuilder(parameters);

            var body = builder.BuildBody();
            var roof = builder.BuildRoof();
            var door = builder.BuildDoor();
            var chimney = builder.BuildChimney();

            doc.Objects.AddBrep(body);
            doc.Objects.AddBrep(roof);
            doc.Objects.AddBrep(door);
            doc.Objects.AddBrep(chimney);

            doc.Views.Redraw();

            RhinoApp.WriteLine("House created successfully.");

            return Result.Success;
        }
    }
}