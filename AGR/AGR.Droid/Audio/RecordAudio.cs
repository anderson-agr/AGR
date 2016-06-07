using System;
using Android.Media;
using System.Threading.Tasks;
using System.IO;

namespace AGR.Droid.Audio
{
    class RecordAudio : INotificationReceiver
    {
     
        static string _filePath = ""; //"/data/data/Example_WorkingWithAudio.Example_WorkingWithAudio/files/testAudio.mp4";
        MediaRecorder _recorder;

        public void StartRecorder()
        {
            try
            {
                GetValueFilePath();
                GetConfigurationRecord();
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.StackTrace);
            }
        }

        private void GetConfigurationRecord()
        {
            if (_recorder == null)
                _recorder = new MediaRecorder(); // Initial state.
            else
                _recorder.Reset();

            _recorder.SetAudioSource(AudioSource.Mic);
            _recorder.SetOutputFormat(OutputFormat.Mpeg4);
            _recorder.SetAudioEncoder(AudioEncoder.Default); // Initialized state.
            _recorder.SetOutputFile(_filePath); // DataSourceConfigured state.
            _recorder.Prepare(); // Prepared state
            _recorder.Start(); // Recording state.
        }

        private static void GetValueFilePath()
        {
            Java.IO.File sdDir = Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryMusic);
            _filePath = sdDir + "/" + "testAudio.mp3";
            if (File.Exists(_filePath))
                File.Delete(_filePath);

            Java.IO.File myFile = new Java.IO.File(_filePath);
            myFile.CreateNewFile();
        }

        public void StopRecorder()
        {
            if (_recorder != null)
            {
                _recorder.Stop();
                _recorder.Release();
                _recorder = null;
            }
        }

        public Task StartAsync()
        {
            StartRecorder();

            var tcs = new TaskCompletionSource<object>();
            tcs.SetResult(null);
            return tcs.Task;
        }

        public void Stop()
        {
            StopRecorder();
        }
    }
}