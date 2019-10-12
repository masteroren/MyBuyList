<%@ Page Language="C#" MasterPageFile="~/MasterPages/MBL.master" AutoEventWireup="true"
    CodeFile="RecipeDetails.aspx.cs" Inherits="PageRecipeDetails" Theme="Standard" %>

<%@ Register Src="~/UserControls/ucSendMailToFriend.ascx" TagPrefix="uc1" TagName="SendToFriend" %>
<%@ Register Src="~/UserControls/RecipeDetailsActions.ascx" TagPrefix="MBL" TagName="RecipeDetailsActions" %>
<%@ Register Src="~/UserControls/AddRecipeButton.ascx" TagPrefix="MBL" TagName="AddRecipeButton" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
    <meta name="Description" id="PageDescription" runat="server" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
    <script type="text/javascript">
        function AddRecipeToFavorites(recipeId) {
            PageMethods.AddRecipeToUserFavorites(recipeId, OnAddSucceeded, OnFailed);
        }
        function RemoveRecipeFromFavorites(recipeId) {
            PageMethods.RemoveRecipeFromFavorites(recipeId, OnRemoveSucceeded, OnFailed);
        }
        <%--function OnAddSucceeded(results) {
            $get('<%= btnAddRecipeToFavorites.ClientID %>').style.display = 'none';
        $get('<%= btnRemoveRecipeFromFavorites.ClientID %>').style.display = 'inline';
        }--%>
        <%--function OnRemoveSucceeded(results) {
            $get('<%= btnAddRecipeToFavorites.ClientID %>').style.display = 'inline';
            $get('<%= btnRemoveRecipeFromFavorites.ClientID %>').style.display = 'none';
        }--%>


        function OnFailed(results) {
        }


        function _onCategoriesLoaded() {
            var modalPopupBehavior = $find('child2');
            modalPopupBehavior.show();
            modalPopupBehavior._backgroundElement.style.zIndex = 300001;
            modalPopupBehavior._foregroundElement.style.zIndex = 300002;
        }

        function _onSelectPictureLoaded() {
            var iframe1 = document.getElementById('iframe1');
            iframe1.src = "SelectRecipePicture.aspx";
            var modalPopupBehavior = $find('child3');
            modalPopupBehavior.show();
            modalPopupBehavior._backgroundElement.style.zIndex = 300001;
            modalPopupBehavior._foregroundElement.style.zIndex = 300002;
        }

        // hide select picture modal
        function HideSelectPicture() {
            var modal = $find('child3');
            modal.hide();
        }

    </script>

    <script src="Scripts/RecipeDetails.js"></script>

    <div style="clear: both; height: 1px;">
    </div>
    <div class="recipe-details">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="recipe-name-wrapper">
                    <div class="recipe_name">
                        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/Header_Recipe.png" Style="margin-left: 11px;" />
                        <div>
                            <asp:Label ID="lblRecipeName" runat="server" />
                        </div>
                    </div>
                    <MBL:AddRecipeButton runat="server" ID="AddRecipeButton" />
                </div>
                <%--<asp:UpdatePanel ID="upTopTags" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>--%>
                        <div class="top-tags-wrapper">
                            <div class="top-tags">
                                <div id="myFavoritesTopTag" runat="server" class="myFavoritesTopTag">
                                </div>
                                <div id="allFavoritesTopTag">
                                    <div style="width: 20px; text-align: center;">
                                        <asp:Label ID="lblAllFavorites" runat="server" />
                                    </div>
                                </div>
                                <div id="allMenusTopTag">
                                    <div style="width: 20px; text-align: center;">
                                        <asp:Label ID="lblAllMenus" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
                <div style="height: 15px;"></div>
                <div class="recipe-wrapper">
                    <div class="recipe-wrapper__top">
                    </div>
                    <div class="recipe-wrapper__middle">
                        <div class="recipe-wrapper__middle__top">
                            <div class="right">
                                <div class="recipe-categories">
                                    <asp:Label ID="Label2" runat="server" Text="קטגוריות: " CssClass="green-title"></asp:Label>
                                    <asp:Label ID="lblRecipeCategories" runat="server" />
                                </div>
                                <div class="recipe-tags m-t-10">
                                    <asp:Label ID="Label3" runat="server" Text="תגיות: " CssClass="green-title"></asp:Label>
                                    <asp:Label ID="lblRecipeTags" runat="server" />
                                </div>
                                <div class="recipe-publisher-box m-t-10">
                                    <asp:Label ID="lblPublishedBy" runat="server" Text='פורסם ע"י' CssClass="m-l-5"></asp:Label>
                                    <asp:HyperLink ID="lnkPublisher" runat="server" NavigateUrl="" CssClass="published_value"></asp:HyperLink>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblPublishedOn" runat="server" Text='בתאריך' CssClass="m-l-5"></asp:Label>
                                    <asp:Label ID="lblPublishDate" runat="server" CssClass="published_value"></asp:Label>
                                </div>
                                <div class="recipe-description m-t-10">
                                    <asp:Label ID="Label1" runat="server" Text="תאור קצר:" CssClass="green-title"></asp:Label>
                                    <asp:Label ID="lblRecipeDescription" runat="server" Style="margin-top: 8px;"></asp:Label>
                                </div>
                                <div class="recipe-ingredients m-t-10">
                                    <asp:Label ID="Label4" runat="server" Text="החומרים הדרושים:" CssClass="green-title"></asp:Label>
                                    <div>
                                        <asp:TextBox ID="txtData" runat="server" Visible="False"></asp:TextBox>
                                        <asp:DataList ID="dlistIngredients" runat="server">
                                            <ItemTemplate>
                                                <%#DataBinder.Eval(Container.DataItem, "displayName")%>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </div>
                                </div>
                                <div class="recipe-tools m-t-10">
                                    <asp:Label ID="Label5" runat="server" Text="כלים:" CssClass="green-title"></asp:Label>
                                    <asp:Label ID="txtTools" runat="server"></asp:Label>
                                </div>
                                <div class="recipe-difficulty m-t-10">
                                    <asp:Label ID="Label6" runat="server" Text="דרגת קושי:" CssClass="green-title"></asp:Label>
                                    <asp:Label ID="txtDifficulty" runat="server" Style="min-height: 5px;"></asp:Label>
                                </div>
                            </div>
                            <div class="left">
                                <div class="left__top-actions">
                                    <MBL:RecipeDetailsActions runat="server" ID="RecipeDetailsActions" />

                                    <%--<div class="social-plugins">
                                        <div style="margin-top: -3px;">
                                            <div class="fb-like" data-send="false" data-layout="button_count" data-width="80"
                                                data-show-faces="true" data-href="<%=FBUrl %>">
                                            </div>
                                        </div>
                                    </div>--%>

                                </div>

                                <div class="recipe-picture">
                                    <div class="recipe-picture__tags">
                                        <div id="servingsInfoTag" class="tag">
                                            <asp:Label ID="lblServNumber" runat="server" />
                                        </div>
                                        <div id="prepTimeInfoTag" class="tag">
                                            <asp:Label ID="lblPrepTime" runat="server" />
                                        </div>
                                        <div id="cookTimeInfoTag" class="tag">
                                            <asp:Label ID="lblCookTime" runat="server" />
                                        </div>
                                    </div>
                                    <div>
                                        <asp:Image ID="imgRecipePicture" runat="server" BorderColor="#656565" BorderWidth="1px"
                                            BorderStyle="Solid" ImageUrl="~/Images/Img_Default.jpg" />
                                    </div>
                                </div>
                                <div id="recipe_video" runat="server" class="recipe_video">
                                </div>
                            </div>
                        </div>
                        <div class="recipe-wrapper__middle__bottom m-t-10">
                            <div class="recipe-instructions">
                                <asp:Label ID="Label7" runat="server" Text="אופן ההכנה:" CssClass="green-title"></asp:Label>
                                <asp:Label ID="txtPreparationMethod" runat="server"></asp:Label>
                            </div>
                            <div runat="server" class="recipe-remarks m-t-10">
                                <asp:Label ID="Label8" runat="server" Text="הערות:" CssClass="green-title"></asp:Label>
                                <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                            </div>
                            <div id="recipe_nutrients" style="display: none">
                                <asp:Image ID="nutValuesTitle" runat="server" ImageUrl="~/Images/SubHeader_Table_100gr.png"
                                    Style="margin-bottom: 15px;" />
                                <div>
                                    <asp:Label ID="txtNoDataForNutritionalValues" runat="server" Font-Bold="true"></asp:Label>
                                </div>
                                <div id="divNutritionalValues" runat="server" style="width: 900px; background-color: #f3f3f3;">
                                    <table style="width: 900px; height: 80px;" cellspacing="1px" cellpadding="2px">
                                        <tr>
                                            <asp:Repeater ID="rptNutritionalValues" runat="server" OnItemDataBound="NutValue_ItemDataBound">
                                                <ItemTemplate>
                                                    <td id="tdRepeater" runat="server" style="text-align: center; vertical-align: top; width: auto; padding: 8px 3px 3px 3px;">
                                                        <div style="vertical-align: top;">
                                                            <asp:Label ID="lblNutItemName1" runat="server" class="nutritional_value_name" Text='<%# Eval("NutItemName") %>' />
                                                        </div>
                                                        <div style="vertical-align: bottom; padding-top: 6px;">
                                                            <asp:Label ID="lblDisplayUnit1" CssClass="nutritional_value_unit" Style="color: white;"
                                                                runat="server" Text='<%# Eval("DisplayUnit") %>' />
                                                        </div>
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                        <tr>
                                            <asp:Repeater ID="rptNutritionalValues1" runat="server" OnItemDataBound="NutValue_ItemDataBound">
                                                <ItemTemplate>
                                                    <td id="tdRepeater" runat="server" style="text-align: center; vertical-align: top; padding-top: 7px; height: 34px;">
                                                        <asp:Label ID="lblDisplayTotalValue1" CssClass="nutritional_value_value" Style="color: white;"
                                                            runat="server" Text='<%# Eval("DisplayTotalValue") %>' />
                                                    </td>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>

                        <%--<div style="clear: both;">
                        </div>--%>
                        <!-- AddThis Button BEGIN -->
                        <%--<script type="text/javascript">
                            var addthis_config = { "data_track_clickback": true };
                        </script>
                        <div class="social-plugins" style="margin-left: 20px">--%>
                        <%--<div style="float: left; margin-top: -3px;">
                                <div class="fb-like" data-send="false" data-layout="button_count" data-width="80"
                                    data-show-faces="true" data-href="<%=FBUrl %>">
                                </div>
                            </div>--%>
                        <%--<div style="float: left;">
                                <div class="addthis_toolbox addthis_default_style" style="padding-left: 20px">
                                    <a href="http://www.addthis.com/bookmark.php?v=250&amp;username=dalit" class="addthis_button_compact"
                                        style="color: #fcab14; font-weight: bold">שתף</a> <span class="addthis_separator">|</span>
                                    <a class="addthis_button_facebook"></a><a class="addthis_button_myspace"></a><a class="addthis_button_google">
                                    </a><a class="addthis_button_twitter"></a>
                                </div>
                            </div>--%>
                        <%--<script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#username=dalit"></script>--%>
                        <!-- AddThis Button END -->
                        <%--<div style="height: 30px">
                            </div>--%>
                        <%--</div>--%>
                    </div>
                    <div class="wrapper_bottom_tab2">
                        <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/bgr_TabIndexReciepesRight.png" />
                        <MBL:RecipeDetailsActions runat="server" ID="RecipeDetailsActions2" />
                        <asp:Image runat="server" ImageUrl="~/Images/bgr_TabIndexReciepesLeft.png" />
                        <%--<table>
                            <tr>
                                <td style="width: 20px;">
                                    <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/bgr_TabIndexReciepesRight.png" />
                                </td>
                                <td class="wrapper_bottom_tab2_3">
                                    <MBL:RecipeDetailsActions runat="server" ID="RecipeDetailsActions1" />
                                </td>
                                <td style="width: 20px;">
                                    <asp:Image runat="server" ImageUrl="~/Images/bgr_TabIndexReciepesLeft.png" />
                                </td>
                            </tr>
                        </table>--%>
                    </div>

                    <div class="fb-comments" data-href="<%=Request.Url.AbsoluteUri.ToString() %>" data-numposts="10" data-width="100%" data-colorscheme="light"></div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <uc1:SendToFriend ID="ucSendMailToFriend" runat="server" OnEmailSent="ucSendMailToFriend_EmailSent" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <div style="margin-top: 10px;">
            <!--<div id="forum" style="margin-top: 0px; padding-right: 16px;">
                <div id="new_in_forum" style="float: right; margin-left: 15px;">
                    <asp:Image ID="imgNewInForum" runat="server" CssClass="header_new_in_forum" ImageUrl="~/Images/SubHeader_LastMessageRecipe.png" />
                    <div id="forum_top">
                    </div>
                    <div id="forum_container">
                        <div style="padding: 12px 32px;">
                            <iframe src="http://62.219.14.86/forum/default.aspx?fsid=matkon<%=this.RecipeId %>&srch=n&ttl=n"
                                id="forum_frame" scrolling="no" frameborder="0" style="height: 216px;"></iframe>
                            <%--<div style="clear: both; height: 15px; margin-top: 10px;">
                                <asp:HyperLink ID="lnkForum" runat="server" CssClass="recipe_forum_link" NavigateUrl="~/Forum.aspx"
                                    Text="כניסה לפורום >"></asp:HyperLink>
                            </div>--%>
                        </div>
                    </div>
                    <div id="forum_bottom">
                    </div>
                </div>
            </div>-->
            <%--<div id="recipe_bottom_left_corner" class="SideTable">
            </div>--%>
        </div>
    </div>

    <asp:HiddenField ID="hfRecipeId" runat="server" ClientIDMode="Static" />

</asp:Content>
