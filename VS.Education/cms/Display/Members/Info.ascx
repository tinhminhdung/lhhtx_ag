<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Info.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Members.Info" %>
<div class="left-ct">
			<div class="itemconten">
				<h2> Thông tin thành viên</h2>
                <div class="item-ct">
				<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
 <div class="frm-add" style=" padding:10px">
    <div class="gachke">
        <div class="tenthanhvien"><%=label("l_username")%> :</div>
        <div> <asp:Literal ID="ltnickname" runat="server"></asp:Literal></div>
    </div>
    <div class="gachke">
        <div class="tenthanhvien"><%=label("lt_fullname")%> :</div>
        <div>  <asp:Literal ID="ltlname" runat="server"></asp:Literal></div>
    </div>
    <div class="gachke">
        <div class="tenthanhvien"><%=label("l_address")%> :</div>
        <div><asp:Literal ID="ltaddress" runat="server"></asp:Literal></div>
    </div>
    <div class="gachke">
        <div class="tenthanhvien"><%=label("l_phone")%> :</div>
        <div> <asp:Literal ID="ltphone" runat="server"></asp:Literal></div>
    </div>
    <div class="gachke">
        <div class="tenthanhvien">Email :</div>
        <div> <asp:Literal ID="ltemail" runat="server"></asp:Literal></div>
    </div>
    <div class="gachke">
        <div class="tenthanhvien">Ảnh đại diện:</div>
        <div><div  class="adaidien"><asp:Literal ID="ltimg" runat="server"></asp:Literal></div></div>
    </div>
<asp:Literal ID="ltregion" runat="server"></asp:Literal>
</div>
    </ContentTemplate>
</asp:UpdatePanel>
</div>
</div>
</div>


