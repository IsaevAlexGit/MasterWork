using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Optimum
{
    public partial class FacilityInfo : Form
    {
        // Объект инфраструктуры, по которому кликнули
        private Facility _infoFacility = new Facility();
        private MapModel _mapModel;

        // Отрисовка границ groupbox
        private PaintGroupBoxBorder _paintGroupBoxBorder = new PaintGroupBoxBorder();

        /// <summary>
        /// Конструктор формы
        /// </summary>
        /// <param name="facility">Объект инфраструктуры, по которому кликнули</param>
        public FacilityInfo(Facility facility, MapModel model)
        {
            _infoFacility = facility;
            _mapModel = model;
            InitializeComponent();
            StartPosition = FormStartPosition.CenterScreen;
            KeyPreview = true;
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

            // Если об объекте инфраструктуры есть хоть 1 столбец информации
            if (_infoFacility.infoAboutFacility.Count >= 1)
            {
                // Сколько столбцов столько и label
                for (int i = 0; i < _infoFacility.infoAboutFacility.Count; i++)
                {
                    Label label = new Label
                    {
                        // Локация надписи, текст (значение столбца), шрифт и размер, курсор
                        Location = new Point(13, 45 + i * 30),
                        BackColor = _mapModel.mainColor,
                        Text = _infoFacility.infoAboutFacility[i],
                        AutoSize = true,
                        Font = new Font("Segoe UI", 12),
                        Cursor = Cursors.Hand
                    };

                    // Сохранить локацию надписи
                    locationBottomLabal = label.Location;

                    // Сохранить текст надписи
                    string textFromLabel = _infoFacility.infoAboutFacility[i];

                    label.DoubleClick += (s, a) =>
                    {
                    // Если у пользователя в загруженных данных нельзя перейти по ссылке
                    try
                        {
                            Process.Start(textFromLabel);
                        }
                        catch
                        {
                            MessageBox.Show("Ошибка при загрузке сайта", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    };
                    Controls.Add(label);
                }

                // Растянуть окно под динамическое количество надписей
                ClientSize = new Size(710, locationBottomLabal.Y + 30);
                MinimumSize = MaximumSize = new Size(710, locationBottomLabal.Y + 70);
                FormBorderStyle = FormBorderStyle.FixedSingle;
            }
            else
            {
                // Если у объектов инфраструктуры нет информации никакой
                Hide();
                Close();
                MessageBox.Show("Данные отстуствуют");
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