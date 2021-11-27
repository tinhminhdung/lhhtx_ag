<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Shopcart.aspx.cs" Inherits="VS.E_Commerce.Shopcart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Resources/js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <link href="/Resources/ShopCart/css/Stylecart.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
       <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<div class="shop_cart ajax">
<asp:panel id="pnmessage" runat="server">
<div class="shop_cart ajax">
  <div class="num0">
    <div class="txt_center">
      <div class="space10px txt_b txt_18">Không có sản phẩm nào trong giỏ hàng</div>
      <a class="btn bg_pink txt_18 txt_b" href="/" style="font-size:18px;"> Tiếp tục mua hàng &gt;&gt; </a>
    </div>
  </div><!--num0-->
</div> 
</asp:panel>

<asp:panel id="pnOrder" runat="server" Visible="false">
 <div class="title">
    <div class="tl txt_b">Giỏ hàng của bạn (<span class="shopping_cart_item"><%=Services.SessionCarts.LoadCart()%></span> sản phẩm)</div>
    <input id="temp_total" value="0" type="hidden">
  </div>
 <table class="tbl_cart" style="" cellpadding="5">
                <tbody>
                <tr id="shopping-cart-first-row" class="txt_u txt_14 txt_b">
                    <td style="">Sản phẩm</td>
                    <td style="" class="shopping-cart-price-col">Đơn giá</td>
                    <td class="shopping-cart-quantity-col center">Số lượng</td>
                    <td style="text-align: right;" class="shopping-cart-sum-col">Thành tiền</td>
                </tr>
 <asp:Repeater id="Repeater1" runat="server">
	<ItemTemplate>
    <tr id="itm17876">
                        <td style="text-align: left;">
                          	<div class="cartInfo-img fl">
                              <img src="<%#Eval("Vimg")%>" style="vertical-align: middle; margin-right: 10px;width:60px;">
                            </div>
                          
                          	<div class="sum" style="padding-left:70px;">
                              <div class="cartInfo-name">
                                      <a class=""><b><%#Eval("Name")%></b></a>
                                   <br>
                              </div>
                               <a class="i-del shows" onclick="AJdeleteShoppingCartItem(<%#Eval("PID")%>,'<%#Eval("Name")%>')"><img src="/Resources/ShopCart/images/xoa.png" /> Bỏ sản phẩm</a>
                          </div>
                        </td>
                        <td style="">
                            <span id="sell_price_pro_17876"><%#AllQuery.MorePro.FormatMoneyCart(Eval("Price").ToString())%></span>
                     	<br><span class="txt_d"><%#Giacu(Eval("PID").ToString())%></span>
                          <br><span class="txt_pink"><%#AllQuery.MorePro.Tietkiem(Eval("PID").ToString())%></span>
                        </td>
                        <td class="center">
                         <asp:HiddenField ID="hiID" Value='<%# Eval("PID") %>' runat="server" />
                       <input type="number" max="999" min="0" style=" width:50px" name='<%#Eval("PID")%>' id='<%#Eval("PID")%>' value="<%#DataBinder.Eval(Container.DataItem, "Quantity")%>" onchange="AddShoppingCartItem(<%#Eval("PID")%>,'<%#Eval("Name")%>')" class="txt_center cor3px shows">
                        </td>
                        <td style="text-align: right;">
                          <span id="price_pro_17876"><%#AllQuery.MorePro.FormatMoneyCart(Eval("Money").ToString())%></span>
                      </td>
                    </tr>
	</ItemTemplate> 
      
    </asp:Repeater>
    <tr>
                        <td colspan="5" class="txt_right">
                          	<div style="line-height: 26px;">
                                Tổng cộng : <span class="sub1 txt_18 txt_pink total_value_step txt_b" id="total_value" data-value="<%=tongien %>"><%=tongien %></span><br>
                                <span id="other-discount">Tổng số sản phẩm: <span data-discount="0" id="price-discount" class="txt_pink"> <%=sosp %></span></span><br>
                       			<span>Thanh toán: <span id="total_shopping_price" class="txt_pink txt_b total_value_step"><%=tongien %></span></span>
                              <br>Giá đã bao gồm VAT
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="txt_right">
                          	<a href="/" class="txt_pink txt_18 txt_b" style="float:left;"><i class="fa fa-angle-left"></i> Tiếp tục mua hàng</a>
                          	<div style="float:right;">
                          <%--	<a class="btn bg_blue txt_center txt_20 txt_u" href="/gio-hang.html" style="padding:5px 50px;">
                              ĐẶT GIỮ HÀNG<br> <span class="txt_12" style="text-transform: none;">(đến siêu thị lấy hàng)</span>
                            </a>--%>
                            <a class="btn bg_pink txt_center txt_20 txt_u" href="/gio-hang.html" style="padding:5px 50px;">
                              MUA ONLINE<br> <span class="txt_12" style="text-transform: none;">(giao hàng tận nơi)</span>
                            </a>
                           </div>
                        </td>
                    </tr>
                     </tbody>
            </table>

        

<script type="text/javascript">
    function AJdeleteShoppingCartItem(id, name) {
        $('.shows').addClass("fancybox fancybox.ajax");
        $('.shows').attr("href", "/Shopcart.aspx");
        $.ajax({
            type: "POST",
            url: "/index.aspx/updatePrice",
            data: "{id:'" + id.toString() + "',quantity:'1'}",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            async: "true",
            success: function (response) {
            },
            error: function (response) {
                // alert(response.status + ' ' + response.statusText);
            }
        });
    }
</script>

<script type="text/javascript">
    function AddShoppingCartItem(id, name) {
        $('.shows').addClass("fancybox fancybox.ajax");
        $('.shows').attr("href", "/Shopcart.aspx");
        var numPro = "#" + id;
        //bắt đầu
        $.ajax({
            type: "POST",
            url: "/index.aspx/capnhatsoluong",
            data: "{id:'" + id.toString() + "',quantity:'" + $(numPro).val().toString() + "'}",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            async: "true",
            success: function (response) {
            },
            error: function (response) {
            }
        });
    }
</script>

</asp:panel>
</div>
</ContentTemplate>
</asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
