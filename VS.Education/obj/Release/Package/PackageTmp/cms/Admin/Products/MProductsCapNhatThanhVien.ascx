﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MProductsCapNhatThanhVien.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.Products.MProductsCapNhatThanhVien" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script src="/Scripts/ckfinder/ckfinder.js" type="text/javascript"></script>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<link href="/cms/Admin/Products/Css/Style.css" rel="stylesheet" type="text/css" />
<script src="/cms/Admin/Products/Css/star-rating-svg/jquery.star-rating-svg.js" type="text/javascript"></script>
<link href="/cms/Admin/Products/Css/star-rating-svg/star-rating-svg.css" rel="stylesheet" type="text/css" />
<script type = "text/javascript">
    var ddlText, ddlValue, ddl, lblMesg;
    function CacheItems() {
        ddlText = new Array();
        ddlValue = new Array();
        ddl = document.getElementById("<%=ddlthanhvien.ClientID %>");
        lblMesg = document.getElementById("<%=lblMessage.ClientID%>");
        for (var i = 0; i < ddl.options.length; i++) {
            ddlText[ddlText.length] = ddl.options[i].text;
            ddlValue[ddlValue.length] = ddl.options[i].value;
        }
    }
    window.onload = CacheItems;
    
    function FilterItems(value) {
        ddl.options.length = 0;
        for (var i = 0; i < ddlText.length; i++) {
            if (ddlText[i].toLowerCase().indexOf(value) != -1) {
                AddItem(ddlText[i], ddlValue[i]);
            }
        }
        lblMesg.innerHTML = "Tìm thấy: "+ ddl.options.length + " kết quả.";
        if (ddl.options.length == 0) {
            AddItem("Không tìm thấy thành viên nào", "");
        }
    }
    
    function AddItem(text, value) {
        var opt = document.createElement("option");
        opt.text = text;
        opt.value = value;
        ddl.options.add(opt);
    }
