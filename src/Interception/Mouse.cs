using System;

namespace Interception
{
  class Mouse : Interception.Device
  {
    public Mouse() : base(Driver.interception_is_mouse)
    {
    }

    public override void ProcessListeners(Driver.Stroke stroke)
    {
      Driver.MouseStroke mouseStroke = stroke;

      foreach (Listener listener in listeners)
      {
        if (mouseStroke.state == listener.keyCode)
        {
          listener.operation(mouseStroke);
        }
      }
    }
  }
}