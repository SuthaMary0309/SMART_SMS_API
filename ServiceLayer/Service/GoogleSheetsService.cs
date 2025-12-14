using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using ServiceLayer.ServiceInterFace;
using System.IO;
using System.Threading.Tasks;

public class GoogleSheetsService :IGoogleSheetService
{
    private readonly SheetsService _sheetService;
    private readonly string _spreadsheetId = "14YWpd6iYRXFHXNz2zk-VyICK-x48qE7NILMijEUvLS4"; // Sheet ID paste here

    public GoogleSheetsService()
    {
        GoogleCredential credential;

        using (var stream = new FileStream("smartsmsproject-696f0690fc20.json", FileMode.Open, FileAccess.Read))
        {
            credential = GoogleCredential.FromStream(stream)
                .CreateScoped(SheetsService.Scope.Spreadsheets);
        }

        _sheetService = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = "SMART_SMS",
        });
    }

    // ⬇ Append Row (Write Data)
    public async Task AppendRowAsync(string sheetName, IList<object> values)
    {
        var body = new ValueRange { Values = new List<IList<object>> { values } };

        var request = _sheetService.Spreadsheets.Values.Append(body, _spreadsheetId, sheetName);
        request.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;

        await request.ExecuteAsync();
    }

    // ⬇ Read rows
    public async Task<IList<IList<object>>> ReadRowsAsync(string range)
    {
        var request = _sheetService.Spreadsheets.Values.Get(_spreadsheetId, range);
        var response = await request.ExecuteAsync();
        return response.Values;
    }
}
