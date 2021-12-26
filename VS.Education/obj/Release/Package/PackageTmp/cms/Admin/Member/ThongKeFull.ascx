<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThongKeFull.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.Member.ThongKeFull" %>
<style>
   .frmss tr {
    line-height: 32px;
}
   .thongkesss{ background:red; padding:5px; color:#fff; border-radius:3px;}
</style>
<div id="cph_Main_ContentPane">
    <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
        <ul>
            <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang chủ</a></li>
            <li class="Last"><span>Thống kê</span></li>
        </ul>
    </div>
    <div style="clear: both;"></div>
    <div class="widget">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row-fluid">
                    <div class="span12">
                        <div class="widget">
                            <div class="widget-title">
                                <h4><i class="icon-reorder"></i>Thống kê</h4>
                            </div>
                            <div class="widget-body">
                                <div class='frm-add frmss'>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="400px"></td>
                                            <td></td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" />Thống kê bán hàng</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Tổng số đơn</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="lttongsodon" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Tổng số sản phẩm đã bán</td>
                                            <td></td>
                                            <td>
                                               <span class='thongkesss'><asp:Literal ID="tongsosanphamdaban" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                         <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>
                                          <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" />Thống kê Sản Phẩm</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>

                                         <tr>
                                            <td style="padding-left: 15px">Tổng sản phẩm đã duyệt</td>
                                            <td></td>
                                            <td>
                                                 <span class='thongkesss'><asp:Literal ID="tongsosanphamphamdaduyet" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                         <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>

                                         <tr>
                                            <td style="padding-left: 15px">Tôi là nhà sản xuất</td>
                                            <td></td>
                                            <td>
                                                 <span class='thongkesss'><asp:Literal ID="lttoilanhasanSuat" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>


                                         <tr>
                                            <td style="padding-left: 15px">Tôi nhà phân phối</td>
                                            <td></td>
                                            <td>
                                                 <span class='thongkesss'><asp:Literal ID="toilanhaphaphoi" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>


                                         <tr>
                                            <td style="padding-left: 15px">Tôi là đại lý</td>
                                            <td></td>
                                            <td>
                                                 <span class='thongkesss'><asp:Literal ID="toiladaily" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>


                                         <tr>
                                            <td style="padding-left: 15px">Tôi là đối tượng khác</td>
                                            <td></td>
                                            <td>
                                                 <span class='thongkesss'><asp:Literal ID="toiladoituongkhac" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" />Thống kê hoa hồng bán hàng</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Số điểm gốc</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="tongdiemgoc" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>

                                         <tr>
                                            <td style="padding-left: 15px">Số điểm đã chia HH</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="sodiemdachiahh" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Số điểm còn lại</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="sodiemconlai" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                       <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" />Thống kê hoa hồng kích hoạt 480</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Số điểm gốc</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="sodiemgoc" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Số điểm đã chia HH</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="ltdiemdachiahhdk" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Số điểm còn lại</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="sodiemconlaidk" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>

                                         <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" />Thống kê sau khi trừ thuế 10%</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Số điểm thu thuế 10%</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="sodiemthuthue" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" />Thống kê Rút Tiền</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Tổng số điểm đã rút</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="tongtiendarut" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>



                                         <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" />Thống kê Admin cấp điểm</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Admin cấp ví thương mại</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="adminVITM" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                          <tr>
                                            <td style="padding-left: 15px">Admin cấp ví Quản Lý</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="viquanly" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>

                                           <tr>
                                            <td style="padding-left: 15px">Admin cấp THƯỞNG MUA HÀNG</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="vivip" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" />Thống kê tổng thu nhập chênh lệch giá</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Tổng điểm chênh lệch</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="tongdiemchenhlech" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>


                                         <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" />Thống kê Thành viên</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Tổng thành viên</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="tongthanhvien" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Tổng thành viên đã kích hoạt</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="thanhviendakichhoat" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Tổng thành viên chưa kích hoạt</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="thanhvienchuakichhoat" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Tổng Level 0</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="Level0" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Tổng Level 1</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="Level1" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Tổng Level 2</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="Level2" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Tổng Level 3</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="Level3" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Tổng Level 4</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="Level4" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Tổng Level 5</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="Level5" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Tổng chi nhánh</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="ltchinhanh" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td style="padding-left: 15px">Tổng thành viên đại lý</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="thanhviendaily" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Tổng thành viên Nhà Cung Cấp</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="thanhviennhacungcap" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Tổng thành viên Nhà Cung Cấp đã đăng sản phẩm</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="thanhviendangsanpham" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>

                                           <tr>
                                            <td style="padding-left: 15px">Tổng thành viên  bị khóa</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="ltbikhoa" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                           <tr>
                                            <td style="padding-left: 15px">Tổng thành viên tạm khóa chứa năng mua bán , chuyển , rút,...</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="ltkhoachucnang" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                             <tr>
                                            <td style="padding-left: 15px">Tổng thành viên cửa hàng</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="ltcuahang" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                              <tr>
                                            <td style="padding-left: 15px">Tổng thành viên leader</td>
                                            <td></td>
                                            <td>
                                                <span class='thongkesss'><asp:Literal ID="leader" runat="server"></asp:Literal> </span>
                                            </td>
                                        </tr>
                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>

                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>
                                       
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
