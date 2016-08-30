namespace HypermediaEngine.API.Infrastructure.Siren.Links
{
    public class PagedProperties
    {
        public static string GetPageDetails(int pageNumber, int pageSize, int totalEntries)
        {
            var pageAbsoluteIndexStart = (pageNumber * pageSize) + 1;

            var pageAbsoluteIndexEnd = (pageNumber * pageSize) + pageSize;
            if (totalEntries < pageAbsoluteIndexEnd)
                pageAbsoluteIndexEnd = totalEntries;

            return string.Format("Showing {0} to {1} of {2} entries", pageAbsoluteIndexStart, pageAbsoluteIndexEnd, totalEntries);
        }
    }
}