<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ViTienThanhVien.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.ViTienThanhVien" %>
<div class="page-title m992">
    <h1 class="title-head margin-top-0"><a href="#" style="color:#127ec2">Ví tiền thành viên</a></h1>
</div>
<asp:Panel ID="pnKichHoat" Visible="false" runat="server">
<div class="col-12 kichhoattaikhan nhapnhay">
<span class="d-flex align-items-center purchase-popup">
<p>Vui lòng kích hoạt thành viên để được hưởng chính sách của hệ thống</p>
<a href="/ho-so-thanh-vien.html" class="btnv download-button purchase-button ml-auto">Kích hoạt thành viên</a>
<i class="mdi mdi-close" id="bannerClose"></i>
</span>
</div>
</asp:Panel>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <asp:Literal ID="ltthongbao" runat="server"></asp:Literal>
        <div class="congtongvuietien">
            <div style="clear: both"></div>
            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col mt-0">
                                <h5 class="card-title">Hoa Hồng</h5>
                            </div>
                            <div class="col-auto">
                                <div class="avatar">
                                    <div class="avatar-title rounded-circle bg-primary-dark">
                                        <i class="fa fa-credit-card" style="color: #fff"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <h1 class="display-5 mt-1 mb-3">
                            <asp:Literal ID="ltViHoaHongMuaBan" runat="server"></asp:Literal></h1>
                        <div class="mb-0">
                            Bao gồm tất cả các điểm hoa hồng 
                        </div>
                    </div>
                </div>

            </div>
            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                <div class="chuyendiemthue">
                    <div style="text-transform: uppercase;">Nộp thuế thu nhập cá nhân</div>
                    <div>
                        <i class="fa fa-long-arrow-right" aria-hidden="true" style="font-size: 35px;float: left;padding-top: 8px;"></i>
                        <asp:Button ID="btchuyensangvithuongmai" OnClick="btchuyensangvithuongmai_Click" CssClass="nutsukien" runat="server" Text="Bấm vào để chuyển điểm" />
                        <div><span style="font-size: 8pt; color: #ed1c24; text-transform: initial; width: 90%; padding: 6px;"><em>Hệ thống sẽ trừ <b><%=MoreAll.Other.Giatri("Thue") %> % </b>thu nhập cá nhân </em></span></div>
                    </div>
                </div>
            </div>

            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                 <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col mt-0">
                                <h5 class="card-title">Thu nhập sau thuế</h5>
                            </div>
                            <div class="col-auto">
                                <div class="avatar">
                                    <div class="avatar-title rounded-circle bg-primary-dark">
                                        <i class="fa fa-credit-card" style="color: #fff"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <h1 class="display-5 mt-1 mb-3">
                            <asp:Literal ID="lttongvicoin" runat="server"></asp:Literal></h1>
                        <div class="mb-0">
                           Điểm thương mại 
                        </div>
                    </div>
                </div>
            </div>
            <div style="clear: both"></div>
            <hr />
            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan" style=" display:none">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col mt-0">
                                <h5 class="card-title">Ví nạp quản lý</h5>
                            </div>
                            <div class="col-auto">
                                <div class="avatar">
                                    <div class="avatar-title rounded-circle bg-primary-dark">
                                        <i class="fa fa-credit-card" style="color: #fff"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <h1 class="display-5 mt-1 mb-3">
                            <asp:Literal ID="lthoahonggioithieu" runat="server"></asp:Literal></h1>
                        <div class="mb-0">
                          Điểm quản lý
                        </div>
                    </div>
                </div>
            
            </div>
            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col mt-0">
                                <h5 class="card-title">Ví tiền mua hàng</h5>
                            </div>
                            <div class="col-auto">
                                <div class="avatar">
                                    <div class="avatar-title rounded-circle bg-primary-dark">
                                        <i class="fa fa-credit-card" style="color: #fff"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <h1 class="display-5 mt-1 mb-3">
                            <asp:Literal ID="ltvimuahang" runat="server"></asp:Literal></h1>
                        <div class="mb-0">
                          Điểm mua hàng
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan" style=" display:none">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col mt-0">
                                <h5 class="card-title">Thưởng mua hàng</h5>
                            </div>
                            <div class="col-auto">
                                <div class="avatar">
                                    <div class="avatar-title rounded-circle bg-primary-dark">
                                        <i class="fa fa-credit-card" style="color: #fff"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <h1 class="display-5 mt-1 mb-3">
                            <asp:Literal ID="ltvivip" runat="server"></asp:Literal></h1>
                        <div class="mb-0">
                        Dùng để trừ vào các đơn hàng
                        </div>
                    </div>
                </div>
            </div>
             <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                <div class="card">
                    <div class="card-body">
                        <div class="row">
                            <div class="col mt-0">
                                <h5 class="card-title">Tiền đầu tư</h5>
                            </div>
                            <div class="col-auto">
                                <div class="avatar">
                                    <div class="avatar-title rounded-circle bg-primary-dark">
                                        <i class="fa fa-credit-card" style="color: #fff"></i>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <h1 class="display-5 mt-1 mb-3">
                            <asp:Literal ID="lttongtiendautu" runat="server"></asp:Literal></h1>
                        <div class="mb-0">
                        Tổng số tiền đầu tư
                        </div>
                    </div>
                </div>
            </div>

            <asp:Panel ID="Panel2" Visible="false" runat="server">
                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Chuyên gia AFF</div>
                        <div class="Coin">
                            <asp:Literal ID="ltchuyengiaAFF" runat="server"></asp:Literal>
                            điểm
                        </div>
                    </div>
                </div>

                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Chuyên gia Agland</div>
                        <div class="Coin">
                            <asp:Literal ID="ltchuyengiaAgland" runat="server"></asp:Literal>
                            điểm
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <div style="clear: both"></div>
            <hr />
            <div class="page-title m992" style="display:none">
                <h1 class="title-head margin-top-0"><a href="#" style="color:#127ec2">Điểm tạm giữa mua bán hàng</a></h1>
            </div>
            <div style="clear: both"></div>

            <asp:Panel ID="Panel1" runat="server" Visible="false">
              <div class="col-xs-6 col-sm-12 col-md-4 ViOther"  style="display:none">
                <div class="stretch-card grid-margin">
                <div class="cardsc bg-gradient-info card-img-holder text-white">
                  <div class="card-body">
                    <img src="/Resources/images/circle.svg" class="card-img-absolute" alt="circle-image">
                    <h4 class="font-weight-normal mb-3">Tạm giữ bán hàng <i class="fa fa-credit-card"></i>
                    </h4>
                    <h2 class="mb-5"> <asp:Literal ID="ltvitamgiu" runat="server"></asp:Literal></h2>
                    <h6 class="card-text">Tự động duyệt sau: <b><%=MoreAll.Other.Giatri("TuDongDuyetDonHang") %></b> ngày</h6>
                  </div>
                </div>
              </div>
            </div>
            </asp:Panel>

            <div class="col-xs-6 col-sm-12 col-md-4 ViOther"  style="display:none">
                <div class="stretch-card grid-margin">
                <div class="cardsc bg-gradient-danger card-img-holder text-white">
                  <div class="card-body">
                    <img src="/Resources/images/circle.svg" class="card-img-absolute" alt="circle-image">
                    <h4 class="font-weight-normal mb-3">Tạm giữ mua hàng <i class="fa fa-credit-card"></i>
                    </h4>
                    <h2 class="mb-5"> <asp:Literal ID="ltvitamgiumuahang" runat="server"></asp:Literal></h2>
                    <h6 class="card-text">Điểm tạm giữ khi mua hàng</h6>
                  </div>
                </div>
              </div>
              
            </div>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btchuyensangvithuongmai" />
    </Triggers>
