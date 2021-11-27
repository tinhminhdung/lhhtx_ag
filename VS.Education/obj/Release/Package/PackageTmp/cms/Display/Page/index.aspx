<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="VS.E_Commerce.cms.Display.Page.index" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script src="/Scripts/ckfinder/ckfinder.js" type="text/javascript"></script>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <link href="/Resources/admin/css/dialog.css" rel="stylesheet" type="text/css" />
    <link href="/Resources/admin/css/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="/Resources/admin/css/Manager.css" rel="stylesheet" type="text/css" />
    <link href="/Resources/admin/css/style.css" rel="stylesheet" type="text/css" />
    <link href="/Resources/admin/PopUp/css/publish.css" rel="stylesheet" type="text/css" />
    <link href="/Resources/admin/PopUp/css/style.css" rel="stylesheet" type="text/css" />
<%--    <link href="/Resources/admin/js/Loadpage/pace-theme-flash.css" rel="stylesheet" type="text/css" />--%>
    <script src="/Resources/js/jquery-1.7.1.min.js" type="text/javascript"></script>
    <script src="/Resources/admin/js/jquery-ui.js" type="text/javascript"></script>
    <%--<script language="javascript" src="/Resources/js/newwindow.js"></script>--%>
    <style type="text/css">
        #toTop
        {
            width: 100px;
            text-align: center;
            font-size: 12px;
            padding: 5px;
            position: fixed;
            bottom: 10px;
            right: 10px;
            cursor: pointer;
            font-weight: bold;
            text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <br />  <br />  <br />  <br />  <br />
         <strong><font color="#ed1f27"><asp:Literal ID="ltmsg1" runat="server"></asp:Literal></font></strong>
        <asp:Button ID="btrunwwebsite" runat="server" Text="Run các web"  OnClick="btrunwwebsite_Click"/>

         <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <div style="color:red ">/cms/Display/Page/index.aspx?kichhoat=0</div>
        <div>/cms/Display/Page/index.aspx?kichhoat=0 ---> Tắt thông báo và Gửi Email</div>
        <div>/cms/Display/Page/index.aspx?kichhoat=1 ---> Kích hoạt gửi Email</div>
        <div>/cms/Display/Page/index.aspx?kichhoat=2---> Kích hoạt Thông báo  +++  lấy nội dung trong CKEditor</div>
        <div>/cms/Display/Page/index.aspx?kichhoat=3---> Kích hoạt Thông báo  +++  lấy nội dung trong Code</div>
        <div>/cms/Display/Page/index.aspx?kichhoat=4---> Tắt nội dung vi phạm</div>
        <div>/cms/Display/Page/index.aspx?kichhoat=5 ---> Tắt gửi Email</div>
       <br /> <br /> <br />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table style="border-collapse: collapse" cellpadding="0" width="100%" border="0">
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td style="height: 26px">
                    <strong><font color="#ed1f27"><asp:Literal ID="ltmsg" runat="server"></asp:Literal></font></strong>
                </td>
            </tr>
              <tr>
                <td>
                </td>
              <td  style="text-transform: uppercase">
                    <img src="/Resources/admin/images/bullet-red.png" border="0" />
                    <strong>Kích hoạt gửi Email</strong>
                </td>
                <td>
                     <asp:RadioButton ID="RadioButton1" runat="server" Text="Tắt" GroupName="Email" Checked="true"></asp:RadioButton>
                     <asp:RadioButton ID="RadioButton2" runat="server" Text="Bật" GroupName="Email"></asp:RadioButton>
                </td>
            </tr>
            <tr>
                <td colspan="3" style="text-transform: uppercase">
                    <img src="/Resources/admin/images/bullet-red.png" border="0" />
                    <strong>
                   <%=MoreAll.Other.website() %> == <%=MoreAll.MoreAll.RequestUrl(Request.Url.Authority) %></strong>
                </td>
            </tr>
          <tr>
                <td>
                </td>
                <td style="padding-left: 15px">
                 Website
                </td>
                <td>
                    <asp:TextBox CssClass="txt_css" ID="txtwebsite" runat="server" Width="752px" Height="150px" TextMode="MultiLine"  ></asp:TextBox>
                </td>
            </tr>

             <tr>
                <td>
                </td>
                <td style=" height:10px">
                </td>
                <td>
                </td>
            </tr>
                   <tr>
                <td>
                </td>
                <td style="padding-left: 15px">
                 Redirect website
                </td>
                <td>
                    <asp:TextBox CssClass="txt_css" ID="txtRedirect" runat="server" Width="752px" Height="50px" ></asp:TextBox>
                </td>
            </tr>

              <tr>
                <td>
                </td>
                    <td style="text-transform: uppercase">
                    <img src="/Resources/admin/images/bullet-red.png" border="0" />
                    <strong>Kích hoạt thông báo</strong>
                </td>
                <td>
                     <asp:RadioButton ID="Thongbao1" runat="server" Text="Không hoạt thông báo vi phạm" GroupName="co" Checked="true"></asp:RadioButton>
                     <asp:RadioButton ID="Thongbao2" runat="server" Text="kích hoạt thông báo vi phạm - lấy nội dung của CKEditor" GroupName="co"></asp:RadioButton>
                     <asp:RadioButton ID="Thongbao3" runat="server" Text="Kích hoạt thông báo vi phạm - lấy nội dung trong code" GroupName="co"></asp:RadioButton>
                </td>
            </tr>
             <tr>
                <td>
                </td>
                <td style="padding-left: 15px">
                 Nọi dung vi phạm
                </td>
                <td>
               <CKEditor:CKEditorControl ID="txtcontent" runat="server" Toolbar="Basic"></CKEditor:CKEditorControl>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnsetup" runat="server" Text="Update" Font-Bold="True" Font-Size="8pt" OnClick="btnsetup_Click" Width="123px"></asp:Button>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

            <table style="border-collapse: collapse" cellpadding="0" width="100%" border="0">
              <tr>
                <td>
                </td>
              <td  style="text-transform: uppercase">
                    <img src="/Resources/admin/images/bullet-red.png" border="0" />
                    <strong>Danh sách các trang vi phạm</strong>
                </td>
                <td>
                </td>
            </tr>
                </table>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div style="margin-top: 10px;" class="frm_search">
                            <asp:Label ID="lbl_curpage" runat="server" Font-Bold="True" ForeColor="Red" Visible="True"></asp:Label>
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>&nbsp;
                            </div>
        <asp:Panel ID="pn_list" runat="server" Width="100%">
            <div style="margin-top: 10px;" class="frm_search">
                <table border="0" cellpadding="0" style="border-collapse: collapse" width="100%">
                    <tr>
                        <td>
                           
                        </td>
                        <td class="topcontent" style="width: 50%">
                            &nbsp;<asp:Button ID="btthemmoi" runat="server" ForeColor="Green" OnClick="btthemmoi_Click" Text="Thêm mới" Width="87px" />
                            <asp:Button ID="btxoa" runat="server" OnClick="btxoa_Click" OnClientClick=" return confirmDelete(this);" Text="Xóa" ToolTip="Xóa những lựa chọn !" Width="35px" ForeColor="Green" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="list_item">
                <asp:Repeater ID="rp_pagelist" runat="server" OnItemCommand="rp_pagelist_ItemCommand">
                    <ItemTemplate>
                       <tr style="background-color:#fcf0e0"  height="40">
                            <td align="center">
                                <asp:CheckBox ID="chkid" runat="server" onclick="javascript:changeColor(this,'white');" /><asp:HiddenField ID="hiID" Value='<%# Eval("ID") %>' runat="server" />
                            </td>
                            <td>
                                 <%#DataBinder.Eval(Container.DataItem,"Name")%>
                            </td>
                              <td>
                                   <a href="<%#DataBinder.Eval(Container.DataItem,"Description")%>" target="_blank"> <%#DataBinder.Eval(Container.DataItem,"Description")%></a>
                            </td>
                            <td align="center">
                              <asp:TextBox ID="txtOrders" style=" border:1px solid #d7d7d7; border-radius:3px; text-align:center"  Text='<%#DataBinder.Eval(Container.DataItem, "Orders")%>' CssClass="txt_css" Width="40px" runat="server" OnTextChanged="txtOrders_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td align="center">
                                <asp:LinkButton CommandName="ChangeStatus" CommandArgument='<%#Eval("ID")+"|"+Eval("Status")%>'  runat="server" ID="Linkbutton4"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "Status").ToString())%></asp:LinkButton>
                            </td>
                            <td align="center">
                                <asp:LinkButton ID="LinkButton1" CommandName="EditDetail" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server">[<%=label("lt_edit")%>]</asp:LinkButton>
                            </td>
                            <td align="center">
                                <div class="del"><asp:LinkButton CommandName="Delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server" ID="Linkbutton3" OnLoad="Delete_Load">[<%=label("ldelete")%>]</asp:LinkButton></div>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                        <tr height="40">
                            <td align="center">
                                <asp:CheckBox ID="chkid" runat="server" onclick="javascript:changeColor(this,'white');" /><asp:HiddenField ID="hiID" Value='<%# Eval("ID") %>' runat="server" />
                            </td>
                            <td>
                                  <%#DataBinder.Eval(Container.DataItem,"Name")%>
                            </td>
                               <td>
                                
                                   <a href="<%#DataBinder.Eval(Container.DataItem,"Description")%>" target="_blank"> <%#DataBinder.Eval(Container.DataItem,"Description")%></a>
                            </td>
                            <td align="center">
                              <asp:TextBox ID="txtOrders" style=" border:1px solid #d7d7d7; border-radius:3px; text-align:center"  Text='<%#DataBinder.Eval(Container.DataItem, "Orders")%>' CssClass="txt_css" Width="40px" runat="server" OnTextChanged="txtOrders_TextChanged" AutoPostBack="true"></asp:TextBox>
                            </td>
                            <td align="center">
                                <asp:LinkButton CommandName="ChangeStatus" CommandArgument='<%#Eval("ID")+"|"+Eval("Status")%>'  runat="server" ID="Linkbutton4"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "Status").ToString())%></asp:LinkButton>
                            </td>
                            <td align="center">
                                <asp:LinkButton ID="LinkButton1" CommandName="EditDetail" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>'  runat="server">[<%=label("lt_edit")%>]</asp:LinkButton>
                            </td>
                            <td align="center">
                                <div class="del"><asp:LinkButton CommandName="Delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server" ID="Linkbutton3" OnLoad="Delete_Load">[<%=label("ldelete")%>]</asp:LinkButton></div>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>
                    <HeaderTemplate>
                        <table border="0" width="100%" cellpadding="0" style="border-collapse: collapse">
                            <tr bgcolor="#C4C4C4" height="22">
                                <td class="header">
                                    <input id="chkAll" onclick="javascript: SelectAllCheckboxes(this, 1);" type="checkbox" />
                                </td>
                                <td class="header">
                                    <%=label("answers_title")%>
                                </td>
                                 <td class="header">
                                 Link
                                </td>
                                <td class="header">
                                    <%=label("lt_order")%>
                                </td>
                                <td class="header">
                                    <%=label("l_status")%>
                                </td>
                               
                                <td class="header">
                                    <%=label("lt_edit")%>
                                </td>
                                <td class="header">
                                    <%=label("ldelete")%>
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <table style="border-collapse: collapse" cellpadding="0" width="100%" border="0">
                <tr height="20">
                                        <td align=right>
                    <div class="phantrang" style=" ">
                  
                    </div>
                    </td>
                </tr>
                <tr height="25" bgcolor="whitesmoke">
                    <td>
                        <asp:LinkButton ID="LinkButton5" Font-Bold="true" OnClick="LinkButton4_Click" runat="server">[Thêm mới]</asp:LinkButton>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pn_insert" runat="server" Visible="False" Width="100%">
            <div class='frm-add'>
                <table style="border-collapse: collapse" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="right" width="175">
                        </td>
                        <td width="10">
                        </td>
                        <td>
                            <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <%=label("answers_title")%>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_title" CssClass="txt_css" runat="server" Width="320px"></asp:TextBox>
                        </td>
                    </tr>
                 
 <tr style=" display:none">
