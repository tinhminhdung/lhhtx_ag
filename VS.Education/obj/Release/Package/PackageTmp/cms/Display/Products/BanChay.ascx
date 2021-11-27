<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BanChay.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Products.BanChay" %>
    <section class="main_container collection col-lg-12 col-md-12 nhomsaa">
  <div class="category-products products">
    <section class="products-view products-view-grid">
      <div class="row">
      <div class="section-title"><h1><a>Sản phẩm bán chạy</a></h1></div>

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

     

