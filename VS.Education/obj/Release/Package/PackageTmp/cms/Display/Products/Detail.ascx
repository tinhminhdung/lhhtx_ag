<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="Detail.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Products.Detail" %>
<%@ Register Src="~/cms/Display/Nav_conten.ascx" TagPrefix="uc1" TagName="Nav_conten" %>
<%@ Register Src="~/cms/Display/AllPage/Box_ShareMangxahoi.ascx" TagPrefix="uc1" TagName="Box_ShareMangxahoi" %>

<script type="text/javascript">
    var r = { 'special': /[\W]/g, 'quotes': /[^0-9^]/g, 'notnumbers': /[^a-zA]/g }
    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
        if (o.value == '') {
            o.value = '1';
        }
    }
</script>
<uc1:Nav_conten runat="server" ID="Nav_conten" />

<div class="container ct_blog_outer_wrap chitiet">
    <div class="row">
        <div class="col-md-12 col-lg-12">
            <section itemscope="" itemtype="http://schema.org/Product">
                <div itemprop="offers" itemscope="" itemtype="http://schema.org/Offer">
                    <div class="containerx">
                        <div class="row">
                            <div class="col-xs-12 details-product">
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-5">
                                        <div class="relative product-image-block ">
                                            <div class="sp-loading">
                                                <img src="/Resources/images/sp-loading.gif" alt=""><br>
                                                LOADING IMAGES
                                            </div>
                                            <div class="sp-wrap">
                                                <asp:Literal ID="ltshowimg" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                        <uc1:Box_ShareMangxahoi runat="server" ID="Box_ShareMangxahoi" />
                                    </div>
                                    <div class="col-xs-12 col-sm-12 col-md-6 col-lg-7 details-pro a-left">
                                        <h1 class="title-head">
                                            <asp:Literal ID="ltname" runat="server"></asp:Literal></h1>


                                        <%--        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:Repeater ID="rpgiathanhvien" runat="server">
                                                    <HeaderTemplate>
                                                        <div class="giathanhdetail">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="tonggia">
                                                            <div class="soluongs"><%#Eval("SoLuongTu") %>-<%#Eval("SoLuongDen") %></div>
                                                            <div class="mucgia"><%#AllQuery.MorePro.FormatMoney_VND(Eval("GiaDaiLy").ToString()) %></div>
                                                        </div>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>--%>


                                        <div class="price-box">
                                            <div class="special-price">
                                                <span class="price product-price" style="color: red">Giá Niêm Yết:
                                                    <asp:Literal ID="ltprice" runat="server"></asp:Literal></span>
                                            </div>
                                        </div>
                                      
                                        <div class="price-box">
                                            <div class="special-price">
                                                <span class="price product-price">Giá Đại Lý:
                                                    <asp:Literal ID="ltgiadaily" runat="server"></asp:Literal></span>
                                            </div>
                                        </div>
                                        
                                        <div class="line">
                                            <b>Mã SP</b>: <span class="masp">
                                                <asp:Literal ID="ltcode" runat="server"></asp:Literal></span>
                                        </div>
                                        <div class="detail-header-info">
                                            Trọng lượng : <span class="vendor">
                                                <asp:Literal ID="lttrongluong" runat="server"></asp:Literal>
                                                Gram </span>

                                        </div>
                                        
                                        <div class="product_description">
                                            <div class="rte description">
                                                <asp:Literal ID="ltdesc" runat="server"></asp:Literal>
                                            </div>
                                        </div>
                                        <div class="form-product">
                                            <div id="add-to-cart-form" class="form-inline margin-bottom-10 dqdt-form">
                                                <div class="box-variant clearfix ">
                                                    <input type="hidden" name="variantId" value="14040570">
                                                </div>
                                                <div class="form-group form-groupx form-detail-action clearfix" >
                                                    <asp:Panel ID="Panel1" Visible="false" runat="server"></asp:Panel>
                                                        <div class=" ">
                                                            <label class="hidden">Số lượng: </label>
                                                            <div class="custom custom-btn-number">
                                                                <asp:TextBox ID="txtsoluong" class="selectspluong" Text="1" runat="server" onkeypress="if ( isNaN(this.value + String.fromCharCode(event.keyCode) )) return false;" onchange="if(this.value == '')this.value=1;">1</asp:TextBox>
                                                            </div>
                                                            <asp:LinkButton CssClass="btnMuaHang" ID="lnkaddtocart" runat="server" OnClick="lnkaddtocart_Click">
                                                            <div class="btn_buyNow"><i class="fa fa-shopping-cart" aria-hidden="true" style=" color:#fff; font-size:16px"></i>MUA NGAY</div>
                                                            <div class="checkout_start">Giao hàng tận nơi</div>
                                                            </asp:LinkButton>
                                                        </div>
                                                    
                                                    <div style="clear: both"></div>

                                                    <asp:Literal ID="ltshop" runat="server"></asp:Literal>

                                                    <%-- <div class="iwi ">
                                                        <a class="iWishAdd iwishAddWrapper" title="Yêu thích" href="javascript:;" data-customer-id="0" data-product="8828377" data-variant="14040570">
                                                            <i class="fa fa-heart"></i>
                                                        </a>
                                                        <a class="iWishAdded iwishAddWrapper iWishHidden" title="Bỏ yêu thích" href="javascript:;" data-customer-id="0" data-product="8828377" data-variant="14040570">
                                                            <i class="fa fa-heart"></i>
                                                        </a>
                                                    </div>--%>
                                                </div>
                                            </div>
                                            <%-- <div class="contact">
                                                Gọi <a href="tel:<%=Commond.Setting("Hotline")%>"><%=Commond.Setting("Hotline")%></a> để được tư vấn miễn phí
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            


                            
                        </div>
                    </div>
            </section>
        </div>
    </div>
