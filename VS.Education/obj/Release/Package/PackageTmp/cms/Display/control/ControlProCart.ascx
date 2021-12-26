<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlProCart.ascx.cs" Inherits="VS.E_Commerce.cms.Display.control.ControlProCart" %>
<%@ Register Src="~/cms/Display/Nav_conten.ascx" TagPrefix="uc1" TagName="Nav_conten" %>
<%@ Register Src="~/cms/Display/Lefmenu.ascx" TagPrefix="uc1" TagName="Lefmenu" %>
<uc1:Nav_conten runat="server" ID="Nav_conten" />
<div class="container loadings">
  <div class="row">
   <asp:PlaceHolder ID="phcontrol" runat="server"></asp:PlaceHolder>
  </div>
</div>

