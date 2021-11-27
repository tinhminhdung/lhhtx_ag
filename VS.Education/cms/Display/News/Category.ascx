<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Category.ascx.cs" Inherits="VS.E_Commerce.cms.Display.News.Category1" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<section class="right-content col-md-9 col-md-push-3">
<div class="box-heading relative">
<h1 class="title-head page_title">
    <asp:Literal ID="ltcatename" runat="server"></asp:Literal></h1>
</div>
<section class="list-blogs blog-main">
<div class="row">
<asp:Literal ID="ltShow" runat="server"></asp:Literal>
<div style="clear: both;"></div>
<div class="pager">
    <asp:Literal ID="ltpage" runat="server"></asp:Literal>
</div>
</div>
</section>
</section>