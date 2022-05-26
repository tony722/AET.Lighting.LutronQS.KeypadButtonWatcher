using System;
using System.Collections.Generic;
using AET.Unity.GoogleSheetsReader;
using AET.Unity.SimplSharp;
using Crestron.SimplSharp.CrestronIO;

namespace AET.Lighting.LutronQS.KeypadButtonWatcher {
  public class KeypadButtonWatcher {
    private AnyKeyDictionary<ushort, SetUshortOutputArrayDelegate> triggerArray = new AnyKeyDictionary<ushort, SetUshortOutputArrayDelegate>();

    public SetUshortOutputArrayDelegate SetGroup1 { set { triggerArray[1] = value; } get { return triggerArray[1]; } }
    public SetUshortOutputArrayDelegate SetGroup2 { set { triggerArray[2] = value; } get { return triggerArray[2]; } }
    public SetUshortOutputArrayDelegate SetGroup3 { set { triggerArray[3] = value; } get { return triggerArray[3]; } }
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
      triggerArray[button.Group](button.Output, pressState);
    }

    public void Initialize(string googleSheetsPublishedCsvUrl, string cacheFilename) {
      Config.Initialize(googleSheetsPublishedCsvUrl, cacheFilename);
    }

    public void Reload() {
      Config.ReadGoogleSheet();
    }
  }
}