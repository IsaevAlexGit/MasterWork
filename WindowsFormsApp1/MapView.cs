using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System.IO;
using System.Drawing;

namespace Optimum
{
    public class MapView
    {
        private MapModel _mapModel;
        private GMapControl _gmap = new GMapControl();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mapModel">Объект MapModel</param>
        public MapView(MapModel mapModel)
        {
            _mapModel = mapModel;
            // Инициализация отрисовщиков
            _InitializationRenderers();
        }

        // Отрисовщики каждого слоя
        private RenderFacility _renderFacility;
        private RenderTerritory _renderTerritory;
        private RenderQuar _renderQuar;
        private RenderUserMarker _renderUserCandidate;
        private RenderAutoPoints _renderAutoPoints;

        private RenderNorma _renderNorma;

        /// <summary>
        /// Инициализация всех отрисовщиков
        /// </summary>
        private void _InitializationRenderers()
        {
            _renderFacility = new RenderFacility(_gmap, _mapModel.GetSublayerFacility());
            _renderTerritory = new RenderTerritory(_gmap, _mapModel.GetSublayerBorderPointsTerritory());
            _renderQuar = new RenderQuar(_gmap, _mapModel.GetSublayerQuarIcon());
            _renderUserCandidate = new RenderUserMarker(_gmap, _mapModel.GetSublayerUserPoints());
            _renderAutoPoints = new RenderAutoPoints(_gmap, _mapModel.GetSublayerAutoUserPoints());


            _renderNorma = new RenderNorma(_gmap, _mapModel.GetSublayerNorma());
        }

        /// <summary>
        /// Отображение всех объектов инфраструктуры
        /// </summary>
        public void DrawFacility()
        {
            _renderFacility.DrawFacility(_gmap);
        }

        /// <summary>
        /// Очистка карты от объектов инфраструктуры
        /// </summary>
        public void ClearFacility()
        {
            _renderFacility.ClearFacility(_gmap);
        }

        /// <summary>
        /// Обработка выводящейся информации при наведении на объект инфраструктуры
        /// </summary>
        /// <param name="modeVisual">Режим вывода информации</param>
        public void SetTextForFacility(int modeVisual)
        {
            _renderFacility.SetMode(modeVisual);
        }

        /// <summary>
        /// Отображение города
        /// </summary>
        public void DrawTerritory()
        {
            _renderTerritory.DrawTerritory(_gmap);
        }
        
        /// <summary>
        /// Очистка карты от города и районов
        /// </summary>
        public void ClearTerritory()
        {
            _renderTerritory.ClearTerritory(_gmap);
        }

        /// <summary>
        /// Очистка карты от площадной раскраски
        /// </summary>
        /// <param name="subQuartet"></param>
        public void ClearPolygonQuar(SublayerQuar subPolygon)
        {
            subPolygon.overlay.Polygons.Clear();
            subPolygon.overlay.Markers.Clear();
            subPolygon.overlay.IsVisibile = false;
            _gmap.Overlays.Remove(subPolygon.overlay);
            subPolygon.overlay.Clear();
        }

        /// <summary>
        /// Отображение точечной раскраски
        /// </summary>
        /// <param name="maxSelectedCriterion">Максимум критерия</param>
        /// <param name="minSelectedCriterion">Минимум критерия</param>
        /// <param name="shadesColor">Количество оттенков</param>
        /// <param name="indexSelectedCriterion">Индекс критерия</param>
        /// <param name="nameCriterion">Названия критерия</param>
        public void ShowIconColoring(double maxSelectedCriterion, double minSelectedCriterion, int shadesColor, int indexSelectedCriterion, string nameCriterion)
        {
            _renderQuar.ShowIconCriterion(_gmap, maxSelectedCriterion, minSelectedCriterion, shadesColor, indexSelectedCriterion, _mapModel.array_icons, nameCriterion);
        }

        /// <summary>
        /// Очистка карты от точечной раскраски
        /// </summary>
        public void ClearIconColoring()
        {
            _renderQuar.ClearIconQuartet(_gmap);
        }