</div>


<div style="height: 20px; clear: both"></div>
<div class="container ct_blog_outer_wrap chitiet">
    <div class="row">
        <div class="col-md-12 col-lg-12">
            <section class="main_container collection col-lg-12 col-md-12 nhomsaa">
                <div class="category-products products">
                    <section class="products-view products-view-grid">
                        <div class="row">
                            <div class="section-title">
                                <h1><a>Thông tin cửa hàng</a></h1>
                            </div>
                            <div style="height: 10px; clear: both"></div>
                            
                            <div class="container hommshopncc">
                                <div class="row">
                                    <div class="col-md-12 col-lg-12">
                                        <div class="containerx">
                                            <div class="row">
                                                <div class="col-xs-12 details-product">
                                                    <div class="row">
                                                        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-4">
                                                            <div class="shoponline">
                                                                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-5">
                                                                    <div class="shopavatarimg">
                                                                        <img src="/Resources/logo.jpg">
                                                                    </div>
                                                                </div>
                                                                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-7">
                                                                    <div class="Tenshops">
                                                                        <asp:Literal ID="lttenshop" runat="server"></asp:Literal>
                                                                    </div>
                                                                    <div class="Tenshopv">
                                                                        Địa chỉ :
                                            <asp:Literal ID="ltdiachikhohang" runat="server"></asp:Literal>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                       
                                                        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-8 cogiannhes">
                                                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                                                <div>
                                                                    <i class="fa fa-heart" aria-hidden="true"></i> Tên shop: <b style="color:red"><asp:Literal ID="lttenshop1" runat="server"></asp:Literal></b>
                                                                </div>
                                                                <div>
                                                                    <i class="fa fa-shopping-basket" aria-hidden="true"></i> Tổng sản phẩm: <span style="color: #fe6500">
                                                                        <asp:Literal ID="lttongsanpham" runat="server"></asp:Literal></span>
                                                                </div>
                                                            </div>
                                                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                                                <div>
                                                                    <i class="fa fa-calendar" aria-hidden="true"></i> Ngày tham gia: <span style="color: #fe6500">
                                                                        <asp:Literal ID="ltngaythamgia" runat="server"></asp:Literal></span>
                                                                </div>
                                                                <div>
                                                                    <i class="fa fa-cart-arrow-down" aria-hidden="true"></i> Sản phẩm đã bán : <span style="color: #fe6500">
                                                                        <asp:Literal ID="ltspdaban" runat="server"></asp:Literal></span>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                    </div>
                                </div>

                            </div>
                            <div style="height: 20px; clear: both"></div>
                        </div>
                    </section>
                </div>
            </section>

        </div>
    </div>
</div>



<div style="height: 20px; clear: both"></div>
<div class="container ct_blog_outer_wrap chitiet">
    <div class="row">
        <div class="col-md-12 col-lg-12">
            <section class="main_container collection col-lg-12 col-md-12 nhomsaa">
                <div class="category-products products">
                    <section class="products-view products-view-grid">
                        <div class="row">
                            <div class="section-title">
                                <h1><a>Chi tiết sản phẩm</a></h1>
                            </div>
                            <div style="height: 10px; clear: both"></div>
                            <div class="News-content">
                                <asp:Literal ID="ltdetail" runat="server"></asp:Literal>
                            </div>
                            <%if (Commond.Setting("Commentsp") == "1")
                              { %>
                            <div class="shares">
                                <div class="fb-comments" data-href="<%=MoreAll.MoreAll.RequestUrl(Request.Url.ToString()) %>" data-width="100%" data-numposts="5" data-colorscheme="light"></div>
                            </div>
                            <%} %>
                            <div style="height: 20px; clear: both"></div>
                            <uc1:Box_ShareMangxahoi runat="server" ID="Box_ShareMangxahoi1" />
                             <div style="height: 20px; clear: both"></div>
                        </div>
                    </section>
                </div>
            </section>

        </div>
    </div>
</div>


<div style="height: 10px; clear: both"></div>
<div class="container ct_blog_outer_wrap chitiet">
    <div class="row">
        <div class="col-md-12 col-lg-12">
            <section class="main_container collection col-lg-12 col-md-12 nhomsaa">
                <div class="category-products products">
                    <section class="products-view products-view-grid">
                        <div class="row">
                            <div class="section-title">
                                <h1><a>Sản phẩm khác</a></h1>
                            </div>
                            <asp:Literal ID="ltother" runat="server"></asp:Literal>
                        </div>
                    </section>
                </div>
            </section>

        </div>
    </div>
</div>
<div style="height: 10px; clear: both"></div>
<h2 style="display: none">
    <asp:Literal ID="ltcatename" runat="server"></asp:Literal></h2>
<asp:HiddenField ID="hdCurAmount" Value="1" runat="server" />
<asp:HiddenField ID="hdipid" Value="1" runat="server" />
<asp:HiddenField ID="hdgiamuommua" Value="0" runat="server" />

