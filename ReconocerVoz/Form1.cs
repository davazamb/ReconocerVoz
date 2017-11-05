using System;
using System.Speech.Recognition;
using System.Windows.Forms;
using System.Speech.Synthesis;

namespace ReconocerVoz
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        SpeechRecognitionEngine recEngine = new SpeechRecognitionEngine();
        SpeechSynthesizer synthesizer = new SpeechSynthesizer();
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
            commands.Add(new string[] { "hola", "mostrar mi nombre", "leeme el texto" });
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
                    PromptBuilder builder = new PromptBuilder();
                    builder.StartSentence();
                    builder.AppendText("Hola David.");
                    builder.EndSentence();
                    builder.AppendBreak(new TimeSpan(0,0,0,0,50));
                    builder.StartSentence();
                    builder.AppendText("Como estas?", PromptEmphasis.Strong);
                    builder.EndSentence();

                    synthesizer.SpeakAsync(builder);
                    break;

                case "mostrar mi nombre":
                    richTextBox1.Text += "\nDavid";
                    break;
                case "leeme el texto":
                    synthesizer.SpeakAsync(richTextBox1.SelectedText);
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