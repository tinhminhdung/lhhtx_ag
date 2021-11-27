<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Box_search.ascx.cs" Inherits="VS.E_Commerce.cms.Display.AllPage.Box_search" %>
<asp:Panel ID="SearchBox" runat="server" DefaultButton="lnksearch" CssClass="bbbboer" >
    <div class="input-group search-bar">
        <asp:TextBox OnLoad="Text_Load"  ID="txtkeyword" class="input-group-field st-default-search-input search-text" runat="server"></asp:TextBox>
        <span class="input-group-btn">
            <asp:LinkButton ID="lnksearch" OnClick="lnksearch_Click"  class="timkiemssss" runat="server"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
        </span>
    </div>
</asp:Panel>
