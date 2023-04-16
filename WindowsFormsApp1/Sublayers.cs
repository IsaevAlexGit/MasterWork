using System.Collections.Generic;
using GMap.NET.WindowsForms;

namespace Optimum
{
    public class SublayerLocation
    {
        public string nameOfOverlay;
        public List<Location> listWithLocation;
        public string iconOfOverlay;
        public GMapOverlay overlay;

        public SublayerLocation() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Name">Название слоя</param>
        /// <param name="Icon">Значок слоя</param>
        public SublayerLocation(string Name, string Icon)
        {
            nameOfOverlay = Name;
            iconOfOverlay = Icon;
        }
    }

    public class SublayerFacility
    {
        public string nameOfOverlay;
        public List<Facility> listWithFacility;
        public string iconOfOverlay;
        public GMapOverlay overlay;

        public SublayerFacility() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Name">Название слоя</param>
        /// <param name="Icon">Значок слоя</param>
        public SublayerFacility(string Name, string Icon)
        {
            nameOfOverlay = Name;
            iconOfOverlay = Icon;
        }
    }

    public class SublayerQuar
    {
        public string nameOfOverlay;
        public List<Quar> listWithQuar;
        public string iconOfOverlay;
        public GMapOverlay overlay;

        public SublayerQuar() { }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="Name">Название слоя</param>
        /// <param name="Icon">Значок слоя</param>
        public SublayerQuar(string Name, string Icon)
        {
            nameOfOverlay = Name;
            iconOfOverlay = Icon;
        }
    }
}