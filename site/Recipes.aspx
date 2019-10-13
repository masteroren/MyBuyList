<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true" CodeFile="Recipes.aspx.cs" Inherits="Recipes" %>

<%@ MasterType VirtualPath="~/MasterPages/MBL.master" %>

<%@ Register Src="~/UserControls/ucSendMailToFriend.ascx" TagPrefix="uc1" TagName="SendToFriend" %>
<%@ Register Src="~/UserControls/ucRecipeCategories.ascx" TagPrefix="uc2" TagName="RecipeCategories" %>
<%@ Register Src="~/UserControls/ucShoppingList.ascx" TagName="ucShoppingList" TagPrefix="uc1" %>
<%@ Register Src="~/UserControls/AddRecipeButton.ascx" TagPrefix="uc1" TagName="AddRecipeButton" %>
<%@ Register Src="~/UserControls/ucRecipesFilter.ascx" TagPrefix="uc1" TagName="ucRecipesFilter" %>



<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <style>
        .recipes-filter {
            display: flex;
            flex-direction: column;
        }

        .categories-breadcrumbs {
            padding-top: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">

    <!-- Recipes -->

    <script>
        var RecipeIdClientID = '<%=hfRecipeId.ClientID%>';
        var RecipeNameClientID = '<%=hfRecipeName.ClientID%>';
    </script>

    <div id="reciep-wrapper">
        <%--<script type="text/javascript">
            function passParametersToSendToFriend(sender) {
                var recipeId = sender.getAttribute('recipeId');
                var recipeName = sender.getAttribute('recipeName');
                setParameters(recipeId, recipeName);
                showSendMailToFriendBox();
            }
        </script>--%>
        <div class="header">
            <div class="top" style="display: none">
                <asp:Image runat="server" ImageUrl="~/Images/Header_Recipes.png" />
            </div>
            <div class="search">
                <uc1:ucRecipesFilter runat="server" ID="ucRecipesFilter" />
                <uc1:AddRecipeButton runat="server" ID="AddRecipeButton" />
            </div>
            <div id="numResults">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lblNumRecipes" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <%--<uc2:RecipeCategories ID="ucRecipeCats" runat="server" OnRefreshData="ucRecipeCats_RefreshData" />--%>
        </div>
        <div class="body">
            <%--<div class="ShoppingList">
                <asp:UpdatePanel ID="upShoppingList" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <uc1:ucShoppingList ID="ucShoppingList1" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>--%>
            <div class="recipes">
                <asp:UpdatePanel ID="upRecipes" runat="server">
                    <ContentTemplate>
                        <div class="recipe_list">
                            <asp:Repeater ID="rptRecipes" runat="server">
                                <HeaderTemplate>
                                    <!-- pager -->
                                    <div id="topPager" class="pager">
                                        <asp:PlaceHolder ID="phPager" runat="server"></asp:PlaceHolder>
                                    </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="recipe-box-wrapper">
                                        <div class="recipe-category">
                                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/bgr_Header_BoxIndex_Right.png" />
                                            <asp:Label ID="lblMainCategory" runat="server" CssClass="category-name"></asp:Label>
                                            <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/bgr_Header_BoxIndex_Left.png" />
                                        </div>
                                        <div class="recipe_box">
                                            <div class="body">
                                                <div class="recipe_inner_box">
                                                    <div class="recipe_title">
                                                        <asp:HyperLink ID="lnkRecipe" runat="server" Text='<%# Eval("name") %>' NavigateUrl='<%# String.Format("RecipeDetails.aspx?RecipeId={0}", Eval("id")) %>'></asp:HyperLink>
                                                    </div>
                                                    <div class="recipe_actions_box">
                                                        <asp:LinkButton ID="ShoppingListAddRemove" runat="server"
                                                            CssClass="recipe_action" CausesValidation="True" OnClientClick='<%# String.Format("addRemoveRecipe(this, \"{0}\")",  Eval("id"))  %>' />
                                                    </div>
                                                    <div id="infoTags" style="clear: both; min-height: 30px; padding-top: 10px;">
                                                        <div id="myFavoritesInfoTag" runat="server" class="myFavoritesInfoTag" title="קיים במועדפים שלי">
                                                        </div>
                                                        <div id="allFavoritesInfoTag" title="גולשים אוהבים מתכון זה">
                                                            <div style="width: 15px; text-align: center;">
                                                                <asp:Label ID="lblAllFavorites" runat="server" />
                                                            </div>
                                                        </div>
                                                        <div id="allMenusInfoTag" title="תפריטים כוללים מתכון זה">
                                                            <div style="width: 16px; text-align: center;">
                                                                <asp:Label ID="lblAllMenus" runat="server" />

                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="recipe_tags">
                                                        <asp:Label ID="lblRecipeTags" runat="server" Text='<%# Eval("tags") %>'></asp:Label>
                                                    </div>
                                                    <div class="recipe_description">
                                                        <asp:Label ID="lblRecipeDescription" runat="server" Text='<%# Eval("description") %>'></asp:Label>
                                                    </div>
                                                    <div class="publisher_box">
                                                        <asp:Label ID="lblPublishedBy" runat="server" Text='פורסם ע"י'></asp:Label>
                                                        <asp:HyperLink ID="lnkPublisher" runat="server" CssClass="published_value" Text='<%# Eval("publishedBy") %>'></asp:HyperLink>&nbsp;&nbsp;&nbsp;
                                                    <div class="published_date">
                                                        <asp:Label ID="lblPublishedOn" runat="server" Text='בתאריך'></asp:Label>
                                                        <asp:Label ID="lblPublishDate" runat="server" Text='<%# Eval("createDate") %>' CssClass="published_value"></asp:Label>
                                                    </div>
                                                    </div>
                                                </div>
                                                <div class="recipe_thumbnail_box">
                                                    <div class="recipe_thumbnail">
                                                        <asp:Image ID="imgThumbnail" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </ItemTemplate>
                                <FooterTemplate>
                                    <!-- pager -->
                                    <div id="bottomPager" class="pager">
                                        <asp:PlaceHolder ID="phPager" runat="server"></asp:PlaceHolder>
                                    </div>
                                </FooterTemplate>
                            </asp:Repeater>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!-- Recipes -->

    <asp:HiddenField ID="hfRecipeId" runat="server" />
    <asp:HiddenField ID="hfRecipeName" runat="server" />

    <script src="Scripts/Recipes.js"></script>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
