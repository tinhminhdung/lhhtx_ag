<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Box_ShareMangxahoi.ascx.cs" Inherits="VS.E_Commerce.cms.Display.AllPage.Box_ShareMangxahoi" %>
<link rel="stylesheet" href="/Resources/css/Share/social-likes_birman.css">
<script src="https://sp.zalo.me/plugins/sdk.js"></script>
<script src="/Resources/css/Share/social-likes.min.js"></script>
<div class="Box_Share" >
    <div style="float:left;" class="social-likes" data-url="<%=ShowUrl %>">
    <div class="facebook" title="Share link on Facebook">Facebook</div>
	<div class="twitter" title="twitter">Twitter</div>
    <div class="plusone" title="Share link on Google+">Google+</div>
    <div class="zalo-share-button"  style="display: inline-table;"  data-href="<%=ShowUrl %>" data-oaid="579745863508352884" data-layout="1" data-color="blue" data-customize=false></div>
</div>
</div>