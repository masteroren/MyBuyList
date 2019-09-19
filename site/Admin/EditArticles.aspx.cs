using MyBuyList.BusinessLayer;
using MyBuyList.Shared;
using System;
using System.Collections.Generic;

public partial class Admin_EditArticles : BasePage
{
    const int FOOTER_ID = 6;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (((BasePage)this.Page).UserType != 1)
            {
                AppEnv.MoveToDefaultPage();
            }

            int id = -1;
            if (Request.QueryString["ArticleId"] != null && int.TryParse(Request.QueryString["ArticleId"], out id))
            {
            }
            else
            {
                id = FOOTER_ID;
            }

            articles article = BusinessFacade.Instance.GetArticleById(id);

            if (article != null)
            {
                this.RebindArticleData(article);
            }            

            articles[] allArticles = BusinessFacade.Instance.GetArticlesList();

            Dictionary<int, string> articleList = new Dictionary<int, string>();

            foreach (articles art in allArticles)
            {
                articleList.Add(art.ArticleId, art.Title);
            }
            articleList.Add(FOOTER_ID, BusinessFacade.Instance.GetArticleById(FOOTER_ID).Title);

            this.ddlArticles.DataSource = articleList;
            this.ddlArticles.DataTextField = "value";
            this.ddlArticles.DataValueField = "key";
            this.ddlArticles.DataBind();
            this.ddlArticles.SelectedValue = id.ToString();      
        }        
    }

    protected void ddlArticles_SelectedIndexChanged(object sender, EventArgs e)
    {
        articles article = BusinessFacade.Instance.GetArticleById(int.Parse(this.ddlArticles.SelectedValue));
        this.RebindArticleData(article);        
    }

    private void RebindArticleData(articles article)
    {
        if (article != null)
        {
            this.hfArticleId.Value = article.ArticleId.ToString();
            this.txtArticleTitle.Text = article.Title;
            this.txtPublisher.Text = article.Publisher;
            this.txtAbstract.Text = article.Abstract;            
            this.lblModifiedDate.Text = article.ModifiedDate.ToShortDateString();

            this.txtBody.Text = article.Body; //change to FCKEditor      
        }
        else
        {
            this.hfArticleId.Value = "-1";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        int returnedArticleId = -1;
        if (BusinessFacade.Instance.CreateOrUpdateArticle(int.Parse(hfArticleId.Value), txtArticleTitle.Text, txtAbstract.Text, txtBody.Text, txtPublisher.Text, out returnedArticleId))
        {
            if (returnedArticleId == FOOTER_ID)
            {
                Response.Redirect("~/");
            }
            else
            {
                Response.Redirect(string.Format("~/articles.aspx?ArticleId={0}", returnedArticleId));
            }
        }
    }
}
