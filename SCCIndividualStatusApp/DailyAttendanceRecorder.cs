using System;

namespace SCCIndividualStatusApp
{
  /// <summary>
  /// 各日付ごとの情報
  /// </summary>
  internal class DailyAttendanceRecorder
  {
    /// <summary>
    /// 日付
    /// </summary>
    public DateTime Date { get; }

    /// <summary>
    /// その日に出席を取った回数
    /// </summary>
    public uint AttendanceCheckCounter { get; set; }

    public DailyAttendanceRecorder(DateTime date, uint attendanceCheckCounter = 0)
    {
      Date = date;
      AttendanceCheckCounter = attendanceCheckCounter;
    }
  }
}
