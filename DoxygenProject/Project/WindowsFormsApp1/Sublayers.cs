using System.Collections.Generic;
using GMap.NET.WindowsForms;

namespace Optimum
{
    //! Класс "Слой для точек"
    public class SublayerLocation
    {
        public string nameOfOverlay;
        public List<Location> listWithLocation;
        public string iconOfOverlay;
        public GMapOverlay overlay;

        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <permission cref="">Доступ к функции: public</permission>
        public SublayerLocation() { }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Name">Название слоя</param>
        /// <param name="Icon">Значок слоя</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public SublayerLocation(string Name, string Icon)
        {
            nameOfOverlay = Name;
            iconOfOverlay = Icon;
        }
    }

    //! Класс "Слой для объектов социальной инфраструктуры"
    public class SublayerFacility
    {
        public string nameOfOverlay;
        public List<Facility> listWithFacility;
        public string iconOfOverlay;
        public GMapOverlay overlay;

        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <permission cref="">Доступ к функции: public</permission>
        public SublayerFacility() { }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Name">Название слоя</param>
        /// <param name="Icon">Значок слоя</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public SublayerFacility(string Name, string Icon)
        {
            nameOfOverlay = Name;
            iconOfOverlay = Icon;
        }
    }

    //! Класс "Слой для полигонов"
    public class SublayerPolygon
    {
        public string nameOfOverlay;
        public List<Polygon> listWithPolygons;
        public string iconOfOverlay;
        public GMapOverlay overlay;

        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <permission cref="">Доступ к функции: public</permission>
        public SublayerPolygon() { }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Name">Название слоя</param>
        /// <param name="Icon">Значок слоя</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public SublayerPolygon(string Name, string Icon)
        {
            nameOfOverlay = Name;
            iconOfOverlay = Icon;
        }
    }
}