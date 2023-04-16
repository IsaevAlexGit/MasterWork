using System.Drawing;
using System.Windows.Forms;

namespace Optimum
{
    public class PaintGroupBoxBorder
    {
        /// <summary>
        /// Отрисовка границ для GroupBox
        /// </summary>
        public void groupBox_Paint(object sender, PaintEventArgs e)
        {
            GroupBox box = sender as GroupBox;
            _DrawGroupBox(box, e.Graphics, Color.Gray);
        }

        public void groupBox_PaintRed(object sender, PaintEventArgs e)
        {
            GroupBox box = sender as GroupBox;
            _DrawGroupBox(box, e.Graphics, Color.Red);
        }

        /// <summary>
        /// Установка параметров для GroupBox
        /// </summary>
        /// <param name="box">Объект GroupBox</param>
        /// <param name="g">Объект Graphics</param>
        /// <param name="borderColor">Цвет границ для GroupBox</param>
        private void _DrawGroupBox(GroupBox box, Graphics g, Color borderColor)
        {
            if (box != null)
            {
                Brush borderBrush = new SolidBrush(borderColor);
                Pen borderPen = new Pen(borderBrush);
                SizeF strSize = g.MeasureString(box.Text, box.Font);
                // Границы у GroupBox
                Rectangle rect = new Rectangle(box.ClientRectangle.X, box.ClientRectangle.Y + (int)(strSize.Height / 2),
                    box.ClientRectangle.Width - 1, box.ClientRectangle.Height - (int)(strSize.Height / 2) - 1);

                // Отрисовка линий у GroupBox
                // Левая линия
                g.DrawLine(borderPen, rect.Location, new Point(rect.X, rect.Y + rect.Height));
                // Правая линия
                g.DrawLine(borderPen, new Point(rect.X + rect.Width, rect.Y), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                // Нижняя линия
                g.DrawLine(borderPen, new Point(rect.X, rect.Y + rect.Height), new Point(rect.X + rect.Width, rect.Y + rect.Height));
                // Верхная левая линия
                g.DrawLine(borderPen, new Point(rect.X, rect.Y), new Point(rect.X + box.Padding.Left, rect.Y));
                // Верхная правая линия
                g.DrawLine(borderPen, new Point(rect.X + box.Padding.Left + (int)(strSize.Width + 5), rect.Y), new Point(rect.X + rect.Width, rect.Y));
            }
        }
    }
}