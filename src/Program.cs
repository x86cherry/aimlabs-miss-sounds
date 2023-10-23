using System;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using Gma.System.MouseKeyHook;
using Interception;
using NAudio.Wave;

namespace Sounds
{
  class Program
  {
    static Audio audio = new Audio();
    static Mouse mouse = new Interception.Mouse();
    static DateTime lastHit = DateTime.Now;

    static void Main(string[] args)
    {
      // CaptureClicksUser32();
      CaptureClicks();
      CaptureAudio();
      Console.ReadLine();
    }

    static void CaptureClicks()
    {
      mouse.On((ushort)Interception.Driver.MouseState.LeftDown, (Interception.Driver.Stroke stroke) =>
      {
        PlayIfMiss();
      });
    }

    /**
    * the preferred method, but i can't get it to work somehow :\
    * interception works more easily, but it's a pain to set up for non-techies
    static void CaptureClicksUser32()
    {
      Hook.AppEvents().MouseDownExt += (sender, e) =>
      {
        PlayIfMiss();
      };
    }
    */

    static void CaptureAudio()
    {
      audio.StartCapture("AimLab_tb");
      audio.OnUpdate(() =>
      {
        lastHit = DateTime.Now;
      });
    }

    static void PlayIfMiss()
    {
      // it takes between 75ms and 150ms for the hit audio to get picked up
      // hope we can optimize this in the future
      Task.Delay(133).ContinueWith((task) =>
      {
        if (DateTime.Now.Subtract(lastHit).TotalMilliseconds > 50)
        {
          PlayMissSound();
        }
      });
    }

    static void PlayMissSound()
    {
      audio.PlayFile("miss.wav");
    }
  }
}