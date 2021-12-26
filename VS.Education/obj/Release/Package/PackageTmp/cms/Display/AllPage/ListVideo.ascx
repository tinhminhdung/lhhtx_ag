<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ListVideo.ascx.cs" Inherits="VS.E_Commerce.cms.Display.AllPage.ListVideo" %>
<div class="Videonoibat">
    <div id="myElement">
        <asp:Literal runat="server" ID="ltrIframeVideo"></asp:Literal></div>
    <div class="ListVideo">
        <asp:Literal runat="server" ID="ltrListVideo"></asp:Literal>
    </div>
    <script type="text/javascript">
        $(".videoItems").on("click", function () {
            var strURL = this.name;
            strURL = strURL.replace("&feature=youtu.be", "");
            strURL = strURL.replace("watch?v=", "embed/");
            strURL = strURL.indexOf("?rel=0") > 0 ? strURL : strURL + "?rel=0";
            strURL = strURL.indexOf("&autoplay=1") > 0 ? strURL : strURL + "&autoplay=1";
            $("#myElement").find("iframe")[0].src = strURL;
        })
    </script>
</div>

