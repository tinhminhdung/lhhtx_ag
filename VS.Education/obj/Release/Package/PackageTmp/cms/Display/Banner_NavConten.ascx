<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Banner_NavConten.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Banner_NavConten" %>
<%if (Request["su"] == "contact" || Request["su"] == "Page" || Case == "99")
  { %>
<%=Advertisings.Ad_vertisings.Advertisings("4") %>
<%} %>
<%if (Request["su"] == "Tinhson")
  { %>
<%=Advertisings.Ad_vertisings.Advertisings("7") %>
<%} %>
<%if (Case == "1" || Case == "2" || Request["su"] == "nws")
  { %>
<%=Advertisings.Ad_vertisings.Advertisings("5") %>
<%} %>
<%if (Request["su"] == "Page404" || Request["su"] == "prd" || Request["su"] == "viewcart" || Request["su"] == "msg" || Request["su"] == "Search" || Case == "20" || Case == "21" || Case == "2" || Case == "23")
  { %>
<%=Advertisings.Ad_vertisings.Advertisings("6") %>
<%} %>

<%--<% if (Request["su"] == "prd" || Request["su"] == "contact" || Request["su"] == "Page404" || Request["su"] == "viewcart" || Request["su"] == "msg" || Request["su"] == "Search" || Case == "0" || Case == "100" || Case == "3" || Case == "4" || Case == "5" || Case == "6" || Case == "7" || Case == "8" || Case == "20" || Case == "21"){%>
<span>Danh mục sản phẩm</span>
<%} %>

<% if (Request["su"] == "nws" || Case == "1" || Case == "2" || Case == "99") {%>
    <span>Danh mục tin</span>
<%} %>--%>