<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Index.ascx.cs" Inherits="VS.E_Commerce.cms.Display.News.Index" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<section class="right-content col-md-9 col-md-push-3">
<div class="box-heading relative">
<h1 class="title-head page_title">Tin tức</h1>
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