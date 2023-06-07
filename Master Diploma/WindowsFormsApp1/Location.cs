namespace Optimum
{
    // Класс "Точка на карте"
    public class Location
    {
        // Координата Х
        public double x { get; set; }
        // Координата Y
        public double y { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
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