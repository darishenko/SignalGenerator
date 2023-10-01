using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalGenerator.Signal.Sound
{
    class SoundGenerator
    {
        public void WriteWaveFile(string filename, double[] data, int sampleRate)
        {
            short bitsPerSample = 32, numChannels = 1;

            using (var bwStream = new BinaryWriter(new FileStream(filename, FileMode.OpenOrCreate)))
            {
                bwStream.Write(Encoding.ASCII.GetBytes("RIFF"));
                bwStream.Write(36 + data.Length * numChannels * bitsPerSample / 8);
                bwStream.Write(Encoding.ASCII.GetBytes("WAVE"));
                bwStream.Write(Encoding.ASCII.GetBytes("fmt "));
                bwStream.Write(16);
                bwStream.Write((short)1);
                bwStream.Write(numChannels);
                bwStream.Write(sampleRate);
                bwStream.Write(sampleRate * numChannels * bitsPerSample / 8);
                bwStream.Write((short)(numChannels * bitsPerSample / 8));
                bwStream.Write(bitsPerSample);
                bwStream.Write(Encoding.ASCII.GetBytes("data"));
                bwStream.Write(data.Length * numChannels * bitsPerSample / 8);

                double result;
                foreach (var value in data)
                {
                    result = value * int.MaxValue;
                    bwStream.Write((int)result);
                }

            }

        }
    }
}
