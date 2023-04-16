using System;
using System.Drawing;
using System.Windows.Forms;

namespace Optimum
{
    class AnimatedBottomPanel : Panel
    {
        // Два состояния кнопки
        public enum stateEnum { close = 0, open = 1 };
        // Кнопка показать и скрыть панель
        public Button actionButton;
        public stateEnum state;
        private MapModel _model;

        // Таймер
        private Timer _tickTack;
        // Всплывающая подсказка при наведении на стрелочку
        ToolTip toolTipForPanel = new ToolTip();

        // Конструктор для нашей панели
        public AnimatedBottomPanel(Size size, int top, Color color, stateEnum state, MapModel model)
        {
            // Задать размер, расположение панели и ее цвет
            Size = size;
            Top = top;
            BackColor = color;
            _model = model;

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
                actionButton.Text = "↓";
                // Шрифт, размер кнопки
                actionButton.Font = new Font("Segoe UI", 12, FontStyle.Bold);
                // Выравнивание текста
                actionButton.TextAlign = ContentAlignment.MiddleCenter;
                // Размер кнопки
                actionButton.Size = new Size(25, 30);
                // Цвет кнопки
                actionButton.BackColor = _model.mainColor;
                // Местонахождение кнопки лево и верх
                actionButton.Left = ClientSize.Width / 2 - 5;
                actionButton.Top = ClientSize.Height / 2 - ClientSize.Height / 2 + 5;
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

        int step = 0;
        bool flagMove = false;
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
                        if (flagMove)
                        {
                            step++;
                            Top -= 5;
                            if (step == 25)
                            {
                                _tickTack.Stop();
                                flagMove = false;
                                // Как только открылась изменить надпись на кнопке
                                actionButton.Text = "↓";
                                // Сменить подсказку
                                toolTipForPanel.SetToolTip(actionButton, "Скрыть боковую панель");
                                // Сменить состояние панели
                                state = stateEnum.open;
                                // Сохранить состояние панели
                                _model.stateOfBottomPanel = "Open";
                                // Остановить таймер
                                step = 0;
                            }
                        }
                    }
                    break;
                // Если открыта панель
                case stateEnum.open:
                    {
                        if (Top > (ClientSize.Height - (5 - actionButton.Size.Height - 5)))
                        {
                            step++;
                            Top += 5;
                            if (step == 25)
                            {
                                _tickTack.Stop();
                                flagMove = true;
                                // Как только открылась изменить надпись на кнопке
                                actionButton.Text = "↑";
                                // Сменить подсказку
                                toolTipForPanel.SetToolTip(actionButton, "Показать боковую панель");
                                // Сменить состояние панели
                                state = stateEnum.close;
                                // Сохранить состояние панели
                                _model.stateOfBottomPanel = "Close";
                                // Остановить таймер
                                step = 0;
                            }
                        }
                    }
                    break;
            }
        }
    }
}