<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Danhsachthanhviencapduoi.aspx.cs" Inherits="VS.E_Commerce.cms.Admin.Member.Danhsachthanhviencapduoi" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link type="text/css" rel="stylesheet" href="/Resources/admins/css/bootstrap.css" />
    <link type="text/css" rel="stylesheet" href="/Resources/admins/css/font-awesome.css" />
    <link type="text/css" rel="stylesheet" href="/Resources/admins/css/style.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
               <br />
               <br />
            <asp:Button ID="btxuatEXEL" OnClick="btxuatEXEL_Click" runat="server" Text="Xuất Exel" />
            <br />

            <asp:Repeater ID="rp_pagelist"  runat="server">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%#Eval("iuser_id") %>
                            <br /> <td>
                            <%#Eval("MTree") %>
                        </td>
                        </td>
                        <td>
                            <%#Eval("vuserun") %>
                        </td>
                        <td>
                            <%#Eval("vfname") %>
                        </td>
                        <td>
                            <%#Eval("vphone") %>
                        </td>
                        <td>
                            <%#Eval("vemail") %>
                        </td>
                        <td>
                            <%#Eval("vaddress") %>
                        </td>
                    </tr>
                </ItemTemplate>
                <HeaderTemplate>
                    <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                        <tbody>
                            <tr class="trHeader" style="height: 25px">
                                <th style="font-weight: bold">ID Thành Viên </th>
                                <th style="font-weight: bold">Tên tài khoản</th>
                                <th style="font-weight: bold">Tên thành viên</th>
                                <th style="font-weight: bold">Điện thoại</th>
                                <th style="font-weight: bold">Email</th>
                                <th style="font-weight: bold">Địa chỉ</th>
                            </tr>
                </HeaderTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <div style=" text-align:center; padding:50px"><asp:Literal ID="Literal1" runat="server"></asp:Literal></div>
        </div>
    </form>
</body>
</html>
