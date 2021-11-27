<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Control.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.settings.Control" %>
<TABLE style="BORDER-COLLAPSE: collapse" cellPadding="0" width="100%" border="0">
	<TR height="25" align=left>
		<TD>
			<b>
            <div class="Sub_menu_top_right">
                <a class='linkmainmenu' style="<%=returnCSS("set") %>" href='?u=set&su=set'>Cài đặt hệ thống</a>&nbsp;|
                <a class='linkmainmenu' style="<%=returnCSS("pr") %>"  href='?u=set&su=pr'>Cài đặt thuộc tính</a>&nbsp;|
                <a class='linkmainmenu' style="<%=returnCSS("OnOff") %>"  href='?u=set&su=OnOff'>Tắt mở trang web</a>&nbsp;|
                <a class='linkmainmenu' style="<%=returnCSS("GoogleAnalytics") %>" href='?u=set&su=GoogleAnalytics'>Nhúng mã</a>&nbsp;|
                  <a class='linkmainmenu' style="<%=returnCSS("TBNhaCungCap") %>" href='?u=set&su=TBNhaCungCap'>Thông báo cho toàn bộ nhà Cung Cấp</a>&nbsp;|
                


<%--                <a class='linkmainmenu' style="<%=returnCSS("languages") %>" href='?u=set&su=languages'>Ngôn ngữ</a>&nbsp;|--%>
                
            </div>
            </b>
		</TD>
	</TR>
	<TR>
		<TD height="1"></TD>
	</TR>
	<TR>
		<TD>
            <asp:PlaceHolder ID="phcontrol" runat="server"></asp:PlaceHolder>
        </TD>
	</TR>
</TABLE>