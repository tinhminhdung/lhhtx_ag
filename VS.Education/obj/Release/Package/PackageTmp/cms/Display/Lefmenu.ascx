<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Lefmenu.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Lefmenu" %>
<aside class="dqdt-sidebar sidebar left left-content col-lg-3 col-md-3 col-lg-pull-9 col-md-pull-9">
  <aside class="aside-item sidebar-category collection-category">
    <div class="aside-title">
      <h2 class="title-head margin-top-0">
        <span>Danh mục</span>
      </h2>
    </div>
    <div class="aside-content">
      <div class="nav-category navbar-toggleable-md">
        <ul class="nav navbar-pills">
            <li class="nav-item"><i class="fa fa-caret-right"></i><a class="nav-link" href="/san-pham-dieu-kien-tro-thanh-dai-ly.html">S.phẩm ĐK trở thành đại lý</a> </li>
            <li class="nav-item"><i class="fa fa-caret-right"></i><a class="nav-link" href="/san-pham-chien-luoc.html">Sản phẩm chiến lược</a> </li>
            <asp:Literal ID="ltMenuPro" EnableViewState="false" runat="server"></asp:Literal>
        </ul>
      </div>
    </div>
  </aside>
  <div class="aside-filter">
<%--    <aside class="aside-item filter-vendor">
      <div class="aside-title">
        <h2 class="title-head margin-top-0">
          <span>Tìm theo thương hiệu</span>
        </h2>
      </div>
      <div class="produce aside-content filter-group">
        <ul>
            <asp:Literal ID="ltthuonghieu" EnableViewState="false" runat="server"></asp:Literal>
        </ul>
      </div>
    </aside>--%>
    <aside class="aside-item filter-price">
      <div class="aside-title">
        <h2 class="title-head margin-top-0">
          <span>Tìm theo mức giá</span>
        </h2>
      </div>
      <div class="price aside-content filter-group">
        <ul>
           <asp:Literal ID="ltloctheogia" EnableViewState="false" runat="server"></asp:Literal>
        </ul>
      </div>
    </aside>
  </div>
  <div class="aside-item aside-mini-list-product">
    <div>
      <div class="aside-title margin-top-5">
        <h2 class="title-head">
          <a href="/san-pham-noi-bat.html">Sản phẩm nổi bật</a>
        </h2>
      </div>
      <div class="aside-content related-product">
        <div class="product-mini-lists">
          <div class="products">
              <asp:Literal ID="ltShowLoadProNoiBat" EnableViewState="false" runat="server"></asp:Literal>
          </div>
        </div>
      </div>
    </div>
  </div>
  
<%--  <script async src="https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
<!-- Right Menu -->
<ins class="adsbygoogle"
     style="display:block"
     data-ad-client="ca-pub-5631632145581414"
     data-ad-slot="6548115061"
     data-ad-format="auto"
     data-full-width-responsive="true"></ins>
<script>
     (adsbygoogle = window.adsbygoogle || []).push({});
</script>--%>



</aside>