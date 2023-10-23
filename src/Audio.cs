using System;
using System.Threading;
using System.Collections.Generic;
using CSCore.CoreAudioAPI;
using NAudio.Wasapi;
using NAudio.Wave;

namespace Sounds
{
  class Audio
  {
    private List<Listener> listeners = new List<Listener>();
    public delegate void Listener();

    public void OnUpdate(Listener listener)
    {
      listeners.Add(listener);
    }

    public void StartCapture(string processName)
    {
      // i've got a nest for your nest for your nest for your
      Thread captureThread = new Thread(() =>
      {

        using (var sessionManager = GetDefaultAudioSessionManager2(DataFlow.Render))
        {
          using (var sessionEnumerator = sessionManager.GetSessionEnumerator())
          {
            while (true)
            {
              foreach (var session in sessionEnumerator)
              {
                using (var audioSessionControl2 = session.QueryInterface<AudioSessionControl2>())
                {
                  var name = audioSessionControl2.Process.ProcessName;

                  if (name != processName) continue;

                  using (var audioMeterInformation = session.QueryInterface<AudioMeterInformation>())
                  {
                    var value = audioMeterInformation.GetPeakValue();

                    if (value == 0) continue;

                    foreach (var listener in listeners)
                    {
                      listener();
                    }
                    break;
                  }
                }
              }
            }
          }
        }
      });

      captureThread.Start();
    }

    private static AudioSessionManager2 GetDefaultAudioSessionManager2(DataFlow dataFlow)
    {
      using (var enumerator = new MMDeviceEnumerator())
      {
        using (var device = enumerator.GetDefaultAudioEndpoint(dataFlow, Role.Multimedia))
        {
          var sessionManager = AudioSessionManager2.FromMMDevice(device);
          return sessionManager;
        }
      }
    }

    public void PlayFile(string path)
    {
      new Thread(() =>
      {
        var outputDevice = new DirectSoundOut();
        var audio = new AudioFileReader(path);
        outputDevice.Init(audio);
        outputDevice.Play();
      }).Start();
    }
  }
}