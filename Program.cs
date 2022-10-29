using SignalGenerator.Signal.SignalType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            Application.Run(new Form1());

            HarmonicSignal signalWave = new HarmonicSignal(1, 0, 2);
            double[] c = signalWave.GenerateSignalWaveDots(10, 8);
            
            for(int i=0; i<c.Length; i++)
            {
                Console.WriteLine(i+1 + "   " + c[i]);
            }
        }
    }
}
