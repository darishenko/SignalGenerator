using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using SignalGenerator.Signal;
using SignalGenerator.Signal.SignalType;
using SignalGenerator.Signal.SignalWave;
using SignalGenerator.Signal.Sound;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace SignalGenerator
{
    public partial class SignalWaveVisualizerForm : Form
    {
        private static PlotModel SignalWavePlotModel;
        private ISignalWave selectedSignalWave;
        private SoundPlayer player;
        private SoundGenerator generator = new SoundGenerator();
        private int Sampling;
        double[] signalWavePoints;

        public SignalWaveVisualizerForm()
        {
            InitializeComponent();
            comboBoxSignalWaveTypes.SelectedIndex = 0;

            SignalWavePlotModel = new PlotModel { Title = "Signal Wave" };
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (System.Text.RegularExpressions.Regex.IsMatch(textBox.Text, "[^0-9]"))
            {
                MessageBox.Show("Please enter only numbers.");
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            }
        }

        private void GenerateSignalWave(object sender, EventArgs e)
        {
            int Time = int.Parse(TextBox_Time.Text);
            Sampling = int.Parse(TextBox_Sampling.Text);

            selectedSignalWave = getSignalWave();
            selectedSignalWave.GenerateSignalWaveDots(Time, Sampling);
            DrawSignalWave(selectedSignalWave.values);
        }

        private ISignalWave getSignalWave()
        {
            double Amplitude = 1;
            double Phase = 0;
            double Frequency = 1;
            var signalWaveType = (SignalWaveType)comboBoxSignalWaveTypes.SelectedIndex;
            if (signalWaveType != SignalWaveType.NOISE) {
                Amplitude = Double.Parse(TextBox_Amplitude.Text);
                Phase = Double.Parse(TextBox_Phase.Text);
                Frequency = Double.Parse(TextBox_Frequency.Text);
            }

            ISignalWave selectedSignalWave;
            switch (signalWaveType)
            {
                case SignalWaveType.HARMONIC:
                    selectedSignalWave = new HarmonicSignalWave(Amplitude, Phase, Frequency);
                    break;
                case SignalWaveType.SQUARE:
                    selectedSignalWave = new SquareSignalWave(Amplitude, Phase, Frequency);
                    break;
                case SignalWaveType.TRIANGLE:
                    selectedSignalWave = new TriangleSignalWave(Amplitude, Phase, Frequency);
                    break;
                case SignalWaveType.SAWTOOTH:
                    selectedSignalWave = new SawtoothSignalWave(Amplitude, Phase, Frequency);
                    break;
                case SignalWaveType.NOISE:
                    selectedSignalWave = new Noise(Amplitude);
                    break;
                case SignalWaveType.PulseWithDifferentDutyCycle:
                    float dutyCycle = float.Parse(DutyCycle.Text);
                    selectedSignalWave = new PulseWithDifferentDutyCycle(Amplitude, dutyCycle, Frequency);
                    break;
                default:
                    selectedSignalWave = new HarmonicSignalWave(Amplitude, Phase, Frequency);
                    break;
            }

            return selectedSignalWave;
        }

        private void DrawSignalWave(double[] signalWavePoints)
        {
            LineSeries SignalWave = new LineSeries()
            {
                Color = OxyColor.FromRgb(47, 94, 94),
                StrokeThickness = 2,
                MarkerSize = 3,
                MarkerType = MarkerType.Circle,
            };

            for (int i = 0; i < signalWavePoints.Length; i++)
            {
                SignalWave.Points.Add(new DataPoint(i * (2f / signalWavePoints.Length), signalWavePoints[i]));
            }

            SignalWavePlotModel.Series.Clear();
            SignalWavePlotModel.Series.Add(SignalWave);
            SignalWavePlot.Model = SignalWavePlotModel;
            SignalWavePlot.InvalidatePlot(true);
        }

        private void b_play_Click(object sender, EventArgs e)
        {
            string filename = "result.wave";
            generator.WriteWaveFile(filename, selectedSignalWave.values, Sampling);
            player = new SoundPlayer(filename);
            player.Play();
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxSignalWaveTypes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<ISignalWave> waves = new List<ISignalWave>();
            ISignalWave tempWave;
            for (int i = 1; i< 6; i++)
            {
                switch(i)
                {
                    case 1:
                        tempWave = getSignalWaveForPolyphonicSignal(comboBox1, a1, ph1, fr1, dc1, t1);
                        if (tempWave != null)
                        {
                            waves.Add(tempWave);
                        }
                        break;
                    case 2:
                        tempWave = getSignalWaveForPolyphonicSignal(comboBox2, a2, ph2, fr2, dc2, t2);
                        if (tempWave != null)
                        {
                            waves.Add(tempWave);
                        }
                        break;
                    case 3:
                        tempWave = getSignalWaveForPolyphonicSignal(comboBox3, a3, ph3, fr3, dc3, t3);
                        if (tempWave != null)
                        {
                            waves.Add(tempWave);
                        }
                        break;
                    case 4:
                        tempWave = getSignalWaveForPolyphonicSignal(comboBox4, a4, ph4, fr4, dc4, t4);
                        if (tempWave != null)
                        {
                            waves.Add(tempWave);
                        }
                        break;
                    case 5:
                        tempWave = getSignalWaveForPolyphonicSignal(comboBox5, a5, ph5, fr5, dc5, t5);
                        if (tempWave != null)
                        {
                            waves.Add(tempWave);
                        }
                        break;
                }                
            }
            signalWavePoints = generatePolyphonicSignal(waves);
            DrawSignalWave(signalWavePoints);

        }

        private double[] generatePolyphonicSignal(List<ISignalWave> waves)
        {
            int maxSize = 0;
            var wavesStream = waves.AsEnumerable();
            foreach (var wave in wavesStream)
            {
                maxSize = wave.values.Length > maxSize ? wave.values.Length : maxSize;
            }                
            double[] result = new double[maxSize];
            foreach (var wave in wavesStream)
            {
                for (int i = 0; i < wave.values.Length; i++)
                {
                    result[i] = result[i] + wave.values[i];
                }
            }            

            return result;
        }

        private ISignalWave getSignalWaveForPolyphonicSignal(
            ComboBox comboBox,
            TextBox textBox_Amplitude,
            TextBox textBox_Phase,
            TextBox textBox_Frequency,
            TextBox textBox_DutyCycle,
            TextBox textBox_Time
            )
        {
            if (comboBox.SelectedIndex == -1)
            {
                return null;
            }

            double Amplitude = 1, Phase = 0, Frequency = 1;
            var signalWaveType = (SignalWaveType)comboBox.SelectedIndex;
            if (signalWaveType != SignalWaveType.NOISE)
            {
                Phase = Double.Parse(textBox_Phase.Text);
                Frequency = Double.Parse(textBox_Frequency.Text);
            }
            Amplitude = Double.Parse(textBox_Amplitude.Text);

            ISignalWave SignalWave;
            switch (signalWaveType)
            {
                case SignalWaveType.HARMONIC:
                    SignalWave = new HarmonicSignalWave(Amplitude, Phase, Frequency);
                    break;
                case SignalWaveType.SQUARE:
                    SignalWave = new SquareSignalWave(Amplitude, Phase, Frequency);
                    break;
                case SignalWaveType.TRIANGLE:
                    SignalWave = new TriangleSignalWave(Amplitude, Phase, Frequency);
                    break;
                case SignalWaveType.SAWTOOTH:
                    SignalWave = new SawtoothSignalWave(Amplitude, Phase, Frequency);
                    break;
                case SignalWaveType.NOISE:
                    SignalWave = new Noise(Amplitude);
                    break;
                case SignalWaveType.PulseWithDifferentDutyCycle:
                    float dutyCycle = float.Parse(textBox_DutyCycle.Text);
                    SignalWave = new PulseWithDifferentDutyCycle(Amplitude, dutyCycle, Frequency);
                    break;
                default:
                    SignalWave = new HarmonicSignalWave(Amplitude, Phase, Frequency);
                    break;
            }
            int Time = int.Parse(textBox_Time.Text);
            SignalWave.GenerateSignalWaveDots(Time, 44100);

            return SignalWave;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string filename = "result.wave";
            generator.WriteWaveFile(filename, signalWavePoints, 44100);
            player = new SoundPlayer(filename);
            player.Play();
        }
    }
}
