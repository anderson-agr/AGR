using System;
using System.Diagnostics;
using System.IO;
using AGR.ControlRenderer;
using AGR.iOS.Buttons;
using AudioToolbox;
using AVFoundation;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(StartRecordButton), typeof(StartRecordingButtonRenderer))]


namespace AGR.iOS.Buttons
{
    public class StartRecordingButtonRenderer : ButtonRenderer
    {
        AVAudioRecorder recorder;
        AVPlayer player;
        NSDictionary settings;
        Stopwatch stopwatch = null;
        NSUrl audioFilePath = null;
        NSObject observer;

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
                    Control.TouchUpInside+= (send, ebla) =>
                    {
                        if (Control.TitleLabel.Text == "Gravar")
                        {
                            Console.WriteLine("Begin Recording");

                            AudioSession.Category = AudioSessionCategory.RecordAudio;
                            AudioSession.SetActive(true);

                            if (!PrepareAudioRecording())
                            {
                                return;
                            }

                            if (!recorder.Record())
                            {
                                return;
                            }

                            this.stopwatch = new Stopwatch();
                            this.stopwatch.Start();

                            Control.SetTitle("Parar", UIControlState.Normal);
                        }
                        else
                        {

                            this.recorder.Stop();

                            Control.SetTitle(string.Format("{0:hh\\:mm\\:ss} Parado", this.stopwatch.Elapsed), UIControlState.Normal); ;
                            this.stopwatch.Stop();

                        }
                    };
                    Control.TitleLabel.Text = "TESTE";
                }
            }
            catch (Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }

        private void Control_TouchDown(object sender, EventArgs e)
        {
        
            if (Control.TitleLabel.Text == "Gravar")
            {
                   Console.WriteLine("Begin Recording");

            AudioSession.Category = AudioSessionCategory.RecordAudio;
            AudioSession.SetActive(true);

            if (!PrepareAudioRecording())
            {
                return;
            }

            if (!recorder.Record())
            {
                return;
            }

            this.stopwatch = new Stopwatch();
            this.stopwatch.Start();

                Control.SetTitle("Parar", UIControlState.Normal);
            }
            else
            {

                this.recorder.Stop();

                Control.SetTitle(string.Format("{0:hh\\:mm\\:ss} Parado", this.stopwatch.Elapsed), UIControlState.Normal); ;
                this.stopwatch.Stop();

            }



        }


        bool PrepareAudioRecording()
        {
            // You must initialize an audio session before trying to record
            var audioSession = AVAudioSession.SharedInstance();
            var err = audioSession.SetCategory(AVAudioSessionCategory.PlayAndRecord);
            if (err != null)
            {
                Console.WriteLine("audioSession: {0}", err);
                return false;
            }
            err = audioSession.SetActive(true);
            if (err != null)
            {
                Console.WriteLine("audioSession: {0}", err);
                return false;
            }

            // Declare string for application temp path and tack on the file extension
            string fileName = string.Format("Myfile{0}.aac", DateTime.Now.ToString("yyyyMMddHHmmss"));
            string tempRecording = Path.Combine(Path.GetTempPath(), fileName);

            Console.WriteLine(tempRecording);
            this.audioFilePath = NSUrl.FromFilename(tempRecording);

            //set up the NSObject Array of values that will be combined with the keys to make the NSDictionary
            NSObject[] values = new NSObject[]
            {
                NSNumber.FromFloat(44100.0f),
                NSNumber.FromInt32((int)AudioToolbox.AudioFormatType.MPEG4AAC),
                NSNumber.FromInt32(1),
                NSNumber.FromInt32((int)AVAudioQuality.High)
            };
            //Set up the NSObject Array of keys that will be combined with the values to make the NSDictionary
            NSObject[] keys = new NSObject[]
            {
                AVAudioSettings.AVSampleRateKey,
                AVAudioSettings.AVFormatIDKey,
                AVAudioSettings.AVNumberOfChannelsKey,
                AVAudioSettings.AVEncoderAudioQualityKey
            };
            //Set Settings with the Values and Keys to create the NSDictionary
            settings = NSDictionary.FromObjectsAndKeys(values, keys);

            //Set recorder parameters
            NSError error;
            recorder = AVAudioRecorder.Create(this.audioFilePath, new AudioSettings(settings), out error);
            if ((recorder == null) || (error != null))
            {
                Console.WriteLine(error);
                return false;
            }

            //Set Recorder to Prepare To Record
            if (!recorder.PrepareToRecord())
            {
                recorder.Dispose();
                recorder = null;
                return false;
            }

            recorder.FinishedRecording += delegate (object sender, AVStatusEventArgs e) {
                recorder.Dispose();
                recorder = null;
                Console.WriteLine("Done Recording (status: {0})", e.Status);
            };

            return true;
        }
    }
}