<td align="right">
icon
</td>
<td></td>
<td>
<div>
    <div align=left style=" float:left; width:700px">
                    <asp:RadioButton ID="rdFromComputer"  runat="server" CssClass="txt_css2" AutoPostBack="True" Checked="true" GroupName="FromType" OnCheckedChanged="rdFromComputer_CheckedChanged" Text="Từ máy tính của bạn" ValidationGroup="downloadtype" />
                    <asp:RadioButton ID="rdFromLinks" runat="server" CssClass="txt_css2" AutoPostBack="True" GroupName="FromType" OnCheckedChanged="rdFromLinks_CheckedChanged"  Text="Từ 1 liên kết" />&nbsp;&nbsp;
                    <asp:Button ID="btDeleteimages" CssClass="txt_css" runat="server" Text="Delete" OnClick="btDeleteimages_Click" Width="75px" /><br />
                    <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
                        <asp:View ID="vwFromComputer" runat="server">
                            <asp:FileUpload CssClass="txt_css"  ID="flimage" runat="server" Width="323px" />
                        </asp:View>
                        <asp:View ID="vwFromLinks" runat="server">
                            <asp:TextBox CssClass="txt_css" ID="txtvimg" runat="server" Width="99%"></asp:TextBox><br />
                        </asp:View>
                    </asp:MultiView>
            </div>
           <div  style="padding:0px 0px 0px 0px">
            <asp:Literal ID="ltimg" runat="server"></asp:Literal>
           </div>
   </div>
