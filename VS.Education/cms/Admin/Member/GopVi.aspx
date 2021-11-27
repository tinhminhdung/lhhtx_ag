<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GopVi.aspx.cs" Inherits="VS.E_Commerce.cms.Admin.Member.GopVi" %>

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
            <div style="width: 900px; margin: auto">
                <br />
                <asp:Repeater ID="rp_pagelist" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#ShowtThanhVien(Eval("IDThanhVien").ToString()) %>
                            </td>
                             <td style=" background:#ffa903">
                                <%#Eval("ViMuaHangAFF") %>
                            </td>
                           <%-- <td style=" background:#ffa903">
                                <%#Eval("VIAAFFILIATE") %>
                            </td>--%>
                            <td style=" background:#ffa903">
                                <%#Eval("TongTienCoinDuocCap") %>
                            </td>
                                <td style=" background:#30a9de">
                                <%#Eval("ViHoaHongAFF") %>
                            </td>
                                <td style=" background:#30a9de">
                                <%#Eval("ViHoaHongMuaBan") %>
                            </td>

                            <td>
                                <%#Eval("NgayTao") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                            <tbody>
                                <tr class="trHeader" style="height: 25px">
                                    <th style="font-weight: bold">ID Thành Viên </th>
                                    <th style="font-weight: bold">ViMuaHangAFF</th>
                                   <%-- <th style="font-weight: bold">Ví quản lý</th>--%>
                                    <th style="font-weight: bold">Ví TM</th>
                                    <th style="font-weight: bold">Hoa hồng quản lý </th>
                                    <th style="font-weight: bold">Hoa hồng mua bán</th>
                                    <th style="font-weight: bold">Ngày tạo</th>
                                </tr>
                    </HeaderTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                
                <br />
                <br />
                <br />
                <hr />
                <div style=" color:red; font-weight:bold">Tổng điểm sau khi gộp lại</div>
                 <br />
                <br />
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#ShowtThanhVien(Eval("IDThanhVien").ToString()) %>
                            </td>
                             <td style=" background:#ffa903">
                                <%#Eval("TongTienCoinDuocCap") %>
                            </td>
                             <td style=" background:#30a9de">
                                <%#Eval("ViHoaHongMuaBan") %>
                            </td>
                            <td>
                                <%#Eval("NgayTao") %>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <HeaderTemplate>
                        <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                            <tbody>
                                <tr class="trHeader" style="height: 25px">
                                    <th style="font-weight: bold">ID Thành Viên </th>
                                    <th style="font-weight: bold">Ví TM</th>
                                    <th style="font-weight: bold">Ví Tổng Hoa hồng </th>
                                    <th style="font-weight: bold">Ngày tạo</th>
                                </tr>
                    </HeaderTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <div style="text-align: center; padding: 50px">
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal></div>
            </div>
        </div>
    </form>
</body>
</html>
