using System.Collections.Generic;

namespace SCCIndividualStatusApp
{
  /// <summary>
  /// 生徒情報
  /// </summary>
  internal class Student
  {
    /// <summary>
    /// 生徒名
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// 学級
    /// </summary>
    public int ClassroomIndex { get; private set; }

    /// <summary>
    /// 出席番号
    /// </summary>
    public int StudentIndex { get; private set; }

    /// <summary>
    /// 出席情報
    /// </summary>
    public List<AttendanceRecord> AttendanceRecordList { get; set; }

    public Student(string name, int studentNumber, int classroomNumber)
    {
      Name = name;
      StudentIndex = studentNumber;
      ClassroomIndex = classroomNumber;
      AttendanceRecordList = new List<AttendanceRecord>();
    }

    public string GetDisplayName() =>
            $"{ClassroomIndex}組{StudentIndex}番 {Name}";

    public string GetIdentificator() =>
        $"{ClassroomIndex}-{StudentIndex}";

    public void Merge(Student source) =>
        AttendanceRecordList.AddRange(source.AttendanceRecordList);

    public double GetTestResultAverage()
    {
      int sum = 0;
      int testCount = 0;

      foreach (var attendanceRecord in AttendanceRecordList)
      {
        if (attendanceRecord.TestResult.HasValue)
        {
          testCount++;
          sum += attendanceRecord.TestResult.Value;
        }
      };

      return testCount > 0 ? sum / testCount : 0.0;
    }
  }
}
