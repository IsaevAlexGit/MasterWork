using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Collections.Generic;

namespace Optimum
{
    public partial class Settings : Form
    {
        // Работа с файлом
        private FileValidator _fileValidator;
        // Отрисовка границ groupbox
        private PaintGroupBoxBorder _paintGroupBoxBorder = new PaintGroupBoxBorder();
        // Настройка для того, чтобы программа работала на английской версии Windows
        private readonly CultureInfo _cultureInfo = new CultureInfo("ru-RU");
        // Название объекта социальной инфраструктуры
        private string _nameFacility;
        // Список граничных точек зоны анализа
        private List<Location> _listPointsBorderTerritory;
        // Список объектов социальной инфраструктуры
        private List<Facility> _listFacilitiesFromFile;
        // Список полигонов
        private List<Polygon> _listPolygonsFromFile;
        // Список критериев оптимальности
        private List<Criterion> _listCriteriaForSearch;
        // Дубликаты списка критериев для их модификации
        private List<Criterion> _criterion;
        private List<Criterion> _saveCriterion;
        // Точка центрирования карты
        private Location _locationCenterMap = new Location();
        // Путь к значку объекта социальной инфраструктуры
        private string _pathToImageFacility;
        private MapModel _mapModel;

        /// <summary>
        /// Создание формы
        /// </summary>
        public Settings(MapModel mapmodel)
        {
            _mapModel = mapmodel;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;
        }

        // Флаг отрисовки критериев
        private bool _flagDrawComboBoxes = false;
        /// <summary>
        /// Загрузка формы
        /// </summary>
        private void Settings_Load(object sender, EventArgs e)
        {
            _pathToImageFacility = _mapModel.pathToIconObjectFacility;
            // Установить значок объекта социальной инфраструктуры, загруженный ранее
            pictureIconFacility.Image = _mapModel.iconFacility;
            // Отобразить название объекта социальной инфраструктуры, введенное ранее
            textBoxNameFacility.Text = _mapModel.nameObjectFacility;
            // Сохранить списки объектов социальной инфраструктуры, полигонов, зона анализа, критериев
            _listPointsBorderTerritory = _mapModel.listUserPointsBorderTerritory;
            _listFacilitiesFromFile = _mapModel.listUserFacilities;
            _listPolygonsFromFile = _mapModel.listUserPolygons;

            // Списки с критериями
            _listCriteriaForSearch = new List<Criterion>();
            for (int i = 0; i < _mapModel.listUserCriterion.Count; i++)
            {
                Criterion zone = (Criterion)_mapModel.listUserCriterion[i].Clone();
                _listCriteriaForSearch.Add(zone);
            }

            _criterion = new List<Criterion>();
            for (int i = 0; i < _mapModel.listUserCriterion.Count; i++)
            {
                Criterion zone = (Criterion)_mapModel.listUserCriterion[i].Clone();
                _criterion.Add(zone);
            }

            _saveCriterion = new List<Criterion>();
            for (int i = 0; i < _mapModel.listUserCriterion.Count; i++)
            {
                Criterion zone = (Criterion)_mapModel.listUserCriterion[i].Clone();
                _saveCriterion.Add(zone);
            }

            _pathToFilePolygons = _mapModel.pathToFilePolygonUser;

            // Валидация загружаемых файлов
            _fileValidator = new FileValidator();

            // Основной цвет интерфейса
            groupBoxCriterion.BackColor = groupBoxIconFacility.BackColor = groupBoxLoadFiles.BackColor =
                groupBoxNameFacility.BackColor = BackColor = comboCountCriterion.BackColor = _mapModel.mainColor;

            // Второстепенный цвет интерфейса
            comboBox2.BackColor = comboBox3.BackColor = comboBox4.BackColor = comboBox5.BackColor = comboBox6.BackColor =
                comboBox7.BackColor = comboBox8.BackColor = _mapModel.secondaryColor;
            buttonValidateAndSave.BackColor = buttonLoadIconFacility.BackColor = buttonLoadBorderTerritory.BackColor =
                buttonLoadPolygons.BackColor = buttonLoadFacilities.BackColor = _mapModel.secondaryColor;

            // Отрисовка границ groupbox
            foreach (Control groupbox in Controls)
            {
                GroupBox everyGroupBox = groupbox as GroupBox;
                if (everyGroupBox != null)
                    everyGroupBox.Paint += _paintGroupBoxBorder.groupBox_Paint;
            }

            ClientSize = new Size(848, 638);
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // Настройка кнопки загрузки значка
            buttonLoadIconFacility.FlatAppearance.BorderSize = 0;
            buttonLoadIconFacility.FlatStyle = FlatStyle.Flat;
            buttonLoadIconFacility.TextAlign = ContentAlignment.MiddleCenter;

            // Настройка выпадающего списка
            comboCountCriterion.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCountCriterion.SelectedItem = _mapModel.listUserCriterion.ToString();

            // Настройка выпадающих списков для минимизации/максимизации
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox7.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox8.DropDownStyle = ComboBoxStyle.DropDownList;

            // Настройка кнопок для загрузки файлов
            buttonLoadPolygons.FlatAppearance.BorderSize = 0;
            buttonLoadPolygons.FlatStyle = FlatStyle.Flat;
            buttonLoadBorderTerritory.FlatAppearance.BorderSize = 0;
            buttonLoadBorderTerritory.FlatStyle = FlatStyle.Flat;
            buttonLoadFacilities.FlatAppearance.BorderSize = 0;
            buttonLoadFacilities.FlatStyle = FlatStyle.Flat;

            // Настройка кнопки "Сохранить"
            buttonValidateAndSave.FlatAppearance.BorderSize = 0;
            buttonValidateAndSave.FlatStyle = FlatStyle.Flat;

            // Заполнить критерии данными, загруженными ранее
            _SetComboBoxesCriterion();
        }

