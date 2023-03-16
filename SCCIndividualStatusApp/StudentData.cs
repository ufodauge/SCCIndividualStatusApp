using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SCCIndividualStatusApp
{
  /// <summary>
  /// 生徒情報
  /// </summary>
  internal class StudentData
  {
    /// <summary>
    /// 生徒名
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 学年
    /// </summary>
    public int ClassNumber { get; private set; }

    /// <summary>
    /// 出席番号
    /// </summary>
    public int StudentNumber { get; private set; }

    /// <summary>
    /// 出席情報
    /// </summary>
    public List<AttendanceStatus> AttendanceStatusList { get; set; }

    public StudentData(string name, int studentNumber, int classNumber)
    {
      Name = name;
      StudentNumber = studentNumber;
      ClassNumber = classNumber;
      AttendanceStatusList = new List<AttendanceStatus>();
    }

    public double GetTestResultAverage()
    {
      int sum = 0;
      int testCount = 0;

      AttendanceStatusList.ForEach(attendanceStatus =>
      {
        if (attendanceStatus.TestResult != null)
        {
          testCount++;
          sum += (int)attendanceStatus.TestResult;
        }
      });

      try
      {
        return sum / testCount;
      }
      catch (DivideByZeroException)
      {
        return 0.0;
      }
    }
  }
}
