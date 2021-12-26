<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CapCha.aspx.cs" Inherits="VS.E_Commerce.CapCha" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="/Resources/js/jquery-1.7.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div style="color:#000;background:#d7d7d7;width:100px;text-align: center;display: table;padding: 20px;font-size: 23px;"><asp:Literal ID="ltshowcapcha" runat="server"></asp:Literal></div>
            <br />
            <br />
             <asp:Literal ID="lthongbao" runat="server"></asp:Literal><br />
            <br />
            <asp:TextBox ID="txtinputcapcha" runat="server"></asp:TextBox>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Button" />
        </div>
    </form>
</body>
</html>
