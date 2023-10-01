
namespace SignalGenerator
{
    partial class SignalWaveVisualizerForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.comboBoxSignalWaveTypes = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.TextBox_Amplitude = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TextBox_Phase = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.TextBox_Frequency = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.TextBox_Time = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.TextBox_Sampling = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.DutyCycle = new System.Windows.Forms.TextBox();
            this.Button_GenerateSignalWave = new System.Windows.Forms.Button();
            this.SignalWavePlot = new OxyPlot.WindowsForms.PlotView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.b_play = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.BackColor = System.Drawing.Color.SlateGray;
            this.flowLayoutPanel1.Controls.Add(this.comboBoxSignalWaveTypes);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel2);
            this.flowLayoutPanel1.Controls.Add(this.flowLayoutPanel3);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(10, 12);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(274, 477);
            this.flowLayoutPanel1.TabIndex = 1;
            // 
            // comboBoxSignalWaveTypes
            // 
            this.comboBoxSignalWaveTypes.FormattingEnabled = true;
            this.comboBoxSignalWaveTypes.Items.AddRange(new object[] {
            "Harmonic Wave",
            "Sawtooth Wave",
            "Square Wave",
            "Triangle Wave",
            "Noise",
            "PulseWithDifferentDutyCycle"});
            this.comboBoxSignalWaveTypes.Location = new System.Drawing.Point(3, 3);
            this.comboBoxSignalWaveTypes.Name = "comboBoxSignalWaveTypes";
            this.comboBoxSignalWaveTypes.Size = new System.Drawing.Size(263, 28);
            this.comboBoxSignalWaveTypes.TabIndex = 3;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label1);
            this.flowLayoutPanel2.Controls.Add(this.TextBox_Amplitude);
            this.flowLayoutPanel2.Controls.Add(this.label2);
            this.flowLayoutPanel2.Controls.Add(this.TextBox_Phase);
            this.flowLayoutPanel2.Controls.Add(this.label3);
            this.flowLayoutPanel2.Controls.Add(this.TextBox_Frequency);
            this.flowLayoutPanel2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 37);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(152, 205);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Amplitude";
            // 
            // TextBox_Amplitude
            // 
            this.TextBox_Amplitude.Location = new System.Drawing.Point(3, 26);
            this.TextBox_Amplitude.Name = "TextBox_Amplitude";
            this.TextBox_Amplitude.Size = new System.Drawing.Size(100, 30);
            this.TextBox_Amplitude.TabIndex = 5;
            this.TextBox_Amplitude.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "Phase";
            // 
            // TextBox_Phase
            // 
            this.TextBox_Phase.Location = new System.Drawing.Point(3, 85);
            this.TextBox_Phase.Name = "TextBox_Phase";
            this.TextBox_Phase.Size = new System.Drawing.Size(100, 30);
            this.TextBox_Phase.TabIndex = 6;
            this.TextBox_Phase.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Frequency";
            // 
            // TextBox_Frequency
            // 
            this.TextBox_Frequency.Location = new System.Drawing.Point(3, 144);
            this.TextBox_Frequency.Name = "TextBox_Frequency";
            this.TextBox_Frequency.Size = new System.Drawing.Size(100, 30);
            this.TextBox_Frequency.TabIndex = 7;
            this.TextBox_Frequency.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.label4);
            this.flowLayoutPanel3.Controls.Add(this.TextBox_Time);
            this.flowLayoutPanel3.Controls.Add(this.label5);
            this.flowLayoutPanel3.Controls.Add(this.TextBox_Sampling);
            this.flowLayoutPanel3.Controls.Add(this.label6);
            this.flowLayoutPanel3.Controls.Add(this.DutyCycle);
            this.flowLayoutPanel3.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 248);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(142, 210);
            this.flowLayoutPanel3.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "Time";
            // 
            // TextBox_Time
            // 
            this.TextBox_Time.Location = new System.Drawing.Point(3, 26);
            this.TextBox_Time.Name = "TextBox_Time";
            this.TextBox_Time.Size = new System.Drawing.Size(100, 30);
            this.TextBox_Time.TabIndex = 2;
            this.TextBox_Time.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 23);
            this.label5.TabIndex = 1;
            this.label5.Text = "Sampling";
            // 
            // TextBox_Sampling
            // 
            this.TextBox_Sampling.Location = new System.Drawing.Point(3, 85);
            this.TextBox_Sampling.Name = "TextBox_Sampling";
            this.TextBox_Sampling.Size = new System.Drawing.Size(100, 30);
            this.TextBox_Sampling.TabIndex = 3;
            this.TextBox_Sampling.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 118);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(99, 23);
            this.label6.TabIndex = 5;
            this.label6.Text = "DutyCycle";
            // 
            // DutyCycle
            // 
            this.DutyCycle.Location = new System.Drawing.Point(3, 144);
            this.DutyCycle.Name = "DutyCycle";
            this.DutyCycle.Size = new System.Drawing.Size(100, 30);
            this.DutyCycle.TabIndex = 4;
            // 
            // Button_GenerateSignalWave
            // 
            this.Button_GenerateSignalWave.BackColor = System.Drawing.Color.LightSlateGray;
            this.Button_GenerateSignalWave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Button_GenerateSignalWave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Button_GenerateSignalWave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Button_GenerateSignalWave.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Button_GenerateSignalWave.Location = new System.Drawing.Point(10, 505);
            this.Button_GenerateSignalWave.Name = "Button_GenerateSignalWave";
            this.Button_GenerateSignalWave.Size = new System.Drawing.Size(272, 55);
            this.Button_GenerateSignalWave.TabIndex = 2;
            this.Button_GenerateSignalWave.Text = "Generate Signal Wave";
            this.Button_GenerateSignalWave.UseVisualStyleBackColor = false;
            this.Button_GenerateSignalWave.Click += new System.EventHandler(this.GenerateSignalWave);
            // 
            // SignalWavePlot
            // 
            this.SignalWavePlot.Location = new System.Drawing.Point(300, 12);
            this.SignalWavePlot.Name = "SignalWavePlot";
            this.SignalWavePlot.PanCursor = System.Windows.Forms.Cursors.Hand;
            this.SignalWavePlot.Size = new System.Drawing.Size(900, 623);
            this.SignalWavePlot.TabIndex = 0;
            this.SignalWavePlot.Text = "SignalWavePlot";
            this.SignalWavePlot.ZoomHorizontalCursor = System.Windows.Forms.Cursors.SizeWE;
            this.SignalWavePlot.ZoomRectangleCursor = System.Windows.Forms.Cursors.SizeNWSE;
            this.SignalWavePlot.ZoomVerticalCursor = System.Windows.Forms.Cursors.SizeNS;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // b_play
            // 
            this.b_play.BackColor = System.Drawing.Color.LightSlateGray;
            this.b_play.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.b_play.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b_play.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.b_play.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.b_play.Location = new System.Drawing.Point(10, 581);
            this.b_play.Name = "b_play";
            this.b_play.Size = new System.Drawing.Size(272, 55);
            this.b_play.TabIndex = 3;
            this.b_play.Text = "Play";
            this.b_play.UseVisualStyleBackColor = false;
            this.b_play.Click += new System.EventHandler(this.b_play_Click);
            // 
            // SignalWaveVisualizerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1238, 648);
            this.Controls.Add(this.b_play);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.Button_GenerateSignalWave);
            this.Controls.Add(this.SignalWavePlot);
            this.Name = "SignalWaveVisualizerForm";
            this.Text = "Signal Generator";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TextBox_Amplitude;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TextBox_Phase;
        private System.Windows.Forms.TextBox TextBox_Frequency;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox TextBox_Time;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox TextBox_Sampling;
        private System.Windows.Forms.Button Button_GenerateSignalWave;
        private OxyPlot.WindowsForms.PlotView SignalWavePlot;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ComboBox comboBoxSignalWaveTypes;
        private System.Windows.Forms.TextBox DutyCycle;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button b_play;
    }
}

