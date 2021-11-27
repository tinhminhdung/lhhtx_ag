<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Detailorders.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.Detailorders" %>
<section class="login page_order_account">
    <div class="">
        <div class="row">
            <div class="col-xs-12 col-sm-12 col-md-12 top_order_title">
                <h1 class="title-headding order-headding">Đơn hàng #<asp:Literal ID="ltmadonhang" runat="server"></asp:Literal></h1>
                <span class="note order_date"><i>Ngày tạo —
                    <asp:Literal ID="ltngaydathang" runat="server"></asp:Literal></i>
                    <a href="/lich-su-mua-hang.html"><i class="fa fa-arrow-left" aria-hidden="true"></i>&nbsp;Quay lại danh sách đơn hàng</a>
                </span>
            </div>
            <div class="col-xs-12 col-sm-12 col-md-12 body_order">
                <div id="order_payment" class="span12 box-address margin-top-20">
                    <div class="box-header">
                        <h2 class="title-head">Địa chỉ giao hàng</h2>
                        <br />
                        <p>
                            <span class="note">Trạng thái thanh toán:</span>
                            <i i="" class="status_pending">
                                <asp:Literal ID="lttrangthai" runat="server"></asp:Literal>
                            </i>
                        </p>
                    </div>
                    <div class="address note">
                        <p>
                            <i class="fa fa-user"></i>
                            <asp:Literal ID="lthovaten" runat="server"></asp:Literal>
                        </p>
                        <p>
                            <i class="fa fa-map-marker"></i>
                            <asp:Literal ID="ltdiachi" runat="server"></asp:Literal>
                        </p>
                        <p>
                            <i class="fa fa-phone"></i>
                            <asp:Literal ID="ltdienthoai" runat="server"></asp:Literal>
                        </p>
                        <p>
                            <asp:Literal ID="ltnoidung" runat="server"></asp:Literal>
                        </p>
                    </div>
                </div>
            </div>
            <div class="col-xs-12 col-sm-12 col-md-12">
                <asp:Literal ID="ltthongbao" runat="server"></asp:Literal>
                <div class="table-responsive-block margin-top-20 table-responsive">
                    <table id="order_details" class="table table-cart scollldh">
                        <thead class="thead-default">
                            <tr>
                                <th style="border-bottom: none;">STT</th>
                                <th style="border-bottom: none;">Code</th>
                                <th style="border-bottom: none; width: 210px;">Sản phẩm</th>
                                <th style="border-bottom: none; width: 180px;">Trạng thái NCC</th>
                                <th style="border-bottom: none; width: 180px;">Trạng thái Người mua</th>
                                <th style="border-bottom: none;">Giá</th>
                                <th style="border-bottom: none;">Số lượng</th>
                                <th style="border-bottom: none;">Tổng</th>
                                <th style="border-bottom: none;">Điểm thanh toán</th>
                                <%--<th style="border-bottom: none;">Điểm thưởng</th>--%>

                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rpcartdetail" OnItemCommand="rpcartdetail_ItemCommand" runat="server">
                                <ItemTemplate>
                                    <tr style="height: 20px" class="tr_while" onmouseover="this.className='tr_while_over'" onmouseout="this.className='tr_while'">
                                        <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%=i++ %><asp:HiddenField ID="hdids" Value='<%# Eval("ID") %>' runat="server" />
                                        </td>
                                        <td style="padding-left: 5px; text-align: left !important" class="cartadmin"><%#Codes(Eval("ipid").ToString())%></td>
                                        <td style="padding-left: 5px; text-align: left !important" class="cartadmin"><%#DataBinder.Eval(Container.DataItem,"Name")%></td>
                                        <td style="padding-left: 5px; text-align: center !important" class="cartadmin">
                                            <%#ShowtrangthaiNCC(DataBinder.Eval(Container.DataItem, "TrangThaiNhaCungCap").ToString(),DataBinder.Eval(Container.DataItem, "LyDoHuyHang").ToString())%>
                                        </td>
                                        <td style="padding-left: 5px; text-align: center !important" class="cartadmin">
                                                    <div class="TTDuyetdon" style='<%#NhaCungCapDaDuyet(Eval("TrangThaiNhaCungCap").ToString())%>'>
                                                <asp:LinkButton Visible='<%#EnableLock_DuyetHang(Eval("TrangThaiNguoiMuaHang").ToString())%>' ID="LinkButton1" OnLoad="ChapNhan_Load" CommandName="ChapNhan" OnClientClick="return JSConfirm('#order');"  CssClass="Duyetdon" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server">Chấp nhận</asp:LinkButton>
                                             
