<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HomeShop.ascx.cs" Inherits="VS.E_Commerce.cms.Display.ShopNhaCC.HomeShop" %>
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
                                        <div class="Tenshopv">Địa chỉ :
                                            <asp:Literal ID="ltdiachikhohang" runat="server"></asp:Literal></div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-8 cogiannhes">
                                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                    <div><i class="fa fa-heart" aria-hidden="true"></i>Tên shop:
                                        <asp:Literal ID="lttenshop1" runat="server"></asp:Literal></div>
                                    <div><i class="fa fa-shopping-basket" aria-hidden="true"></i>Tổng sản phẩm: <span style="color: #fe6500"> <asp:Literal ID="lttongsanpham" runat="server"></asp:Literal></span></div>
                                </div>
                                <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                    <div>
                                        <i class="fa fa-calendar" aria-hidden="true"></i>Ngày tham gia: <span style="color: #fe6500">
                                            <asp:Literal ID="ltngaythamgia" runat="server"></asp:Literal></span>
                                    </div>
                                    <div>
                                        <i class="fa fa-cart-arrow-down" aria-hidden="true"></i>Sản phẩm đã bán : <span style="color: #fe6500">
                                            <asp:Literal ID="ltspdaban" runat="server"></asp:Literal></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="loadings">
                    <section class="awe-section-2d">
                        <div style="clear: both; height: 10px;"></div>
                        <div class="section-title" style="background: #f5f5f5; padding: 4px; float: left; width: 100%; height: 42px; color: #fff; padding-left: 12px;">
                            <h2 style="padding: 0px; margin: 0px; color: #000; font-size: 22px;">Sản phẩm của shop</h2>
                        </div>
                        <div style="clear: both; height: 10px;"></div>
                        <div class="row row-noGutter">
                            <div class="col-sm-12">
                                <div class="content">
                                    <div>
                                        <asp:Literal ID="ltShow" runat="server"></asp:Literal>
                                        <div style="clear: both;"></div>
                                        <div class="pager">
                                            <asp:Literal ID="ltpage" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>
                </div>

            </div>
        </div>
    </div>
</div>
