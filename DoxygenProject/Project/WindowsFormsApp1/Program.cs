using System;
using System.Windows.Forms;

namespace Optimum
{
    //! Класс для запуска приложения
    static class Program
    {
        /*!
        \version 1.0
        */
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