</script>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <div id="cph_Main_ContentPane">
            <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
                <ul>
                    <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang chủ</a></li>
                    <li class="Last"><span>Danh sách sản phẩm</span></li>
                </ul>
            </div>
            <div style="clear: both;"></div>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="widget">
                                <div class="widget-title">
                                    <h4><i class="icon-reorder"></i>Danh sách sản phẩm</h4>

                                    <div class="ui-widget-content ui-corner-top ui-corner-bottom">
                                        <div id="toolbox">
                                            <div class="toolbox-content" style="float: right; margin-top: 9px;">
                                                <asp:Label ID="ltmsg" runat="server" Font-Bold="True" ForeColor="Red" Visible="True"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="widget-body">
                                    <div class="row-fluid">
                                        <div class="span9">
                                            <div id="sample_1_length" class="dataTables_length">
                                                 <div class="" >
                                                    <div>
                                                        <asp:TextBox ID="txtSearch" placeholder="Tìm kiếm thành viên" runat="server"  onkeyup = "FilterItems(this.value)"></asp:TextBox>
                                                         <asp:DropDownList ID="ddlthanhvien" CssClass="txt" runat="server"></asp:DropDownList>
                                                          <asp:LinkButton ID="lnkcapnhatthanhvien"  runat="server" OnClick="lnkcapnhatthanhvien_Click" CssClass="vadd toolbar btn btn-info" style=" background:red"> <i class=" icon-folder-close"></i>&nbsp;Cập nhật thành viên</asp:LinkButton>
                                                      <br />  <asp:Label ID="lblMessage" Font-Bold="true" ForeColor="Red" runat="server" Text=""></asp:Label>
                                                          </div>
                                                     </div>

                                                <div class="frm_search">
                                                    <div>
                                                        <asp:TextBox ID="txtkeyword" runat="server" CssClass="txt_csssearch" Width="400px"></asp:TextBox>
                                                        <asp:LinkButton ID="lnksearch" runat="server" OnClick="lnksearch_Click" CssClass="vadd toolbar btn btn-info" Style="margin-top: -9px;"> <i class="icon-search"></i>&nbsp;Tìm kiếm</asp:LinkButton>
                                                        <asp:Label ID="ltthongbao" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                    </div>
                                                    <div style="margin-top: 10px;">
                                                        <asp:DropDownList ID="ddlcategories" CssClass="txt" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlcategories_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                                            <asp:ListItem Value="-1" Selected="True">Tất cả các mục</asp:ListItem>
                                                            <asp:ListItem Value="1">Hiển thị</asp:ListItem>
                                                            <asp:ListItem Value="0">Ẩn</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddltrangthaithanhvien" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddltrangthaithanhvien_SelectedIndexChanged">
                                                            <asp:ListItem Value="0" Selected="True">Tất cả thành viên</asp:ListItem>
                                                            <asp:ListItem Value="1">Hiển thị theo Admin</asp:ListItem>
                                                            <asp:ListItem Value="2">Hiển thị theo thành viên</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlorderby" runat="server" AutoPostBack="true" CssClass="txt" OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                                            <asp:ListItem Selected="True" Value="Create_Date">S.xếp:Ngày cập nhật</asp:ListItem>
                                                            <%-- <asp:ListItem Value="Modified_Date">S.xếp:Ngày hết hạn</asp:ListItem>--%>
                                                            <asp:ListItem Value="Price">S.xếp:Theo giá hiện tại</asp:ListItem>
                                                            <asp:ListItem Value="Views">S.xếp:Lần xem</asp:ListItem>
                                                            <asp:ListItem Value="Name">S.xếp:Tiêu đề (ABC)</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlordertype" runat="server" AutoPostBack="True" CssClass="txt" OnSelectedIndexChanged="ddlordertype_SelectedIndexChanged">
                                                            <asp:ListItem Value="desc">Giảm dần</asp:ListItem>
                                                            <asp:ListItem Value="asc">Tăng dần</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlvitri" Visible="false" runat="server" AutoPostBack="True" CssClass="txt" OnSelectedIndexChanged="ddlvitri_SelectedIndexChanged">
                                                            <asp:ListItem Value="0" Selected="True">Vị trí hiển thị</asp:ListItem>
                                                            <asp:ListItem Value="1">Số lượng - Hết hàng</asp:ListItem>
                                                            <asp:ListItem Value="2">Số lượng - Từ 0 đến 10</asp:ListItem>
                                                            <asp:ListItem Value="3">Số lượng - Từ 10 đến 50</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span3">
                                            <div class="dataTables_filter" id="sample_1_filter">
                                                <asp:LinkButton ID="bthienthi" runat="server" OnClick="bthienthi_Click" CssClass="vadd toolbar btn btn-info"> <i class=" icon-folder-close"></i>&nbsp;Hiện thị</asp:LinkButton>
                                                <asp:LinkButton ID="btDeleteall" ToolTip="Xóa những lựa chọn !" OnClientClick=" return confirmDelete(this);" runat="server" OnClick="btDeleteall_Click" CssClass="vadd toolbar btn btn-info"><i class="icon-trash"></i>&nbsp;Xóa</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="list_item">
                                        <asp:Repeater ID="rpitems" runat="server" OnItemCommand="rpitems_ItemCommand1">
                                            <ItemTemplate>
                                                <tr height="40">
                                                    <td style="text-align: center;">
                                                        <asp:CheckBox ID="chkid" runat="server" onclick="javascript:changeColor(this,'white');" /><asp:HiddenField ID="hiID" Value='<%# Eval("ipid") %>' runat="server" />
                                                    </td>
                                                    <td style="text-align: center;" class="Imgsmall">
                                                        <div style="position: relative;">
                                                            <span class="anhn"><a title="<%#Eval("Name")%>" target="_blank" href="/<%#Eval("TangName")%>.html"><%#MoreAll.MoreImage.Image(Eval("Images").ToString())%></a></span>
                                                            <%#Hethang(Eval("Quantity").ToString())%>
                                                        </div>
                                                        <%--<div class="my-rating<%#Eval("ipid")%>"></div>
                                                        <script>$(".my-rating<%#Eval("ipid")%>").starRating({ initialRating: <%#GetRating(Eval("ipid").ToString())%>, strokeColor: '#894A00', strokeWidth: 5, starSize: 15, readOnly: true }); </script>--%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtTieude" Style="border: 1px solid #d7d7d7; border-radius: 3px; text-align: left; width: 95%; height: 50px" TextMode="MultiLine" Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' CssClass="txt_css" runat="server" OnTextChanged="txtTieude_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                    </td>
                                                    <%-- <td>
                                                        <b><a title="<%#Eval("Name")%>" target="_blank" href="/<%#Eval("TangName")%>.html"><%#MoreAll.MoreAll.Substring(Eval("Name").ToString(), 30)%></a></b>
                                                        <div class="Mausac"><%#MoreMau(Eval("ipid").ToString())%></div>
                                                        <div style="clear: left"></div>
                                                        <div class="Kichhuoc"><%#MoreSize(Eval("ipid").ToString())%></div>
                                                    </td>--%>
                                                    <td>
                                                        
                                                        <div style="margin-bottom: 4px;"><span style="width: 15px;">N/Y:</span><asp:TextBox ID="txtgiacu" Style="width: 100px; text-align: center;" Text='<%#AllQuery.MorePro.FormatMoney_Cart(Eval("OldPrice").ToString())%>' onkeyup="valid(this,'quotes')" onblur="valid(this,'quotes')" CssClass="txt_css" runat="server" OnTextChanged="txtgiacu_TextChanged" AutoPostBack="true"></asp:TextBox></div>
                                                        <div><span style="width: 15px;">Bán:</span><asp:TextBox ID="txtgiaban" Style="width: 100px; text-align: center;" Text='<%#AllQuery.MorePro.FormatMoney_Cart(Eval("Price").ToString())%>' onkeyup="valid(this,'quotes')" onblur="valid(this,'quotes')" CssClass="txt_css" runat="server" OnTextChanged="txtgiaban_TextChanged" AutoPostBack="true"></asp:TextBox></div>
                                                        <div><span style="width: 15px;">Đ.Lý:</span><asp:TextBox ID="txtgiabanthanhvien" Style="width: 100px; text-align: center;" Text='<%#AllQuery.MorePro.FormatMoney_Cart(Eval("GiaThanhVien").ToString())%>' onkeyup="valid(this,'quotes')" onblur="valid(this,'quotes')" CssClass="txt_css" runat="server" OnTextChanged="txtgiabanthanhvien_TextChanged" AutoPostBack="true"></asp:TextBox></div>
                                                        <div><span style="width: 15px;">C.TY:</span><asp:TextBox ID="txtgiacongtynhapvao" Style="width: 100px; text-align: center;" Text='<%#AllQuery.MorePro.FormatMoney_Cart(Eval("Giacongtynhapvao").ToString())%>' onkeyup="valid(this,'quotes')" onblur="valid(this,'quotes')" CssClass="txt_css" runat="server" OnTextChanged="txtgiacongtynhapvao_TextChanged" AutoPostBack="true"></asp:TextBox></div>
                                                     <%--   <div> <%#MoreImages(Eval("ipid").ToString(), Eval("icid").ToString())%></div>--%>
                                                    </td>
                                               

                                                    <td style="text-align: center;">
                                                        <%#ShowThanhVien(DataBinder.Eval(Container.DataItem,"IDThanhVien").ToString())%>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <%#Commond.DiemTichLuyAdd(DataBinder.Eval(Container.DataItem,"GiaThanhVien").ToString(),DataBinder.Eval(Container.DataItem,"Giacongtynhapvao").ToString())%>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <%#MoreAll.MoreAll.FormatDate(DataBinder.Eval(Container.DataItem, "Create_Date"))%>
                                                        <div>
                                                            <asp:LinkButton ID="LinkButton4" CssClass="active action-link-button" CommandName="Chekdata" CommandArgument='<%#Eval("ipid") %>' runat="server"> <%#MoreAll.MoreAll.Enable_Date(DataBinder.Eval(Container.DataItem, "Chekdata").ToString())%></asp:LinkButton>
                                                        </div>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <%#DataBinder.Eval(Container.DataItem,"Views")%>
                                                    </td>
                                                    <td style="text-align: center;">
                                                     
                                                        <asp:LinkButton ID="LinkButton5" CssClass="active action-link-button" CommandName="updat_date" CommandArgument='<%#Eval("ipid") %>' runat="server"><i class=" icon-refresh"></i></asp:LinkButton>
                                                    </td>
                                                    <td style="text-align: center; display: none">
                                                        <asp:TextBox ID="TextBox1" Text='<%#DataBinder.Eval(Container.DataItem, "Quantity")%>' CssClass="txt_css" Width="30px" runat="server" OnTextChanged="txtxQuantity_TextChanged" AutoPostBack="true"></asp:TextBox></td>

                                                    <td style="text-align: center;">
                                                        <asp:LinkButton CommandName="ChangeHome" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("Home")%>' runat="server" ID="Linkbutton6"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "Home").ToString())%></asp:LinkButton>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:LinkButton CommandName="ChangeNews" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("News")%>' runat="server" ID="Linkbutton7"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "News").ToString())%></asp:LinkButton>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:LinkButton CommandName="ChangeCheck_01" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("Check_01")%>' runat="server" ID="Linkbutton8"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "Check_01").ToString())%></asp:LinkButton>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:LinkButton CommandName="ChangeCheck_02" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("Check_02")%>' runat="server" ID="Linkbutton9"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "Check_02").ToString())%></asp:LinkButton>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:LinkButton CommandName="ChangeCheck_03" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("Check_03")%>' runat="server" ID="Linkbutton10"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "Check_03").ToString())%></asp:LinkButton>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:LinkButton CommandName="ChangeCheck_04" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("Check_04")%>' runat="server" ID="Linkbutton11"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "Check_04").ToString())%></asp:LinkButton>
                                                    </td>
                                                    <td style="text-align: center;">
                                                        <asp:LinkButton CommandName="ChangeCheck_05" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("Check_05")%>' runat="server" ID="Linkbutton12"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "Check_05").ToString())%></asp:LinkButton>
                                                    </td>

                                                    <td style="text-align: center;">
                                                        <asp:LinkButton CommandName="ChangeStatus" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("Status")%>' runat="server" ID="Linkbutton3"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "Status").ToString())%></asp:LinkButton>
                                                       <%-- <asp:LinkButton ID="LinkButton1" CssClass="active action-link-button" CommandName="update" CommandArgument='<%#Eval("ipid") %>' runat="server"><i class="icon-edit"></i></asp:LinkButton>
                                                        <asp:LinkButton CssClass="active action-link-button" OnLoad="Delete_Load" CommandName="delete" CommandArgument='<%#Eval("ipid") %>' ID="LinkButton2" runat="server"><i class="icon-trash"></i></asp:LinkButton>--%>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                                                    <tr class="trHeader" style="height: 25px">
                                                        <td class="header">
                                                            <input id="Checkbox1" onclick="javascript:SelectAllCheckboxes(this,1);" type="checkbox" /></td>
                                                        <td class="header">Hình ảnh</td>
                                                        <td class="header">Tên sản phẩm</td>
                                                        <td class="header" style="width: 130px;">Giá</td>
                                                        <td class="header">Thành viên</td>
                                                        <td class="header">Điểm<br /> Mua hàng</td>
                                                        <td class="header">Ngày tạo</td>
                                                        <td class="header">Xem</td>
                                                        <td class="header">Làm mới</td>
                                                        <%--  <td class="header">Số lượng</td>--%>
                                                        <td class="header">Trang chủ</td>
                                                        <td class="header">Giá tốt<br />
                                                            Hôm nay</td>
                                                        <td class="header">Sản phẩm<br />
                                                            Gợi ý</td>
                                                        <td class="header">Có thể
                                                            <br />
                                                            bạn thích</td>
                                                        <td class="header">Nổi bật</td>
                                                        <td class="header">Bán chạy</td>
                                                        <td class="header">Khuyến mãi</td>
                                                        <td class="header" colspan="3">Chức năng</td>
                                                    </tr>
                                            </HeaderTemplate>
                                            <FooterTemplate>
                                                </TABLE>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <asp:Label ID="lterr" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
                                    </div>
                                    <table border="0" cellpadding="0" style="border-collapse: collapse" width="100%">
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
                                        
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>

                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div class="widget-title">
                        <h4><i class="icon-list-alt"></i>&nbsp;Thêm mới - cập nhật</h4>
                    </div>
                    <div class="widget-body widget">
                        <div class="row-fluid">
                            <div class="span3">
                                <div class="dataTables_length" id="sample_1_length">
                                    <asp:Literal ID="ltshowiavascrip" runat="server"></asp:Literal>
                                    <asp:Label ID="lblmsg" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class='frm-add'>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 145px"></td>
                                    <td style="width: 5px"></td>
                                    <td>
                                        <asp:Label ID="lbl_msg" runat="server" Font-Bold="true" ForeColor="red"></asp:Label></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; color: #6d6d6d">Phân loại nhóm</td>
                                    <td></td>
                                    <td>
                                        <asp:DropDownList ID="ddlcategoriesdetail" runat="server" CssClass="txt_css" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; color: #6d6d6d">Thương Hiệu</td>
                                    <td></td>
                                    <td>
                                        <asp:DropDownList ID="ddlthuonghieu" runat="server" CssClass="txt_css" Width="250px">
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr style="display: none">
                                    <td style="height: 19px; color: #6d6d6d">Mầu</td>
                                    <td></td>
                                    <td>
                                        <div class="Maunhe">
                                            <asp:CheckBoxList ID="cblcat" runat="server" RepeatColumns="10"></asp:CheckBoxList>
                                        </div>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr style="display: none">
                                    <td style="height: 19px; color: #6d6d6d">Kích thước</td>
                                    <td></td>
                                    <td>
                                        <div class="Kthuoc">
                                            <asp:CheckBoxList ID="ckichthuoc" runat="server" RepeatColumns="10"></asp:CheckBoxList>
                                        </div>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; color: #6d6d6d">Mã sản phẩm
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="txtcode" runat="server" Width="250px" CssClass="txt_css"></asp:TextBox>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; color: #6d6d6d">Tiêu đề 
                                    </td>
                                    <td style="height: 10px"></td>
                                    <td style="height: 10px">
                                        <asp:TextBox ID="txtname" runat="server" Width="593px" CssClass="txt_css"></asp:TextBox>
                                    </td>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; color: #6d6d6d">Rewrite Url
                                    </td>
                                    <td style="height: 10px"></td>
                                    <td style="height: 10px">
                                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtRewriteUrl" runat="server" CssClass="txt_css" Width="700px"></asp:TextBox>
                                                <asp:Button ID="btkiemtra" Style="margin-top: -11px;" runat="server" Text="Kiểm tra" CssClass="btn btn-primary" OnClick="btkiemtra_Click" />
                                                <br />
                                                <asp:Label ID="ltshowurl" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; color: #6d6d6d">Tiêu đề Alt ảnh
                                    </td>
                                    <td style="height: 10px"></td>
                                    <td style="height: 10px">
                                        <asp:TextBox ID="txtAlt" runat="server" Width="593px" CssClass="txt_css"></asp:TextBox>
                                        <div style="font-size: 8pt; color: #ed1c24"><em>(Thẻ Tiêu đề thẻ Alt dành cho seo ảnh, nếu không điền thì sẽ mặc định là tiêu đề sản phẩm)</em></div>
                                    </td>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr style="display: none">
                                    <td style="height: 19px; color: #6d6d6d">Điểm mua hàng
                                    </td>
                                    <td style="height: 10px"></td>
                                    <td style="height: 10px">
                                        <asp:TextBox ID="txtdiemmuahang" runat="server" Width="150px" CssClass="txt_css"></asp:TextBox>
                                    </td>
                                    <td style="height: 10px"></td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; color: #6d6d6d">Hình ảnh
                                    </td>
                                    <td style="height: 18px"></td>
                                    <td style="height: 18px">

                                        <asp:TextBox ID="txtImage" runat="server" CssClass="text image"></asp:TextBox>
                                        <input id="btnImage" type="button" onclick="BrowseServer('<%=txtImage.ClientID %>','News');" value="Chọn ảnh" class="toolbar btns btn-info" />
                                        <asp:Literal ID="ltimgs" runat="server"></asp:Literal>
                                        <asp:HiddenField ID="hdimgnews" runat="server" />
                                        <div style="font-size: 8pt; color: #ed1c24"><em>(Kích thước ảnh của 2 ô: Rộng: 500px X Cao: (Tùy ý) Hoặc 500px --><b> Nếu ở chi tiết sản phẩm có zoom ảnh thì ảnh lên để to là Rộng:900px X Cao: (Tùy ý) Hoặc 900px</b>)</em></div>
                                        <div style="display: none">
                                            <div align="left" style="float: left; width: 700px">
                                                <asp:RadioButton ID="rdFromComputer" runat="server" CssClass="txt_css2" AutoPostBack="True" Checked="true" GroupName="FromType" OnCheckedChanged="rdFromComputer_CheckedChanged" Text="Từ máy tính của bạn" ValidationGroup="downloadtype" />
                                                <asp:RadioButton ID="rdFromLinks" runat="server" CssClass="txt_css2" AutoPostBack="True" GroupName="FromType" OnCheckedChanged="rdFromLinks_CheckedChanged" Text="Từ 1 liên kết" />&nbsp;&nbsp;
                                                     <asp:Button ID="btDeleteimages" runat="server" Text="Delete" OnClick="btDeleteimages_Click" Width="75px" /><br />
                                                <asp:MultiView ID="MultiView2" runat="server" ActiveViewIndex="0">
                                                    <asp:View ID="vwFromComputer" runat="server">
                                                        <asp:FileUpload CssClass="txt_css" ID="flimage" runat="server" Width="323px" />
                                                    </asp:View>
                                                    <asp:View ID="vwFromLinks" runat="server">
                                                        <asp:TextBox CssClass="txt_css" ID="txtvimg" runat="server" Width="99%"></asp:TextBox><br />
                                                    </asp:View>
                                                </asp:MultiView>
                                            </div>
                                            <div style="padding: 0px 0px 0px 0px">
                                                <div class="adaidien">
                                                    <asp:Literal ID="ltimg" runat="server"></asp:Literal>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                    <td style="height: 18px"></td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; color: #6d6d6d">Thêm nhiều ảnh
                                    </td>
                                    <td style="height: 7px"></td>
                                    <td style="height: 7px">
                                        <div>
                                            <asp:TextBox ID="txtMImage" runat="server" CssClass="text image"></asp:TextBox>
                                            <input id="btnBrowseImage" onclick="BrowseServerNew('<%=txtMImage.ClientID %>','Adv')" type="button" value="Chọn nhiều ảnh" class="toolbar btns btn-info" />
                                            <input id="btndelall" onclick="delall();" type="button" value="Xóa tất cả" class="toolbar btns btn-info" />
                                        </div>
                                        <div style="clear: both"></div>
                                        <ul id="container-img"></ul>
                                    </td>
                                    <td style="height: 7px; font-size: 12pt; font-family: Times New Roman;"></td>
                                </tr>
                                <tr>
                                <tr>
                                    <td style="height: 19px; color: #6d6d6d">Mô tả
                                    </td>
                                    <td style="height: 7px"></td>
                                    <td style="height: 7px">
                                        <CKEditor:CKEditorControl ID="txtdesc" runat="server" Toolbar="Basic"></CKEditor:CKEditorControl>
                                    </td>
                                    <td style="height: 7px; font-size: 12pt; font-family: Times New Roman;"></td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; color: #6d6d6d">Nội dung
                                    </td>
                                    <td></td>
                                    <td>
                                        <CKEditor:CKEditorControl ID="txtcontent" runat="server" Height="300px"></CKEditor:CKEditorControl>
                                    </td>
                                    <td></td>
                                </tr>

                                <tr>
                                    <td style="height: 19px; color: #6d6d6d">Giá</td>
                                    <td></td>
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                            <tr valign="top">
                                                <td style="width: 150px">Giá bán </td>
                                                <td>Giá bán cho đại lý</td>
                                                <td>Giá công ty nhập vào</td>
                                                <td>Giá niêm yết</td>
                                            </tr>
                                            <tr valign="top">
                                                <td style="width: 100px">
                                                    <asp:TextBox ID="txtprice" runat="server" Width="112px" CssClass="txt_css">0</asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtprice"></cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:TextBox ID="txtGiaThanhVien" runat="server" Width="112px" CssClass="txt_css">0</asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtGiaThanhVien"></cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:TextBox ID="txtgiacongtynhapvao" runat="server" Width="112px" CssClass="txt_css"></asp:TextBox>&nbsp;
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txtgiacongtynhapvao"></cc1:FilteredTextBoxExtender>
                                                </td>
                                                <td style="width: 100px">
                                                    <asp:TextBox ID="txtoldprice" runat="server" Width="112px" CssClass="txt_css"></asp:TextBox>&nbsp;
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtoldprice"></cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                        </table>
                                        <span style="font-size: 8pt; color: #ed1c24"><em>(Luu ý: khi điền giá không có ký tự nào hay dấu chấm phẩy nào kèm theo mà chỉ điền dãy số : Ex:12000 - sau khi điền ở ngoài sẽ hiển thị là 12,000)</em></span>
                                    </td>
                                    <td></td>
                                </tr>

                                <tr>
                                    <td>Tính năng seo
                                    </td>
                                    <td style="height: 7px"></td>
                                    <td style="height: 7px">
                                    <td></td>
                                </tr>

                                <tr>
                                    <td valign="top">Tiêu đề từ khóa (Title)
                                    </td>
                                    <td valign="top"></td>

                                    <td>
                                        <asp:TextBox ID="txttitleseo" CssClass="txt_css" runat="server" Width="320px"></asp:TextBox>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>

                                    <td valign="top">Nội dung hiển thị trong (Description)
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="txtmeta" CssClass="txt_css" runat="server" Width="392px" Height="35px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td valign="top"></td>
                                </tr>
                                <tr>
                                    <td valign="top">Nội dung hiển thị trong (Keyword)
                                    </td>
                                    <td></td>
                                    <td>
                                        <asp:TextBox ID="txtKeywordS" CssClass="txt_css" runat="server" Width="459px" Height="43px" TextMode="MultiLine"></asp:TextBox>
                                    </td>
                                    <td valign="top"></td>
                                </tr>


                                <tr style="display: none">
                                    <td>Thời gian</td>
                                    <td></td>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:CheckBox ID="chkdaytype" runat="server" Text="Hiển thị trong thời gian" AutoPostBack="True" OnCheckedChanged="chkdaytype_CheckedChanged" />
                                                <asp:Panel ID="pnadddate" Visible="false" runat="server">
                                                    Ngày đăng tin
                <br />
                                                    <asp:TextBox ID="txtfromday" runat="server" CssClass="txt" Height="22px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtfromday_CalendarExtender0" runat="server" TargetControlID="txtfromday"></cc1:CalendarExtender>
                                                    tồn tại trong
                <asp:TextBox ID="txtindays" runat="server" CssClass="txt" Width="48px">365</asp:TextBox>
                                                    ngày
                                                </asp:Panel>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <asp:CheckBox ID="CheckHome" CssClass="txt_css2" runat="server" Font-Bold="False" Text="Trang chủ" />
                                        <asp:CheckBox ID="Checknews" CssClass="txt_css2" runat="server" Font-Bold="False" Text="Sản phẩm giá tốt hôm nay" />
                                        <asp:CheckBox ID="Check_01" CssClass="txt_css2" runat="server" Font-Bold="False" Text="Sản phẩm gợi ý" />
                                        <asp:CheckBox ID="Check_02" CssClass="txt_css2" runat="server" Font-Bold="False" Text="Sản phẩm có thể bạn thích" />
                                        <asp:CheckBox ID="Check_03" CssClass="txt_css2" runat="server" Font-Bold="False" Text="Sản phẩm Nổi bật" />
                                        <asp:CheckBox ID="Check_04" CssClass="txt_css2" runat="server" Font-Bold="False" Text="Sản phẩm bán chạy" />
                                        <asp:CheckBox ID="Check_05" CssClass="txt_css2" runat="server" Font-Bold="False" Text="Sản phẩm khuyến mãi" />
                                    </td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td style="height: 19px; color: #6d6d6d">Tùy chọn
                                    </td>
                                    <td style="height: 20px"></td>
                                    <td style="height: 20px">
                                        <asp:CheckBox ID="chkstatus" CssClass="txt_css2" runat="server" Checked="True" Text="Chọn: kích hoạt" Font-Bold="False" />
                                    </td>
                                    <td style="height: 20px"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtquantity" runat="server" Visible="False" Width="14px"></asp:TextBox></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                            <asp:HiddenField ID="hdinsertupdate" runat="server" Value="insert" />
                            <asp:HiddenField ID="hdid" runat="server" />
                            <asp:HiddenField ID="hdcid" runat="server" />
                            <asp:HiddenField ID="hdipid" runat="server" />
                            <asp:HiddenField ID="hdFileName" runat="server" />
                            <asp:HiddenField ID="hdimgsmall" runat="server" />
                            <asp:HiddenField ID="hdimgMax" runat="server" />
                            <asp:HiddenField ID="hdimgMaxEdit" runat="server" />
                            <asp:HiddenField ID="hdimgsmallEdit" runat="server" />
                            <asp:HiddenField ID="hdthanhvien" runat="server" />
                        </div>

                        <div style="height: 20px;"></div>
                        <div style="padding-left: 120px;">
                            <asp:LinkButton ID="btnsave" runat="server" OnClick="btnsave_Click" CssClass="toolbar btn btn-info" Style="background: #ed1c24"> <i class="icon-save"></i>  Cập nhật </asp:LinkButton>
                            <asp:LinkButton ID="btncancel" runat="server" OnClick="btncancel_Click" CssClass="toolbar btn btn-info"> <i class="icon-chevron-left"></i>Hủy</asp:LinkButton>

                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </div>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnsave" />
        <asp:PostBackTrigger ControlID="btkiemtra" />
    </Triggers>
