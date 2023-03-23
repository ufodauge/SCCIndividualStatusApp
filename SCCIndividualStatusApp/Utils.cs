using QuestPDF.Drawing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCCIndividualStatusApp
{
  public partial class MainForm : Form
  {
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

    private void PrintConsole(string text)
    {
      LogTextBox.Focus();
      LogTextBox.AppendText($"[{DateTime.Now:T}] {text}\n");
    }

  }
}

