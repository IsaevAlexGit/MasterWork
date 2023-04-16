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
        private SublayerQuar _sublayerQuartetIcon = new SublayerQuar();

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="gmap">Карта</param>
        /// <param name="sublayerQuartet">Слой на карте</param>
        public RenderQuar(GMapControl gmap, SublayerQuar sublayerQuartet)
        {
            _sublayerQuartetIcon = sublayerQuartet;
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
            _sublayerQuartetIcon.overlay.Markers.Clear();
            // Убрать слой с карты
            gmap.Overlays.Remove(_sublayerQuartetIcon.overlay);
            // Очистка слоя
            _sublayerQuartetIcon.overlay.Clear();
            // Добавление слоя на карту
            gmap.Overlays.Add(_sublayerQuartetIcon.overlay);
            // Инициализация маркеров
            _InitializationMarkers(_sublayerQuartetIcon, iconsForColoring);
            // Делаем слой видимым
            _sublayerQuartetIcon.overlay.IsVisibile = true;
        }

        /// <summary>
        /// Инициализация маркеров
        /// </summary>
        /// <param name="subQuartet">Слой</param>
        /// <param name="icons">Массив значков</param>
        private void _InitializationMarkers(SublayerQuar subQuartet, List<Image> icons)
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

            //System.Windows.Forms.MessageBox.Show(intervals.Count.ToString());
            //for (int i = 0; i < intervals.Count; i++)
            //    System.Windows.Forms.MessageBox.Show(intervals[i].ToString());

            // Определить в какой диапазон попадает полигон
            for (int i = 0; i < subQuartet.listWithQuar.Count; i++)
            {
                for (int j = 0; j < intervals.Count - 1; j++)
                {
                    if (subQuartet.listWithQuar[i].valuesEveryCriterionForQuartet[indexSelectedCriterion] >= intervals[j] &&
                        subQuartet.listWithQuar[i].valuesEveryCriterionForQuartet[indexSelectedCriterion] <= intervals[j + 1])
                    {
                        Bitmap icon = new Bitmap(usingArrayIcons[j]);
                        GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(subQuartet.listWithQuar[i].xCentreOfQuartet,
                            subQuartet.listWithQuar[i].yCentreOfQuartet), icon);
                        marker.ToolTip = new GMapRoundedToolTip(marker);
                        marker.Offset = new Point(-icon.Width / 2, -icon.Height / 2);
                        marker.ToolTipText = nameSelectedCriterion + ": " + subQuartet.listWithQuar[i].valuesEveryCriterionForQuartet[indexSelectedCriterion].ToString();
                        subQuartet.overlay.Markers.Add(marker);
                    }
                }
            }
        }

        /// <summary>
        /// Очистка раскраски иконками
        /// </summary>
        /// <param name="gmap"></param>
        public void ClearIconQuartet(GMapControl gmap)
        {
            // Очистить у слоя маркеры
            _sublayerQuartetIcon.overlay.Markers.Clear();
            // Убрать слой с карты
            gmap.Overlays.Remove(_sublayerQuartetIcon.overlay);
            // Убрать видимость слоя
            _sublayerQuartetIcon.overlay.IsVisibile = false;
            // Очистка слоя
            _sublayerQuartetIcon.overlay.Clear();
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