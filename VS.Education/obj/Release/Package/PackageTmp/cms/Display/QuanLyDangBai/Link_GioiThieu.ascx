<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Link_GioiThieu.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.Link_GioiThieu" %>
<div class="page-title m992">
    <h1 class="title-head margin-top-0"><a href="#">Giới thiệu link</a></h1>
</div>

<div class="form-group">
    <div class="leftForm">
        <b>
            <p><i class="fa fa-share-alt" aria-hidden="true"></i> Link giới thiệu đăng ký thành viên hoặc nhà cung cấp</p>
        </b>
    </div>
    <div style="clear: both;"></div>
    <div class="leftForm rightForm">

        <asp:TextBox class="form-control" ID="txtlinkgioithieu" runat="server"></asp:TextBox>

    </div>
    <p>
        <%=Commond.Setting("txtnoidunglink") %>
    </p>
</div>