</asp:UpdatePanel>
<div style="clear: both"></div>
<hr />
<div style="clear: both"></div>
<div class="widget"  style=" display:none">
    <div class="widget-title">
        <h4 style="color: red; text-transform: uppercase"><i class="icon-tags"></i>Danh sách video hướng dẫn </h4>
        <span class="tools">
            <a href="javascript:;" class="icon-chevron-down"></a>
            <a href="javascript:;" class="icon-remove"></a>
        </span>
    </div>
    <div class="widget-body">
        <div class="MVideo">
            <div class="row">
                <div style="clear: both"></div>
                <div class="videos">
                    <asp:Literal runat="server" ID="ltrListVideo"></asp:Literal>
                </div>
                <div style="clear: both"></div>
            </div>

        </div>
        <div class="space7"></div>
    </div>
</div>

<div style="clear: both"></div>


<asp:HiddenField ID="hdid" runat="server" />
<asp:HiddenField ID="hdIDGiohang" Value="0" runat="server" />
<asp:HiddenField ID="hdvalue" Value="0" runat="server" />
<asp:HiddenField ID="hdHoaHongGioiThieuF" Value="0" runat="server" />
<asp:HiddenField ID="hdCauHinhDuyetTuDong" Value="0" runat="server" />
<%-- <script type="text/javascript">
    var submit = 0;
    function ChuyenDiem() {
        if (++submit > 1) {
            alert('Việc tải File đang được thực hiện, nếu xảy ra lỗi bạn có thể bấm F5 để thực hiện lại');
            return false;
        }
        return true;
        $('#Control_ctl00_ctl00_btchuyensangvithuongmai').submit();
    }
</script>

 <script type="text/javascript">
     var submit2 = 0;
     function ChuyenDiem2() {
         if (++submit2 > 1) {
             alert('Việc tải File đang được thực hiện, nếu xảy ra lỗi bạn có thể bấm F5 để thực hiện lại');
             return false;
         }
         return true;
         $('#Control_ctl00_ctl00_btchuyensangviquanly').submit();
     }
</script>--%>

<asp:Literal ID="ltjavascript" runat="server"></asp:Literal>