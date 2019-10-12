using MySqlDataAccess;

namespace MyBuyListShare.Models
{
    public class CategoryModel : categories
    {
        public int recipes { get; set; }
    }

    public class ShortCategoryModel
    {
        public int categoryId { get; set; }
        public string categoryName { get; set; }
    }
}
