﻿using GMap.NET;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Optimum
{
    public partial class StartSettings : Form
    {
        // Язык карты
        private LanguageType _languageOfMap;
        // Цвет интерфейса
        private Color _colorForApplication, _colorForElem;
        // значок объекта инфраструктуры
        private string _pathToIconFacility;
        // Название объекта инфраструктуры
        private string _nameFacility;
        // Лист критериев оптимальности
        private List<Criterion> _listCriteriaForSearch;
        // Список граничных точек территории
        private List<Location> _listPointsBorderTerritory;
        // Список полигонов из файла
        private List<Quar> _listQuartetsFromFile;
        // Список объектов инфраструктуры из файла
        private List<Facility> _listFacilitiesFromFile;
        // Точка центрирования карты
        Location _locationCenterMap = new Location();

        // Работа с файлом
        private FileValidator _fileValidator;
        // Отрисовка границ groupbox
        private PaintGroupBoxBorder _paintGroupBoxBorder = new PaintGroupBoxBorder();
        // Настройка для того, чтобы программа работала на английской версии Windows
        private readonly CultureInfo _cultureInfo = new CultureInfo("ru-RU");

        /// <summary>
        /// Создание формы
        /// </summary>
        /// <param name="language">Локализаци карты</param>
        /// <param name="_colorForInterface">Основной цвет интерфейса</param>
        /// <param name="_colorForElements">Второстепенный цвет интерфейса</param>
        public StartSettings(LanguageType language, Color _colorForInterface, Color _colorForElements)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;

            _languageOfMap = language;
            _colorForApplication = _colorForInterface;
            _colorForElem = _colorForElements;
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Settings_Load(object sender, EventArgs e)
        {
            // Валидация загружаемых файлов
            _fileValidator = new FileValidator();

            // Основной цвет интерфейса
            BackColor = comboCountCriterion.BackColor = buttonHelpSetCriterion.BackColor = buttonHelpSetFileBoundary.BackColor = buttonHelpSetFilePolygons.BackColor =
                buttonHelpSetIcon.BackColor = buttonHelpSetName.BackColor = buttonHelpSetFileSocialFacilities.BackColor = _colorForApplication;

            // Основной цвет интерфейса для выпадающих списков
            comboBox2.BackColor = comboBox3.BackColor = comboBox4.BackColor = comboBox5.BackColor = comboBox6.BackColor = comboBox7.BackColor =
                comboBox8.BackColor = _colorForApplication;

            // Второстепенный цвет интерфейса
            buttonValidateAndSave.BackColor = buttonLoadIconFacility.BackColor = buttonLoadBorderTerritory.BackColor =
                buttonLoadQuartets.BackColor = buttonLoadFacilities.BackColor = _colorForElem;

            // Отрисовка границ groupbox
            foreach (Control groupbox in Controls)
            {
                GroupBox everyGroupBox = groupbox as GroupBox;
                if (everyGroupBox != null)
                    everyGroupBox.Paint += _paintGroupBoxBorder.groupBox_Paint;
            }

            ClientSize = new Size(848, 638);
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // Настройка загрузки иконки
            pictureIconFacility.Image = Properties.Resources.iconNoIcon;
            buttonHelpSetIcon.FlatAppearance.BorderSize = 0;
            buttonHelpSetIcon.FlatStyle = FlatStyle.Flat;
            buttonLoadIconFacility.FlatAppearance.BorderSize = 0;
            buttonLoadIconFacility.FlatStyle = FlatStyle.Flat;
            buttonLoadIconFacility.TextAlign = ContentAlignment.MiddleCenter;

            // Настройка загрузки названия
            buttonHelpSetName.FlatAppearance.BorderSize = 0;
            buttonHelpSetName.FlatStyle = FlatStyle.Flat;

            // Настройка загрузки критериев
            buttonHelpSetCriterion.FlatAppearance.BorderSize = 0;
            buttonHelpSetCriterion.FlatStyle = FlatStyle.Flat;

            // Настройка выпадающего списка
            comboCountCriterion.DropDownStyle = ComboBoxStyle.DropDownList;
            comboCountCriterion.SelectedItem = "3";

            // Настройка выпадающих списков для минимизации/максимизации
            comboBox2.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox3.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox4.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox5.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox6.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox7.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox8.DropDownStyle = ComboBoxStyle.DropDownList;

            // Настройка загрузки файлов
            buttonHelpSetFileBoundary.FlatAppearance.BorderSize = 0;
            buttonHelpSetFileBoundary.FlatStyle = FlatStyle.Flat;
            buttonHelpSetFilePolygons.FlatAppearance.BorderSize = 0;
            buttonHelpSetFilePolygons.FlatStyle = FlatStyle.Flat;
            buttonHelpSetFileSocialFacilities.FlatAppearance.BorderSize = 0;
            buttonHelpSetFileSocialFacilities.FlatStyle = FlatStyle.Flat;
            buttonLoadQuartets.FlatAppearance.BorderSize = 0;
            buttonLoadQuartets.FlatStyle = FlatStyle.Flat;
            buttonLoadBorderTerritory.FlatAppearance.BorderSize = 0;
            buttonLoadBorderTerritory.FlatStyle = FlatStyle.Flat;
            buttonLoadFacilities.FlatAppearance.BorderSize = 0;
            buttonLoadFacilities.FlatStyle = FlatStyle.Flat;

            // Настройка кнопки "Сохранить"
            buttonValidateAndSave.FlatAppearance.BorderSize = 0;
            buttonValidateAndSave.FlatStyle = FlatStyle.Flat;

            buttonValidateAndSave_Click(sender, e);
        }

        // Загруженный пользователем значок для отображения объекта на карте
        private Bitmap _bitmapForLoadIconFacility;
        // Флаг успешной загрузки значка
        private bool _IsLoadIcon = false;
        /// <summary>
        /// Загрузка значка
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoadIconFacility_Click(object sender, EventArgs e)
        {
            // Обнулить флаг
            _IsLoadIcon = false;
            try
            {
                OpenFileDialog DialogLoadingIconFacility = new OpenFileDialog();
                // PNG — оптимален для изображений с небольшим количеством цветов, например, для иконок, схем, рисунков и скриншотов.
                // JPEG — это формат изображений, который использует сжатие с потерями и не поддерживает прозрачность.
                DialogLoadingIconFacility.Filter = "Files (*.PNG)|*.PNG;";
                DialogLoadingIconFacility.InitialDirectory = Application.StartupPath + @"\Icon\IconsCollection";
                DialogLoadingIconFacility.Title = "Выберите значок для отображения объекта на карте";

                if (DialogLoadingIconFacility.ShowDialog() == DialogResult.OK)
                {
                    // Получение загруженной картинки
                    _bitmapForLoadIconFacility = (Bitmap)Bitmap.FromFile(DialogLoadingIconFacility.FileName.ToString());
                    Image image = (Image)_bitmapForLoadIconFacility;

                    // Если разрешение файла от 5х5 до 90x90 пикселов 
                    if (_bitmapForLoadIconFacility.Size.Height >= 5 && _bitmapForLoadIconFacility.Size.Height <= 90 &&
                        _bitmapForLoadIconFacility.Size.Width >= 5 && _bitmapForLoadIconFacility.Size.Width <= 90)
                    {
                        // Если файл png формата
                        if (image.RawFormat.Equals(ImageFormat.Png))
                        {
                            // Замена значка для объекта на карте
                            _pathToIconFacility = DialogLoadingIconFacility.FileName.ToString();
                            // Отобразить значок в pictureBox
                            pictureIconFacility.Image = image;
                            // Картинка успешно загружена
                            _IsLoadIcon = true;
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

        // Флаг введенного названия
        private bool _IsLoadName = false;
        /// <summary>
        /// Проверка корректности названия
        /// </summary>
        private void CheckVadidateNameFaicility()
        {
            // Обнулить флаг
            _IsLoadName = false;

            // Если пустое поле
            if (!string.IsNullOrEmpty(textBoxNameFacility.Text) && !string.IsNullOrWhiteSpace(textBoxNameFacility.Text))
            {
                // Если длина строки больше 2 и меньше 30
                if (textBoxNameFacility.TextLength >= 2 && textBoxNameFacility.TextLength <= 20)
                {
                    // Название успешно задано
                    _IsLoadName = true;
                    // Сохранить название объекта
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
        /// <param name="text">Название критерия</param>
        /// <returns></returns>
        private bool CheckNameCriterion(string text)
        {
            // Поле не пустое
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text) && text != "Очистить")
            {
                // Название от 2 до 30 символов
                if (text.Length >= 2 && text.Length <= 30)
                    return true;
                else return false;
            }
            else return false;
        }

        /// <summary>
        ///  Проверка корректности направления
        /// </summary>
        /// <param name="text">Направление критерия</param>
        /// <returns></returns>
        private bool CheckDirectionCriterion(string text)
        {
            // Если поле не пустое
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text))
            {
                // Если в поле min или max
                if (text == "min" || text == "max")
                    return true;
                else return false;
            }
            else return false;
        }

        /// <summary>
        /// Проверка корректности весового коэффициента
        /// </summary>
        /// <param name="text">Весовой коэффициент</param>
        /// <returns></returns>
        private bool CheckWeightCriterion(string text)
        {
            // Если поле не пустое
            if (!string.IsNullOrEmpty(text) && !string.IsNullOrWhiteSpace(text))
            {
                double tempValue;
                bool testForResidents = double.TryParse(text, out tempValue);
                // Если строку можно преобразовать в число
                if (testForResidents)
                {
                    double weihgtResidents = Convert.ToDouble(text);
                    // Если вес в интервале [0;1]
                    if (weihgtResidents >= 0 && weihgtResidents <= 1)
                        return true;
                    else return false;
                }
                else return false;
            }
            else return false;
        }

        // Список критериев
        List<Criterion> criterion = new List<Criterion>();
        // Флаг загруженных критериев
        private bool _IsLoadCriterion = false;
        // Количество критериев из выпадающего списка
        int countCriterionOnComboBox = 0;
        /// <summary>
        /// Проверка заполненности критериев-направлений-весов
        /// </summary>
        /// <returns>Статус загрузки данных</returns>
        private string CheckValidateCriterion()
        {
            // Строка с ошибкой
            var errorCriterion = new StringBuilder();
            errorCriterion.AppendLine("В одном из критериев некорректные данные");
            errorCriterion.AppendLine("1. Название критерия должно быть от 2 до 30 символов");
            errorCriterion.AppendLine("2. У каждого критерия должно быть выбрано направление");
            errorCriterion.AppendLine("3. Весовой коэффициент должен быть число от 0 до 1");
            errorCriterion.AppendLine("4. Запрещено называть критерий \"Очистить\"");

            // Очистить список и флаг
            criterion.Clear();
            _IsLoadCriterion = false;

            // Если выбран 1 критерий
            if (countCriterionOnComboBox == 1)
            {
                // Проверка названия, направления и веса
                if (CheckNameCriterion(textBox1.Text) && CheckDirectionCriterion(comboBox2.Text) && CheckWeightCriterion(textBox4.Text))
                {
                    double weight_1 = Convert.ToDouble(textBox4.Text);
                    if (weight_1 == 1)
                    {
                        if (comboBox2.Text == "min")
                            criterion.Add(new Criterion(textBox1.Text, false, weight_1));
                        else
                            criterion.Add(new Criterion(textBox1.Text, true, weight_1));

                        // 1 критерий успешно задан
                        _IsLoadCriterion = true;
                        // Список критериев
                        _listCriteriaForSearch = criterion;
                        return "Успешно";
                    }
                    else
                        return "Сумма весов должна быть равна 1";
                }
                else
                    return errorCriterion.ToString();
            }
            // Если выбрано 2 критерия
            else if (countCriterionOnComboBox == 2)
            {
                // Проверка названия, направления и веса
                if (CheckNameCriterion(textBox1.Text) && CheckDirectionCriterion(comboBox2.Text) && CheckWeightCriterion(textBox4.Text) &&
                    CheckNameCriterion(textBox2.Text) && CheckDirectionCriterion(comboBox3.Text) && CheckWeightCriterion(textBox5.Text))
                {
                    double weight_1 = Convert.ToDouble(textBox4.Text);
                    double weight_2 = Convert.ToDouble(textBox5.Text);

                    if (weight_1 + weight_2 >= 0.998 &&
                      weight_1 + weight_2 <= 1)
                    {
                        if (comboBox2.Text == "min")
                            criterion.Add(new Criterion(textBox1.Text, false, weight_1));
                        else
                            criterion.Add(new Criterion(textBox1.Text, true, weight_1));

                        if (comboBox3.Text == "min")
                            criterion.Add(new Criterion(textBox2.Text, false, weight_2));
                        else
                            criterion.Add(new Criterion(textBox2.Text, true, weight_2));

                        // 2 критерия успешно задано
                        _IsLoadCriterion = true;
                        // Список критериев
                        _listCriteriaForSearch = criterion;
                        return "Успешно";
                    }
                    else
                        return "Сумма весов должна быть равна 1";
                }
                else
                    return errorCriterion.ToString();
            }

            // Если выбрано 3 критерия
            else if (countCriterionOnComboBox == 3)
            {
                // Проверка названия, направления и веса
                if (CheckNameCriterion(textBox1.Text) && CheckDirectionCriterion(comboBox2.Text) && CheckWeightCriterion(textBox4.Text) &&
                    CheckNameCriterion(textBox2.Text) && CheckDirectionCriterion(comboBox3.Text) && CheckWeightCriterion(textBox5.Text) &&
                    CheckNameCriterion(textBox3.Text) && CheckDirectionCriterion(comboBox4.Text) && CheckWeightCriterion(textBox6.Text))
                {
                    double weight_1 = Convert.ToDouble(textBox4.Text);
                    double weight_2 = Convert.ToDouble(textBox5.Text);
                    double weight_3 = Convert.ToDouble(textBox6.Text);

                    if (weight_1 + weight_2 + weight_3 >= 0.998 &&
                       weight_1 + weight_2 + weight_3 <= 1)
                    {
                        if (comboBox2.Text == "min")
                            criterion.Add(new Criterion(textBox1.Text, false, weight_1));
                        else
                            criterion.Add(new Criterion(textBox1.Text, true, weight_1));

                        if (comboBox3.Text == "min")
                            criterion.Add(new Criterion(textBox2.Text, false, weight_2));
                        else
                            criterion.Add(new Criterion(textBox2.Text, true, weight_2));

                        if (comboBox4.Text == "min")
                            criterion.Add(new Criterion(textBox3.Text, false, weight_3));
                        else
                            criterion.Add(new Criterion(textBox3.Text, true, weight_3));

                        // 3 критерия успешно задано
                        _IsLoadCriterion = true;
                        // Список критериев
                        _listCriteriaForSearch = criterion;
                        return "Успешно";
                    }
                    else
                        return "Сумма весов должна быть равна 1";
                }
                else
                    return errorCriterion.ToString();
            }
            // Если выбрано 4 критерия
            else if (countCriterionOnComboBox == 4)
            {
                // Проверка названия, направления и веса
                if (CheckNameCriterion(textBox1.Text) && CheckDirectionCriterion(comboBox2.Text) && CheckWeightCriterion(textBox4.Text) &&
                    CheckNameCriterion(textBox2.Text) && CheckDirectionCriterion(comboBox3.Text) && CheckWeightCriterion(textBox5.Text) &&
                    CheckNameCriterion(textBox3.Text) && CheckDirectionCriterion(comboBox4.Text) && CheckWeightCriterion(textBox6.Text) &&
                    CheckNameCriterion(textBox7.Text) && CheckDirectionCriterion(comboBox5.Text) && CheckWeightCriterion(textBox11.Text))
                {
                    double weight_1 = Convert.ToDouble(textBox4.Text);
                    double weight_2 = Convert.ToDouble(textBox5.Text);
                    double weight_3 = Convert.ToDouble(textBox6.Text);
                    double weight_4 = Convert.ToDouble(textBox11.Text);

                    if (weight_1 + weight_2 + weight_3 + weight_4 >= 0.998 &&
                       weight_1 + weight_2 + weight_3 + weight_4 <= 1)
                    {
                        if (comboBox2.Text == "min")
                            criterion.Add(new Criterion(textBox1.Text, false, weight_1));
                        else
                            criterion.Add(new Criterion(textBox1.Text, true, weight_1));

                        if (comboBox3.Text == "min")
                            criterion.Add(new Criterion(textBox2.Text, false, weight_2));
                        else
                            criterion.Add(new Criterion(textBox2.Text, true, weight_2));

                        if (comboBox4.Text == "min")
                            criterion.Add(new Criterion(textBox3.Text, false, weight_3));
                        else
                            criterion.Add(new Criterion(textBox3.Text, true, weight_3));

                        if (comboBox5.Text == "min")
                            criterion.Add(new Criterion(textBox7.Text, false, weight_4));
                        else
                            criterion.Add(new Criterion(textBox7.Text, true, weight_4));

                        // 4 критерия успешно задано
                        _IsLoadCriterion = true;
                        // Список критериев
                        _listCriteriaForSearch = criterion;
                        return "Успешно";
                    }
                    else
                        return "Сумма весов должна быть равна 1";
                }
                else
                    return errorCriterion.ToString();
            }
            // Если выбрано 5 критериев
            else if (countCriterionOnComboBox == 5)
            {
                // Проверка названия, направления и веса
                if (CheckNameCriterion(textBox1.Text) && CheckDirectionCriterion(comboBox2.Text) && CheckWeightCriterion(textBox4.Text) &&
                    CheckNameCriterion(textBox2.Text) && CheckDirectionCriterion(comboBox3.Text) && CheckWeightCriterion(textBox5.Text) &&
                    CheckNameCriterion(textBox3.Text) && CheckDirectionCriterion(comboBox4.Text) && CheckWeightCriterion(textBox6.Text) &&
                    CheckNameCriterion(textBox7.Text) && CheckDirectionCriterion(comboBox5.Text) && CheckWeightCriterion(textBox11.Text) &&
                    CheckNameCriterion(textBox10.Text) && CheckDirectionCriterion(comboBox6.Text) && CheckWeightCriterion(textBox9.Text))
                {
                    double weight_1 = Convert.ToDouble(textBox4.Text);
                    double weight_2 = Convert.ToDouble(textBox5.Text);
                    double weight_3 = Convert.ToDouble(textBox6.Text);
                    double weight_4 = Convert.ToDouble(textBox11.Text);
                    double weight_5 = Convert.ToDouble(textBox9.Text);

                    if (weight_1 + weight_2 + weight_3 + weight_4 + weight_5 >= 0.998 &&
                        weight_1 + weight_2 + weight_3 + weight_4 + weight_5 <= 1)
                    {
                        if (comboBox2.Text == "min")
                            criterion.Add(new Criterion(textBox1.Text, false, weight_1));
                        else
                            criterion.Add(new Criterion(textBox1.Text, true, weight_1));

                        if (comboBox3.Text == "min")
                            criterion.Add(new Criterion(textBox2.Text, false, weight_2));
                        else
                            criterion.Add(new Criterion(textBox2.Text, true, weight_2));

                        if (comboBox4.Text == "min")
                            criterion.Add(new Criterion(textBox3.Text, false, weight_3));
                        else
                            criterion.Add(new Criterion(textBox3.Text, true, weight_3));

                        if (comboBox5.Text == "min")
                            criterion.Add(new Criterion(textBox7.Text, false, weight_4));
                        else
                            criterion.Add(new Criterion(textBox7.Text, true, weight_4));
                        if (comboBox6.Text == "min")
                            criterion.Add(new Criterion(textBox10.Text, false, weight_5));
                        else
                            criterion.Add(new Criterion(textBox10.Text, true, weight_5));

                        // 5 критериев успешно задано
                        _IsLoadCriterion = true;
                        // Список критериев
                        _listCriteriaForSearch = criterion;
                        return "Успешно";
                    }
                    else
                        return "Сумма весов должна быть равна 1";
                }
                else
                    return errorCriterion.ToString();
            }
            // Если выбрано 6 критериев
            else if (countCriterionOnComboBox == 6)
            {
                // Проверка названия, направления и веса
                if (CheckNameCriterion(textBox1.Text) && CheckDirectionCriterion(comboBox2.Text) && CheckWeightCriterion(textBox4.Text) &&
                    CheckNameCriterion(textBox2.Text) && CheckDirectionCriterion(comboBox3.Text) && CheckWeightCriterion(textBox5.Text) &&
                    CheckNameCriterion(textBox3.Text) && CheckDirectionCriterion(comboBox4.Text) && CheckWeightCriterion(textBox6.Text) &&
                    CheckNameCriterion(textBox7.Text) && CheckDirectionCriterion(comboBox5.Text) && CheckWeightCriterion(textBox11.Text) &&
                    CheckNameCriterion(textBox10.Text) && CheckDirectionCriterion(comboBox6.Text) && CheckWeightCriterion(textBox9.Text) &&
                    CheckNameCriterion(textBox12.Text) && CheckDirectionCriterion(comboBox7.Text) && CheckWeightCriterion(textBox8.Text))
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
                        if (comboBox2.Text == "min")
                            criterion.Add(new Criterion(textBox1.Text, false, weight_1));
                        else
                            criterion.Add(new Criterion(textBox1.Text, true, weight_1));

                        if (comboBox3.Text == "min")
                            criterion.Add(new Criterion(textBox2.Text, false, weight_2));
                        else
                            criterion.Add(new Criterion(textBox2.Text, true, weight_2));

                        if (comboBox4.Text == "min")
                            criterion.Add(new Criterion(textBox3.Text, false, weight_3));
                        else
                            criterion.Add(new Criterion(textBox3.Text, true, weight_3));

                        if (comboBox5.Text == "min")
                            criterion.Add(new Criterion(textBox7.Text, false, weight_4));
                        else
                            criterion.Add(new Criterion(textBox7.Text, true, weight_4));
                        if (comboBox6.Text == "min")
                            criterion.Add(new Criterion(textBox10.Text, false, weight_5));
                        else
                            criterion.Add(new Criterion(textBox10.Text, true, weight_5));
                        if (comboBox7.Text == "min")
                            criterion.Add(new Criterion(textBox12.Text, false, weight_6));
                        else
                            criterion.Add(new Criterion(textBox12.Text, true, weight_6));

                        // 6 критериев успешно задано
                        _IsLoadCriterion = true;
                        // Список критериев
                        _listCriteriaForSearch = criterion;
                        return "Успешно";
                    }
                    else
                        return "Сумма весов должна быть равна 1";
                }
                else
                    return errorCriterion.ToString();
            }
            // Если выбрано 7 критериев
            else if (countCriterionOnComboBox == 7)
            {
                // Проверка названия, направления и веса
                if (CheckNameCriterion(textBox1.Text) && CheckDirectionCriterion(comboBox2.Text) && CheckWeightCriterion(textBox4.Text) &&
                    CheckNameCriterion(textBox2.Text) && CheckDirectionCriterion(comboBox3.Text) && CheckWeightCriterion(textBox5.Text) &&
                    CheckNameCriterion(textBox3.Text) && CheckDirectionCriterion(comboBox4.Text) && CheckWeightCriterion(textBox6.Text) &&
                    CheckNameCriterion(textBox7.Text) && CheckDirectionCriterion(comboBox5.Text) && CheckWeightCriterion(textBox11.Text) &&
                    CheckNameCriterion(textBox10.Text) && CheckDirectionCriterion(comboBox6.Text) && CheckWeightCriterion(textBox9.Text) &&
                    CheckNameCriterion(textBox12.Text) && CheckDirectionCriterion(comboBox7.Text) && CheckWeightCriterion(textBox8.Text) &&
                    CheckNameCriterion(textBox14.Text) && CheckDirectionCriterion(comboBox8.Text) && CheckWeightCriterion(textBox13.Text))
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
                        if (comboBox2.Text == "min")
                            criterion.Add(new Criterion(textBox1.Text, false, weight_1));
                        else
                            criterion.Add(new Criterion(textBox1.Text, true, weight_1));

                        if (comboBox3.Text == "min")
                            criterion.Add(new Criterion(textBox2.Text, false, weight_2));
                        else
                            criterion.Add(new Criterion(textBox2.Text, true, weight_2));

                        if (comboBox4.Text == "min")
                            criterion.Add(new Criterion(textBox3.Text, false, weight_3));
                        else
                            criterion.Add(new Criterion(textBox3.Text, true, weight_3));

                        if (comboBox5.Text == "min")
                            criterion.Add(new Criterion(textBox7.Text, false, weight_4));
                        else
                            criterion.Add(new Criterion(textBox7.Text, true, weight_4));
                        if (comboBox6.Text == "min")
                            criterion.Add(new Criterion(textBox10.Text, false, weight_5));
                        else
                            criterion.Add(new Criterion(textBox10.Text, true, weight_5));
                        if (comboBox7.Text == "min")
                            criterion.Add(new Criterion(textBox12.Text, false, weight_6));
                        else
                            criterion.Add(new Criterion(textBox12.Text, true, weight_6));
                        if (comboBox8.Text == "min")
                            criterion.Add(new Criterion(textBox14.Text, false, weight_7));
                        else
                            criterion.Add(new Criterion(textBox14.Text, true, weight_7));

                        // 7 критериев успешно задано
                        _IsLoadCriterion = true;
                        // Список критериев
                        _listCriteriaForSearch = criterion;
                        return "Успешно";
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

        // Флаг загрузки файла с границами территории
        private bool _IsLoadFileBorderTerritory = false;
        /// <summary>
        /// Загрузка файла с границами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoadBorderTerritory_Click(object sender, EventArgs e)
        {
            // Обнулить флаг
            _IsLoadFileBorderTerritory = false;

            // Если файл открыт в данный момент
            try
            {
                OpenFileDialog OpenDialogLoadFileBorder = new OpenFileDialog();
                OpenDialogLoadFileBorder.Filter = "Files (*.csv)|*.csv;";
                OpenDialogLoadFileBorder.InitialDirectory = Application.StartupPath + @"\Data";
                OpenDialogLoadFileBorder.Title = "Выберите CSV-файл с граничными точками территории";

                if (OpenDialogLoadFileBorder.ShowDialog() == DialogResult.OK)
                {
                    string pathToFile = OpenDialogLoadFileBorder.FileName.ToString();
                    string isValidFile = _fileValidator.FileUserValidation(pathToFile);

                    // Если файл прошёл все проверки
                    if (isValidFile == "Успешно")
                    {
                        // Валидация хранения данных в файле
                        string isValidDataFile = _fileValidator.DataValidationUserBorder(pathToFile);

                        // Если файл прошёл все проверки
                        if (isValidDataFile == "Успешно")
                        {
                            // Список для чтения координат
                            List<Location> _tempListPoints = new List<Location>();
                            using (StreamReader filereader = new StreamReader(pathToFile, Encoding.GetEncoding(1251)))
                            {
                                while (!filereader.EndOfStream)
                                {
                                    string OneString = filereader.ReadLine();
                                    string[] SplitOneString = OneString.Split(new char[] { ';' });
                                    _tempListPoints.Add(new Location(Convert.ToDouble(SplitOneString[0], _cultureInfo),
                                        Convert.ToDouble(SplitOneString[1], _cultureInfo)));
                                }
                            }
                            // Сохранить список граничных точек
                            _listPointsBorderTerritory = _tempListPoints;
                            // Территория граничная загружена
                            _IsLoadFileBorderTerritory = true;

                            // При загрузке новых границ сбросить файлы с полигонами и объектами инфраструктуры
                            _IsLoadFileFacilities = false;
                            _IsLoadFileQuartets = false;

                            //for (int i = 0; i < listPointsCity.Count; i++)
                            //    MessageBox.Show(listPointsCity[i].x + " " + listPointsCity[i].y);
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
                MessageBox.Show("Закройте файл, из которого должны загрузиться данные", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        // Массив строк с информацией о каждом объекте инфраструктуры
        List<string> infoFacility = new List<string>();
        // Флаг загрузки объектов инфраструктуры
        private bool _IsLoadFileFacilities = false;
        /// <summary>
        /// Загрузка файла с объектами инфраструктуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoadFacilities_Click(object sender, EventArgs e)
        {
            _listFacilitiesFromFile = new List<Facility>();
            // Если загружены границы территории
            if (_IsLoadFileBorderTerritory)
            {
                // Обнулить флаг
                _IsLoadFileFacilities = false;

                // Если файл открыт в данный момент
                try
                {
                    OpenFileDialog OpenDialogLoadFileFacility = new OpenFileDialog();
                    OpenDialogLoadFileFacility.Filter = "Files (*.csv)|*.csv;";
                    OpenDialogLoadFileFacility.InitialDirectory = Application.StartupPath + @"\Data";
                    OpenDialogLoadFileFacility.Title = "Выберите CSV-файл для загрузки данных об объектах инфраструктуры";

                    if (OpenDialogLoadFileFacility.ShowDialog() == DialogResult.OK)
                    {
                        string pathToFile = OpenDialogLoadFileFacility.FileName.ToString();
                        string isValidFile = _fileValidator.FileUserValidation(pathToFile);

                        // Если файл прошёл все проверки
                        if (isValidFile == "Успешно")
                        {
                            // Валидация хранения данных в файле - путь к фалу с объектами инфраструктуры и граничные точки
                            string isValidDataFile = _fileValidator.DataValidationUserFacility(pathToFile, _listPointsBorderTerritory);

                            // Если файл прошёл все проверки
                            if (isValidDataFile == "Успешно")
                            {
                                // Список с объектами инфраструктуры
                                List<Facility> _tempListPointsFacility = new List<Facility>();

                                _listFacilitiesFromFile.Clear();

                                using (StreamReader filereader = new StreamReader(pathToFile, Encoding.GetEncoding(1251)))
                                {
                                    while (!filereader.EndOfStream)
                                    {
                                        string OneString = filereader.ReadLine();
                                        string[] SplitOneString = OneString.Split(new char[] { ';' });
                                        infoFacility = new List<string>();
                                        for (int i = 3; i < SplitOneString.Length; i++)
                                        {
                                            infoFacility.Add(SplitOneString[i].ToString());
                                        }

                                        // Добавить в список объект инфраструктуры - ID, x, y, данные о нем
                                        _tempListPointsFacility.Add(new Facility(Convert.ToInt32(SplitOneString[0]),
                                            Convert.ToDouble(SplitOneString[1], _cultureInfo), Convert.ToDouble(SplitOneString[2], _cultureInfo), infoFacility));
                                    }
                                    // Сохранить список с объектами инфраструктуры
                                    _listFacilitiesFromFile = _tempListPointsFacility;
                                    // Флаг успешной загрузки файла
                                    _IsLoadFileFacilities = true;
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
                    MessageBox.Show("Закройте файл, из которого должны загрузиться данные", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
                MessageBox.Show("Сначала загрузите файл с граничными точками территории", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        // Флаг загрузки полигонов
        private bool _IsLoadFileQuartets = false;
        /// <summary>
        /// Загрузка файла с полигонами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonLoadQuartets_Click(object sender, EventArgs e)
        {
            // Если не загружены границы
            if (_IsLoadFileBorderTerritory)
            {
                // Проверить загруженные критерии
                string resultValidateCriterion = CheckValidateCriterion();
                if (_IsLoadCriterion)
                {
                    // Обнулить флаг
                    _IsLoadFileQuartets = false;

                    // Если файл открыт в данный момент
                    try
                    {
                        OpenFileDialog OpenDialogLoadFileQuartet = new OpenFileDialog();
                        OpenDialogLoadFileQuartet.Filter = "Files (*.csv)|*.csv;";
                        OpenDialogLoadFileQuartet.InitialDirectory = Application.StartupPath + @"\Data";
                        OpenDialogLoadFileQuartet.Title = "Выберите CSV-файл для загрузки данных о полигонах";

                        if (OpenDialogLoadFileQuartet.ShowDialog() == DialogResult.OK)
                        {
                            string pathToFile = OpenDialogLoadFileQuartet.FileName.ToString();
                            string isValidFile = _fileValidator.FileUserValidation(pathToFile);

                            // Если файл прошёл все проверки
                            if (isValidFile == "Успешно")
                            {
                                // Валидация хранения данных в файле - путь к файлу, граничные точки территории, количество критериев у каждого полигона
                                string isValidDataFile = _fileValidator.DataValidationUserQuar(pathToFile, _listPointsBorderTerritory, criterion.Count);

                                // Если файл прошёл все проверки
                                if (isValidDataFile == "Успешно")
                                {
                                    // Список кварталов
                                    List<Quar> _tempListQuartets = new List<Quar>();
                                    // Список точек для каждого полигона
                                    List<Location> tempListPoints = new List<Location>();

                                    using (StreamReader readerCsvFile = new StreamReader(pathToFile, Encoding.GetEncoding(1251)))
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
                                                    tempListPoints.Add(new Location(Convert.ToDouble(SplitOneString[j]), Convert.ToDouble(SplitOneString[j + 1])));
                                                    j += 2;
                                                }

                                                // Координаты центральной точки района
                                                double CentreX = Convert.ToDouble(SplitOneString[CountBoundaryPoints * 2 + 2]);
                                                double CentreY = Convert.ToDouble(SplitOneString[CountBoundaryPoints * 2 + 3]);

                                                // Список чисел значений каждого критерия у каждого полигона
                                                List<double> countValuesOfEveryCriterion = new List<double>();

                                                // SplitOneString[0] = ID
                                                // SplitOneString[1] = количество точек
                                                // SplitOneString[CountBoundaryPoints * 2] = граничные точки
                                                // SplitOneString[CountBoundaryPoints * 2 + 2] = х центра
                                                // SplitOneString[CountBoundaryPoints * 2 + 3] = у центра
                                                // SplitOneString[CountBoundaryPoints * 2 + 4] = 1 критерий
                                                // fffff = последний критерий

                                                // ID, CountBoundaryPoints, CountBoundaryPoints*2, xcenter, ycenter
                                                int _lastCriterion = CountBoundaryPoints * 2 + 4 + criterion.Count;

                                                // Все значения критериев полигона добавить в список
                                                for (int j = CountBoundaryPoints * 2 + 4; j < _lastCriterion; j++)
                                                    countValuesOfEveryCriterion.Add(Convert.ToDouble(SplitOneString[j]));

                                                // Если все критерии больше или равны 0
                                                bool flag = true;
                                                for (int k = 0; k < countValuesOfEveryCriterion.Count; k++)
                                                {
                                                    if (countValuesOfEveryCriterion[k] < 0)
                                                        flag = false;
                                                }
                                                // Если все критерии >= 0
                                                if (flag)
                                                    // Добавить полигон в список
                                                    _tempListQuartets.Add(new Quar(ID, CountBoundaryPoints, tempListPoints, CentreX, CentreY, countValuesOfEveryCriterion));
                                            }
                                        }
                                    }

                                    // Сохранить список полигонов
                                    _listQuartetsFromFile = _tempListQuartets;
                                    // Флаг успешной загрузки файла
                                    _IsLoadFileQuartets = true;
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
                        MessageBox.Show("Закройте файл, из которого должны загрузиться данные", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                    MessageBox.Show(resultValidateCriterion, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
                MessageBox.Show("Сначала загрузите файл с граничными точками территории", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public void ffunc(bool fdsafsdafsdf)
        {
            if (fdsafsdafsdf)
            {
                _pathToIconFacility = Application.StartupPath + @"\Icon\iconOld.png";
                string pathToFile1 = Application.StartupPath + @"\Data\!!!dataBorderCityForUser.csv";
                string pathToFile2 = Application.StartupPath + @"\Data\!!dataQuartet.csv";
                string pathToFile3 = Application.StartupPath + @"\Data\!dataPharmacy.csv";
                // string pathToFile3 = Application.StartupPath + @"\Data\3123123.csv";

                CheckVadidateNameFaicility();
                CheckValidateCriterion();

                // Список для чтения координат
                List<Location> _tempListPoints = new List<Location>();
                using (StreamReader filereader = new StreamReader(pathToFile1, Encoding.GetEncoding(1251)))
                {
                    while (!filereader.EndOfStream)
                    {
                        string OneString = filereader.ReadLine();
                        string[] SplitOneString = OneString.Split(new char[] { ';' });
                        _tempListPoints.Add(new Location(Convert.ToDouble(SplitOneString[0], _cultureInfo),
                            Convert.ToDouble(SplitOneString[1], _cultureInfo)));
                    }
                }
                // Сохранить список граничных точек
                _listPointsBorderTerritory = _tempListPoints;
                // Территория граничная загружена
                _IsLoadFileBorderTerritory = true;

                // При загрузке новых границ сбросить файлы с полигонами и объектами инфраструктуры
                _IsLoadFileFacilities = false;
                _IsLoadFileQuartets = false;









                // Список кварталов
                List<Quar> _tempListQuartets = new List<Quar>();
                // Список точек для каждого полигона
                List<Location> tempListPoints = new List<Location>();

                using (StreamReader readerCsvFile = new StreamReader(pathToFile2, Encoding.GetEncoding(1251)))
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
                                tempListPoints.Add(new Location(Convert.ToDouble(SplitOneString[j]), Convert.ToDouble(SplitOneString[j + 1])));
                                j += 2;
                            }

                            // Координаты центральной точки района
                            double CentreX = Convert.ToDouble(SplitOneString[CountBoundaryPoints * 2 + 2]);
                            double CentreY = Convert.ToDouble(SplitOneString[CountBoundaryPoints * 2 + 3]);

                            // Список чисел значений каждого критерия у каждого полигона
                            List<double> countValuesOfEveryCriterion = new List<double>();

                            // SplitOneString[0] = ID
                            // SplitOneString[1] = количество точек
                            // SplitOneString[CountBoundaryPoints * 2] = граничные точки
                            // SplitOneString[CountBoundaryPoints * 2 + 2] = х центра
                            // SplitOneString[CountBoundaryPoints * 2 + 3] = у центра
                            // SplitOneString[CountBoundaryPoints * 2 + 4] = 1 критерий
                            // fffff = последний критерий

                            // ID, CountBoundaryPoints, CountBoundaryPoints*2, xcenter, ycenter
                            int _lastCriterion = CountBoundaryPoints * 2 + 4 + criterion.Count;

                            //MessageBox.Show(criterion.Count.ToString());

                            // Все значения критериев полигона добавить в список
                            for (int j = CountBoundaryPoints * 2 + 4; j < _lastCriterion; j++)
                                countValuesOfEveryCriterion.Add(Convert.ToDouble(SplitOneString[j]));

                            // Если все критерии больше или равны 0
                            bool flag = true;
                            for (int k = 0; k < countValuesOfEveryCriterion.Count; k++)
                            {
                                if (countValuesOfEveryCriterion[k] < 0)
                                    flag = false;
                            }
                            // Если все критерии >= 0
                            if (flag)
                                // Добавить полигон в список
                                _tempListQuartets.Add(new Quar(ID, CountBoundaryPoints, tempListPoints, CentreX, CentreY, countValuesOfEveryCriterion));
                        }
                    }
                }

                // Сохранить список полигонов
                _listQuartetsFromFile = _tempListQuartets;

                //for (int i = 0; i < _listQuartetsFromFile.Count; i++)
                //{
                //    MessageBox.Show(_listQuartetsFromFile[i].valuesEveryCriterionForQuartet[0].ToString());
                //    MessageBox.Show(_listQuartetsFromFile[i].valuesEveryCriterionForQuartet[1].ToString());
                //    MessageBox.Show(_listQuartetsFromFile[i].valuesEveryCriterionForQuartet[2].ToString());

                //}

                // Флаг успешной загрузки файла
                _IsLoadFileQuartets = true;










                // Список с объектами инфраструктуры
                List<Facility> _tempListPointsFacility = new List<Facility>();


                using (StreamReader filereader = new StreamReader(pathToFile3, Encoding.GetEncoding(1251)))
                {
                    while (!filereader.EndOfStream)
                    {
                        string OneString = filereader.ReadLine();
                        string[] SplitOneString = OneString.Split(new char[] { ';' });
                        infoFacility = new List<string>();
                        for (int i = 3; i < SplitOneString.Length; i++)
                        {
                            infoFacility.Add(SplitOneString[i].ToString());
                        }

                        // Добавить в список объект инфраструктуры - ID, x, y, данные о нем
                        _tempListPointsFacility.Add(new Facility(Convert.ToInt32(SplitOneString[0]),
                            Convert.ToDouble(SplitOneString[1], _cultureInfo), Convert.ToDouble(SplitOneString[2], _cultureInfo), infoFacility));
                    }
                    // Сохранить список с объектами инфраструктуры
                    _listFacilitiesFromFile = _tempListPointsFacility;
                    // Флаг успешной загрузки файла
                    _IsLoadFileFacilities = true;
                }

                _locationCenterMap = FindCenterPoint();

                Hide();
                MainMap mainForm = new MainMap(_languageOfMap, _colorForApplication, _colorForElem,
                    _pathToIconFacility, _nameFacility, _listCriteriaForSearch,
                    _listQuartetsFromFile, _listFacilitiesFromFile, _listPointsBorderTerritory, _locationCenterMap);
                mainForm.ShowDialog();
                Close();
            }
            else
            {
                // Проверка загруженного значка
                if (_IsLoadIcon)
                {
                    // Проверить название объекта
                    CheckVadidateNameFaicility();
                    // Проверка введенного названия объекта
                    if (_IsLoadName)
                    {
                        string resultRead = CheckValidateCriterion();
                        // Проверка заданных критерий-направление-вес
                        if (resultRead == "Успешно" && _IsLoadCriterion)
                        {
                            // Проверить загруженный файл с границами территории
                            if (_IsLoadFileBorderTerritory)
                            {
                                // Проверить загруженный файл с полигонами
                                if (_IsLoadFileQuartets)
                                {
                                    // Проверить загруженный файл с объектами инфраструктуры
                                    if (_IsLoadFileFacilities)
                                    {
                                        _locationCenterMap = FindCenterPoint();

                                        Hide();
                                        // Открыть Главное окно, передав туда
                                        // - локализация карты
                                        // - цвет интерфейса 
                                        // - значок объекта на карте
                                        // - название объекта инфраструктуры
                                        // - список критериев
                                        // - список полигонов
                                        // - список объектов инфраструктуры
                                        // - список граничных точек территории
                                        MainMap mainForm = new MainMap(_languageOfMap, _colorForApplication, _colorForElem,
                                            _pathToIconFacility, _nameFacility, _listCriteriaForSearch,
                                            _listQuartetsFromFile, _listFacilitiesFromFile, _listPointsBorderTerritory, _locationCenterMap);
                                        mainForm.ShowDialog();
                                        Close();
                                    }
                                    else
                                        MessageBox.Show("Не загружен файл с объектами инфраструктуры", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                    MessageBox.Show("Не загружен файл с полигонами", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                                MessageBox.Show("Не загружен файл с граничными точками территории", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                            MessageBox.Show(resultRead, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                    MessageBox.Show("Необходимо загрузить значок", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Кнопка "Сохранить"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonValidateAndSave_Click(object sender, EventArgs e)
        {
            // true - автоматом, false - вручную
            //ffunc(false);
            ffunc(true);
        }

        /// <summary>
        /// Найти точку центрирования карты
        /// </summary>
        private Location FindCenterPoint()
        {
            double xSumm = 0, ySumm = 0;
            for (int i = 0; i < _listQuartetsFromFile.Count; i++)
            {
                xSumm = xSumm + _listQuartetsFromFile[i].xCentreOfQuartet;
                ySumm = ySumm + _listQuartetsFromFile[i].yCentreOfQuartet;
            }
            Location _location = new Location(xSumm / _listQuartetsFromFile.Count, ySumm / _listQuartetsFromFile.Count);
            return _location;
        }

        /// <summary>
        /// Закрытие формы на ESC
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Settings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }

        /// <summary>
        /// Обработка выбранного числа в выпадающем списке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comboCountCriterion_SelectedIndexChanged(object sender, EventArgs e)
        {
            // При изменении критериев пропадает загруженный файл с полигонами
            countCriterionOnComboBox = Convert.ToInt32(comboCountCriterion.Text);
            if (countCriterionOnComboBox == 1)
            {
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox2.Visible = comboBox3.Visible = textBox5.Visible = false;
                textBox3.Visible = comboBox4.Visible = textBox6.Visible = false;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = false;
                textBox10.Visible = comboBox6.Visible = textBox9.Visible = false;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = false;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = false;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 144);
                _IsLoadFileQuartets = false;
            }
            else if (countCriterionOnComboBox == 2)
            {
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox3.Visible = comboBox4.Visible = textBox6.Visible = false;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = false;
                textBox10.Visible = comboBox6.Visible = textBox9.Visible = false;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = false;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = false;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 179);
                _IsLoadFileQuartets = false;
            }
            else if (countCriterionOnComboBox == 3)
            {
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox3.Visible = comboBox4.Visible = textBox6.Visible = true;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = false;
                textBox10.Visible = comboBox6.Visible = textBox9.Visible = false;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = false;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 214);
                _IsLoadFileQuartets = false;
            }
            else if (countCriterionOnComboBox == 4)
            {
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox3.Visible = comboBox4.Visible = textBox6.Visible = true;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = true;
                textBox10.Visible = comboBox6.Visible = textBox9.Visible = false;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = false;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 254);
                _IsLoadFileQuartets = false;
            }
            else if (countCriterionOnComboBox == 5)
            {
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox3.Visible = comboBox4.Visible = textBox6.Visible = true;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = true;
                textBox10.Visible = comboBox6.Visible = textBox9.Visible = true;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = false;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 287);
                _IsLoadFileQuartets = false;
            }
            else if (countCriterionOnComboBox == 6)
            {
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox3.Visible = comboBox4.Visible = textBox6.Visible = true;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = true;
                textBox10.Visible = comboBox6.Visible = textBox9.Visible = true;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = true;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = false;
                groupBoxCriterion.Size = new Size(504, 323);
                _IsLoadFileQuartets = false;
            }
            else
            {
                textBox1.Visible = comboBox2.Visible = textBox4.Visible = true;
                textBox2.Visible = comboBox3.Visible = textBox5.Visible = true;
                textBox3.Visible = comboBox4.Visible = textBox6.Visible = true;
                textBox7.Visible = comboBox5.Visible = textBox11.Visible = true;
                textBox10.Visible = comboBox6.Visible = textBox9.Visible = true;
                textBox12.Visible = comboBox7.Visible = textBox8.Visible = true;
                textBox14.Visible = comboBox8.Visible = textBox13.Visible = true;
                groupBoxCriterion.Size = new Size(504, 358);
                _IsLoadFileQuartets = false;
            }
        }

        /// <summary>
        /// Открыть HTML, как загрузить значок
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHelpSetIcon_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\Data\Manual\index.html");
            }
            catch
            {
                MessageBox.Show("Файл не найден", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Открыть HTML, как задать название объекта инфраструктуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHelpSetName_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\Data\Manual\index.html");
            }
            catch
            {
                MessageBox.Show("Файл не найден", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Открыть HTML, как задать критерии оптимальности
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHelpSetCriterion_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\Data\Manual\index.html");
            }
            catch
            {
                MessageBox.Show("Файл не найден", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Открыть HTML, как загрузить файл с граничными точками территории
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHelpSetFileBoundary_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\Data\Manual\index.html");
            }
            catch
            {
                MessageBox.Show("Файл не найден", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Открыть HTML, как загрузить файл с полигонами
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHelpSetFilePolygons_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\Data\Manual\index.html");
            }
            catch
            {
                MessageBox.Show("Файл не найден", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Открыть HTML, как загрузить файл с объектами инфраструктуры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonHelpSetFileSocialFacilities_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\Data\Manual\index.html");
            }
            catch
            {
                MessageBox.Show("Файл не найден", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}