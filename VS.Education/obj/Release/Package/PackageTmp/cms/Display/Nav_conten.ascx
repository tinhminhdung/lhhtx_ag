<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Nav_conten.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Nav_conten" %>
<div class="fixbg-breadcrumb">
<section class="bread-crumb">
<div class="container">
<div class="row">
<div class="col-xs-12">
<ul class="breadcrumb" itemscope="" itemtype="http://data-vocabulary.org/Breadcrumb">
    <li class="home">
        <a itemprop="url" href="/"><span itemprop="title">Trang chủ</span></a>
    </li>
    <asp:Literal ID="ltrnav" runat="server"></asp:Literal>
</ul>
</div>
</div>
</div>
</section>
</div>
