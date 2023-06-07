using System;
using System.Drawing;
using System.Windows.Forms;

namespace Optimum
{
    class AnimatedLeftPanel : Panel
    {
        // Два состояния панели: открыта или закрыта
        public enum stateEnum
        {
            close = 0,
            open = 1
        };
        // Кнопка показать/Скрыть панель
        public Button actionButton;
        // Состояние панели
        public stateEnum state;
        // Таймер
        private Timer _tickTack;
        // Всплывающая подсказка при наведении на кнопку
        private ToolTip _toolTipForPanel = new ToolTip();

        /// <summary>
        /// Конструктор панели
        /// </summary>
        /// <param name="size">Размер панели</param>
        /// <param name="top">Расположение панели</param>
        /// <param name="color">Цвет панели</param>
        /// <param name="state">Состояние панели</param>
        public AnimatedLeftPanel(Size size, int top, Color color, stateEnum state)
        {
            // Задать размер, расположение панели и ее цвет
            Size = size;
            Top = top;
            BackColor = color;

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
                actionButton.Text = "←";
                // Шрифт, размер кнопки
                actionButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                // Выравнивание текста
                actionButton.TextAlign = ContentAlignment.MiddleCenter;
                // Размер кнопки
                actionButton.Size = new Size(25, 38);
                // Цвет кнопки
                actionButton.BackColor = BackColor;
                // Расположение кнопки
                actionButton.Left = ClientSize.Width - actionButton.Size.Width - 5;
                actionButton.Top = ClientSize.Height / 2 - actionButton.Size.Height;
                actionButton.Cursor = Cursors.Hand;
                // Обработка клика по кнопке
                actionButton.Click += _ActionButtonClick;
                // Привязка кнопки и подсказки
                _toolTipForPanel.SetToolTip(actionButton, "Скрыть боковую панель.");
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
                        Left = -ClientSize.Width + 5 + actionButton.Size.Width + 5;
                    }
                    break;
                // Если открытое состояние
                case stateEnum.open:
                    {
                        Left = 0;
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

        /// <summary>
        /// Обработка клика по кнопке
        /// </summary>
        private void _ActionButtonClick(object Sender, EventArgs E)
        {
            _tickTack.Start();
        }

        /// <summary>
        /// Обработка тика таймера
        /// </summary>
        private void _TickTack(object Sender, EventArgs E)
        {
            switch (state)
            {
                // Если закрыта панель
                case stateEnum.close:
                    {
                        // Координата левого верхнего угла скрытой панели
                        if (Left < 0)
                            Left += 20;
                        else
                        {
                            // Как только открылась панель, надо изменить надпись на кнопке
                            actionButton.Text = "←";
                            // Сменить подсказку на кнопке
                            _toolTipForPanel.SetToolTip(actionButton, "Скрыть боковую панель.");
                            // Сменить состояние панели
                            state = stateEnum.open;
                            // Остановить таймер
                            _tickTack.Stop();
                        }
                    }
                    break;
                // Если открыта панель
                case stateEnum.open:
                    {
                        // Координата левого верхнего угла показанной панели больше той точки, которая у нее будет в скрытом виде
                        if (Left > (-ClientSize.Width + 5 + actionButton.Size.Width + 5))
                            Left -= 20;
                        else
                        {
                            // Как только закрылась панель, надо изменить надпись на кнопке
                            actionButton.Text = "→";
                            // Сменить подсказку на кнопке
                            _toolTipForPanel.SetToolTip(actionButton, "Показать боковую панель.");
                            // Сохранить состояние панели в _model
                            state = stateEnum.close;
                            // Остановить таймер
                            _tickTack.Stop();
                        }
                    }
                    break;
            }
        }
    }
}