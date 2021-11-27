<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="QuyLuatQuangCao.ascx.cs" Inherits="VS.E_Commerce.cms.Display.AllPage.QuyLuatQuangCao" %>

<%--Kiểu quang cáo có thẻ a--%>
<%=Advertisings.Ad_vertisings.Advertisings_A_Images("1") %>



<%----------------------------------------------------------------------------%>
<%--Kiểu quảng cáo có thẻ li--%>
<%=Advertisings.Ad_vertisings.Advertisings_LI("1") %>



<%----------------------------------------------------------------------------%>
<%--Kiểu quảng cáo dành cho tất cả các nhóm như và CÓ tiêu đề ở dưới
1,Text
2,Image
3,Video Youtube
4,Flash--%>
<%--CÓ tiêu đề ở dưới--%>
<%=Advertisings.Ad_vertisings.Advertisings("1") %>



<%----------------------------------------------------------------------------%>
<%--Kiểu quảng cáo dành cho tất cả các nhóm như và KHÔNG tiêu đề ở dưới
1,Text
2,Image
3,Video Youtube
4,Flash--%>
<%--KHÔNG tiêu đề ở dưới--%>
<%=Advertisings.Ad_vertisings.Advertisings_Div("1") %>
