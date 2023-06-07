using System.Collections.Generic;

namespace Optimum
{
    //! Класс "Оптимальная зона"
    public class OptimalZone
    {
        //! Оптимальная буферная зона
        public BufferZone optimalZone { get; set; }
        //! Список сверток, которые выбрали буферную зону оптимальной
        public List<string> namesConvolutiones { get; set; }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <permission cref="">Доступ к функции: public</permission>
        public OptimalZone() { }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="OptimalZone">Оптимальная точка</param>
        /// <param name="Convolutiones">Названия сверток, которые проголосовали за эту оптимальную точку</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public OptimalZone(BufferZone OptimalZone, List<string> Convolutiones)
        {
            optimalZone = OptimalZone;
            namesConvolutiones = Convolutiones;
        }
    }
}