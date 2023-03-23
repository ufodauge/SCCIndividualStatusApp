using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using ExcelDataReader;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;


namespace SCCIndividualStatusApp
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
    }

    /// <summary>
    /// PDF生成ボタン
    /// </summary>
    private void GeneratePdfButton_Click(object sender, EventArgs e)
    {
      try
      {
        Process();
        ConsoleLog("正常に処理が完了しました。");
      }
      catch (Exception ex)
      {
        ConsoleLog(ex.ToString());
        ConsoleLog("処理を中断しました。");
      }
    }

    private void BrowseOutputFolderButton_Click(object sender, EventArgs e)
    {
      //フォルダを選択するダイアログを表示する
      if (OutputFolderBrowserDialog.ShowDialog() == DialogResult.OK)
      {
        OutputFolderTextbox.Text = OutputFolderBrowserDialog.SelectedPath;
      }
    }

    private void Process()
    {
      // 項目の入力チェック
      if (OutputFolderTextbox.Text == "")
      {
        throw new Exception("出力フォルダを選択してください。");
      }

      // URLのバリデーション
      string[] urls = SpreadsheetUrlRichTextBox.Text.Trim(' ', '\n').Split('\n');

      foreach (var url in urls)
      {
        if (!Uri.TryCreate(url, UriKind.Absolute, out Uri _))
          throw new Exception($"入力されたURLが不正です: {url}");
      }

      // 用紙サイズのバリデーション
      if (CommonConstants.PAGESIZE_DICT[PaperSizeComboBox.Text] == null)
        throw new Exception($"現在その用紙サイズはサポートされていません。");

      List<byte[]> excelFiles = new List<byte[]>();

      ConsoleLog("データをダウンロードしています……");

      // スプレッドシートからエクセルファイルをエクスポート
      foreach (var url in urls)
      {
        string sheetId = url.Split('/')[5];
        excelFiles.Add(DownloadExcelFromSpreadsheetById(sheetId));

        ConsoleLog($"'{sheetId}' からのダウンロードが完了しました。");
      }

      string path = OutputFolderTextbox.Text.Trim() + @"/";

      var studentDataDict = new Dictionary<string, StudentData>();

      foreach (var excelFile in excelFiles)
      {
        using (var stream = new MemoryStream(excelFile))
        {
          var reader = ExcelReaderFactory.CreateReader(stream);
          do
          {
            ConsoleLog($"シート {reader.Name} の読み取り開始……");

            // クラスのシートでない場合はスキップ
            // シート名に"組"などの情報が含まれていたり、
            // 関係のないシートが数値に変換できたらおしまい
            // 現状識別方法がそれしかないのでしょうがない
            if (!int.TryParse(reader.Name, out int classNumber))
            {
              ConsoleLog("スキップ");
              continue;
            }

            // SCC開講日一覧の取得
            // ヘッダーの読み取りは一回でいいが
            // 各クラスで情報が違うことも想定しうるので念のため
            var dateStatusList = new List<DateStatus>();
            uint maxDataCount = 0;

            // 1行目（日付）
            // 日付情報を取得
            reader.Read();
            for (int col = CommonConstants.COL_DATE_STATUS_BEGIN; col < reader.FieldCount; col++)
            {
              if (reader.GetFieldType(col) != typeof(DateTime))
                continue;

              // 今日以降の日付を取得して処理をする必要はない
              if (IncludeTodayCheckBox.Checked && DateTime.Today < reader.GetDateTime(col))
                break;
              else if (DateTime.Today <= reader.GetDateTime(col))
                break;

              dateStatusList.Add(
                new DateStatus(reader.GetDateTime(col)));
            }

            // 2行目（曜日、不要なのでスキップ）
            reader.Read();

            // 3行目（出席確認の回数）
            reader.Read();
            var enumerator = dateStatusList.GetEnumerator();
            for (int col = CommonConstants.COL_DATE_STATUS_BEGIN; col < reader.FieldCount; col++)
            {
              // 空欄行であるか、
              // ①と書いてあるが日付の情報が無い（データ不整合）
              if (reader.GetValue(col) == null
                || (reader.GetValue(col).ToString() == CommonConstants.STRING_ROUNDED_1 && !enumerator.MoveNext()))
                break;

              maxDataCount = Math.Max(++enumerator.Current.DataCount, maxDataCount);
            }
            enumerator.Dispose();

            // 以降すべての行について、生徒のデータがある限りは読み取り
            while (
              reader.Read() &&
              reader.GetValue(CommonConstants.COL_STUDENT_NUMBER) != null)
            {
              if (!int.TryParse(reader.GetValue(CommonConstants.COL_STUDENT_NUMBER).ToString(), out int studentNumber))
                throw new Exception("スプレッドシートのフォーマットが変更されています。" +
                  "フォーマットを以前のものにするか、システムの変更を依頼して下さい。");

              var studentData = new StudentData(
                  reader.GetValue(CommonConstants.COL_STUDENT_NAME).ToString(),
                  studentNumber,
                  classNumber);

              // 各日付について処理
              int currentCol = CommonConstants.COL_DATE_STATUS_BEGIN;
              foreach (var dateStatus in dateStatusList)
              {
                var attendanceStatus = new List<Boolean>();

                int? testResult = null;
                if (reader.GetValue(currentCol) != null)
                {
                  int.TryParse(reader.GetValue(currentCol).ToString(), out int _result);
                  testResult = _result;
                }

                for (int i = 0; i < dateStatus.DataCount; i++)
                  attendanceStatus.Add(reader.GetValue(currentCol++) != null);

                studentData.AttendanceStatusList.Add(new AttendanceStatus());
                studentData.AttendanceStatusList.Last().Date = dateStatus.Date;
                studentData.AttendanceStatusList.Last().StatusList = attendanceStatus;
                studentData.AttendanceStatusList.Last().TestResult = testResult;
              }

              if (studentDataDict.ContainsKey(studentData.GetIdentificator()))
              {
                studentDataDict[studentData.GetIdentificator()]
                  .MergeStudentData(studentData);
              }
              else
              {
                studentDataDict.Add(
                  studentData.GetIdentificator(),
                  studentData);
              }
            }

          } while (reader.NextResult());

          reader.Close();
        }
      }

      // PDFに出力
      foreach (var studentData in studentDataDict.Values)
      {
        // クラスのフォルダを用意
        var classDirectory = path + $@"{studentData.ClassNumber}/";
        if (!Directory.Exists(classDirectory))
          Directory.CreateDirectory(classDirectory);

        var fileName = RemoveInvalidFileNameChars(
          studentData.GetDisplayName());

        ConsoleLog($"ファイル '{fileName}' 生成中…");

        GenerateDocumentFromStudentData(studentData)
          .GeneratePdf(classDirectory + fileName + ".pdf");
      }
    }

    private Document GenerateDocumentFromStudentData(StudentData studentData)
    {
      return Document.Create(container =>
      {
        container.Page(page =>
        {
          page.Size(CommonConstants.PAGESIZE_DICT[PaperSizeComboBox.Text]);
          page.Margin(2, Unit.Centimetre);
          page.PageColor(Colors.White);
          page.DefaultTextStyle(x => x
            .FontFamily(CommonConstants.STRING_DEFAULT_FONT_FAMILY_NAME)
            .FontSize(12));

          page.Header()
            .AlignCenter()
            .Text($"{studentData.ClassNumber}組{studentData.StudentNumber}番 {studentData.Name}")
            .SemiBold()
            .FontSize(20);

          page.Content()
            .PaddingVertical(1, Unit.Centimetre)
            .Column(x =>
            {
              const float COL_DATE_SPAN_PX = 80F;
              const float COL_TEST_REESUT_SPAN_PX = 50F;
              const float COL_ATTENDANCE_STATUS_SPAN_PX = 50F;

              const uint HEADER_ATTENDANCE_CHECK_COUNT = 2;

              x.Item()
                .Border(0.5F)
                .Background("#DDD")
                .Row(row =>
                {
                  row.ConstantItem(COL_DATE_SPAN_PX)
                     .Border(0.5F)
                     .AlignCenter()
                     .Text("日付");

                  row.ConstantItem(COL_TEST_REESUT_SPAN_PX)
                     .Border(0.5F)
                     .AlignCenter()
                     .Text("小テ");

                  for (int i = 0; i < HEADER_ATTENDANCE_CHECK_COUNT; i++)
                    row.ConstantItem(COL_ATTENDANCE_STATUS_SPAN_PX)
                       .Border(0.5F)
                       .AlignCenter()
                       .Text($"出欠{i + 1}");
                });

              // 出欠状況
              foreach (var attendanceStatus in studentData.AttendanceStatusList)
              {
                x.Item()
                  .Border(0.5F)
                  .Row(row =>
                  {
                    row.ConstantItem(COL_DATE_SPAN_PX)
                        .Border(0.5F)
                       .AlignCenter()
                       .Text(
                         attendanceStatus.Date.ToString("M/d (ddd)",
                         CommonConstants.CULTUREINFO_JAPANESE));

                    row.ConstantItem(COL_TEST_REESUT_SPAN_PX)
                        .Border(0.5F)
                        .AlignCenter()
                       .Text(attendanceStatus.TestResult.ToString());

                    foreach (var status in attendanceStatus.StatusList)
                      row.ConstantItem(COL_ATTENDANCE_STATUS_SPAN_PX)
                         .Border(0.5F)
                         .AlignCenter()
                         .Text(status ? "" : "欠");
                  });
              }

              x.Item()
                .Border(0.5F)
                .Row(row =>
                {
                  row.ConstantItem(COL_DATE_SPAN_PX)
                    .AlignCenter()
                    .Text("平均点");

                  row.ConstantItem(COL_TEST_REESUT_SPAN_PX)
                    .AlignCenter()
                    .Text(string.Format(
                      "{0:f1}",
                      studentData.GetTestResultAverage()));
                });
            });
        });
      });
    }

    private void FontLoader(string resourceName)
    {
      using (var stream = Assembly
        .GetExecutingAssembly()
        .GetManifestResourceStream(resourceName))
      {
        if (stream == null)
          throw new ArgumentException("フォント読み込みエラー：開発者に相談してください" + resourceName);

        FontManager.RegisterFont(stream);
      }
    }

    private byte[] DownloadExcelFromSpreadsheetById(string sheetId)
    {
      return DownloadFile($@"https://docs.google.com/spreadsheets/d/{sheetId}/export?format=xlsx");
    }

    private byte[] DownloadFile(string url)
    {
      using (var client = new WebClient())
      {
        return client.DownloadData(url);
      }
    }

    private string RemoveInvalidFileNameChars(string fileName)
    {
      var invalidChars = Path.GetInvalidFileNameChars();
      var sb = new StringBuilder(fileName);

      foreach (var c in invalidChars)
      {
        sb.Replace(c.ToString(), "");
      }

      return sb.ToString();
    }

    private void ConsoleLog(string log)
    {
      LogTextBox.Focus();
      LogTextBox.AppendText($"[{DateTime.Now:T}] {log}\n");
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      // PDF用のフォント読み込み
      FontLoader("SCCIndividualStatusApp.Resource.Font.NotoSansJP-Medium.otf");
      SpreadsheetUrlRichTextBox.Text =
        "https://docs.google.com/spreadsheets/d/XXXXXXXXXX\n" +
        "https://docs.google.com/spreadsheets/d/YYYYYYYYYY\n" +
        "...";
      PaperSizeComboBox.Text = "A4";
    }
  }
}
