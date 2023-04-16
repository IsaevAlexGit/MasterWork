using System;
using System.Drawing;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;

namespace Optimum
{
    public class RenderUserMarker
    {
        // Слой с маркерами пользователя
        private SublayerLocation _subLocationCandidates = new SublayerLocation();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="gmap">Карта</param>
        /// <param name="subUser">Слой пользователя</param>
        public RenderUserMarker(GMapControl gmap, SublayerLocation subUser)
        {
            _subLocationCandidates = subUser;
        }

        // Радиус поиска и оптимум
        private int _radiusSearch;
        private List<OptimalZone> _optimalZones;

        /// <summary>
        /// Инициализация точек на карте
        /// </summary>
        /// <param name="gmap">Карта</param>
        /// <param name="radius">Радиус</param>
        /// <param name="optimalZones">Оптимальные точки</param>
        public void InitializationPoint(GMapControl gmap, int radius, List<OptimalZone> optimalZones)
        {
            _radiusSearch = radius;
            _optimalZones = optimalZones;

            // Очистить у слоя маркеры
            _subLocationCandidates.overlay.Markers.Clear();
            // Очистить у слоя полигоны
            _subLocationCandidates.overlay.Polygons.Clear();
            // Убрать слой с карты
            gmap.Overlays.Remove(_subLocationCandidates.overlay);
            // Очистка слоя
            _subLocationCandidates.overlay.Clear();
            if (_subLocationCandidates.listWithLocation.Count != 0)
            {
                // Добавление слоя на карту
                gmap.Overlays.Add(_subLocationCandidates.overlay);
                // Инициализация маркеров
                _InitializationMarkers();
                // Делаем слой видимым
                _subLocationCandidates.overlay.IsVisibile = true;
            }
        }

        /// <summary>
        /// Инициализация маркеров
        /// </summary>
        /// <param name="_subUser"></param>
        private void _InitializationMarkers()
        {
            int IDcandidte = 1;
            if (_optimalZones.Count != 0)
            {
                int IDoptimum = 1;
                
                for (int i = 0; i < _optimalZones.Count; i++)
                {
                    _optimalZones[i].optimal.x = Math.Round(_optimalZones[i].optimal.x, 6);
                    _optimalZones[i].optimal.y = Math.Round(_optimalZones[i].optimal.y, 6);
                }

                // Список с маркерами пользователя
                for (int i = 0; i < _subLocationCandidates.listWithLocation.Count; i++)
                {
                    double roundX_Location = Math.Round(_subLocationCandidates.listWithLocation[i].x, 6);
                    double roundY_Location = Math.Round(_subLocationCandidates.listWithLocation[i].y, 6);

                    bool flag = false;
                    for (int j = 0; j < _optimalZones.Count; j++)
                    {
                        if (_optimalZones[j].optimal.x == roundX_Location && _optimalZones[j].optimal.y == roundY_Location)
                        {
                            // Значок маркера
                            Bitmap icon = new Bitmap(_subLocationCandidates.iconOfOverlay);
                            // Координаты маркера
                            PointLatLng point = new PointLatLng(_subLocationCandidates.listWithLocation[i].x, _subLocationCandidates.listWithLocation[i].y);
                            GMarkerGoogle marker = new GMarkerGoogle(point, icon);
                            marker.ToolTip = new GMapRoundedToolTip(marker);
                            marker.Offset = new Point(-icon.Width / 2, -icon.Height / 2);

                            string TextInfo = "Оптимум #" + IDoptimum + Environment.NewLine + "Эту точки выбрали:" + Environment.NewLine;

                            for (int k = 0; k < _optimalZones[j].namesConvolutiones.Count; k++)
                                // Вывести все данные с новой строки
                                TextInfo = TextInfo + "Метод №" + (k + 1) + ": " + _optimalZones[j].namesConvolutiones[k] + Environment.NewLine;

                            // Подпись маркера для оптимума
                            marker.ToolTipText = TextInfo;

                            flag = true;
                            IDoptimum++;
                            // marker.ToolTipMode = MarkerTooltipMode.Always;
                            // Добавление маркера на слой
                            _subLocationCandidates.overlay.Markers.Add(marker);
                            // Отрисовка окружности у маркера
                            _InitializationZoneOptimum(point);
                        }
                    }
                    if (!flag)
                    {
                        // Значок маркера
                        Bitmap icon = new Bitmap(_subLocationCandidates.iconOfOverlay);
                        // Координаты маркера
                        PointLatLng point = new PointLatLng(_subLocationCandidates.listWithLocation[i].x, _subLocationCandidates.listWithLocation[i].y);
                        GMarkerGoogle marker = new GMarkerGoogle(point, icon);
                        marker.ToolTip = new GMapRoundedToolTip(marker);
                        marker.Offset = new Point(-icon.Width / 2, -icon.Height / 2);
                        // Подпись маркера
                        marker.ToolTipText = "Кандидат #" + IDcandidte;
                        IDcandidte++;
                        // Добавление маркера на слой
                        _subLocationCandidates.overlay.Markers.Add(marker);
                        // Отрисовка окружности у маркера
                        _InitializationZone(point);
                    }
                }
            }

            else
            {
                // Список с маркерами пользователя
                for (int i = 0; i < _subLocationCandidates.listWithLocation.Count; i++)
                {
                    // Значок маркера
                    Bitmap icon = new Bitmap(_subLocationCandidates.iconOfOverlay);
                    // Координаты маркера
                    PointLatLng point = new PointLatLng(_subLocationCandidates.listWithLocation[i].x, _subLocationCandidates.listWithLocation[i].y);
                    GMarkerGoogle marker = new GMarkerGoogle(point, icon);
                    marker.ToolTip = new GMapRoundedToolTip(marker);
                    marker.Offset = new Point(-icon.Width / 2, -icon.Height / 2);
                    // Подпись маркера
                    marker.ToolTipText = "Кандидат #" + IDcandidte;
                    IDcandidte++;
                    // Добавление маркера на слой
                    _subLocationCandidates.overlay.Markers.Add(marker);
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
            _subLocationCandidates.overlay.Polygons.Add(_polygon);
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
            _subLocationCandidates.overlay.Polygons.Add(_polygon);
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
        }

        /// <summary>
        /// Отрисовка окружности оптимальной точки
        /// </summary>
        /// <param name="centre">Центр окружности</param>
        /// <param name="radius">Радиус окружности</param>
        private void _CreateCircleOptimum(PointLatLng centre, double radius)
        {
            // Список для полигонов
            _listPointsForCircle = new List<PointLatLng>();
            // Для каждого сегмента
            for (int i = 0; i < _segments; i++)
                _listPointsForCircle.Add(_FindPointAtDistanceFrom(centre, i * (Math.PI / 180), radius / 1000));

            // Создание полигона
            _polygon = new GMapPolygon(_listPointsForCircle, "Circle");
            // Заливка полигона и его прозрачность
            _polygon.Fill = new SolidBrush(Color.FromArgb(100, Color.Green));
            // Цвет и толщина границы окружности
            _polygon.Stroke = new Pen(Color.Transparent, 0);
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