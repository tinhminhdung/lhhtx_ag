<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RutTien.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.RutTien" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
            <div class="nentrang" style="float: left;">
                <div class="page-title m992">
                    <h1 class="title-head margin-top-0"><a href="#">Rút điểm hoa hồng</a></h1>
                </div>
                <div class='frm-contact'>
                    <div style="padding: 10px 10px 10px 10px">
                        <asp:Label ID="ltmsg" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
                    </div>
                    <div style="width: 100%">
                        <div class="tbinput">
                            <div style=" font-size:20px; color:red; text-transform:uppercase">
                               Tổng điểm trong ví: <asp:Literal ID="lttongtien" runat="server"></asp:Literal> điểm
                            </div>
                        </div>
                        <div class="tbinput">
                            <div class="labelll">
                                Số điểm cần rút:
                            </div>
                            <div>
                                <asp:TextBox ID="txtsotiencanrut" placeholder="Nhập số điểm cần rút" class="Rutien" ValidationGroup="Rutien" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidator2" runat="server" ValidationGroup="Rutien" ControlToValidate="txtsotiencanrut" ErrorMessage="*"></asp:RequiredFieldValidator>
                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtsotiencanrut"></cc1:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="tbinput">
                            <div class="labelll">
                                Tên ngân hàng:
                            </div>
                            <div>
                                <asp:TextBox ID="txttennganhang"  class="Rutien" ValidationGroup="Rutien" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidator3" runat="server" ValidationGroup="Rutien" ControlToValidate="txttennganhang" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="tbinput">
                            <div class="labelll">
                                Họ và tên:
                            </div>
                            <div>
                                <asp:TextBox ID="txthovaten"  class="Rutien" ValidationGroup="Rutien" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidator4" runat="server" ValidationGroup="Rutien" ControlToValidate="txthovaten" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="tbinput">
                            <div class="labelll">
                                Số tài khoản:
                            </div>
                            <div>
                                <asp:TextBox ID="txtsotaikhoan"  class="Rutien" ValidationGroup="Rutien" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidator1" runat="server" ValidationGroup="Rutien" ControlToValidate="txtsotaikhoan" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                          <div class="tbinput">
                            <div class="labelll">
                               Chi nhánh:
                            </div>
                            <div>
                                <asp:TextBox ID="txtchinhanh"  class="Rutien" ValidationGroup="Rutien" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="tbinput">
                            <div class="labelll">
                                Nội dung chuyển tiền:
                            </div>
                            <div>
                                <asp:TextBox ID="txtnoidungchuyentien" ValidationGroup="Rutien" TextMode="MultiLine" Style="height: 50px" runat="server"></asp:TextBox>
                            </div>
                            <asp:RequiredFieldValidator SetFocusOnError="true"  ID="RequiredFieldValidator5" runat="server" ValidationGroup="Rutien" ControlToValidate="txtnoidungchuyentien" ErrorMessage="*"></asp:RequiredFieldValidator>
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
                    <asp:Button ID="btrutien" runat="server" ValidationGroup="Rutien" Text="Rút điểm" CssClass="btnadd" OnClick="btrutien_Click" />
                     <asp:Button ID="bthuy" runat="server" Text="Hủy" CssClass="btnadd" OnClick="bthuy_Click" />
                </div>
            </div>
<asp:HiddenField ID="hdid" runat="server" />

<asp:HiddenField ID="hdmobile" runat="server" />

<asp:HiddenField ID="hdemail" runat="server" />

<asp:HiddenField ID="hdaddress" runat="server" />
