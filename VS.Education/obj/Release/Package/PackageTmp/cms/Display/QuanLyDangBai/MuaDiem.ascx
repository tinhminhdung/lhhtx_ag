<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MuaDiem.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.MuaDiem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="nentrang" style="float: left;">
    <div class="page-title m992">
        <h1 class="title-head margin-top-0"><a href="#">Mua điểm</a></h1>
    </div>
    <div class='frm-contact' style=" display:none">
        <div style="padding: 10px 10px 10px 10px">
            <asp:Label ID="ltmsg" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
        </div>
        <div style="width: 100%">
            <div class="tbinput">
                <div class="labelll">
                    Số điểm cần mua:
                </div>
                <div>
                    <asp:TextBox ID="txtsotiencanmua" placeholder="Nhập số điểm cần mua" class="Muadiem" ValidationGroup="Muadiem" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator SetFocusOnError="true" ID="RequiredFieldValidator2" runat="server" ValidationGroup="Muadiem" ControlToValidate="txtsotiencanmua" ErrorMessage="*"></asp:RequiredFieldValidator>
                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtsotiencanmua"></cc1:FilteredTextBoxExtender>
                </div>
            </div>
            <div class="tbinput">
                <div class="labelll">
                    Xác nhận mật khẩu:
                </div>
                <div>
                    <asp:TextBox CssClass='Muadiem' ID="txtmatkhau" TextMode="Password" ValidationGroup="Muadiem" runat="server"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ValidationGroup="Muadiem" ControlToValidate="txtmatkhau" ErrorMessage="*"></asp:RequiredFieldValidator>
                </div>
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
    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 a-left"  style=" display:none">
        <br />
        <asp:Button ID="btmuadiem" runat="server" ValidationGroup="Muadiem" Text="Mua điểm" CssClass="btnadd" OnClick="btmuadiem_Click" />
    </div>
    <br />
    <br />
    <div class="News-content">
        <div class="contents">
            <%=Commond.Setting("txtnganhangmuadiem") %>
        </div>
    </div>
</div>
<asp:HiddenField ID="hdid" runat="server" />

<script type="text/javascript">
    function DisableButton() {
        document.getElementById("<%=btmuadiem.ClientID %>").disabled = true;
    }
    window.onbeforeunload = DisableButton;
</script>
