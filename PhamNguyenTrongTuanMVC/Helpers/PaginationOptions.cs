namespace PhamNguyenTrongTuanMVC.Helpers
{
    public class PaginationOptions
    {
        public static readonly string Pagination = "Pagination";

        public int PageSize { get; set; }

        public int[] PageSizeList { get; set; } = [];
    }
}
