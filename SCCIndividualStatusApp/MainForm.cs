using System;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SCCIndividualStatusApp
{
  public partial class MainForm : Form
  {
    public MainForm()
    {
      InitializeComponent();
    }

    /// <summary>
    /// フォーム起動時の処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
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

    /// <summary>
    /// PDF生成ボタン
    /// </summary>
    private void GeneratePdfButton_Click(object sender, EventArgs e)
    {
      try
      {
        //await Task.Run(() =>
        //{
          Process();
          PrintConsole("正常に処理が完了しました。");
        //});
      }
      catch (Exception ex)
      {
        PrintConsole(ex.ToString());
        PrintConsole("処理を中断しました。");
      }
    }

    /// <summary>
    /// 参照ボタンクリック時の処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void BrowseOutputFolderButton_Click(object sender, EventArgs e)
    {
      //フォルダを選択するダイアログを表示する
      if (OutputFolderBrowserDialog.ShowDialog() == DialogResult.OK)
      {
        OutputFolderTextbox.Text = OutputFolderBrowserDialog.SelectedPath;
      }
    }
  }
}
