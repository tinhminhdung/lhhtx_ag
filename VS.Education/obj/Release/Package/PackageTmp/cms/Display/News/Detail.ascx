<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Detail.ascx.cs" Inherits="VS.E_Commerce.cms.Display.News.Detail1" %>
<section class="right-content col-md-9 col-md-push-3">
<div class="box-heading relative">
<h1 class="title-head page_title">
    <asp:Literal ID="ltcatename" runat="server"></asp:Literal></h1>
</div>
<section class="list-blogs blog-main">

<div class="News-content">
<div class="title"><h1><asp:Literal ID="lttitle" runat="server"></asp:Literal></h1></div>
<div><asp:Literal ID="ltFacebook" runat="server"></asp:Literal></div>
<div class="des-news"><asp:Literal ID="ltdesc" runat="server"></asp:Literal></div>
<div class="Other"><asp:Literal ID="ltTinlienquan" runat="server"></asp:Literal></div>
<div class="contents">    
<asp:Literal ID="ltcontent" runat="server"></asp:Literal>

<div style=" height:10px"></div>
<%if(Commond.Setting("Comment")=="1"){ %>
<div class="shares">
    <div class="fb-comments" data-href="<%=MoreAll.MoreAll.RequestUrl(Request.Url.ToString()) %>" data-width="100%" data-numposts="5" data-colorscheme="light"></div>
</div>
<%} %>

<div style=" height:10px"></div>
<div class="list-more-news">
<asp:Repeater ID="rpitems2" runat="server">
<HeaderTemplate>
<div class="title-more-news"><%=label("cactintruoc")%></div>
</HeaderTemplate>
<ItemTemplate>
<div><a href="<%#Eval("TangName")%>.html" title="<%#Eval("Title")%>"><%#AllQuery.MoreNews.Substring_Title(Eval("Title").ToString())%></a></div>                                                                                            
</ItemTemplate>
</asp:Repeater>
</div> 
<div class="list-more-news">
<asp:Repeater ID="rpitems" runat="server">
<HeaderTemplate>
<div class="title-more-news"><%=label("cactintieptheo")%></div>
</HeaderTemplate>
<ItemTemplate>
<div><a href="<%#Eval("TangName")%>.html" title="<%#Eval("Title")%>"><%#AllQuery.MoreNews.Substring_Title(Eval("Title").ToString())%></a></div>                                                                                            
</ItemTemplate>
</asp:Repeater>
</div>
 </div>
</div>

</section>
</section>

