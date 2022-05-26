using System.Collections.Generic;
using AET.Unity.GoogleSheetsReader;
using AET.Unity.SimplSharp;

namespace AET.Lighting.LutronQS.KeypadButtonWatcher {
  internal static class Config {
    static Config()  {
      ButtonConfig = new Dictionary<string, ButtonConfig>();      
    }

    public static Dictionary<string, ButtonConfig> ButtonConfig { get; set; }
    public static string GoogleSheetsPublishedCsvUrl { get; set; }
    public static string CacheFilename { get; set; }

    public static void Initialize(string googleSheetsPublishedCsvUrl, string cacheFilename) {
      GoogleSheetsPublishedCsvUrl = googleSheetsPublishedCsvUrl;
      CacheFilename = cacheFilename;
      ReadGoogleSheet();
    }

    public static void ReadGoogleSheet() {
      var reader = new GoogleReader(GoogleSheetsPublishedCsvUrl, CacheFilename);
      var csv = reader.ReadPublishedGoogleSheetCsv();
      ParseGoogleSheet(csv);
    }

    public static void ParseGoogleSheet(string csv) {
      if (string.IsNullOrEmpty(csv)) return;
      var sheet = new SheetReader().ReadCsvText(csv);
      Section section;
      if (!sheet.Sections.TryGet("Keypads", out section)) {
        ErrorMessage.Error(@"LutronQS.KeypadButtonWatcher: Supplied sheet did not contain section ""Keypads""");
        return;
      }
      ButtonConfig.Clear();
      foreach (var row in section.Rows) {
        var buttonConfig = new ButtonConfig {
          Address = row[0],
          Group = row[1].SafeParseUshort(),
          Output = row[2].SafeParseUshort()
        };
        ButtonConfig.Add(buttonConfig.Address, buttonConfig);
      }
    }
  }
}