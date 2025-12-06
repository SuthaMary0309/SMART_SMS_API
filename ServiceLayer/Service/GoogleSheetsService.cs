using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

public class GoogleSheetsService
{
    private readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
    private readonly string ApplicationName = "SMART_SMS";
    private readonly string SpreadsheetId = "YOUR_SPREADSHEET_ID";
    private SheetsService _service;

    public GoogleSheetsService(string jsonPath)
    {
        GoogleCredential credential;
        using (var stream = new FileStream(jsonPath, FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream).CreateScoped(Scopes);
        }

        _service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName
        });
    }

    public void AddAttendance(string studentName, string date, string status)
    {
        var range = "Attendance!A:C"; // Sheet name and columns
        var valueRange = new ValueRange();
        var oblist = new List<object>() { studentName, date, status };
        valueRange.Values = new List<IList<object>> { oblist };

        var appendRequest = _service.Spreadsheets.Values.Append(valueRange, SpreadsheetId, range);
        appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
        appendRequest.Execute();
    }
}
