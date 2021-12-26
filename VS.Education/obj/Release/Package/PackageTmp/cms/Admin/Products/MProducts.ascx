<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MProducts.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.Products.MProducts" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script src="/Scripts/ckfinder/ckfinder.js" type="text/javascript"></script>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<link href="/cms/Admin/Products/Css/Style.css" rel="stylesheet" type="text/css" />
<script src="/cms/Admin/Products/Css/star-rating-svg/jquery.star-rating-svg.js" type="text/javascript"></script>
<link href="/cms/Admin/Products/Css/star-rating-svg/star-rating-svg.css" rel="stylesheet" type="text/css" />

<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
<div id="cph_Main_ContentPane">
    <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
        <ul>
            <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang chủ</a></li>
            <li class="Last"><span>Danh sách sản phẩm</span></li>
        </ul>
    </div>
    <div style="clear: both;"></div>
    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
        <asp:View ID="View1" runat="server">
            <div class="row-fluid">
                <div class="span12">
                    <div class="widget">
                        <div class="widget-title">
                            <h4><i class="icon-reorder"></i>Danh sách sản phẩm</h4>
                            <div class="ui-widget-content ui-corner-top ui-corner-bottom">
                                <div id="toolbox">
                                    <div class="toolbox-content" style="float: right; margin-top: 9px;">
                                        <asp:Label ID="ltmsg" runat="server" Font-Bold="True" ForeColor="Red" Visible="True"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="widget-body">
                            <div class="row-fluid">
                                <div class="span12">
                                    <div id="sample_1_length" class="dataTables_length">
                                        <div class="frm_search">
                                            <div>
                                                <asp:TextBox ID="txtkeyword" runat="server" CssClass="txt_csssearch" Width="400px"></asp:TextBox>
                                                <asp:LinkButton ID="lnksearch" runat="server" OnClick="lnksearch_Click" CssClass="vadd toolbar btn btn-info" Style="margin-top: -9px;"> <i class="icon-search"></i>&nbsp;Tìm kiếm</asp:LinkButton>
                                                <asp:Label ID="ltthongbao" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                            </div>
                                            <div style="margin-top: 10px;">
                                                <asp:DropDownList ID="ddlcategories" CssClass="txt" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlcategories_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                                    <asp:ListItem Value="-1" Selected="True">-- Duyệt --</asp:ListItem>
                                                    <asp:ListItem Value="1">Duyệt lần 2 (Hiển thị ra Website)</asp:ListItem>
                                                    <asp:ListItem Value="2">Duyệt lần 1 (Duyệt tạm)</asp:ListItem>
                                                    <asp:ListItem Value="3">Yêu cầu xem lại</asp:ListItem>
                                                    <asp:ListItem Value="4">NCC cập nhật lại</asp:ListItem>
                                                    <asp:ListItem Value="5">NCC đã xóa</asp:ListItem>
                                                    <asp:ListItem Value="0">Chưa duyệt</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlnhaphanphoi" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlnhaphanphoi_SelectedIndexChanged" CssClass="txt">
                                                    <asp:ListItem Value="-1">== Chọn thông tin ==</asp:ListItem>
                                                    <asp:ListItem Value="1">Tôi là nhà sản xuất</asp:ListItem>
                                                    <asp:ListItem Value="2">Tôi nhà phân phối</asp:ListItem>
                                                    <asp:ListItem Value="3">Tôi là đại lý</asp:ListItem>
                                                    <asp:ListItem Value="4">Tôi là đối tượng khác</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlloctheocheck" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlloctheocheck_SelectedIndexChanged" CssClass="txt">
                                                    <asp:ListItem Value="-1">== Chọn thông xuất hiện ==</asp:ListItem>
                                                    <asp:ListItem Value="1">Trang chủ</asp:ListItem>
                                                    <asp:ListItem Value="2">SP.Chiến lược</asp:ListItem>
                                                    <asp:ListItem Value="3">Sản phẩm Gợi ý</asp:ListItem>
                                                    <asp:ListItem Value="4">Có thể bạn thích</asp:ListItem>
                                                    <asp:ListItem Value="5">Nổi bật</asp:ListItem>
                                                    <asp:ListItem Value="6">Bán chạy</asp:ListItem>
                                                    <asp:ListItem Value="7">Khuyến mãi</asp:ListItem>
                                                </asp:DropDownList>

                                                <%--           <asp:DropDownList ID="ddltrangthaithanhvien" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddltrangthaithanhvien_SelectedIndexChanged">
                                                            <asp:ListItem Value="0" Selected="True">Tất cả thành viên</asp:ListItem>
                                                            <asp:ListItem Value="1">Hiển thị theo Admin</asp:ListItem>
                                                            <asp:ListItem Value="2">Hiển thị theo thành viên</asp:ListItem>
                                                        </asp:DropDownList>--%>
                                                <asp:DropDownList ID="ddlorderby" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="Create_Date">S.xếp:Ngày cập nhật</asp:ListItem>
                                                    <%-- <asp:ListItem Value="Modified_Date">S.xếp:Ngày hết hạn</asp:ListItem>--%>
                                                    <asp:ListItem Value="Price">S.xếp:Theo giá hiện tại</asp:ListItem>
                                                    <asp:ListItem Value="Views">S.xếp:Lần xem</asp:ListItem>
                                                    <asp:ListItem Value="Name">S.xếp:Tiêu đề (ABC)</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlordertype" runat="server" AutoPostBack="True" CssClass="txt" OnSelectedIndexChanged="ddlordertype_SelectedIndexChanged">
                                                    <asp:ListItem Value="desc">Giảm dần</asp:ListItem>
                                                    <asp:ListItem Value="asc">Tăng dần</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlNhomchietkhau" runat="server" AutoPostBack="True" CssClass="txt" OnSelectedIndexChanged="ddlNhomchietkhau_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0"> == Lọc theo % chiết khấu ==</asp:ListItem>
                                                    <asp:ListItem Value="1"><=10 %</asp:ListItem>
                                                    <asp:ListItem Value="2"><=20 %</asp:ListItem>
                                                    <asp:ListItem Value="3"><=30 %</asp:ListItem>
                                                    <asp:ListItem Value="4"><=40 %</asp:ListItem>
                                                    <asp:ListItem Value="5"><=50 %</asp:ListItem>
                                                    <asp:ListItem Value="6"><=60 %</asp:ListItem>
                                                    <asp:ListItem Value="7"><=70 %</asp:ListItem>
                                                    <asp:ListItem Value="8"><=80 %</asp:ListItem>
                                                    <asp:ListItem Value="9"><=90 %</asp:ListItem>
                                                    <asp:ListItem Value="10"><=100 %</asp:ListItem>
                                                </asp:DropDownList>

                                                <asp:DropDownList ID="ddlNhomchietkhauTV" Visible="false" runat="server" AutoPostBack="True" CssClass="txt" OnSelectedIndexChanged="ddlNhomchietkhauTV_SelectedIndexChanged">
                                                    <asp:ListItem Selected="True" Value="0"> == Lọc theo nhóm chiết khấu Thành viên ==</asp:ListItem>
                                                    <asp:ListItem Value="30">Nhóm A: => 50% : 30</asp:ListItem>
                                                    <asp:ListItem Value="20">Nhóm B: => 40% : 20</asp:ListItem>
                                                    <asp:ListItem Value="10">Nhóm C: => 30% : 10</asp:ListItem>
                                                    <asp:ListItem Value="5">Nhóm D: => 20% : 5</asp:ListItem>
                                                    <asp:ListItem Value="2">Nhóm E: => 10% : 2</asp:ListItem>
                                                    <asp:ListItem Value="1">Nhóm F: <= 10% : 1</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlvitri" Visible="false" runat="server" AutoPostBack="True" CssClass="txt" OnSelectedIndexChanged="ddlvitri_SelectedIndexChanged">
                                                    <asp:ListItem Value="0" Selected="True">Vị trí hiển thị</asp:ListItem>
                                                    <asp:ListItem Value="1">Số lượng - Hết hàng</asp:ListItem>
                                                    <asp:ListItem Value="2">Số lượng - Từ 0 đến 10</asp:ListItem>
                                                    <asp:ListItem Value="3">Số lượng - Từ 10 đến 50</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="span12" style="margin-left: 0px;">
                                    <div id="sample_1_length" class="dataTables_length">
                                        <asp:LinkButton ID="lnkduyettamthoi" Style="background: #000000" runat="server" OnClick="lnkduyettamthoi_Click" CssClass="vadd toolbar btn btn-info"> <i class="icon-save"></i>&nbsp;Duyệt tạm thời</asp:LinkButton>
                                        <asp:LinkButton ID="lnkyeucauxemlai" Style="background: #ed1c24" runat="server" OnClick="lnkyeucauxemlai_Click" CssClass="vadd toolbar btn btn-info"> <i class="icon-save"></i>&nbsp;Yêu cầu xem lại</asp:LinkButton>

                                        <asp:LinkButton ID="ltduyethienthi" Style="background: #317f2d" runat="server" OnClick="ltduyethienthi_Click" CssClass="vadd toolbar btn btn-info"> <i class="icon-eye-open"></i>&nbsp;Duyệt hiển thị</asp:LinkButton>
                                        <asp:LinkButton ID="ltHuyDuyet" Style="background: #a20937" runat="server" OnClick="ltHuyDuyet_Click" CssClass="vadd toolbar btn btn-info"> <i class="icon-save"></i>&nbsp;Chưa duyệt</asp:LinkButton>

                                        <asp:LinkButton ID="bntcapnhat" CssClass="vadd toolbar btn btn-info" Style="background: #ed1c24" OnClick="bntcapnhat_Click" runat="server"><i class="icon-save"></i> Cập nhật giá</asp:LinkButton>

                                        <asp:LinkButton ID="bthienthi" runat="server" OnClick="bthienthi_Click" CssClass="vadd toolbar btn btn-info"> <i class=" icon-folder-close"></i>&nbsp;Hiện thị</asp:LinkButton>
                                        <asp:LinkButton ID="btthemmoi" Visible="false" runat="server" Text="Thêm mới" OnClick="btthemmoi_Click" CssClass="vadd toolbar btn btn-info"><i class="icon-plus"></i>&nbsp;Thêm mới</asp:LinkButton>
                                        <asp:LinkButton ID="btDeleteall" ToolTip="Xóa những lựa chọn !" OnClientClick=" return confirmDelete(this);" runat="server" OnClick="btDeleteall_Click" CssClass="vadd toolbar btn btn-info"><i class="icon-trash"></i>&nbsp;Xóa</asp:LinkButton>
                                        <asp:LinkButton ID="LinkButton3" runat="server" Text="Thêm mới" OnClick="LinkButton3_Click" CssClass="vadd toolbar btn btn-info"><i class="icon-plus"></i>&nbsp; Xuất Exel</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div style="padding: 10px; color: red; font-size: 15px">Tổng sản phẩm:
                                <asp:Literal ID="lttong" runat="server"></asp:Literal></div>
                            <div style="padding: 1px; color: red; font-size: 13px"><b>Chú ý: Khi đăng (SẢN PHẨM CHIẾN LƯỢC) để được trừ vào (THƯỞNG MUA HÀNG)</b></div>
                            <div style="padding: 1px; color: red; font-size: 12px">- 10% (THƯỞNG MUA HÀNG) trừ cho thành viên mua sản phẩm chiến lược </div>
                            <div style="padding: 1px; color: red; font-size: 12px">- 30% trừ vào (THƯỞNG MUA HÀNG) cho thành viên mua sản phẩm thông thường ko phải sản phẩm chiến lược </div>

                            <div class="list_item">
                                <asp:Repeater ID="rpitems" runat="server" OnItemCommand="rpitems_ItemCommand1">
                                    <ItemTemplate>
                                        <tr height="40">
                                            <td style="text-align: center;">
                                                <asp:CheckBox ID="chkid" runat="server" onclick="javascript:changeColor(this,'white');" /><asp:HiddenField ID="hiID" Value='<%# Eval("ipid") %>' runat="server" />
                                            </td>
                                            <td style="text-align: center;">
                                                <%#MoreAll.MoreAll.DuyetBai(DataBinder.Eval(Container.DataItem, "Status").ToString())%><br />
                                                <br />
                                                <%-- <asp:LinkButton CommandName="ChangeStatus" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("Status")%>' runat="server" ID="Linkbutton3"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "Status").ToString())%></asp:LinkButton>--%>
                                                <asp:LinkButton ID="LinkButton1" CssClass="active action-link-button" CommandName="update" CommandArgument='<%#Eval("ipid") %>' runat="server"><i class="icon-edit"></i></asp:LinkButton>
                                                <asp:LinkButton CssClass="active action-link-button" OnLoad="Delete_Load" CommandName="delete" CommandArgument='<%#Eval("ipid") %>' ID="LinkButton2" runat="server"><i class="icon-trash"></i></asp:LinkButton>
                                            </td>

                                            <td style="text-align: center;" class="Imgsmall">
                                                <div style="padding-bottom: 5px; color: red; font-weight: bold;">Đã bán: <span style="background: #90bf49; color: #fff; padding: 5px; margin: 5px;"><%#ShowDaBanSanPham(Eval("ipid").ToString())%></span></div>

                                                <div style="position: relative;">
                                                    <span class="anhn"><a title="<%#Eval("Name")%>" target="_blank" href="/<%#Eval("TangName")%>_sp<%#Eval("ipid")%>.html"><%#MoreAll.MoreImage.Image(Eval("Images").ToString())%></a></span>
                                                    <%#Hethang(Eval("Quantity").ToString())%>
                                                </div>
                                                <%#MoreAll.MoreAll.NhaCC(DataBinder.Eval(Container.DataItem, "Phaply").ToString())%>
                                                <div style="clear: both; height: 10px;"></div>
                                                <%#ShowXemAnhPhapLy(Eval("ipid").ToString(),Eval("Noidung2").ToString())%>
                                            </td>
                                            <td>
                                                <%#DataBinder.Eval(Container.DataItem, "Name")%>
                                                <br />
                                                <%--<div class="sale-flash new">- <%#(Eval("ChietKhau").ToString())%> % </div>
                                                <div class="Boddernnhom">Đại lý: <%#(Eval("PhanTramChietKhauDaiLy").ToString())%> % </div>
                                                 <div class="Boddernnhom">Thành viên: <%#(Eval("PhanTramChietKhauThanhVien").ToString())%> % </div>--%>
                                                <%--    <div> <%#Commond.CapBacChietKhau((Eval("ChietKhau").ToString()))%></div>--%>
                                                <%--<div class="sale-flash new"><%#Giamgia(Eval("Price").ToString(),Eval("Giacongtynhapvao").ToString())%> </div>--%>
                                                <br />
                                                <%-- <asp:TextBox ID="txtTieude" Style="border: 1px solid #d7d7d7; border-radius: 3px; text-align: left; width: 95%; height: 50px" TextMode="MultiLine" Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' CssClass="txt_css" runat="server" OnTextChanged="txtTieude_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                                <%#MoreAll.MoreAll.TrangThaiAgLang(DataBinder.Eval(Container.DataItem, "TrangThaiAgLang").ToString())%>
                                            </td>
                                            <td>
                                                <span class="Boddernnhom"><%#ShowNhom(Eval("icid").ToString())%></span>
                                            </td>
                                            <%-- <td>
                                                        <b><a title="<%#Eval("Name")%>" target="_blank" href="/<%#Eval("TangName")%>.html"><%#MoreAll.MoreAll.Substring(Eval("Name").ToString(), 30)%></a></b>
                                                        <div class="Mausac"><%#MoreMau(Eval("ipid").ToString())%></div>
                                                        <div style="clear: left"></div>
                                                        <div class="Kichhuoc"><%#MoreSize(Eval("ipid").ToString())%></div>
                                                    </td>--%>
                                            <td>
                                                <div style="margin-bottom: 4px;"><span style="width: 15px;">N/Y:</span><asp:TextBox ID="txtgiacu" Style="width: 100px; text-align: center;" Text='<%#AllQuery.MorePro.FormatMoney_Cart(Eval("OldPrice").ToString())%>' onkeyup="valid(this,'quotes')" onblur="valid(this,'quotes')" CssClass="txt_css" runat="server"></asp:TextBox>VNĐ</div>
                                                <div style="margin-bottom: 4px;">
                                                    <span style="width: 15px;">Giá đại lý: </span>
                                                    <asp:TextBox ID="txtgiaban" Style="width: 100px; text-align: center;" Text='<%#AllQuery.MorePro.FormatMoney_Cart(Eval("Price").ToString())%>' onkeyup="valid(this,'quotes')" onblur="valid(this,'quotes')" CssClass="txt_css" runat="server"></asp:TextBox>VNĐ
                                                </div>

                                                <%--<div style="margin-bottom: 4px;">
                                                    <span style="width: 15px; color: red">Tặng TV Free: </span>
                                                    <asp:TextBox ID="txtthanhvienFree" Style="width: 100px; text-align: center; background: #a9cde7; color: #fff" Text='<%#AllQuery.MorePro.FormatMoney_Cart(Eval("GiaThanhVienFree").ToString())%>' CssClass="txt_css" runat="server">0</asp:TextBox><span style="color: red">Điểm</span>
                                                </div>
                                                <div style="margin-bottom: 4px;">
                                                    <span style="width: 15px; color: red">CK Đại Lý: </span>
                                                    <asp:TextBox ID="txtGiaChietKhauDaiLy" Style="width: 100px; text-align: center; background: #a9cde7; color: #fff" Text='<%#AllQuery.MorePro.FormatMoney_Cart(Eval("GiaChietKhauDaiLy").ToString())%>' CssClass="txt_css" runat="server">0</asp:TextBox><span style="color: red">Điểm</span>
                                                </div>
                                                <div style="margin-bottom: 4px;">
                                                    <span style="width: 15px; color: red">CK cửa hàng: </span>
                                                    <asp:TextBox ID="txtgiacuahangs" Style="width: 100px; text-align: center; background: #a9cde7; color: #fff" Text='<%#AllQuery.MorePro.FormatMoney_Cart(Eval("GiaCuaHang").ToString())%>' onkeyup="valid(this,'quotes')" onblur="valid(this,'quotes')" CssClass="txt_css" runat="server"></asp:TextBox><span style="color: red">Điểm</span>
                                                </div>--%>
                                               <%-- <div style="margin-bottom: 4px;">
                                                    <span style="width: 15px;">Giá đại lý: </span>
                                                    <asp:TextBox ID="txtgiacongtynhapvao" Style="width: 100px; text-align: center;" Text='<%#AllQuery.MorePro.FormatMoney_Cart(Eval("Giacongtynhapvao").ToString())%>' onkeyup="valid(this,'quotes')" onblur="valid(this,'quotes')" CssClass="txt_css" runat="server"></asp:TextBox>VNĐ
                                                </div>--%>
                                               <%-- <div style="margin-bottom: 4px;">
                                                    <span style="width: 15px;">Giá Lợi nhuận: </span>
                                                    <asp:TextBox ID="txtgiabanthanhvien" Style="width: 100px; text-align: center; background: #ed1c24; color: #fff" Text='<%#AllQuery.MorePro.FormatMoney_Cart(Eval("GiaThanhVien").ToString())%>' onkeyup="valid(this,'quotes')" onblur="valid(this,'quotes')" CssClass="txt_css" runat="server"></asp:TextBox>VNĐ
                                                </div>--%>
                                                <%--<div style="margin-bottom: 4px;"> <%#MoreImages(Eval("ipid").ToString(), Eval("icid").ToString())%></div>--%>
                                            </td>
                                            <td style="text-align: center;">
                                                <%#ShowThanhVien(DataBinder.Eval(Container.DataItem,"IDThanhVien").ToString())%>
                                            </td>
                                            <%--  <td style="text-align: center;">
                                                        <%#Commond.DiemTichLuyAdd(DataBinder.Eval(Container.DataItem,"GiaThanhVien").ToString(),DataBinder.Eval(Container.DataItem,"Giacongtynhapvao").ToString())%>
                                                    </td>--%>
                                            <td style="text-align: center;">
                                                <%#MoreAll.MoreAll.FormatDate(DataBinder.Eval(Container.DataItem, "Create_Date"))%>
                                                <div>
                                                    <asp:LinkButton ID="LinkButton4" CssClass="active action-link-button" CommandName="Chekdata" CommandArgument='<%#Eval("ipid") %>' runat="server"> <%#MoreAll.MoreAll.Enable_Date(DataBinder.Eval(Container.DataItem, "Chekdata").ToString())%></asp:LinkButton>
                                                </div>
                                            </td>
                                            <td style="text-align: center;">
                                                <%#DataBinder.Eval(Container.DataItem,"Views")%>
                                            </td>
                                            <td style="text-align: center;">

                                                <asp:LinkButton ID="LinkButton5" CssClass="active action-link-button" CommandName="updat_date" CommandArgument='<%#Eval("ipid") %>' runat="server"><i class=" icon-refresh"></i></asp:LinkButton>
                                            </td>
                                            <td style="text-align: center; display: none">
                                                <asp:TextBox ID="TextBox1" Text='<%#DataBinder.Eval(Container.DataItem, "Quantity")%>' CssClass="txt_css" Width="30px" runat="server" OnTextChanged="txtxQuantity_TextChanged" AutoPostBack="true"></asp:TextBox></td>

                                            <td style="text-align: center; display:none">
                                                <asp:LinkButton CommandName="ChangeHome" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("Home")%>' runat="server" ID="Linkbutton6"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "Home").ToString())%></asp:LinkButton>
                                            </td>
                                            <td style="text-align: center; background:RGB(255,169,3)">
                                                <asp:LinkButton CommandName="ChangeNews" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("News")%>' runat="server" ID="Linkbutton7"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "News").ToString())%></asp:LinkButton>
                                            </td>

                                            <td style="text-align: center;">
                                                <asp:LinkButton CommandName="ChangeCheck_01" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("Check_01")%>' runat="server" ID="Linkbutton8"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "Check_01").ToString())%></asp:LinkButton>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:LinkButton CommandName="ChangeCheck_02" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("Check_02")%>' runat="server" ID="Linkbutton9"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "Check_02").ToString())%></asp:LinkButton>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:LinkButton CommandName="ChangeCheck_03" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("Check_03")%>' runat="server" ID="Linkbutton10"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "Check_03").ToString())%></asp:LinkButton>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:LinkButton CommandName="ChangeCheck_04" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("Check_04")%>' runat="server" ID="Linkbutton11"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "Check_04").ToString())%></asp:LinkButton>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:LinkButton CommandName="ChangeCheck_05" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("Check_05")%>' runat="server" ID="Linkbutton12"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "Check_05").ToString())%></asp:LinkButton>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:LinkButton CommandName="ChangeCheck_06" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("KichHoatDaiLy")%>' runat="server" ID="Linkbutton13"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "KichHoatDaiLy").ToString())%></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                                            <tr class="trHeader" style="height: 25px">
                                                <td class="header">
                                                    <input id="Checkbox1" onclick="javascript:SelectAllCheckboxes(this,1);" type="checkbox" /></td>
                                                <td class="header">Chức năng</td>
                                                <td class="header">Hình ảnh</td>
                                                <td class="header" style="width: 300px;">Tên sản phẩm</td>
                                                <td class="header">Nhóm sản phẩm</td>
                                                <td class="header" style="width: 237px;">Giá</td>
                                                <td class="header">Thành viên</td>
                                                <%--<td class="header">Điểm<br /> Mua hàng</td>--%>
                                                <td class="header">Ngày tạo</td>
                                                <td class="header">Xem</td>
                                                <td class="header">Làm mới</td>
                                                <%--  <td class="header">Số lượng</td>--%>
                                                <%--<td class="header">Trang chủ</td>--%>
                                                <td class="header" style=" color:red">SP.Chiến lược</td>
                                                <td class="header">Sản phẩm<br />
                                                    Gợi ý</td>
                                                <td class="header">Có thể
                                                            <br />
                                                    bạn thích</td>
                                                <td class="header">Nổi bật</td>
                                                <td class="header">Bán chạy</td>
                                                <td class="header">Khuyến mãi</td>
                                                <td class="header">Kích hoạt<br />
                                                    Đại lý</td>
                                            </tr>
                                    </HeaderTemplate>
                                    <FooterTemplate>
                                        </TABLE>
                                    </FooterTemplate>
                                </asp:Repeater>
                                <asp:Label ID="lterr" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
                            </div>
                            <table border="0" cellpadding="0" style="border-collapse: collapse" width="100%">
                                <tr height="20">
                                    <td align="right">
                                        <asp:Literal ID="ltpage" runat="server"></asp:Literal>

                                        <div class="phantrang" style="">
                                            <cc1:CollectionPager ID="CollectionPager1" runat="server" BackNextDisplay="HyperLinks" BackNextLocation="Split"
                                                BackText="<<" ShowFirstLast="True" ResultsLocation="Bottom" PagingMode="QueryString" MaxPages="50" FirstText="Trang đầu" HideOnSinglePage="True" IgnoreQueryString="False" LabelStyle="font-weight: bold;color:red" LabelText="" LastText="Cuối cùng" NextText=">>" PageNumbersDisplay="Numbers"
                                                ResultsFormat="Hiển thị từ  {0} Đến {1} (của {2})" ResultsStyle="padding-bottom:5px;padding-top:14px;font-weight: bold;" ShowLabel="False" ShowPageNumbers="True" BackNextStyle="font-weight: bold; margin: 14px;" ControlCssClass="" ControlStyle="" UseSlider="True" PageNumbersSeparator="">
                                            </cc1:CollectionPager>
                                        </div>
                                    </td>
                                </tr>
                                <tr bgcolor="whitesmoke" height="25" style="display: none">
                                    <td style="height: 25px"><b>
                                        <asp:LinkButton ID="lnkcreatenew" runat="server" Font-Bold="True" OnClick="lnkcreatenew_Click" CssClass="lnk">[Thêm mới]</asp:LinkButton></b></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>

        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="widget-title">
                <h4><i class="icon-list-alt"></i>&nbsp;Thêm mới - cập nhật</h4>
            </div>
            <div class="widget-body widget">
                <div class="row-fluid">
                    <div class="span3">
                        <div class="dataTables_length" id="sample_1_length">
                            <asp:Literal ID="ltshowiavascrip" runat="server"></asp:Literal>
                            <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>
                <div class='frm-add'>
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td style="width: 145px"></td>
                            <td style="width: 5px"></td>
                            <td>
                                <asp:Label ID="lbl_msg" runat="server" Font-Bold="true" ForeColor="red"></asp:Label></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="height: 19px; color: #6d6d6d">Phân loại nhóm</td>
                            <td></td>
                            <td>
                                <asp:DropDownList ID="ddlcategoriesdetail" runat="server" CssClass="txt_css" Width="250px">
                                </asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="height: 19px; color: #6d6d6d">Sản phẩm thuộc</td>
                            <td></td>
                            <td>
                                <asp:DropDownList ID="ddlsanphanthuoc" runat="server" CssClass="txt_css" Width="250px">
                                    <asp:ListItem Value="0" Selected="True">== Lựa chọn kiểu sản phẩm ==</asp:ListItem>
                                    <asp:ListItem Value="1">Kiểu Sản Phẩm</asp:ListItem>
                                    <asp:ListItem Value="2">Kiểu Bất Động Sản</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td></td>
                        </tr>

                        <%-- <tr>
                                    <td style="height: 19px; color: #6d6d6d">Thương Hiệu</td>
                                    <td></td>
                                    <td>
                                        <asp:DropDownList ID="ddlthuonghieu" runat="server" CssClass="txt_css" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                </tr>--%>
                        <tr style="display: none">
                            <td style="height: 19px; color: #6d6d6d">Mầu</td>
                            <td></td>
                            <td>
                                <div class="Maunhe">
                                    <asp:CheckBoxList ID="cblcat" runat="server" RepeatColumns="10"></asp:CheckBoxList>
                                </div>
                            </td>
                            <td></td>
                        </tr>
                        <tr style="display: none">
                            <td style="height: 19px; color: #6d6d6d">Kích thước</td>
                            <td></td>
                            <td>
                                <div class="Kthuoc">
                                    <asp:CheckBoxList ID="ckichthuoc" runat="server" RepeatColumns="10"></asp:CheckBoxList>
                                </div>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="height: 19px; color: #6d6d6d">Mã vạch sản phẩm
                            </td>
                            <td></td>
                            <td>
                                <asp:TextBox ID="txtcode" runat="server" Width="250px" CssClass="txt_css"></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="height: 19px; color: #6d6d6d">Tiêu đề 
                            </td>
                            <td style="height: 10px"></td>
                            <td style="height: 10px">
                                <asp:TextBox ID="txtname" runat="server" Width="593px" CssClass="txt_css"></asp:TextBox>
                            </td>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="height: 19px; color: #6d6d6d">Rewrite Url
                            </td>
                            <td style="height: 10px"></td>
                            <td style="height: 10px">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtRewriteUrl" runat="server" CssClass="txt_css" Width="700px"></asp:TextBox>
                                        <asp:Button ID="btkiemtra" Style="margin-top: -11px;" runat="server" Text="Kiểm tra" CssClass="btn btn-primary" OnClick="btkiemtra_Click" />
                                        <br />
                                        <asp:Label ID="ltshowurl" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="height: 19px; color: #6d6d6d">Tiêu đề Alt ảnh
                            </td>
                            <td style="height: 10px"></td>
                            <td style="height: 10px">
                                <asp:TextBox ID="txtAlt" runat="server" Width="593px" CssClass="txt_css"></asp:TextBox>
                                <div style="font-size: 8pt; color: #ed1c24"><em>(Thẻ Tiêu đề thẻ Alt dành cho seo ảnh, nếu không điền thì sẽ mặc định là tiêu đề sản phẩm)</em></div>
                            </td>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr style="display: none">
                            <td style="height: 19px; color: #6d6d6d">Điểm mua hàng
                            </td>
                            <td style="height: 10px"></td>
                            <td style="height: 10px">
                                <asp:TextBox ID="txtdiemmuahang" runat="server" Width="150px" CssClass="txt_css"></asp:TextBox>
                            </td>
                            <td style="height: 10px"></td>
                        </tr>
                        <tr>
                            <td style="height: 19px; color: #6d6d6d">Hình ảnh
                            </td>
                            <td style="height: 18px"></td>
                            <td style="height: 18px">

                                <asp:TextBox ID="txtImage" runat="server" CssClass="text image"></asp:TextBox>
                                <input id="btnImage" type="button" onclick="BrowseServer('<%=txtImage.ClientID %>    ','News');" value="Chọn ảnh" class="toolbar btns btn-info" />
                                <asp:Literal ID="ltimgs" runat="server"></asp:Literal>
                                <asp:HiddenField ID="hdimgnews" runat="server" />
                                <div style="font-size: 8pt; color: #ed1c24"><em>(Kích thước ảnh của 2 ô: Rộng: 500px X Cao: (Tùy ý) Hoặc 500px --><b> Nếu ở chi tiết sản phẩm có zoom ảnh thì ảnh lên để to là Rộng:900px X Cao: (Tùy ý) Hoặc 900px</b>)</em></div>
                                <div style="display: none">
                                    <div align="left" style="float: left; width: 700px">
                                        <asp:RadioButton ID="rdFromComputer" runat="server" CssClass="txt_css2" AutoPostBack="True" Checked="true" GroupName="FromType" OnCheckedChanged="rdFromComputer_CheckedChanged" Text="Từ máy tính của bạn" ValidationGroup="downloadtype" />
                                        <asp:RadioButton ID="rdFromLinks" runat="server" CssClass="txt_css2" AutoPostBack="True" GroupName="FromType" OnCheckedChanged="rdFromLinks_CheckedChanged" Text="Từ 1 liên kết" />&nbsp;&nbsp;
                                                     <asp:Button ID="btDeleteimages" runat="server" Text="Delete" OnClick="btDeleteimages_Click" Width="75px" /><br />
                                        <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
                                            <asp:View ID="vwFromComputer" runat="server">
                                                <asp:FileUpload CssClass="txt_css" ID="flimage" runat="server" Width="323px" />
                                            </asp:View>
                                            <asp:View ID="vwFromLinks" runat="server">
                                                <asp:TextBox CssClass="txt_css" ID="txtvimg" runat="server" Width="99%"></asp:TextBox><br />
                                            </asp:View>
                                        </asp:MultiView>
                                    </div>
                                    <div style="padding: 0px 0px 0px 0px">
                                        <div class="adaidien">
                                            <asp:Literal ID="ltimg" runat="server"></asp:Literal>
                                        </div>
                                    </div>
                                </div>
                            </td>
                            <td style="height: 18px"></td>
                        </tr>
                        <tr>
                            <td style="height: 19px; color: #6d6d6d">Thêm nhiều ảnh
                            </td>
                            <td style="height: 7px"></td>
                            <td style="height: 7px">
                                <div>
                                    <asp:TextBox ID="txtMImage" runat="server" CssClass="text image"></asp:TextBox>
                                    <input id="btnBrowseImage" onclick="BrowseServerNew('<%=txtMImage.ClientID %>    ','Adv')" type="button" value="Chọn nhiều ảnh" class="toolbar btns btn-info" />
                                    <input id="btndelall" onclick="delall();" type="button" value="Xóa tất cả" class="toolbar btns btn-info" />
                                </div>
                                <div style="clear: both"></div>
                                <ul id="container-img"></ul>
                            </td>
                            <td style="height: 7px; font-size: 12pt; font-family: Times New Roman;"></td>
                        </tr>
                        <tr>
                            <td style="height: 19px; color: #6d6d6d">Thêm ảnh pháp lý
                            </td>
                            <td style="height: 7px"></td>
                            <td style="height: 7px">
                                <asp:TextBox ID="txtMImageSS" runat="server" CssClass="text image"></asp:TextBox>
                                <input id="btnBrowseImageSS" style="color: #fff; text-transform: uppercase" onclick="BrowseServerNewSS('<%=txtMImageSS.ClientID %>    ','Adv')" type="button" value="Thêm nhiều ảnh pháp lý (Nếu có)" class="toolbar btns btn-info" />
                                <input id="btndelallSS" onclick="delallSS();" style="color: #fff; text-transform: uppercase" type="button" value="Xóa tất cả" class="toolbar btns btn-info" />
                                <div style="clear: both"></div>
                                <div style="font-size: 8pt; color: red; width: 100%"><em>(Vui lòng đính kèm theo các giấy tờ sau: Giấy phép kinh doanh, Giấy công bố sản phẩm, giấy an toàn vệ sinh thực phẩm, giấy tờ liên quan (Nếu có))</em></div>
                                <div style="clear: both"></div>
                                <ul id="container-imgSS"></ul>
                            </td>
                            <td style="height: 7px; font-size: 12pt; font-family: Times New Roman;"></td>
                        </tr>
                        <tr>
                            <td style="height: 19px; color: #6d6d6d">Mô tả
                            </td>
                            <td style="height: 7px"></td>
                            <td style="height: 7px">
                                <CKEditor:CKEditorControl ID="txtdesc" runat="server" Toolbar="Basic"></CKEditor:CKEditorControl>
                            </td>
                            <td style="height: 7px; font-size: 12pt; font-family: Times New Roman;"></td>
                        </tr>
                        <tr>
                            <td style="height: 19px; color: #6d6d6d">Nội dung
                            </td>
                            <td></td>
                            <td>
                                <CKEditor:CKEditorControl ID="txtcontent" runat="server" Height="300px"></CKEditor:CKEditorControl>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="height: 19px; color: #6d6d6d">Trọng lượng
                            </td>
                            <td></td>
                            <td>
                                <asp:TextBox ID="txtTrongLuong" runat="server" Width="112px" CssClass="txt_css">0</asp:TextBox>
                                Gram
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="height: 19px; color: #6d6d6d">Chọn
                            </td>
                            <td></td>
                            <td>


                                <asp:DropDownList ID="ddlchon" size="10" Style="height: 120px !important" ValidationGroup="GInfo" runat="server">
                                    <asp:ListItem Value="0">== Chọn thông tin ==</asp:ListItem>
                                    <asp:ListItem Value="1">Tôi là nhà sản xuất</asp:ListItem>
                                    <asp:ListItem Value="2">Tôi nhà phân phối</asp:ListItem>
                                    <asp:ListItem Value="3">Tôi là đại lý</asp:ListItem>
                                    <asp:ListItem Value="4">Tôi là đối tượng khác</asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="*" Text="Vui lòng chọn thông tin" InitialValue="0" ControlToValidate="ddlchon" ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>




                        <tr>
                            <td style="height: 19px; color: #6d6d6d">Giá</td>
                            <td></td>
                            <td>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr valign="top">
                                        <td>Giá niêm yết</td>
                                        <td>Giá  đại lý</td>
                                        <td></td>
                                        <td></td>
                                        <td></td
                                        <td style="width: 150px"></td>
                                        <td></td>
                                    </tr>
                                    <tr valign="top">
                                         <td style="width: 100px;">
                                            <asp:TextBox ID="txtoldprice" runat="server" Width="112px" CssClass="txt_css"></asp:TextBox>
                                            / vnđ
                                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtoldprice"></cc1:FilteredTextBoxExtender>
                                        </td>

                                        <td style="width: 100px">
                                            <asp:TextBox ID="txtprice" runat="server" Width="112px" CssClass="txt_css">0</asp:TextBox>
                                            / vnđ
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtprice"></cc1:FilteredTextBoxExtender>
                                        </td>

                                        <td style="width: 100px; display:none">
                                            <asp:TextBox ID="txtGiaThanhVien" runat="server" Width="112px" CssClass="txt_css">0</asp:TextBox>
                                            / vnđ
                                        </td>

                                       <td style="width: 100px; display:none">
                                            <asp:TextBox ID="txtthanhvienFree" Style="width: 100px; text-align: center; background: #a9cde7; color: #fff" runat="server" Width="112px" CssClass="txt_css">0</asp:TextBox>
                                            / Điểm
                                        </td>
                                        <td style="width: 100px; display:none">
                                            <asp:TextBox ID="txtgiachietkhaudaily" Style="width: 100px; text-align: center; background: #a9cde7; color: #fff" runat="server" Width="112px" CssClass="txt_css">0</asp:TextBox>
                                            / Điểm
                                        </td>
                                        <%-- <td style="width: 100px">
                                                    <asp:TextBox ID="txtgiathanhvienfree" runat="server" Width="112px" CssClass="txt_css"></asp:TextBox>&nbsp;
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="txtgiathanhvienfree"></cc1:FilteredTextBoxExtender>
                                                </td>--%>
                                          <td style="width: 100px; display:none">
                                            <asp:TextBox ID="txtgiacongtynhapvao" runat="server" Width="112px" CssClass="txt_css"></asp:TextBox>
                                            / vnđ
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txtgiacongtynhapvao"></cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="width: 100px; display:none">
                                            <asp:TextBox ID="txtgiacuahang" Style="width: 100px; text-align: center; background: #a9cde7; color: #fff" runat="server" Width="112px" CssClass="txt_css"></asp:TextBox>
                                            / Điểm
                                        </td>
                                       
                                    </tr>
                                </table>
                                <span style="font-size: 8pt; color: #ed1c24"><em>(Luu ý: khi điền giá không có ký tự nào hay dấu chấm phẩy nào kèm theo mà chỉ điền dãy số : Ex:12000 - sau khi điền ở ngoài sẽ hiển thị là 12,000)</em></span>
                            </td>
                            <td></td>
                        </tr>

                        <tr>
                            <td>Tính năng seo
                            </td>
                            <td style="height: 7px"></td>
                            <td style="height: 7px">
                            <td></td>
                        </tr>

                        <tr>
                            <td valign="top">Tiêu đề từ khóa (Title)
                            </td>
                            <td valign="top"></td>

                            <td>
                                <asp:TextBox ID="txttitleseo" CssClass="txt_css" runat="server" Width="320px"></asp:TextBox>
                            </td>
                            <td></td>
                        </tr>
                        <tr>

                            <td valign="top">Nội dung hiển thị trong (Description)
                            </td>
                            <td></td>
                            <td>
                                <asp:TextBox ID="txtmeta" CssClass="txt_css" runat="server" Width="392px" Height="35px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td valign="top"></td>
                        </tr>
                        <tr>
                            <td valign="top">Nội dung hiển thị trong (Keyword)
                            </td>
                            <td></td>
                            <td>
                                <asp:TextBox ID="txtKeywordS" CssClass="txt_css" runat="server" Width="459px" Height="43px" TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <td valign="top"></td>
                        </tr>


                        <tr style="display: none">
                            <td>Thời gian</td>
                            <td></td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <asp:CheckBox ID="chkdaytype" runat="server" Text="Hiển thị trong thời gian" AutoPostBack="True" OnCheckedChanged="chkdaytype_CheckedChanged" />
                                        <asp:Panel ID="pnadddate" Visible="false" runat="server">
                                            Ngày đăng tin
                <br />
                                            <asp:TextBox ID="txtfromday" runat="server" CssClass="txt" Height="22px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtfromday_CalendarExtender0" runat="server" TargetControlID="txtfromday"></cc1:CalendarExtender>
                                            tồn tại trong
                <asp:TextBox ID="txtindays" runat="server" CssClass="txt" Width="48px">365</asp:TextBox>
                                            ngày
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:CheckBox ID="CheckHome" CssClass="txt_css2" runat="server" Font-Bold="False" Text="Trang chủ" />
                                <asp:CheckBox ID="Checknews" CssClass="txt_css2" runat="server" Font-Bold="False" Text="Sản phẩm chiến lược" />
                                <asp:CheckBox ID="Check_01" CssClass="txt_css2" runat="server" Font-Bold="False" Text="Sản phẩm gợi ý" />
                                <asp:CheckBox ID="Check_02" CssClass="txt_css2" runat="server" Font-Bold="False" Text="Sản phẩm có thể bạn thích" />
                                <asp:CheckBox ID="Check_03" CssClass="txt_css2" runat="server" Font-Bold="False" Text="Sản phẩm Nổi bật" />
                                <asp:CheckBox ID="Check_04" CssClass="txt_css2" runat="server" Font-Bold="False" Text="Sản phẩm bán chạy" />
                                <asp:CheckBox ID="Check_05" CssClass="txt_css2" runat="server" Font-Bold="False" Text="Sản phẩm khuyến mãi" />
                                <asp:CheckBox ID="checkAg" CssClass="txt_css2" runat="server" Font-Bold="False" Text="Sản phẩm tương tự này được bán trên Ag chưa?" />
                                <asp:CheckBox ID="Check_06" CssClass="txt_css2" runat="server" Font-Bold="False" Text="Sản phẩm điều kiện trở thành đại lý" />
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td style="height: 19px; color: #6d6d6d">Tùy chọn
                            </td>
                            <td></td>
                            <td>
                                <asp:DropDownList ID="ddltuychon" size="10" Style="height: 120px !important" ValidationGroup="GInfo" runat="server">
                                    <asp:ListItem Value="0">Chưa Duyệt</asp:ListItem>
                                    <asp:ListItem Value="1">Đã hiển thị</asp:ListItem>
                                    <asp:ListItem Value="2">Duyệt tạm thời</asp:ListItem>
                                    <asp:ListItem Value="3">Yêu cầu xem lại</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="*" Text="Vui lòng chọn thông tin" InitialValue="0" ControlToValidate="ddlchon" ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txtquantity" runat="server" Visible="False" Width="14px"></asp:TextBox></td>
                            <td></td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="hdinsertupdate" runat="server" Value="insert" />
                    <asp:HiddenField ID="hdid" runat="server" />
                    <asp:HiddenField ID="hdcid" runat="server" />
                    <asp:HiddenField ID="hdipid" runat="server" />
                    <asp:HiddenField ID="hdFileName" runat="server" />
                    <asp:HiddenField ID="hdimgsmall" runat="server" />
                    <asp:HiddenField ID="hdimgMax" runat="server" />
                    <asp:HiddenField ID="hdimgMaxEdit" runat="server" />
                    <asp:HiddenField ID="hdimgsmallEdit" runat="server" />
                    <asp:HiddenField ID="hdthanhvien" runat="server" />
                </div>

                <div style="height: 20px;"></div>
                <div style="padding-left: 120px;">
                    <asp:LinkButton ID="btnsave" runat="server" OnClick="btnsave_Click" ValidationGroup="GInfo" CssClass="toolbar btn btn-info" Style="background: #ed1c24"> <i class="icon-save"></i>  Cập nhật </asp:LinkButton>
                    <asp:LinkButton ID="btncancel" runat="server" OnClick="btncancel_Click" CssClass="toolbar btn btn-info"> <i class="icon-chevron-left"></i>Hủy</asp:LinkButton>

                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</div>
