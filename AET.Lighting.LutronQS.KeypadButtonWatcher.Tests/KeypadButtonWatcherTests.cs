using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AET.Lighting.LutronQS.KeypadButtonWatcher.Tests {
  [TestClass]
  public class KeypadButtonWatcherTests {
    [TestMethod]
    public void LutronRX_MatchingAddress_ButtonFires() {
      var csv = "Keypads,Address,Group,Output #,Output Desc,Label,Description\r\n,\"37,6\",2,20,Shade 20 - Dining room/1,Win Shade,MAIN\\Dining Room\\DINING @ foyer\r\n,\"39,6\",2,10,Shade 10 - Family Room/Kitchen door,Gr Door Shade,MAIN\\Kitchen\\KITCHEN @ nook patio\r\n,\"232,6\",1,12,Shades Grp 12 - Family Patio,Door Shade,MAIN\\Main Hallway\\HALLWAY @ theater";
      Config.ParseGoogleSheet(csv);
      var watcher = new KeypadButtonWatcher();
      ushort shade10Triggered = 0;
      watcher.SetGroup2 = (i, v) => { if (i == 10) shade10Triggered = v; };
      watcher.LutronRx("~DEVICE,39,6,3\x0D\x0A");
      shade10Triggered.Should().Be(1,"Because output 10 on group 2 is Shade 10, and the keypad button was pressed");
      watcher.LutronRx("~DEVICE,39,6,4\x0D\x0A");
      shade10Triggered.Should().Be(0, "Because output 10 on group 2 is Shade 10, and the keypad button was released");
    }
  }
}
