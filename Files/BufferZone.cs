using System;
using System.Collections.Generic;

namespace Optimum
{
    // ICloneable позволяет копировать список объектов
    // Класс "Буферная зона"
    public class BufferZone : ICloneable
    {
        // Идентификатор буферной зоны
        public int idBufferZone { get; set; }
        // Координата Х буферной зоны
        public double x { get; set; }
        // Координата Y буферной зоны
        public double y { get; set; }
        // Радиус поиска буферной зоны
        public int lengthRadiusSearch { get; set; }
        // Массив значений частных критериев для буферной зоны
        public List<double> arrayValuesCriterionOnZone { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public BufferZone() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="IdBufferZone">Идентификатор буферной зоны</param>
        /// <param name="X">Координата Х</param>
        /// <param name="Y">Координата Y</param>
        /// <param name="LengthRadiusSearch">Радиус поиска буферной зоны</param>
        /// <param name="ArrayValuesCriterionOnZone">Массив значений частных критериев для буферной зоны</param>
        public BufferZone(int IdBufferZone, double X, double Y, int LengthRadiusSearch, List<double> ArrayValuesCriterionOnZone)
        {
            idBufferZone = IdBufferZone;
            x = X;
            y = Y;
            lengthRadiusSearch = LengthRadiusSearch;
            arrayValuesCriterionOnZone = ArrayValuesCriterionOnZone;
        }

        /// <summary>
        /// Клонирование объекта, но не ссылкой, а по значению
        /// </summary>
        /// <returns>объект класса BufferZone</returns>
        public object Clone()
        {
            return new BufferZone
            {
                idBufferZone = this.idBufferZone,
                x = this.x,
                y = this.y,
                lengthRadiusSearch = this.lengthRadiusSearch,
                arrayValuesCriterionOnZone = new List<double>(arrayValuesCriterionOnZone)
            };
        }
    }
}