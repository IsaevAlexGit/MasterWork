using System;
using System.Drawing;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;

namespace Optimum
{
    // Класс "Отрисовщик точек-кандидатов"
    public class RenderUserMarker
    {
        // Слой с маркерами пользователя
        private SublayerLocation _sublayerLocationCandidates = new SublayerLocation();
        // Радиус поиска и оптимумы
        private int _radiusSearch;
        private List<OptimalZone> _optimalZones;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="sublayerUserPoints">Слой пользователя</param>
        public RenderUserMarker(SublayerLocation sublayerUserPoints)
        {
            _sublayerLocationCandidates = sublayerUserPoints;
        }

        /// <summary>
        /// Инициализация точек на карте
        /// </summary>
        /// <param name="gmap">Карта</param>
        /// <param name="radius">Радиус поиска</param>
        /// <param name="optimalZones">Оптимальные точки</param>
        public void InitializationPoint(GMapControl gmap, int radius, List<OptimalZone> optimalZones)
        {
            _radiusSearch = radius;
            _optimalZones = optimalZones;

            // Очистить у слоя маркеры
            _sublayerLocationCandidates.overlay.Markers.Clear();
            // Очистить у слоя полигоны
            _sublayerLocationCandidates.overlay.Polygons.Clear();
            // Убрать слой с карты
            gmap.Overlays.Remove(_sublayerLocationCandidates.overlay);
            // Очистка слоя
            _sublayerLocationCandidates.overlay.Clear();
            // Добавление слоя на карту, если точек не 0
            if (_sublayerLocationCandidates.listWithLocation.Count != 0)
            {
                // Добавление слоя на карту
                gmap.Overlays.Add(_sublayerLocationCandidates.overlay);
                // Инициализация маркеров
                _InitializationMarkers();
                // Делаем слой видимым
                _sublayerLocationCandidates.overlay.IsVisibile = true;
            }
        }

