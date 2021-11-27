<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Videmo.aspx.cs" Inherits="VS.E_Commerce.Videmo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
          <br /><br />
            <div class="page-title m992" style=" text-align:center">
                <h1 class="title-head margin-top-0"><a href="#">Ví tiền thành viên</a> <b style="color: red">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal></b></h1>
            </div>
            
             <div style=" width:1000px ; margin:auto">
                  <div class="congtongvuietien">
                <div style="clear: both"></div>
                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div class="Coin" style=" font-size:25px">
                            Lệch: <asp:Literal ID="ltlech" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>
                </div>
            <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                <tbody>
                    <tr class="trHeader" style="height: 25px">
                        <th style="font-weight: bold; text-align: center">Đầu vào :  <span style="color:#ed1c24"><asp:Literal ID="TongVao" runat="server"></asp:Literal></span></th>
                        <th style="font-weight: bold; text-align: center;">Đầu ra :   <span style="color:#ed1c24"><asp:Literal ID="TongRa" runat="server"></asp:Literal></span></th>
                        <th style="font-weight: bold; text-align: center">Tồn thực tế :   <span style="color:#ed1c24"><asp:Literal ID="Tonthucte" runat="server"></asp:Literal></span></th>
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                           Tổng cấp: <asp:Literal ID="ltdiemduoccap" runat="server"></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                            Đã chuyển:  <asp:Literal ID="ltdiemdachuyen" runat="server"></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                            Ví thương mại: <asp:Literal ID="lttongvicoin" runat="server"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                          HH NCC: <asp:Literal ID="lthoahongban" runat="server"></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                            <asp:HiddenField ID="hd480" Value="480" runat="server" />
                            <asp:CheckBox ID="CheckBox1" runat="server" Text=" Kích: 480"  AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" Checked="True" />
                        </td>
                        <td style="text-align: center;">
                            Ví nạp quản lý:  <asp:Literal ID="lthoahonggioithieu" runat="server"></asp:Literal>
                        </td>
                    
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                           HH Mua bán:      <asp:Literal ID="lthoahongmua" runat="server"></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                            Mua hàng: <asp:Literal ID="lttongtienmuahang" runat="server"></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                            Ví mua hàng:  <asp:Literal ID="ltvimuahang" runat="server"></asp:Literal>
                        </td>
                       
                    </tr>
                    <tr>
                        <td style="text-align: center; background:#f3f200; color:#000">
                             <asp:HiddenField ID="hdlailand" Value="0"  runat="server" />
                            <asp:CheckBox ID="checklang" runat="server" Text=" Kích: Land"  AutoPostBack="true" OnCheckedChanged="checklang_CheckedChanged" Checked="false" />
                            Land : <asp:Literal ID="ltviagland" runat="server"></asp:Literal> ???? (Chưa cộng)

                            <asp:Literal ID="ltviagland2" Visible="false" runat="server"></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                            Rút:  <asp:Literal ID="lttongdarut" runat="server"></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                            HH mua bán chưa bị trừ thuế: <asp:Literal ID="ltViHoaHongMuaBan" runat="server"></asp:Literal>
                        </td>
                       
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                          HH Quản lý:   <asp:Literal ID="lthhviquanly" runat="server"></asp:Literal></td>
                        <td style="text-align: center;">
                            Tạm giữu mua hàng:  <asp:Literal ID="ltvitamgiumuahang" runat="server"></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                           HOA HỒNG QUẢN LÝ chưa thuế:  <asp:Literal ID="ltViHoaHongAFF" runat="server"></asp:Literal>
                        </td>
                        
                    </tr>
                    <tr>
                        <td style="text-align: center;">
                           Mua điểm: <asp:Literal ID="ltmuadiem" runat="server"></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                            Trừ thuế TM:  <asp:Literal ID="ltthue" runat="server"></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                            
                            THƯỞNG MUA HÀNG:  <asp:Literal ID="ltvivip" runat="server"></asp:Literal>
                            
                        </td>
                       
                    </tr>
                     <tr>
                        <td style="text-align: center;">
                            &nbsp;</td>
                        <td style="text-align: center;">
                            Trừ thuế AFF:  <asp:Literal ID="ltthueAFF" runat="server"></asp:Literal>
                        </td>
                        <td style="text-align: center;">

                        </td>
                      
                    </tr>
                    <tr >
                        <td style="text-align: center;">
                        </td>
                        <td style="text-align: center; background:#f3f200; color:#000">
                          Số điểm đã quét : <asp:Literal ID="ltquyet" runat="server"></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                        </td>
                    </tr>
                    <tr >
                        <td style="text-align: center;">
                        </td>
                        <td style="text-align: center; background:#f3f200; color:#000">
                          THƯỞNG MUA HÀNG đi mua hàng : <asp:Literal ID="ltvivipmuahang" runat="server"></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                        </td>
                    </tr>

                    <tr >
                        <td style="text-align: center;">
                        </td>
                        <td style="text-align: center; background:#f3f200; color:#000">
                          THƯỞNG MUA HÀNG đi mua hàng tạm giữ: <asp:Literal ID="ltvipmuahangtamgiu" runat="server"></asp:Literal>
                        </td>
                        <td style="text-align: center;">
                        </td>
                    </tr>
                </tbody>
            </table>

               </div>
            <div style=" display:none">
            <asp:Literal ID="ltthongbao" runat="server"></asp:Literal>
            <div class="congtongvuietien">
                <div style="clear: both"></div>
                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Hoa hồng mua bán</div>
                        <div class="Coin">
                            
                            điểm
                        </div>
                    </div>
                </div>



                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Ví thương mại</div>
                        <div class="Coin">
                 
                            điểm
                        </div>
                    </div>
                </div>

                <div style="clear: both"></div>
                <hr />

                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Hoa hồng quản lý</div>
                        <div class="Coin">
                           
                            điểm
                        </div>
                    </div>
                </div>

                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Ví mua hàng</div>
                        <div class="Coin">
                           
                            điểm
                        </div>
                    </div>
                </div>

                <div style="clear: both"></div>
                <hr />

                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Tổng ví Thương mại + Quản Lý</div>
                        <div class="Coin">
                            <asp:Literal ID="lttmql" runat="server"></asp:Literal>
                            điểm
                        </div>
                    </div>
                </div>

                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Tổng điểm sau khi cộng trừ MUA BÁN, RÚT, CHUYỂN,...</div>
                        <div class="Coin">
                            <asp:Literal ID="lttongcong" runat="server"></asp:Literal>
                            điểm
                        </div>
                    </div>
                </div>



                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Ví nạp quản lý</div>
                        <div class="Coin">
                          
                            điểm
                        </div>
                    </div>
                </div>
                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Thuế bị trừ Thương mại</div>
                        <div class="Coin">
                           
                            điểm
                        </div>
                    </div>
                </div>



                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Thuế bị trừ AFF</div>
                        <div class="Coin">
                            
                            điểm
                        </div>
                    </div>
                </div>


                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Tổng tiền mua hàng</div>
                        <div class="Coin">
                       
                            điểm
                        </div>
                    </div>
                </div>
                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Tổng đã rút</div>
                        <div class="Coin">
                           
                            điểm
                        </div>
                    </div>
                </div>


                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Tổng hoa hồng ví quản lý</div>
                        <div class="Coin">
                          
                            điểm
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

                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Hoa hồng mua bán</div>
                        <div class="Coin">
                       
                            điểm
                        </div>
                    </div>
                </div>


                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Hoa hồng nhà cung cấp</div>
                        <div class="Coin">
                            
                            điểm
                        </div>
                    </div>
                </div>


                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Hoa hồng mua bán QRCode</div>
                        <div class="Coin">
                            <asp:Literal ID="lthoahongmuaQRCode" runat="server"></asp:Literal>
                            điểm
                        </div>
                    </div>
                </div>




                <asp:Panel ID="pnViagland" Visible="false" runat="server"></asp:Panel>
                <asp:Panel ID="pnviuutien" Visible="false" runat="server"></asp:Panel>
                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Ví Ưu tiên</div>
                        <div class="Coin">
                            <asp:Literal ID="ltviuutien" runat="server"></asp:Literal>
                            điểm
                        </div>
                    </div>
                </div>

                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Ví AG Land   /
                            <asp:Literal ID="ltsogoi" runat="server"></asp:Literal></div>
                        <div class="Coin">
                            lãi 1 ngày /
                            <asp:Literal ID="ltviaglandngay" runat="server"></asp:Literal>
                            <br />
                       

                            điểm
                        </div>
                    </div>
                </div>

                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Số cổ phần đang sở hữu </div>
                        <div class="Coin">
                            <asp:Literal ID="ltsophancophan" runat="server"></asp:Literal>
                        </div>
                    </div>
                </div>

                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Số tiền cổ phần đang sở hữu </div>
                        <div class="Coin">
                            <asp:Literal ID="ltsotiendangsohuucophan" runat="server"></asp:Literal>
                            VNĐ
                        </div>
                    </div>
                </div>


                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Hoa hồng Hỗ trợ</div>
                        <div class="Coin">
                            <asp:Literal ID="lthoahongvimoi" runat="server"></asp:Literal>
                            điểm
                        </div>
                    </div>
                </div>
                <asp:Panel ID="Panel1" runat="server" Visible="false">
                    <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan ">
                        <div class="vitien nhapnhay">
                            <div><i class="fa fa-credit-card"></i>Ví tạm giữ bán hàng</div>
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
                        <div><i class="fa fa-credit-card"></i>Ví tạm giữ mua hàng</div>
                        <div class="Coin">
                        
                            điểm
                        </div>
                    </div>
                </div>

                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Tổng điểm được cấp</div>
                        <div class="Coin">
                            
                            điểm
                        </div>
                    </div>
                </div>
                <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                    <div class="vitien">
                        <div><i class="fa fa-credit-card"></i>Tổng điểm đã chuyển</div>
                        <div class="Coin">
                           
                            điểm
                        </div>
                    </div>
                </div>

                <asp:Panel ID="pvViQRCode" runat="server" Visible="false">
                    <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
                        <div class="vitien">
                            <div><i class="fa fa-credit-card"></i>Ví QRCode</div>
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
            </div>


            <asp:HiddenField ID="hdid" runat="server" />
            <asp:HiddenField ID="hdIDGiohang" Value="0" runat="server" />
            <asp:HiddenField ID="hdvalue" Value="0" runat="server" />
            <asp:HiddenField ID="hdHoaHongGioiThieuF" Value="0" runat="server" />
            <asp:HiddenField ID="hdCauHinhDuyetTuDong" Value="0" runat="server" />

        </div>
    </form>
</body>
</html>
