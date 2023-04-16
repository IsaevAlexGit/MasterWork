using System.Collections.Generic;

namespace Optimum
{
    public class OptimalZone
    {
        public BufferZone optimal { get; set; }
        public List<string> namesConvolutiones { get; set; }

        public OptimalZone() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="bz">Оптимальная точка</param>
        /// <param name="names">Названия сверток, которые проголосовали за эту оптимальную точку</param>
        public OptimalZone(BufferZone optimalZone, List<string> convolutiones)
        {
            optimal = optimalZone;
            namesConvolutiones = convolutiones;
        }
    }
}