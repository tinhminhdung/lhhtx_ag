<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlNews.ascx.cs" Inherits="VS.E_Commerce.cms.Display.control.ControlNews" %>
<%@ Register Src="~/cms/Display/Lefmenu_News.ascx" TagPrefix="uc1" TagName="Lefmenu_News" %>
<%@ Register Src="~/cms/Display/Nav_conten.ascx" TagPrefix="uc1" TagName="Nav_conten" %>
<uc1:Nav_conten runat="server" id="Nav_conten" />
<div class="container loadings chitiet">
  <div class="row">
    <asp:PlaceHolder ID="phcontrol" runat="server"></asp:PlaceHolder>
       <uc1:Lefmenu_News runat="server" id="Lefmenu_News" />
  </div>
</div>
