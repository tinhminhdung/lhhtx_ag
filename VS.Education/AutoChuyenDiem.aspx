<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AutoChuyenDiem.aspx.cs" Inherits="VS.E_Commerce.AutoChuyenDiem" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <html xmlns="http://www.w3.org/1999/xhtml" xml:lang="vi" lang="vi-VN">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" rel="stylesheet" href="/Resources/admins/css/bootstrap.css" />
    <link type="text/css" rel="stylesheet" href="/Resources/admins/css/font-awesome.css" />
    <link type="text/css" rel="stylesheet" href="/Resources/admins/css/style.css" />
    <script src="/Resources/admins/js/bootstrap.min.js"></script>
    <script src="/Resources/js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="/Resources/admin/js/jquery-ui.js" type="text/javascript"></script>
        <link href="Resources/css/Css_All.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <asp:Literal ID="ltpage" runat="server"></asp:Literal>
         <div class="list_item">
          <asp:Repeater ID="rp_pagelist" runat="server" >
                                    <HeaderTemplate>
                                        <table class="table table-striped table-bordered" id="sample_1">
                                            <tr>
                                                <th class="hidden-phone">Họ và tên</th>
                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="odd gradeX">
                                            <td align="left" style="padding-left: 10px; line-height: 22px; color: #646465;width:200px">
                                                <span style="color: red; font-weight: bold;" id="<%#DataBinder.Eval(Container.DataItem, "vuserpwd")%>"><a target="_blank" title="Lịch sửa đăng nhập" href="/admin.aspx?u=Thanhvien&IDThanhVien=<%#Eval("iuser_id").ToString()%>" style=" color:red">Tên Đăng Nhập: <%#DataBinder.Eval(Container.DataItem, "vuserun")%></a></span><br />
                                                Họ và tên:<span style="color: #444444; padding-left: 27px; font-weight: bold"><a data-tooltip="sticky<%#Eval("iuser_id") %>" target="_blank" title="Xem chi tiết hoa hồng" href="/admin.aspx?u=Thanhvien&IDThanhVien=<%# Eval("iuser_id") %>"><%#Eval("vfname") %></a></span><br />
                                         </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                                    </div>
    </form>
</body>
</html>
