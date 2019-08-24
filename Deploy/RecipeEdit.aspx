﻿<%@ page title="" language="C#" masterpagefile="~/MasterPages/MBL.master" enableeventvalidation="false" autoeventwireup="true" inherits="RecipeEdit, mybuylist" validaterequest="true" theme="Standard" %>

<%@ Register Src="~/UserControls/ucRecipe.ascx" TagPrefix="uc1" TagName="Recipe" %>
<%@ Register Src="~/UserControls/ucRecipeCategories.ascx" TagPrefix="uc2" TagName="RecipeCategories" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphMain" runat="Server">
   
    <div style="height: 75px;"></div>

    <uc1:Recipe ID="ucRecipe" runat="server" />    
    <uc2:RecipeCategories ID="ucRecipeCats" runat="server" OnRefreshData="RecipeCategories_RefreshData" />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphFooter" runat="Server">
</asp:Content>
