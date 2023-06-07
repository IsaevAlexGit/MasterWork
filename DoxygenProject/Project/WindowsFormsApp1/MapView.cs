using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;

namespace Optimum
{
    //! Класс для визуализации
    public class MapView
    {
        private MapModel _mapModel;
        private GMapControl _gmap = new GMapControl();

        //! Отрисовщик слоя
        private RenderFacility _renderFacility;
        //! Отрисовщик слоя
        private RenderTerritory _renderTerritory;
        //! Отрисовщик слоя
        private RenderPolygon _renderPolygon;
        //! Отрисовщик слоя
        private RenderUserMarker _renderUserPoints;
        //! Отрисовщик слоя
        private RenderAutoPoints _renderAutoPoints;
        //! Отрисовщик слоя
        private RenderNorma _renderNorma;
        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mapModel">Объект MapModel</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public MapView(MapModel mapModel)
        {
            _mapModel = mapModel;
            // Инициализация отрисовщиков
            _InitializationRenders();
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Инициализация всех отрисовщиков
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _InitializationRenders()
        {
            _renderFacility = new RenderFacility(_mapModel.GetSublayerFacility());
            _renderTerritory = new RenderTerritory( _mapModel.GetSublayerBorderPointsTerritory());
            _renderPolygon = new RenderPolygon(_mapModel.GetSublayerPolygonIcon());
            _renderUserPoints = new RenderUserMarker(_mapModel.GetSublayerUserPoints());
            _renderAutoPoints = new RenderAutoPoints(_mapModel.GetSublayerAutoUserPoints());
            _renderNorma = new RenderNorma(_mapModel.GetSublayerNorma());
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Отобразить все объекты социальной инфраструктуры
        /// </summary>
        /// <permission cref="">Доступ к функции: public</permission>
        public void DrawFacility()
        {
            _renderFacility.DrawFacility(_gmap);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Скрыть все объекты социальной инфраструктуры
        /// </summary>
        /// <permission cref="">Доступ к функции: public</permission>
        public void ClearFacility()
        {
            _renderFacility.ClearFacility(_gmap);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Обработка выводящейся информации при наведении на объект социальной инфраструктуры
        /// </summary>
        /// <param name="modeVisual">Режим вывода информации</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public void SetHoverTextForFacility(int modeVisual)
        {
            _renderFacility.SetMode(modeVisual);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Отобразить зону анализа
        /// </summary>
        /// <permission cref="">Доступ к функции: public</permission>
        public void DrawTerritory()
        {
            _renderTerritory.DrawTerritory(_gmap);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Скрыть зону анализа
        /// </summary>
        /// <permission cref="">Доступ к функции: public</permission>
        public void ClearTerritory()
        {
            _renderTerritory.ClearTerritory(_gmap);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Скрыть площадную раскраску
        /// </summary>
        /// <param name="sublayerPolygon"></param>
        /// <permission cref="">Доступ к функции: public</permission>
        public void ClearSquareColoring(SublayerPolygon sublayerPolygon)
        {
            sublayerPolygon.overlay.Polygons.Clear();
            sublayerPolygon.overlay.Markers.Clear();
            sublayerPolygon.overlay.IsVisibile = false;
            _gmap.Overlays.Remove(sublayerPolygon.overlay);
            sublayerPolygon.overlay.Clear();
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Отобразить точечную раскраску
        /// </summary>
        /// <param name="maxSelectedCriterion">Максимум критерия</param>
        /// <param name="minSelectedCriterion">Минимум критерия</param>
        /// <param name="shadesColor">Количество оттенков</param>
        /// <param name="indexSelectedCriterion">Индекс критерия</param>
        /// <param name="nameCriterion">Название критерия</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public void DrawIconColoring(double maxSelectedCriterion, double minSelectedCriterion, 
            int shadesColor, int indexSelectedCriterion, string nameCriterion)
        {
            _renderPolygon.ShowIconCriterion(_gmap, maxSelectedCriterion, minSelectedCriterion, shadesColor, 
                indexSelectedCriterion, _mapModel.arrayIcons, nameCriterion);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Скрыть точечную раскраску
        /// </summary>
        /// <permission cref="">Доступ к функции: public</permission>
        public void ClearIconColoring()
        {
            _renderPolygon.ClearIconPolygon(_gmap);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Проверка принадлежности точки зоне анализа
        /// </summary>
        /// <param name="point">Точка</param>
        /// <permission cref="">Доступ к функции: public</permission>
        /// <returns>Флаг принадлежности зоне анализа</returns>
        public bool CheckInsidePointTerritory(PointLatLng point)
        {
            // Принадлежность точки зоне анализа
            bool isInsydeTerritory = false;

            // Список граничных точек зоны анализа
            SublayerLocation listBoundaryPointsTerritory = _mapModel.GetSublayerBorderPointsTerritory();
            List<PointLatLng> pointsPolygon = new List<PointLatLng>();
            // Добавляем граничные точки для создания полигона для зоны анализа
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

        /*!
        \version 1.0
        */
        /// <summary>
        /// Проверка принадлежности точки полигонам
        /// </summary>
        /// <param name="point">Точка</param>
        /// <permission cref="">Доступ к функции: public</permission>
        /// <returns>Флаг принадлежности полигонам</returns>
        public bool CheckInsidePointPolygon(PointLatLng point)
        {
            // Слой для проверки
            SublayerPolygon sublayer = _mapModel.GetSublayerPolygonCheck();
            List<PointLatLng> tempListForCreatePolygon;
            // Принадлежность точки любому полигону
            bool isInsydePolygon = false;

            // Заполнение таблицы данными из списка с полигонами
            DataTable dataPolygons = _mapModel.CreateTableFromPolygons();
            int countOfPolygons = dataPolygons.Rows.Count;

            // Создаем для каждого полигона динамический список граничных точек
            List<PointLatLng>[] listBoundaryForEveryPolygon = new List<PointLatLng>[countOfPolygons];
            for (int i = 0; i < listBoundaryForEveryPolygon.Length; i++)
                listBoundaryForEveryPolygon[i] = new List<PointLatLng>();

            // Заполнение граничными точками каждого полигона
            for (int j = 0; j < countOfPolygons; j++)
            {
                tempListForCreatePolygon = new List<PointLatLng>();
                for (int k = 0; k < sublayer.listWithPolygons[j].listBoundaryPoints.Count; k++)
                {
                    Location tempPoint = new Location(sublayer.listWithPolygons[j].listBoundaryPoints[k].x,
                        sublayer.listWithPolygons[j].listBoundaryPoints[k].y);
                    PointLatLng pointForList = new PointLatLng(tempPoint.x, tempPoint.y);
                    tempListForCreatePolygon.Add(pointForList);
                }
                listBoundaryForEveryPolygon[j] = tempListForCreatePolygon;
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

        /*!
        \version 1.0
        */
        /// <summary>
        /// Отрисовка буферной зоны около точки-кандидата и оптимальных точек
        /// </summary>
        /// <permission cref="">Доступ к функции: public</permission>
        public void DrawPointBufferZone()
        {
            _renderUserPoints.InitializationPoint(_gmap, _mapModel.radiusBufferZone, _mapModel.optimalZones);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Отрисовка буферной зоны авто-кандидатов
        /// </summary>
        /// <param name="radiusForSearch">Радиус буферной зоны</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public void DrawAutoPointBufferZone(int radiusForSearch)
        {
            _renderAutoPoints.InitializationPoint(_gmap, radiusForSearch);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Отрисовка оптимальных зон авто-оптимумов
        /// </summary>
        /// <param name="listIdealBufferZoneForAutoSearch">Список буферных зон</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public void DrawAutoIdealPointBufferZone(List<BufferZone> listIdealBufferZoneForAutoSearch)
        {
            _renderAutoPoints.InitializationIdealPoints(_gmap, listIdealBufferZoneForAutoSearch);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Очистить авто-оптимумы
        /// </summary>
        /// <param name="sublayerAutoPoints">Слой с авто-оптимумами</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public void ClearIdealPoints(SublayerLocation sublayerAutoPoints)
        {
            sublayerAutoPoints.listWithLocation.Clear();
            sublayerAutoPoints.overlay.Polygons.Clear();
            sublayerAutoPoints.overlay.Markers.Clear();
            sublayerAutoPoints.overlay.Clear();
            sublayerAutoPoints.overlay.IsVisibile = false;
            _gmap.Overlays.Remove(sublayerAutoPoints.overlay);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Отрисовка буферных зон кандидатов нормы
        /// </summary>
        /// <param name="radiusForSearch">Радиус буферной зоны</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public void DrawNormaBufferZone(int radiusForSearch)
        {
            _renderNorma.InitializationPoint(_gmap, radiusForSearch);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Отрисовка буферных зон оптимумов нормы
        /// </summary>
        /// <param name="listIdealBufferZoneForNorma">Список буферных зон</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public void DrawNormaIdealPointBufferZone(List<BufferZone> listIdealBufferZoneForNorma)
        {
            _renderNorma.InitializationIdealPoints(_gmap, listIdealBufferZoneForNorma);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Очистка карты от нормы
        /// </summary>
        /// <param name="sublayerNorma">Слой c нормой</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public void ClearNormaPoints(SublayerLocation sublayerNorma)
        {
            sublayerNorma.listWithLocation.Clear();
            sublayerNorma.overlay.Polygons.Clear();
            sublayerNorma.overlay.Markers.Clear();
            sublayerNorma.overlay.Clear();
            sublayerNorma.overlay.IsVisibile = false;
            _gmap.Overlays.Remove(sublayerNorma.overlay);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Центрирование карты
        /// </summary>
        /// <param name="center">Точка для центрирования карты</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public void ChangeCenterMap(Location center)
        {
            _gmap.Position = new PointLatLng(center.x, center.y);
            _gmap.Zoom = 12;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Инициализация настроек карты
        /// </summary>
        /// <param name="gMapControl">Карта</param>
        /// <param name="center">Центр карты</param>
        /// <permission cref="">Доступ к функции: public</permission>
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