using System;
using System.Drawing;
using System.Windows.Forms;
using System.Device.Location;

namespace Optimum
{
    //! Класс для окна с установкой радиуса буферной зоны
    public partial class SettingRadius : Form
    {
        //! Объект MapModel
        private MapModel _mapModel;

        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор формы
        /// </summary>
        /// <param name="mapModel">Объект MapModel</param>
        /// <permission cref="">Доступ к функции: public</permission>
        public SettingRadius(MapModel mapModel)
        {
            _mapModel = mapModel;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;
        }

        //! Шаг ползунка
        private const int STEP_TRACK_BAR = 50;

        /*!
        \version 1.0
        */
        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void SettingRadius_Load(object sender, EventArgs e)
        {
            // Цвет формы, меню, ползунка
            BackColor = labelRadiusLong.BackColor = labelRadius.BackColor = _mapModel.mainColor;
            menuStrip.BackColor = trackBarRadius.BackColor = _mapModel.secondaryColor;

            ClientSize = new Size(696, 215);
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // Ползунок от 6 до Х, текущее значение 6
            // Мы передаем в радиус значение с ползунка помноженное на STEP_TRACK_BAR
            // То есть 6 это STEP_TRACK_BAR*6 метров
            trackBarRadius.Minimum = 6;
            // Максимальный радиус зависит от рассматриваемой площади
            trackBarRadius.Maximum = _FindRadius() / STEP_TRACK_BAR;
            // При входе отображать значение на ползунке текущего радиуса
            if (_mapModel.radiusBufferZone / STEP_TRACK_BAR > trackBarRadius.Maximum)
                trackBarRadius.Value = 6;
            else
                trackBarRadius.Value = _mapModel.radiusBufferZone / STEP_TRACK_BAR;
            labelRadiusLong.Text = (trackBarRadius.Value * STEP_TRACK_BAR).ToString() + " м.";

            // Настройка ползунка
            trackBarRadius.Cursor = Cursors.Hand;
            trackBarRadius.Orientation = Orientation.Horizontal;
            trackBarRadius.TickStyle = TickStyle.Both;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Найти динамиечески максимальную длину для радиуса
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        /// <returns>Радиус в метрах</returns>
        private int _FindRadius()
        {
            // Список крайних точек зоны анализа
            SublayerLocation listLocationBorderTerritory = _mapModel.GetSublayerBorderPointsTerritory();

            // Минимимум и максимум (х,у) для начала первая точка из списка
            double minX = listLocationBorderTerritory.listWithLocation[0].x;
            double minY = listLocationBorderTerritory.listWithLocation[0].y;

            double maxX = listLocationBorderTerritory.listWithLocation[0].x;
            double maxY = listLocationBorderTerritory.listWithLocation[0].y;

            // Поиск левой верхней и правой нижней точки зоны анализа
            for (int i = 0; i < listLocationBorderTerritory.listWithLocation.Count; i++)
            {
                if (minX > listLocationBorderTerritory.listWithLocation[i].x)
                    minX = listLocationBorderTerritory.listWithLocation[i].x;
                if (minY > listLocationBorderTerritory.listWithLocation[i].y)
                    minY = listLocationBorderTerritory.listWithLocation[i].y;

                if (maxX < listLocationBorderTerritory.listWithLocation[i].x)
                    maxX = listLocationBorderTerritory.listWithLocation[i].x;
                if (maxY < listLocationBorderTerritory.listWithLocation[i].y)
                    maxY = listLocationBorderTerritory.listWithLocation[i].y;
            }

            // Левая нижняя точка
            GeoCoordinate leftPoint = new GeoCoordinate(minX, minY);
            // Правая верхняя точка
            GeoCoordinate rightPoint = new GeoCoordinate(maxX, maxY);
            int radius = Convert.ToInt32(leftPoint.GetDistanceTo(rightPoint) / 5);
            // Радиус - это 1/5 от диагонали загруженной зоны анализа
            return radius;
        }

        //! Флаг сохранения радиуса
        private bool _flagSaveRadius = true;

        /*!
        \version 1.0
        */
        /// <summary>
        /// Установка радиуса буферной зоны и возврат на главное окно
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void InputRadiusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Сохранить текущее положение ползунка при выходе
            int radiusZone = trackBarRadius.Value * STEP_TRACK_BAR;
            _mapModel.radiusBufferZone = radiusZone;
            _flagSaveRadius = true;
            Close();
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Прокрутка ползунка
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void trackBarRadius_Scroll(object sender, EventArgs e)
        {
            // Если хоть раз сдвинули ползунок, значит флаг сохранения радиуса изменяется
            _flagSaveRadius = false;
            // Переписать текст в надписи на текущее значение радиуса в метрах
            labelRadiusLong.Text = (trackBarRadius.Value * STEP_TRACK_BAR).ToString() + " м.";
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Закрытие формы
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void SettingRadius_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Если пользователь сохранил радиус
            if (_flagSaveRadius == true)
                e.Cancel = false;
            else
            {
                if (MessageBox.Show("Вы не сохранили радиус поиска. Выйти?", "Предупреждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    e.Cancel = false;
                else
                    e.Cancel = true;
            }
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Закрыть форму через ESC
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        private void SettingRadius_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}