        /// <summary>
        /// Проверка принадлежности точки городу
        /// </summary>
        /// <param name="point">Точка</param>
        /// <returns>Флаг принадлежности городу</returns>
        public bool CheckInsidePointTerritory(PointLatLng point)
        {
            // Принадлежность точки городу
            bool isInsydeTerritory = false;

            // Список граничных точек территории
            SublayerLocation listBoundaryPointsTerritory = _mapModel.GetSublayerBorderPointsTerritory();
            List<PointLatLng> pointsPolygon = new List<PointLatLng>();
            // Добавляем граничные точки для создания полигона для территории
            for (int i = 0; i < listBoundaryPointsTerritory.listWithLocation.Count; i++)
                pointsPolygon.Add(new PointLatLng(listBoundaryPointsTerritory.listWithLocation[i].x, listBoundaryPointsTerritory.listWithLocation[i].y));

            // Создание полигона
            var polygonForTerritory = new GMapPolygon(pointsPolygon, "territory");
            // Проверка принадлежности
            if (polygonForTerritory.IsInside(point))
                isInsydeTerritory = true;
            else
                isInsydeTerritory = false;
            return isInsydeTerritory;
        }

        /// <summary>
        /// Проверка принадлежности точки полигону
        /// </summary>
        /// <param name="point">Точка</param>
        /// <returns>Флаг принадлежности полигону</returns>
        public bool CheckInsidePointPolygon(PointLatLng point)
        {
            // Слой для проверки
            SublayerQuar sublayer = _mapModel.GetSublayerQuarCheck();
            List<PointLatLng> listTemp;
            // Принадлежность точки любому кварталу
            bool isInsydePolygon = false;

            // Заполнение таблицы данными из списка с кварталами
            DataTable dataquartets = _mapModel.CreateTableFromQuartets();
            int countOfPolygons = dataquartets.Rows.Count;

            List<PointLatLng>[] listBoundaryForEveryPolygon = new List<PointLatLng>[countOfPolygons];
            for (int i = 0; i < listBoundaryForEveryPolygon.Length; i++)
                listBoundaryForEveryPolygon[i] = new List<PointLatLng>();

            // Заполнение listTemp
            for (int j = 0; j < countOfPolygons; j++)
            {
                listTemp = new List<PointLatLng>();
                for (int k = 0; k < sublayer.listWithQuar[j].listBoundaryPoints.Count; k++)
                {
                    Location tempPoint = new Location(sublayer.listWithQuar[j].listBoundaryPoints[k].x, sublayer.listWithQuar[j].listBoundaryPoints[k].y);
                    PointLatLng pointForList = new PointLatLng(tempPoint.x, tempPoint.y);
                    listTemp.Add(pointForList);
                }
                listBoundaryForEveryPolygon[j] = listTemp;
            }

            // Добавление всех полигонов на слой
            for (int j = 0; j < countOfPolygons; j++)
            {
                var mapPolygon = new GMapPolygon(listBoundaryForEveryPolygon[j], "Polygon" + j.ToString());
                sublayer.overlay.Polygons.Add(mapPolygon);
            }

            // Все полигоны слоя
            var polygons = sublayer.overlay.Polygons;
            foreach (var polygon in polygons)
            {
                // В каждом полигоне слоя проверяем принадлежность точки полигону
                if (polygon.IsInside(point))
                    isInsydePolygon = true;
            }

            sublayer.overlay.Polygons.Clear();
            return isInsydePolygon;
        }

        /// <summary>
        /// Отрисовка маркера и окружности пользователя
        /// </summary>
        /// <param name="_radiusForSearch">Радиус буферной зоны</param>
        public void DrawPointBufferZone()
        {
            _renderUserCandidate.InitializationPoint(_gmap, _mapModel.radiusBufferZone, _mapModel._optimalZones);
        }