<%--                                                <asp:LinkButton Visible='<%#EnableLock_DuyetHang(Eval("TrangThaiNguoiMuaHang").ToString())%>' ID="LinkButton2" OnLoad="TraHang_Load" CommandName="TraHang" CssClass="huydon" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server">Trả hàng</asp:LinkButton>--%>
                                                <button type="button" class="btn btn-primary" style='<%#EnableLock_TraHangstyle(Eval("TrangThaiNguoiMuaHang").ToString())%>; background: #8fb004; color: #fff; margin: auto; margin-bottom: 9px; width: 100px;' data-toggle="modal" onclick="GanGiaTri('<%#DataBinder.Eval(Container.DataItem,"ID")%>')" data-target="#exampleModal">Trả hàng</button>
                                            </div>
                                        
                                             <asp:LinkButton Visible='<%#EnableLock_XoaDon(Eval("TrangThaiKhieuKien").ToString(),Eval("TrangThaiNhaCungCap").ToString(),Eval("TrangThaiNguoiMuaHang").ToString())%>' ID="LinkButton3" OnLoad="XoaDon_Load" CommandName="XoaDon" CssClass="Duyetdon" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server">Hủy sản phẩm này</asp:LinkButton>
                                            <%#Showtrangthai(DataBinder.Eval(Container.DataItem, "TrangThaiNguoiMuaHang").ToString())%><br />
                                          <br /> <%#ShowLyDoTraHang(Eval("LyDoTraHang").ToString())%>


                                        </td>
                                        <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%#AllQuery.MorePro.Detail_Price(Eval("Price").ToString())%> <%=Commond.Setting("Dongiapro") %></td>
                                        <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%#DataBinder.Eval(Container.DataItem,"Quantity")%></td>
                                        <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%#AllQuery.MorePro.Detail_Price(Eval("Money").ToString())%> <%=Commond.Setting("Dongiapro") %></td>
                                        <td style="padding-left: 5px; text-align: center !important" class="cartadmin">
                                            <%#(Eval("TongTienThanhToan").ToString())%><br />
                                             <%#LayTuViNao(DataBinder.Eval(Container.DataItem, "TienTuViNao").ToString())%><br />
                                        </td>
<%--                                        <td style="padding-left: 5px; text-align: center !important" class="cartadmin"><%#HoaHongTheoLevel_TheoThoiDiemMuahang_News(Eval("HoaHongTheoLevel").ToString(),Eval("Diemcoin").ToString())%> </td>--%>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                        </tbody>
                    </table>

                </div>
            </div>

            <div style="clear: both; height: 20px"></div>
            <div style=" padding:10px;">
                <asp:Literal ID="ltthongtin" runat="server"></asp:Literal>
            </div>
            <div style="clear: both; height: 20px"></div>


            <div class="col-xs-12 oder_total_monney" style="display: none">
                <table class="table  totalorders">
                    <tfoot>

                        <tr class="order_summary note" style="color: red;">
                            <td class="fix-width-200" colspan="">Tổng tiền bằng chữ:</td>
                            <td class="total money right"><strong style="color: #fe3232">
                                <asp:Literal ID="lttongtienbangchu" runat="server"></asp:Literal></strong></td>
                        </tr>

                        <tr class="order_summary order_total">
                            <td class="fix-width-200">Tổng tiền:</td>
                            <td class="right"><strong>
                                <asp:Literal ID="lttongtien" runat="server"></asp:Literal>
                                ₫ </strong></td>
                        </tr>
                    </tfoot>
                </table>
            </div>
        </div>
    </div>
</section>
<asp:HiddenField ID="hdid" runat="server" />
<asp:HiddenField ID="hdIDGiohang" Value="0" runat="server" />
<asp:HiddenField ID="hdvalue" Value="0" runat="server" />
<asp:HiddenField ID="hdHoaHongGioiThieuF" Value="0" runat="server" />
<asp:HiddenField ID="hdgiatri" runat="server" Value="0" />
<script>
    function GanGiaTri(id) {
        debugger;
        document.getElementById("<%=hdgiatri.ClientID%>").value = id;
    }
</script>
<div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
<div class="modal-dialog" role="document">
<div class="modal-content">
    <div class="modal-header">
        <h5 class="modal-title" id="exampleModalLabel">Vui lòng điền nội dung trả lại hàng.</h5>
        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
            <span aria-hidden="true">&times;</span>
        </button>
    </div>
    <div class="modal-body">
        <asp:TextBox ID="txtnoidung" ValidationGroup="checkthongtin" TextMode="MultiLine" placeholder="Vui lòng điền nội dung trả lại hàng." Style="width: 100%; height: 100px;" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="checkthongtin" ControlToValidate="txtnoidung" ErrorMessage="Vui lòng nhập thông tin vào ô nội dung."></asp:RequiredFieldValidator>
         <asp:RegularExpressionValidator ID="RequiredFieldValidator2" ValidationExpression="^[\s\S]{10,}$"  runat="server" ControlToValidate="txtnoidung" ErrorMessage="Yêu cầu nhập thông tin trên 10 ký tự trở lên!" ValidationGroup="checkthongtin"></asp:RegularExpressionValidator>
    </div>
    <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
        <span runat="server" style='float: right; margin-left: 6px;' >
            <asp:Button ID="btTrahang" class="btn btn-primary " ValidationGroup="checkthongtin"  OnClick="btTrahang_Click" runat="server" Text="Chấp nhận trả hàng" />
        </span>
    </div>
</div>
</div>
</div>

<script type="text/javascript">
    var submit = 0;
    function JSConfirm(btnSubmit) {
        var result = confirm("Bạn có chắc chắn?");
        if (result) {
            if (++submit > 1) {
                alert('Dữ liệu đang được xử lý. Thanks!');
                return false;
            }
            else {
                $(btnSubmit).submit();
            }
        }
        else {
            return false;
        }
    }
</script>

