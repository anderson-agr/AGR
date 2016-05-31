using System;
using System.Threading.Tasks;
using AGR.ControlRenderer;
using AGR.Droid.Audio;
using AGR.Droid.Buttons;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using NotificationManager = AGR.Droid.Audio.NotificationManager;

[assembly: ExportRenderer(typeof(PlayButton), typeof(PlayButtonRederer))]

namespace AGR.Droid.Buttons
{
    public class PlayButtonRederer : ButtonRenderer
    {
        private NotificationManager nMan = new NotificationManager();
        PlayAudio playAudio = new PlayAudio();
        public static bool UseNotifications = false;
        private bool _isPlaying;
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
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
                try
                {
                    Control.Click += async delegate
                                  {
                                      if (!_isPlaying)
                                      {
                                          await StartOperationAsync(playAudio);
                                          _isPlaying = true;
                                          Control.Text = "Parar";
                                      }
                                      else
                                      {
                                          StopOperation(playAudio);
                                          Control.Text = "Play";
                                          _isPlaying = false;
                                      }
                                  };
                }
                catch (Exception exception)
                {

                    throw new Exception(exception.Message); 
                }




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