        /// <summary>
        /// Отрисовка автомаркера и окружности
        /// </summary>
        /// <param name="_radiusForSearch">Радиус буферной зоны</param>
        public void DrawAutoPointBufferZone(int _radiusForSearch)
        {
            _renderAutoPoints.InitializationPoint(_gmap, _radiusForSearch);
        }
        /// <summary>
        /// Отрисовка автомаркера и окружности
        /// </summary>
        /// <param name="_radiusForSearch">Радиус буферной зоны</param>
        public void DrawAutoIdealPointBufferZone(List<BufferZone> _list)
        {
            _renderAutoPoints.InitializationIdealPoints(_gmap, _list);
        }

        /// <summary>
        /// Убрать авто-кандидаты
        /// </summary>
        /// <param name="renderQuartet">Слой с кварталами</param>
        public void ClearIdealPoints(SublayerLocation sublayer)
        {
            sublayer.listWithLocation.Clear();
            sublayer.overlay.Polygons.Clear();
            sublayer.overlay.Markers.Clear();
            sublayer.overlay.Clear();

            sublayer.overlay.IsVisibile = false;
            _gmap.Overlays.Remove(sublayer.overlay);
        }





















        /// <summary>
        /// Отрисовка нормы
        /// </summary>
        /// <param name="_radiusForSearch">Радиус буферной зоны</param>
        public void DrawNormaBufferZone(int _radiusForSearch)
        {
            _renderNorma.InitializationPoint(_gmap, _radiusForSearch);
        }

        /// <summary>
        /// Отрисовка нормы
        /// </summary>
        /// <param name="_radiusForSearch">Радиус буферной зоны</param>
        public void DrawNormaIdealPointBufferZone(List<BufferZone> _list)
        {
            _renderNorma.InitializationIdealPoints(_gmap, _list);
        }

        public void ClearNormaPoints(SublayerLocation sub)
        {
            sub.listWithLocation.Clear();
            sub.overlay.Polygons.Clear();
            sub.overlay.Markers.Clear();
            sub.overlay.Clear();

            sub.overlay.IsVisibile = false;
            _gmap.Overlays.Remove(sub.overlay);
        }






        /// <summary>
        /// Центрирование карты
        /// </summary>
        /// <param name="center"></param>
        public void ChangeCenterMap(Location center)
        {
            _gmap.Position = new PointLatLng(center.x, center.y);
            _gmap.Zoom = 12;
        }

        /// <summary>
        /// Инициализация настроек карты
        /// </summary>
        /// <param name="gMapControl">Карта</param>
        public void InitGMapControl(GMapControl gMapControl, Location center)
        {
            _gmap = gMapControl;
            // Угол карты
            _gmap.Bearing = 0;
            // Перетаскивание правой кнопки мыши
            _gmap.CanDragMap = true;
            // Перетаскивание карты левой кнопкой мыши
            _gmap.DragButton = MouseButtons.Left;
            _gmap.GrayScaleMode = true;
            // Все маркеры будут показаны
            _gmap.MarkersEnabled = true;
            // Максимальное приближение
            _gmap.MaxZoom = 17;
            // Минимальное приближение
            _gmap.MinZoom = 2;
            // Курсор мыши в центр карты
            _gmap.MouseWheelZoomType = MouseWheelZoomType.MousePositionWithoutCenter;
            // Отключение нигативного режима
            _gmap.NegativeMode = false;
            // Разрешение полигонов
            _gmap.PolygonsEnabled = true;
            // Разрешение маршрутов
            _gmap.RoutesEnabled = true;
            // Скрытие внешней сетки карты
            _gmap.ShowTileGridLines = false;
            // При загрузке 12-кратное увеличение
            _gmap.Zoom = 12;
            // Убрать красный крестик по центру
            _gmap.ShowCenter = false;
            // Поставщик карты
            _gmap.MapProvider = GMapProviders.GoogleMap;
            // Карта работает только с Интернетом
            GMaps.Instance.Mode = AccessMode.ServerOnly;
            // Начальная точка загрузки карты
            _gmap.Position = new PointLatLng(center.x, center.y);
        }
    }
}