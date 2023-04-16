using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Optimum
{
    public partial class CalculateOptimum : Form
    {
        private List<BufferZone> _listBufferZones;
        private MapModel _mapModel;

        /// <summary>
        /// Инициализация формы
        /// </summary>
        /// <param name="mapmodel">Объект MapModel</param>
        /// <param name="buffers">Список буферных зон, которые пользователь разместил на карте</param>
        public CalculateOptimum(MapModel mapmodel, List<BufferZone> buffers)
        {
            _listBufferZones = buffers;
            _mapModel = mapmodel;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;
        }

        // Отрисовка границ groupbox
        private PaintGroupBoxBorder _paintGroupBoxBorder = new PaintGroupBoxBorder();

        /// <summary>
        /// Загрузка формы
        /// </summary>
        private void CalculateOptimum_Load(object sender, EventArgs e)
        {
            // Установка весов по умолчанию
            //textBoxWeightResidents.Text = _mapModel.weightResidents.ToString();
            //textBoxWeightRetired.Text = _mapModel.weightRetired.ToString();
            //textBoxWeightPharmacy.Text = _mapModel.weightPharmacy.ToString();

            ClientSize = new Size(950, 788);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            groupBoxCoordinatesOptimum.Visible = false;

            foreach (Control groupbox in Controls)
            {
                GroupBox everyGroupBox = groupbox as GroupBox;
                if (everyGroupBox != null)
                    everyGroupBox.Paint += _paintGroupBoxBorder.groupBox_Paint;
            }

            int indexRowTable = 0;
            // Заполнение таблицы данными о буферных зонах
            for (int i = 0; i < _listBufferZones.Count; i++)
            {
                // Добавление строки
                indexRowTable = dataGridView.Rows.Add();
                dataGridView.Rows[indexRowTable].Cells[0].Value = _listBufferZones[i].x;
                dataGridView.Rows[indexRowTable].Cells[1].Value = _listBufferZones[i].y;
                dataGridView.Rows[indexRowTable].Cells[2].Value = _listBufferZones[i].lengthRadiusSearch;
                //dataGridView.Rows[indexRowTable].Cells[3].Value = _listBufferZones[i].countOfPharmacy;
                //dataGridView.Rows[indexRowTable].Cells[4].Value = _listBufferZones[i].countOfResidents;
                //dataGridView.Rows[indexRowTable].Cells[5].Value = _listBufferZones[i].countOfRetired;
            }
            // Выравнивание заголовков таблицы по центру
            dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // Удаление выделения первой строки по умолчанию
            dataGridView.FirstDisplayedCell.Selected = false;
            // Запрет на растягивание высоты заголовков столбцов
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        }

        private bool _flagConfirmationWeight = false;
        /// <summary>
        /// Загрузка весов для критериев
        /// </summary>
        private void LoadPriorityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если все поля введены
            if (!string.IsNullOrEmpty(textBoxWeightResidents.Text) && !string.IsNullOrWhiteSpace(textBoxWeightResidents.Text) &&
                !string.IsNullOrEmpty(textBoxWeightRetired.Text) && !string.IsNullOrWhiteSpace(textBoxWeightRetired.Text) &&
                !string.IsNullOrEmpty(textBoxWeightPharmacy.Text) && !string.IsNullOrWhiteSpace(textBoxWeightPharmacy.Text))
            {
                double tempValue;
                bool testForResidents = double.TryParse(textBoxWeightResidents.Text, out tempValue);
                bool testForRetired = double.TryParse(textBoxWeightRetired.Text, out tempValue);
                bool testForPharmacy = double.TryParse(textBoxWeightPharmacy.Text, out tempValue);

                // Если во всех полях дробные числа
                if (testForResidents == true && testForRetired == true && testForPharmacy == true)
                {
                    double weihgtResidents = Convert.ToDouble(textBoxWeightResidents.Text);
                    double weihgtRetired = Convert.ToDouble(textBoxWeightRetired.Text);
                    double weihgtPharma = Convert.ToDouble(textBoxWeightPharmacy.Text);

                    // Если все веса в интервале [0,1]
                    if (weihgtResidents >= 0 && weihgtResidents <= 1
                        && weihgtRetired >= 0 && weihgtRetired <= 1
                        && weihgtPharma >= 0 && weihgtPharma <= 1)
                    {
                        // Если сумма весов равна 1
                        if (weihgtResidents + weihgtRetired + weihgtPharma == 1)
                        {
                            //_mapModel.weightResidents = weihgtResidents;
                            //_mapModel.weightRetired = weihgtRetired;
                            //_mapModel.weightPharmacy = weihgtPharma;
                            _flagConfirmationWeight = true;
                            MessageBox.Show("Веса заданы верно и сохранены", "Уведомление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Если groupBox уже был показан
                            groupBoxCoordinatesOptimum.Visible = false;
                            // Все строки в таблице залить изначальным цветом
                            for (int i = 0; i < dataGridView.RowCount; i++)
                                dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.PeachPuff;
                        }
                        else
                            MessageBox.Show("Сумма весов должна быть равна 1", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        MessageBox.Show("Все значения должны быть в пределах от 0 до 1", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("В поля должны быть введены только числа", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Все поля в блоке \"Установка приоритетов\" должны быть заполнены", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private bool _isOptimumFound = false;
        private List<OptimalZone> _optimalPoints;
        private MathOptimumModel _math;
        /// <summary>
        /// Поиск оптимальной точки
        /// </summary>
        private void SearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Получить список буферных зон
            _mapModel.CreateListBufferZones();
            // Список буферных зон для поиска оптимума
            _listBufferZones = _mapModel.listPointsBufferZone;

            // Если веса заданы
            if (_flagConfirmationWeight == true)
            {
                // Поиск оптимальной точки с заданными буферными зонами и весами важности
                //_math = new MathOptimumModel(_listBufferZones, _mapModel.listUserCriterion);
                //_optimalPoints = _math.GetOptimum();
                //_isOptimumFound = true;

                //// Вывести координаты в groupBox
                //labelX.Text = _optimalPoint.x.ToString();
                //labelY.Text = _optimalPoint.y.ToString();
                //groupBoxCoordinatesOptimum.Visible = true;

                //// Выделение строки зеленым цветом с оптимальной точкой
                //for (int i = 0; i < dataGridView.RowCount; i++)
                //{
                //    dataGridView.Rows[i].Selected = false;
                //    // Если в таблице координаты в строке соответствуют координатам оптимальной точки
                //    if (dataGridView.Rows[i].Cells[0].Value.ToString().Contains(_optimalPoint.x.ToString())
                //        && dataGridView.Rows[i].Cells[1].Value.ToString().Contains(_optimalPoint.y.ToString()))
                //    {
                //        // Выделить строку зеленым цветом
                //        dataGridView.Rows[i].DefaultCellStyle.BackColor = Color.LightGreen;
                //        // Если все точки стоят в одном месте, то break выделит только первую строку в таблице
                //        // Без break, все строки в таблице выделятся
                //        break;
                //    }
                //}
            }
            else
                MessageBox.Show("Задайте веса в блоке \"Установка приоритетов\"", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private bool _flagSaveOptimumPoint = false;
        /// <summary>
        /// Сохранение оптимальной точки и возврат на главное окно
        /// </summary>
        private void SavePointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Если пользователь нашел оптимальную точку
            if (_isOptimumFound == true)
            {
                _mapModel._optimalZones = _optimalPoints;
                _flagSaveOptimumPoint = true;
                Close();
            }
            else
                MessageBox.Show("Найдите оптимальную точку", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Закрытие формы
        /// </summary>
        private void CalculateOptimum_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Если пользователь сохранил точку
            if (_flagSaveOptimumPoint == true)
                e.Cancel = false;
            else
            {
                if (MessageBox.Show("Вы не сохранили оптимальную точку. Выйти?", "Предупреждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    e.Cancel = false;
                else
                    e.Cancel = true;
            }
        }

        /// <summary>
        /// Закрыть форму через ESC
        /// </summary>
        private void CalculateOptimum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}