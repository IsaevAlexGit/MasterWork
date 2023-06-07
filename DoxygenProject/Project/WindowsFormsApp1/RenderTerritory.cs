using System.Drawing;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace Optimum
{
    //! Класс "Отрисовщик зоны анализа"
    public class RenderTerritory
    {
        //! Слой для отображения зоны анализа
        private SublayerLocation _sublayerBoundaryPointsTerritory = new SublayerLocation();
        //! Список точек для построения полигона
        private List<PointLatLng> _listPointsPolygon = new List<PointLatLng>();

        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="gmap">Карта</param>
        /// <param name="sublayerTerritory">Слой с зоной анализа</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public RenderTerritory(SublayerLocation sublayerTerritory)
        {
            _sublayerBoundaryPointsTerritory = sublayerTerritory;
        }

        /*!
         \version 1.0
         */
        /// <summary>
        /// Отображение зоны анализа
        /// </summary>
        /// <param name="gmap">Карта</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public void DrawTerritory(GMapControl gmap)
        {
            // Очистить у слоя маркеры
            _sublayerBoundaryPointsTerritory.overlay.Markers.Clear();
            // Убрать слой с карты
            gmap.Overlays.Remove(_sublayerBoundaryPointsTerritory.overlay);
            // Очистка слоя
            _sublayerBoundaryPointsTerritory.overlay.Clear();
            // Очистка списка точек
            _listPointsPolygon.Clear();

            // Создание списка точек для полигона зоны анализа
            for (int i = 0; i < _sublayerBoundaryPointsTerritory.listWithLocation.Count; i++)
                _listPointsPolygon.Add(new PointLatLng(_sublayerBoundaryPointsTerritory.listWithLocation[i].x,
                    _sublayerBoundaryPointsTerritory.listWithLocation[i].y));

            // Создание полигона из считанных точек
            var polygon = new GMapPolygon(_listPointsPolygon, "territory");
            // Заливка полигона красным цветом, прозрачностью 40
            polygon.Fill = new SolidBrush(Color.FromArgb(40, Color.Red));
            // Граница красного цвета
            polygon.Stroke = new Pen(Color.Red);
            // Добавление полигона на слой
            _sublayerBoundaryPointsTerritory.overlay.Polygons.Add(polygon);
            // Добавление слоя на карту
            gmap.Overlays.Add(_sublayerBoundaryPointsTerritory.overlay);
            _sublayerBoundaryPointsTerritory.overlay.IsVisibile = true;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Скрыть отображение зоны анализа
        /// </summary>
        /// <param name="gmap">Карта</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public void ClearTerritory(GMapControl gmap)
        {
            // Очистить у слоя маркеры
            _sublayerBoundaryPointsTerritory.overlay.Markers.Clear();
            // Очистить у слоя полигоны
            _sublayerBoundaryPointsTerritory.overlay.Polygons.Clear();
            // Убрать слой с карты
            gmap.Overlays.Remove(_sublayerBoundaryPointsTerritory.overlay);
            // Убрать видимость слоя
            _sublayerBoundaryPointsTerritory.overlay.IsVisibile = false;
            // Очистка слоя
            _sublayerBoundaryPointsTerritory.overlay.Clear();
            // Очистка списка точек
            _listPointsPolygon.Clear();
        }
    }
}