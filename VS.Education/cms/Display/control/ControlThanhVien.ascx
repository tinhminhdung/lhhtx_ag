<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ControlThanhVien.ascx.cs" Inherits="VS.E_Commerce.cms.Display.control.ControlThanhVien" %>
<%@ Register Src="~/cms/Display/Nav_conten.ascx" TagPrefix="uc1" TagName="Nav_conten" %>
<uc1:Nav_conten runat="server" id="Nav_conten" />
<div class="container chitiet">
<div class="row">
<asp:PlaceHolder ID="phcontrol" runat="server"></asp:PlaceHolder>
</div>
</div>