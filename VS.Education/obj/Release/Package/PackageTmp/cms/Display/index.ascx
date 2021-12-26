<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="false" CodeBehind="index.ascx.cs" Inherits="VS.E_Commerce.cms.Display.index" %>
<%@ Register Src="~/cms/Display/Page/Menuleft.ascx" TagPrefix="uc1" TagName="Menuleft" %>
<%@ Register Src="~/cms/Display/Page/MenuDuoi.ascx" TagPrefix="uc1" TagName="MenuDuoi" %>
<%@ Register Src="~/cms/Display/Gamemini.ascx" TagPrefix="uc1" TagName="Gamemini" %>

<section class="awe-section-1">
    <div class="section section-category ">
        <div class="cate-overlay"></div>
        <div class="container">
            <div class="row row-noGutter">
                <div class="col-lg-3 col-262">
                    <div class="cate-sidebar">
                        <nav>
                            <div class="hidden-md hidden-lg">
                                <h2 class="mobile-title">Danh mục sản phẩm </h2>
                            </div>
                            <ul id="nav" class="site-nav vertical-nav">
                                <uc1:Menuleft runat="server" ID="Menuleft" />
                            </ul>
                        </nav>
                    </div>
                </div>
                <div class="col-lg-9 col-md-12">
                    <div class="cate-banner">
                        <div id="slider" class="home-slider owl-carousel" data-lg-items='1' data-md-items='1' data-sm-items='1' data-xs-items="1" data-xss-items="1" data-margin='0' data-nav="true">
                            <asp:Literal ID="ltsildechinh" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>


<script>
    $(document).ready(function () {
        var owl = $(".home-slider");
        owl.owlCarousel({
            //items: 1,
            //autoplay: true,
            //autoPlaySpeed: 3000,
            //autoplayHoverPause: true
            items: 1,
            slideSpeed: 2000,
            nav: true,
            autoplay: true,
            dots: true,
            loop: true,
            responsiveRefreshRate: 200

        });
    });
</script>

<section class="awe-section-2d" style='display:none'>
    <div class="section section_tab_product section_giatothomnay products-view-grid topchienluoc">
        <div class="container hottt">

            <div class="trend-catalog wrap-catalog-main">
                <div class="trend-catalog-title">
                    <h2>Danh mục nổi bật</h2>
                </div>
                <div>
                    <ul class="list-catalog-main">
                        <asp:Literal ID="ltShowDanhMuc" runat="server"></asp:Literal>
                         <li class="catalog-main-item Mobile">
                            <a href="/san-pham.html" title="Xem tất cả">
                                <div class="icon-cat-main-thumb">
                                    <span>
                                        <img class="lazy-img lazy-loaded" alt="Xem tất cả"  src="/Resources/images/hot-category.png">
                                    </span>
                                </div>
                                <div class="name-cat-main">Xem tất cả</div>
                            </a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
        
    </div>
</section>

<div class="loadings">
    <section class="awe-section-2d">
        <div class="section section_tab_product section_giatothomnay products-view-grid topchienluoc">
            <div class="container hottt">
                <%--  <div class="bannerchienluoc">
                    <%=Advertisings.Ad_vertisings.Advertisings_A_Images("11") %>
                </div>--%>
                <div class="section-title ">
                    <h2>
                        Sản phẩm chiến lược  
                </div>
                <div class="e-tabs not-dqtab ajax-tab-1" data-section="ajax-tab-1">
                    <div class="row row-noGutter">
                        <div class="col-sm-12">
                            <div class="content">
                                <div>
                                    <asp:Literal ID="ltShowLoadPro" runat="server"></asp:Literal>
                                    <script> </script>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>


    <section class="awe-section-2d">
        <div class="section section_tab_product section_giatothomnay products-view-grid">
            <div class="container hottt">
                <div class="section-title">
                    <h2>
                        Sản phẩm bán chạy    
                </div>
                <div class="e-tabs not-dqtab ajax-tab-1" data-section="ajax-tab-1">
                    <div class="row row-noGutter">
                        <div class="col-sm-12">
                            <div class="content">
                                <div>
                                    <asp:Literal ID="ltbanchay" runat="server"></asp:Literal>
                                    <script> </script>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>

    <div class="nhomsaa">
        <asp:Literal ID="ltlistpro" runat="server"></asp:Literal>
    </div>
    <div style="clear: both"></div>

    
<section class="awe-section-8" id="awe-section-8">
    <div class="section section_blog pt-4 pb-4">
        <div class="container">
            <div class="section-title">
                <h2>
                    <a href="/tin-tuc-new.html">Tin mới nhất</a>
                </h2>
            </div>
            <div class="section-content">
                <div class="blog-slider owl-carousel" data-lg-items='4' data-md-items='4' data-sm-items='2' data-xs-items="2" data-nav="true">
                    <asp:Literal ID="ltrpNews" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
</section>
<%--    <section class="awe-section-9">
        <div id="Diemdanh"></div>
        <div class="section section-topcollection">
            <uc1:Gamemini runat="server" ID="Gamemini" />
        </div>
    </section>--%>
    
    <div style="clear: both"></div>
    <script src="/Resources/assets/jquery-2.2.3.min0596.js" type="text/javascript"></script>
    <div class="widget widget-policy">
        <div class="container">
            <div class="widget-inner">
                <div class="row">
                    <asp:Literal ID="ltShowPhuogThucThanhToan" runat="server"></asp:Literal>
                </div>
            </div>
        </div>
    </div>
</div>


