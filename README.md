# AET.Lighting.LutronQS.KeypadButtonWatcher

### Provides a Crestron Simpl# library for watching buttons being pressed on LutronQS keypads and making them available to Crestron programs. 

[Download latest version here: v1.0.0](https://github.com/tony722/AET.Lighting.LutronQS.KeypadButtonWatcher/releases/download/v1.0.0/AET.Lighting.LutronQS.KeypadButtonWatcher.v1.0.0.zip)

### Requirements for Google Sheet.
This module relies on a Google Sheet for configuration. The sheet is downloaded by the processor, and cached in case the Internet is unavailable.
[See example Google Sheet here](https://docs.google.com/spreadsheets/d/1gqKyX94nDUOcLBB64_71M1E5LS90OLXfvoh7pt2Wmzc/edit?usp=sharing). 
To use this sheet as a template, open it and go to "File->Make A Copy" and save it in your own Google Drive.

* Row 1 is required: it is the column headers (the names of each column)
* Column A is required. Cell A1 must be "Keypads". _(All other cells in this column must be blank.)_
* Column B is required. It is the LutronQS Integration Id for the keypad and the button number (Id,button). e.g. 37,6
* Column C is required. It is the group number of output on the SIMPL+ module. 
* Column D is required. It is the output number in the group on the SIMPL+ module.

_As in the sample Google Sheet, you may have additional columns for documentation. They are ignored--but keep them to a minimum. Do not add additional rows above or below your data--use other sheets for additional documentation._

To publish a Google Sheet as a csv:
1. In Google Sheets go to File->Share->Publish to the Web. 
2. Under "Link" select the sheet you want to publish.
3. Under "Embed" select Comma-separated values (.csv) 
4. Hit Publish
5. Copy the provided URL and put it in the parameter in the SIMPL+ module.

### SIMPL# Pro
This module can be easily adapted to SIMPL# Pro. Look at the included SIMPL+ module to see what methods and delegates to utilize.

### Linked Libraries
Information: This module utilizes two libraries from the Unity framework:
* [AET.Unity.GoogleSheetsReader](https://github.com/tony722/Unity.GoogleSheetsReader)
* [AET.Unity.SimplSharp](https://github.com/tony722/Unity.SimplSharp)

### Support
For paid support on this module, or with any Crestron programming, you may contact me at http://www.iConsultants.net