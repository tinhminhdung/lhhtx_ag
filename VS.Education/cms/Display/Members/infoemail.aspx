<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="infoemail.aspx.cs" Inherits="VS.E_Commerce.cms.Display.Members.infoemail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>::. <%=Commond.Setting("webname")%> .::</title> 
</head>
<body>
    <form id="form1" runat="server">
    <div><div style="text-align: center; line-height:29px; font-weight:bold; color:red; text-transform:uppercase">
        <br />    <br />    <br />    <br />
        <asp:Literal ID="ltthongbao" runat="server"></asp:Literal>
        </div>
    </div>
    </form>
</body>
</html>