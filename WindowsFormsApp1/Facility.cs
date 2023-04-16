using System.Collections.Generic;

namespace Optimum
{
    // Класс Объект инфраструктуры
    public class Facility
    {
        public int idSocialFacility { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public List<string> infoAboutFacility { get; set; }
        
        public Facility() { }
        
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Id">ID объекта</param>
        /// <param name="X">Координата Х объекта</param>
        /// <param name="Y">"Координата Y объекта</param>
        /// <param name="info">Перечень данных об объекте</param>
        public Facility(int Id, double X, double Y, List<string> info)
        {
            idSocialFacility = Id;
            x = X;
            y = Y;
            infoAboutFacility = info;
        }
    }
}