<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Category.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Downloads.Category" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<link href="/cms/display/Download/Resources/css/StyleSheet.css" rel="stylesheet" type="text/css" />
<link href="/cms/display/news/Resources/css/StyleSheet.css" rel="stylesheet"  type="text/css" />
    <div class="right-home">
      <div class="rows-home">
        <p class="linktag"><a href="/">Trang chủ </a>/ <span>Thư viện Download</span></p>
  
<div class="News">
<asp:Repeater ID="rpcates" runat="server">
<ItemTemplate>
<div class="item-news">
    <a  href='/<%#MoreAll.AddURL.SeoURL(Eval("Title").ToString())%>_ta<%#Eval("ID")%>.aspx'><%#AllQuery.MoreDownload.ImagesDisplay(Eval("Images").ToString())%></a>
    <div class="title-news"><a href='/<%#MoreAll.AddURL.SeoURL(Eval("Title").ToString())%>_ta<%#Eval("ID")%>.aspx'><%#(Eval("Title").ToString())%></a></div>
    <div class="des-news"><%#(Eval("Brief"))%></div>
    <div class="chitiet"><a target=_blank href="/cms/display/Download/Defaul.aspx?id=<%#Eval("ID") %>">Tải file</a></div>
</div>
</ItemTemplate>
</asp:Repeater>
</div>
<asp:Literal ID="lterr" runat="server"></asp:Literal>
<div class="pager" style="margin-left:10px; margin-right:10px;color: #999;">
<cc1:CollectionPager id="CollectionPager1" runat="server"  BackNextDisplay="HyperLinks" BackNextLocation="Split"
BackText="◄" ShowFirstLast="True" ResultsLocation="None" PagingMode="PostBack" MaxPages="50" FirstText="Trang đầu" HideOnSinglePage="True"  IgnoreQueryString="False" LabelStyle="FONT-WEIGHT: bold;color:red" LabelText="" LastText="Cuối cùng" NextText="►" PageNumbersDisplay="Numbers" 
ResultsFormat="Hiển thị từ  {0} Đến {1} (của {2})" ResultsStyle="PADDING-BOTTOM:4px;PADDING-TOP:4px;FONT-WEIGHT: bold;" ShowLabel="False" ShowPageNumbers="True" BackNextStyle="FONT-WEIGHT: bold; margin: 14px;" ControlCssClass="" ControlStyle="" UseSlider="True" PageNumbersSeparator="&nbsp;">
</cc1:CollectionPager></div>
</div>
</div>
