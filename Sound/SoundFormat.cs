namespace SignalGenerator.Sound
{
    internal class SoundFormat
    {
        public byte[] AudioFormat = new byte[2];
        public byte[] BitsPerSample = new byte[2];
        public byte[] BlockAlign = new byte[2];
        public byte[] ByteRate = new byte[4];
        public byte[] ChunkID = new byte[4];
        public byte[] ChunkSize = new byte[4];
        public byte[] Data;
        public byte[] Format = new byte[4];
        public byte[] NumChannels = new byte[2];
        public byte[] SampleRate = new byte[4];
        public byte[] Subchunk1ID = new byte[4];
        public byte[] Subchunk1Size = new byte[4];
        public byte[] Subchunk2ID = new byte[4];
        public byte[] Subchunk2Size = new byte[4];
    }
}