using System;
using AET.Unity.SimplSharp;

namespace AET.Lighting.LutronQS.KeypadButtonWatcher {
  public class KeypadButtonWatcher {
    public SetUshortOutputArrayDelegate SetOutput { get; set; }
    
    public void LutronRx(string value) {
      //~DEVICE,96,1,3\x0D\x0A
      if (value.Substring(0, 8) == "~DEVICE,") HandleButton(value);
    }

    private void HandleButton(string value) {
      var parts = value.Split(',');
      var address = string.Concat(parts[1], ',', parts[2]);
      ButtonConfig button;
      if (!Config.ButtonConfig.TryGetValue(address, out button)) return;
      var pressState = (ushort)(parts[3].Substring(0, 1) == "3" ? 1 : 0);
      SetOutput(button.Output, pressState);
    }
    
    public void RegisterOutput(ushort output, string address) {
      var button = new ButtonConfig {
        Address = address,
        Output = output
      };
      Config.ButtonConfig.Add(address, button);
    }
  }
}