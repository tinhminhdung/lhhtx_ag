<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThanhToanQRCode.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.ThanhToanQRCode" %>

<div class="container contact">
    <div class="row">
        <div class="col-lg-12">
            <div class="page-login page_contact">
                <div id="login">
                    <h1 class="title-head contact-title margin-bottom-30">

                        <br />
                        <span>Chuyển điểm QRCode: </span>
                    </h1>
                    <div class="group_contact">

                        <asp:Label ID="ltmess" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
                        <div style="text-align: left; color: red; font-weight: bold; font-size: 16px; text-transform: uppercase">Người nhận điểm</div>
                        <div style="clear: both; height: 20px;"></div>
                        <asp:HiddenField ID="hdIDThanhVien" runat="server" />

                        <asp:HiddenField ID="hdvalue" Value="0" runat="server" />
                        <asp:HiddenField ID="hdHoaHongGioiThieuF" Value="0" runat="server" />


                        <div class="booo">
                            <div style="padding: 10px;">
                                <div class="tongcc cccao">
                                    <div class="thontintiede ">Họ và tên:</div>
                                    <div class="noidungcc" style="color: red">
                                        <asp:Literal ID="lthovaten" runat="server"></asp:Literal>
                                    </div>
                                </div>
                                <div class="tongcc cccao">
                                    <div class="thontintiede">Email:</div>
                                    <div class="noidungcc" style="color: red">
                                        <asp:Literal ID="ltemail" runat="server"></asp:Literal>
                                    </div>
                                </div>

                                <div class="tongcc cccao">
                                    <div class="thontintiede">Số điện thoại:</div>
                                    <div class="noidungcc" style="color: red">
                                        <asp:Literal ID="ltsodienthoai" runat="server"></asp:Literal>
                                    </div>
                                </div>

                            </div>

                        </div>
                        <div style="clear: both; height: 20px;"></div>
                        <div style="text-align: left; color: red; font-weight: bold; font-size: 16px; text-transform: uppercase">Người chuyển điểm</div>
                        <div style="clear: both; height: 20px;"></div>
                        <div class="booo">
                            <div style="padding: 10px;">

                                <asp:Panel ID="Panel1" Visible="false" runat="server">
                                <div class="tongcc cccao">
                                    <div class="thontintiede">Tên Thành viên:</div>
                                   <div class="noidungcc" style="color: red">
                                        <asp:Literal ID="lttenthanhvien" runat="server"></asp:Literal>
                                    </div>
                                </div>

                                  <div class="tongcc cccao">
                                    <div class="thontintiede">Số điểm còn lại:</div>
                                     <div class="noidungcc" style="color: red">
                                        <asp:Literal ID="sodiemconlai" runat="server"></asp:Literal> / điểm
                                    </div>
                                </div>
                                  
                                </asp:Panel>


                                <div class="tongcc">
                                    <div class="thontintiede">Tên đăng nhập:</div>
                                    <div class="noidungcc">
                                        <asp:TextBox ID="txttendangnhap" ValidationGroup="GInfo" CssClass="ipuu" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true" runat="server" ValidationGroup="GInfo" ControlToValidate="txttendangnhap" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="tongcc">
                                    <div class="thontintiede">Mật khẩu:</div>
                                    <div class="noidungcc">
                                        <asp:TextBox ID="txtmatkhau" ValidationGroup="GInfo" CssClass="ipuu" TextMode="Password" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true" runat="server" ValidationGroup="GInfo" ControlToValidate="txtmatkhau" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="tongcc">
                                    <div class="thontintiede">Số điểm thanh toán:</div>
                                    <div class="noidungcc">
                                        <asp:TextBox ID="txtsotiencanthanhtoan" CssClass="ipuu" ValidationGroup="GInfo" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true" runat="server" ValidationGroup="GInfo" ControlToValidate="txtsotiencanthanhtoan" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                 <div class="tongcc">
                                    <div class="thontintiede">Nội dung thanh toán:</div>
                                    <div class="noidungcc">
                                        <asp:TextBox ID="txtnoidungthanhtoan" placeholder="Vui lòng ghi thông tin sản phẩm cần thanh toán." CssClass="ipuu" style=" border:1px solid #d7d7d7" ValidationGroup="GInfo" TextMode="MultiLine" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true" runat="server" ValidationGroup="GInfo" ControlToValidate="txtnoidungthanhtoan" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </div>
                                </div>

                                <div class="tongcc">
                                    <div class="thontintiede">&nbsp;&nbsp;&nbsp; </div>
                                    <div class="noidungcc">
                                        <asp:Button ID="btthanhtoan" ValidationGroup="GInfo" OnClick="btthanhtoan_Click" CssClass="btnadd" runat="server" Text="Thanh toán" />
                                    </div>
                                </div>

                            </div>
                        </div>


                    </div>

                </div>
            </div>
        </div>
    </div>
</div>


<br />
<br />
<br />
<br />