</asp:UpdatePanel>

<%--
<div class="my-rating5"></div>
<script>
    $(".my-rating5").starRating({
        initialRating: 4,
        strokeColor: '#894A00',
        strokeWidth: 10,
        starSize: 25,
         readOnly: true
    });
</script>
--%>
<script type="text/javascript">
    $('[id*=btnBrowseImage]').each(function () {
        $(this).click(function () {
            BrowseServerNew(<%=txtMImage.ClientID%>, '');
        });
    });
    $("#container-img").sortable({
        stop: function (event, ui) {
            $('#<%=txtMImage.ClientID%>').val(GetStringImg());
        }
    });
        function delall() {
            $("#container-img").html('') ;
            $('#<%=txtMImage.ClientID%>').val(GetStringImg());
        }
        function BrowseServerNew(functionData, startupPath) {

            var finder = new CKFinder();
            finder.basePath = '~/scripts/ckfinder/';
            finder.startupPath = startupPath;
            finder.selectActionFunction = SetFileFieldNew;
            finder.selectActionData = functionData;
            finder.popup();
        }
        function SetFileFieldNew(fileUrl, data, allFiles) {
            var str = "";
            var strimg ="";
            allFiles.forEach(function(item) {
                strimg += "<li class='ui-state-default'><div class='box-img'><a href='javascript:void(0)' onclick=\"delimg($(this),'" + data["selectActionData"] + "');\" class='btn-close'>x</a> <img src='" + item.url + "' /> </div></li>";
            })
            $("#container-img").html($("#container-img").html() + strimg);
            $("#container-img").sortable({
                stop: function (event, ui) {
                    $('#<%=txtMImage.ClientID%>').val(GetStringImg());
                }
            });
                $("#container-img").disableSelection();
                $('#'+data["selectActionData"]).val(GetStringImg());
            }
            function LoadStringImg(strImg, inputimg) {
                var arr = strImg.split(',');
                var strimg="";
                arr.forEach(function(item) {
                    strimg += "<li class='ui-state-default'><div class='box-img'><a href='javascript:void(0)' onclick=\"delimg($(this),'" + inputimg + "');\" class='btn-close'>x</a> <img src='" + item + "' /> </div></li>";
                })
                $("#container-img").html($("#container-img").html() + strimg);
                $("#container-img").sortable({
                    stop: function (event, ui) {
                        $('#<%=txtMImage.ClientID%>').val(GetStringImg());
                    }
                });
                    $("#container-img").disableSelection();
                    $('#<%=txtMImage.ClientID%>').val(GetStringImg());
            }
            function GetStringImg() {
                var str = "";
                $(".box-img img").each(function () {
                    str += $(this).attr('src') + ',';
                })
                return str;
            }
            function delimg(img, inputimg)
            {
                img.parent().parent().remove();
                $('#'+ inputimg).val(GetStringImg());
            }
</script>
<script type="text/javascript">
    var r = {
        'special': /[\W]/g,
        'quotes': /[^0-9^]/g,
        'notnumbers': /[^a-zA]/g
    }
    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
    }
    var substringMatcher = function (strs) {
        return function findMatches(q, cb) {
            var matches; matches = []; substrRegex = new RegExp(q, 'i'); $.each(strs, function (i, str)
            { if (substrRegex.test(str)) { matches.push(str); } }); cb(matches);
        };
    };
</script>
