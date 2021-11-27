<%@ Page Language="C#" AutoEventWireup="true" ResponseEncoding="utf-8" CodeBehind="index.aspx.cs" Debug="true" Inherits="VS.E_Commerce.index1" ValidateRequest="false" EnableViewStateMac="false" ViewStateEncryptionMode="Never" MaxPageStateFieldLength="40" EnableEventValidation="false" %>
<%@ Register Src="~/cms/Display/Footer.ascx" TagPrefix="uc1" TagName="Footer" %>
<%@ Register Src="~/cms/Display/Header.ascx" TagPrefix="uc1" TagName="Header" %>
<%@ Register Src="~/cms/Display/Control.ascx" TagPrefix="uc1" TagName="Control" %>
<%--<%@ Register Src="~/cms/Display/AllPage/TabBar_Footter.ascx" TagPrefix="uc1" TagName="TabBar_Footter" %>--%>
<%@ Register Src="~/cms/Display/AllPage/Box_Hotline.ascx" TagPrefix="uc1" TagName="Box_Hotline" %>
<%@ Register Src="~/cms/Display/QuanLyDangBai/ThongBaoXuLyDonHang.ascx" TagPrefix="uc1" TagName="ThongBaoXuLyDonHang" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=VS.E_Commerce.App.Template.WebTitle(hp, Modul)%></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <html xmlns="http://www.w3.org/1999/xhtml" xml:lang="vi" lang="vi-VN">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name='revisit-after' content='1 days' />
    <meta property="og:type" content="article" />
    <asp:Literal ID="ltFacebook" runat="server"></asp:Literal>
    <meta name="robots" content="index,follow,all" />
    <link href="/Resources/assets/plugin.min0596.css" rel="stylesheet" type="text/css" />
    <link href="/Resources/assets/base.scss0596.css" rel="stylesheet" type="text/css" />
    <link href="/Resources/assets/style.scss0596.css" rel="stylesheet" type="text/css" />
    <link href="/Resources/assets/module.scss0596.css" rel="stylesheet" type="text/css" />
    <link href="/Resources/assets/responsive.scss0596.css" rel="stylesheet" type="text/css" />
    <link href="/Resources/assets/iwish0596.css" rel="stylesheet" type="text/css" />
    <script src="/Resources/assets/iwishheader0596.js" type="text/javascript"></script>
    <link href="/Resources/css/Css_All.css" rel="stylesheet" type="text/css" />
    <link href="/Resources/css/smoothproducts.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="/Resources/Zoomanh/zomphonganh.css">
    <link href="/Resources/ResponsiveNews/css/flexnav.css" media="screen, projection" rel="stylesheet" type="text/css">
    <link href="/Resources/css/Mobile.css" rel="stylesheet" type="text/css" />
    <link href="/Resources/font-awesome/font-awesome.min.css" rel="stylesheet" />
            <link rel="stylesheet" href="/Resources/css/jquery.toast.css">
