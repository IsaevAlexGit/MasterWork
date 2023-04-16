using System;
using System.Drawing;
using System.Windows.Forms;

namespace Optimum
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Вход в программу
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new StartForm());
            // Application.Run(new StartSettings(GMap.NET.LanguageType.Russian, Color.FromArgb(138, 230, 145), Color.FromArgb(174, 238, 180)));
            // Application.Run(new MainMap(GMap.NET.LanguageType.Russian));
            // Application.Run(new SearchNormPerCapita(GMap.NET.LanguageType.Russian));
        }
    }
}