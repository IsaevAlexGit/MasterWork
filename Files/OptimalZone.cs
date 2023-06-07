using System.Collections.Generic;

namespace Optimum
{
    // Класс "Оптимальная зона"
    public class OptimalZone
    {
        // Оптимальная буферная зона
        public BufferZone optimalZone { get; set; }
        // Список сверток, которые выбрали буферную зону оптимальной
        public List<string> namesConvolutiones { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public OptimalZone() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="OptimalZone">Оптимальная точка</param>
        /// <param name="Convolutiones">Названия сверток, которые проголосовали за эту оптимальную точку</param>
        public OptimalZone(BufferZone OptimalZone, List<string> Convolutiones)
        {
            optimalZone = OptimalZone;
            namesConvolutiones = Convolutiones;
        }
    }
}