using System.Reactive.Subjects;

namespace MyBuyListShare.Services
{
    public static class SearchService
    {
        public static readonly Subject<SearchType> Search = new Subject<SearchType>();

        public static void DoSearch(SearchType value)
        {
            Search.OnNext(value);
        }
    }
}
