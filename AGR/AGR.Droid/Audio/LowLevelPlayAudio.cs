using System.IO;
using Android.Media;
using System.Threading.Tasks;
using System;

namespace AGR.Droid.Audio
{
    class LowLevelPlayAudio : INotificationReceiver
    {
        static string filePath = "/data/data/Example_WorkingWithAudio.Example_WorkingWithAudio/files/testAudio.mp4";
        byte[] _buffer;
        AudioTrack _audioTrack;

        public async Task PlaybackAsync()
        {
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            long totalBytes = new FileInfo(filePath).Length;
            _buffer = binaryReader.ReadBytes((Int32)totalBytes);
            fileStream.Close();
            fileStream.Dispose();
            binaryReader.Close();
            await PlayAudioTrackAsync();
        }

        protected async Task PlayAudioTrackAsync()
        {
            _audioTrack = new AudioTrack(
                // Stream type
                Android.Media.Stream.Music,
                // Frequency
                11025,
                // Mono or stereo
                ChannelOut.Mono,
                // Audio encoding
                Encoding.Pcm16bit,
                // Length of the audio clip.
                _buffer.Length,
                // Mode. Stream or static.
                AudioTrackMode.Stream);

            _audioTrack.Play();

            await _audioTrack.WriteAsync(_buffer, 0, _buffer.Length);
        }

        public async Task StartAsync()
        {
            await PlaybackAsync();
        }

        public void Stop()
        {
            if (_audioTrack == null)return;
            _audioTrack.Stop();
            _audioTrack.Release();
            _audioTrack = null;
        }
    }
}