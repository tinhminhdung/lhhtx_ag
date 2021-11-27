<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GioiThieu.ascx.cs" Inherits="VS.E_Commerce.cms.Display.GioiThieu.GioiThieu" %>
<div class="col-right">
      <div class="hd-content-page-child">
         <span><a href="/">Trang chủ</a></span> <a>Giới thiệu</a>
         <div class="cart">
             <div class="inner-cart">
                 <a href="/gio-hang.html">Giỏ hàng (<%=Services.SessionCarts.LoadCart() %>)</a>
             </div>
         </div>
      </div>
<div class="News-content">
<div class="title"><asp:Literal ID="lttitle" runat="server"></asp:Literal></div>
<div class="des-news"><asp:Literal ID="ltdesc" runat="server"></asp:Literal></div>
<div class="Other"><asp:Literal ID="ltTinlienquan" runat="server"></asp:Literal></div>
<div class="contents">    
<asp:Literal ID="ltcontent" runat="server"></asp:Literal>

<div style=" height:10px"></div>


</div>
</div>
</div>
