<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DiagramTree.aspx.cs" Inherits="VS.E_Commerce.cms.Admin.Member.DiagramTree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        body { color: #000; font-family: 'Arial'; padding: 0 !important; margin: 0 !important; font-size: 12px; }
        .tree,.tree ul,.tree li{list-style:none;margin:0;padding:0;position:relative}
        .tree{margin:0 0 1em;text-align:center}
        .tree,.tree ul{display:table}
        .tree ul{width:100%}
        .tree li{display:table-cell;padding:.5em 0;vertical-align:top}
        .tree li:before{outline:solid 1px #666;content:"";left:0;position:absolute;right:0;top:0}
        .tree li:first-child:before{left:50%}
        .tree li:last-child:before{right:50%}
        .tree code,.tree span{border:solid .1em #666;border-radius:.2em;display:inline-block;margin:0 .2em .5em;padding:.2em .5em;position:relative}
        .tree code{font-family:monaco,Consolas,'Lucida Console',monospace}
        .tree ul:before,.tree code:before,.tree span:before{outline:solid 1px #666;content:"";height:.5em;left:50%;position:absolute}
        .tree ul:before{top:-.5em}
        .tree code:before,.tree span:before{top:-.55em}
        .tree > li{margin-top:0}
        .tree > li:before,.tree > li:after,.tree > li > code:before,.tree > li > span:before{outline:none}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
            <figure>
            <figcaption  style=" color:#00a9d2; font-size:20px; text-align:center; padding-bottom:10px">DANH SÁCH THÀNH VIÊN CẤP DƯỚI</figcaption>
                <div style=" color:red; font-size:17px; text-align:center; padding-bottom:10px"><asp:Literal ID="ltname" runat="server"></asp:Literal></div>
              <ul class="tree">
                <asp:Literal ID="ltshow" runat="server"></asp:Literal>
               <%-- <li><code>html</code>
                  <ul>
                    <li><code>head</code>
                      <ul>
                        <li><code>title</code></li>
                      </ul>
                    </li>
                    <li><code>body</code>
                    </li>
                  </ul>
                </li>--%>
              </ul>
            </figure>

    </div>
    </form>
</body>
</html>
