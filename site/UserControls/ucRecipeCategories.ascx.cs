using MyBuyListShare.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

public class SaveEventArgs
{
    public List<ShortCategoryModel> SelectedCategories;
}

public partial class ucRecipeCategories : System.Web.UI.UserControl
{
    public int RecipeId;
    public RecipeModel recipe;

    public delegate void SaveEventHandler(object sender, SaveEventArgs e);
    public event SaveEventHandler SaveClick;

    private List<ShortCategoryModel> recipeCategories;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (recipe != null)
        {
            recipeCategories = recipe.categories;
        }

        if (!IsPostBack)
        {
            tvCategories.Nodes.Clear();
            tvCategories.ShowCheckBoxes = TreeNodeTypes.All;
            Response<CategoryModel> response = HttpHelper.GetMeny<CategoryModel>("categories");
            List<CategoryModel> categories = response.results.ToList();
            BuildTree(categories, null, null);
        }
    }

    private void BuildTree(List<CategoryModel> categories, TreeNode rootNode, int? parentCategoryId)
    {
        List<CategoryModel> list = categories.Where(c => c.ParentCategoryId == parentCategoryId).ToList();

        list.ForEach(category =>
        {
            TreeNode node = new TreeNode(category.CategoryName, category.CategoryId.ToString());

            if (recipeCategories != null && recipeCategories.Find(a => a.categoryId == category.CategoryId) != null)
            {
                node.Checked = true;
            }

            if (rootNode == null)
            {
                tvCategories.Nodes.Add(node);
            }
            else
            {
                rootNode.ChildNodes.Add(node);
            }

            BuildTree(categories, node, category.CategoryId);
        });
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveClick != null)
        {
            List<ShortCategoryModel> selectedCategories = new List<ShortCategoryModel>();

            foreach (TreeNode node in tvCategories.CheckedNodes)
            {
                selectedCategories.Add(new ShortCategoryModel
                {
                    categoryId = Convert.ToInt32(node.Value),
                    categoryName = node.Text
                });
            }

            SaveClick(sender, new SaveEventArgs
            {
                SelectedCategories = selectedCategories
            });
        }

        mpeCategories.Hide();
    }
}
