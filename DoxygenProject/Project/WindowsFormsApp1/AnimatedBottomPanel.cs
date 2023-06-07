using System;
using System.Drawing;
using System.Windows.Forms;

namespace Optimum
{
    //! Класс для нижней панели
    class AnimatedBottomPanel : Panel
    {
        //! Два состояния панели: открыта или закрыта
        public enum stateEnum
        {
            close = 0,
            open = 1
        };

        //! Кнопка показать/Скрыть панель
        public Button actionButton;
        //! Состояние панели
        public stateEnum state;
        private MapModel _model;

        //! Таймер
        private Timer _tickTack;
        //! Всплывающая подсказка при наведении на кнопку
        private ToolTip _toolTipForPanel = new ToolTip();

        /*!
        \version 1.0
        */
        /// <summary>
        /// Конструктор панели
        /// </summary>
        /// <permission cref="">Доступ к функции: public</permission>
        /// <param name="size">Размер панели</param>
        /// <param name="top">Расположение панели</param>
        /// <param name="color">Цвет панели</param>
        /// <param name="state">Состояние панели</param>
        /// <param name="model">Объект MapModel</param>
        /// <remarks>
        /// Функция создаёт нижнюю выдвигающуюся панель
        /// </remarks>
        public AnimatedBottomPanel(Size size, int top, Color color, stateEnum state, MapModel model)
        {
            // Задать размер, расположение панели и ее цвет
            Size = size;
            Top = top;
            BackColor = color;
            _model = model;

            // Сколько будет видна подсказка при наведении на стрелочку скрытия
            _toolTipForPanel.AutoPopDelay = 10000;
            // Через какое время будет показываться подсказка после наведения
            _toolTipForPanel.InitialDelay = 1;
            _toolTipForPanel.ReshowDelay = 1000;
            // Показывать подсказку
            _toolTipForPanel.ShowAlways = true;

            // Создать кнопку
            actionButton = new Button();
            {
                // Надпись на кнопке
                actionButton.Text = "↓";
                // Шрифт, размер кнопки
                actionButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                // Выравнивание текста
                actionButton.TextAlign = ContentAlignment.MiddleCenter;
                // Размер кнопки
                actionButton.Size = new Size(25, 30);
                // Цвет кнопки
                actionButton.BackColor = _model.mainColor;
                // Расположение кнопки
                actionButton.Left = ClientSize.Width / 2 - 5;
                actionButton.Top = ClientSize.Height / 2 - ClientSize.Height / 2 + 5;
                actionButton.Cursor = Cursors.Hand;
                // Обработка клика по кнопке
                actionButton.Click += _ActionButtonClick;
                // Привязка кнопки и подсказки
                _toolTipForPanel.SetToolTip(actionButton, "Скрыть нижнюю панель.");
                // Добавить кнопку в контроллеры окна
                Controls.Add(actionButton);
            }

            // Проверка, какое сейчас состояние у панели должно быть
            this.state = state;
            switch (this.state)
            {
                // Если закрытое состояние
                case stateEnum.close:
                    {
                        // У этой панели
                        Top = ClientSize.Height - (5 - actionButton.Size.Height - 5);
                    }
                    break;
                // Если открытое состояние
                case stateEnum.open:
                    {
                        Left = 300;
                    }
                    break;
            }

            // Запуск таймера
            _tickTack = new Timer();
            {
                // Период таймера
                _tickTack.Interval = 5;
                // Функция обработки таймера
                _tickTack.Tick += _TickTack;
            }

            BorderStyle = BorderStyle.FixedSingle;
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Обработка клика по кнопке
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        /// <remarks>
        /// Функция запускает таймер для движения панели
        /// </remarks>
        private void _ActionButtonClick(object Sender, EventArgs E)
        {
            _tickTack.Start();
        }

        //! Шаг сдвига панели
        private int _step = 0;
        //! Флаг остановки движения панели
        private bool _flagMove = false;

        /*!
        \version 1.0
        */
        /// <summary>
        /// Обработка тика таймера
        /// </summary>
        /// <permission cref="">Доступ к функции: private</permission>
        /// <remarks>
        /// Функция выполняет действия каждый тик таймера
        /// </remarks>
        private void _TickTack(object Sender, EventArgs E)
        {
            switch (state)
            {
                // Если закрыта панель
                case stateEnum.close:
                    {
                        if (_flagMove)
                        {
                            _step++;
                            Top -= 5;
                            if (_step == 25)
                            {
                                _tickTack.Stop();
                                _flagMove = false;
                                // Как только открылась панель, надо изменить надпись на кнопке
                                actionButton.Text = "↓";
                                // Сменить подсказку на кнопке
                                _toolTipForPanel.SetToolTip(actionButton, "Скрыть боковую панель.");
                                // Сменить состояние панели
                                state = stateEnum.open;
                                // Сохранить состояние панели в _model
                                _model.stateOfBottomPanel = "Open";
                                // Остановить таймер
                                _step = 0;
                            }
                        }
                    }
                    break;
                // Если открыта панель
                case stateEnum.open:
                    {
                        // Координата левого верхнего угла показанной панели больше той точки, которая у нее будет в скрытом виде
                        if (Top > (ClientSize.Height - (5 - actionButton.Size.Height - 5)))
                        {
                            _step++;
                            Top += 5;
                            if (_step == 25)
                            {
                                _tickTack.Stop();
                                _flagMove = true;
                                // Как только закрылась панель, надо изменить надпись на кнопке
                                actionButton.Text = "↑";
                                // Сменить подсказку на кнопке
                                _toolTipForPanel.SetToolTip(actionButton, "Показать боковую панель.");
                                // Сменить состояние панели
                                state = stateEnum.close;
                                // Сохранить состояние панели в _model
                                _model.stateOfBottomPanel = "Close";
                                // Остановить таймер
                                _step = 0;
                            }
                        }
                    }
                    break;
            }
        }
    }
}