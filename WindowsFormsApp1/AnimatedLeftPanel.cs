using System;
using System.Drawing;
using System.Windows.Forms;

namespace Optimum
{
    class AnimatedLeftPanel : Panel
    {
        // Два состояния кнопки
        public enum stateEnum { close = 0, open = 1 };
        // Кнопка показать и скрыть панель
        public Button actionButton;
        public stateEnum state;

        // Таймер
        private Timer _tickTack;
        // Всплывающая подсказка при наведении на стрелочку
        ToolTip toolTipForPanel = new ToolTip();

        // Конструктор для нашей панели
        public AnimatedLeftPanel(Size size, int top, Color color, stateEnum state)
        {
            // Задать размер, расположение панели и ее цвет
            Size = size;
            Top = top;
            BackColor = color;

            // Сколько будет показана подсказка видна при наведении на стрелочку скрытия
            toolTipForPanel.AutoPopDelay = 10000;
            // Через какое время будет показываться подсказка после наведения
            toolTipForPanel.InitialDelay = 1;
            toolTipForPanel.ReshowDelay = 1000;
            // Показывать подсказку
            toolTipForPanel.ShowAlways = true;

            // Создать кнопку
            actionButton = new Button();
            {
                // Надпись на кнопочке
                actionButton.Text = "←";
                // Шрифт, размер кнопки
                actionButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                // Выравнивание текста
                actionButton.TextAlign = ContentAlignment.MiddleCenter;
                // Размер кнопки
                actionButton.Size = new Size(25, 38);
                // Цвет кнопки
                actionButton.BackColor = Color.FromArgb(174, 238, 180);
                // Местонахождение кнопки лево и верх
                actionButton.Left = ClientSize.Width - actionButton.Size.Width - 5;
                actionButton.Top = ClientSize.Height / 2 - actionButton.Size.Height;
                actionButton.Cursor = Cursors.Hand;
                // Обработка клика по кнопке
                actionButton.Click += ActionButtonClick;
                // Привязка кнопки и подсказки
                toolTipForPanel.SetToolTip(actionButton, "Скрыть боковую панель");
                // Добавить кнопку в контроллеры окна
                Controls.Add(actionButton);
            }

            // Проверка какое сейчас состояние у панели должно быть
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
                // Переодичность таймера
                _tickTack.Interval = 5;
                // Функция обработки таймера
                _tickTack.Tick += TickTack;
            }

            BorderStyle = BorderStyle.FixedSingle;
        }

        /// <summary>
        /// Обработка клика по кнопке
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void ActionButtonClick(object Sender, EventArgs E)
        {
            _tickTack.Start();
        }

        /// <summary>
        /// Обрабтка тика таймера
        /// </summary>
        /// <param name="Sender"></param>
        /// <param name="E"></param>
        private void TickTack(object Sender, EventArgs E)
        {
            switch (state)
            {
                // Если закрыта панель
                case stateEnum.close:
                    {
                        // Координата левого верхнего угла скрытой панели, которая где-то за формой слева
                        if (Left < 0)
                            Left += 20;
                        else
                        {
                            // Как только открылась изменить надпись на кнопке
                            actionButton.Text = "←";
                            // Сменить подсказку
                            toolTipForPanel.SetToolTip(actionButton, "Скрыть боковую панель");
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
                            // Как только закрылась изменить надпись на кнопке
                            actionButton.Text = "→";
                            // Сменить подсказку
                            toolTipForPanel.SetToolTip(actionButton, "Показать боковую панель");
                            // Сменить состояние панели
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