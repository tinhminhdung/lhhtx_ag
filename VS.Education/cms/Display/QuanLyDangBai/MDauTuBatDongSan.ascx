<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MDauTuBatDongSan.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.MDauTuBatDongSan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="nentrang" style="float: left;">
    <div class="page-title m992">
        <h1 class="title-head margin-top-0"><a href="#">Đầu tư bất động sản</a></h1>
    </div>
    <div class='frm-contact'>
        <div><%=Commond.Setting("NDDauTuBDS") %></div>
        <div style="padding: 10px 10px 10px 10px">
            <asp:Label ID="ltmsg" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
        </div>
        <div style="width: 100%">
            <div class="tbinput">
                <div class="labelll">
                    Tổng số tiền đầu tư
                </div>
                <div>
                    <asp:TextBox ID="txtsotiencanrut" placeholder="Nhập số tiền cần đầu tư" class="Rutien" ValidationGroup="Rutien" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator7" runat="server" ValidationGroup="Rutien" ControlToValidate="txtsotiencanrut" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtsotiencanrut"></cc1:FilteredTextBoxExtender>
                </div>
            </div>
            <div class="tbinput">
                <div class="labelll">
                    Họ và tên:
                </div>
                <div>
                    <asp:TextBox ID="txthovaten" class="Rutien" ValidationGroup="Rutien" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator4" runat="server" ValidationGroup="Rutien" ControlToValidate="txthovaten" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="tbinput">
                <div class="labelll">
                    Địa chỉ:
                </div>
                <div>
                    <asp:TextBox ID="DiaChi" class="Rutien" ValidationGroup="Rutien" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator2" runat="server" ValidationGroup="Rutien" ControlToValidate="DiaChi" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="tbinput">
                <div class="labelll">
                    Điện thoại
                </div>
                <div>
                    <asp:TextBox ID="DienThoai" class="Rutien" ValidationGroup="Rutien" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator6" runat="server" ValidationGroup="Rutien" ControlToValidate="DienThoai" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="tbinput">
                <div class="labelll">
                    CMND / CCCD
                </div>
                <div>
                    <asp:TextBox ID="CMND" class="Rutien" ValidationGroup="Rutien" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator8" runat="server" ValidationGroup="Rutien" ControlToValidate="CMND" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="tbinput">
                <div class="labelll">
                    Tên ngân hàng:
                </div>
                <div>
                    <asp:TextBox ID="txttennganhang" class="Rutien" ValidationGroup="Rutien" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator3" runat="server" ValidationGroup="Rutien" ControlToValidate="txttennganhang" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="tbinput">
                <div class="labelll">
                    Số tài khoản:
                </div>
                <div>
                    <asp:TextBox ID="txtsotaikhoan" class="Rutien" ValidationGroup="Rutien" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator1" runat="server" ValidationGroup="Rutien" ControlToValidate="txtsotaikhoan" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
            </div>
            <div class="tbinput">
                <div class="labelll">
                    Chi nhánh:
                </div>
                <div>
                    <asp:TextBox ID="txtchinhanh" class="Rutien" ValidationGroup="Rutien" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="tbinput">
                <div class="labelll">
                    Ảnh giao dịch chuyển tiền
                </div>
                <div>
                    <asp:FileUpload ID="flAnh" runat="server" CssClass="contact3 anhdaidien" ValidationGroup="Rutien" Height="24px" Width="250px" /><br />
                </div>
                <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator5" runat="server" ValidationGroup="Rutien" ControlToValidate="flAnh" ErrorMessage="*"></asp:RequiredFieldValidator>
            </div>

            <div class="tbinput">
                <div class="labelll">
                    Ghi chú:
                </div>
                <div>
                    <asp:TextBox ID="txtghichu" TextMode="MultiLine" Style="height: 70px" runat="server"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 a-left">
        <br />
        <div style="">
            <asp:Label ID="lthongbao" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
        </div>
        <asp:Button ID="btrutien" runat="server" ValidationGroup="Rutien" Text="Đầu tư" CssClass="btnadd" OnClick="btrutien_Click" />
        <asp:Button ID="bthuy" runat="server" Text="Hủy" CssClass="btnadd" OnClick="bthuy_Click" />
    </div>
</div>
<asp:HiddenField ID="hdid" runat="server" />
<asp:HiddenField ID="hdchungminhthumattruoc" runat="server" />
<%--<script>
    $("#<%=txtsotiencanrut.ClientID%>").on('keyup', function () {
        var n = parseInt($(this).val().replace(/\D/g, ''), 10);
        $(this).val(n.toLocaleString());
    });
    $("#<%=txtsotiencanrut.ClientID%>").load('keyup', function () {
        var n = parseInt($(this).val().replace(/\D/g, ''), 10);
        if (n.toLocaleString() != 'NaN') {
            $(this).val(n.toLocaleString());
        }
    });
</script>--%>
