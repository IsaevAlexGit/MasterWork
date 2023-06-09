﻿using System;
using System.Drawing;
using System.Windows.Forms;
using GMap.NET;

namespace Optimum
{
    public partial class StartForm : Form
    {
        // Локализация карты
        private LanguageType _languageOfMap;
        private string _selectedMapLanguage;
        // Цвета интерфейса по умолчанию
        private Color _mainColor = Color.FromArgb(138, 230, 145);
        private Color _secondaryColor = Color.FromArgb(174, 238, 180);
        // Состояние Интернета
        private bool _flagConnection = false;

        /// <summary>
        /// Конструктор формы
        /// </summary>
        public StartForm()
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;

            // Расширеный выбор цвета и оттенков для изменения цвета интерфейса
            colorDialog.FullOpen = true;
            colorDialog.Color = BackColor;

            // Старт таймера
            timer.Enabled = true;
            timer.Start();
            timer.Interval = 1000;
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        private void StartForm_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(810, 487);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            // Цвет интерфейса
            BackColor = comboBoxLanguage.BackColor = buttonManual.BackColor = buttonSelectColor.BackColor = _mainColor;
            // Цвет "Продолжить"
            buttonProceed.BackColor = _secondaryColor;

            // Установка картинки
            pictureLogoApplication.Image = Properties.Resources.iconApplication;
            // Выравнивание картинки по центру
            pictureLogoApplication.Left = (ClientSize.Width - pictureLogoApplication.Width) / 2;
            // Визуализация кнопки "Руководство пользователя"
            buttonManual.FlatAppearance.BorderSize = 0;
            buttonManual.FlatStyle = FlatStyle.Flat;
            // Визуализация кнопки "Цвет интерфейса"
            buttonSelectColor.FlatAppearance.BorderSize = 0;
            buttonSelectColor.FlatStyle = FlatStyle.Flat;
            // Визуализация кнопки "Продолжить"
            buttonProceed.FlatAppearance.BorderSize = 0;
            buttonProceed.FlatStyle = FlatStyle.Flat;
            // Выровнить текст на кнопке по центру кнопки
            buttonProceed.TextAlign = ContentAlignment.MiddleCenter;
            // Настройка выпадающего списка
            comboBoxLanguage.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxLanguage.SelectedItem = "Русская";
        }

        /// <summary>
        /// Установка цвета для интерфейса
        /// </summary>
        private void buttonSelectColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                // Если задали черный цвет или очень близкий к нему
                if (colorDialog.Color.R <= 40 && colorDialog.Color.G <= 40 && colorDialog.Color.B <= 40)
                    MessageBox.Show("Нельзя выбрать черный цвет", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    // Сохранение выбранного цвета
                    _mainColor = colorDialog.Color;

                    // Установить цвет для данного окна и кнопок
                    BackColor = comboBoxLanguage.BackColor = buttonManual.BackColor = buttonSelectColor.BackColor = _mainColor;

                    // Найти оттенок выбранного цвета
                    int R = BackColor.R + 15;
                    if (R < 0 || R > 255)
                        R = BackColor.R - 15;
                    int G = BackColor.G + 15;
                    if (G < 0 || G > 255)
                        G = BackColor.G - 15;
                    int B = BackColor.B + 15;
                    if (B < 0 || B > 255)
                        B = BackColor.B - 15;

                    // Задать оттенок дополнительным элементам интерфейса
                    _secondaryColor = Color.FromArgb(R, G, B);
                    // Установить новый оттенок для кнопки "Продолжить"
                    buttonProceed.BackColor = _secondaryColor;
                }
            }
        }

        /// <summary>
        /// Переход на главный экран
        /// </summary>
        private void buttonProceed_Click(object sender, EventArgs e)
        {
            _selectedMapLanguage = comboBoxLanguage.Text;
            // Если Интернет есть переходим на настройки
            timer_Tick(sender, e);
            if (_flagConnection)
            {
                if (_selectedMapLanguage == "Русская")
                {
                    _languageOfMap = LanguageType.Russian;
                    Hide();
                    StartSettings settings = new StartSettings(_languageOfMap, _mainColor, _secondaryColor);
                    settings.ShowDialog();
                    Close();
                }
                else
                {
                    _languageOfMap = LanguageType.English;
                    Hide();
                    StartSettings settings = new StartSettings(_languageOfMap, _mainColor, _secondaryColor);
                    settings.ShowDialog();
                    Close();
                }
            }
            else
                MessageBox.Show("Соединение с интернетом не найдено", "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Обработка таймера каждую секунду для проверки наличие Интернета
        /// </summary>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (Internet.CheckConnection())
            {
                _flagConnection = true;
                pictureBoxConnection.Image = Properties.Resources.iconWifiTrue;
                labelStateConnetion.Text = "Соединение с интернетом найдено";
            }
            else
            {
                _flagConnection = false;
                pictureBoxConnection.Image = Properties.Resources.iconWifiFalse;
                labelStateConnetion.Text = "Соединение с интернетом не найдено";
            }
        }

        /// <summary>
        /// Открытие руководства пользователя в HTML
        /// </summary>
        private void buttonPDF_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(Application.StartupPath + @"\Data\Manual\index.html");
            }
            catch
            {
                MessageBox.Show("Файл не найден", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Закрыть форму через ESC
        /// </summary>
        private void StartForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}