namespace MyBuyListShare.Models
{
    public class ListResponse<T>
    {
        public MetaData metaData { get; set; }
        public T results { get; set; }
    }
}