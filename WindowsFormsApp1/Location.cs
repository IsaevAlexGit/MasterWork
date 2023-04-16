namespace Optimum
{
    public class Location
    {
        public double x { get; set; }
        public double y { get; set; }
        public Location() { }
        
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="X">Координата Х</param>
        /// <param name="Y">Координата Y</param>
        public Location(double X, double Y)
        {
            x = X;
            y = Y;
        }
    }
}