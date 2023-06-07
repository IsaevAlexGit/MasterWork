using System;
using System.Drawing;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;

namespace Optimum
{
    //! Класс "Отрисовщик авто-оптимумов"
    class RenderAutoPoints
    {
        //! Слой с маркерами пользователя
        private SublayerLocation _sublayerLocationAutoPoints = new SublayerLocation();
        //! Радиус поиска
        private int _radiusSearch;

        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="gmap">Карта</param>
        /// <param name="sublayerLocation">Слой пользователя</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public RenderAutoPoints(SublayerLocation sublayerLocation)
        {
            _sublayerLocationAutoPoints = sublayerLocation;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Отрисовать звездочками идеальные оптимумы (1-10)
        /// </summary>
        /// <param name="gmap">Карта</param>
        /// <param name="listBufferZones">Список буферных зон</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public void InitializationIdealPoints(GMapControl gmap, List<BufferZone> listBufferZones)
        {
            // Очистить у слоя маркеры
            _sublayerLocationAutoPoints.overlay.Markers.Clear();
            // Очистить у слоя полигоны
            _sublayerLocationAutoPoints.overlay.Polygons.Clear();
            // Убрать слой с карты
            gmap.Overlays.Remove(_sublayerLocationAutoPoints.overlay);
            // Очистка слоя
            _sublayerLocationAutoPoints.overlay.Clear();

            // Добавление слоя на карту
            gmap.Overlays.Add(_sublayerLocationAutoPoints.overlay);

            // Список с маркерами пользователя
            for (int i = 0; i < listBufferZones.Count; i++)
            {
                // Значок маркера
                Bitmap icon = new Bitmap(_sublayerLocationAutoPoints.iconOfOverlay);
                // Координаты маркера
                PointLatLng point = new PointLatLng(listBufferZones[i].x, listBufferZones[i].y);
                GMarkerGoogle marker = new GMarkerGoogle(point, icon);
                marker.ToolTip = new GMapRoundedToolTip(marker);
                marker.Offset = new Point(-icon.Width / 2, -icon.Height / 2);
                // Подпись маркера
                marker.ToolTipText = "Авто-оптимум #" + (_sublayerLocationAutoPoints.overlay.Markers.Count + 1);
                // Добавление маркера на слой
                _sublayerLocationAutoPoints.overlay.Markers.Add(marker);
            }
            // Делаем слой видимым
            _sublayerLocationAutoPoints.overlay.IsVisibile = true;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Инициализация точек на карте
        /// </summary>
        /// <param name="gmap">Карта</param>
        /// <param name="radiusAutoBufferZone">Радиус буферной зоны</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public void InitializationPoint(GMapControl gmap, int radiusAutoBufferZone)
        {
            // Радиус буферной зоны для авто-точек
            _radiusSearch = radiusAutoBufferZone;
            // Очистить у слоя маркеры
            _sublayerLocationAutoPoints.overlay.Markers.Clear();
            // Очистить у слоя полигоны
            _sublayerLocationAutoPoints.overlay.Polygons.Clear();
            // Убрать слой с карты
            gmap.Overlays.Remove(_sublayerLocationAutoPoints.overlay);
            // Очистка слоя
            _sublayerLocationAutoPoints.overlay.Clear();
            // Добавление слоя на карту
            gmap.Overlays.Add(_sublayerLocationAutoPoints.overlay);
            // Инициализация маркеров
            _InitializationMarkers(_sublayerLocationAutoPoints);
            // Делаем слой видимым
            _sublayerLocationAutoPoints.overlay.IsVisibile = false;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Отрисовка маркера на карте
        /// </summary>
        /// <param name="sublayerAutoZone">Слой с авто-точками</param>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _InitializationMarkers(SublayerLocation sublayerAutoZone)
        {
            // Список с координатами буферных зон
            for (int i = 0; i < sublayerAutoZone.listWithLocation.Count; i++)
            {
                // Значок маркера
                Bitmap icon = new Bitmap(sublayerAutoZone.iconOfOverlay);
                // Координаты маркера
                PointLatLng point = new PointLatLng(sublayerAutoZone.listWithLocation[i].x, sublayerAutoZone.listWithLocation[i].y);
                GMarkerGoogle marker = new GMarkerGoogle(point, icon);
                marker.ToolTip = new GMapRoundedToolTip(marker);
                marker.Offset = new Point(-icon.Width / 2, -icon.Height / 2);
                // Подпись маркера
                marker.ToolTipText = "Авто-оптимум #" + (sublayerAutoZone.overlay.Markers.Count + 1);
                // Добавление маркера на слой
                sublayerAutoZone.overlay.Markers.Add(marker);
                // Отрисовка окружности у маркера
                _InitializationZone(point);
            }
        }

        private GMapPolygon _polygon;
        //! Доля от окружности
        private readonly int _segments = 360;
        private List<PointLatLng> _listPointsForCircle;
        private const double CONVERSION_FROM_DEGREES_TO_RADIANS = Math.PI / 180;
        private const double CONVERSION_FROM_RADIANS_TO_DEGREES = 180 / Math.PI;
        //! Радиус планеты Земля = 6 371 км.
        private const double RADIUS_OF_THE_EARTH_IN_KILOMETERS = 6371.01;
        /*!
        \version 1.0
        */
        /// <summary>
        /// Инициализация буферной зоны
        /// </summary>
        /// <param name="point">Точка</param>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _InitializationZone(PointLatLng point)
        {
            // Отрисовка окружности
            _CreateCircle(point, _radiusSearch);
            // Добавление полигона на слой карты
            _sublayerLocationAutoPoints.overlay.Polygons.Add(_polygon);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Отрисовка окружности
        /// </summary>
        /// <param name="center">Центр окружности</param>
        /// <param name="radius">Радиус окружности</param>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _CreateCircle(PointLatLng center, double radius)
        {
            // Список для полигонов
            _listPointsForCircle = new List<PointLatLng>();
            // Для каждого сегмента
            for (int i = 0; i < _segments; i++)
                _listPointsForCircle.Add(_FindPointAtDistanceFrom(center, i * (Math.PI / 180), radius / 1000));

            // Создание полигона
            _polygon = new GMapPolygon(_listPointsForCircle, "Circle");
            // Заливка полигона и его прозрачность
            _polygon.Fill = new SolidBrush(Color.FromArgb(100, Color.Red));
            // Цвет и толщина границы окружности
            _polygon.Stroke = new Pen(Color.Transparent, 0);
            _polygon.IsVisible = false;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Вычисление каждой точки окружности
        /// </summary>
        /// <param name="startPoint">Начальная точка (центр окружности)</param>
        /// <param name="initialBearingRadians">Точка сегмента (от 0 до 360)</param>
        /// <param name="distanceKilometres">Радиус в километрах</param>
        /// <permission cref="">Доступ к функции: private</permission>
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
