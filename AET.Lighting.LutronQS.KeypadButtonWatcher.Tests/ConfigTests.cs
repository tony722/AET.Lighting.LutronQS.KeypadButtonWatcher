using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AET.Lighting.LutronQS.KeypadButtonWatcher.Tests {
  [TestClass()]
  public class ConfigTests {
    [TestMethod]
    public void ParseGoogleSheet_NullString_HandlesWithoutException() {
      Action act = () => Config.ParseGoogleSheet(null);
      act.Should().NotThrow<Exception>();
    }

    [TestMethod]
    public void ParseGoogleSheet_InvaldCSV_HandlesWithoutException() {
      Action act = () => Config.ParseGoogleSheet("1,2,3\r\n4,5,6");
      act.Should().NotThrow<Exception>();
    }

    [TestMethod]
    public void ParseGoogleSHeet_ValidCSV_ConfigIsPopulated() {
      var csv = "Keypads,Address,Group,Output #,Output Desc,Label,Description\r\n,\"37,6\",2,20,Shade 20 - Dining room/1,Win Shade,MAIN\\Dining Room\\DINING @ foyer\r\n,\"39,6\",2,10,Shade 10 - Family Room/Kitchen door,Gr Door Shade,MAIN\\Kitchen\\KITCHEN @ nook patio\r\n,\"232,6\",1,12,Shades Grp 12 - Family Patio,Door Shade,MAIN\\Main Hallway\\HALLWAY @ theater";
      Config.ParseGoogleSheet(csv);
      Config.ButtonConfig.Count.Should().Be(3);
      var button = Config.ButtonConfig["39,6"];
      button.Address.Should().Be("39,6");
      button.Group.Should().Be(2);
      button.Output.Should().Be(10);
    }
  }
}