using System;
using System.Drawing;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;

namespace Optimum
{
    class RenderAutoPoints
    {
        // Слой с маркерами пользователя
        private SublayerLocation _subLocationAutoPoints = new SublayerLocation();
        // Радиус поиска и оптимум
        private int _radiusSearch;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="gmap">Карта</param>
        /// <param name="subUser">Слой пользователя</param>
        public RenderAutoPoints(GMapControl gmap, SublayerLocation subUser)
        {
            _subLocationAutoPoints = subUser;
        }

        /// <summary>
        /// Отрисовать звездочками идеальные оптимумы (1-10)
        /// </summary>
        /// <param name="gmap"></param>
        /// <param name="_list"></param>
        public void InitializationIdealPoints(GMapControl gmap, List<BufferZone> _list)
        {
            // Очистить у слоя маркеры
            _subLocationAutoPoints.overlay.Markers.Clear();
            // Очистить у слоя полигоны
            _subLocationAutoPoints.overlay.Polygons.Clear();
            // Убрать слой с карты
            gmap.Overlays.Remove(_subLocationAutoPoints.overlay);
            // Очистка слоя
            _subLocationAutoPoints.overlay.Clear();

            // Добавление слоя на карту
            gmap.Overlays.Add(_subLocationAutoPoints.overlay);

            // Список с маркерами пользователя
            for (int i = 0; i < _list.Count; i++)
            {
                // Значок маркера
                Bitmap icon = new Bitmap(_subLocationAutoPoints.iconOfOverlay);
                // Координаты маркера
                PointLatLng point = new PointLatLng(_list[i].x, _list[i].y);
                GMarkerGoogle marker = new GMarkerGoogle(point, icon);
                marker.ToolTip = new GMapRoundedToolTip(marker);
                marker.Offset = new Point(-icon.Width / 2, -icon.Height / 2);
                // Подпись маркера
                marker.ToolTipText = "Авто-оптимум #" + (_subLocationAutoPoints.overlay.Markers.Count + 1);
                // Добавление маркера на слой
                _subLocationAutoPoints.overlay.Markers.Add(marker);
            }
            // Делаем слой видимым
            _subLocationAutoPoints.overlay.IsVisibile = true;
        }

        /// <summary>
        /// Инициализация точек на карте
        /// </summary>
        /// <param name="gmap">Карта</param>
        /// <param name="radius">Радиус</param>
        /// <param name="optimum">Оптимальная точка</param>
        public void InitializationPoint(GMapControl gmap, int radius)
        {
            _radiusSearch = radius;
            // Очистить у слоя маркеры
            _subLocationAutoPoints.overlay.Markers.Clear();
            // Очистить у слоя полигоны
            _subLocationAutoPoints.overlay.Polygons.Clear();
            // Убрать слой с карты
            gmap.Overlays.Remove(_subLocationAutoPoints.overlay);
            // Очистка слоя
            _subLocationAutoPoints.overlay.Clear();
            // Добавление слоя на карту
            gmap.Overlays.Add(_subLocationAutoPoints.overlay);
            // Инициализация маркеров
            _InitializationMarkers(_subLocationAutoPoints);
            // Делаем слой видимым
            _subLocationAutoPoints.overlay.IsVisibile = false;
        }

        /// <summary>
        /// Инициализация маркеров
        /// </summary>
        /// <param name="_subUser"></param>
        private void _InitializationMarkers(SublayerLocation _subUser)
        {
            // Список с маркерами пользователя
            for (int i = 0; i < _subUser.listWithLocation.Count; i++)
            {
                // Значок маркера
                Bitmap icon = new Bitmap(_subUser.iconOfOverlay);
                // Координаты маркера
                PointLatLng point = new PointLatLng(_subUser.listWithLocation[i].x, _subUser.listWithLocation[i].y);
                GMarkerGoogle marker = new GMarkerGoogle(point, icon);
                marker.ToolTip = new GMapRoundedToolTip(marker);
                marker.Offset = new Point(-icon.Width / 2, -icon.Height / 2);
                // Подпись маркера
                marker.ToolTipText = "Авто-оптимум #" + (_subUser.overlay.Markers.Count + 1);
                // Добавление маркера на слой
                _subUser.overlay.Markers.Add(marker);
                // Отрисовка окружности у маркера
                _InitializationZone(point);
            }
        }