<%--    <script type="text/javascript" src="/Resources/js/jquery-1.7.1.min.js"></script>--%>
    <script src="/Resources/js/jquery-1.9.1.js"></script>
  <link href="/Resources/Timkiem/jquery-ui.css" rel="stylesheet" type="text/css" />
 <script type="text/javascript" src="/Resources/js/jquery.toast.js"></script>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:Literal ID="ltShowbody" runat="server"></asp:Literal>
        <uc1:Header runat="server" ID="Header" />
        <uc1:Control runat="server" ID="Control" />
        <uc1:Footer runat="server" ID="Footer" />
       <%-- <uc1:TabBar_Footter runat="server" id="TabBar_Footter" />--%>
        <%=Commond.Setting("Livechat")%>
        <script src="/Resources/assets/plugin0596.js" type="text/javascript"></script>
        <script type="text/javascript" src="/Resources/js/maina.js"></script>
        <%if (Request["su"] == null && Modul == "") {%>
          <script src="/Resources/assets/main0596.js" type="text/javascript"></script>
        <h1 style="display: none"><%=Commond.Setting("webname")%></h1>
        <%} %>

        <asp:Literal ID="ltjavascript" runat="server"></asp:Literal>
    </form>
     <script type="text/javascript">
         function Menuopen() {
             var hidden = document.getElementById('khodattenqua');
             if (hidden.style.display === 'none') {
                 hidden.style.display = 'block';
             }
             else {
                 hidden.style.display = 'none';
             }
         }
    </script>
    <script src="/Resources/Responsive/js/jquery.flexnav.js" type="text/javascript"></script>
   <script type="text/javascript">
       jQuery(document).ready(function ($) {
           $(".flexnav").flexNav();
       });
       function openNav() { document.getElementById("mySidenav").style.width = "250px"; }
       function closeNav() { document.getElementById("mySidenav").style.width = "0"; }
    </script>
    <link href="/Resources/ShopCart/css/popup.css" rel="stylesheet" type="text/css" />
    <script src="/Resources/ShopCart/js/popup.js" type="text/javascript"></script>
    <link href="/Resources/ShopCart/css/Stylecart.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="/Resources/js/smoothproducts.min.js"></script>
    <script type="text/javascript" src="/Resources/Zoomanh/zomphonganh.js"></script>
    <script type="text/javascript">
        $('.sp-wrap').smoothproducts();
    </script>
     <script src="/Resources/Timkiem/jquery-ui.min.js" type="text/javascript"></script>


    <!-- Popup Shopcart Products -->
    <%--    <div id="jName" style="margin-bottom: 25px; text-align: center; background: #fff8d1 none repeat scroll 0 0; border: 1px solid #fcab0f; border-radius: 3px; bottom: 15px; color: #000; display: none; padding: 10px; position: fixed; right: 0; width: 300px; z-index: 9999;"></div>--%>
    <%-- Ajaxloading--%>
     <div id="Ajaxloading">
        <div class="inner">
            <img src="/Resources/ShopCart/images/ajax-loader_2.gif"><p>Đang xử lý...</p>
        </div>
    </div>
    <%-- Ajaxloading--%>
    <%-- popupbox--%>
    <div class="popupbox3" id="popuprel3">
        <div class="closes">
            <img src="/Resources/ShopCart/images/b_close.png" /></div>
        <div id="intabdiv3">
            <div id="scoll">
                <div id="cartContent"></div>
            </div>
        </div>
    </div>
<%--<div style="display: none;" id="fade"></div>--%>
    <%-- popupbox--%>
<%--lazyload_News Load ảnh và iframe sau khi load web xong -- và js auto height--%>
<script type="text/javascript" src="/Resources/js/equalheight.js"></script>
<script type="text/javascript" charset="utf-8">
    $(window).load(function () {
	    equalheight('.product-box .product-thumbnail>a');
        //equalheight('.product-box');
	   // equalheight('.product-name');
	    equalheight('.Nhomtrangchu');
    });
    $(window).resize(function () {
	    equalheight('.product-box .product-thumbnail>a');
        //equalheight('.product-box');
	   // equalheight('.product-name');
	    equalheight('.Nhomtrangchu');
    });
    </script>
<script src="/Resources/ShopCart/js/popup.js" type="text/javascript"></script>
<script type="text/javascript" charset="utf-8">
    function Showthanhvien() {
        var hidden = document.getElementById('myDropdown');
        if (hidden.style.display == 'none') {
            hidden.style.display = 'block';
        }
        else {
            hidden.style.display = 'none';
        }
    }
    function ShowthanhvienDT() {
        var hidden = document.getElementById('myDropdownDestop');
        if (hidden.style.display == 'none') {
            hidden.style.display = 'block';
            document.getElementById("overlay").style.display = "block";
        }
        else {
            hidden.style.display = 'none';
            document.getElementById("overlay").style.display = "none";
        }
    }
   $(document).ready(function () {
       $('#overlay').click(function () {
           var hidden = document.getElementById('myDropdownDestop');
           hidden.style.display = 'none';
            document.getElementById("mySidenav").style.width = "0";
            $('#overlay').fadeOut();
            return false;
        });
    });
