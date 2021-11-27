<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoiNhanhThanhVien.aspx.cs" Inherits="VS.E_Commerce.cms.Admin.Member.DoiNhanhThanhVien" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
           <div style="font-size: 18px; padding-bottom: 20px">Thành viên cấp điểm:
                                        <asp:Literal ID="ltname" runat="server"></asp:Literal></div>

      ID người giới thiệu:  <asp:TextBox ID="txtIDGioiThieu" runat="server"></asp:TextBox>
        <asp:Button ID="btnguoigioithieu" OnClick="btnguoigioithieu_Click" runat="server" Text="Cập nhật người giới thiệu" />
    </div>
    </form>
</body>
</html>
