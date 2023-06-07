using System;
using System.Drawing;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using GMap.NET.WindowsForms.ToolTips;

namespace Optimum
{
    // Класс "Отрисовщик объектов социальной инфраструктуры"
    public class RenderFacility
    {
        // Слой с объектами социальными инфраструктуры
        private SublayerFacility _sublayerFacility = new SublayerFacility();

        // Режим выводящейся информации об объекте социальной инфраструктуры
        // 1 - Название объекта социальной инфраструктуры
        // 2 - Вся информация об объекте социальной инфраструктуры
        private int _modeHoveTextFacility = 1;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="sublayerFacility">Слой для объектов социальной инфраструктуры</param>
        public RenderFacility(SublayerFacility sublayerFacility)
        {
            _sublayerFacility = sublayerFacility;
        }

        /// <summary>
        /// Установка режима выводящейся информации об объекте социальной инфраструктуры
        /// </summary>
        /// <param name="mode">Выбранный режим</param>
        public void SetMode(int mode)
        {
            _modeHoveTextFacility = mode;
        }

        /// <summary>
        /// Отображение всех объектов социальной инфраструктуры
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
        /// Инициализация маркеров объектов социальной инфраструктуры
        /// </summary>
        /// <param name="sublayerFacility">Слой объектов социальной инфраструктуры</param>
        private void _InitializationMarkersFacility(SublayerFacility sublayerFacility)
        {
            // Проход по списку точек и установка маркеров на карте
            for (int i = 0; i < sublayerFacility.listWithFacility.Count; i++)
            {
                Bitmap icon = new Bitmap(sublayerFacility.iconOfOverlay);
                GMarkerGoogle marker = new GMarkerGoogle(new PointLatLng(sublayerFacility.listWithFacility[i].x,
                    sublayerFacility.listWithFacility[i].y), icon);
                marker.ToolTip = new GMapRoundedToolTip(marker);
                marker.Offset = new Point(-icon.Width / 2, -icon.Height / 2);
                marker.ToolTipText = _ChangeInfoTextFacility(_modeHoveTextFacility, sublayerFacility.listWithFacility[i]);
                sublayerFacility.overlay.Markers.Add(marker);
            }
        }

        /// <summary>
        /// Изменение выводимой информации об объекте социальной инфраструктуры
        /// </summary>
        /// <param name="mode">Режим выводимой информации</param>
        /// <param name="point">Точка объекта социальной инфраструктуры на карте</param>
        /// <returns>Выводящаяся информация об объекте социальной инфраструктуры</returns>
        private string _ChangeInfoTextFacility(int mode, Facility point)
        {
            // Конечная подпись объекта
            string textHoverInfoFaciliry = "";

            // Если 1 - Название объекта социальной инфраструктуры
            if (mode == 1)
                textHoverInfoFaciliry = _sublayerFacility.nameOfOverlay;
            // Иначе вся информация об объекте социальной инфраструктуры, которая была в файле
            else
            {
                if (point.infoAboutFacility.Count != 0)
                {
                    for (int i = 0; i < point.infoAboutFacility.Count; i++)
                        // Вывести все данные с новой строки
                        if (point.infoAboutFacility[i] != "")
                            textHoverInfoFaciliry = textHoverInfoFaciliry + "Поле №" + (i + 1) + ": " + point.infoAboutFacility[i] + Environment.NewLine;
                        else
                            textHoverInfoFaciliry = textHoverInfoFaciliry + "Поле №" + (i + 1) + ": отсутствует" + Environment.NewLine;
                }
                else
                    textHoverInfoFaciliry = "Данные отсутствуют";
            }
            return textHoverInfoFaciliry;
        }

        /// <summary>
        /// Скрыть все объектов социальной инфраструктуры
        /// </summary>
        /// <param name="gmap">Карта</param>
        public void ClearFacility(GMapControl gmap)
        {
            // Очистить у слоя маркеры
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