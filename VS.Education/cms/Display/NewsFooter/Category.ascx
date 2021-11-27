<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Category.ascx.cs" Inherits="VS.E_Commerce.cms.Display.NewsFooter.Category" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>

    <div class="right-home">
      <div class="rows-home">
        <p class="linktag"><a href="/">Trang chủ </a>/ <span><asp:Literal ID="ltcatename" runat="server"></asp:Literal></span></p>
    <div class="News">
  
<asp:Repeater ID="rpcates" runat="server">
<ItemTemplate>
<div class="item-news">
    <div class="title-news"><a href="/<%#Eval("TangName")%>.html" title="<%#Eval("Title")%>"><%#Eval("Title")%></a></div>
</div>
</ItemTemplate>
</asp:Repeater>
</div>
<asp:Literal ID="lterr" runat="server"></asp:Literal>
<div style="clear: both;"></div>
<div class="pager" style="margin-left:10px; margin-right:10px;color: #999;">
<cc1:CollectionPager id="CollectionPager1" runat="server"  BackNextDisplay="HyperLinks" BackNextLocation="Split"
BackText="◄" ShowFirstLast="True" ResultsLocation="None" PagingMode="PostBack" MaxPages="50" FirstText="Trang đầu" HideOnSinglePage="True"  IgnoreQueryString="False" LabelStyle="FONT-WEIGHT: bold;color:red" LabelText="" LastText="Cuối cùng" NextText="►" PageNumbersDisplay="Numbers" 
ResultsFormat="Hiển thị từ  {0} Đến {1} (của {2})" ResultsStyle="PADDING-BOTTOM:4px;PADDING-TOP:4px;FONT-WEIGHT: bold;" ShowLabel="False" ShowPageNumbers="True" BackNextStyle="FONT-WEIGHT: bold; margin: 14px;" ControlCssClass="" ControlStyle="" UseSlider="True" PageNumbersSeparator="&nbsp;">
</cc1:CollectionPager></div>
    </div>
</div>
