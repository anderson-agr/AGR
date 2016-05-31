using System;
using System.Threading.Tasks;
using AGR.ControlRenderer;
using AGR.Droid.Audio;
using AGR.Droid.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using NotificationManager = AGR.Droid.Audio.NotificationManager;



[assembly: ExportRenderer(typeof(StartRecordButton), typeof(StartRecordingButtonRenderer))]

namespace AGR.Droid.Buttons
{
    public class StartRecordingButtonRenderer : ButtonRenderer
    {
        private RecordAudio _recordAudio = new RecordAudio();
        private NotificationManager nMan = new NotificationManager();
        public static bool UseNotifications = false;
        private bool _isRecording;

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            try
            {
                base.OnElementChanged(e);

                if (e.NewElement != null)
                {
                    //sobreescreve eventos e outras coisas   
                }
                else if (e.OldElement != null)
                {
                    //cancela a assinatura dos eventos
                }

                if (Control != null)
                {
                    Control.Click += async delegate
                    {
                        if (!_isRecording)
                        {
                            await StartOperationAsync(_recordAudio);

                            _isRecording = true;
                            //haveRecording = true;
                            Control.Text = "Parar";
                        }
                        else
                        {
                            StopOperation(_recordAudio);
                            _isRecording = false;
                            Control.Text = "Gravar";
                        }

                    };
                    
                }
            }
            catch (Exception exe)
            {
                throw new Exception(exe.Message);
            }
            
        }

        private void StopOperation(INotificationReceiver nRec)
        {
            nRec.Stop();
            if (UseNotifications)
            {
                nMan.ReleaseAudioResources();
            }

        }

        private async Task StartOperationAsync(INotificationReceiver nRec)
        {
            if (UseNotifications)
            {
                bool haveFocus = nMan.RequestAudioResources(nRec);
                if (haveFocus)
                {
                    //	status.Text = "Granted";
                    await nRec.StartAsync();
                }
                else
                {
                    //status.Text = "Denied";
                }
            }
            else
            {
                await nRec.StartAsync();
            }
        }
    }
}