</td>
</tr>
 <tr>
                        <td align="right">
                            <%=label("answers_title")%>
                        </td>
                        <td>
                        </td>
                        <td>
                             <asp:TextBox ID="txtlink" CssClass="txt_css" runat="server" Width="100%"></asp:TextBox>
                        </td>
                    </tr>
                        <tr style=" display:none">
                        <td align="right">
                           Tính năng seo
                        </td>
                        <td>
                        </td>
                        <td>
                            
                             <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                <ProgressTemplate>
                                   <img src="/Resources/admin/images/loading.gif" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <asp:Button ID="btseo" runat="server" Text="Tính năng seo"  onclick="btseo_Click" />
                        </td>
                    </tr>
                    <asp:Panel ID="pnseo" runat="server" Visible="False" Width="100%">
                     <tr>
                        <td align="left" colspan="3">
                           <div style=" background:#f7f7f7;border:1px solid #d7d7d7; -webkit-border-radius: 3px;-moz-border-radius: 3px;border-radius: 3px;width:700px; margin-left:180px">
                           <table>
                          <tr>
                        <td align=left>
                           Tiêu đề từ khóa (Title)
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="txttitleseo" CssClass="txt_css" runat="server" Width="320px"></asp:TextBox>
                        </td>
                    </tr>
                        <tr>
                          <td align=left>
                            Nội dung hiển thị trong (Description)
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="txtmeta" CssClass="txt_css" runat="server" Width="392px" 
                                Height="35px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                         <td align=left>
                           Nội dung hiển thị trong (Keyword)
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:TextBox ID="txtKeyword" CssClass="txt_css" runat="server" Width="459px" 
                                Height="43px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                       </table>
                           </div>
                          
                        </td>
                    </tr>
                    </asp:Panel>
           <tr style=" display:none">
    <td align="right">
        Tùy chọn</td>
    <td>
    </td>
    <td>
           
            <asp:CheckBox ID="chknews" Visible="false" CssClass="txt_css2" runat="server" Text="Mới" />
            <asp:CheckBox ID="chkTrangChu"  CssClass="txt_css2" runat="server"  Text="Trang chủ" />
           <span style="font-size: 8pt; color: #ed1c24"><em>(Sẽ được hiển thị vào trong các mục định sẵn.)</em></span>
    </td>
