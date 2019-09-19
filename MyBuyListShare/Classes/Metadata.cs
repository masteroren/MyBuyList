namespace MyBuyListShare.Models
{
    public class MetaData
    {
        private int startIndex;
        private int endIndex;

        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int pages { get; set; }
        public int totalItems { get; set; }
        public string searchQuery { get; set; }

        public MetaData(int _totalItems, int _pageSize, int _pageIndex, string _searchQuery)
        {
            if (_totalItems == 0)
            {
                return;
            }

            totalItems = _totalItems;
            pageSize = _pageSize;
            pageIndex = _pageIndex;
            searchQuery = _searchQuery;

            pages = totalItems / pageSize;
            if (totalItems % pageSize != 0)
            {
                pages += 1;
            }
            if (pageIndex < 0)
            {
                pageIndex = 0;
            }
            if (pageIndex > pages)
            {
                pageIndex = pages;
            }
            startIndex = pageSize * pageIndex;
            endIndex = startIndex + pageSize - 1;
        }

        public int getStartIndex()
        {
            return startIndex;
        }
    }
}
