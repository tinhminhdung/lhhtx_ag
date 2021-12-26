<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="contact.ascx.cs" Inherits="VS.E_Commerce.cms.Display.contact.contact" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/cms/Display/Nav_conten.ascx" TagPrefix="uc1" TagName="Nav_conten" %>

<uc1:Nav_conten runat="server" ID="Nav_conten" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="container contact chitiet">
            <div class="row">
                <div class="col-lg-6">
                    <div class="page-login page_contact">
                        <div id="login">
                            <h1 class="title-head contact-title margin-bottom-30">
                                <span>Liên hệ với chúng tôi: </span>
                            </h1>
                            <div class="group_contact">
                                <asp:Label ID="ltmsg" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>

                                <fieldset class="form-group col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <asp:TextBox ID="txtfullname" runat="server" placeholder="Họ và tên" ValidationGroup="GInfo" class="form-control  form-control-lg"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="GInfo" ControlToValidate="txtfullname" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </fieldset>
                                <fieldset class="form-group col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <asp:TextBox ID="txtemail" runat="server" placeholder="Email của bạn" ValidationGroup="GInfo" class="form-control  form-control-lg"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtemail" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RequiredFieldValidator4" ValidationGroup="GInfo" runat="server" ControlToValidate="txtemail" ErrorMessage="Vui lòng nhập email hợp lệ." ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" meta:resourcekey="valRegExResource1"></asp:RegularExpressionValidator>
                                </fieldset>
                                <fieldset class="form-group col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <asp:TextBox ID="txtphone" runat="server" placeholder="Điện thoại" ValidationGroup="GInfo" class="form-control  form-control-lg"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtphone" Display="Dynamic" ErrorMessage="*" SetFocusOnError="True" ValidationGroup="GInfo"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtphone" Display="Dynamic" ErrorMessage="Số điện thoại phải là số !" SetFocusOnError="True" ValidationExpression="\d*" ValidationGroup="GInfo"></asp:RegularExpressionValidator>
                                </fieldset>
                                <fieldset class="form-group col-lg-6 col-md-6 col-sm-6 col-xs-12">
                                    <asp:TextBox ID="txtaddress" runat="server" placeholder="Địa chỉ" ValidationGroup="GInfo" class="form-control  form-control-lg"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ValidationGroup="GInfo" ControlToValidate="txtaddress" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </fieldset>
                                <fieldset class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <asp:TextBox ID="txttieude" runat="server" placeholder="Tiêu đề" ValidationGroup="GInfo" class="form-control  form-control-lg"></asp:TextBox>
                                </fieldset>
                                <fieldset class="form-group col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <asp:TextBox ID="txtcontent" runat="server" Height="111px" placeholder="Viết liên hệ của bạn" TextMode="MultiLine" ValidationGroup="GInfo" class="form-control  form-control-lg"></asp:TextBox>
                                </fieldset>
                                <div style=" clear:both"></div>
                                <div class="">
                                    <asp:Button ID="btgui" runat="server" OnClick="btgui_Click" ValidationGroup="GInfo" Text="Gửi liên hệ" CssClass="btn btn-hai btn-primary" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-6">
                    <div class="widget-item info-contact">
                        <h2 class="title-head contact-title margin-bottom-30">
                                <span>Về Chúng Tôi: </span>
                            </h2>
                        <ul class="widget-menu">
                            <asp:Literal ID="ltcontactcontent" runat="server"></asp:Literal>
                        </ul>
                    </div>
                </div>
            </div>
        </div>

        <div class="box-maps">
            <br />
            <div class="iFrameMap">
                <div class="google-map">
                    <div class="maps_iframe">
                        <%=Commond.Setting("txtbando")%>
                    </div>
                </div>
            </div>
        </div>





    </ContentTemplate>
</asp:UpdatePanel>