</tr>
            <tr>
                <td align="right">
                    <%=label("lt_order")%>
                </td>
                <td>
                </td>
                <td>
                    <asp:TextBox ID="txt_order" runat="server" CssClass="txt_css" Width="32px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <%=label("lt_display")%>
                </td>
                <td>
                </td>
                <td>
                    <asp:CheckBox ID="chck_Enable" CssClass="txt_css2" runat="server" Visible="True" />
            </tr>
            </table> </div>
            <div style="padding-left: 150px; padding-top: 10px;">
                <asp:Button ID="btn_InsertUpdate" runat="server" OnClick="btn_InsertUpdate_Click"  Text="Insert/Update" Width="120px" />
                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" Width="56px" />
            </div>
            <asp:HiddenField ID="hdFileName" runat="server" />
            <asp:HiddenField ID="hdid" runat="server" />
        </asp:Panel>
        <input id="hd_insertupdate" type="hidden" size="1" name="Hidden1" runat="server">
        <input id="hd_id" type="hidden" size="1" name="Hidden2" runat="server">
        <input id="hd_page_edit_id" type="hidden" size="1" name="Hidden2" runat="server">
        <input id="hd_imgpath" type="hidden" size="1" name="Hidden2" runat="server">
        <input id="hd_rootpic" type="hidden" size="1" runat="server">
        <input id="hd_par_id" type="hidden" size="1" name="Hidden2" runat="server">
         <asp:HiddenField ID="hidLevel" runat="server" />
    </ContentTemplate>
     <Triggers>
        <asp:PostBackTrigger ControlID="btn_InsertUpdate" />
            <asp:PostBackTrigger ControlID="btDeleteimages" />
    </Triggers>
</asp:UpdatePanel>



    </div>
    </form>
</body>
</html>
