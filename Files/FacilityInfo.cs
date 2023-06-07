using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Optimum
{
    public partial class FacilityInfo : Form
    {
        // Объект социальной инфраструктуры, по которому кликнули ЛКМ
        private Facility _infoFacility = new Facility();
        // Объект класса MapModel
        private MapModel _mapModel;

        // Отрисовка границ groupbox
        private PaintGroupBoxBorder _paintGroupBoxBorder = new PaintGroupBoxBorder();

        /// <summary>
        /// Конструктор формы
        /// </summary>
        /// <param name="facility">Объект социальной инфраструктуры, по которому кликнули ЛКМ</param>
        /// <param name="model">Объект класса MapModel</param>
        public FacilityInfo(Facility facility, MapModel model)
        {
            _infoFacility = facility;
            _mapModel = model;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;
        }

        /// <summary>
        /// Проверка: является ли строка веб-адресом
        /// </summary>
        /// <param name="url">Строка с веб-адресом</param>
        /// <returns>Результат проверки: true - строка является веб-адресом</returns>
        public bool CheckValidateWebAddress(string url)
        {
            bool result = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
               && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }

        /// <summary>
        /// Загрузка формы
        /// </summary>
        private void FacilityInfo_Load(object sender, EventArgs e)
        {
            // Цвет фона формы
            BackColor = _mapModel.mainColor;
            foreach (Control groupbox in Controls)
            {
                GroupBox everyGroupBox = groupbox as GroupBox;
                if (everyGroupBox != null)
                    everyGroupBox.Paint += _paintGroupBoxBorder.groupBox_Paint;
            }

            // Локация самого нижнего label
            Point locationBottomLabal = new Point();

            // Если об объекте инфраструктуры в файле есть хотя бы 1 столбец с данными
            if (_infoFacility.infoAboutFacility.Count >= 1)
            {
                // Проход по всем столбцам с данными
                for (int i = 0; i < _infoFacility.infoAboutFacility.Count; i++)
                {
                    Label label = new Label
                    {
                        // Локация надписи, текст (значение столбца), шрифт и размер надписи
                        Location = new Point(13, 35 + i * 30),
                        BackColor = _mapModel.mainColor,
                        Text = _infoFacility.infoAboutFacility[i],
                        AutoSize = true,
                        Font = new Font("Segoe UI", 12),
                    };

                    // Сохранить локацию надписи
                    locationBottomLabal = label.Location;

                    // Сохранить текст надписи
                    string textFromLabel = _infoFacility.infoAboutFacility[i];
                    // Если строка является веб-адресом, то эта строка кликабельна, и курсор становится другим
                    if (CheckValidateWebAddress(textFromLabel))
                    {
                        Cursor = Cursors.Hand;
                        label.DoubleClick += (s, a) =>
                        {
                            // Обработка открытия веб-адреса
                            try
                            {
                                Process.Start(textFromLabel);
                            }
                            catch
                            {
                                MessageBox.Show("Ошибка при загрузке сайта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        };
                    }
                    Controls.Add(label);
                }

                // Размер окна динамический и зависит от количества выводимых данных об объекте социальной инфраструктуры
                ClientSize = new Size(704, locationBottomLabal.Y + 30);
                MinimumSize = MaximumSize = new Size(704, locationBottomLabal.Y + 70);
                FormBorderStyle = FormBorderStyle.FixedSingle;
                StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                // Если в файле у объектов социальной инфраструктуры нет данных, то вместо окна отображать предупреждение
                Hide();
                Close();
                MessageBox.Show("Данные отсутствуют");
                ClientSize = new Size(0, 0);
                FormBorderStyle = FormBorderStyle.None;
            }
        }

        /// <summary>
        /// Закрыть форму через ESC
        /// </summary>
        private void FacilityInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();
        }
    }
}