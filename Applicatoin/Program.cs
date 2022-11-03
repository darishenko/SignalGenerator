using SignalGenerator.Signal.SignalType;
using SignalGenerator.Signal;
using System;
using System.Windows.Forms;

namespace SignalGenerator
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new SignalWaveVisualizerForm());
        }
    }
}
