<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="index.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Allbums.index" %>
<%@ Register Src="~/cms/Display/Lefmenu.ascx" TagPrefix="uc1" TagName="Lefmenu" %>
<div class="container" itemscope="" itemtype="http://schema.org/Blog">
  <div class="row">
    <section class="right-content margin-bottom-50 col-md-9 col-md-push-3">
      <div class="box-heading relative">
        <h1 class="title-head page_title"><asp:Literal ID="ltcatename" runat="server"></asp:Literal></h1>
      </div>
      <section class="list-blogs ">
        <div class="row">
           <asp:Literal ID="ltshowalbum" runat="server"></asp:Literal>
        </div>
      </section>
      <div class="row">
        <div class="col-xs-12 text-left"> </div>
      </div>
    </section>
    <uc1:Lefmenu runat="server" ID="Lefmenu" />
  </div>
</div>


       
