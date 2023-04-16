using System.Drawing;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.WindowsForms;

namespace Optimum
{
    public class RenderTerritory
    {
        // Слой для отображения территории
        private SublayerLocation _ListBoundaryPointsTerritory = new SublayerLocation();

       /// <summary>
       /// Конструктор
       /// </summary>
       /// <param name="gmap">Карта</param>
       /// <param name="layer">Слой с границей территории</param>
        public RenderTerritory(GMapControl gmap, SublayerLocation layer)
        {
            _ListBoundaryPointsTerritory = layer;
        }

        // Список точек для построения полигона
        private List<PointLatLng> _pointsPolygon = new List<PointLatLng>();

        /// <summary>
        /// Отображение города
        /// </summary>
        /// <param name="gmap">Карта</param>
        public void DrawTerritory(GMapControl gmap)
        {
            // Очистить у слоя маркеры
            _ListBoundaryPointsTerritory.overlay.Markers.Clear();
            // Убрать слой с карты
            gmap.Overlays.Remove(_ListBoundaryPointsTerritory.overlay);
            // Очистка слоя
            _ListBoundaryPointsTerritory.overlay.Clear();
            // Очистка списка точек
            _pointsPolygon.Clear();

            // Создание списка точек для полигона
            for (int i = 0; i < _ListBoundaryPointsTerritory.listWithLocation.Count; i++)
                _pointsPolygon.Add(new PointLatLng(_ListBoundaryPointsTerritory.listWithLocation[i].x, _ListBoundaryPointsTerritory.listWithLocation[i].y));

            // Создание полигона из считанных точек
            var polygon = new GMapPolygon(_pointsPolygon, "territory");
            // Заливка полигона красным цветом, прозрачностью 40
            polygon.Fill = new SolidBrush(Color.FromArgb(40, Color.Red));
            // Граница красного цвета
            polygon.Stroke = new Pen(Color.Red);
            // Добавление полигона на слой
            _ListBoundaryPointsTerritory.overlay.Polygons.Add(polygon);
            // Добавление слоя на карту
            gmap.Overlays.Add(_ListBoundaryPointsTerritory.overlay);
            _ListBoundaryPointsTerritory.overlay.IsVisibile = true;
        }

        /// <summary>
        /// Очистка отображения районов или города
        /// </summary>
        /// <param name="gmap">Карта</param>
        public void ClearTerritory(GMapControl gmap)
        {
            // Очистить у слоя маркеры
            _ListBoundaryPointsTerritory.overlay.Markers.Clear();
            // Очистить у слоя полигоны
            _ListBoundaryPointsTerritory.overlay.Polygons.Clear();
            // Убрать слой с карты
            gmap.Overlays.Remove(_ListBoundaryPointsTerritory.overlay);
            // Убрать видимость слоя
            _ListBoundaryPointsTerritory.overlay.IsVisibile = false;
            // Очистка слоя
            _ListBoundaryPointsTerritory.overlay.Clear();
            // Очистка списка точек
            _pointsPolygon.Clear();
        }
    }
}