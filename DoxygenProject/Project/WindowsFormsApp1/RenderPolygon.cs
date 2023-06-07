using System;
using System.Drawing;
using System.Collections.Generic;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;

namespace Optimum
{
    //! Класс "Отрисовщик точечной раскраски"
    public class RenderPolygon
    {
        //! Слой для отображения точечной раскраски
        private SublayerPolygon _sublayerPolygonIcon = new SublayerPolygon();

        //! Максимум критерия
        private double _maxSelectedCriterion = 0;
        //! Минимум критерия
        private double _minSelectedCriterion = 0;
        //! Количество оттенков
        private int _shadesColor = 0;
        //! Индекс выбранного критерия
        private int _indexSelectedCriterion = 0;
        //! Название критерия
        private string _nameSelectedCriterion = "";

        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="gmap">Карта</param>
        /// <param name="sublayerPolygon">Слой с полигонами</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public RenderPolygon(SublayerPolygon sublayerPolygon)
        {
            _sublayerPolygonIcon = sublayerPolygon;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Точечная раскраска
        /// </summary>
        /// <param name="gmap">Карта</param>
        /// <param name="maxCriterion">Максимум критерия</param>
        /// <param name="minCriterion">Минимум критерия</param>
        /// <param name="shades">Количество оттенков</param>
        /// <param name="indexCriterion">Индекс выбранного критерия</param>
        /// <param name="iconsForColoring">Массив значков для точечной раскраски</param>
        /// <param name="nameCriterion">Название критерия</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public void ShowIconCriterion(GMapControl gmap, double maxCriterion, double minCriterion, int shades,
            int indexCriterion, List<Image> iconsForColoring, string nameCriterion)
        {
            _maxSelectedCriterion = maxCriterion;
            _minSelectedCriterion = minCriterion;
            _shadesColor = shades;
            _indexSelectedCriterion = indexCriterion;
            _nameSelectedCriterion = nameCriterion;
            // Очистить у слоя маркеры
            _sublayerPolygonIcon.overlay.Markers.Clear();
            // Убрать слой с карты
            gmap.Overlays.Remove(_sublayerPolygonIcon.overlay);
            // Очистка слоя
            _sublayerPolygonIcon.overlay.Clear();
            // Добавление слоя на карту
            gmap.Overlays.Add(_sublayerPolygonIcon.overlay);
            // Инициализация маркеров
            _InitializationMarkers(_sublayerPolygonIcon, iconsForColoring);
            // Делаем слой видимым
            _sublayerPolygonIcon.overlay.IsVisibile = true;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Инициализация маркеров
        /// </summary>
        /// <param name="sublayerPolygon">Слой</param>
        /// <param name="iconsForColorng">Массив значков</param>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _InitializationMarkers(SublayerPolygon sublayerPolygon, List<Image> iconsForColorng)
        {
            // Получить массив значков, которые требуются для заданного количества оттенков
            _GetIconsForSelectedCountShades(iconsForColorng);

            _minSelectedCriterion = 0;
            // Разница между максимальным и минимальным значением критерия
            double diff = _maxSelectedCriterion - _minSelectedCriterion;
            // Делим разницу на количество оттенков
            double step = diff / _shadesColor;
            // Создаем список интервалов
            List<double> intervals = new List<double>();
            intervals.Add(_minSelectedCriterion);
            double k = _minSelectedCriterion + step;
            // Заполнить массив числами интервалов
            while (Math.Round(k, 1) <= _maxSelectedCriterion)
            {
                intervals.Add(k);
                k = k + step;
            }

            // Определить в какой диапазон попадает полигон
            for (int i = 0; i < sublayerPolygon.listWithPolygons.Count; i++)
            {
                for (int j = 0; j < intervals.Count - 1; j++)
                {
                    // Берем значения критерия каждого полигона и выводим их цветными точками в зависимости от принадлежности к интервалу
                    if (sublayerPolygon.listWithPolygons[i].valuesForEveryCriterionOfPolygon[_indexSelectedCriterion] >= intervals[j] &&
                        sublayerPolygon.listWithPolygons[i].valuesForEveryCriterionOfPolygon[_indexSelectedCriterion] <= intervals[j + 1])
                    {
                        Bitmap icon = new Bitmap(_usingArrayIcons[j]);
                        GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(sublayerPolygon.listWithPolygons[i].xCenterOfPolygon,
                            sublayerPolygon.listWithPolygons[i].yCenterOfPolygon), icon);
                        marker.ToolTip = new GMapRoundedToolTip(marker);
                        marker.Offset = new Point(-icon.Width / 2, -icon.Height / 2);
                        marker.ToolTipText = _nameSelectedCriterion + ": " + 
                            sublayerPolygon.listWithPolygons[i].valuesForEveryCriterionOfPolygon[_indexSelectedCriterion].ToString();
                        sublayerPolygon.overlay.Markers.Add(marker);
                    }
                }
            }
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Скрыть точечную раскраску
        /// </summary>
        /// <param name="gmap"></param>
        /// <permission cref="">Доступ к функции: public</permission>
        public void ClearIconPolygon(GMapControl gmap)
        {
            // Очистить у слоя маркеры
            _sublayerPolygonIcon.overlay.Markers.Clear();
            // Убрать слой с карты
            gmap.Overlays.Remove(_sublayerPolygonIcon.overlay);
            // Убрать видимость слоя
            _sublayerPolygonIcon.overlay.IsVisibile = false;
            // Очистка слоя
            _sublayerPolygonIcon.overlay.Clear();
        }

        //! Массив используемых значков для точечной раскраски
        private List<Image> _usingArrayIcons = new List<Image>();
        /*!
        \version 1.0
        */
        /// <summary>
        /// Получить значки для выбранного количества оттенков
        /// </summary>
        /// <param name="icons">Массив значков</param>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _GetIconsForSelectedCountShades(List<Image> icons)
        {
            // Очистить от прошлых изменений
            _usingArrayIcons.Clear();

            // 1 оттенок - зеленый
            if (_shadesColor == 1)
                _usingArrayIcons.Add(icons[9]);
            // 2 оттенка - зеленый и красный
            else if (_shadesColor == 2)
            {
                _usingArrayIcons.Add(icons[0]);
                _usingArrayIcons.Add(icons[9]);
            }
            // 3 оттенка - зеленый желтый красный
            else if (_shadesColor == 3)
            {
                _usingArrayIcons.Add(icons[0]);
                _usingArrayIcons.Add(icons[4]);
                _usingArrayIcons.Add(icons[9]);
            }
            else if (_shadesColor == 4)
            {
                _usingArrayIcons.Add(icons[0]);
                _usingArrayIcons.Add(icons[2]);
                _usingArrayIcons.Add(icons[4]);
                _usingArrayIcons.Add(icons[9]);
            }
            else if (_shadesColor == 5)
            {
                _usingArrayIcons.Add(icons[0]);
                _usingArrayIcons.Add(icons[2]);
                _usingArrayIcons.Add(icons[4]);
                _usingArrayIcons.Add(icons[6]);
                _usingArrayIcons.Add(icons[9]);
            }
            else if (_shadesColor == 6)
            {
                _usingArrayIcons.Add(icons[0]);
                _usingArrayIcons.Add(icons[2]);
                _usingArrayIcons.Add(icons[3]);
                _usingArrayIcons.Add(icons[4]);
                _usingArrayIcons.Add(icons[6]);
                _usingArrayIcons.Add(icons[9]);
            }
            else if (_shadesColor == 7)
            {
                _usingArrayIcons.Add(icons[0]);
                _usingArrayIcons.Add(icons[2]);
                _usingArrayIcons.Add(icons[3]);
                _usingArrayIcons.Add(icons[4]);
                _usingArrayIcons.Add(icons[6]);
                _usingArrayIcons.Add(icons[7]);
                _usingArrayIcons.Add(icons[9]);
            }
            else if (_shadesColor == 8)
            {
                _usingArrayIcons.Add(icons[0]);
                _usingArrayIcons.Add(icons[2]);
                _usingArrayIcons.Add(icons[3]);
                _usingArrayIcons.Add(icons[4]);
                _usingArrayIcons.Add(icons[5]);
                _usingArrayIcons.Add(icons[6]);
                _usingArrayIcons.Add(icons[7]);
                _usingArrayIcons.Add(icons[9]);
            }
            else if (_shadesColor == 9)
            {
                _usingArrayIcons.Add(icons[0]);
                _usingArrayIcons.Add(icons[1]);
                _usingArrayIcons.Add(icons[2]);
                _usingArrayIcons.Add(icons[3]);
                _usingArrayIcons.Add(icons[4]);
                _usingArrayIcons.Add(icons[5]);
                _usingArrayIcons.Add(icons[6]);
                _usingArrayIcons.Add(icons[7]);
                _usingArrayIcons.Add(icons[9]);
            }
            else
            {
                _usingArrayIcons.Add(icons[0]);
                _usingArrayIcons.Add(icons[1]);
                _usingArrayIcons.Add(icons[2]);
                _usingArrayIcons.Add(icons[3]);
                _usingArrayIcons.Add(icons[4]);
                _usingArrayIcons.Add(icons[5]);
                _usingArrayIcons.Add(icons[6]);
                _usingArrayIcons.Add(icons[7]);
                _usingArrayIcons.Add(icons[8]);
                _usingArrayIcons.Add(icons[9]);
            }
        }
    }
}