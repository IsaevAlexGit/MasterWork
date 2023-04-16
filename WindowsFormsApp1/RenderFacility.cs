using System;
using System.Drawing;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;

namespace Optimum
{
    // Класс Отрисовщик
    public class RenderFacility
    {
        // Слой с объектами инфраструктуры
        private SublayerFacility _sublayerFacility = new SublayerFacility();

        // Режим выводящейся информации об объекте инфраструктуры
        // 1 - Название объекта инфраструктуры
        // 2 - Вся информация
        private int _modeInfoFacility = 1;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="gmap">Карта</param>
        /// <param name="sublayerFacility">Слой для объектов инфраструктуры</param>
        public RenderFacility(GMapControl gmap, SublayerFacility sublayerFacility)
        {
            _sublayerFacility = sublayerFacility;
        }

        /// <summary>
        /// Установка режима выводящейся информации об объекте инфраструктуры
        /// </summary>
        /// <param name="mode">Режим</param>
        public void SetMode(int mode)
        {
            _modeInfoFacility = mode;
        }

        /// <summary>
        /// Отображение всех объектов инфраструктуры
        /// </summary>
        /// <param name="gmap">Карта</param>
        public void DrawFacility(GMapControl gmap)
        {
            // Очистить у слоя маркеры
            _sublayerFacility.overlay.Markers.Clear();
            // Убрать слой с карты
            gmap.Overlays.Remove(_sublayerFacility.overlay);
            // Очистка слоя
            _sublayerFacility.overlay.Clear();
            // Добавление слоя на карту
            gmap.Overlays.Add(_sublayerFacility.overlay);
            // Инициализация маркеров
            _InitializationMarkersFacility(_sublayerFacility);
            // Делаем слой видимым
            _sublayerFacility.overlay.IsVisibile = true;
        }

        /// <summary>
        /// Инициализация маркеров объектов инфраструктуры
        /// </summary>
        /// <param name="sub">Слой объектов инфраструктуры</param>
        private void _InitializationMarkersFacility(SublayerFacility sub)
        {
            for (int i = 0; i < sub.listWithFacility.Count; i++)
            {
                Bitmap icon = new Bitmap(sub.iconOfOverlay);
                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(sub.listWithFacility[i].x, sub.listWithFacility[i].y), icon);
                marker.ToolTip = new GMapRoundedToolTip(marker);
                marker.Offset = new Point(-icon.Width / 2, -icon.Height / 2);
                marker.ToolTipText = _ChangeInfoTextFacility(_modeInfoFacility, sub.listWithFacility[i]);
                sub.overlay.Markers.Add(marker);
            }
        }

        /// <summary>
        /// Изменение выводимой информации об объекте инфраструктуры
        /// </summary>
        /// <param name="mode">Режим выводимой информации</param>
        /// <param name="point">Точка объекта инфраструктуры на карте</param>
        /// <returns>Выводящаяся информация об объекте инфраструктуры</returns>
        private string _ChangeInfoTextFacility(int mode, Facility point)
        {
            // Конечная подпись объекта
            string TextInfo = "";

            // Если 1 - Название объекта инфраструктуры
            if (mode == 1)
                TextInfo = _sublayerFacility.nameOfOverlay;
            // Иначе вся информация об объекте инфраструктуры
            else
            {
                for (int i = 0; i < point.infoAboutFacility.Count; i++)
                    // Вывести все данные с новой строки
                    TextInfo = TextInfo + "Поле №" + (i + 1) + ": " + point.infoAboutFacility[i] + Environment.NewLine;
            }
            return TextInfo;
        }

        /// <summary>
        /// Очистка отображения всех объектов инфраструктуры
        /// </summary>
        /// <param name="gmap">Карта</param>
        public void ClearFacility(GMapControl gmap)
        {
            // Очистить у слоя маркеры, если они были проинициализированы ранее
            _sublayerFacility.overlay.Markers.Clear();
            // Убираем слой с карты
            gmap.Overlays.Remove(_sublayerFacility.overlay);
            // Убираем видимость маркеров
            _sublayerFacility.overlay.IsVisibile = false;
            // Очищаем слой
            _sublayerFacility.overlay.Clear();
        }
    }
}