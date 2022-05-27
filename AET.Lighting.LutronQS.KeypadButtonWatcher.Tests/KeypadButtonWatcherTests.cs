using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AET.Lighting.LutronQS.KeypadButtonWatcher.Tests {
  [TestClass]
  public class KeypadButtonWatcherTests {
    [TestMethod]
    public void LutronRX_MatchingAddress_ButtonFires() {
      Config.ButtonConfig["39,6"] = new ButtonConfig { Address = "39,6", Output = 10 };
      var watcher = new KeypadButtonWatcher();
      ushort output10Triggered = 0;
      watcher.SetOutput = (i, v) => { if (i == 10) output10Triggered = v; };
      watcher.LutronRx("~DEVICE,39,6,3\x0D\x0A");
      output10Triggered.Should().Be(1, "Because the button for output 10 was pressed."); ;
      watcher.LutronRx("~DEVICE,39,6,4\x0D\x0A");
      output10Triggered.Should().Be(0, "Because the button for output 10 was released.");
    }
  }
}
