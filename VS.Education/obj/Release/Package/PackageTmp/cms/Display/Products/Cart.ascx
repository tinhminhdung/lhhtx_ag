<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Cart.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Products.Cart" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<link href="/cms/Display/Products/Resources/Giohang.css" rel="stylesheet" type="text/css" />
<link href="/Resources/ShopCart/css/Stylecart.css" rel="stylesheet" />
<section class="col-lg-12 col-md-12">
    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>

    <asp:Panel ID="pncart" runat="server">
        <div class='frm_cart '>
            <div class="bgContent">
                <div class="borderBlock_ListPro ">
                    <asp:Literal ID="ltkickhoat" runat="server"></asp:Literal>
                            <div style="text-align: left">
                                <%=label("l_have")%> <span style="color: #F00; font-weight: bold;">
                                <asp:Literal ID="ltnumberofproducts" runat="server"></asp:Literal></span> <%=label("lproductsincart")%>
                            </div>
                     <div style=" clear:both"></div>
                    <hr />
                     <div style=" clear:both"></div>
                    <asp:Panel ID="PNMobile" runat="server">
                    <div id="cartReview "><%--Mobile--%>
                        <div class="cartItemProd cartItemProd_20961284">
                            <asp:Repeater ID="rpmobile" runat="server" OnItemCommand="rpmobile_ItemCommand">
                                <ItemTemplate>
                                    <div class="cartItemProdInfo">
                                        <div class="cotrtraigh">
                                            <a class="khoianh">
                                                <img class="anhgiohang" src="<%#Eval("Vimg")%>" alt="<%#Eval("Name")%>" title="<%#Eval("Name")%>">
                                            </a>
                                            <div class="Khoitni">
                                                <p class="tensp"><%#Eval("Name")%></p>
                                                <p class="pricesv"><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Price").ToString())%></p>
                                                <p>
                                                    <asp:HiddenField ID="hiID" Value='<%# Eval("PID") %>' runat="server" />
                                                    <asp:TextBox ID="txtxQuantity" Style="width: 40px; border: 1px solid #d7d7d7; text-align: center; margin: auto; padding: 0px;" Text='<%#DataBinder.Eval(Container.DataItem, "Quantity")%>' CssClass="txt_css" Width="40px" runat="server" OnTextChanged="txtxQuantity_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="cotphaigiohang">
                                            <p><b class="pricesv"><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Money").ToString())%></b></p>
                                            <span class="xpoaa">
                                                <asp:LinkButton ID="LinkButton4" CommandName="delete" OnLoad="Delete_Load" CssClass="spriteIcon removeCartItem fa fa-trash-o" CommandArgument='<%#Eval("PID")%>' runat="server">   </asp:LinkButton>
                                            </span>
                                        </div>
                                       <%-- <div class="dongkeee"></div>--%>
                                    </div>
                                    <div style=" clear:both"></div>
                                    <hr />
                                       <div style=" clear:both"></div>
                                </ItemTemplate>
                                <AlternatingItemTemplate>
                                    <div class="cartItemProdInfo"  style="background: #fff5ee;padding-top: 10px;">
                                        <div class="cotrtraigh">
                                            <a class="khoianh">
                                                <img class="anhgiohang" src="<%#Eval("Vimg")%>" alt="<%#Eval("Name")%>" title="<%#Eval("Name")%>">
                                            </a>
                                            <div class="Khoitni">
                                                <p class="tensp"><%#Eval("Name")%></p>
                                                <p class="pricesv"><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Price").ToString())%></p>
                                                <p>
                                                    <asp:HiddenField ID="hiID" Value='<%# Eval("PID") %>' runat="server" />
                                                    <asp:TextBox ID="txtxQuantity" Style="width: 40px; border: 1px solid #d7d7d7; text-align: center; margin: auto; padding: 0px;" Text='<%#DataBinder.Eval(Container.DataItem, "Quantity")%>' CssClass="txt_css" Width="40px" runat="server" OnTextChanged="txtxQuantity_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                </p>
                                            </div>
                                        </div>
                                        <div class="cotphaigiohang">
                                            <p><b class="pricesv"><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Money").ToString())%></b></p>
                                            <span class="xpoaa">
                                                <asp:LinkButton ID="LinkButton4" CommandName="delete" OnLoad="Delete_Load" CssClass="spriteIcon removeCartItem fa fa-trash-o" CommandArgument='<%#Eval("PID")%>' runat="server">   </asp:LinkButton>
                                            </span>
                                        </div>
                                      <%--  <div class="dongkeee"></div>--%>
                                    </div>
                                    <div style=" clear:both"></div>
                                    <hr />
                                       <div style=" clear:both"></div>
                                </AlternatingItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    </asp:Panel>
                    <asp:Panel ID="PNDestop" runat="server">
                    <div class=""><%--Destop--%>
                        <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" OnItemDataBound="ItemDataBound_RP">
                            <HeaderTemplate>
                                <table cellpadding="0" cellspacing="0" class="dcart scoll" width="100%" bgcolor="#FFFFFF" bordercolor="#C3C3C3">
                                    <tr class="procart">
                                        <td align="left"><strong>STT</strong></td>
                                        <td align="left"><strong><%=label("l_images") %></strong></td>
                                        <td align="center"><strong><%=label("lt_firstname") %></strong></td>
                                        <td align="center"><strong><%=label("lprice") %></strong></td>
                                        <td align="center"><strong><%=label("lquantity") %></strong></td>
                                        <td align="center"><strong><%=label("l_tomoney") %></strong></td>
                                        <td align="right"><strong>[<%=label("ldelete")%>]</strong></td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr align="center">
                                    <td height="30" align="center" class="TitleItem">
                                        <asp:Label ID="lb_tt" runat="server"></asp:Label></td>
                                    <td align="left" class="TitleItem">
                                        <img src="<%#Eval("Vimg")%>" style="width: 77px; height: auto; border: solid 1px #d7d7d7" />
                                    </td>
                                    <td align="center" class="TitleItem"><strong><%#Eval("Name")%></strong>
                                    <td align="center" class="TitleItem"><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Price").ToString())%> </td>
                                    <td align="center" class="TitleItem">
                                        <asp:HiddenField ID="hiID" Value='<%# Eval("PID") %>' runat="server" />
                                        <asp:TextBox ID="txtxQuantity" Style="width: 40px; border: 1px solid #d7d7d7; text-align: center; margin: auto; padding: 0px;" Text='<%#DataBinder.Eval(Container.DataItem, "Quantity")%>' CssClass="txt_css" Width="40px" runat="server" OnTextChanged="txtxQuantity_TextChanged" AutoPostBack="true"></asp:TextBox>
                                    </td>
                                    <td align="center" class="TitleItem"><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Money").ToString())%> </td>
                                    <td align="center">
                                        <asp:LinkButton ID="LinkButton4" CommandName="delete" OnLoad="Delete_Load" CssClass="lnk" CommandArgument='<%#Eval("PID")%>' runat="server"><span class="cmdxoa"></span></asp:LinkButton></td>
                                </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                <tr style="background: #fff5ee;">
                                    <td height="30" align="center" class="TitleItem">
                                        <asp:Label ID="lb_tt" runat="server"></asp:Label></td>
                                    <td align="left" class="TitleItem">
                                        <img src="<%#Eval("Vimg")%>" style="width: 77px; height: auto; border: solid 1px #d7d7d7" />
                                    </td>
                                    <td align="center" class="TitleItem"><strong><%#Eval("Name")%></strong><br />
                                    <td align="center" class="TitleItem"><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Price").ToString())%> </td>
                                    <td align="center" class="TitleItem">
                                        <asp:HiddenField ID="hiID" Value='<%# Eval("PID") %>' runat="server" />
                                        <asp:TextBox ID="txtxQuantity" Style="width: 40px; border: 1px solid #d7d7d7; text-align: center; margin: auto; padding: 0px;" Text='<%#DataBinder.Eval(Container.DataItem, "Quantity")%>' CssClass="txt_css" Width="40px" runat="server" OnTextChanged="txtxQuantity_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                                    <td align="center" class="TitleItem"><%#AllQuery.MorePro.FormatMoney_Cart(Eval("Money").ToString())%> </td>
                                    <td align="center">
                                        <asp:LinkButton ID="LinkButton4" CommandName="delete" OnLoad="Delete_Load" CssClass="lnk" CommandArgument='<%#Eval("PID")%>' runat="server"><span class="cmdxoa"></span></asp:LinkButton></td>
                                </tr>
                            </AlternatingItemTemplate>
                        </asp:Repeater>
                        </table>
                    </div>
                    </asp:Panel>
                    <table cellpadding="0" cellspacing="0" class="" width="100%" bgcolor="#FFFFFF" bordercolor="#C3C3C3" style="border: none">
                        <tr>
                            <td height="30" colspan="6" align="right" class="TitleItem"><strong style="float: right; font-weight: bold; padding-right: 10px; color: red">Số điểm cần thanh toán:</strong></td>
                            <td colspan="3" align="center" class="TitleItem"><strong style="color: red">
                                <asp:Literal ID="lttotalvnd" runat="server"></asp:Literal>
                                / Điểm
                            </strong></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <div style="white-space: 100%">
            <div class="bacoc">
                <span class="order-header" style=" color:red; font-weight:bold">THÔNG TIN ĐẶT HÀNG</span>
                <div class="maunen">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red"></asp:Label>
                    <div class='frm-contact'>
                        <div style="width: 100%">
                            <div style="float: left; width: 100%">
                                <div class="labelll">
                                    <%=label("lt_fullname")%>: <span style="color: red">(*)</span>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtName" CssClass="CSTextBox" ValidationGroup="cart" runat="server" Width="222px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="cart" ControlToValidate="txtName" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div style="float: left; width: 100%">
                                <div class="labelll">
                                    <%=label("l_address")%>: <span style="color: red">(*)</span>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtAddress" CssClass="CSTextBox" ValidationGroup="cart" runat="server" Width="222px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="cart" ControlToValidate="txtAddress" ErrorMessage="*"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div style="float: left; width: 100%">
                                <div class="labelll">
                                    Tỉnh thành: <span style="color: red">(*)</span>
                                </div>
                                <div>
                                    <asp:DropDownList ID="ddltinhthanh" class="CSTextBox" ValidationGroup="cart" Style="border-radius: 0px !important; height: 30px; border: 1px solid #d7d7d7;" runat="server"></asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ErrorMessage="*" Text="Vui lòng chọn tỉnh thành" InitialValue="0" ControlToValidate="ddltinhthanh" ValidationGroup="cart"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div style="float: left; width: 100%">
                                <div class="labelll">
                                    <%=label("l_phone")%>: <span style="color: red">(*)</span>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtPhone" CssClass="CSTextBox" ValidationGroup="cart" runat="server" Width="124px"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="cart" ControlToValidate="txtPhone" ErrorMessage="*"></asp:RequiredFieldValidator>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtPhone">
                                    </cc1:FilteredTextBoxExtender>
                                </div>
                            </div>

                            <div style="float: left; width: 100%">
                                <div class="labelll">
                                    <%=label("l_email")%>: <span style="color: red">(*)</span>
                                </div>
                                <div>
                                    <asp:TextBox ID="txtEmail" CssClass="CSTextBox" ValidationGroup="cart" runat="server" Width="222px"></asp:TextBox>
                                </div>
                            </div>
                            <div style="float: left; width: 100%">
                                <div class="labelll">
                                    <%=label("l_content")%>: <span style="color: red">(*)</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="cart" ControlToValidate="txtnoidung" ErrorMessage=" Vui lòng điền vào ô nội dung"></asp:RequiredFieldValidator>
                                </div>
                                <div style="float: left;">
                                    <asp:TextBox ID="txtnoidung" placeholder="Vui lòng ghi chi nhánh cần nhận hàng. Hoặc địa chỉ lấy giao hàng của bạn" CssClass="CSTextBox" runat="server" ValidationGroup="cart" Width="464px" Height="120px" TextMode="MultiLine"></asp:TextBox>

                                    <br />
                                    <span style="font-size: 10pt; color: #ed1c24"><em><b>Chú ý</b>: Đối với 1 số sản phẩm thì quý khách vui lòng điền nội dung theo yêu cầu của sản phẩm<br />
                                        <b>Ví dụ</b>: Thẻ điện thoại (Phải ghi rõ số điện thoại cần nạp, số trả trước hay trả sau)</em></span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div style="width: 100%; float: left">
                <asp:Button ID="btnSendOrder" ValidationGroup="cart" CssClass="btnadd dathanggiohang" runat="server" Text="Đặt hàng" OnClick="btnSendOrder_Click" />
                <asp:Button ID="btnEditCart" CssClass="btnadd" runat="server" Text="Sửa lại giỏ hàng" OnClick="btnEditCart_Click" />
                <asp:Button ID="_btctnew" CssClass="btnadd" runat="server" Text="Mua thêm" OnClick="_btctnew_Click" />
                <asp:Button ID="btnCancelOrder" CssClass="btnadd" runat="server" Text="Hủy đặt hàng" OnClick="btnCancelOrder_Click" />
            </div>
            <div style="color: #333; float: left; font: 400 14px/22px arial; padding-top: 10px; width: 98%;">
                <asp:Literal ID="ltgiohang" runat="server"></asp:Literal>
            </div>
        </div>


    </asp:Panel>
    <asp:Panel ID="pnmessage" runat="server">
        <div class="modalbodys cart_body">
            <i class="icon_cart"></i>
            <h2><%=label("giohang1")%></h2>
            <p><%=label("giohang2")%></p>
            <a class="adrbutton" href="/"><%=label("giohang3")%></a>

        </div>
        <br />
        <br />
    </asp:Panel>

    <asp:Panel ID="Panel1" runat="server" Visible="false">
        <div class="modalbodys cart_body">
            <i class="icon_cart"></i>
            <asp:Literal ID="lblKQ" runat="server"></asp:Literal>
            <br />
            <h2><%=label("giohang1")%></h2>
            <p><%=label("giohang2")%></p>
            <a class="adrbutton" href="/"><%=label("giohang3")%></a>
        </div>
        <br />
        <br />
    </asp:Panel>
    <asp:HiddenField ID="hdidthanhvien" Visible="false" runat="server" />
    <asp:HiddenField ID="hdtongtien" Visible="false" runat="server" />
    <asp:HiddenField ID="hdthanhvienFree" Visible="false" runat="server" />
    <asp:HiddenField ID="hdcuahang" Visible="false" runat="server" />
    <asp:HiddenField ID="hdChiNhanh" Visible="false" runat="server" />
    <asp:HiddenField ID="hdvivip" Visible="false" runat="server" />
    <%--    </ContentTemplate>
</asp:UpdatePanel>--%>
    <div style="clear: both"></div>
</section>

<%--<script type="text/javascript">
    $(document).ready(function () {
        $('input[type="submit"]').click(function () {
            $('#loadingAjax').addClass('loading');
            setTimeout(function () {
                $('#loadingAjax').removeClass('loading');
            }, 4000);
        });
    });
</script>--%>
<%--<div id="loadingAjax" style=" display:none">
<div class="inner"><img src="/Resources/ShopCart/images/ajax-loader_2.gif"><p>Đang xử lý ...</p></div>
</div>--%>