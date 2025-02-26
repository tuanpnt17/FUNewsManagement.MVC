namespace PhamNguyenTrongTuanMVC.Helpers
{
    public static class FormatHelper
    {
        public static string TruncateString(string? input, int wordCount)
        {
            if (input == null)
            {
                return string.Empty;
            }
            var words = input.Split(' ');
            if (words.Length <= wordCount)
            {
                return input;
            }
            return string.Join(" ", words.Take(wordCount)) + "...";
        }

        public static string FormatActualTime(DateTime dateTime, string format = "dd/MM/yyyy HH:mm")
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc).ToLocalTime().ToString(format);
        }
    }
}
