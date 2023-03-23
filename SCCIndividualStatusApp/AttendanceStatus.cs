using System;
using System.Collections.Generic;

namespace SCCIndividualStatusApp
{
  internal class AttendanceRecord
  {
    /// <summary>
    /// 日付
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// 出席情報
    /// </summary>
    public List<bool> StatusList { get; set; }

    /// <summary>
    /// テストの点数
    /// </summary>
    public int? TestResult { get; set; }
  }
}
