using System;

namespace SCCIndividualStatusApp
{
  /// <summary>
  /// 各日付ごとの情報
  /// </summary>
  internal class DateStatus
  {
    /// <summary>
    /// 日付
    /// </summary>
    public DateTime Date { get; private set; }

    /// <summary>
    /// その日に出席を取った回数
    /// </summary>
    public uint DataCount { get; set; }

    public DateStatus(DateTime date, uint dataCount)
    {
      Date = date;
      DataCount = dataCount;
    }

    public DateStatus(DateTime date)
    {
      Date = date;
      DataCount = 0;
    }
  }
}
