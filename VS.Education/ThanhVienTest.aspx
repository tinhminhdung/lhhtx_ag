<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ThanhVienTest.aspx.cs" Inherits="VS.E_Commerce.ThanhVienTest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link type="text/css" rel="stylesheet" href="/Resources/admins/css/bootstrap.css" />
    <link type="text/css" rel="stylesheet" href="/Resources/admins/css/font-awesome.css" />
    <link type="text/css" rel="stylesheet" href="/Resources/admins/css/style.css" />
    <link href="Resources/css/Css_All.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" OnClick="Button1_Click" runat="server" Text="Thanh toán" />

             <asp:Button ID="bttrahag" OnClick="bttrahag_Click" runat="server" Text="Trả hàng" />

            <%-- <asp:Literal ID="Literal1" runat="server"></asp:Literal>
             <br />
               <asp:Literal ID="ltpage" runat="server"></asp:Literal>
            <br />
            <asp:Repeater ID="Repeater1" runat="server">
                <HeaderTemplate>
                    <table class="table table-striped table-bordered" id="sample_1">
                        <tr>
                            <th class="hidden-phone">Họ và tên</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="odd gradeX">
                        <td align="left" style="padding-left: 10px; line-height: 22px; color: #646465" width="300px">
                            <%#(Eval("MTRee").ToString())%><br />
                             <%#(Eval("iuser_id").ToString())%><br />

                            <span style="color: red; font-weight: bold;" id="<%#DataBinder.Eval(Container.DataItem, "vuserpwd")%>">Tên Đăng Nhập: <%#DataBinder.Eval(Container.DataItem, "vuserun")%></span><br />
                            Họ và tên:<span style="color: #444444; padding-left: 27px; font-weight: bold"><a data-tooltip="sticky<%#Eval("iuser_id") %>" target="_blank" title="Xem chi tiết hoa hồng" href="/admin.aspx?u=HoaHong&ThanhVien=<%# Eval("iuser_id") %>"><%#Eval("vfname") %></a></span><br />
                            Địa chỉ:<span style="color: #444444; padding-left: 40px; font-weight: bold"><%#DataBinder.Eval(Container.DataItem, "vaddress")%></span><br />

                        </td>

                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>--%>
        </div>


     

    </form>
</body>
</html>
