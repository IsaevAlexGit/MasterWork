using System.Collections.Generic;

namespace Optimum
{
    // Класс "Объект социальной инфраструктуры"
    public class Facility
    {
        // Идентификатор объекта социальной инфраструктуры
        public int idFacility { get; set; }
        // Координата Х объекта социальной инфраструктуры
        public double x { get; set; }
        // Координата Y объекта социальной инфраструктуры
        public double y { get; set; }
        // Список с информацией об объекте социальной инфраструктуры
        public List<string> infoAboutFacility { get; set; }
        
        /// <summary>
        /// Конструктор
        /// </summary>
        public Facility() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Id">Идентификатор объекта социальной инфраструктуры</param>
        /// <param name="X">Координата Х</param>
        /// <param name="Y">Координата Y</param>
        /// <param name="InfoAboutFacility">Список с информацией об объекте социальной инфраструктуры</param>
        public Facility(int Id, double X, double Y, List<string> InfoAboutFacility)
        {
            idFacility = Id;
            x = X;
            y = Y;
            infoAboutFacility = InfoAboutFacility;
        }
    }
}