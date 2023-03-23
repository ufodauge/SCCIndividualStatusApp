using ExcelDataReader;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SCCIndividualStatusApp
{
  public partial class MainForm : Form
  {
    private void Process()
    {
      // 項目の入力チェック
      if (OutputFolderTextbox.Text.Length == 0)
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
      if (CommonConstants.PageSizeDict[PaperSizeComboBox.Text] == null)
        throw new Exception($"現在その用紙サイズはサポートされていません。");

      List<byte[]> excelFiles = new List<byte[]>();

      PrintConsole("データをダウンロードしています……");

      // スプレッドシートからエクセルファイルをエクスポート
      foreach (var url in urls)
      {
        string sheetId = url.Split('/')[5];
        excelFiles.Add(DownloadExcelFromSpreadsheetById(sheetId));

        PrintConsole($"'{sheetId}' からのダウンロードが完了しました。");
      }

      string path = OutputFolderTextbox.Text.Trim() + @"/";

      var studentDict = new Dictionary<string, Student>();

      foreach (var excelFile in excelFiles)
      {
        using (var stream = new MemoryStream(excelFile))
        {
          var reader = ExcelReaderFactory.CreateReader(stream);
          do
          {
            PrintConsole($"シート {reader.Name} の読み取り開始……");

            // クラスのシートでない場合はスキップ
            // シート名に"組"などの情報が含まれていたり、
            // 関係のないシートが数値に変換できたらおしまい
            // 現状識別方法がそれしかないのでしょうがない
            if (!int.TryParse(reader.Name, out int classroomNumber))
            {
              PrintConsole("スキップ");
              continue;
            }

            // SCC開講日一覧の取得
            // ヘッダーの読み取りは一回でいいが
            // 各クラスで情報が違うことも想定しうるので念のため
            var DailyAttendanceRecorderList = new List<DailyAttendanceRecorder>();
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

              DailyAttendanceRecorderList.Add(
                new DailyAttendanceRecorder(reader.GetDateTime(col)));
            }

            // 2行目（曜日、不要なのでスキップ）
            reader.Read();

            // 3行目（出席確認の回数）
            reader.Read();
            var enumerator = DailyAttendanceRecorderList.GetEnumerator();
            for (int col = CommonConstants.COL_DATE_STATUS_BEGIN; col < reader.FieldCount; col++)
            {
              // 空欄行であるか、
              // ①と書いてあるが日付の情報が無い（データ不整合）
              if (reader.GetValue(col) == null
                || (reader.GetValue(col).ToString() == CommonConstants.STRING_ROUNDED_1 && !enumerator.MoveNext()))
                break;

              maxDataCount = Math.Max(++enumerator.Current.AttendanceCheckCounter, maxDataCount);
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

              var student = new Student(
                  reader.GetValue(CommonConstants.COL_STUDENT_NAME).ToString(),
                  studentNumber,
                  classroomNumber);

              // 各日付について処理
              int currentCol = CommonConstants.COL_DATE_STATUS_BEGIN;
              foreach (var DailyAttendanceRecorder in DailyAttendanceRecorderList)
              {
                var attendanceStatus = new List<Boolean>();

                int? testResult = null;
                if (reader.GetValue(currentCol) != null)
                {
                  int.TryParse(reader.GetValue(currentCol).ToString(), out int _result);
                  testResult = _result;
                }

                for (int i = 0; i < DailyAttendanceRecorder.AttendanceCheckCounter; i++)
                  attendanceStatus.Add(reader.GetValue(currentCol++) != null);

                student.AttendanceRecordList.Add(new AttendanceRecord());
                student.AttendanceRecordList.Last().Date = DailyAttendanceRecorder.Date;
                student.AttendanceRecordList.Last().StatusList = attendanceStatus;
                student.AttendanceRecordList.Last().TestResult = testResult;
              }

              if (studentDict.ContainsKey(student.GetIdentificator()))
              {
                studentDict[student.GetIdentificator()]
                  .Merge(student);
              }
              else
              {
                studentDict.Add(
                  student.GetIdentificator(),
                  student);
              }
            }

          } while (reader.NextResult());

          reader.Close();
        }
      }

      // PDFに出力
      foreach (var student in studentDict.Values)
      {
        // クラスのフォルダを用意
        var classDirectory = path + $@"{student.ClassroomIndex}/";
        if (!Directory.Exists(classDirectory))
          Directory.CreateDirectory(classDirectory);

        var fileName = RemoveInvalidFileNameChars(
          student.GetDisplayName());

        PrintConsole($"ファイル '{fileName}' 生成中…");

        GenerateDocumentFromStudentData(student)
          .GeneratePdf(classDirectory + fileName + ".pdf");
      }
    }
  }
}
