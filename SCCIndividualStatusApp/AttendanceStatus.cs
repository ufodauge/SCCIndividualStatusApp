using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SCCIndividualStatusApp
{
  internal class AttendanceStatus
  {
    /// <summary>
    /// 日付
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// 出席情報
    /// </summary>
    public List<Boolean> StatusList { get; set; }

    /// <summary>
    /// テストの点数
    /// </summary>
    public int? TestResult { get; set; }
  }
}
