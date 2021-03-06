﻿using System;
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
        AVAudioRecorder _recorder;
        AVPlayer _player;
        NSUrl _audioFilePath;
        NSDictionary _settings;
        Stopwatch _stopwatch;

        NSObject _observer;
      
        public StartRecordingButtonRenderer()
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
                    Control.TouchUpInside += (send, ebla) =>
                     {
                         if (Control.TitleLabel.Text.Contains("Gravar"))
                         {
                             Console.WriteLine("Begin Recording");

                             AudioSession.Category = AudioSessionCategory.RecordAudio;
                             AudioSession.SetActive(true);

                             if (!PrepareAudioRecording()) return;
                             if (!_recorder.Record()) return;
                             _stopwatch = new Stopwatch();
                             _stopwatch.Start();

                             Control.SetTitle("Parar", UIControlState.Normal);
                         }
                         else
                         {
                             if (_recorder != null && !_recorder.Record()) return;
                             _recorder?.Stop();
                             Control.SetTitle($" Gravar {_stopwatch.Elapsed:hh\\:mm\\:ss} ", UIControlState.Normal);
                             _stopwatch.Stop();

                             MessagingCenter.Send<StartRecordingButtonRenderer, NSUrl>(this, "audioFile", _audioFilePath);

                         }
                     };
                    _observer = NSNotificationCenter.DefaultCenter.AddObserver(AVPlayerItem.DidPlayToEndTimeNotification, delegate
                    {
                        _player?.Dispose();
                        _player = null;
                    });


                }
            }
            catch (Exception exe)
            {
                throw new Exception(exe.Message);
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
            string fileName = $"Myfile{DateTime.Now.ToString("yyyyMMddHHmmss")}.aac";
            string tempRecording = Path.Combine(Path.GetTempPath(), fileName);

            Console.WriteLine(tempRecording);
            _audioFilePath = NSUrl.FromFilename(tempRecording);

           

            //set up the NSObject Array of values that will be combined with the keys to make the NSDictionary
            NSObject[] values = new NSObject[]
             {
                NSNumber.FromFloat(44100.0f),
                NSNumber.FromInt32((int)AudioFormatType.MPEG4AAC),
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
            _settings = NSDictionary.FromObjectsAndKeys(values, keys);

            //Set recorder parameters
            NSError error;
            _recorder = AVAudioRecorder.Create(_audioFilePath, new AudioSettings(_settings), out error);
            if ((_recorder == null) || (error != null))
            {
                Console.WriteLine(error);
                return false;
            }
            //Set Recorder to Prepare To Record
            if (!_recorder.PrepareToRecord())
            {
                _recorder.Dispose();
                _recorder = null;
                return false;
            }
            _recorder.FinishedRecording += delegate (object sender, AVStatusEventArgs e)
            {
                _recorder.Dispose();
                _recorder = null;
                Console.WriteLine("Done Recording (status: {0})", e.Status);
            };
            return true;
        }
    }
}