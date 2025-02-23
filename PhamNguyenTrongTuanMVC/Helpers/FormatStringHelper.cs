namespace PhamNguyenTrongTuanMVC.Helpers
{
    public static class FormatStringHelper
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
    }
}