</script>
<%--<script type="text/javascript" src="/Resources/js/lazyload_News.js"></script>
    Đang bị xung khi tìm kiếm
<script src="/Resources/js/jquery.devrama.lazyload.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function () {
        $.DrLazyload({ effect: "fadeIn" }); //Yes! that's it!
    });
</script>--%>
<div id="overlay"></div>
    <script src="/Resources/js/bootstrap.min.js"></script>
        <%if (Request["su"] == null && Modul == "") {%>
            <script src="/Resources/PopUp/popup.js" type="text/javascript"></script>
            <asp:Literal ID="ltpopup" runat="server"></asp:Literal>
        <%}else{%>
        <uc1:ThongBaoXuLyDonHang runat="server" ID="ThongBaoXuLyDonHang" />
        <%} %>


  <%--  <div id="popupContact">
		<a id="popupContactClose"><img src="/Resources/images/b_close.png" /></a>
		<div id="contactArea" class="popupnd">
           qwertyju<br />qwertyju<br />qwertyju<br />qwertyju<br />qwertyju<br />qwertyju<br />
		</div>
	</div>--%>

	<div id="backgroundPopup"></div>
    <div class="bar-l">
    <ul class="bar-top">
        <li class="bar-sort">
            <a href="http://m.me/<%=Commond.Setting("txtmessengerFacebook")%>" title="icon messager" target="_blank">
                <noscript>
                    <img class="img-fluid" src="/Resources/images/icon-messager.png">
                </noscript>
                <img class="img-fluid lazyloaded" src="/Resources/images/icon-messager.png">
            </a>
        </li>
        <li class="bar-sort">
            <a href="https://zalo.me/<%=Commond.Setting("txtZalo")%>" title="icon zalo" target="_blank">
                <noscript>
                    <img class="img-fluid" src="/Resources/images/icon-zalo.png">
                </noscript>
                <img class="img-fluid lazyloaded" src="/Resources/images/icon-zalo.png">
            </a>
        </li>
        <li class="bar-sort bar-phone">
            <a href="tel:<%=Commond.Setting("Hotline")%>" title="icon phone">
                <noscript>
                    <img class="img-fluid" src="/Resources/images/icon-phone.png">
                </noscript>
                <img class="img-fluid lazyloaded" src="/Resources/images/icon-phone.png">
            </a>
        </li>
    </ul>
</div>

<script>
    $(function () {
        $("[id$=txtkeyword]").autocomplete({
            source: function (request, response) {
                $.ajax({
                    url: '<%=ResolveUrl("~/index.aspx/GetAutocomplete") %>',
                    data: "{ 'prefix': '" + request.term + "','condition': '' }",
                    dataType: "json",
                    type: "POST",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        response($.map(data.d, function (item) {
                            return {
                                label: item.Name,
                                image: item.ImagesSmall,
                                price: item.Price,
                                oldprice: item.OldPrice,
                                iicid: item.icid,
                                iipid: item.ipid,
                                iNhom: item.Nhom,
                                iTangName: item.TangName
                            }
                        }))
                    },
                    error: function (response) {
                        // alert(response.responseText);
                    },
                    failure: function (response) {
                        // alert(response.responseText);
                    }
                });
            },
            select: function (e, i) {

            },
            minLength: 1
        })
        .autocomplete("instance")._renderItem = function (ul, item) {
            return $("<li>")
                   .append("<div><a href='/" + item.iTangName + "_sp" + item.iipid + ".html' title='" + item.label + "'><div class='auto-img'><img src='" + item.image + "' alt='" + item.label + "' /></div><div class='auto-name'><h3>"
                            + item.label + "</h3></div><span class='auto-price'>" + item.price + "</span></a></div>")
                   .appendTo(ul);
        };
    });
</script>


</body>
</html>
