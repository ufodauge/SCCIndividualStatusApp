using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Windows.Forms;

namespace SCCIndividualStatusApp
{
  public partial class MainForm : Form
  {
    private const float ColumnDateSpanPx = 80f;
    private const float ColumnTestResultSpanPx = 50f;
    private const float ColumnAttendanceStatusSpanPx = 50f;
    private const uint HeaderAttendanceCheckCount = 2;

    private const float ColumnWidth =
      ColumnDateSpanPx +
      ColumnTestResultSpanPx +
      ColumnAttendanceStatusSpanPx * HeaderAttendanceCheckCount;

    private Document GenerateDocumentFromStudentData(Student student) =>
      Document.Create(container =>
      {
        container.Page(page =>
        {
          page.Size(CommonConstants.PageSizeDict[PaperSizeComboBox.Text]);
          page.Margin(2, Unit.Centimetre);
          page.PageColor(Colors.White);
          page.DefaultTextStyle(x => x
            .FontFamily(CommonConstants.STRING_DEFAULT_FONT_FAMILY_NAME)
            .FontSize(10));

          GenerateHeader(student, page);

          GenerateTable(student, page);
        });
      });

    private static void GenerateTable(Student student, PageDescriptor page)
    {
      page.Content()
        .PaddingVertical(1, Unit.Centimetre)
        .Width(ColumnWidth)
        .Column(x =>
        {
          GenerateTableHeader(x);
          GenerateTableContent(student, x);
          GenerateFooter(student, x);
        });
    }

    private static void GenerateTableHeader(ColumnDescriptor column) =>
      column.Item()
        .Border(0.5F)
        .Background("#EEE")
        .Row(row =>
        {
          row.ConstantItem(ColumnDateSpanPx)
             .Border(0.5F)
             .AlignCenter()
             .Text("日付");

          row.ConstantItem(ColumnTestResultSpanPx)
             .Border(0.5F)
             .AlignCenter()
             .Text("小テ");

          for (int i = 0; i < HeaderAttendanceCheckCount; i++)
          {
            row.ConstantItem(ColumnAttendanceStatusSpanPx)
               .Border(0.5F)
               .AlignCenter()
               .Text($"出欠{i + 1}");
          }
        });

    private static void GenerateTableContent(Student student, ColumnDescriptor x)
    {
      // 出欠状況
      foreach (var attendanceStatus in student.AttendanceRecordList)
      {
        x.Item()
          .Border(0.5F)
          .Row(row =>
          {
            row.ConstantItem(ColumnDateSpanPx)
               .Border(0.5F)
               .AlignCenter()
               .Text(
                 attendanceStatus.Date.ToString("M/d (ddd)",
                 CommonConstants.CULTUREINFO_JAPANESE));

            row.ConstantItem(ColumnTestResultSpanPx)
               .Border(0.5F)
               .AlignCenter()
               .Text(attendanceStatus.TestResult.ToString());

            foreach (var status in attendanceStatus.StatusList)
            {
              row.ConstantItem(ColumnAttendanceStatusSpanPx)
                 .Border(0.5F)
                 .AlignCenter()
                 .Text(status ? "" : "欠");
            }
          });
      }
    }

    private static void GenerateFooter(Student student, ColumnDescriptor x)
    {
      x.Item()
        .Border(0.5F)
        .Row(row =>
        {
          row.ConstantItem(ColumnDateSpanPx)
            .AlignCenter()
            .Text("平均点");

          row.ConstantItem(ColumnTestResultSpanPx)
            .AlignCenter()
            .Text(string.Format(
              "{0:f1}",
              student.GetTestResultAverage()));
        });
    }

    private static void GenerateHeader(Student student, PageDescriptor page)
      => page.Header()
             .AlignCenter()
             .Text(student.GetDisplayName())
             .SemiBold()
             .FontSize(18);
  }
}
