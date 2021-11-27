<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Control.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.WebAnalytics.Control" %>
<TABLE style="BORDER-COLLAPSE: collapse" cellPadding="0" width="100%" border="0">
<TR height="25" align=left>
	<TD>
        <b>
         <div class="Sub_menu_top_right">
         <a class='linkmainmenu' style="<%=returnCSS("WebAnalytics") %>" href='?u=WebAnalytics&su=WebAnalytics'>Biểu đồ thống kê</a>&nbsp;|
            <%--  <a class='linkmainmenu' style="<%=returnCSS("StatisticsLink") %>" href='?u=WebAnalytics&su=StatisticsLink'>Link người truy cập</a>&nbsp;|--%>
         <a class='linkmainmenu' style="<%=returnCSS("Posts") %>" href='?u=WebAnalytics&su=Posts'>Hướng dẫn bài viết</a>
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
