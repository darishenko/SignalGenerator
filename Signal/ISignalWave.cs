using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalGenerator.Signal
{
    interface ISignalWave
    {
        double[] GenerateSignal();
    }
}
