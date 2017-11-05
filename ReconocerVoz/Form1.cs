using System;
using System.Speech.Recognition;
using System.Windows.Forms;

namespace ReconocerVoz
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        private SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();

        public Form1()
        {
            InitializeComponent();
        }

        private void btnHabilitar_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsync(RecognizeMode.Multiple);
            btnDeshabilitar.Enabled = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Choices commands = new Choices();
            commands.Add(new string[] { "hola", "mostrar mi nombre" });
            GrammarBuilder gBuilder = new GrammarBuilder();
            gBuilder.Append(commands);
            Grammar grammar = new Grammar(gBuilder);

            recEngine.LoadGrammarAsync(grammar);
            recEngine.SetInputToDefaultAudioDevice();
            recEngine.SpeechRecognized += RecEngine_SpeechRecognized;
        }

        void RecEngine_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            switch (e.Result.Text)
            {
                case "hola":
                    MessageBox.Show("Hola David, Como estas?");
                    break;

                case "mostrar mi nombre":
                    richTextBox1.Text += "\nDavid";
                    break;
            }
        }

        private void btnDeshabilitar_Click(object sender, EventArgs e)
        {
            recEngine.RecognizeAsyncStop();
            btnDeshabilitar.Enabled = false;
        }
    }
}