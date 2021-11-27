<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MChuyenDiem.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.MChuyenDiem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<section class=" col-md-12 NPMP">
    <h1 class="title-head">
        <span>Chuyển điểm cho thành viên</span>
    </h1>
    <div class="minimal-page-wrapper">
        <div class="edito-exergue">
            <span class="border-left"></span>
            <div class="wrap ">

                <div class="frm_Thanhvien">
                <%--    <div class="Danhchothanhvien">Thông tin người cấp điểm </div>--%>
                    <div class="nenchokhoi" style=" display:none">
                        <div class="gachke">
                            <div class="tenthanhvien Otherthanhvien">Họ và tên:</div>
                            <div class="sodiemhientai ">
                                <asp:Literal ID="lthovaten" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="gachke">
                            <div class="tenthanhvien Otherthanhvien">Email:</div>
                            <div class="sodiemhientai ">
                                <asp:Literal ID="ltemail" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="gachke">
                            <div class="tenthanhvien Otherthanhvien">Điện thoại:</div>
                            <div class="sodiemhientai ">
                                <asp:Literal ID="ltdienthoai" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="gachke">
                            <div class="tenthanhvien Otherthanhvien">Ví điểm thương mại:</div>
                            <div class="sodiemhientai ">
                                <asp:Literal ID="ltsodiemhientai" runat="server"></asp:Literal>
                                / điểm
                            </div>
                        </div>
                    </div>
                    <div class="Danhchothanhvien" style=" display:none">Dành cho thành viên được nhận điểm</div>
                    <div style="clear: both"></div>
                    <asp:Label ID="ltmsg" runat="server" ForeColor="White"></asp:Label>
                    <div style="clear: both"></div>
                    <div class="thongbao">
                        <div id="Showketqua" class="btsent" style="color: #ed1c24; padding-bottom: 0px;"></div>

                    </div>
                    <div class="nenchokhoi" style=" background:none">
                        <div class="gachke">
                            <div class="tenthanhvien">Ví Chuyển điểm:</div>
                            <div class="">
                                <asp:DropDownList ID="ddlvicanchuyen" ValidationGroup="GInfo" runat="server">
                                    <%--<asp:ListItem Value="0">= Chọn ví cần chuyển =</asp:ListItem>--%>
                                    <asp:ListItem Value="1" Selected="True">Ví thương mại</asp:ListItem>
                                    <asp:ListItem Value="2">Ví điểm quản lý</asp:ListItem>
                                    <%--<asp:ListItem Value="3">Ví điểm mua hàng</asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="*" Text="*" InitialValue="0" ControlToValidate="ddlvicanchuyen" ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="gachke">
                            <div class="tenthanhvien">Người Nhận:</div>
                            <div class="iconnthanhvien">
                                <asp:TextBox CssClass='contact3' ID="txtnguoinhan" ValidationGroup="GInfo" placeholder="Tên đăng nhập của người nhận" runat="server" Width="358px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="GInfo" ControlToValidate="txtnguoinhan" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="gachke">
                            <div class="tenthanhvien">Ví nhận điểm:</div>
                            <div class="">
                                <asp:DropDownList ID="ddlViNhanDiem" ValidationGroup="GInfo" runat="server">
                                    <%--<asp:ListItem Value="0">= Chọn ví nhận chuyển =</asp:ListItem>--%>
                                    <asp:ListItem Value="1" Selected="True">Ví thương mại</asp:ListItem>
                                    <asp:ListItem Value="2">Ví điểm quản lý</asp:ListItem>
                                    <%--<asp:ListItem Value="3">Ví điểm mua hàng</asp:ListItem>--%>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" Text="*" InitialValue="0" ControlToValidate="ddlViNhanDiem" ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                            </div>
                        </div>

                        <div class="gachke">
                            <div class="tenthanhvien">Số điểm</div>
                            <div class="iconsoCoin">
                                <asp:TextBox ID="txtsocoin" placeholder="Số điểm cần chuyển" ValidationGroup="GInfo" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="GInfo" ControlToValidate="txtsocoin" ErrorMessage="*"></asp:RequiredFieldValidator>
                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtsocoin"></cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="gachke">
                            <div class="tenthanhvien">Mật khẩu xác nhận</div>
                            <div class="Password">
                                <asp:TextBox CssClass='contact3' ID="txtmatkhau" TextMode="Password" ValidationGroup="GInfo" runat="server" Width="358px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="GInfo" ControlToValidate="txtmatkhau" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="gachke">
                            <div class="tenthanhvien">Mã bảo vệ</div>
                            <div class="Password">
                                <asp:TextBox ID="txtcapcha" TabIndex="11" runat="server" ValidationGroup="GInfo" class="form-control form-control-lg csscapcha"></asp:TextBox>
                                <div class="capchass">
                                    <asp:Literal ID="ltshowcapcha" runat="server"></asp:Literal>
                                </div>
                                <br />
                                <asp:Literal ID="lthongbao" runat="server"></asp:Literal>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtcapcha" Display="Dynamic" ErrorMessage="Mã bảo vệ không được để trống !" SetFocusOnError="True" ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="nutguicd">
                            <div style="color: red; font-weight: bold">Lưu ý: không được kích đúp chuột (CHUYỂN ĐIỂM) nhiều lần cùng 1 lúc.</div>
                            <asp:Button ID="btnsave" Style="padding-left: 5px; color: #ffffff" ValidationGroup="GInfo" runat="server" CssClass="btnadd" Text="Chuyển điểm" OnClick="btnsave_Click" />
                            <%--<% if (Commond.Setting("HH").Equals("1")) {%>
                                <asp:Button ID="btnsave" Style="padding-left: 5px; color: #ffffff" ValidationGroup="GInfo" runat="server" CssClass="btnadd" Text="Chuyển điểm" OnClick="btnsave_Click" />
                                <%}else{ %>
                                <asp:Button ID="Button1" CssClass="btnadd" Style="padding-left: 5px; color: #ffffff" OnClientClick="return confirm('Tính năng chuyển điểm này tạm thời dừng , Quý khách vui lòng quay lại chuyển điểm sau. Chân trọng cảm ơn.')"  runat="server" Text="Chuyển điểm" />
                                <%} %>--%>
                        </div>
                    </div>
                    <br />
                    <br />
                    <div style="clear: both; margin-bottom: 20px;"></div>
                </div>
                <asp:HiddenField ID="hdid" runat="server" />
            </div>
        </div>
    </div>
</section>
<%--<style>
    .Password div {
        background: none !important;
    }
</style>--%>
<script type="text/javascript">
    function DisableButton() {
        document.getElementById("<%=btnsave.ClientID %>").disabled = true;
        alert('aa');
    }
    window.onbeforeunload = DisableButton;
</script>

