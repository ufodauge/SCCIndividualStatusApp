using System.Collections.Generic;
using System.Globalization;
using QuestPDF.Helpers;

namespace SCCIndividualStatusApp
{
  internal class CommonConstants
  {
    public static readonly int COL_STUDENT_NUMBER = 0;
    public static readonly int COL_STUDENT_NAME = 1;
    public static readonly int COL_DATE_STATUS_BEGIN = 2;
    public static readonly string STRING_ROUNDED_1 = "①";
    public static readonly string STRING_DEFAULT_FONT_FAMILY_NAME = "Noto Sans JP";
    public static readonly CultureInfo CULTUREINFO_JAPANESE = new CultureInfo("ja-JP");

    public static readonly Dictionary<string, PageSize> PAGESIZE_DICT = new Dictionary<string, PageSize>
    {
      ["A4"] = PageSizes.A4,
      ["B5"] = PageSizes.B5,
    };
  }
}
