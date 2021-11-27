<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Footer.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Footer" %>
<%--<%@ Register Src="~/cms/Display/AllPage/Box_Facebook.ascx" TagPrefix="uc1" TagName="Box_Facebook" %>--%>
<%@ Register Src="~/cms/Display/Page/MenuBottom.ascx" TagPrefix="uc1" TagName="MenuBottom" %>
<%@ Register Src="~/cms/Display/QuanLyDangBai/ThongBaoXuLyDonHang.ascx" TagPrefix="uc1" TagName="ThongBaoXuLyDonHang" %>

<div class="Doitac">
    <h2 class="line_branch line_branch2">ĐỐI TÁC CHIẾN LƯỢC</h2>
    <p class="img_footer">
     <%=Advertisings.Ad_vertisings.Advertisings_A_Images("13") %>
    </p>
</div>


<footer class="footer loadings">
    <div class="site-footer chitiet">
        <div class="container">
            <div class="footer-inner padding-top-40">
                <div class="row">

                    <div class="col-xs-12 col-sm-6 col-md-3 col-fix-5">
                        <div class="footer-widget drop-mobile">
                            <h4>
                                <span>Chấp nhận thanh toán</span>
                                <i class="fa fa-angle-down hidden-sm hidden-md hidden-lg"></i>
                            </h4>
                            <div class="footer-widget">
                                <div class="payment">
                                    <img src="/Resources/images/payment.png" alt="Payment">
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-xs-12 col-sm-6 col-md-3 col-fix-5">
                        <div class="footer-widget">
                            <h4>
                                <span>Kết nối với chúng tôi</span>
                            </h4>
                            <div class="footer-widget">
                                <%-- <uc1:Box_Facebook runat="server" ID="Box_Facebook" />--%>
                            </div>
                        </div>
                    </div>

                    <uc1:MenuBottom runat="server" ID="MenuBottom" />
                </div>
            </div>
        </div>
        <div class="site-footer ">
            <div class="container">
                <div class="footer-address">
                    <div class="container">
                        <div class="content row">
                            <%=Commond.Setting("FooTer") %>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</footer>
