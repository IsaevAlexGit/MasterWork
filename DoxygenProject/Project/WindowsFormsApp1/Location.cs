namespace Optimum
{
    //! Класс "Точка на карте"
    public class Location
    {
        //! Координата Х
        public double x { get; set; }
        //! Координата Y
        public double y { get; set; }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <permission cref="">Доступ к функции: public</permission>
        public Location() { }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="X">Координата Х</param>
        /// <param name="Y">Координата Y</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public Location(double X, double Y)
        {
            x = X;
            y = Y;
        }
    }
}