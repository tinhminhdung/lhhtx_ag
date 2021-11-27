<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Box_search_header.ascx.cs" Inherits="VS.E_Commerce.cms.Display.AllPage.Box_search_header" %>
<asp:Panel ID="SearchBoxHeader" runat="server" DefaultButton="lnksearchheader" CssClass="bbbboerss" >
    <div class="input-group search-bar">
        <asp:TextBox OnLoad="Text_Load" ID="txtkeyword" class="input-group-field st-default-search-input search-text tkiemmobule" runat="server"></asp:TextBox>
        <span class="input-group-btn">
             <asp:Button ID="lnksearchheader" OnClick="lnksearchheader_Click" runat="server" class="btn icon-fallback-text" Text="Tìm kiếm" />
        </span>
    </div>
</asp:Panel>
