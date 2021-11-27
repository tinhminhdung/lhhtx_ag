<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaoQrcode.aspx.cs" Inherits="VS.E_Commerce.TaoQrcode" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
<%--    <script type="text/javascript" src="/Resources/js/jquery-1.7.1.min.js"></script>--%>
    <script src="/Resources/js/jquery-1.9.1.js"></script>
  <link href="/Resources/Timkiem/jquery-ui.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <div class="page-title m992">
    <h1 class="title-head margin-top-0"><a href="#">Ví tiền thành viên</a></h1>
</div>


        <asp:Literal ID="ltthongbao" runat="server"></asp:Literal>
        <div class="congtongvuietien">
            <div style="clear: both"></div>
            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                <div class="vitien">
                    <div><i class="fa fa-credit-card"></i> Hoa hồng mua bán</div>
                    <div class="Coin">
                        <asp:Literal ID="ltViHoaHongMuaBan" runat="server"></asp:Literal>
                        điểm
                    </div>
                </div>
            </div>
            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                <div class="chuyendiemthue">
                    <div style="text-transform: uppercase;">Chuyển sang ví thương mại</div>
                    <div>
                        <i class="fa fa-long-arrow-right" aria-hidden="true" style="font-size: 35px; float: left;"></i> 
                        <asp:Button ID="btchuyensangvithuongmai" OnClick="btchuyensangvithuongmai_Click" CssClass="bamchuyendiem" runat="server" Text="Bấm vào để chuyển điểm" />
                        <div><span style="font-size: 8pt; color: #ed1c24; text-transform: initial; width: 90%; padding: 6px;"><em>Hệ thống sẽ trừ <b><%=MoreAll.Other.Giatri("Thue") %> % </b>thu nhập cá nhân </em></span></div>
                    </div>
                </div>
            </div>


            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                <div class="vitien">
                    <div><i class="fa fa-credit-card"></i> Ví thương mại</div>
                    <div class="Coin">
                        <asp:Literal ID="lttongvicoin" runat="server"></asp:Literal>
                        điểm
                    </div>
                </div>
            </div>

            <div style="clear: both"></div>
            <hr />

            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                <div class="vitien">
                    <div><i class="fa fa-credit-card"></i> Hoa hồng quản lý</div>
                    <div class="Coin">
                        <asp:Literal ID="ltViHoaHongAFF" runat="server"></asp:Literal>
                        điểm
                    </div>
                </div>
            </div>
            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                <div class="chuyendiemthue">
                    <div style="text-transform: uppercase;">Chuyển sang ví mua hàng</div>
                    <div>
                        <i class="fa fa-long-arrow-right" aria-hidden="true" style="font-size: 35px; float: left;"></i> 
                        <asp:Button ID="btchuyensangviquanly" OnClick="btchuyensangviquanly_Click" CssClass="bamchuyendiem" runat="server" Text="Bấm vào để chuyển điểm" />
                        <div><span style="font-size: 8pt; color: #ed1c24; text-transform: initial; width: 90%; padding: 6px;"><em>Hệ thống sẽ trừ <b><%=MoreAll.Other.Giatri("Thue") %> % </b>thu nhập cá nhân</em></span></div>
                    </div>
                </div>
            </div>

            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                <div class="vitien">
                    <div><i class="fa fa-credit-card"></i> Ví mua hàng</div>
                    <div class="Coin">
                        <asp:Literal ID="ltvimuahang" runat="server"></asp:Literal>
                        điểm
                    </div>
                </div>
            </div>

            <div style="clear: both"></div>
            <hr />
            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                <div class="vitien">
                    <div><i class="fa fa-credit-card"></i> Ví nạp quản lý</div>
                    <div class="Coin">
                        <asp:Literal ID="lthoahonggioithieu" runat="server"></asp:Literal>
                        điểm
                    </div>
                </div>
            </div>
                  <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                <div class="vitien">
                    <div><i class="fa fa-credit-card"></i> THƯỞNG MUA HÀNG. Để mua hàng</div>
                    <div class="Coin">
                        <asp:Literal ID="ltvivip" runat="server"></asp:Literal>
                        điểm
                    </div>

                     <div><span style="font-size: 8pt; color: #ed1c24; text-transform: initial; width: 90%; padding: 6px;"><em>Ví dùng để trừ vào đơn thánh toán mua hàng</em></span></div>
                </div>
            </div>

                  <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan" >
                <div class="vitien">
                    <div><i class="fa fa-credit-card"></i> Tổng tiền mua hàng</div>
                    <div class="Coin">
                        <asp:Literal ID="lttongtienmuahang" runat="server"></asp:Literal>
                        điểm
                    </div>
                </div>
            </div>
               <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan" >
                <div class="vitien">
                    <div><i class="fa fa-credit-card"></i> Tổng đã rút</div>
                    <div class="Coin">
                        <asp:Literal ID="lttongdarut" runat="server"></asp:Literal>
                        điểm
                    </div>
                </div>
            </div>

            <asp:Panel ID="Panel2"  runat="server">
                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i> Chuyên gia AFF</div>
                        <div class="Coin">
                            <asp:Literal ID="ltchuyengiaAFF" runat="server"></asp:Literal>
                            điểm
                        </div>
                    </div>
                </div>

                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i> Chuyên gia Agland</div>
                        <div class="Coin">
                            <asp:Literal ID="ltchuyengiaAgland" runat="server"></asp:Literal>
                            điểm
                        </div>
                    </div>
                </div>
            </asp:Panel>

            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan" >
                <div class="vitien">
                    <div><i class="fa fa-credit-card"></i> Hoa hồng mua bán</div>
                    <div class="Coin">
                        <asp:Literal ID="lthoahongmua" runat="server"></asp:Literal>
                        điểm
                    </div>
                </div>
            </div>


            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan" >
                <div class="vitien">
                    <div><i class="fa fa-credit-card"></i> Hoa hồng nhà cung cấp</div>
                    <div class="Coin">
                        <asp:Literal ID="lthoahongban" runat="server"></asp:Literal>
                        điểm
                    </div>
                </div>
            </div>


            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                <div class="vitien">
                    <div><i class="fa fa-credit-card"></i> Hoa hồng mua bán QRCode</div>
                    <div class="Coin">
                        <asp:Literal ID="lthoahongmuaQRCode" runat="server"></asp:Literal>
                        điểm
                    </div>
                </div>
            </div>




            <asp:Panel ID="pnViagland"  runat="server">
                <asp:Panel ID="pnviuutien"  runat="server">
                    <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                        <div class="vitien">
                            <div><i class="fa fa-credit-card"></i> Ví Ưu tiên</div>
                            <div class="Coin">
                                <asp:Literal ID="ltviuutien" runat="server"></asp:Literal>
                                điểm
                            </div>
                        </div>
                    </div>
                </asp:Panel>
                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan" >
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i> Ví AG Land</div>
                        <div class="Coin">
                            <asp:Literal ID="ltviagland" runat="server"></asp:Literal>
                            điểm
                        </div>
                      <div><span style="font-size: 8pt; color: #ed1c24; text-transform: initial; width: 90%; padding: 6px;"><em>(Lãi suất chưa được trừ thuế 5% từ hoạt động đầu tư)</em></span></div>
                    </div>
                </div>

                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan" >
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i> Số cổ phần đang sở hữu </div>
                        <div class="Coin">
                            <asp:Literal ID="ltsophancophan" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>

                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan" >
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i> Số tiền cổ phần đang sở hữu </div>
                        <div class="Coin">
                            <asp:Literal ID="ltsotiendangsohuucophan" runat="server"></asp:Literal>
                            VNĐ
                        </div>
                    </div>
                </div>

            </asp:Panel>
            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan" >
                <div class="vitien">
                    <div><i class="fa fa-credit-card"></i> Hoa hồng Hỗ trợ</div>
                    <div class="Coin">
                        <asp:Literal ID="lthoahongvimoi" runat="server"></asp:Literal>
                        điểm
                    </div>
                </div>
            </div>
            <asp:Panel ID="Panel1" runat="server" >
                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan ">
                    <div class="vitien nhapnhay">
                        <div><i class="fa fa-credit-card"></i> Ví tạm giữ bán hàng</div>
                        <div class="Coin">
                            <asp:Literal ID="ltvitamgiu" runat="server"></asp:Literal>
                            điểm
                        </div>
                        <div><span style="font-size: 7pt; color: #ed1c24; text-transform: initial; width: 90%; padding: 6px;"><em>(Hệ thống sẽ tự động duyệt điểm sau khi đại lý đặt hàng sau: <b><%=MoreAll.Other.Giatri("TuDongDuyetDonHang") %></b> ngày tạm giữ)</em></span></div>
                    </div>
                </div>
            </asp:Panel>
          
            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                <div class="vitien">
                    <div><i class="fa fa-credit-card"></i> Ví tạm giữ mua hàng</div>
                    <div class="Coin">
                        <asp:Literal ID="ltvitamgiumuahang" runat="server"></asp:Literal>
                        điểm
                    </div>
                </div>
            </div>

            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan" >
                <div class="vitien">
                    <div><i class="fa fa-credit-card"></i> Tổng điểm được cấp</div>
                    <div class="Coin">
                        <asp:Literal ID="ltdiemduoccap" runat="server"></asp:Literal>
                        điểm
                    </div>
                </div>
            </div>
            <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan" >
                <div class="vitien">
                    <div><i class="fa fa-credit-card"></i> Tổng điểm đã chuyển</div>
                    <div class="Coin">
                        <asp:Literal ID="ltdiemdachuyen" runat="server"></asp:Literal>
                        điểm
                    </div>
                </div>
            </div>

            <asp:Panel ID="pvViQRCode" runat="server" >
                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i> Ví QRCode</div>
                        <div class="Coin">
                            <asp:Literal ID="ltViQRCode" runat="server"></asp:Literal>
                            điểm
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </div>
<div style="clear: both"></div>
<hr />
<div style="clear: both"></div>
<div class="widget">
    <div class="widget-title">
        <h4 style="color: red; text-transform: uppercase"><i class="icon-tags"></i> Danh sách video hướng dẫn </h4>
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
    </div>
    </form>
</body>
</html>
