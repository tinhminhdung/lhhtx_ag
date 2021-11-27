<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MarketingSenmail.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.Marketing.MarketingSenmail" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script src="/Scripts/ckfinder/ckfinder.js" type="text/javascript"></script>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>

<div class="container-fluid">
    <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
        <ul>
            <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang chủ</a></li>
            <li class="Last"><span>Gửi thông báo đến thành viên</span></li>
        </ul>
    </div>
    <div class="row-fluid">
        <div class="span12 sortable">
            <div class="widget">
                <div class="widget-title">
                    <h4><i class="icon-reorder"></i>Gửi thông báo đến thành viên</h4>
                    <span class="tools">
                        <a href="javascript:;" class="icon-chevron-down"></a>
                        <a href="javascript:;" class="icon-remove"></a>
                    </span>
                </div>
                <div class="widget-body">
                    <div class='frm-add'>
                        <div style="padding: 5px">
                            <table width="100%">
                                <div align="left" style="padding-left: 70px; padding-bottom: 10px">
                                    <asp:Label ID="lblmsg" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
                                </div>

                                <tr>
                                    <td colspan="2"></td>
                                    <td><span style="font-size: 8pt; color: #ed1c24"><em>Nhập địa chỉ (cách nhau bằng dấu phẩy).</em></span></td>
                                </tr>
                                <tr>
                                    <td valign="top" style="width: 65px" class="sendto">Danh sách 
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="txtto" CssClass="txt_css" TextMode="MultiLine" runat="server" Height="50px" Width="97%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="top" style="width: 65px">
                                        <strong>Nội dung</strong>
                                    </td>
                                    <td></td>
                                    <td>
                                        <CKEditor:CKEditorControl ID="txtcontent" runat="server" Toolbar="Basic"></CKEditor:CKEditorControl>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btcapnhatthongbao" Style="background: #ed1c24" runat="server" Text="Lưu cấu hình" CssClass="vadd toolbar btn btn-info" OnClick="btcapnhatthongbao_Click" />&nbsp;
                                        
                                        <asp:Button ID="btnsend" runat="server" Text="Gửi thông báo" CssClass="btn btn-primary" OnClick="btnsend_Click" />&nbsp;
                                     <asp:Button ID="btnhuy" runat="server" Text="Hủy bỏ" CssClass="btn btn-primary" />
                                    </td>
                                </tr>
                            </table>

                            <br />
                            <div id="sample_1_length" class="dataTables_length">
                                <div class="frm_search">
                                    <div>
                                        <asp:TextBox ID="txtkeyword" runat="server" CssClass="txt_csssearch" Width="400px"></asp:TextBox>
                                        <asp:LinkButton ID="lnksearch" runat="server" OnClick="lkbsearch_Click" CssClass="vadd toolbar btn btn-info" Style="margin-top: -9px;">  <i class="icon-search"></i>&nbsp;Tìm kiếm</asp:LinkButton>
                                    </div>
                                    <div style="margin-top: 10px;">
                                        <asp:DropDownList ID="ddlchinhanh" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlchinhanh_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlcapdo" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlcapdo_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="-1">= Tất cả cấp độ Level =</asp:ListItem>
                                            <asp:ListItem Value="0">1 Sao</asp:ListItem>
                                            <asp:ListItem Value="1">2 Sao</asp:ListItem>
                                            <asp:ListItem Value="2">3 Sao</asp:ListItem>
                                            <asp:ListItem Value="3">4 Sao</asp:ListItem>
                                            <asp:ListItem Value="4">5 Sao</asp:ListItem>
                                            <asp:ListItem Value="5">6 Sao</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlkieuthanhvien" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlkieuthanhvien_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="-1">= Tất cả Loại tài khoản =</asp:ListItem>
                                            <asp:ListItem Value="1">Thành viên</asp:ListItem>
                                            <asp:ListItem Value="2">Tất cả nhà cung cấp</asp:ListItem>
                                            <asp:ListItem Value="3">Chỉ có nhà cung cấp đã đăng sản phẩm</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlAgLand" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlAgLand_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="-1">= Tất cả AG Land =</asp:ListItem>
                                            <asp:ListItem Value="0">Chưa kích hoạt AgLand</asp:ListItem>
                                            <asp:ListItem Value="1">Đã kích hoạt AgLand</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddluutien" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddluutien_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="-1">= Tất cả Ưu tiên=</asp:ListItem>
                                            <asp:ListItem Value="0">Chưa kích hoạt Ưu tiên</asp:ListItem>
                                            <asp:ListItem Value="1">Đã kích hoạt Ưu tiên</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddlQRCode" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlQRCode_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="-1">= QRCode=</asp:ListItem>
                                            <asp:ListItem Value="0">Chưa kích hoạt QRCode</asp:ListItem>
                                            <asp:ListItem Value="1">Đã kích hoạt QRCode</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:DropDownList ID="ddltheoLead" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddltheoLead_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="-1">= Tất cả Lead =</asp:ListItem>
                                            <asp:ListItem Value="0">Thành viên</asp:ListItem>
                                            <asp:ListItem Value="1">Lead</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlorderby" runat="server" AutoPostBack="true" CssClass="txt"
                                            OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="dcreatedate">S.xếp:Ngày cập nhật</asp:ListItem>
                                            <asp:ListItem Value="iuser_id">S.xếp:Tăng dần</asp:ListItem>
                                            <asp:ListItem Value="vlname">S.xếp:Tên (ABC)</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlordertype" runat="server" AutoPostBack="True" CssClass="txt" OnSelectedIndexChanged="ddlordertype_SelectedIndexChanged">
                                            <asp:ListItem Value="desc">Giảm dần</asp:ListItem>
                                            <asp:ListItem Value="asc">Tăng dần</asp:ListItem>
                                        </asp:DropDownList>

                                        <asp:TextBox Style="width: 200px;" ID="txtNgayThangNam" placeholder="Tìm kiếm từ ngày/tháng/năm" AutoPostBack="true" OnTextChanged="txtNgayThangNam_TextChanged" runat="server" CssClass="txt_csssearch" Width="200px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" Format="dd/MM/yyyy" runat="server" TargetControlID="txtNgayThangNam"></cc1:CalendarExtender>
                                        <asp:TextBox Style="width: 200px;" ID="txtDenNgayThangNam" placeholder="Tìm kiếm đến ngày/tháng/năm" AutoPostBack="true" OnTextChanged="txtDenNgayThangNam_TextChanged" runat="server" CssClass="txt_csssearch" Width="200px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy" runat="server" TargetControlID="txtDenNgayThangNam"></cc1:CalendarExtender>
                                    </div>
                                </div>
                            </div>


                            <div class="Content">
                                <div class="ContactsList">
                                    <div>
                                        Tổng thành viên:
                                        <asp:Label ID="lttongtb" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                    </div>
                                    <asp:Literal ID="ltpage1" runat="server"></asp:Literal>
                                    <asp:Repeater ID="Repeater1" runat="server">
                                        <HeaderTemplate>
                                            <table class="table table-striped table-bordered" id="sample_1">
                                                <tr bgcolor="#f1f1f1" height="22">
                                                    <td>
                                                        <input id="chkAll" onclick="javascript: SelectAllCheckboxes(this, 1);" type="checkbox" /></td>
                                                    <td>
                                                        <b>Tên đăng nhập</b>
                                                    </td>
                                                    <td>
                                                        <b>Họ và tên</b>
                                                    </td>
                                                    <td>
                                                        <b>Phone</b>
                                                    </td>
                                                    <td>
                                                        <b>Email</b>
                                                    </td>
                                                </tr>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <tr class="odd gradeX">
                                                <td>
                                                    <asp:CheckBox ID="chkid" runat="server" onclick="javascript:changeColor(this,'white');" />
                                                    <asp:HiddenField ID="hiID" Value='<%# Eval("vuserun") %>' runat="server" />
                                                    <%-- <input type="checkbox" ID="chkid"  name="chk" value="<%#Eval("vuserun") %>" />--%><%-- /  <%#Eval("iuser_id") %>--%>
                                                </td>
                                                <td>
                                                    <a href="/admin.aspx?u=TBNotification&IDThanhVien=<%#Eval("iuser_id") %>" target="_blank"><%#Eval("vuserun") %></a>
                                                </td>
                                                <td>
                                                    <%#Eval("vfname") %>
                                                </td>
                                                <td>
                                                    <%#Eval("vemail") %>
                                                </td>
                                                <td>
                                                    <%#Eval("vphone") %>
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            </table>
                                        </FooterTemplate>
                                    </asp:Repeater>
                                    <br />
                                    <asp:Literal ID="ltpage" runat="server"></asp:Literal>
                                    <br />
                                </div>
                                <asp:Button ID="btnadd" runat="server" Text="Thêm vào danh sách" CssClass="btn btn-primary" OnClick="btnadd_Click" />&nbsp;
                                <asp:Button ID="btnhuy2" runat="server" Text="Hủy bỏ" OnClick="btnhuy2_Click" CssClass="btn btn-primary" />
                                <br />
                                <br />
                            </div>
                            <br />

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
