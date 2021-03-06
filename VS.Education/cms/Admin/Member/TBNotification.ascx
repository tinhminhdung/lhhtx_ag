<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TBNotification.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.Member.TBNotification" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script src="/Scripts/ckfinder/ckfinder.js" type="text/javascript"></script>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<script language="javascript">
    var r = {
        'special': /[\W]/g,
        'quotes': /[^0-9^,]/g,
        'notnumbers': /[^a-zA]/g
    }
    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
    }
</script>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div id="cph_Main_ContentPane">
            <div id="">
                <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
                    <ul>
                        <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang chủ</a></li>
                        <li class="Last"><span>Thông báo cho Thành viên</span></li>
                    </ul>
                </div>
                <div style="clear: both;"></div>
                <div class="widget">
                    <asp:Panel ID="pn_list" runat="server" Width="100%">
                        <div class="widget-title">
                            <h4><i class="icon-list-alt"></i>&nbsp;Thông báo cho Thành viên</h4>
                            <div class="ui-widget-content ui-corner-top ui-corner-bottom">
                                <div id="toolbox">
                                    <div class="toolbox-content" style="float: right;">
                                        <table class="toolbar">
                                            <tbody>
                                                <tr>
                                                    <td align="center">
                                                        <asp:LinkButton ID="btthemmoi" CssClass="vadd toolbar btn btn-info" OnClick="btthemmoi_Click" runat="server"><i class="icon-plus"></i>&nbsp; Thêm mới</asp:LinkButton></td>
                                                
                                                <td align="center">
                                                        <a href="/admin.aspx?u=Thanhvien" class="vadd toolbar btn btn-info"><i class=" icon-undo"></i>&nbsp; Quay về danh sách thành viên</a>
                                                    </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="widget-body">
                            <div class="row-fluid">
                                <div class="span9">

                                    <div class="dataTables_length" id="sample_1_length">
                                    <asp:DropDownList runat="server" ID="ddlPage" AutoPostBack="true" OnSelectedIndexChanged="ddlPage_SelectedIndexChanged">
                                        <asp:ListItem Value="50" Selected="True">Chọn số Bản ghi / Trang</asp:ListItem>
                                        <asp:ListItem Value="100">100 Bản ghi / Trang</asp:ListItem>
                                        <asp:ListItem Value="200">200 Bản ghi / Trang</asp:ListItem>
                                        <asp:ListItem Value="300">300 Bản ghi / Trang</asp:ListItem>
                                        <asp:ListItem Value="400">400 Bản ghi / Trang</asp:ListItem>
                                        <asp:ListItem Value="1000">1000 Bản ghi / Trang</asp:ListItem>
                                    </asp:DropDownList>
                                          <div><asp:Label ID="ltmsg" runat="server" Font-Bold="True" ForeColor="Red" Visible="True"></asp:Label></div>
                                      <div><asp:Label ID="lbl_curpage" runat="server" Font-Bold="True" ForeColor="Red" Visible="True"></asp:Label></div>
                                    </div>
                                </div>
                            </div>
                            <div class="list_item">
                                <asp:Repeater ID="rp_pagelist" runat="server" OnItemCommand="rp_pagelist_ItemCommand" >
                                    <ItemTemplate>
                                        <tr>
                                            <td style="text-align: center;">
                                             <%#MoreAll.MoreAll.RemoveHTMLTags(MoreAll.MoreAll.Substring(DataBinder.Eval(Container.DataItem, "NoiDung").ToString(), 150)) %>
                                            </td>
                                             <td style="text-align: center;">
                                             <%#DataBinder.Eval(Container.DataItem, "NgayTao")%>
                                            </td>
                                            <td style="text-align: center;">
                                                <%#DataBinder.Eval(Container.DataItem, "NguoiTao").ToString()%>
                                            </td>
                                              <td style="text-align: center;">
                                              <%#MoreAll.MoreAll.XacnhanNCC(DataBinder.Eval(Container.DataItem, "TrangThai").ToString())%>
                                            </td>
                                            <td style="text-align: center;">
                                                <asp:LinkButton CssClass="active action-link-button" ID="LinkButton1" CommandName="EditDetail" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server"><i class="icon-edit"></i></asp:LinkButton>
                                                <asp:LinkButton CssClass="active action-link-button" CommandName="Delete" CommandArgument='<%#DataBinder.Eval(Container.DataItem,"ID")%>' runat="server" ID="Linkbutton3" OnLoad="Delete_Load"><i class="icon-trash"></i></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                                            <tbody>
                                                <tr class="trHeader" style="height: 25px">
                                                    <th style="width: 4%; font-weight: bold; display: none;" class="contentadmin">No</th>
                                                     <th style="font-weight: bold;width: 500px;">Nội dung</th>
                                                    <th style="font-weight: bold">Ngày tạo</th>
                                                    <th style="font-weight: bold">Người tạo</th>
                                                     <th style="font-weight: bold">Trạng thái</th>
                                                    <th style="width: 140px; text-align: center; font-weight: bold;">Chức năng</th>
                                                </tr>
                                    </HeaderTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </div>
                            <table style="border-collapse: collapse" cellpadding="0" width="100%" border="0">
                                <tr height="20">
                                    <td align="right">
                                        <div class="phantrang" style="">
                                            <cc1:CollectionPager ID="CollectionPager1" runat="server" BackNextDisplay="HyperLinks" BackNextLocation="Split"
                                                BackText="<<" ShowFirstLast="True" ResultsLocation="Bottom" PagingMode="QueryString" MaxPages="50" FirstText="Trang đầu" HideOnSinglePage="True" IgnoreQueryString="False" LabelStyle="font-weight: bold;color:red" LabelText="" LastText="Cuối cùng" NextText=">>" PageNumbersDisplay="Numbers"
                                                ResultsFormat="Hiển thị từ  {0} Đến {1} (của {2})" ResultsStyle="padding-bottom:5px;padding-top:14px;font-weight: bold;" ShowLabel="False" ShowPageNumbers="True" BackNextStyle="font-weight: bold; margin: 14px;" ControlCssClass="" ControlStyle="" UseSlider="True" PageNumbersSeparator="">
                                            </cc1:CollectionPager>
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
                        <div class="widget-title">
                            <h4><i class="icon-list-alt"></i>&nbsp;Thêm mới + cập nhật cấp điểm </h4>
                        </div>
                        <div class="widget-body">
                            <div class="row-fluid">
                                <div class="span3">
                                    <div class="dataTables_length" id="sample_1_length">
                                        <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class='frm-add'>
                                <table class="Tables">
                                    <tr>
                                        <td align="left" width="200"></td>
                                        <td width="10"></td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left">Nội dung thông báo
                                        </td>
                                        <td></td>
                                        <td>
                                             <CKEditor:CKEditorControl ID="txtnoidung" runat="server" Toolbar="Basic"></CKEditor:CKEditorControl>
                                        </td>
                                    </tr>
                                    <tr style="display:none">
                                        <td align="left">
                                            Hiển thị
                                        </td>
                                        <td></td>
                                        <td>
                                            <asp:CheckBox ID="chck_Enable" CssClass="CsCheckBox" runat="server" Visible="True" />
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td align="left"></td>
                                        <td></td>
                                        <td>
                                            <asp:LinkButton ID="btn_InsertUpdate" runat="server" OnClick="btn_InsertUpdate_Click" style="background:#ed1c24" CssClass="toolbar btn btn-info"> <i class="icon-save"></i> Cấp điểm </asp:LinkButton>
                                            <asp:LinkButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="toolbar btn btn-info"> <i class="icon-chevron-left"></i>Hủy</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>

                            </div>
                            <asp:HiddenField ID="hdFileName" runat="server" />
                            <asp:HiddenField ID="hdid" runat="server" />
                    </asp:Panel>
                </div>
            </div>
        </div>
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
    </Triggers>
</asp:UpdatePanel>
