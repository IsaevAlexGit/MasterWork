using System.Drawing;
using System.Windows.Forms;

namespace Optimum
{
    //! Класс "Отрисовка границ рамки у GroupBox"
    public class PaintGroupBoxBorder
    {
        /*!
        \version 1.0
        */
        /// <summary>
        /// Отрисовка границ для GroupBox
        /// </summary>
        /// <permission cref="">Доступ к функции: public</permission>
        public void groupBox_Paint(object sender, PaintEventArgs e)
        {
            GroupBox box = sender as GroupBox;
            _DrawGroupBox(box, e.Graphics, Color.Gray);
        }

        /*!
        \version 1.0
        */
        /// <summary>
        /// Установка параметров для GroupBox
        /// </summary>
        /// <param name="box">Объект GroupBox</param>
        /// <param name="graphics">Объект Graphics</param>
        /// <param name="borderColor">Цвет границ для GroupBox</param>
        /// <permission cref="">Доступ к функции: private</permission>
        private void _DrawGroupBox(GroupBox box, Graphics graphics, Color borderColor)
        {
            if (box != null)
            {
                Brush borderBrush = new SolidBrush(borderColor);
                Pen borderPen = new Pen(borderBrush);
                SizeF strSize = graphics.MeasureString(box.Text, box.Font);
                // Границы у GroupBox
                Rectangle rect = new Rectangle(box.ClientRectangle.X, box.ClientRectangle.Y + (int)(strSize.Height / 2),
                    box.ClientRectangle.Width - 1, box.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);

                // Отрисовка линий у GroupBox
                // Левая линия
                graphics.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
                // Правая линия
                graphics.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                // Нижняя линия
                graphics.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                // Верхная левая линия
                graphics.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + box.Padding.Left, rect.Y));
                // Верхная правая линия
                graphics.DrawLine(borderPen, new Point(rect.X + box.Padding.Left + (int)(strSize.Width + 5), rect.Y), new Point(rect.X + rect.Width, rect.Y));
            }
        }
    }
}