using System.Globalization;

namespace Smart_Warehouse.Commons
{
    public static class Common
    {
        public const string ServerAPI = "ServerAPI";
        public const string Format_yyyyMMdd = "yyyy-MM-dd HH:mm:ss";
        public const string Format_yyyyddMM = "MM-dd-yyyy HH:mm:ss";
        public const string FormatNoTime_yyyMMdd = "yyyy-MM-dd";
        public const string FormatNoTime_yyyddMM = "MM-dd-yyyy";
        public const string FormatNoTime_ddMMyyyy = "dd/MM/yyyy";

        public static DateTime? ParseDate_ddMMyyyy(string? input)
        {
            string[] formats = { "d/M/yyyy", "dd-MM-yyyy", "dd/MM/yyyy", "yyyy/MM/dd", "dd-MM-yy", "dd/MM/yy", "ddMMyyyy", "ddMMyy", "dd-MM", "dd/MM", "ddMM" };

            foreach (var format in formats)
            {
                if (DateTime.TryParseExact(input?.Trim(), format, null, DateTimeStyles.None, out var result))
                {
                    return result;
                }
            }

            return null;
        }
    }
}
