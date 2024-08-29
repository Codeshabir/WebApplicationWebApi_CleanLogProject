using Vosk;
using NAudio.Wave;
using System.IO;

namespace Client.Services
{
    public class SpeechToTextService
    {
        private readonly Vosk.Model _model;

        public SpeechToTextService(string modelPath)
        {
            _model = new Vosk.Model(modelPath);
        }

        public string Recognize(byte[] audioBytes)
        {
            using (var ms = new MemoryStream(audioBytes))
            using (var waveStream = new WaveFileReader(ms))
            using (var rec = new VoskRecognizer(_model, waveStream.WaveFormat.SampleRate))
            {
                rec.SetMaxAlternatives(0);
                rec.SetWords(true);

                string result = string.Empty;

                while (waveStream.Position < waveStream.Length)
                {
                    var buffer = new byte[4096];
                    int bytesRead = waveStream.Read(buffer, 0, buffer.Length);
                    if (rec.AcceptWaveform(buffer, bytesRead))
                    {
                        result += rec.Result();
                    }
                    else
                    {
                        result += rec.PartialResult();
                    }
                }

                result += rec.FinalResult();
                return result;
            }
        }
    }
}
