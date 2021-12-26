<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="XinChao.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Members.XinChao" %>
<%if (HttpContext.Current.Request.Cookies["Members"] == null) %>
<%{%>
    <a href="/Dang-nhap.html">Đăng nhập</a>
        <a  href="/Dang-ky.html">
            Đăng ký
        </a>
<%}
  else
  {%>
<div class="thanhvien">
    <ul>
        <li style="top: 8px;"><a href="/thong-tin-thanh-vien.html" style="float: left; height: 26px;">Xin chào, <%= MoreAll.MoreAll.GetCookies("Members").ToString()%></a>
            <ul>
                <li><a href="/thong-tin-thanh-vien.html"><i class="fa fa-caret-right"></i>Thông tin thành viên</a> </li>
                <li><a href="/xem-thong-tin-thanh-vien.html"><i class="fa fa-caret-right"></i>Cập nhật thông tin</a></li>
                <li><a href="/thay-doi-mat-khau.html"><i class="fa fa-caret-right"></i>Đổi mật khẩu</a></li>
                <li>
                    <asp:LinkButton ID="lnkthoat" runat="server" OnClick="lnkthoat_Click">[Thoát]</asp:LinkButton></li>
            </ul>
        </li>
    </ul>
</div>
<%}%>