<%--</ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnsave" />
        <asp:PostBackTrigger ControlID="btkiemtra" />
    </Triggers>
</asp:UpdatePanel>--%>

<style>
    .list_item {
        width: 100%;
        overflow-x: scroll;
    }

        .list_item table {
            width: 2000px;
        }
</style>
<%--
<div class="my-rating5"></div>
<script>
    $(".my-rating5").starRating({
        initialRating: 4,
        strokeColor: '#894A00',
        strokeWidth: 10,
        starSize: 25,
         readOnly: true
    });
</script>
--%>
<script type="text/javascript">
    $('[id*=btnBrowseImage]').each(function () {
        $(this).click(function () {
            BrowseServerNew(<%=txtMImage.ClientID%>, '');
        });
    });
    $("#container-img").sortable({
        stop: function (event, ui) {
            $('#<%=txtMImage.ClientID%>').val(GetStringImg());
        }
    });
        function delall() {
            $("#container-img").html('') ;
            $('#<%=txtMImage.ClientID%>').val(GetStringImg());
        }
        function BrowseServerNew(functionData, startupPath) {

            var finder = new CKFinder();
            finder.basePath = '~/scripts/ckfinder/';
            finder.startupPath = startupPath;
            finder.selectActionFunction = SetFileFieldNew;
            finder.selectActionData = functionData;
            finder.popup();
        }
        function SetFileFieldNew(fileUrl, data, allFiles) {
            var str = "";
            var strimg ="";
            allFiles.forEach(function(item) {
                strimg += "<li class='ui-state-default'><div class='box-img'><a href='javascript:void(0)' onclick=\"delimg($(this),'" + data["selectActionData"] + "');\" class='btn-close'>x</a> <img src='" + item.url + "' /> </div></li>";
            })
            $("#container-img").html($("#container-img").html() + strimg);
            $("#container-img").sortable({
                stop: function (event, ui) {
                    $('#<%=txtMImage.ClientID%>').val(GetStringImg());
                }
            });
                $("#container-img").disableSelection();
                alert(data["selectActionData"]);
                $('#'+data["selectActionData"]).val(GetStringImg());
            }
            function LoadStringImg(strImg, inputimg) {
                var arr = strImg.split(',');
                var strimg="";
                arr.forEach(function(item) {
                    strimg += "<li class='ui-state-default'><div class='box-img'><a href='javascript:void(0)' onclick=\"delimg($(this),'" + inputimg + "');\" class='btn-close'>x</a> <img src='" + item + "' /> </div></li>";
                })
                $("#container-img").html($("#container-img").html() + strimg);
                $("#container-img").sortable({
                    stop: function (event, ui) {
                        $('#<%=txtMImage.ClientID%>').val(GetStringImg());
                    }
                });
                    $("#container-img").disableSelection();
                    $('#<%=txtMImage.ClientID%>').val(GetStringImg());
                }
                function GetStringImg() {
                    var str = "";
                    $(".box-img img").each(function () {
                        str += $(this).attr('src') + ',';
                    })
                    return str;
                }
                function delimg(img, inputimg)
                {
                    img.parent().parent().remove();
                    $('#'+ inputimg).val(GetStringImg());
                }