        private GMapPolygon _polygon;
        // Доля от окружности
        private readonly int _segments = 360;
        private List<PointLatLng> _listPointsForCircle;
        private const double CONVERSION_FROM_DEGREES_TO_RADIANS = Math.PI / 180;
        private const double CONVERSION_FROM_RADIANS_TO_DEGREES = 180 / Math.PI;
        // Радиус планеты Земля = 6 371 км.
        private const double RADIUS_OF_THE_EARTH_IN_KILOMETERS = 6371.01;

        /// <summary>
        /// Инициализация буферной зоны
        /// </summary>
        /// <param name="point">Точка</param>
        private void _InitializationZone(PointLatLng point)
        {
            // Отрисовка окружности
            _CreateCircle(point, _radiusSearch);
            // Добавление полигона на слой карты
            _subLocationAutoPoints.overlay.Polygons.Add(_polygon);
        }

        /// <summary>
        /// Отрисовка окружности
        /// </summary>
        /// <param name="centre">Центр окружности</param>
        /// <param name="radius">Радиус окружности</param>
        private void _CreateCircle(PointLatLng centre, double radius)
        {
            // Список для полигонов
            _listPointsForCircle = new List<PointLatLng>();
            // Для каждого сегмента
            for (int i = 0; i < _segments; i++)
                _listPointsForCircle.Add(_FindPointAtDistanceFrom(centre, i * (Math.PI / 180), radius / 1000));

            // Создание полигона
            _polygon = new GMapPolygon(_listPointsForCircle, "Circle");
            // Заливка полигона и его прозрачность
            _polygon.Fill = new SolidBrush(Color.FromArgb(100, Color.Red));
            // Цвет и толщина границы окружности
            _polygon.Stroke = new Pen(Color.Transparent, 0);
            _polygon.IsVisible = false;
        }

        /// <summary>
        /// Вычисление каждой точки окружности
        /// </summary>
        /// <param name="startPoint">Начальная точка (центр окружности)</param>
        /// <param name="initialBearingRadians">Точка сегмента (от 0 до 360)</param>
        /// <param name="distanceKilometres">Радиус в километрах</param>
        /// <returns>Точка, которая образует окружность</returns>
        private static PointLatLng _FindPointAtDistanceFrom(PointLatLng startPoint, double initialBearingRadians, double distanceKilometres)
        {
            // Cоотношение радиуса окружности к радиусу Земли = расстояние от центра до сегмента / радиус Земли
            var distRatio = distanceKilometres / RADIUS_OF_THE_EARTH_IN_KILOMETERS;

            // Синус соотношения
            var distRatioSine = Math.Sin(distRatio);
            // Косинус соотношения
            var distRatioCosine = Math.Cos(distRatio);

            // Перевод начальнрнр х и у (центр) в радианы
            var startLatRad = startPoint.Lat * CONVERSION_FROM_DEGREES_TO_RADIANS;
            var startLonRad = startPoint.Lng * CONVERSION_FROM_DEGREES_TO_RADIANS;

            // Синус и косинус х,у центра в радианах
            var startLatCos = Math.Cos(startLatRad);
            var startLatSin = Math.Sin(startLatRad);

            // Поиск х,у конечной точки, где будет стоять точка, из которых будет строиться круг
            // Формулы для расчета новой позиции
            var endLatRads = Math.Asin((startLatSin * distRatioCosine) + (startLatCos * distRatioSine * Math.Cos(initialBearingRadians)));
            var endLonRads = startLonRad + Math.Atan2(Math.Sin(initialBearingRadians) * distRatioSine * startLatCos,
                distRatioCosine - startLatSin * Math.Sin(endLatRads));

            // Конечные х,у в радианах переводим в градусы и возвращаем эту точку, до которой будет строиться линия
            return new PointLatLng(endLatRads * CONVERSION_FROM_RADIANS_TO_DEGREES, endLonRads * CONVERSION_FROM_RADIANS_TO_DEGREES);
        }
    }
}
