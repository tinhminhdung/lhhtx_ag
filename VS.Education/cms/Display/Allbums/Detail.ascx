<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Detail.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Allbums.Detail" %>
<link href="/Resources/LibraryAlbum/css/styles.css" type="text/css" rel="stylesheet" />
<link rel="stylesheet" type="text/css" href="/Resources/LibraryAlbum/css/photoswipe.css">
<script type="text/javascript" src="/Resources/LibraryAlbum/js/klass.min.js"></script>
<script type="text/javascript" src="/Resources/LibraryAlbum/js/code.photoswipe-3.0.5.min.js"></script>
<script type="text/javascript">
    (function (window, PhotoSwipe) {
        document.addEventListener('DOMContentLoaded', function () {
            var options = {}, instance = PhotoSwipe.attach(window.document.querySelectorAll('#Gallery a'), options);
        }, false);
    }(window, window.Code.PhotoSwipe));
</script>

<%@ Register Src="~/cms/Display/Lefmenu.ascx" TagPrefix="uc1" TagName="Lefmenu" %>
<div class="container" itemscope="" itemtype="http://schema.org/Blog">
    <div class="row">
        <section class="right-content margin-bottom-50 col-md-9 col-md-push-3">
            <div class="box-heading relative">
                <h1 class="title-head page_title">
                    <asp:Literal ID="ltcatename" runat="server"></asp:Literal></h1>
            </div>
            <section class="list-blogs blog-main">
                <div class="row">
                    <div class="nhomnhe">
                        <h2 class="title">
                            <asp:Literal ID="lttitle" runat="server"></asp:Literal></h2>
                        <div class="lineboder"></div>
                    </div>
                    <div class="region region-content">
                        <div class="block block-system" id="block-system-main">
                            <div class="content">
                                <div id="box_center">
                                    <div id="bor_centers">
                                        <div style="width: 100%">
                                            <ul id="Gallery" class="gallery Album">
                                                <%=ViewslideMax() %>
                                            </ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
            </section>
            <div class="row">
                <div class="col-xs-12 text-left"></div>
            </div>
        </section>
        <uc1:Lefmenu runat="server" ID="Lefmenu" />
    </div>
</div>




<style>
    .nivo-directionNav {
        display: none;
    }
</style>
