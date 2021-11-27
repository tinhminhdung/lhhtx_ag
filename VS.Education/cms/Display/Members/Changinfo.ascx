<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Changinfo.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Members.Changinfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<section class=" col-md-12 NPMP">
    <h1 class="title-head">
        <span>Hồ sơ thành viên</span>
    </h1>
    <div class="minimal-page-wrapper">
        <div class="edito-exergue">
            <span class="border-left"></span>
            <div class="wrap ">
                <div class="thongbao">
                    <asp:Label ID="ltmsg" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div class="frm_Thanhvien">

                    <div class="nenchokhoi">
                        <div class="gachke">
                            <div class="tenthanhvien Otherthanhvien">Loại tài khoản</div>
                            <div style="float: left;">
                                <asp:Literal ID="ltloaithanhvien" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="gachke" style=" display:none">
                            <div class="tenthanhvien Otherthanhvien">Kiểu thành viên</div>
                            <div style="float: left;">
                                <asp:Literal ID="kieuthanhvien" runat="server"></asp:Literal>
                            </div>
                        </div>

                        <div class="gachke">
                            <div class="tenthanhvien Otherthanhvien">Cấp bậc</div>
                            <div style="float: left">
                                <asp:Literal ID="ltcapdo" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="gachke">
                            <div class="tenthanhvien Otherthanhvien">Loại Chi nhánh</div>
                            <div style="float: left">
                                <asp:Literal ID="ltchinhanh" runat="server"></asp:Literal>
                            </div>
                        </div>
                        <div class="gachke">
                            <div class="tenthanhvien Otherthanhvien">Thuộc chi nhánh</div>
                            <div style="float: left">
                                <asp:Literal ID="ltthuocchinhanh" runat="server"></asp:Literal>
                            </div>
                        </div>
                         <div class="gachke nhapnhay" style=" display:none">
                            <div class="tenthanhvien Otherthanhvien">Kích hoạt</div>
                            <div style="float: left">
                                <asp:Literal ID="ltkickhoat" runat="server"></asp:Literal>
                                <asp:DropDownList ID="ddlvitien"  runat="server">
                                    <asp:ListItem Value="1">Ví Quản lý</asp:ListItem>
                                    <asp:ListItem Value="2">Ví Thương Mại</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Button ID="btkichhoat" Visible="false" Style="background: red; color: #fff; text-align: center; padding-left: 5px;" OnClick="btkichhoat_Click" runat="server" Text="Kích hoạt thành viên" />
                            </div>
                        </div>
                        <div class="gachke" style=" display:none">
                            <div class="tenthanhvien Otherthanhvien">Kích hoạt QRcode</div>
                            <div style="float: left">
                                <asp:Literal ID="ltQRcode" runat="server"></asp:Literal>
                                <asp:PlaceHolder ID="plQRCode" runat="server" />
                                <asp:Button ID="btqrcode" Visible="false" Style="background: red; color: #fff; text-align: center; padding-left: 5px;" OnClick="btqrcode_Click" runat="server" Text="Kích hoạt tính năng QRcode" />
                            </div>
                        </div>
                        <asp:Panel ID="Panel1" runat="server" Visible="false">
                            <div class="gachke">
                                <div class="tenthanhvien Otherthanhvien">% Chiết khấu QRcode</div>
                                <div style="float: left">
                                    <asp:TextBox ID="txtchietkhauQRcode" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="gachke" style="display: none">
                                <div class="tenthanhvien Otherthanhvien">% Hoa hồng cho người mua</div>
                                <div style="float: left">
                                    <asp:TextBox ID="txthoahongnguoimuaQRcode" runat="server">60</asp:TextBox>
                                </div>
                            </div>
                            <div class="gachke" style="display: none">
                                <div class="tenthanhvien Otherthanhvien">% Hoa hồng cho hệ thống</div>
                                <div style="float: left">
                                    <asp:TextBox ID="txthoahonghethongQRcode" runat="server">40</asp:TextBox>
                                </div>
                            </div>
                            <div class="gachke">
                                <div class="tenthanhvien Otherthanhvien">&nbsp;&nbsp;  </div>
                                <div style="float: left">
                                    <asp:Button ID="btluuhoahongQRCode" Style="background: red; color: #fff; text-align: center; padding-left: 5px;" OnClick="btluuhoahongQRCode_Click" runat="server" Text="Cập nhật hoa hồng" />
                                </div>
                            </div>
                            <br />

                        </asp:Panel>
                    </div>
                    <div class="Danhchothanhvien">Thông tin thành viên</div>
                    <div class="gachke">
                        <div class="tenthanhvien"><%=label("l_name")%> :</div>
                        <div class="iconnthanhvien">
                            <asp:TextBox CssClass='contact3' ID="txtname" Enabled="false" runat="server" Width="358px"></asp:TextBox>
                        </div>
                    </div>
                    <div class="gachke">
                        <div class="tenthanhvien"><%=label("lt_birthday") %></div>
                        <div class="iconngaysinh">
                            <asp:TextBox CssClass='contact3' ID="txtbirthday" runat="server" Width="358px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalExtfrm" PopupButtonID="txtbirthday" CssClass="black" runat="server" TargetControlID="txtbirthday" Format="yyyy-MM-dd"></cc1:CalendarExtender>
                        </div>
                    </div>
                    <div class="gachke">
                        <div class="tenthanhvien"><%=label("l_address") %> theo hộ khẩu</div>
                        <div class="DiaChi">
                            <asp:TextBox CssClass='contact3' ID="txtaddress" Enabled="false"  TextMode="MultiLine" Style="height: 70px" runat="server" Width="358px"></asp:TextBox>
                        </div>
                    </div>

                      <div class="gachke">
                        <div class="tenthanhvien">Tỉnh thành</div>
                        <div class="DiaChi">
                          <asp:DropDownList ID="ddltinhthanh" class="CSTextBox"  ValidationGroup="GInfo" style="border-radius: 0px !important;height: 35px;border: 1px solid #d7d7d7; width:80%" runat="server"></asp:DropDownList>
                        </div>
                           <asp:RequiredFieldValidator ID="RequiredFieldValidator13"  runat="server" ErrorMessage="*" Text="Vui lòng chọn tỉnh thành" InitialValue="0" ControlToValidate="ddltinhthanh" ValidationGroup="GInfo"></asp:RequiredFieldValidator> 
                    </div>


                    <div class="gachke">
                        <div class="tenthanhvien">Email</div>
                        <div class="iconnthanhvien">
                            <asp:TextBox CssClass='contact3' ID="txtemail" Enabled="false"  runat="server" Width="358px"></asp:TextBox>
                        </div>
                    </div>
                    <div class="gachke">
                        <div class="tenthanhvien"><%=label("l_phone")%></div>
                        <div class="Phone">
                            <asp:TextBox CssClass='contact3' ID="txtphone" Enabled="false"  ValidationGroup="GInfo" runat="server" Width="358px"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtphone" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtphone" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationExpression="\d*" ValidationGroup="GInfo"></asp:RegularExpressionValidator>
                    </div>
                    <div class="gachke">
                        <div class="tenthanhvien">Loại chứng minh</div>
                        <div class="Phone">
                            <asp:DropDownList ID="ddlloaichungminhthu" runat="server">
                                <asp:ListItem Value="1">Chứng minh thư</asp:ListItem>
                                <asp:ListItem Value="2">Thẻ căn cước công dân</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="gachke">
                        <div class="tenthanhvien">Số chứng minh thư</div>
                        <div class="Phone">
                            <asp:TextBox CssClass='contact3' ID="txtsochungminhthu" ValidationGroup="GInfo" runat="server" Width="358px"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsochungminhthu" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                    </div>
                    <div class="gachke">
                        <div class="tenthanhvien">Ngày cấp</div>
                        <div class="Phone">
                            <asp:TextBox CssClass='contact3' ID="txtngaycap" ValidationGroup="GInfo" runat="server" Width="358px"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtngaycap" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                    </div>
                    <div class="gachke">
                        <div class="tenthanhvien">Nơi cấp</div>
                        <div class="Phone">
                            <asp:TextBox CssClass='contact3' ID="txtnoicap" ValidationGroup="GInfo" runat="server" Width="358px"></asp:TextBox>
                        </div>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtnoicap" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                    </div>

                    <div class="gachke">
                        <div class="tenthanhvien">Ảnh chứng minh thư <span style="font-size: 12px; color: #ed1c24">(Mặt trước)</span></div>
                        <div style="float: left">
                            <asp:FileUpload ID="flchungminhthutruoc" runat="server" CssClass="contact3 anhdaidien" Height="20px" Width="250px" /><br />
                            <%--<div style="float:left;"><asp:RequiredFieldValidator style="float:left;" ID="RequiredFieldValidator7" runat="server" ControlToValidate="flchungminhthutruoc" Display="Dynamic" ErrorMessage="Vui lòng đăng ảnh chứng minh thư (Mặt trước)" SetFocusOnError="True" ValidationGroup="GInfo"></asp:RequiredFieldValidator></div>--%>
                            <div class="adaidien">
                                <asp:Literal ID="lanhchungminhthutruoc" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>

                    <div class="gachke">
                        <div class="tenthanhvien">Ảnh chứng minh thư <span style="font-size: 12px; color: #ed1c24">(Mặt sau)</span></div>
                        <div style="float: left">
                            <asp:FileUpload ID="flchungminhthusau" runat="server" CssClass="contact3 anhdaidien" Height="20px" Width="250px" /><br />
                            <%-- <div style="float:left;"><asp:RequiredFieldValidator style="float:left;" ID="RequiredFieldValidator4" runat="server" ControlToValidate="flchungminhthusau" Display="Dynamic" ErrorMessage="Vui lòng đăng ảnh chứng minh thư (Mặt sau)" SetFocusOnError="True" ValidationGroup="GInfo"></asp:RequiredFieldValidator></div>--%>
                            <div class="adaidien">
                                <asp:Literal ID="lanhchungminhthusau" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                    <%--<div class="Danhchothanhvien">Dành cho nhà cung cấp</div>--%>

                    <div class="gachke" style="display: none">
                        <div class="tenthanhvien">Người giới thiệu</div>
                        <div class="iconnthanhvien">
                            <asp:TextBox CssClass='contact3' ID="txttennguoigioithieu" placeholder="Tên đăng nhập của người giới thiệu" runat="server" Width="358px"></asp:TextBox>
                            <div class="thongb">
                                <asp:Label ID="ltmess" runat="server" ForeColor="Red"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <asp:Panel ID="Panel2" runat="server">
                        <div class="gachke">
                            <div class="tenthanhvien">Tên shop</div>
                            <div class="iconnthanhvien">
                                <asp:TextBox CssClass='contact3' ID="txttenshop" placeholder="Tên shop của bạn" runat="server" Width="358px"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txttenshop" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="GInfo"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="gachke">
                            <div class="tenthanhvien">Địa khỉ kho hàng</div>
                            <div class="iconnthanhvien">
                                <asp:TextBox CssClass='contact3' ID="txtdiachikhohang" placeholder="Địa chỉ kho hàng của bạn" TextMode="MultiLine" Style="height: 70px" runat="server" Width="358px"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtdiachikhohang" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="GInfo"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="gachke">
                            <div class="tenthanhvien">Mã số doanh nghiệp</div>
                            <div class="iconnthanhvien">
                                <asp:TextBox CssClass='contact3' ID="txtmasodoanhnghiep" placeholder="Nhập mã doanh nghiệp" runat="server" Width="358px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="gachke">
                            <div class="tenthanhvien">Ảnh giấy phép kinh doanh</div>
                            <div style="float: left">
                                <asp:FileUpload ID="flileGiayDangKyKinhDoanh" runat="server" CssClass="contact3 anhdaidien" Height="20px" Width="250px" /><br />
                                <div class="adaidien">
                                    <asp:Literal ID="ltimggiayphep" runat="server"></asp:Literal>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>

                    <div class="gachke">
                        <div class="tenthanhvien">Ảnh đại diện</div>
                        <div style="float: left">
                            <asp:FileUpload ID="flavatar" runat="server" CssClass="contact3 anhdaidien" Height="20px" Width="250px" /><br />
                            <div class="adaidien">
                                <asp:Literal ID="ltimg" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>


                <div class="nutguicd">
                    <asp:Button ID="btnsave" ValidationGroup="GInfo" runat="server" CssClass="btnadd" Text="Lưu thông tin" OnClick="btnsave_Click" />
                </div>
                <asp:HiddenField ID="hdid" runat="server" />
                <asp:HiddenField ID="hdimg" runat="server" />
                <asp:HiddenField ID="hdgiayphep" runat="server" />
                <asp:HiddenField ID="hdchungminhthumattruoc" runat="server" />
                <asp:HiddenField ID="hdchungminhthumatsau" runat="server" />
                <asp:HiddenField ID="hdChiNhanh" runat="server" Value="0" />

            </div>
        </div>
    </div>
</section>


<asp:Literal ID="ltjavascript" runat="server"></asp:Literal>