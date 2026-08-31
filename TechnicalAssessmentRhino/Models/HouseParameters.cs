namespace TechnicalAssessmentRhino.Models
{
    public class HouseParameters
    {
        public double Width { get; }
        public double Depth { get; }
        public double Height { get; }

        public HouseParameters(double width, double depth, double height)
        {
            Width = width;
            Depth = depth;
            Height = height;
        }
    }
}