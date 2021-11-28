<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Register.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Members.Register" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>

<div class="container margin-bottom-20">
    <h1 class="title-head">
        <a href="#">Đăng ký tài khoản</a>
    </h1>
    <div class="row">
        <div class="col-lg-12 ">

            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
                    <div class="page-login">
                        <div id="login">
                            <span>Nếu chưa có tài khoản vui lòng đăng ký tại đây</span>
                            <div class="form-signup">
                                <div>
                                    <asp:Label ID="ltmsg" runat="server" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div class="form-signup clearfix">

                                <div class="row">
                                    <div class="col-md-6">
                                       <%-- <fieldset class="form-group">
                                            <label>
                                                Tên đăng nhập<span class="required">*</span>
                                            </label>
                                            <asp:TextBox ID="txtusername" TabIndex="1" runat="server" class="form-control form-control-lg" ValidationGroup="GInfo"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server" ValidationGroup="GInfo" ControlToValidate="txtusername" ErrorMessage=" Tên đăng nhập không được để trống !"></asp:RequiredFieldValidator>
                                        </fieldset>--%>
                                        
                                        <fieldset class="form-group">
                                            <label>
                                                Họ và tên <span class="required">*</span>
                                            </label>
                                            <asp:TextBox ID="txtlastname" TabIndex="4" runat="server" ValidationGroup="GInfo" class="form-control form-control-lg"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server" ValidationGroup="GInfo" ControlToValidate="txtlastname" ErrorMessage="Họ và tên không được để trống !"></asp:RequiredFieldValidator>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <label>
                                                Điện thoại <span class="required">*</span>
                                            </label>
                                            <asp:TextBox ID="txt_phone" TabIndex="5" runat="server" class="form-control form-control-lg" ValidationGroup="GInfo" MaxLength="11"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txt_phone" Display="Dynamic" ErrorMessage="Số di động không được để trống !" SetFocusOnError="True" ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txt_phone" Display="Dynamic" ErrorMessage="Số điện thoại phải là số !" SetFocusOnError="True" ValidationExpression="\d*" ValidationGroup="GInfo"></asp:RegularExpressionValidator>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <label>
                                                Mật khẩu <span class="required">*</span>
                                            </label>
                                            <asp:TextBox ID="txtpassword" TabIndex="2" runat="server" TextMode="Password" ValidationGroup="GInfo" class="form-control form-control-lg"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server" ValidationGroup="GInfo" ControlToValidate="txtpassword" ErrorMessage="Mật khẩu không được để trống !"></asp:RequiredFieldValidator>
                                            <div style="font-size: 8pt; color: #ed1c24"><em>(Mật khẩu bao gồm 8 ký tự : Từ a đến z và số từ 0 đến 9)</em></div>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <label>
                                                Xác nhận Mật khẩu <span class="required">*</span>
                                            </label>
                                            <asp:TextBox ID="txtxacnhanmatkhau" TabIndex="3" runat="server" TextMode="Password" ValidationGroup="GInfo" class="form-control form-control-lg"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" SetFocusOnError="true" runat="server" ValidationGroup="GInfo" ControlToValidate="txtxacnhanmatkhau" ErrorMessage="Xác nhận mật khẩu không được để trống !"></asp:RequiredFieldValidator>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <label>
                                                Email <span class="required">*</span>
                                            </label>
                                            <asp:TextBox ID="txtemail" TabIndex="6" runat="server" ValidationGroup="GInfo" class="form-control form-control-lg"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtemail" Display="Dynamic" ErrorMessage="Email không được để trống !" SetFocusOnError="True" ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RequiredFieldValidator4" ValidationGroup="GInfo" runat="server" ControlToValidate="txtemail" ErrorMessage="Vui lòng nhập một địa chỉ email hợp lệ." ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="valRegExResource1"></asp:RegularExpressionValidator>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <label>
                                                Tỉnh thành <span class="required">*</span>
                                            </label>
                                            <asp:DropDownList ID="ddltinhthanh" class="CSTextBox" ValidationGroup="GInfo" Style="border-radius: 0px !important; height: 35px; border: 1px solid #d7d7d7; width: 100%" runat="server"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ErrorMessage="*" Text="Vui lòng chọn tỉnh thành" InitialValue="0" ControlToValidate="ddltinhthanh" ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                                        </fieldset>
                                    </div>
                                    <div class="col-md-6">

                                        <fieldset class="form-group">
                                            <label>
                                                Địa chỉ theo hộ khẩu<span class="required">*</span>
                                            </label>
                                            <asp:TextBox ID="txt_add" TabIndex="7" runat="server" class="form-control form-control-lg" ValidationGroup="GInfo"></asp:TextBox>
                                            <span style="font-size: 7pt; color: #ff7f50; font-family: Tahoma">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true" runat="server" ValidationGroup="GInfo" ControlToValidate="txt_add" ErrorMessage="Địa chỉ không được để trống !"></asp:RequiredFieldValidator>
                                        </fieldset>
                                        <fieldset class="form-group" style=" display:none">
                                            <label>
                                                Loại tài khoản
                                            </label>
                                            <asp:DropDownList ID="ddlloaitaikhoan" TabIndex="8" class="form-control form-control-lg" runat="server">
                                                <asp:ListItem Value="1">Thành viên</asp:ListItem>
                                                <asp:ListItem Value="2">Nhà cung cấp</asp:ListItem>
                                            </asp:DropDownList>
                                            <span style="font-size: 7pt; color: #ff7f50; font-family: Tahoma">*</span>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <label>
                                                Người giới thiệu
                                            </label>
                                            <asp:TextBox ID="txtnguoigioithieu" TabIndex="9" placeholder="Số điện thoại của người giới thiệu" runat="server" ValidationGroup="GInfo" class="form-control form-control-lg"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" SetFocusOnError="true" runat="server" ValidationGroup="GInfo" ControlToValidate="txtnguoigioithieu" ErrorMessage="Vui lòng điền Số điện thoại giới thiệu."></asp:RequiredFieldValidator>
                                            <div style="color: red; font-weight: bold">
                                                <asp:Literal ID="ltgoithieu" runat="server"></asp:Literal>
                                            </div>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <label>
                                                Chi nhánh <span class="required">*</span>
                                            </label>
                                            <asp:DropDownList ID="ddlchinhanh" TabIndex="10" class="form-control form-control-lg" ValidationGroup="GInfo" runat="server"></asp:DropDownList>
                                            <span style="font-size: 7pt; color: #ff7f50; font-family: Tahoma">*</span>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" SetFocusOnError="true" runat="server" ErrorMessage="Vui lòng chọn chi nhánh" InitialValue="0" ControlToValidate="ddlchinhanh" ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                                        </fieldset>
                                        <fieldset class="form-group">
                                            <label>Mã bảo vệ <span class="required">*</span> </label>
                                            <asp:TextBox ID="txtcapcha" TabIndex="11" runat="server" ValidationGroup="GInfo" class="form-control form-control-lg csscapcha"></asp:TextBox>
                                            <div class="capchass">
                                                <asp:Literal ID="ltshowcapcha" runat="server"></asp:Literal></div>
                                            <br />
                                            <asp:Literal ID="lthongbao" runat="server"></asp:Literal>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtcapcha" Display="Dynamic" ErrorMessage="Mã bảo vệ không được để trống !" SetFocusOnError="True" ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                                        </fieldset>
                                        <fieldset class="form-group checkchapnhan">
                                            <label for="opt-in11">
                                                <input type="checkbox" id="myCheck" tabindex="12" onclick="myFunction()">
                                                Tôi đã điền đơn đăng ký hội viên này hoàn toàn tự nguyện. Theo sự hiểu biết tốt nhất của tôi, thông tin trong Đơn này là sự thật, trọn vẹn, đúng và được hoàn tất trong thiện chí. Tôi hiểu rằng Liên hiệp hợp tác xã Việt Nam (VCU) có quyền kiểm tra tất cả mọi thông tin trong Đơn này, chấp nhận hoặc không chấp nhận Đơn của tôi.
                                            </label>
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<div class="dieukhoandk"><a tabindex="16" href="/dieu-khoan-dang-ky.html">Điều khoản đăng ký</a></div>
                                        </fieldset>
                                    </div>
                                </div>

                                <div class="col-xs-12 text-xs-left" style="padding: 0">
                                    <% if (Commond.Setting("HH").Equals("1"))
                                       {%>
                                    <asp:Button ID="btnregister" TabIndex="13" ValidationGroup="GInfo" runat="server" Text="Đăng ký" OnClick="Button1_Click" class="btn  btn-style btn-primary disabledbutton" />
                                    <%}
                                       else
                                       { %>
                                    <asp:Button ID="Button1" TabIndex="13" ValidationGroup="GInfo" OnClientClick="return confirm('Tính năng đăng ký này tạm thời dừng , Quý khách vui lòng quay lại đăng ký sau. Chân trọng cảm ơn.')" runat="server" Text="Đăng ký" class="btn  btn-style btn-primary " />
                                    <%} %>
                                    <a href="/dang-nhap.html" tabindex="14" class="btn-link-style btn-register" style="margin-left: 20px; text-decoration: underline;">Đăng nhập</a>
                                   <%-- <div class="block social-login--facebooks">
                                        <p class="a-center">
                                            Hoặc đăng nhập bằng
                                        </p>
                                        <a href="javascript:void(0)" tabindex="15" class="social-login--facebook" onclick="loginFacebook()">
                                            <img width="129px" height="37px" alt="facebook-login-button" src="/Resources/images/fb-btn.svg">
                                        </a>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href="javascript:void(0)" tabindex="16" class="social-login--google" onclick="loginGoogle()"><img width="129px" height="37px" alt="google-login-button" src="/Resources/images/gp-btn.svg">
                                        </a>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </div>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div class="dangkythanhcong">Bạn đã đăng ký thành công. Vui lòng đăng nhập để sử dụng những tính năng mà hệ thống đang có.</div>
                </asp:View>
            </asp:MultiView>


        </div>
    </div>
</div>
<%--    </ContentTemplate>
</asp:UpdatePanel>--%>
<style>
    .disabledbutton {
        pointer-events: none;
        opacity: 0.6;
    }
</style>

<script type="text/javascript">
    function myFunction() {
        var checkBox = document.getElementById("myCheck");
        var text = document.getElementById("Control_ctl00_ctl00_btnregister");
        if (checkBox.checked == true) {
            $(text).removeClass('disabledbutton');

        } else {
            $(text).addClass('disabledbutton');
        }
    }
    $(document).ready(function () {
        $('input[type="submit"]').click(function () {
            $('#loadingAjax').addClass('loading');
            setTimeout(function () {
                $('#loadingAjax').removeClass('loading');
            }, 4000);
        });
    });
</script>

<div id="loadingAjax" style=" display:none">
<div class="inner"><img src="/Resources/ShopCart/images/ajax-loader_2.gif"><p>Đang xử lý ...</p></div>
</div>