        /// <summary>
        /// Заполнить критерии данными, загруженными ранее
        /// </summary>
        private void _SetComboBoxesCriterion()
        {
            // Заполнить критерии
            comboCountCriterion.Text = _listCriteriaForSearch.Count.ToString();
            int countCriterionOnComboBox = Convert.ToInt32(comboCountCriterion.Text);

            // Сколько было ранее задано критериев, столько и отобразить строк с критериями
            if (countCriterionOnComboBox == 1)
            {
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox1.Text = _listCriteriaForSearch[0].nameOfCriterion;
                comboBox2.Text = _listCriteriaForSearch[0].directionOfCriterion == false ? "min" : "max";
                textBox4.Text = _listCriteriaForSearch[0].weightOfCriterion.ToString();

                textBox2.Visible = comboBox3.Visible = textBox5.Visible = false;
                textBox3.Visible = comboBox4.Visible = textBox6.Visible = false;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = false;
                textBox10.Visible = comboBox6.Visible = textBox9.Visible = false;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = false;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 144);
            }
            else if (countCriterionOnComboBox == 2)
            {
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox1.Text = _listCriteriaForSearch[0].nameOfCriterion;
                comboBox2.Text = _listCriteriaForSearch[0].directionOfCriterion == false ? "min" : "max";
                textBox4.Text = _listCriteriaForSearch[0].weightOfCriterion.ToString();

                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox2.Text = _listCriteriaForSearch[1].nameOfCriterion;
                comboBox3.Text = _listCriteriaForSearch[1].directionOfCriterion == false ? "min" : "max";
                textBox5.Text = _listCriteriaForSearch[1].weightOfCriterion.ToString();

                textBox3.Visible = comboBox4.Visible = textBox6.Visible = false;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = false;
                textBox10.Visible = comboBox6.Visible = textBox9.Visible = false;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = false;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 179);
            }
            else if (countCriterionOnComboBox == 3)
            {
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox1.Text = _listCriteriaForSearch[0].nameOfCriterion;
                comboBox2.Text = _listCriteriaForSearch[0].directionOfCriterion == false ? "min" : "max";
                textBox4.Text = _listCriteriaForSearch[0].weightOfCriterion.ToString();

                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox2.Text = _listCriteriaForSearch[1].nameOfCriterion;
                comboBox3.Text = _listCriteriaForSearch[1].directionOfCriterion == false ? "min" : "max";
                textBox5.Text = _listCriteriaForSearch[1].weightOfCriterion.ToString();

                textBox3.Visible = comboBox4.Visible = textBox6.Visible = true;
                textBox3.Text = _listCriteriaForSearch[2].nameOfCriterion;
                comboBox4.Text = _listCriteriaForSearch[2].directionOfCriterion == false ? "min" : "max";
                textBox6.Text = _listCriteriaForSearch[2].weightOfCriterion.ToString();

                textBox7.Visible = comboBox5.Visible = textBox11.Visible = false;
                textBox10.Visible = comboBox6.Visible = textBox9.Visible = false;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = false;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 214);
            }
            else if (countCriterionOnComboBox == 4)
            {
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox1.Text = _listCriteriaForSearch[0].nameOfCriterion;
                comboBox2.Text = _listCriteriaForSearch[0].directionOfCriterion == false ? "min" : "max";
                textBox4.Text = _listCriteriaForSearch[0].weightOfCriterion.ToString();

                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox2.Text = _listCriteriaForSearch[1].nameOfCriterion;
                comboBox3.Text = _listCriteriaForSearch[1].directionOfCriterion == false ? "min" : "max";
                textBox5.Text = _listCriteriaForSearch[1].weightOfCriterion.ToString();

                textBox3.Visible = comboBox4.Visible = textBox6.Visible = true;
                textBox3.Text = _listCriteriaForSearch[2].nameOfCriterion;
                comboBox4.Text = _listCriteriaForSearch[2].directionOfCriterion == false ? "min" : "max";
                textBox6.Text = _listCriteriaForSearch[2].weightOfCriterion.ToString();

                textBox7.Visible = comboBox5.Visible = textBox11.Visible = true;
                textBox7.Text = _listCriteriaForSearch[3].nameOfCriterion;
                comboBox5.Text = _listCriteriaForSearch[3].directionOfCriterion == false ? "min" : "max";
                textBox11.Text = _listCriteriaForSearch[3].weightOfCriterion.ToString();

                textBox10.Visible = comboBox6.Visible = textBox9.Visible = false;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = false;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 254);
            }
            else if (countCriterionOnComboBox == 5)
            {
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox1.Text = _listCriteriaForSearch[0].nameOfCriterion;
                comboBox2.Text = _listCriteriaForSearch[0].directionOfCriterion == false ? "min" : "max";
                textBox4.Text = _listCriteriaForSearch[0].weightOfCriterion.ToString();

                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox2.Text = _listCriteriaForSearch[1].nameOfCriterion;
                comboBox3.Text = _listCriteriaForSearch[1].directionOfCriterion == false ? "min" : "max";
                textBox5.Text = _listCriteriaForSearch[1].weightOfCriterion.ToString();

                textBox3.Visible = comboBox4.Visible = textBox6.Visible = true;
                textBox3.Text = _listCriteriaForSearch[2].nameOfCriterion;
                comboBox4.Text = _listCriteriaForSearch[2].directionOfCriterion == false ? "min" : "max";
                textBox6.Text = _listCriteriaForSearch[2].weightOfCriterion.ToString();

                textBox7.Visible = comboBox5.Visible = textBox11.Visible = true;
                textBox7.Text = _listCriteriaForSearch[3].nameOfCriterion;
                comboBox5.Text = _listCriteriaForSearch[3].directionOfCriterion == false ? "min" : "max";
                textBox11.Text = _listCriteriaForSearch[3].weightOfCriterion.ToString();

                textBox10.Visible = comboBox6.Visible = textBox9.Visible = true;
                textBox10.Text = _listCriteriaForSearch[4].nameOfCriterion;
                comboBox6.Text = _listCriteriaForSearch[4].directionOfCriterion == false ? "min" : "max";
                textBox9.Text = _listCriteriaForSearch[4].weightOfCriterion.ToString();

                textBox12.Visible = comboBox7.Visible = textBox8.Visible = false;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 287);
            }
            else if (countCriterionOnComboBox == 6)
            {
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox1.Text = _listCriteriaForSearch[0].nameOfCriterion;
                comboBox2.Text = _listCriteriaForSearch[0].directionOfCriterion == false ? "min" : "max";
                textBox4.Text = _listCriteriaForSearch[0].weightOfCriterion.ToString();

                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox2.Text = _listCriteriaForSearch[1].nameOfCriterion;
                comboBox3.Text = _listCriteriaForSearch[1].directionOfCriterion == false ? "min" : "max";
                textBox5.Text = _listCriteriaForSearch[1].weightOfCriterion.ToString();

                textBox3.Visible = comboBox4.Visible = textBox6.Visible = true;
                textBox3.Text = _listCriteriaForSearch[2].nameOfCriterion;
                comboBox4.Text = _listCriteriaForSearch[2].directionOfCriterion == false ? "min" : "max";
                textBox6.Text = _listCriteriaForSearch[2].weightOfCriterion.ToString();

                textBox7.Visible = comboBox5.Visible = textBox11.Visible = true;
                textBox7.Text = _listCriteriaForSearch[3].nameOfCriterion;
                comboBox5.Text = _listCriteriaForSearch[3].directionOfCriterion == false ? "min" : "max";
                textBox11.Text = _listCriteriaForSearch[3].weightOfCriterion.ToString();

                textBox10.Visible = comboBox6.Visible = textBox9.Visible = true;
                textBox10.Text = _listCriteriaForSearch[4].nameOfCriterion;
                comboBox6.Text = _listCriteriaForSearch[4].directionOfCriterion == false ? "min" : "max";
                textBox9.Text = _listCriteriaForSearch[4].weightOfCriterion.ToString();

                textBox12.Visible = comboBox7.Visible = textBox8.Visible = true;
                textBox12.Text = _listCriteriaForSearch[5].nameOfCriterion;
                comboBox7.Text = _listCriteriaForSearch[5].directionOfCriterion == false ? "min" : "max";
                textBox8.Text = _listCriteriaForSearch[5].weightOfCriterion.ToString();

                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 323);
            }
            else
            {
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox1.Text = _listCriteriaForSearch[0].nameOfCriterion;
                comboBox2.Text = _listCriteriaForSearch[0].directionOfCriterion == false ? "min" : "max";
                textBox4.Text = _listCriteriaForSearch[0].weightOfCriterion.ToString();

                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox2.Text = _listCriteriaForSearch[1].nameOfCriterion;
                comboBox3.Text = _listCriteriaForSearch[1].directionOfCriterion == false ? "min" : "max";
                textBox5.Text = _listCriteriaForSearch[1].weightOfCriterion.ToString();

                textBox3.Visible = comboBox4.Visible = textBox6.Visible = true;
                textBox3.Text = _listCriteriaForSearch[2].nameOfCriterion;
                comboBox4.Text = _listCriteriaForSearch[2].directionOfCriterion == false ? "min" : "max";
                textBox6.Text = _listCriteriaForSearch[2].weightOfCriterion.ToString();

                textBox7.Visible = comboBox5.Visible = textBox11.Visible = true;
                textBox7.Text = _listCriteriaForSearch[3].nameOfCriterion;
                comboBox5.Text = _listCriteriaForSearch[3].directionOfCriterion == false ? "min" : "max";
                textBox11.Text = _listCriteriaForSearch[3].weightOfCriterion.ToString();

                textBox10.Visible = comboBox6.Visible = textBox9.Visible = true;
                textBox10.Text = _listCriteriaForSearch[4].nameOfCriterion;
                comboBox6.Text = _listCriteriaForSearch[4].directionOfCriterion == false ? "min" : "max";
                textBox9.Text = _listCriteriaForSearch[4].weightOfCriterion.ToString();

                textBox12.Visible = comboBox7.Visible = textBox8.Visible = true;
                textBox12.Text = _listCriteriaForSearch[5].nameOfCriterion;
                comboBox7.Text = _listCriteriaForSearch[5].directionOfCriterion == false ? "min" : "max";
                textBox8.Text = _listCriteriaForSearch[5].weightOfCriterion.ToString();

                textBox14.Visible = comboBox8.Visible = textBox13.Visible = true;
                textBox14.Text = _listCriteriaForSearch[6].nameOfCriterion;
                comboBox8.Text = _listCriteriaForSearch[6].directionOfCriterion == false ? "min" : "max";
                textBox13.Text = _listCriteriaForSearch[6].weightOfCriterion.ToString();

                groupBoxCriterion.Size = new Size(504, 358);
            }
            _flagDrawComboBoxes = true;
            _mapModel.flagChangeCriterion = false;
        }

        // Загруженный пользователем значок для отображения объекта социальной инфраструктуры на карте
        private Bitmap _bitmapForLoadIconFacility;
        /// <summary>
        /// Загрузка значка
        /// </summary>
        private void buttonLoadIconFacility_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialogLoadingIconFacility = new OpenFileDialog();
                // PNG — оптимален для изображений с небольшим количеством цветов, например, для иконок, схем, рисунков и скриншотов.
                // JPEG — это формат изображений, который использует сжатие с потерями и не поддерживает прозрачность.
                dialogLoadingIconFacility.Filter = "Files (*.PNG)|*.PNG;";
                dialogLoadingIconFacility.InitialDirectory = Application.StartupPath + @"\Icon\IconsCollection";
                dialogLoadingIconFacility.Title = "Выберите значок для отображения объекта на карте";

                if (dialogLoadingIconFacility.ShowDialog() == DialogResult.OK)
                {
                    // Получение загруженного значка
                    _bitmapForLoadIconFacility = (Bitmap)Image.FromFile(dialogLoadingIconFacility.FileName.ToString());
                    Image image = _bitmapForLoadIconFacility;

                    // Если разрешение файла от 5х5 до 90x90 пикселов 
                    if (_bitmapForLoadIconFacility.Size.Height >= 5 && _bitmapForLoadIconFacility.Size.Height <= 90 &&
                        _bitmapForLoadIconFacility.Size.Width >= 5 && _bitmapForLoadIconFacility.Size.Width <= 90)
                    {
                        // Если файл png формата
                        if (image.RawFormat.Equals(ImageFormat.Png))
                        {
                            _pathToImageFacility = dialogLoadingIconFacility.FileName.ToString();
                            // Отобразить значок в pictureBox
                            pictureIconFacility.Image = image;
                        }
                        else
                            MessageBox.Show("Файл должен иметь png-расширение", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        MessageBox.Show("Разрешение файла должно быть в пределах от 5х5 до 90x90 пикселов", "Предупреждение",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("Не получилось открыть файл", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Флаг введенного названия объекта социальной инфраструктуры
        private bool _isLoadName = true;
        /// <summary>
        /// Проверка корректности названия объекта социальной инфраструктуры
        /// </summary>
        private void _CheckVadidateNameFaicility()
        {
            // Обнулить флаг
            _isLoadName = false;

            // Если непустое поле
            if (!string.IsNullOrEmpty(textBoxNameFacility.Text) && !string.IsNullOrWhiteSpace(textBoxNameFacility.Text))
            {
                // Если длина строки больше 2 и меньше 20
                if (textBoxNameFacility.TextLength >= 2 && textBoxNameFacility.TextLength <= 20)
                {
                    // Название успешно задано
                    _isLoadName = true;
                    // Сохранить название объекта социальной инфраструктуры
                    _nameFacility = textBoxNameFacility.Text;
                }
                else
                    MessageBox.Show("Длина поля должна быть от 2 до 20 символов", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Поле \"Название объекта\" не заполнено", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Проверка корректности названия критериея
        /// </summary>
        /// <param name="nameCriterion">Название критерия</param>
        /// <returns>Флаг корректности</returns>
        private bool _CheckNameCriterion(string nameCriterion)
        {
            // Если непустое поле
            if (!string.IsNullOrEmpty(nameCriterion) && !string.IsNullOrWhiteSpace(nameCriterion) && nameCriterion != "Скрыть")
            {
                // Название от 2 до 30 символов
                if (nameCriterion.Length >= 2 && nameCriterion.Length <= 30)
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        ///  Проверка корректности направления
        /// </summary>
        /// <param name="directionCriterion">Направление критерия</param>
        /// <returns>Флаг корректности</returns>
        private bool _CheckDirectionCriterion(string directionCriterion)
        {
            // Если непустое поле
            if (!string.IsNullOrEmpty(directionCriterion) && !string.IsNullOrWhiteSpace(directionCriterion))
            {
                // Если в поле min или max
                if (directionCriterion == "min" || directionCriterion == "max")
                    return true;
                else
                    return false;
            }
            else
                return false;
        }

        /// <summary>
        /// Проверка корректности весового коэффициента
        /// </summary>
        /// <param name="weihgt">Весовой коэффициент</param>
        /// <returns></returns>
        private bool _CheckWeightCriterion(string weihgt)
        {
            // Если поле не пустое
            if (!string.IsNullOrEmpty(weihgt) && !string.IsNullOrWhiteSpace(weihgt))
            {
                double tempValue;
                bool testForCriterion = double.TryParse(weihgt, out tempValue);
                // Если строку можно преобразовать в число
                if (testForCriterion)
                {
                    double Weihgt = Convert.ToDouble(weihgt);
                    // Если вес в интервале [0;1]
                    if (Weihgt >= 0 && Weihgt <= 1)
                        return true;
                    else
                        return false;
                }
                else
                    return false;
            }
            else
                return false;
        }

        // Флаг загрузки критериев
        private bool _isLoadCriterion = false;
        // Количество критериев из выпадающего списка
        private int _countCriterionOnComboBox = 0;
        /// <summary>
        /// Проверка заполненности критериев-направлений-весов
        /// </summary>
        /// <returns>Статус загрузки данных</returns>
        private string _CheckValidateCriterion()
        {
            // Строка с ошибкой
            var errorCriterion = new StringBuilder();
            errorCriterion.AppendLine("В одном из критериев некорректные данные");
            errorCriterion.AppendLine("1. Название критерия должно быть от 2 до 30 символов");
            errorCriterion.AppendLine("2. У каждого критерия должно быть выбрано направление");
            errorCriterion.AppendLine("3. Весовой коэффициент должен быть значением от 0 до 1");
            errorCriterion.AppendLine("4. Запрещено называть критерий \"Скрыть\"");
            errorCriterion.AppendLine("5. У критериев должны быть различающиеся названия");

            // Очистить список и флаг
            _isLoadCriterion = false;

            // Если выбран 1 критерий
            if (_countCriterionOnComboBox == 1)
            {
                // Проверка названия, направления и веса
                if (_CheckNameCriterion(textBox1.Text) && _CheckDirectionCriterion(comboBox2.Text) && _CheckWeightCriterion(textBox4.Text))
                {
                    double weight_1 = Convert.ToDouble(textBox4.Text);
                    if (weight_1 == 1)
                    {
                        _criterion.Clear();
                        if (comboBox2.Text == "min")
                            _criterion.Add(new Criterion(textBox1.Text, false, weight_1));
                        else
                            _criterion.Add(new Criterion(textBox1.Text, true, weight_1));

                        // Валидация файла: соответствует ли файл заданному количеству критериев
                        _ValidateNewFilePolygon();
                        if (_isLoadFilePolygons)
                        {
                            // 1 критерий успешно задан
                            _isLoadCriterion = true;
                            // Список критериев
                            _listCriteriaForSearch = _criterion;
                            _mapModel.flagChangeCriterion = true;
                            return "Успешно";
                        }
                        else
                        {
                            _criterion = _saveCriterion;
                            return "В файле указано меньшее количество критериев";
                        }
                    }
                    else
                        return "Сумма весов должна быть равна 1";
                }
                else
                    return errorCriterion.ToString();
            }
            // Если выбрано 2 критерия
            else if (_countCriterionOnComboBox == 2)
            {
                // Проверка названия, направления и веса
                if (_CheckNameCriterion(textBox1.Text) && _CheckDirectionCriterion(comboBox2.Text) && _CheckWeightCriterion(textBox4.Text) &&
                    _CheckNameCriterion(textBox2.Text) && _CheckDirectionCriterion(comboBox3.Text) && _CheckWeightCriterion(textBox5.Text))
                {
                    double weight_1 = Convert.ToDouble(textBox4.Text);
                    double weight_2 = Convert.ToDouble(textBox5.Text);

                    if (weight_1 + weight_2 >= 0.998 &&
                      weight_1 + weight_2 <= 1)
                    {
                        _criterion.Clear();
                        if (comboBox2.Text == "min")
                            _criterion.Add(new Criterion(textBox1.Text, false, weight_1));
                        else
                            _criterion.Add(new Criterion(textBox1.Text, true, weight_1));

                        if (comboBox3.Text == "min")
                            _criterion.Add(new Criterion(textBox2.Text, false, weight_2));
                        else
                            _criterion.Add(new Criterion(textBox2.Text, true, weight_2));

                        // Валидация файла: соответствует ли файл заданному количеству критериев
                        _ValidateNewFilePolygon();
                        if (_isLoadFilePolygons)
                        {
                            // 2 критерия успешно задан
                            _isLoadCriterion = true;
                            // Список критериев
                            _listCriteriaForSearch = _criterion;
                            _mapModel.flagChangeCriterion = true;
                            return "Успешно";
                        }
                        else
                        {
                            _criterion = _saveCriterion;
                            return "В файле указано меньшее количество критериев";
                        }
                    }
                    else
                        return "Сумма весов должна быть равна 1";
                }
                else
                    return errorCriterion.ToString();
            }

            // Если выбрано 3 критерия
            else if (_countCriterionOnComboBox == 3)
            {
                // Проверка названия, направления и веса
                if (_CheckNameCriterion(textBox1.Text) && _CheckDirectionCriterion(comboBox2.Text) && _CheckWeightCriterion(textBox4.Text) &&
                    _CheckNameCriterion(textBox2.Text) && _CheckDirectionCriterion(comboBox3.Text) && _CheckWeightCriterion(textBox5.Text) &&
                    _CheckNameCriterion(textBox3.Text) && _CheckDirectionCriterion(comboBox4.Text) && _CheckWeightCriterion(textBox6.Text))
                {
                    double weight_1 = Convert.ToDouble(textBox4.Text);
                    double weight_2 = Convert.ToDouble(textBox5.Text);
                    double weight_3 = Convert.ToDouble(textBox6.Text);

                    if (weight_1 + weight_2 + weight_3 >= 0.998 &&
                       weight_1 + weight_2 + weight_3 <= 1)
                    {
                        _criterion.Clear();
                        if (comboBox2.Text == "min")
                            _criterion.Add(new Criterion(textBox1.Text, false, weight_1));
                        else
                            _criterion.Add(new Criterion(textBox1.Text, true, weight_1));

                        if (comboBox3.Text == "min")
                            _criterion.Add(new Criterion(textBox2.Text, false, weight_2));
                        else
                            _criterion.Add(new Criterion(textBox2.Text, true, weight_2));

                        if (comboBox4.Text == "min")
                            _criterion.Add(new Criterion(textBox3.Text, false, weight_3));
                        else
                            _criterion.Add(new Criterion(textBox3.Text, true, weight_3));

                        // Валидация файла: соответствует ли файл заданному количеству критериев
                        _ValidateNewFilePolygon();
                        if (_isLoadFilePolygons)
                        {
                            // 3 критерия успешно задан
                            _isLoadCriterion = true;
                            // Список критериев
                            _listCriteriaForSearch = _criterion;
                            _mapModel.flagChangeCriterion = true;
                            return "Успешно";
                        }
                        else
                        {
                            _criterion = _saveCriterion;
                            return "В файле указано меньшее количество критериев";
                        }
                    }
                    else
                        return "Сумма весов должна быть равна 1";
                }
                else
                    return errorCriterion.ToString();
            }
            // Если выбрано 4 критерия
            else if (_countCriterionOnComboBox == 4)
            {
                // Проверка названия, направления и веса
                if (_CheckNameCriterion(textBox1.Text) && _CheckDirectionCriterion(comboBox2.Text) && _CheckWeightCriterion(textBox4.Text) &&
                    _CheckNameCriterion(textBox2.Text) && _CheckDirectionCriterion(comboBox3.Text) && _CheckWeightCriterion(textBox5.Text) &&
                    _CheckNameCriterion(textBox3.Text) && _CheckDirectionCriterion(comboBox4.Text) && _CheckWeightCriterion(textBox6.Text) &&
                    _CheckNameCriterion(textBox7.Text) && _CheckDirectionCriterion(comboBox5.Text) && _CheckWeightCriterion(textBox11.Text))
                {
                    double weight_1 = Convert.ToDouble(textBox4.Text);
                    double weight_2 = Convert.ToDouble(textBox5.Text);
                    double weight_3 = Convert.ToDouble(textBox6.Text);
                    double weight_4 = Convert.ToDouble(textBox11.Text);

                    if (weight_1 + weight_2 + weight_3 + weight_4 >= 0.998 &&
                       weight_1 + weight_2 + weight_3 + weight_4 <= 1)
                    {
                        _criterion.Clear();
                        if (comboBox2.Text == "min")
                            _criterion.Add(new Criterion(textBox1.Text, false, weight_1));
                        else
                            _criterion.Add(new Criterion(textBox1.Text, true, weight_1));

                        if (comboBox3.Text == "min")
                            _criterion.Add(new Criterion(textBox2.Text, false, weight_2));
                        else
                            _criterion.Add(new Criterion(textBox2.Text, true, weight_2));

                        if (comboBox4.Text == "min")
                            _criterion.Add(new Criterion(textBox3.Text, false, weight_3));
                        else
                            _criterion.Add(new Criterion(textBox3.Text, true, weight_3));

                        if (comboBox5.Text == "min")
                            _criterion.Add(new Criterion(textBox7.Text, false, weight_4));
                        else
                            _criterion.Add(new Criterion(textBox7.Text, true, weight_4));

                        // Валидация файла: соответствует ли файл заданному количеству критериев
                        _ValidateNewFilePolygon();
                        if (_isLoadFilePolygons)
                        {
                            // 4 критерия успешно задан
                            _isLoadCriterion = true;
                            // Список критериев
                            _listCriteriaForSearch = _criterion;
                            _mapModel.flagChangeCriterion = true;
                            return "Успешно";
                        }
                        else
                        {
                            _criterion = _saveCriterion;
                            return "В файле указано меньшее количество критериев";
                        }
                    }
                    else
                        return "Сумма весов должна быть равна 1";
                }
                else
                    return errorCriterion.ToString();
            }
            // Если выбрано 5 критериев
            else if (_countCriterionOnComboBox == 5)
            {
                // Проверка названия, направления и веса
                if (_CheckNameCriterion(textBox1.Text) && _CheckDirectionCriterion(comboBox2.Text) && _CheckWeightCriterion(textBox4.Text) &&
                    _CheckNameCriterion(textBox2.Text) && _CheckDirectionCriterion(comboBox3.Text) && _CheckWeightCriterion(textBox5.Text) &&
                    _CheckNameCriterion(textBox3.Text) && _CheckDirectionCriterion(comboBox4.Text) && _CheckWeightCriterion(textBox6.Text) &&
                    _CheckNameCriterion(textBox7.Text) && _CheckDirectionCriterion(comboBox5.Text) && _CheckWeightCriterion(textBox11.Text) &&
                    _CheckNameCriterion(textBox10.Text) && _CheckDirectionCriterion(comboBox6.Text) && _CheckWeightCriterion(textBox9.Text))
                {
                    double weight_1 = Convert.ToDouble(textBox4.Text);
                    double weight_2 = Convert.ToDouble(textBox5.Text);
                    double weight_3 = Convert.ToDouble(textBox6.Text);
                    double weight_4 = Convert.ToDouble(textBox11.Text);
                    double weight_5 = Convert.ToDouble(textBox9.Text);

                    if (weight_1 + weight_2 + weight_3 + weight_4 + weight_5 >= 0.998 &&
                        weight_1 + weight_2 + weight_3 + weight_4 + weight_5 <= 1)
                    {
                        _criterion.Clear();
                        if (comboBox2.Text == "min")
                            _criterion.Add(new Criterion(textBox1.Text, false, weight_1));
                        else
                            _criterion.Add(new Criterion(textBox1.Text, true, weight_1));

                        if (comboBox3.Text == "min")
                            _criterion.Add(new Criterion(textBox2.Text, false, weight_2));
                        else
                            _criterion.Add(new Criterion(textBox2.Text, true, weight_2));

                        if (comboBox4.Text == "min")
                            _criterion.Add(new Criterion(textBox3.Text, false, weight_3));
                        else
                            _criterion.Add(new Criterion(textBox3.Text, true, weight_3));

                        if (comboBox5.Text == "min")
                            _criterion.Add(new Criterion(textBox7.Text, false, weight_4));
                        else
                            _criterion.Add(new Criterion(textBox7.Text, true, weight_4));
                        if (comboBox6.Text == "min")
                            _criterion.Add(new Criterion(textBox10.Text, false, weight_5));
                        else
                            _criterion.Add(new Criterion(textBox10.Text, true, weight_5));

                        // Валидация файла: соответствует ли файл заданному количеству критериев
                        _ValidateNewFilePolygon();
                        if (_isLoadFilePolygons)
                        {
                            // 5 критериев успешно задан
                            _isLoadCriterion = true;
                            // Список критериев
                            _listCriteriaForSearch = _criterion;
                            _mapModel.flagChangeCriterion = true;
                            return "Успешно";
                        }
                        else
                        {
                            _criterion = _saveCriterion;
                            return "В файле указано меньшее количество критериев";
                        }
                    }
                    else
                        return "Сумма весов должна быть равна 1";
                }
                else
                    return errorCriterion.ToString();
            }
            // Если выбрано 6 критериев
            else if (_countCriterionOnComboBox == 6)
            {
                // Проверка названия, направления и веса
                if (_CheckNameCriterion(textBox1.Text) && _CheckDirectionCriterion(comboBox2.Text) && _CheckWeightCriterion(textBox4.Text) &&
                    _CheckNameCriterion(textBox2.Text) && _CheckDirectionCriterion(comboBox3.Text) && _CheckWeightCriterion(textBox5.Text) &&
                    _CheckNameCriterion(textBox3.Text) && _CheckDirectionCriterion(comboBox4.Text) && _CheckWeightCriterion(textBox6.Text) &&
                    _CheckNameCriterion(textBox7.Text) && _CheckDirectionCriterion(comboBox5.Text) && _CheckWeightCriterion(textBox11.Text) &&
                    _CheckNameCriterion(textBox10.Text) && _CheckDirectionCriterion(comboBox6.Text) && _CheckWeightCriterion(textBox9.Text) &&
                    _CheckNameCriterion(textBox12.Text) && _CheckDirectionCriterion(comboBox7.Text) && _CheckWeightCriterion(textBox8.Text))
                {
                    double weight_1 = Convert.ToDouble(textBox4.Text);
                    double weight_2 = Convert.ToDouble(textBox5.Text);
                    double weight_3 = Convert.ToDouble(textBox6.Text);
                    double weight_4 = Convert.ToDouble(textBox11.Text);
                    double weight_5 = Convert.ToDouble(textBox9.Text);
                    double weight_6 = Convert.ToDouble(textBox8.Text);

                    if (weight_1 + weight_2 + weight_3 + weight_4 + weight_5 + weight_6 >= 0.998 &&
                        weight_1 + weight_2 + weight_3 + weight_4 + weight_5 + weight_6 <= 1)
                    {
                        _criterion.Clear();
                        if (comboBox2.Text == "min")
                            _criterion.Add(new Criterion(textBox1.Text, false, weight_1));
                        else
                            _criterion.Add(new Criterion(textBox1.Text, true, weight_1));

                        if (comboBox3.Text == "min")
                            _criterion.Add(new Criterion(textBox2.Text, false, weight_2));
                        else
                            _criterion.Add(new Criterion(textBox2.Text, true, weight_2));

                        if (comboBox4.Text == "min")
                            _criterion.Add(new Criterion(textBox3.Text, false, weight_3));
                        else
                            _criterion.Add(new Criterion(textBox3.Text, true, weight_3));

                        if (comboBox5.Text == "min")
                            _criterion.Add(new Criterion(textBox7.Text, false, weight_4));
                        else
                            _criterion.Add(new Criterion(textBox7.Text, true, weight_4));
                        if (comboBox6.Text == "min")
                            _criterion.Add(new Criterion(textBox10.Text, false, weight_5));
                        else
                            _criterion.Add(new Criterion(textBox10.Text, true, weight_5));
                        if (comboBox7.Text == "min")
                            _criterion.Add(new Criterion(textBox12.Text, false, weight_6));
                        else
                            _criterion.Add(new Criterion(textBox12.Text, true, weight_6));

                        // Валидация файла: соответствует ли файл заданному количеству критериев
                        _ValidateNewFilePolygon();
                        if (_isLoadFilePolygons)
                        {
                            // 6 критериев успешно задан
                            _isLoadCriterion = true;
                            // Список критериев
                            _listCriteriaForSearch = _criterion;
                            _mapModel.flagChangeCriterion = true;
                            return "Успешно";
                        }
                        else
                        {
                            _criterion = _saveCriterion;
                            return "В файле указано меньшее количество критериев";
                        }
                    }
                    else
                        return "Сумма весов должна быть равна 1";
                }
                else
                    return errorCriterion.ToString();
            }
            // Если выбрано 7 критериев
            else if (_countCriterionOnComboBox == 7)
            {
                // Проверка названия, направления и веса
                if (_CheckNameCriterion(textBox1.Text) && _CheckDirectionCriterion(comboBox2.Text) && _CheckWeightCriterion(textBox4.Text) &&
                    _CheckNameCriterion(textBox2.Text) && _CheckDirectionCriterion(comboBox3.Text) && _CheckWeightCriterion(textBox5.Text) &&
                    _CheckNameCriterion(textBox3.Text) && _CheckDirectionCriterion(comboBox4.Text) && _CheckWeightCriterion(textBox6.Text) &&
                    _CheckNameCriterion(textBox7.Text) && _CheckDirectionCriterion(comboBox5.Text) && _CheckWeightCriterion(textBox11.Text) &&
                    _CheckNameCriterion(textBox10.Text) && _CheckDirectionCriterion(comboBox6.Text) && _CheckWeightCriterion(textBox9.Text) &&
                    _CheckNameCriterion(textBox12.Text) && _CheckDirectionCriterion(comboBox7.Text) && _CheckWeightCriterion(textBox8.Text) &&
                    _CheckNameCriterion(textBox14.Text) && _CheckDirectionCriterion(comboBox8.Text) && _CheckWeightCriterion(textBox13.Text))
                {
                    double weight_1 = Convert.ToDouble(textBox4.Text);
                    double weight_2 = Convert.ToDouble(textBox5.Text);
                    double weight_3 = Convert.ToDouble(textBox6.Text);
                    double weight_4 = Convert.ToDouble(textBox11.Text);
                    double weight_5 = Convert.ToDouble(textBox9.Text);
                    double weight_6 = Convert.ToDouble(textBox8.Text);
                    double weight_7 = Convert.ToDouble(textBox13.Text);

                    if (weight_1 + weight_2 + weight_3 + weight_4 + weight_5 + weight_6 + weight_7 >= 0.998 &&
                        weight_1 + weight_2 + weight_3 + weight_4 + weight_5 + weight_6 + weight_7 <= 1)
                    {
                        _criterion.Clear();
                        if (comboBox2.Text == "min")
                            _criterion.Add(new Criterion(textBox1.Text, false, weight_1));
                        else
                            _criterion.Add(new Criterion(textBox1.Text, true, weight_1));

                        if (comboBox3.Text == "min")
                            _criterion.Add(new Criterion(textBox2.Text, false, weight_2));
                        else
                            _criterion.Add(new Criterion(textBox2.Text, true, weight_2));

                        if (comboBox4.Text == "min")
                            _criterion.Add(new Criterion(textBox3.Text, false, weight_3));
                        else
                            _criterion.Add(new Criterion(textBox3.Text, true, weight_3));

                        if (comboBox5.Text == "min")
                            _criterion.Add(new Criterion(textBox7.Text, false, weight_4));
                        else
                            _criterion.Add(new Criterion(textBox7.Text, true, weight_4));
                        if (comboBox6.Text == "min")
                            _criterion.Add(new Criterion(textBox10.Text, false, weight_5));
                        else
                            _criterion.Add(new Criterion(textBox10.Text, true, weight_5));
                        if (comboBox7.Text == "min")
                            _criterion.Add(new Criterion(textBox12.Text, false, weight_6));
                        else
                            _criterion.Add(new Criterion(textBox12.Text, true, weight_6));
                        if (comboBox8.Text == "min")
                            _criterion.Add(new Criterion(textBox14.Text, false, weight_7));
                        else
                            _criterion.Add(new Criterion(textBox14.Text, true, weight_7));

                        // Валидация файла: соответствует ли файл заданному количеству критериев
                        _ValidateNewFilePolygon();
                        if (_isLoadFilePolygons)
                        {
                            // 7 критериев успешно задан
                            _isLoadCriterion = true;
                            // Список критериев
                            _listCriteriaForSearch = _criterion;
                            _mapModel.flagChangeCriterion = true;
                            return "Успешно";
                        }
                        else
                        {
                            _criterion = _saveCriterion;
                            return "В файле указано меньшее количество критериев";
                        }
                    }
                    else
                        return "Сумма весов должна быть равна 1";
                }
                else
                    return errorCriterion.ToString();
            }
            else
                return "";
        }

        // Флаг загрузки файла с границами зоны анализа
        private bool _isLoadFileBorderTerritory = true;
        private bool _isChangeFileBorderTerritory = false;
        /// <summary>
        /// Загрузка файла с границами
        /// </summary>
        private void buttonLoadBorderTerritory_Click(object sender, EventArgs e)
        {
            // Если файл открыт в данный момент
            try
            {
                OpenFileDialog openDialogLoadFileBorder = new OpenFileDialog();
                openDialogLoadFileBorder.Filter = "Files (*.csv)|*.csv;";
                openDialogLoadFileBorder.InitialDirectory = Application.StartupPath + @"\Data\Cases\Зоны анализа";
                openDialogLoadFileBorder.Title = "Выберите CSV-файл с граничными точками зоны анализа";

                if (openDialogLoadFileBorder.ShowDialog() == DialogResult.OK)
                {
                    string pathToFile = openDialogLoadFileBorder.FileName.ToString();
                    string isValidFile = _fileValidator.ValidateUserFileCSV(pathToFile);

                    // Если файл прошёл все проверки
                    if (isValidFile == "Успешно")
                    {
                        // Валидация хранения данных в файле
                        string isValidDataFile = _fileValidator.ValidateUserFileTerritory(pathToFile);

                        // Если файл прошёл все проверки
                        if (isValidDataFile == "Успешно")
                        {
                            // Список для чтения координат
                            List<Location> tempListPoints = new List<Location>();
                            using (StreamReader fileReader = new StreamReader(pathToFile, Encoding.GetEncoding(1251)))
                            {
                                while (!fileReader.EndOfStream)
                                {
                                    string oneString = fileReader.ReadLine();
                                    string[] splitOneString = oneString.Split(new char[] { ';' });
                                    tempListPoints.Add(new Location(Convert.ToDouble(splitOneString[0], _cultureInfo),
                                        Convert.ToDouble(splitOneString[1], _cultureInfo)));
                                }
                            }
                            // Сохранить список граничных точек зоны анализа
                            _listPointsBorderTerritory = tempListPoints;
                            _isChangeFileBorderTerritory = true;

                            // При загрузке нового файла сбросить файлы с полигонами и объектами социальной инфраструктуры
                            // Они должны быть загружены заново
                            _isLoadFileFacilities = false;
                            _ValidateNewFilePolygonAndCriterion();
                        }
                        else
                            MessageBox.Show(isValidDataFile, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        MessageBox.Show(isValidFile, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Закройте файл, из которого загружаются данные", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Список строк с информацией о каждом объекте социальной инфраструктуры
        private List<string> _infoFacility = new List<string>();
        // Флаг загрузки объектов социальной инфраструктуры
        private bool _isLoadFileFacilities = true;
        /// <summary>
        /// Загрузка файла с объектами социальной инфраструктуры
        /// </summary>
        private void buttonLoadFacilities_Click(object sender, EventArgs e)
        {
            _listFacilitiesFromFile = new List<Facility>();

            // Если файл открыт в данный момент
            try
            {
                OpenFileDialog openDialogLoadFileFacility = new OpenFileDialog();
                openDialogLoadFileFacility.Filter = "Files (*.csv)|*.csv;";
                openDialogLoadFileFacility.InitialDirectory = Application.StartupPath + @"\Data\Cases\Объекты инфраструктуры";
                openDialogLoadFileFacility.Title = "Выберите CSV-файл для загрузки данных об объектах инфраструктуры";

                if (openDialogLoadFileFacility.ShowDialog() == DialogResult.OK)
                {
                    string pathToFile = openDialogLoadFileFacility.FileName.ToString();
                    string isValidFile = _fileValidator.ValidateUserFileCSV(pathToFile);

                    // Если файл прошёл все проверки
                    if (isValidFile == "Успешно")
                    {
                        // Валидация хранения данных в файле
                        string isValidDataFile = _fileValidator.ValidateUserFileFacility(pathToFile, _listPointsBorderTerritory);

                        // Если файл прошёл все проверки
                        if (isValidDataFile == "Успешно")
                        {
                            // Список с объектами инфраструктуры
                            List<Facility> tempListPointsFacility = new List<Facility>();

                            _listFacilitiesFromFile.Clear();

                            using (StreamReader fileReader = new StreamReader(pathToFile, Encoding.GetEncoding(1251)))
                            {
                                while (!fileReader.EndOfStream)
                                {
                                    string oneString = fileReader.ReadLine();
                                    string[] splitOneString = oneString.Split(new char[] { ';' });
                                    _infoFacility = new List<string>();
                                    for (int i = 3; i < splitOneString.Length; i++)
                                    {
                                        if (splitOneString[i].Length == 0 || splitOneString[i] == "" || splitOneString[i] == " ")
                                            _infoFacility.Add("отсутствует");
                                        else
                                            _infoFacility.Add(splitOneString[i].ToString());
                                    }

                                    // Добавить в список объект социальной инфраструктуры
                                    tempListPointsFacility.Add(new Facility(Convert.ToInt32(splitOneString[0]),
                                        Convert.ToDouble(splitOneString[1], _cultureInfo), Convert.ToDouble(splitOneString[2], _cultureInfo), _infoFacility));
                                }
                                // Сохранить список с объектами социальной инфраструктуры
                                _listFacilitiesFromFile = tempListPointsFacility;
                                // Флаг успешной загрузки файла с объектами социальной инфраструктуры
                                _isLoadFileFacilities = true;
                            }
                        }
                        else
                            MessageBox.Show(isValidDataFile, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        MessageBox.Show(isValidFile, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (IOException)
            {
                MessageBox.Show("Закройте файл, из которого загружаются данные", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Проверить, подходит ли файл с полигонами на новое измененное количеств критериев
        /// </summary>
        private void _ValidateNewFilePolygonAndCriterion()
        {
            _isLoadFilePolygons = false;
            // Проверить загруженные критерии
            string resultValidateCriterion = _CheckValidateCriterion();
            if (_isLoadCriterion)
            {
                string pathToFile = _mapModel.pathToFilePolygonUser;
                string isValidFile = _fileValidator.ValidateUserFileCSV(pathToFile);

                // Если файл прошёл все проверки
                if (isValidFile == "Успешно")
                {
                    // Валидация хранения данных в файле
                    string isValidDataFile = _fileValidator.ValidateUserFilePolygons(pathToFile, _listPointsBorderTerritory, _criterion.Count);

                    // Если файл прошёл все проверки
                    if (isValidDataFile == "Успешно")
                        _isLoadFilePolygons = true;
                    else
                        _isLoadFilePolygons = false;
                }
                else
                    _isLoadFilePolygons = false;
            }
        }

        /// <summary>
        /// Проверить, подходит ли файл с полигонами на неизменяющееся количество критериев
        /// </summary>
        private void _ValidateNewFilePolygon()
        {
            string pathToFile;
            if (_pathToFilePolygons == "")
                pathToFile = _mapModel.pathToFilePolygonUser;
            else
                pathToFile = _pathToFilePolygons;

            string isValidFile = _fileValidator.ValidateUserFileCSV(pathToFile);

            // Если файл прошёл все проверки
            if (isValidFile == "Успешно")
            {
                // Валидация хранения данных в файле
                string isValidDataFile = _fileValidator.ValidateUserFilePolygons(pathToFile, _listPointsBorderTerritory, _criterion.Count);

                // Если файл прошёл все проверки
                if (isValidDataFile == "Успешно")
                    _isLoadFilePolygons = true;
                else
                    _isLoadFilePolygons = false;
            }
            else
                _isLoadFilePolygons = false;
        }

        // Флаг загрузки полигонов
        private bool _isLoadFilePolygons = true;
        // Путь к файлу с полигонами
        private string _pathToFilePolygons = "";
        /// <summary>
        /// Загрузка файла с полигонами
        /// </summary>
        private void buttonLoadPolygons_Click(object sender, EventArgs e)
        {
            // Если не загружены границы зоны анализа
            if (_isLoadFileBorderTerritory)
            {
                // Если файл открыт в данный момент
                try
                {
                    OpenFileDialog openDialogLoadFilePolygon = new OpenFileDialog();
                    openDialogLoadFilePolygon.Filter = "Files (*.csv)|*.csv;";
                    openDialogLoadFilePolygon.InitialDirectory = Application.StartupPath + @"\Data\Cases\Полигоны";
                    openDialogLoadFilePolygon.Title = "Выберите CSV-файл для загрузки данных о полигонах";

                    if (openDialogLoadFilePolygon.ShowDialog() == DialogResult.OK)
                    {
                        string pathToFile = openDialogLoadFilePolygon.FileName.ToString();
                        string isValidFile = _fileValidator.ValidateUserFileCSV(pathToFile);

                        // Если файл прошёл все проверки
                        if (isValidFile == "Успешно")
                        {
                            // Валидация хранения данных в файле
                            _ValidateNewFilePolygonAndCriterion();
                            string isValidDataFile = _fileValidator.ValidateUserFilePolygons(pathToFile, _listPointsBorderTerritory, _criterion.Count);

                            // Если файл прошёл все проверки
                            if (isValidDataFile == "Успешно")
                            {
                                _pathToFilePolygons = pathToFile;
                                // Список полигонов
                                List<Polygon> tempListPolygons = new List<Polygon>();
                                // Список точек для каждого полигона
                                List<Location> tempListPoints = new List<Location>();

                                using (StreamReader readerCsvFile = new StreamReader(pathToFile, Encoding.GetEncoding(1251)))
                                {
                                    while (!readerCsvFile.EndOfStream)
                                    {
                                        string oneString = readerCsvFile.ReadLine();
                                        string[] splitOneString = oneString.Split(new char[] { ';' });
                                        int ID = Convert.ToInt32(splitOneString[0]);
                                        int countBoundaryPoints = Convert.ToInt32(splitOneString[1]);

                                        if (countBoundaryPoints >= 3)
                                        {
                                            tempListPoints = new List<Location>();
                                            for (int j = 2; j <= countBoundaryPoints * 2;)
                                            {
                                                tempListPoints.Add(new Location(Convert.ToDouble(splitOneString[j]),
                                                    Convert.ToDouble(splitOneString[j + 1])));
                                                j += 2;
                                            }

                                            // Координаты центральной точки полигона
                                            double centerX = Convert.ToDouble(splitOneString[countBoundaryPoints * 2 + 2]);
                                            double centerY = Convert.ToDouble(splitOneString[countBoundaryPoints * 2 + 3]);

                                            // Список значений каждого критерия для каждого полигона
                                            List<double> countValuesOfEveryCriterion = new List<double>();

                                            int lastCriterion = countBoundaryPoints * 2 + 4 + _criterion.Count;
                                            for (int j = countBoundaryPoints * 2 + 4; j < lastCriterion; j++)
                                                countValuesOfEveryCriterion.Add(Convert.ToDouble(splitOneString[j]));

                                            // Если все критерии больше или равны 0
                                            bool flagValueCriterionAboveZero = true;
                                            for (int k = 0; k < countValuesOfEveryCriterion.Count; k++)
                                            {
                                                if (countValuesOfEveryCriterion[k] < 0)
                                                    flagValueCriterionAboveZero = false;
                                            }

                                            if (flagValueCriterionAboveZero)
                                                // Добавить полигон в список
                                                tempListPolygons.Add(new Polygon(ID, countBoundaryPoints, tempListPoints,
                                                    centerX, centerY, countValuesOfEveryCriterion));
                                        }
                                    }
                                }

                                // Сохранить список полигонов
                                _listPolygonsFromFile = tempListPolygons;
                                // Флаг успешной загрузки файла с полигонами
                                _isLoadFilePolygons = true;
                            }
                            else
                                MessageBox.Show(isValidDataFile, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                            MessageBox.Show(isValidFile, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (IOException)
                {
                    MessageBox.Show("Закройте файл, из которого загружаются данные", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show("Сначала загрузите файл с граничными точками зоны анализа", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Кнопка "Сохранить"
        /// </summary>
        private void buttonValidateAndSave_Click(object sender, EventArgs e)
        {
            // Валидация названия объекта социальной инфраструктуры
            _CheckVadidateNameFaicility();
            if (_isLoadName)
            {
                // Валидация уникальности названий критериев оптимальности
                if (_CheckSameNameCriterion())
                {
                    // Валидация заданных критериев оптимальности
                    string resultRead = _CheckValidateCriterion();
                    if (resultRead == "Успешно" && _isLoadCriterion)
                    {
                        // Валидация файла с границами зоны анализа
                        if (_isLoadFileBorderTerritory)
                        {
                            // Валидация файла с полигонами
                            if (_isLoadFilePolygons)
                            {
                                // Валидация файла с объектами социальной инфраструктуры
                                if (_isLoadFileFacilities)
                                {
                                    _CheckValidateCriterion();
                                    // Список полигонов
                                    List<Polygon> tempListPolygons = new List<Polygon>();
                                    // Список точек для каждого полигона
                                    List<Location> tempListPoints = new List<Location>();

                                    using (StreamReader readerCsvFile = new StreamReader(_pathToFilePolygons, Encoding.GetEncoding(1251)))
                                    {
                                        while (!readerCsvFile.EndOfStream)
                                        {
                                            string OneString = readerCsvFile.ReadLine();
                                            string[] SplitOneString = OneString.Split(new char[] { ';' });
                                            int ID = Convert.ToInt32(SplitOneString[0]);
                                            int CountBoundaryPoints = Convert.ToInt32(SplitOneString[1]);

                                            if (CountBoundaryPoints >= 3)
                                            {
                                                tempListPoints = new List<Location>();
                                                for (int j = 2; j <= CountBoundaryPoints * 2;)
                                                {
                                                    tempListPoints.Add(new Location(Convert.ToDouble(SplitOneString[j]),
                                                        Convert.ToDouble(SplitOneString[j + 1])));
                                                    j += 2;
                                                }

                                                // Координаты центральной точки полигона
                                                double centerX = Convert.ToDouble(SplitOneString[CountBoundaryPoints * 2 + 2]);
                                                double centerY = Convert.ToDouble(SplitOneString[CountBoundaryPoints * 2 + 3]);

                                                // Список значений каждого критерия для каждого полигона
                                                List<double> countValuesOfEveryCriterion = new List<double>();

                                                int lastCriterion = CountBoundaryPoints * 2 + 4 + _criterion.Count;
                                                for (int j = CountBoundaryPoints * 2 + 4; j < lastCriterion; j++)
                                                    countValuesOfEveryCriterion.Add(Convert.ToDouble(SplitOneString[j]));

                                                // Если все критерии >= 0
                                                bool flagValueCriterionAboveZero = true;
                                                for (int k = 0; k < countValuesOfEveryCriterion.Count; k++)
                                                {
                                                    if (countValuesOfEveryCriterion[k] < 0)
                                                        flagValueCriterionAboveZero = false;
                                                }

                                                if (flagValueCriterionAboveZero)
                                                    // Добавить полигон в список
                                                    tempListPolygons.Add(new Polygon(ID, CountBoundaryPoints, tempListPoints,
                                                        centerX, centerY, countValuesOfEveryCriterion));
                                            }
                                        }
                                    }
                                    // Список с полигонами
                                    _listPolygonsFromFile = tempListPolygons;

                                    // Центрирование карты
                                    _locationCenterMap = _FindCenterPoint();
                                    _mapModel.centerMap = _locationCenterMap;

                                    // Значок и пусть к значку объекта социальной инфраструктуры
                                    _mapModel.iconFacility = pictureIconFacility.Image;
                                    _mapModel.sublayerFacility.iconOfOverlay = _pathToImageFacility;
                                    _mapModel.pathToIconObjectFacility = _pathToImageFacility;

                                    // Название объекта социальной инфраструктуры
                                    _mapModel.nameObjectFacility = _nameFacility;
                                    _mapModel.sublayerFacility.nameOfOverlay = _nameFacility;

                                    // Если загружен файл с объектами социальной инфраструктуры
                                    if (_isLoadFileFacilities)
                                    {
                                        _mapModel.listUserFacilities = _listFacilitiesFromFile;
                                        _mapModel.sublayerFacility.listWithFacility = _listFacilitiesFromFile;
                                    }

                                    // Если загружен файл с полигонами
                                    if (_isLoadFilePolygons)
                                    {
                                        _mapModel.flagChangePolygon = true;
                                        _mapModel.pathToFilePolygonUser = _pathToFilePolygons;

                                        _mapModel.listUserPolygons = _listPolygonsFromFile;
                                        _mapModel.sublayerPolygonrIcon.listWithPolygons = _listPolygonsFromFile;
                                        _mapModel.sublayerPolygonSquare.listWithPolygons = _listPolygonsFromFile;
                                        _mapModel.sublayerPolygonCheck.listWithPolygons = _listPolygonsFromFile;
                                        _mapModel.pathToFilePolygonUser = _pathToFilePolygons;
                                    }

                                    // Критерии
                                    _mapModel.listUserCriterion = _listCriteriaForSearch;

                                    // Если загружен файл с зоной анализа
                                    if (_isChangeFileBorderTerritory)
                                    {
                                        _mapModel.listUserPointsBorderTerritory = _listPointsBorderTerritory;
                                        _mapModel.sublayerBorderPointsTerritory.listWithLocation = _listPointsBorderTerritory;
                                        _mapModel.flagChangeTerritory = true;
                                    }

                                    Close();
                                }
                                else
                                    MessageBox.Show("Не загружен файл с объектами инфраструктуры",
                                        "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                                MessageBox.Show("Не загружен файл с полигонами", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                            MessageBox.Show("Не загружен файл с граничными точками зоны анализа",
                                "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                        MessageBox.Show(resultRead, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                    MessageBox.Show("Некоторые критерии имеют одинаковое название", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Проверка уникальности названия критериев
        /// </summary>
        /// <returns>Флаг уникальны или нет названия</returns>
        private bool _CheckSameNameCriterion()
        {
            // Список названий критериев
            List<string> namesCriterion = new List<string>();

            // Наполнить список названиями
            if (_countCriterionOnComboBox == 1)
                namesCriterion.Add(textBox1.Text);
            else if (_countCriterionOnComboBox == 2)
            {
                namesCriterion.Add(textBox1.Text);
                namesCriterion.Add(textBox2.Text);
            }
            else if (_countCriterionOnComboBox == 3)
            {
                namesCriterion.Add(textBox1.Text);
                namesCriterion.Add(textBox2.Text);
                namesCriterion.Add(textBox3.Text);
            }
            else if (_countCriterionOnComboBox == 4)
            {
                namesCriterion.Add(textBox1.Text);
                namesCriterion.Add(textBox2.Text);
                namesCriterion.Add(textBox3.Text);
                namesCriterion.Add(textBox7.Text);
            }
            else if (_countCriterionOnComboBox == 5)
            {
                namesCriterion.Add(textBox1.Text);
                namesCriterion.Add(textBox2.Text);
                namesCriterion.Add(textBox3.Text);
                namesCriterion.Add(textBox7.Text);
                namesCriterion.Add(textBox10.Text);
            }
            else if (_countCriterionOnComboBox == 6)
            {
                namesCriterion.Add(textBox1.Text);
                namesCriterion.Add(textBox2.Text);
                namesCriterion.Add(textBox3.Text);
                namesCriterion.Add(textBox7.Text);
                namesCriterion.Add(textBox10.Text);
                namesCriterion.Add(textBox12.Text);
            }
            else if (_countCriterionOnComboBox == 7)
            {
                namesCriterion.Add(textBox1.Text);
                namesCriterion.Add(textBox2.Text);
                namesCriterion.Add(textBox3.Text);
                namesCriterion.Add(textBox7.Text);
                namesCriterion.Add(textBox10.Text);
                namesCriterion.Add(textBox12.Text);
                namesCriterion.Add(textBox14.Text);
            }
            // Получить список уникальных названий критериев
            var distinct = namesCriterion.Distinct().ToList();

            // Если количество всех критериев и уникальных одинаково, значит все уникальные названия
            if (namesCriterion.Count == distinct.Count)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Поиск точки центрирования карты
        /// </summary>
        /// <returns>Координата для отцентрирования карты</returns>
        private Location _FindCenterPoint()
        {
            double xSumm = 0, ySumm = 0;
            for (int i = 0; i < _listPolygonsFromFile.Count; i++)
            {
                xSumm = xSumm + _listPolygonsFromFile[i].xCenterOfPolygon;
                ySumm = ySumm + _listPolygonsFromFile[i].yCenterOfPolygon;
            }
            // Сумма координат Х и координат Y всех центров полигонов и среднее значение будет центром карты
            Location location = new Location(xSumm / _listPolygonsFromFile.Count, ySumm / _listPolygonsFromFile.Count);
            return location;
        }

        /// <summary>
        /// Обработка выбранного числа в выпадающем списке количества критериев
        /// </summary>
        private void comboCountCriterion_SelectedIndexChanged(object sender, EventArgs e)
        {
            // При изменении критериев необходимо проверить, соответствует ли новому количеству критериев ранее загруженный файл с полигонами
            _countCriterionOnComboBox = Convert.ToInt32(comboCountCriterion.Text);
            if (_countCriterionOnComboBox == 1 && _flagDrawComboBoxes)
            {
                _ValidateNewFilePolygonAndCriterion();
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox2.Visible = comboBox3.Visible = textBox5.Visible = false;
                textBox3.Visible = comboBox4.Visible = textBox6.Visible = false;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = false;
                textBox10.Visible = comboBox6.Visible = textBox9.Visible = false;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = false;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 144);
            }
            else if (_countCriterionOnComboBox == 2 && _flagDrawComboBoxes)
            {
                _ValidateNewFilePolygonAndCriterion();
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox3.Visible = comboBox4.Visible = textBox6.Visible = false;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = false;
                textBox10.Visible = comboBox6.Visible = textBox9.Visible = false;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = false;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 179);
            }
            else if (_countCriterionOnComboBox == 3 && _flagDrawComboBoxes)
            {
                _ValidateNewFilePolygonAndCriterion();
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox3.Visible = comboBox4.Visible = textBox6.Visible = true;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = false;
                textBox10.Visible = comboBox6.Visible = textBox9.Visible = false;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = false;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 214);
            }
            else if (_countCriterionOnComboBox == 4 && _flagDrawComboBoxes)
            {
                _ValidateNewFilePolygonAndCriterion();
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox3.Visible = comboBox4.Visible = textBox6.Visible = true;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = true;
                textBox10.Visible = comboBox6.Visible = textBox9.Visible = false;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = false;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 254);
            }
            else if (_countCriterionOnComboBox == 5 && _flagDrawComboBoxes)
            {
                _ValidateNewFilePolygonAndCriterion();
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox3.Visible = comboBox4.Visible = textBox6.Visible = true;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = true;
                textBox10.Visible = comboBox6.Visible = textBox9.Visible = true;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = false;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 287);
            }
            else if (_countCriterionOnComboBox == 6 && _flagDrawComboBoxes)
            {
                _ValidateNewFilePolygonAndCriterion();
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox3.Visible = comboBox4.Visible = textBox6.Visible = true;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = true;
                textBox10.Visible = comboBox6.Visible = textBox9.Visible = true;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = true;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 323);
            }
            else if (_countCriterionOnComboBox == 7 && _flagDrawComboBoxes)
            {
                _ValidateNewFilePolygonAndCriterion();
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox3.Visible = comboBox4.Visible = textBox6.Visible = true;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = true;
                textBox10.Visible = comboBox6.Visible = textBox9.Visible = true;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = true;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = true;
                groupBoxCriterion.Size = new Size(504, 358);
            }
        }

        /// <summary>
        /// Закрыть форму через ESC
        /// </summary>
        private void Settings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}