</script>

<script type="text/javascript">
    $('[id*=btnBrowseImageSS]').each(function () {
        $(this).click(function () {
            BrowseServerNewSS(<%=txtMImageSS.ClientID%>, '');
        });
    });
    $("#container-imgSS").sortable({
        stop: function (event, ui) {
            $('#<%=txtMImageSS.ClientID%>').val(GetStringImgSS());
          }
      });
          function delallSS() {
              $("#container-imgSS").html('') ;
              $('#<%=txtMImageSS.ClientID%>').val(GetStringImgSS());
                     }
                     function BrowseServerNewSS(functionData, startupPath) {
                         var finder = new CKFinder();
                         finder.basePath = '~/scripts/ckfinder/';
                         finder.startupPath = startupPath;
                         finder.selectActionFunction = SetFileFieldNewSS;
                         finder.selectActionData = functionData;
                         finder.popup();
                     }
                     function SetFileFieldNewSS(fileUrl, data, allFiles) {
                         debugger;
                         var str = "";
                         var strimg ="";
                         allFiles.forEach(function(item) {
                             strimg += "<li class='ui-state-default'><div class='box-imgSS'><a href='javascript:void(0)' onclick=\"delimgSS($(this),'<%=txtMImageSS.ClientID%>');\" class='btn-close'>x</a> <img src='" + item.url + "' /> </div></li>";
                        })
                        $("#container-imgSS").html($("#container-imgSS").html() + strimg);
                        $("#container-imgSS").sortable({
                            stop: function (event, ui) {
                                $('#<%=txtMImageSS.ClientID%>').val(GetStringImgSS());
                            }
                        });
                            $("#container-imgSS").disableSelection();
                            $('#<%=txtMImageSS.ClientID%>').val(GetStringImgSS());
                        //  $('#'+data["selectActionData"]).val(GetStringImgSS());
                     }
                     function LoadStringImgSS(strImg, inputimg) {
                         debugger;
                         var arr = strImg.split(',');
                         var strimg="";
                         arr.forEach(function(item) {
                             strimg += "<li class='ui-state-default'><div class='box-imgSS'><a href='javascript:void(0)' onclick=\"delimgSS($(this),'<%=txtMImageSS.ClientID%>');\" class='btn-close'>x</a> <img src='" + item + "' /> </div></li>";
                         })
                         $("#container-imgSS").html($("#container-imgSS").html() + strimg);
                         $("#container-imgSS").sortable({
                             stop: function (event, ui) {
                                 $('#<%=txtMImageSS.ClientID%>').val(GetStringImgSS());
                                }
                            });
                                $("#container-imgSS").disableSelection();
                            }
                            function GetStringImgSS() {
                                debugger;
                                var str = "";
                                $(".box-imgSS img").each(function () {
                                    str += $(this).attr('src') + ',';
                                })
                                return str;
                            }
                            function delimgSS(img, inputimg)
                            {
                                img.parent().parent().remove();
                                $('#'+ inputimg).val(GetStringImgSS());
                            }
</script>
<script type="text/javascript">
    var r = {
        'special': /[\W]/g,
        'quotes': /[^0-9^]/g,
        'notnumbers': /[^a-zA]/g
    }
    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
    }
    var substringMatcher = function (strs) {
        return function findMatches(q, cb) {
            var matches; matches = []; substrRegex = new RegExp(q, 'i'); $.each(strs, function (i, str)
            { if (substrRegex.test(str)) { matches.push(str); } }); cb(matches);
        };
    };
</script>

