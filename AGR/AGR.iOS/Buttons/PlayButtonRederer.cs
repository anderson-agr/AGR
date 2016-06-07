using AGR.ControlRenderer;
using AGR.iOS.Buttons;
using AudioToolbox;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System;
using System.Diagnostics;
using AVFoundation;
using Foundation;
using UIKit;

[assembly: ExportRenderer(typeof(PlayButton), typeof(PlayButtonRederer))]

namespace AGR.iOS.Buttons
{
    public class PlayButtonRederer : ButtonRenderer
    {
        AVPlayer _player;
        NSObject _observer;
        public PlayButtonRederer()
        {
            AudioSession.Initialize();
        }

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

                    _observer = NSNotificationCenter.DefaultCenter.AddObserver(AVPlayerItem.DidPlayToEndTimeNotification, delegate
                                           {
                                               _player?.Dispose();
                                               _player = null;
                                               Control.SetTitle("Play", UIControlState.Normal);
                                           });
                    Control.Enabled = false;
                    Console.WriteLine("Playing iniciado");

                    MessagingCenter.Subscribe<StartRecordingButtonRenderer, NSUrl>(this, "audioFile", (sen, audioFile) =>
                      {
                         

                          Control.Enabled = true;
                          Control.TouchUpInside += (send, ebla) =>
                              {

                                  Control.SetTitle("Iniciado", UIControlState.Normal);
                                  AudioSession.Category = AudioSessionCategory.MediaPlayback;
                                  _player = new AVPlayer(audioFile);
                                  _player.Play();
                                  Console.WriteLine("Playing Back Recording " + audioFile.ToString());
                              };
                      });

                    //  Console.WriteLine("Playing Back Recording " + bla.AudioFilePath.ToString());


                }
            }
            catch (Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }
    }
}