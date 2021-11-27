<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThanhVienDangBai.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.ThanhVienDangBai" %>
<%@ Register Assembly="FredCK.FCKeditorV2" Namespace="FredCK.FCKeditorV2" TagPrefix="FCKeditorV2" %>
<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>
<script src="/Scripts/ckfinder/ckfinder.js" type="text/javascript"></script>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<script src="/Resources/js/jquery-1.7.1.min.js"></script>
<script src="/Resources/js/jquery-ui.min_1.10.3.js"></script>
<link href="/Resources/css/Quanlydangbai.css" rel="stylesheet" />
<link type="text/css" rel="stylesheet" href="/Resources/admins/css/font-awesome.css" />
<div class=" margin-bottom-20">
    <h1 class="title-head">
        <span>Danh sách sản phẩm</span>
    </h1>
    <div class="row">
        <div class="col-lg-12">

            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
                    <div class="row-fluid">
                        <div class="span12">
                            <div class="widget">
                                <div class="widget-title">
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
                                                <div class="frm_search">
                                                    <div>
                                                        <asp:TextBox ID="txtkeyword" runat="server" CssClass="form-control timkiem"></asp:TextBox>
                                                        <asp:LinkButton ID="lnksearch" runat="server" OnClick="lnksearch_Click" CssClass="vadd toolbar btn btn-info" Style="margin-top: 2px; margin-left: 8px;"> <i class="icon-search"></i>&nbsp;Tìm kiếm</asp:LinkButton>
                                                        <asp:Label ID="ltthongbao" runat="server" Font-Bold="true" ForeColor="Red"></asp:Label>
                                                    </div>
                                                    <div style="clear: both"></div>
                                                    <div style="margin-top: 10px; width: 100%">
                                                        <asp:DropDownList ID="ddlcategories" CssClass="form-control " AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlcategories_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlstatus" runat="server" AutoPostBack="true" CssClass="form-control " OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                                            <asp:ListItem Value="-1" Selected="True">Tất cả các mục</asp:ListItem>
                                                            <asp:ListItem Value="1">Hiển thị</asp:ListItem>
                                                            <asp:ListItem Value="0">Ẩn</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlorderby" runat="server" AutoPostBack="true" CssClass="form-control " OnSelectedIndexChanged="ddlorderby_SelectedIndexChanged">
                                                            <asp:ListItem Selected="True" Value="Create_Date">S.xếp:Ngày cập nhật</asp:ListItem>
                                                            <asp:ListItem Value="Price">S.xếp:Theo giá hiện tại</asp:ListItem>
                                                            <asp:ListItem Value="Views">S.xếp:Lần xem</asp:ListItem>
                                                            <asp:ListItem Value="Name">S.xếp:Tiêu đề (ABC)</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:DropDownList ID="ddlordertype" runat="server" AutoPostBack="True" CssClass="form-control " OnSelectedIndexChanged="ddlordertype_SelectedIndexChanged">
                                                            <asp:ListItem Value="desc">Giảm dần</asp:ListItem>
                                                            <asp:ListItem Value="asc">Tăng dần</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="span3">
                                            <div class="dataTables_filter" id="sample_1_filter">
                                                <asp:LinkButton ID="bthienthi" runat="server" OnClick="bthienthi_Click" CssClass="vadd toolbar btn btn-info"> <i class=" icon-folder-close"></i>&nbsp;Hiện thị</asp:LinkButton>
                                                <asp:LinkButton ID="btthemmoi" runat="server" Text="Thêm mới" OnClick="btthemmoi_Click" CssClass="vadd toolbar btn btn-info"><i class="icon-plus"></i>&nbsp;Thêm mới</asp:LinkButton>
                                                <asp:LinkButton ID="btDeleteall" ToolTip="Xóa những lựa chọn !" OnClientClick=" return confirmDelete(this);" runat="server" OnClick="btDeleteall_Click" CssClass="vadd toolbar btn btn-info"><i class="icon-trash"></i>&nbsp;Xóa</asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                    <div style="clear: both; height: 20px"></div>
                                    <div class="table-responsive tab-all" style="overflow-x: auto;">
                                        <div class="list_item">

                                            <asp:Repeater ID="rpitems" runat="server" OnItemCommand="rpitems_ItemCommand1" OnItemDataBound="rp_pagelist_ItemDataBound">
                                                <ItemTemplate>
                                                    <tr height="40">
                                                        <td style="text-align: center;">
                                                            <asp:CheckBox ID="chkid" runat="server" onclick="javascript:changeColor(this,'white');" /><asp:HiddenField ID="hiID" Value='<%# Eval("ipid") %>' runat="server" />
                                                        </td>
                                                        <%-- <td style="text-align: center;" class="Imgsmall">
                                                        <div style="position: relative;">
                                                            <span class="anhn"><a title="<%#Eval("Name")%>" target="_blank" href="/<%#Eval("TangName")%>.html"><%#MoreAll.MoreImage.Image(Eval("Images").ToString())%></a></span>
                                                        </div>
                                                    </td>--%>
                                                        <td>
                                                            <a title="<%#Eval("Name")%>" target="_blank" href="/<%#Eval("TangName")%>_sp<%#Eval("ipid")%>.html"><%#DataBinder.Eval(Container.DataItem, "Name")%> <span class="xemnhanh">Click Xem nhanh</span></a>
                                                            <%#MoreAll.MoreAll.TrangThaiAgLang(DataBinder.Eval(Container.DataItem, "TrangThaiAgLang").ToString())%><br />
                                                              <%-- <asp:TextBox ID="txtTieude" Style="border: 1px solid #d7d7d7; border-radius: 3px; text-align: left; width:95%; height: 50px" TextMode="MultiLine" Text='<%#DataBinder.Eval(Container.DataItem, "Name")%>' CssClass="form-control" runat="server" OnTextChanged="txtTieude_TextChanged" AutoPostBack="true"></asp:TextBox>--%>
                                                       
                                                <br />    
                                                <asp:DropDownList ID="ddlNhom"  CssClass="form-control " runat="server" OnSelectedIndexChanged="ddlNhom_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                                <asp:HiddenField ID="hdNhom" Value='<%#Eval("icid") %>' runat="server" />
                                                <asp:Label ID="lblicid" runat="server" Visible="false" Text='<%# Eval("icid").ToString() %>'></asp:Label>
                                                        </td>
                                                        <%--<td style="text-align: center; display:none">
                                                        <div><span style=" width:25px; color:red">Giá Bán:</span><asp:TextBox ID="txtgiaban" Style="width:100%;text-align: center;"  Text='<%#AllQuery.MorePro.FormatMoney_Cart(Eval("Price").ToString())%>' onkeyup="valid(this,'quotes')" onblur="valid(this,'quotes')" CssClass="form-control " runat="server" OnTextChanged="txtgiaban_TextChanged" AutoPostBack="true"></asp:TextBox></div>
                                                        <div><span style=" width:25px;color:#228df2">Giá thành viên:</span><asp:TextBox ID="txtgiabanthanhvien" Style="width:100%;text-align: center;"  Text='<%#AllQuery.MorePro.FormatMoney_Cart(Eval("GiaThanhVien").ToString())%>' onkeyup="valid(this,'quotes')" onblur="valid(this,'quotes')" CssClass="txt_css" runat="server" OnTextChanged="txtgiabanthanhvien_TextChanged" AutoPostBack="true"></asp:TextBox></div>
                                                    <div style=" margin-bottom:4px;"><span style=" width:25px;">Giá Cũ:</span><asp:TextBox ID="txtgiacu" Style="width:100%;text-align: center;" Text='<%#AllQuery.MorePro.FormatMoney_Cart(Eval("OldPrice").ToString())%>' onkeyup="valid(this,'quotes')" onblur="valid(this,'quotes')" CssClass="form-control " runat="server" OnTextChanged="txtgiacu_TextChanged" AutoPostBack="true"></asp:TextBox></div>
                                                    </td>
                                                        --%>
                                                        <td style="text-align: center;">
                                                            <div><span style="width: 25px; color: red">Giá NY:</span><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Price").ToString())%></div>
                                                          <%--  <div><span style="width: 25px; color: #228df2">Đại lý:</span><%#AllQuery.MorePro.FormatMoney_Cart(Eval("GiaThanhVien").ToString())%></div>--%>
                                                            <%--<div><span style="width: 25px;">Thành viên Free :</span><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Giacongtynhapvao").ToString())%></div>--%>
                                                            <div><span style="width: 25px;">Giá bán cho Ag :</span><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Giacongtynhapvao").ToString())%></div>
                                                           <%-- <div><a style="color: red; font-weight: bold;" href="/quan-ly-gia-dai-ly/<%#Eval("ipid")%>"><i style="font-size: 18px; color: red;" class="icon-plus"></i> Thêm giá bán đại lý</a></div>--%>
                                                        </td>
                                                        <td style="text-align: center;">
                                                            <%#(Commond.DiemTichLuyAdd(DataBinder.Eval(Container.DataItem,"GiaThanhVien").ToString(),DataBinder.Eval(Container.DataItem,"Giacongtynhapvao").ToString()))%>
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
<%--                                                        <td style="text-align: center;">
                                                            <asp:LinkButton ID="LinkButton5" CssClass="active action-link-button" CommandName="updat_date" CommandArgument='<%#Eval("ipid") %>' runat="server"><i class=" icon-refresh"></i></asp:LinkButton>
                                                        </td>--%>
                                                        <td style="text-align: center; display: none">
                                                            <asp:TextBox ID="TextBox1" Text='<%#DataBinder.Eval(Container.DataItem, "Quantity")%>' CssClass="txt_css" Width="30px" runat="server" OnTextChanged="txtxQuantity_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                                        <td style="text-align: center;">
                                                            <%#MoreAll.MoreAll.DuyetBaiTV(DataBinder.Eval(Container.DataItem, "Status").ToString())%><br /><br />
                                                            <%--<asp:LinkButton CommandName="ChangeStatus" CssClass="active action-link-button" CommandArgument='<%#Eval("ipid")+"|"+Eval("Status")%>' runat="server" ID="Linkbutton3"><%#MoreAll.MoreAll.Enable(DataBinder.Eval(Container.DataItem, "Status").ToString())%></asp:LinkButton>--%>
                                                            <asp:LinkButton ID="LinkButton1" CssClass="active action-link-button" CommandName="update" CommandArgument='<%#Eval("ipid") %>' runat="server"><i class="icon-edit"></i></asp:LinkButton>
                                                            <asp:LinkButton CssClass="active action-link-button" OnLoad="Delete_Load" CommandName="delete" CommandArgument='<%#Eval("ipid") %>' ID="LinkButton2" runat="server"><i class="icon-trash"></i></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <table cellspacing="0" style="border-collapse: collapse; margin-top: 18px" class="table table-striped table-bordered dataTable table-hover">
                                                        <tr class="trHeader" style="height: 25px">
                                                            <td class="header" style="width: 60px; text-align: center;">
                                                                <input id="Checkbox1" onclick="javascript:SelectAllCheckboxes(this,1);" type="checkbox" /></td>
                                                            <%-- <td class="header" style="text-align:center">Hình ảnh</td>--%>
                                                            <td class="header" style="width: 200px; text-align: center">Tên sản phẩm</td>
                                                            <td class="header" style="width: 180px; text-align: center">Giá</td>
                                                            <td class="header" style="width: 100px; text-align: center">Điểm thưởng</td>
                                                            <td class="header" style="text-align: center">Ngày tạo</td>
                                                            <td class="header" style="text-align: center">Xem</td>
                                                          <%--  <td class="header" style="text-align: center">Làm mới</td>--%>
                                                            <td class="header" colspan="3" style="text-align: center">Chức năng</td>
                                                        </tr>
                                                </HeaderTemplate>
                                                <FooterTemplate>
                                                    </TABLE>
                                                </FooterTemplate>
                                            </asp:Repeater>

                                            <asp:Label ID="lterr" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
                                        </div>
                                    </div>
                                    <div class="pager">
                                        <asp:Literal ID="ltpage" runat="server"></asp:Literal>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                </asp:View>
                <asp:View ID="View2" runat="server">
                    <div class="widget-title">
                        <h4 style=" color:red"><i class="icon-list-alt"></i>&nbsp; THÊM MỚI CẬP NHẬT SẢN PHẨM</h4>
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
                            <asp:Label ID="lbl_msg" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>

                            <div class="frm_SanPham">
                                <div class="cottongfrom">
                                    <div class="Tieudesp">Phân loại nhóm <span style="color:red">(*)</span></div>
                                    <div class="froms">
                                        <asp:DropDownList ID="ddlcategoriesdetail"  runat="server" CssClass="form-control" Width="250px">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="cottongfrom" style=" display:none">
                                    <div class="Tieudesp">Sản phẩm thuộc <span style="color:red">(*)</span></div>
                                    <div class="froms">
                                          <asp:DropDownList ID="ddlsanphanthuoc" ValidationGroup="GInfo" runat="server" CssClass="form-control" Width="250px">
                                         <%--   <asp:ListItem Value="0" Selected="True">== Lựa chọn kiểu sản phẩm ==</asp:ListItem>--%>
                                            <asp:ListItem Value="1" Selected="True">Kiểu Sản Phẩm</asp:ListItem>
                                            <asp:ListItem Value="2">Kiểu Bất Động Sản</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator21"  runat="server" ErrorMessage="*" Text="Vui lòng chọn kiểu sản phẩm" InitialValue="0" ControlToValidate="ddlsanphanthuoc" ValidationGroup="GInfo"></asp:RequiredFieldValidator> 
                                    </div>
                                </div>
                               <%-- <div class="cottongfrom">
                                    <div class="Tieudesp">Thương Hiệu</div>
                                    <div class="froms">
                                        <asp:DropDownList ID="ddlthuonghieu" runat="server" CssClass="form-control" Width="250px">
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                                <div class="cottongfrom">
                                    <div class="Tieudesp">Mã vạch sản phẩm</div>
                                    <div class="froms">
                                        <asp:TextBox ID="txtcode" ValidationGroup="GInfo" runat="server" Width="250px" CssClass="txt_css"></asp:TextBox>
                                         <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" SetFocusOnError="true"  runat="server" ValidationGroup="GInfo" ControlToValidate="txtcode" ErrorMessage="Vui lòng điền mã sản phẩm !"></asp:RequiredFieldValidator>--%>
                                        <br /><div style="font-size: 8pt; color: #ed1c24; width:100%;float:left"><em>(Đối với sản phẩm có mã vạch quý khách vui lòng nhập đầy đủ mã vạch in trên bao bì vào ô.)</em></div>

                                    </div>
                                </div>
                                <div class="cottongfrom">
                                    <div class="Tieudesp">Tiêu đề <span style="color:red">(*)</span></div>
                                    <div class="froms">
                                        <asp:TextBox ID="txtname" ValidationGroup="GInfo" runat="server" Width="593px" CssClass="txt_css"></asp:TextBox>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator2" SetFocusOnError="true"  runat="server" ValidationGroup="GInfo" ControlToValidate="txtname" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="cottongfrom">
                                    <div class="Tieudesp">Tiêu đề Alt ảnh</div>
                                    <div class="froms">
                                        <asp:TextBox ID="txtAlt"  runat="server" Width="593px" CssClass="txt_css"></asp:TextBox>
                                          <div style="clear: both"></div>
                                        <div style="font-size: 8pt; color: #9c9c9c; width:100%"><em>(Thẻ Tiêu đề thẻ Alt dành cho seo ảnh, nếu không điền thì sẽ mặc định là tiêu đề sản phẩm)</em></div>
                                    </div>
                                </div>
                                <div class="cottongfrom">
                                    <div class="Tieudesp">Ảnh đại diện <span style="color:red">(*)</span></div>
                                    <div class="froms">
                                        <asp:TextBox ID="txtImage" ValidationGroup="GInfo" runat="server" CssClass="text image"></asp:TextBox>
                                        <input id="btnImage" type="button" style="color: #fff; text-transform:uppercase" onclick="BrowseServer('<%=txtImage.ClientID %>','News');" value="Chọn ảnh" class="toolbar btns btn-info" />
                                        <asp:Literal ID="ltimgs" runat="server"></asp:Literal>
                                        <asp:HiddenField ID="hdimgnews" runat="server" />
                                          <div style="clear: both"></div>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator3" SetFocusOnError="true"  runat="server" ValidationGroup="GInfo" ControlToValidate="txtImage" ErrorMessage="Vui lòng chọn ảnh đại diện cho sản phẩm!"></asp:RequiredFieldValidator>
                                        <div style="font-size: 8pt; color: #9c9c9c; width:100%"><em>(Kích thước ảnh của 2 ô: Rộng: 500px X Cao: (Tùy ý) Hoặc 500px
                                            <br /><b> Nếu ở chi tiết sản phẩm có zoom ảnh thì ảnh lên để to là Rộng:900px X Cao: (Tùy ý) Hoặc 900px</b>)

                                          </em></div>
                                    </div>
                                </div>
                                <div class="cottongfrom">
                                    <div class="Tieudesp">Thêm nhiều ảnh
                                    </div>
                                    <div class="froms">
                                        <asp:TextBox ID="txtMImage" runat="server" CssClass="text image"></asp:TextBox>
                                        <input id="btnBrowseImage" style="color: #fff; text-transform:uppercase" onclick="BrowseServerNew('<%=txtMImage.ClientID %>','Adv')" type="button" value="Chọn nhiều ảnh" class="toolbar btns btn-info" />
                                        <input id="btndelall" onclick="delall();"  style="color: #fff; text-transform:uppercase" type="button" value="Xóa tất cả" class="toolbar btns btn-info" />
                                        <div style="clear: both"></div>
                                         <div style="font-size: 8pt; color: #9c9c9c; width:100%"><em>(Sẽ hiển thị ở chi tiết sản phẩm)</em></div>
                                         <div style="clear: both"></div>
                                        <ul id="container-img"></ul>
                                    </div>
                                </div>

                                 <div class="cottongfrom">
                <div class="Tieudesp">
                    Thêm giấy tờ liên quan
                </div>
                <div class="froms">
                    <asp:TextBox ID="txtMImageSS" runat="server" CssClass="text image"></asp:TextBox>
                    <input id="btnBrowseImageSS" style="color: #fff; text-transform: uppercase" onclick="BrowseServerNewSS('<%=txtMImageSS.ClientID %>','Adv')" type="button" value="Thêm nhiều ảnh pháp lý (Nếu có)" class="toolbar btns btn-info" />
                    <input id="btndelallSS" onclick="delallSS();" style="color: #fff; text-transform: uppercase" type="button" value="Xóa tất cả" class="toolbar btns btn-info" />
                    <div style="clear: both"></div>
                    <div style="font-size: 8pt; color: red; width: 100%"><em>(Vui lòng đính kèm theo các giấy tờ sau: Giấy công bố sản phẩm, giấy an toàn vệ sinh thực phẩm, giấy tờ liên quan (Nếu có))</em></div>
                     <div style="clear: both"></div>
                    <ul id="container-imgSS"></ul>
                </div>
            </div>
                                <div class="cottongfrom">
                                    <div class="Tieudesp">Mô tả <span style="color:red">(*)</span></div>
                                    <div class="froms">
                                        <CKEditor:CKEditorControl ID="txtdesc" ValidationGroup="GInfo" runat="server" Toolbar="Basic"></CKEditor:CKEditorControl>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" SetFocusOnError="true"  runat="server" ValidationGroup="GInfo" ControlToValidate="txtdesc" ErrorMessage="Vui lòng điền mô tả cho sản phẩm!"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="cottongfrom">
                                    <div class="Tieudesp">Nội dung <span style="color:red">(*)</span></div>
                                    <div class="froms">
                                        <CKEditor:CKEditorControl ID="txtcontent"  ValidationGroup="GInfo" runat="server" Height="300px"></CKEditor:CKEditorControl>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator5" SetFocusOnError="true"  runat="server" ValidationGroup="GInfo" ControlToValidate="txtImage" ErrorMessage="Vui lòng điền nội dung cho sản phẩm!"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                 <div class="cottongfrom">
                                    <div class="Tieudesp">Chọn <span style="color:red">(*)</span></div>
                                    <div class="froms">
                                        <asp:DropDownList ID="ddlchon" size="10" style="height:120px !important" ValidationGroup="GInfo" runat="server">
                                            <asp:ListItem Value="0">== Chọn thông tin ==</asp:ListItem>
                                            <asp:ListItem Value="1">Tôi là nhà sản xuất</asp:ListItem>
                                             <asp:ListItem Value="2">Tôi nhà phân phối</asp:ListItem>
                                             <%--<asp:ListItem Value="3">Tôi là đại lý</asp:ListItem>--%>
                                             <asp:ListItem Value="4">Tôi là đối tượng khác</asp:ListItem>
                                        </asp:DropDownList>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator10"  runat="server" ErrorMessage="*" Text="Vui lòng chọn thông tin" InitialValue="0" ControlToValidate="ddlchon" ValidationGroup="GInfo"></asp:RequiredFieldValidator> 
                                    </div>
                                </div>
                                  <div class="cottongfrom">
                                    <div class="Tieudesp">Chọn</div>
                                    <div class="froms">
                                         <asp:CheckBox ID="checkAg" CssClass="txt_css2" runat="server" Font-Bold="False" Text="Sản phẩm tương tự này được bán trên Ag chưa?" />
                                    </div>
                                </div>
                                  <div class="cottongfrom">
                                    <div class="Tieudesp">Trọng lượng <span style="color:red">(*)</span></div>
                                    <div class="froms">
                                        <asp:TextBox ID="txttrongluong" runat="server" ValidationGroup="GInfo" Width="200px" CssClass="txt_css">0</asp:TextBox>  gram 
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" SetFocusOnError="true"  runat="server" ValidationGroup="GInfo" ControlToValidate="txttrongluong" ErrorMessage="Vui lòng điền trọng lượng Gram !"></asp:RequiredFieldValidator>
                                          </div>
                                </div>

                                <div class="cottongfrom">
                                    <div class="Tieudesp">Giá Niêm yết <span style="color:red">(*)</span></div>
                                    <div class="froms">
                                        <asp:TextBox ID="txtprice" runat="server" ValidationGroup="GInfo" Width="200px" CssClass="txt_css">0</asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtprice"></cc1:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" SetFocusOnError="true"  runat="server" ValidationGroup="GInfo" ControlToValidate="txtprice" ErrorMessage="Vui lòng điền giá bán cho sản phẩm!"></asp:RequiredFieldValidator>
                                          </div>
                                </div>
                            <%--    <div class="cottongfrom">
                                    <div class="Tieudesp">Giá bán cho đại lý <span style="color:red">(*)</span></div>
                                    <div class="froms">
                                        <asp:TextBox ID="txtGiaThanhVien" runat="server" ValidationGroup="GInfo" Width="200px" CssClass="txt_css">0</asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtGiaThanhVien"></cc1:FilteredTextBoxExtender>
                                      <asp:RequiredFieldValidator ID="RequiredFieldValidator7" SetFocusOnError="true"  runat="server" ValidationGroup="GInfo" ControlToValidate="txtGiaThanhVien" ErrorMessage="Vui lòng điền giá bán cho đại lý cho sản phẩm!"></asp:RequiredFieldValidator>
                                    </div>
                                </div>--%>
                                <div class="cottongfrom">
                                    <div class="Tieudesp">Giá bán cho Ag  <span style="color:red">(*)</span></div>
                                    <div class="froms">
                                        <asp:TextBox ID="txtgiacongtynhapvao" runat="server" ValidationGroup="GInfo" Width="200px" CssClass="txt_css"></asp:TextBox>&nbsp;
                                          <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="txtgiacongtynhapvao"></cc1:FilteredTextBoxExtender>
                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator8" SetFocusOnError="true"  runat="server" ValidationGroup="GInfo" ControlToValidate="txtgiacongtynhapvao" ErrorMessage="Vui lòng điền giá công ty nhập vào cho sản phẩm!"></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                
                                 <div style="display: none">
                                <div class="cottongfrom">
                                    <div class="Tieudesp" style=" color:red">Tính năng seo</div>
                                    <div class="froms">
                                    </div>
                                </div>
                                <div class="cottongfrom">
                                    <div class="Tieudesp">Tiêu đề từ khóa (Title)</div>
                                    <div class="froms">
                                        <asp:TextBox ID="txttitleseo" CssClass="txt_css" runat="server" Width="320px"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="cottongfrom">
                                    <div class="Tieudesp">Nội dung hiển thị trong (Description)</div>
                                    <div class="froms">
                                        <asp:TextBox ID="txtmeta" CssClass="txt_css" runat="server" Width="392px" Height="35px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="cottongfrom">
                                    <div class="Tieudesp">Nội dung hiển thị trong (Keyword)</div>
                                    <div class="froms">
                                        <asp:TextBox ID="txtKeywordS" CssClass="txt_css" runat="server" Width="459px" Height="43px" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                 </div>
                            </div>
                            <div style="display: none">
                              Rewrite Url
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtRewriteUrl" runat="server" CssClass="txt_css" Width="700px"></asp:TextBox>
                                            <asp:Button ID="btkiemtra" Style="margin-top: -11px;" runat="server" Text="Kiểm tra" CssClass="btn btn-primary" OnClick="btkiemtra_Click" />
                                            <br />
                                            <asp:Label ID="ltshowurl" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                              
                            </div>
                            
       
                            <asp:TextBox ID="txtquantity" runat="server" Visible="False" Width="14px"></asp:TextBox></td>
                            <asp:HiddenField ID="hdinsertupdate" runat="server" Value="insert" />
                            <asp:HiddenField ID="hdid" runat="server" />
                            <asp:HiddenField ID="hdcid" runat="server" />
                            <asp:HiddenField ID="hdipid" runat="server" />
                            <asp:HiddenField ID="hdFileName" runat="server" />
                            <asp:HiddenField ID="hdimgsmall" runat="server" />
                            <asp:HiddenField ID="hdimgMax" runat="server" />
                            <asp:HiddenField ID="hdimgMaxEdit" runat="server" />
                            <asp:HiddenField ID="hdimgsmallEdit" runat="server" />
                            <asp:HiddenField ID="hdthanhvien" Value="0" runat="server" />
                            <asp:HiddenField ID="hdGiaThanhVienFree" Value="0" runat="server" />
                            
                        </div>
                        <div style="height: 20px;"></div>
                        <div class="nutsukiensp">
                            <asp:LinkButton ID="btnsave" ValidationGroup="GInfo" runat="server" OnClick="btnsave_Click" CssClass="toolbar btn btn-info" Style="background: #ed1c24"> <i class="icon-save"></i>  Cập nhật </asp:LinkButton>
                            <asp:LinkButton ID="btncancel" runat="server" OnClick="btncancel_Click" CssClass="toolbar btn btn-info"> <i class="icon-chevron-left"></i>Hủy</asp:LinkButton>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>

            <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnsave" />
    </Triggers>
