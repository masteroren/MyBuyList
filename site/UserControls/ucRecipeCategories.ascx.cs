﻿using MyBuyList.BusinessLayer;
using MyBuyList.Shared;
using MyBuyList.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

public partial class ucRecipeCategories : System.Web.UI.UserControl
{
    int RecipeId
    {
        get { return ViewState["RecipeId"] == null ? 0 : (int)ViewState["RecipeId"]; }
        set { ViewState["RecipeId"] = value; }
    }

    SRL_RecipeCategory[] RecipeCategories
    {
        get { return (SRL_RecipeCategory[])ViewState["RecipeCategories"]; }
        set { ViewState["RecipeCategories"] = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (tvCategories.CheckedNodes.Count != 0)
        {
            SetCheckedCategory();
        }

        tvCategories.Nodes.Clear();

        categories[] categories = BusinessFacade.Instance.GetCategoriesList();
        //BuildTree(categories, null, null);

        tvCategories.ShowCheckBoxes = TreeNodeTypes.All;
        tvCategories.DataBind();

        updatePanel.Update();
    }

    public void storeRecipeCategories(int recipeId)
    {        
        if (recipeId != 0)
        {
            recipes recipe = BusinessFacade.Instance.GetRecipe(recipeId);
            if (recipe != null)
            {
                //var list = from item in recipe.categories
                //           select new SRL_RecipeCategory(recipe.RecipeId, item.CategoryId, item.CategoryName);
                //RecipeCategories = list.ToArray();
                //RecipeId = recipe.RecipeId;
            }
        }
    }

    public void ShowCategories(int recipeId, SRL_RecipeCategory[] recipeCategories)
    {
        this.RecipeId = recipeId;
        this.RecipeCategories = recipeCategories;

        this.tvCategories.Nodes.Clear();
        categories[] categories = BusinessFacade.Instance.GetCategoriesList();
        this.BuildTree(categories, null, null);

        this.tvCategories.ShowCheckBoxes = TreeNodeTypes.All;
        this.tvCategories.DataBind();
       
        this.updatePanel.Update();
        //ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "_onCatsPopupDisplay", "_onCategoriesLoaded();", true);
        this.mpeCategories.Show();
    }

    public void ShowCategories(SRL_RecipeCategory[] recipeCategories)
    {
        this.RecipeId = 0;
        this.RecipeCategories = recipeCategories;

        this.tvCategories.Nodes.Clear();
        categories[] categories = BusinessFacade.Instance.GetCategoriesList();
        this.BuildTree(categories, null, null);

        this.tvCategories.ShowCheckBoxes = TreeNodeTypes.All;
        this.tvCategories.DataBind();

        this.updatePanel.Update();
        this.mpeCategories.Show();
    }

    private void BuildTree(categories[] cats, int? parentCategoryId, TreeNode rootNode)
    {
        var list = cats.Where(c => c.ParentCategoryId == parentCategoryId);
        foreach (categories item in list)
        {
            TreeNode node = new TreeNode(item.CategoryName, item.CategoryId.ToString());
            if (this.RecipeCategories != null &&
                this.RecipeCategories.SingleOrDefault(rc => rc.RecipeId == this.RecipeId &&
                                                            rc.CategoryId == item.CategoryId) != null)
            {
                node.Checked = true;
            }

            if (rootNode == null)
            {
                this.tvCategories.Nodes.Add(node);
            }
            else
            {
                rootNode.ChildNodes.Add(node);
            }

            BuildTree(cats, item.CategoryId, node);
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        mpeCategories.Hide();
    }

    private void SetCheckedCategory()
    {
        List<SRL_RecipeCategory> list = new List<SRL_RecipeCategory>();
        foreach (TreeNode node in tvCategories.CheckedNodes)
        {
            SRL_RecipeCategory recipeCat = new SRL_RecipeCategory(RecipeId, int.Parse(node.Value), node.Text);
            list.Add(recipeCat);
        }
        RecipeCategories = list.ToArray();

        if (RefreshData != null)
        {
            RefreshData.Invoke(RecipeCategories);
        }
    }

    public delegate void RefreshHandler(SRL_RecipeCategory[] arr);
    public event RefreshHandler RefreshData;
}
