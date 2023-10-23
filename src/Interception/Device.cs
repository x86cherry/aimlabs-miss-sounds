using System;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace Interception
{
  public abstract class Device
  {
    public List<Listener> listeners = new List<Listener>();
    public Driver.Stroke stroke = new Driver.Stroke();
    public IntPtr context = Driver.interception_create_context();
    public int device;

    public abstract void ProcessListeners(Driver.Stroke stroke);

    public Device(Driver.InterceptionPredicate filter)
    {
      Thread thread = new Thread(() =>
      {
        ElevateProcessPriority();
        Driver.interception_set_filter(context, filter, (ushort)Driver.FilterKeyState.All);
        CaptureInput();
      });
      thread.Start();
    }

    public void On(ushort keyCode, Listener.Operation operation)
    {
      listeners.Add(new Listener(keyCode, operation));
    }

    public void Send(Driver.Stroke stroke)
    {
      byte[] strokeBytes = Driver.getBytes(stroke);
      Driver.interception_send(context, device, strokeBytes, 1);
    }

    private void CaptureInput()
    {
      while (Driver.interception_receive(context, device = Driver.interception_wait(context), ref stroke, 1) > 0)
      {
        ProcessListeners(stroke);
        Send(stroke);
      }
    }

    private void ElevateProcessPriority()
    {
      Process process = Process.GetCurrentProcess();
      process.PriorityClass = ProcessPriorityClass.RealTime;
    }
  }

  public struct Listener
  {
    public delegate void Operation(Driver.Stroke stroke);
    public ushort keyCode;
    public Operation operation;

    public Listener(ushort keyCode, Operation operation)
    {
      this.keyCode = keyCode;
      this.operation = operation;
    }
  }
}