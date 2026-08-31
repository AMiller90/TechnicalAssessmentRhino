using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

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

            RhinoApp.WriteLine($"House dimensions: Width={width}, Depth={depth}, Height={height}");

            return Result.Success;
        }
    }
}
