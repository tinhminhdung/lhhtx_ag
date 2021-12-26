<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ChuyenDiemTrongVi.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.ChuyenDiemTrongVi" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<div class="page-title m992">
    <h1 class="title-head margin-top-0"><a href="#">Chuyển điểm trong ví thương mại sang ví mua hàng</a></h1>
</div>
 <asp:Label ID="ltmsg" runat="server" ForeColor="White" ></asp:Label>
<asp:Literal ID="ltthongbao" runat="server"></asp:Literal>
<div class="congtongvuietien">
    <div style="clear: both"></div>
    <br />   <br />
    <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
        <div class="vitien">
            <div><i class="fa fa-credit-card"></i> Ví thương mại</div>
            <div class="Coin">
                <asp:Literal ID="ltvithuongmai" runat="server"></asp:Literal>
                điểm
            </div>
        </div>
    </div>
    <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
        <div class="chuyendiemthue">
            <div style="text-transform: uppercase;"> <i class="fa fa-long-arrow-right" aria-hidden="true" style="font-size: 35px; float: left;"></i> Chuyển sang ví mua hàng</div>
            <div>
                <asp:TextBox ID="txtsodiem" style=" height: 36px !important; margin-bottom: -5px !important; " placeholder="Số điểm cần chuyển" ValidationGroup="GInfo" runat="server"></asp:TextBox><br />
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="GInfo" ControlToValidate="txtsodiem" ErrorMessage="Vui lòng điền số điểm cần chuyển."></asp:RequiredFieldValidator>
               <div style="clear: both"></div>
                 <asp:Button ID="btchuyensangvithuongmai" OnClick="btchuyensangvithuongmai_Click" ValidationGroup="GInfo" CssClass="bamchuyendiem" runat="server" Text="Chuyển điểm" />
            </div>
        </div>
    </div>

    <div class="col-xs-6 col-sm-12 col-md-4 vithanhtoan">
        <div class="vitien">
            <div><i class="fa fa-credit-card"></i> Ví mua hàng</div>
            <div class="Coin">
                <asp:Literal ID="ltvimuahang" runat="server"></asp:Literal>
                điểm
            </div>
        </div>
    </div>
        <div style="clear: both"></div>
                <div><span style="font-size: 11pt; color: #ed1c24; text-transform: initial; width: 90%; padding: 6px;"><em>Chuyển điểm từ ví thương mại sang ví mua hàng của chính mình</em></span></div>

    <div style="clear: both"></div>
    <hr />
</div>

<div style="clear: both"></div>
<asp:HiddenField ID="hdid" runat="server" />