</asp:UpdatePanel>--%>

            <%--<script type="text/javascript">
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
            debugger;
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
                debugger;
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
</script>--%>

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
                     debugger;
                     var str = "";
                     var strimg ="";
                     allFiles.forEach(function(item) {
                         strimg += "<li class='ui-state-default'><div class='box-img'><a href='javascript:void(0)' onclick=\"delimg($(this),'<%=txtMImage.ClientID%>');\" class='btn-close'>x</a> <img src='" + item.url + "' /> </div></li>";
                     })
                     $("#container-img").html($("#container-img").html() + strimg);
                     $("#container-img").sortable({
                         stop: function (event, ui) {
                             $('#<%=txtMImage.ClientID%>').val(GetStringImg());
                            }
                        });
                            $("#container-img").disableSelection();
                            $('#<%=txtMImage.ClientID%>').val(GetStringImg());
                        //  $('#'+data["selectActionData"]).val(GetStringImg());
                    }
                    function LoadStringImg(strImg, inputimg) {
                        debugger;
                        var arr = strImg.split(',');
                        var strimg="";
                        arr.forEach(function(item) {
                            strimg += "<li class='ui-state-default'><div class='box-img'><a href='javascript:void(0)' onclick=\"delimg($(this),'<%=txtMImage.ClientID%>');\" class='btn-close'>x</a> <img src='" + item + "' /> </div></li>";
                        })
                        $("#container-img").html($("#container-img").html() + strimg);
                        $("#container-img").sortable({
                            stop: function (event, ui) {
                                $('#<%=txtMImage.ClientID%>').val(GetStringImg());
                    }
                });
                    $("#container-img").disableSelection();
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
                 $('[id*=btnBrowseImageSS]').each(function () {
                     $(this).click(function () {
                         BrowseServerNewSS(<%=txtMImageSS.ClientID%>, '');
                    });
                });
                $("#container-imgSS").sortable({
                    stop: function (event, ui) {
                        $('#<%=txtMImageSS.ClientID%>').val(GetStringImgSS());
                    }
                });
                    function delallSS() {
                        $("#container-imgSS").html('') ;
                        $('#<%=txtMImageSS.ClientID%>').val(GetStringImgSS());
                 }
                 function BrowseServerNewSS(functionData, startupPath) {
                     var finder = new CKFinder();
                     finder.basePath = '~/scripts/ckfinder/';
                     finder.startupPath = startupPath;
                     finder.selectActionFunction = SetFileFieldNewSS;
                     finder.selectActionData = functionData;
                     finder.popup();
                 }
                 function SetFileFieldNewSS(fileUrl, data, allFiles) {
                     debugger;
                     var str = "";
                     var strimg ="";
                     allFiles.forEach(function(item) {
                         strimg += "<li class='ui-state-default'><div class='box-imgSS'><a href='javascript:void(0)' onclick=\"delimgSS($(this),'<%=txtMImageSS.ClientID%>');\" class='btn-close'>x</a> <img src='" + item.url + "' /> </div></li>";
                     })
                     $("#container-imgSS").html($("#container-imgSS").html() + strimg);
                     $("#container-imgSS").sortable({
                         stop: function (event, ui) {
                             $('#<%=txtMImageSS.ClientID%>').val(GetStringImgSS());
                            }
                        });
                            $("#container-imgSS").disableSelection();
                            $('#<%=txtMImageSS.ClientID%>').val(GetStringImgSS());
                        //  $('#'+data["selectActionData"]).val(GetStringImgSS());
                    }
                    function LoadStringImgSS(strImg, inputimg) {
                        debugger;
                        var arr = strImg.split(',');
                        var strimg="";
                        arr.forEach(function(item) {
                            strimg += "<li class='ui-state-default'><div class='box-imgSS'><a href='javascript:void(0)' onclick=\"delimgSS($(this),'<%=txtMImageSS.ClientID%>');\" class='btn-close'>x</a> <img src='" + item + "' /> </div></li>";
                        })
                        $("#container-imgSS").html($("#container-imgSS").html() + strimg);
                        $("#container-imgSS").sortable({
                            stop: function (event, ui) {
                                $('#<%=txtMImageSS.ClientID%>').val(GetStringImgSS());
                    }
                });
                    $("#container-imgSS").disableSelection();
                }
                 function GetStringImgSS() {
                     debugger;
                    var str = "";
                    $(".box-imgSS img").each(function () {
                        str += $(this).attr('src') + ',';
                    })
                    return str;
                }
                function delimgSS(img, inputimg)
                {
                    img.parent().parent().remove();
                    $('#'+ inputimg).val(GetStringImgSS());
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

        </div>
    </div>
</div>
<style>
    .frm-add table tr td {
        border: none;
    }
    .xemnhanh { text-align: center; background: #00a9d2; padding-right: 4px; margin-left: 5px; color: #fff; border-radius: 3px; display: inline-block; margin-bottom: 2px; }
</style>

     <asp:Literal ID="ltanh1" runat="server"></asp:Literal>
     <asp:Literal ID="ltanh2" runat="server"></asp:Literal>