        /// <summary>
        /// Инициализация маркеров
        /// </summary>
        private void _InitializationMarkers()
        {
            int idCandidte = 1;
            // Если отрисовать надо и кандидатов и оптимумы
            if (_optimalZones.Count != 0)
            {
                int idOptimum = 1;

                for (int i = 0; i < _optimalZones.Count; i++)
                {
                    _optimalZones[i].optimalZone.x = Math.Round(_optimalZones[i].optimalZone.x, 6);
                    _optimalZones[i].optimalZone.y = Math.Round(_optimalZones[i].optimalZone.y, 6);
                }

                // Сортировка оптимумов по количеству голосов сверток
                _optimalZones.Sort((a, b) => b.namesConvolutiones.Count.CompareTo(a.namesConvolutiones.Count));
                // Сколько выбрало сверток самый популярный оптимум
                int countConvolutionesBestOptimim = _optimalZones[0].namesConvolutiones.Count;

                // Список с маркерами пользователя
                for (int i = 0; i < _sublayerLocationCandidates.listWithLocation.Count; i++)
                {
                    double roundX_Location = Math.Round(_sublayerLocationCandidates.listWithLocation[i].x, 6);
                    double roundY_Location = Math.Round(_sublayerLocationCandidates.listWithLocation[i].y, 6);

                    bool flag = false;
                    // Проход по оптимальным зонам
                    for (int j = 0; j < _optimalZones.Count; j++)
                    {
                        // Если точка является оптимальной выводим ее зеленой
                        if (_optimalZones[j].optimalZone.x == roundX_Location && _optimalZones[j].optimalZone.y == roundY_Location)
                        {
                            // Значок маркера
                            Bitmap icon = new Bitmap(_sublayerLocationCandidates.iconOfOverlay);
                            // Координаты маркера
                            PointLatLng point = new PointLatLng(_sublayerLocationCandidates.listWithLocation[i].x,
                                _sublayerLocationCandidates.listWithLocation[i].y);
                            GMarkerGoogle marker = new GMarkerGoogle(point, icon);
                            marker.ToolTip = new GMapRoundedToolTip(marker);
                            marker.Offset = new Point(-icon.Width / 2, -icon.Height / 2);
                            // Надпись для оптимума
                            string TextInfo = "Оптимум #" + idOptimum + Environment.NewLine + "Эту точки выбрали:" + Environment.NewLine;
                            // Названия критериев, которые выбрали эту точку оптимальной
                            for (int k = 0; k < _optimalZones[j].namesConvolutiones.Count; k++)
                                // Вывести все данные с новой строки
                                TextInfo = TextInfo + "Метод №" + (k + 1) + ": " + _optimalZones[j].namesConvolutiones[k] + Environment.NewLine;
                            // Подпись маркера для оптимума
                            marker.ToolTipText = TextInfo;
                            // Точка является оптимальной
                            flag = true;
                            idOptimum++;
                            // Добавление маркера на слой
                            _sublayerLocationCandidates.overlay.Markers.Add(marker);
                            // Если оптимум набрал наибольшее количество голосов сверток
                            if (_optimalZones[j].namesConvolutiones.Count == countConvolutionesBestOptimim)
                                _InitializationBestZoneOptimum(point);
                            else
                                // Отрисовка окружности у маркера
                                _InitializationZoneOptimum(point);
                        }
                    }
                    // Если точка не является оптимальной, выводим ее красной
                    if (!flag)
                    {
                        // Значок маркера
                        Bitmap icon = new Bitmap(_sublayerLocationCandidates.iconOfOverlay);
                        // Координаты маркера
                        PointLatLng point = new PointLatLng(_sublayerLocationCandidates.listWithLocation[i].x,
                            _sublayerLocationCandidates.listWithLocation[i].y);
                        GMarkerGoogle marker = new GMarkerGoogle(point, icon);
                        marker.ToolTip = new GMapRoundedToolTip(marker);
                        marker.Offset = new Point(-icon.Width / 2, -icon.Height / 2);
                        // Подпись маркера
                        marker.ToolTipText = "Кандидат #" + idCandidte;
                        idCandidte++;
                        // Добавление маркера на слой
                        _sublayerLocationCandidates.overlay.Markers.Add(marker);
                        // Отрисовка окружности у маркера
                        _InitializationZone(point);
                    }
                }
            }
            // Если отрисовать надо только кандидатов
            else
            {
                // Список с маркерами пользователя
                for (int i = 0; i < _sublayerLocationCandidates.listWithLocation.Count; i++)
                {
                    // Значок маркера
                    Bitmap icon = new Bitmap(_sublayerLocationCandidates.iconOfOverlay);
                    // Координаты маркера
                    PointLatLng point = new PointLatLng(_sublayerLocationCandidates.listWithLocation[i].x,
                        _sublayerLocationCandidates.listWithLocation[i].y);
                    GMarkerGoogle marker = new GMarkerGoogle(point, icon);
                    marker.ToolTip = new GMapRoundedToolTip(marker);
                    marker.Offset = new Point(-icon.Width / 2, -icon.Height / 2);
                    // Подпись маркера
                    marker.ToolTipText = "Кандидат #" + idCandidte;
                    idCandidte++;
                    // Добавление маркера на слой
                    _sublayerLocationCandidates.overlay.Markers.Add(marker);
                    // Отрисовка окружности у маркера
                    _InitializationZone(point);
                }
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
            _sublayerLocationCandidates.overlay.Polygons.Add(_polygon);
        }

        /// <summary>
        /// Инициализация оптимальной буферной зоны
        /// </summary>
        /// <param name="point">Точка</param>
        private void _InitializationZoneOptimum(PointLatLng point)
        {
            // Отрисовка окружности
            _CreateCircleOptimum(point, _radiusSearch);
            // Добавление полигона на слой карты
            _sublayerLocationCandidates.overlay.Polygons.Add(_polygon);
        }

        /// <summary>
        /// Инициализация самой оптимальной буферной зоны
        /// </summary>
        /// <param name="point">Точка</param>
        private void _InitializationBestZoneOptimum(PointLatLng point)
        {
            // Отрисовка окружности
            _CreateCircleBestOptimum(point, _radiusSearch);
            // Добавление полигона на слой карты
            _sublayerLocationCandidates.overlay.Polygons.Add(_polygon);
        }

        /// <summary>
        /// Отрисовка окружности
        /// </summary>
        /// <param name="center">Центр окружности</param>
        /// <param name="radius">Радиус окружности</param>
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
        }

        /// <summary>
        /// Отрисовка окружности оптимальной точки
        /// </summary>
        /// <param name="center">Центр окружности</param>
        /// <param name="radius">Радиус окружности</param>
        private void _CreateCircleOptimum(PointLatLng center, double radius)
        {
            // Список для полигонов
            _listPointsForCircle = new List<PointLatLng>();
            // Для каждого сегмента
            for (int i = 0; i < _segments; i++)
                _listPointsForCircle.Add(_FindPointAtDistanceFrom(center, i * (Math.PI / 180), radius / 1000));

            // Создание полигона
            _polygon = new GMapPolygon(_listPointsForCircle, "Circle");
            // Заливка полигона и его прозрачность
            _polygon.Fill = new SolidBrush(Color.FromArgb(100, Color.Green));
            // Цвет и толщина границы окружности
            _polygon.Stroke = new Pen(Color.Transparent, 0);
        }

        /// <summary>
        /// Отрисовка окружности самых оптимальных точек
        /// </summary>
        /// <param name="center">Центр окружности</param>
        /// <param name="radius">Радиус окружности</param>
        private void _CreateCircleBestOptimum(PointLatLng center, double radius)
        {
            // Список для полигонов
            _listPointsForCircle = new List<PointLatLng>();
            // Для каждого сегмента
            for (int i = 0; i < _segments; i++)
                _listPointsForCircle.Add(_FindPointAtDistanceFrom(center, i * (Math.PI / 180), radius / 1000));

            // Создание полигона
            _polygon = new GMapPolygon(_listPointsForCircle, "Circle");
            // Заливка полигона и его прозрачность
            _polygon.Fill = new SolidBrush(Color.FromArgb(160, Color.Green));
            // Цвет и толщина границы окружности
            _polygon.Stroke = new Pen(Color.Yellow, 2);
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

            // Перевод начальной х и у (центр) в радианы
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