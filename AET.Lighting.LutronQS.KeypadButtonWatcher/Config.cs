using System.Collections.Generic;
using AET.Unity.SimplSharp;

namespace AET.Lighting.LutronQS.KeypadButtonWatcher {
  internal static class Config {
    static Config()  {
      ButtonConfig = new Dictionary<string, ButtonConfig>();      
    }

    public static Dictionary<string, ButtonConfig> ButtonConfig { get; set; }
  }
}