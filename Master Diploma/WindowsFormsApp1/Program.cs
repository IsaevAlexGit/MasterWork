using System;
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
        }
    }
}