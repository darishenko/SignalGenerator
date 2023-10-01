using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SignalGenerator.Signal.Sound
{
    class SoundFormat
    {
        public byte[] ChunkID = new byte[4];
        public byte[] ChunkSize = new byte[4];
        public byte[] Format = new byte[4];
        public byte[] Subchunk1ID = new byte[4];
        public byte[] Subchunk1Size = new byte[4];
        public byte[] AudioFormat = new byte[2];
        public byte[] NumChannels = new byte[2];
        public byte[] SampleRate = new byte[4];
        public byte[] ByteRate = new byte[4];
        public byte[] BlockAlign = new byte[2];
        public byte[] BitsPerSample = new byte[2];
        public byte[] Subchunk2ID = new byte[4];
        public byte[] Subchunk2Size = new byte[4];
        public byte[] Data;
    }
}
