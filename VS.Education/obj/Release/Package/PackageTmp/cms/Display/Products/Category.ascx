<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Category.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Products.Category" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<section class="main_container collection col-lg-12 col-md-12 nhomsaa">
  <div class="category-products products">
    <section class="products-view products-view-grid">
      <div class="row">
      <div class="section-title"><h1><a><asp:Literal ID="ltcatename" runat="server"></asp:Literal></a></h1></div>
        <div class="News-content" style="background:#fff;display: inline-block;">
            <div class="contents">    
                <asp:Literal ID="ltcontent" runat="server"></asp:Literal>
            </div>
        </div>
        <asp:Literal ID="ltShow" runat="server"></asp:Literal>
        <div style="clear: both;"></div>
        <div class="pager">
             <asp:Literal ID="ltpage" runat="server"></asp:Literal>
        </div>

      </div>
      <div class="text-xs-right">
        <div class="margin-bottom-30"> &nbsp; </div>
      </div>
    </section>
  </div>
</section>

     

