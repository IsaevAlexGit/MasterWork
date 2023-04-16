using System;
using System.Drawing;
using System.Windows.Forms;
using System.Device.Location;

namespace Optimum
{
    public partial class SettingRadius : Form
    {
        private MapModel _mapModel;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        /// <param name="mapModel">Объект MapModel</param>
        public SettingRadius(MapModel mapModel)
        {
            _mapModel = mapModel;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        private void SettingRadius_Load(object sender, EventArgs e)
        {
            // Цвет формы, меню, ползунка
            BackColor = labelRadiusLong.BackColor = labelRadius.BackColor = _mapModel.mainColor;
            menuStrip.BackColor = _mapModel.secondaryColor;
            trackBarRadius.BackColor = _mapModel.secondaryColor;

            ClientSize = new Size(696, 215);
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // Ползунок от 6 до Х, текущее значение 6
            // Мы передаем в радиус значение с ползунка помноженное на 50
            // То есть 6 это 300 метров
            trackBarRadius.Minimum = 6;
            // Максимальный радиус зависит от рассматриваемой площади
            trackBarRadius.Maximum = FindRadius() / 50;
            // При входе отображать значение на ползунке текущего радиуса
            if (_mapModel.radiusBufferZone / 50 > trackBarRadius.Maximum)
                trackBarRadius.Value = 6;
            else
                trackBarRadius.Value = _mapModel.radiusBufferZone / 50;
            labelRadiusLong.Text = (trackBarRadius.Value * 50).ToString() + " м.";

            trackBarRadius.Cursor = Cursors.Hand;
            trackBarRadius.Orientation = Orientation.Horizontal;
            trackBarRadius.TickStyle = TickStyle.Both;
        }

        /// <summary>
        /// Найти динамиечески максимальную длину для радиуса
        /// </summary>
        /// <returns>Радиус в метрах</returns>
        private int FindRadius()
        {
            // Список крайних точек территории
            SublayerLocation listLocationBorderTerritory = _mapModel.GetSublayerBorderPointsTerritory();

            // Минимимум и максимум (х,у) для начала первая точка из списка
            double minX = listLocationBorderTerritory.listWithLocation[0].x;
            double minY = listLocationBorderTerritory.listWithLocation[0].y;

            double maxX = listLocationBorderTerritory.listWithLocation[0].x;
            double maxY = listLocationBorderTerritory.listWithLocation[0].y;

            // Поиск левой верхней и правой нижней точки
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

            GeoCoordinate leftPoint = new GeoCoordinate(minX, minY);
            GeoCoordinate rightPoint = new GeoCoordinate(maxX, maxY);
            int radius = Convert.ToInt32(leftPoint.GetDistanceTo(rightPoint) / 5);
            // Радиус - это 1/5 от диагонали загруженной территории
            return radius;
        }

        private bool _flagSaveRadius = true;
        /// <summary>
        /// Установка радиуса буферной зоны и возврат на главное окно
        /// </summary>
        private void InputRadiusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Сохранить радиус
            int radiusZone = trackBarRadius.Value * 50;
            _mapModel.radiusBufferZone = radiusZone;
            _flagSaveRadius = true;
            Close();
        }

        /// <summary>
        /// Прокрутка ползунка
        /// </summary>
        private void trackBarRadius_Scroll(object sender, EventArgs e)
        {
            // Если хоть раз сдвинули ползунок, значит уже изменения внеслись
            _flagSaveRadius = false;
            // Переписать текст в надписи на текущее значение радиуса в метрах
            labelRadiusLong.Text = (trackBarRadius.Value * 50).ToString() + " м.";
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
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

        /// <summary>
        /// Закрыть форму через ESC
        /// </summary>
        private void SettingRadius_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}