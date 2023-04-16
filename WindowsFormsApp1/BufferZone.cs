using System;
using System.Collections.Generic;

namespace Optimum
{
    // ICloneable - позволяет копировать список объектов
    public class BufferZone : ICloneable
    {
        public int idBufferZone { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public int lengthRadiusSearch { get; set; }

        // Список чисел - которых столько же сколько критериев, и тут хранится их значение подсуммированное в каждой буферной зоне
        public List<double> arrayValuesCriterionOnZone { get; set; }

        public BufferZone() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Id">ID буферной зоны</param>
        /// <param name="X">Координата X буферной зоны</param>
        /// <param name="Y">Координата Y буферной зоны</param>
        /// <param name="LengthRadius">Радиус буферной зоны</param>
        /// <param name="listValues">Список значений всех критериев</param>
        public BufferZone(int Id, double X, double Y, int LengthRadius, List<double> listValues)
        {
            idBufferZone = Id;
            x = X;
            y = Y;
            lengthRadiusSearch = LengthRadius;
            arrayValuesCriterionOnZone = listValues;
        }

        /// <summary>
        /// Клонирование объекта, но не ссылкой, а по значению
        /// </summary>
        /// <returns></returns>
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