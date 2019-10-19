<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Ingridiants.ascx.cs" Inherits="UserControls_Ingridiants" %>

<div class="ingridiants">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" RenderMode="Inline">
        <ContentTemplate>
            <div class="input-section">
                <div class="input-wrapper">
                    <div class="input-section-item quantity0">
                        <asp:Label ID="lblQuantity" runat="server" Text="<%$ Resources:MyGlobalResources, Quantity %>"
                            Font-Bold="true" />
                        <asp:TextBox ID="TextBox1" runat="server" Width="40"></asp:TextBox>
                    </div>
                    <div class="input-section-item quantity1">
                        <asp:Label ID="lblAnd" runat="server" Text="<%$ Resources:MyGlobalResources, And %>"
                            Font-Bold="true" />
                        <asp:DropDownList ID="ddlFractions" runat="server" CssClass="drop-down-fractions"></asp:DropDownList>
                    </div>
                    <div class="input-section-item unit">
                        <asp:Label ID="lblMeasurementUnits" runat="server" Text="<%$ Resources:MyGlobalResources, MeasurementUnit %>"
                            Font-Bold="true" />
                        <asp:DropDownList ID="ddlMeasurementUnits" class="drop-down-measurement-units" runat="server"></asp:DropDownList>
                    </div>
                    <div class="input-section-item name">
                        <asp:Label ID="lblFoodName" runat="server"
                            Text="<%$ Resources:MyGlobalResources, IngridiantName %>" Font-Bold="true" />
                        <asp:TextBox ID="IngridiantName" runat="server"></asp:TextBox>
                        <ajaxToolkit:AutoCompleteExtender
                            ID="AutoCompleteExtender1"
                            TargetControlID="IngridiantName"
                            ServiceMethod="GetIngrediants"
                            ServicePath="WebServices/AutoComplete.asmx"
                            MinimumPrefixLength="3"
                            EnableCaching="true"
                            CompletionSetCount="10"
                            OnClientItemSelected="ingrediantSelected"
                            runat="server">
                        </ajaxToolkit:AutoCompleteExtender>
                        <asp:HiddenField ID="IngridiantId" ClientIDMode="Static" runat="server" />
                    </div>
                    <div class="input-section-item remark">
                        <asp:Label ID="lblFoodRemark" runat="server" Text="<%$ Resources:MyGlobalResources, IngridiantProcess %>" Font-Bold="true" />
                        <asp:TextBox ID="txtFoodRemark" CssClass="food-process" runat="server" />
                    </div>
                </div>
                <div class="actions">
                    <asp:ImageButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" CssClass="hide-on-logout" ImageUrl="~/Images/btn_AddProduct_up.png" />
                    <asp:ImageButton ID="ImageButton2" runat="server" OnClick="ImageButton2_Click" ImageUrl="~/Images/btn_EditProduct_up.png" Visible="false" />
                </div>
            </div>
            <div id="divIngredients" runat="server" style="clear: both; margin-top: 4px;">
                <p style="font-weight: bold;">
                    מצרכים שהוספת
                </p>
            </div>
            <div>
                <asp:Panel ID="ingredientsContainer" runat="server" CssClass="ingredients-container ingredients ingredientsEditable">
                    <asp:ListView ID="ListView1" runat="server">
                        <ItemTemplate>
                            <div class="list-item list-item-<%# Eval("foodId")%>">
                                <asp:LinkButton ID="LinkButton1" runat="server"
                                    CssClass="list-btn edit-ingrediant hide-on-logout"
                                    OnClick="LinkButton1_Click"
                                    CommandArgument='<%# Eval("foodId")%>'
                                    Text="<%$ Resources:MyGlobalResources, Edit%>"></asp:LinkButton>
                                <asp:LinkButton ID="LinkButton2" runat="server"
                                    CssClass="list-btn delete-ingrediant hide-on-logout"
                                    OnClick="LinkButton2_Click"
                                    CommandArgument='<%# Eval("foodId")%>'
                                    Text="<%$ Resources:MyGlobalResources, Delete%>"></asp:LinkButton>
                                <span class="display-name"><%# Eval("displayName")%></span>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</div>

<script src="Scripts/ingridiants.js"></script>
<script src="Scripts/RecipeEdit.js"></script>
