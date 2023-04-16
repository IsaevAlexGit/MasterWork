using System;
using System.Collections.Generic;
using System.Drawing;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;

namespace Optimum
{
    public class RenderQuar
    {
        // Слой для отображения точечной раскраски
        private SublayerQuar _sublayerPolygonIcon = new SublayerQuar();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="gmap">Карта</param>
        /// <param name="sublayerPolygon">Слой на карте</param>
        public RenderQuar(GMapControl gmap, SublayerQuar sublayerPolygon)
        {
            _sublayerPolygonIcon = sublayerPolygon;
        }

        // Максимум критерия
        private double maxSelectedCriterion = 0;
        // Минимум критерия
        private double minSelectedCriterion = 0;
        // Количество оттенков
        private int shadesColor = 0;
        // Индекс выбранного критерия
        private int indexSelectedCriterion = 0;
        // Название критерия
        private string nameSelectedCriterion = "";

        /// <summary>
        /// Точечная раскраска
        /// </summary>
        /// <param name="gmap">Карта</param>
        /// <param name="maxCriterion">Максимум критерия</param>
        /// <param name="minCriterion">Минимум критерия</param>
        /// <param name="shades">Количество оттенков</param>
        /// <param name="indexSelectedCriterion1">Индекс выбранного критерия</param>
        /// <param name="iconsForColoring">Массив значков для точечной раскраски</param>
        /// <param name="nameCriterion1">Название критерия</param>
        public void ShowIconCriterion(GMapControl gmap, double maxCriterion, double minCriterion, int shades,
            int indexCriterion, List<Image> iconsForColoring, string nameCriterion)
        {
            maxSelectedCriterion = maxCriterion;
            minSelectedCriterion = minCriterion;
            shadesColor = shades;
            indexSelectedCriterion = indexCriterion;
            nameSelectedCriterion = nameCriterion;
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

        /// <summary>
        /// Инициализация маркеров
        /// </summary>
        /// <param name="subPolygon">Слой</param>
        /// <param name="icons">Массив значков</param>
        private void _InitializationMarkers(SublayerQuar subPolygon, List<Image> icons)
        {
            GetIconsForSelectedCountShades(icons);

            minSelectedCriterion = 0;
            double diff = maxSelectedCriterion - minSelectedCriterion;
            double step = diff / shadesColor;
            List<double> intervals = new List<double>();
            intervals.Add(minSelectedCriterion);
            double k = minSelectedCriterion + step;
            // Заполнить массив числами интервалов
            while (Math.Round(k, 1) <= maxSelectedCriterion)
            {
                intervals.Add(k);
                k = k + step;
            }

            // Определить в какой диапазон попадает полигон
            for (int i = 0; i < subPolygon.listWithQuar.Count; i++)
            {
                for (int j = 0; j < intervals.Count - 1; j++)
                {
                    if (subPolygon.listWithQuar[i].valuesEveryCriterionForQuartet[indexSelectedCriterion] >= intervals[j] &&
                        subPolygon.listWithQuar[i].valuesEveryCriterionForQuartet[indexSelectedCriterion] <= intervals[j + 1])
                    {
                        Bitmap icon = new Bitmap(usingArrayIcons[j]);
                        GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(subPolygon.listWithQuar[i].xCentreOfQuartet,
                            subPolygon.listWithQuar[i].yCentreOfQuartet), icon);
                        marker.ToolTip = new GMapRoundedToolTip(marker);
                        marker.Offset = new Point(-icon.Width / 2, -icon.Height / 2);
                        marker.ToolTipText = nameSelectedCriterion + ": " + 
                            subPolygon.listWithQuar[i].valuesEveryCriterionForQuartet[indexSelectedCriterion].ToString();
                        subPolygon.overlay.Markers.Add(marker);
                    }
                }
            }
        }

        /// <summary>
        /// Очистка раскраски иконками
        /// </summary>
        /// <param name="gmap"></param>
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

        // Массив используемых значков для раскраски
        List<Image> usingArrayIcons = new List<Image>();
        /// <summary>
        /// Получить значки для выбранного количества оттенков
        /// </summary>
        /// <param name="icons">Массив значков</param>
        public void GetIconsForSelectedCountShades(List<Image> icons)
        {
            // Очистить от прошлых изменений
            usingArrayIcons.Clear();

            // 1 оттенок - зеленый
            if (shadesColor == 1)
                usingArrayIcons.Add(icons[9]);
            // 2 оттенка - зеленый и красный
            else if (shadesColor == 2)
            {
                usingArrayIcons.Add(icons[0]);
                usingArrayIcons.Add(icons[9]);
            }
            // 3 оттенка - зеленый желтый красный
            else if (shadesColor == 3)
            {
                usingArrayIcons.Add(icons[0]);
                usingArrayIcons.Add(icons[4]);
                usingArrayIcons.Add(icons[9]);
            }
            else if (shadesColor == 4)
            {
                usingArrayIcons.Add(icons[0]);
                usingArrayIcons.Add(icons[2]);
                usingArrayIcons.Add(icons[4]);
                usingArrayIcons.Add(icons[9]);
            }
            else if (shadesColor == 5)
            {
                usingArrayIcons.Add(icons[0]);
                usingArrayIcons.Add(icons[2]);
                usingArrayIcons.Add(icons[4]);
                usingArrayIcons.Add(icons[6]);
                usingArrayIcons.Add(icons[9]);
            }
            else if (shadesColor == 6)
            {
                usingArrayIcons.Add(icons[0]);
                usingArrayIcons.Add(icons[2]);
                usingArrayIcons.Add(icons[3]);
                usingArrayIcons.Add(icons[4]);
                usingArrayIcons.Add(icons[6]);
                usingArrayIcons.Add(icons[9]);
            }
            else if (shadesColor == 7)
            {
                usingArrayIcons.Add(icons[0]);
                usingArrayIcons.Add(icons[2]);
                usingArrayIcons.Add(icons[3]);
                usingArrayIcons.Add(icons[4]);
                usingArrayIcons.Add(icons[6]);
                usingArrayIcons.Add(icons[7]);
                usingArrayIcons.Add(icons[9]);
            }
            else if (shadesColor == 8)
            {
                usingArrayIcons.Add(icons[0]);
                usingArrayIcons.Add(icons[2]);
                usingArrayIcons.Add(icons[3]);
                usingArrayIcons.Add(icons[4]);
                usingArrayIcons.Add(icons[5]);
                usingArrayIcons.Add(icons[6]);
                usingArrayIcons.Add(icons[7]);
                usingArrayIcons.Add(icons[9]);
            }
            else if (shadesColor == 9)
            {
                usingArrayIcons.Add(icons[0]);
                usingArrayIcons.Add(icons[1]);
                usingArrayIcons.Add(icons[2]);
                usingArrayIcons.Add(icons[3]);
                usingArrayIcons.Add(icons[4]);
                usingArrayIcons.Add(icons[5]);
                usingArrayIcons.Add(icons[6]);
                usingArrayIcons.Add(icons[7]);
                usingArrayIcons.Add(icons[9]);
            }
            else
            {
                usingArrayIcons.Add(icons[0]);
                usingArrayIcons.Add(icons[1]);
                usingArrayIcons.Add(icons[2]);
                usingArrayIcons.Add(icons[3]);
                usingArrayIcons.Add(icons[4]);
                usingArrayIcons.Add(icons[5]);
                usingArrayIcons.Add(icons[6]);
                usingArrayIcons.Add(icons[7]);
                usingArrayIcons.Add(icons[8]);
                usingArrayIcons.Add(icons[9]);
            }
        }
    }
}