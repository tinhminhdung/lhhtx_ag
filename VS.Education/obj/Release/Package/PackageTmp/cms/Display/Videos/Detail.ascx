<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Detail.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Videos.Detail" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<%@ Register Src="~/cms/Display/AllPage/Box_ShareMangxahoi.ascx" TagPrefix="uc1" TagName="Box_ShareMangxahoi" %>
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

                    <div class="News-content">
                        <div class="title">
                            <h1>
                                <asp:Literal ID="lttitle" runat="server"></asp:Literal></h1>
                        </div>
                        <div class="contents">
                            <asp:Literal ID="ltdesc" runat="server"></asp:Literal>
                        </div>
                        <div class="contents">
                            <asp:Literal ID="ltcontent" runat="server"></asp:Literal>
                            <div style="text-align: left; padding-bottom: 20px; padding-top: 10px">
                                <uc1:Box_ShareMangxahoi runat="server" ID="Box_ShareMangxahoi" />
                            </div>
                        </div>
                    </div>
                    <div style="clear: both; height: 20px"></div>
                    <div class="Chitietsp">Các  Video khác</div>
                    <div class="dangky">
                        <div class="trai"></div>
                        <div class="phai"></div>
                    </div>

                    <div style="clear: both"></div>
                    <div class="clear"></div>
                    <div class="videos">
                        <asp:Repeater ID="rpcates" runat="server">
                            <ItemTemplate>
                                <div class="vd-item">
                                    <div class="img">
                                        <a title="<%#(Eval("Title").ToString())%>" href="/<%#Eval("TangName")%>.html"><%#MoreAll.MoreImage.Image_width_height_Title_Alt(Eval("ImagesSmall").ToString(), "195", "146", Eval("Title").ToString(), Eval("Title").ToString())%></a>
                                        <div class="pl">
                                            <a title="<%#(Eval("Title").ToString())%>" href="/<%#Eval("TangName")%>.html">
                                                <img src="/Resources/images/play.png" /></a>
                                        </div>
                                    </div>
                                    <span><a title="<%#(Eval("Title").ToString())%>" href="/<%#Eval("TangName")%>.html"><%#Eval("Title") %></a></span>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div class="clear"></div>
                    <div>
                        <asp:Literal ID="lterr" runat="server"></asp:Literal>
                    </div>
                    <div style="clear: both;"></div>
                    <div class="pager" style="margin-left: 10px; margin-right: 10px; color: #999;">
                        <cc1:CollectionPager ID="CollectionPager1" runat="server" BackNextDisplay="HyperLinks" BackNextLocation="Split"
                            BackText="◄" ShowFirstLast="True" ResultsLocation="None" PagingMode="PostBack" MaxPages="50" FirstText="Trang đầu" HideOnSinglePage="True" IgnoreQueryString="False" LabelStyle="FONT-WEIGHT: bold;color:red" LabelText="" LastText="Cuối cùng" NextText="►" PageNumbersDisplay="Numbers"
                            ResultsFormat="Hiển thị từ  {0} Đến {1} (của {2})" ResultsStyle="PADDING-BOTTOM:4px;PADDING-TOP:4px;FONT-WEIGHT: bold;" ShowLabel="False" ShowPageNumbers="True" BackNextStyle="FONT-WEIGHT: bold; margin: 14px;" ControlCssClass="" ControlStyle="" UseSlider="True" PageNumbersSeparator="&nbsp;">
                        </cc1:CollectionPager>
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

