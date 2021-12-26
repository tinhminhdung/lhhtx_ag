<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Lefmenu_News.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Lefmenu_News" %>
<aside class="left left-content col-md-3 col-md-pull-9">
      <aside class="blog-aside aside-item sidebar-category blog-category">
        <div class="aside-title">
          <h2 class="title-head">
            <span>Danh mục bài viết</span>
          </h2>
        </div>
        <div class="aside-content">
          <div class="nav-category navbar-toggleable-md">
            <ul class="nav navbar-pills">
                <asp:Literal ID="ltMenuNews" runat="server"></asp:Literal>
            </ul>
          </div>
        </div>
      </aside>
      <div class="blog-aside aside-item">
        <div>
          <div class="aside-title">
            <h2 class="title-head">
              <a href="/tin-tuc-new.html">Bài viết xem nhiều</a>
            </h2>
          </div>
          <div class="aside-content">
            <div class="blog-list blog-image-list">
                <asp:Literal ID="ltShowtinxemnhieu" runat="server"></asp:Literal>
     
            </div>
          </div>
        </div>
      </div>
      <div class="blog-aside aside-item aside-tags" style=" display:none">
        <div>
          <div class="aside-title margin-top-5">
            <h2 class="title-head">
              <span>Tags</span>
            </h2>
          </div>
          <div class="aside-content list-tags">
            <span class="tag-item">
              <a href="/blogs/all/tagged/gia-dung">Gia dụng</a>
            </span>
            <span class="tag-item">
              <a href="/blogs/all/tagged/iphone">Iphone</a>
            </span>
            <span class="tag-item">
              <a href="/blogs/all/tagged/moi-quen">Mới quen</a>
            </span>
            <span class="tag-item">
              <a href="/blogs/all/tagged/qua-tang">Quà tặng</a>
            </span>
            <span class="tag-item">
              <a href="/blogs/all/tagged/thoi-trang">thời trang</a>
            </span>
            <span class="tag-item">
              <a href="/blogs/all/tagged/tu-lanh">Tủ lạnh</a>
            </span>
            <span class="tag-item">
              <a href="/blogs/all/tagged/valentine">Valentine</a>
            </span>
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