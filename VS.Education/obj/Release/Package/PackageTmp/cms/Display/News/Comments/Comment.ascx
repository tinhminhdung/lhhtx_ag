<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Comment.ascx.cs" Inherits="VS.E_Commerce.cms.Display.News.Comments.Comment" %>

<asp:Panel ID="Panel1" Visible=false runat="server">
<asp:UpdatePanel ID="UpdatePanel1"  runat="server">
    <ContentTemplate>
        <asp:Panel ID="pnList" runat="server">
            <div class="mdlitcmt" style="text-align: left;">
            <table cellpadding="0" cellspacing="0" style="border: none; width: 100%;">
                <tr>
                    <td style="width: 175px; text-align: left">
                        <span class="cmctlt">Phản hồi </span><span class="ctotal">(<asp:Literal ID="lttotal1" runat="server"></asp:Literal>)</span>
                    </td>
                    <td style="text-align: right">
                    </td>
                </tr>
            </table>
            <asp:Repeater ID="rpitems" runat="server">
                <ItemTemplate>
                    <div class="comment_item">
                        <div class="comment_left">
                             <div class="user">
                                    <%#Eval("Name") %>
                             </div>
                             <div class="time">(<%#FormatDate(Eval("Create_Date"))%>)</div>
                        </div>
                        <div class="comment_right">
                            <div class="ttl">
                                <%#Eval("Title") %></div>
                            <div class="dsc">
                                <%#Eval("Contens")%></div>
                        </div>
                        <div style="clear:both"></div>
                    </div>
                </ItemTemplate>
                <FooterTemplate>
                    <div style="height:10px"></div>
                </FooterTemplate>
            </asp:Repeater>
            
            <table cellpadding="0" cellspacing="0" style="border: none; width: 100%">
                <tr>
                    <td>
                        Hiển thị
                        <asp:Literal ID="ltviewitems" runat="server"></asp:Literal>&nbsp;trong&nbsp;
                        <asp:Literal ID="lttotal2" runat="server"></asp:Literal>&nbsp;phản hồi &nbsp;
                        <asp:LinkButton   ID="lnkviewmorecomments" CssClass="morecomment" runat="server" OnClick="LinkButton1_Click">Xem thêm phản hồi</asp:LinkButton>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdnumofitems" runat="server" Value="10" />
            <asp:HiddenField ID="hdCommentSend" runat="server" />
            <asp:HiddenField ID="hdCommentCheck" runat="server" />
            <br />
            <br />
        </div>
            </asp:Panel>
        <asp:Panel ID="pnComment" runat="server">
            <div class="dlTitle">Viết phản hồi</div>
             <table class="all" cellpadding="0" cellspacing="0" border="0px" width="85%">
                                                <tr>
                                                    <td></td>
                                                    <td style="width:2px"></td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <span style="color: Red">
                                                            <asp:Literal ID="ltmsg" runat="server"></asp:Literal></span>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td style="width:2px; height:5px"></td>
                                                    <td></td>
                                                </tr>                                      
                                                <tr style="vertical-align:bottom">
                                                    <td style="width:250px">
                                                        <asp:TextBox ID="txtfullname" runat="server" CssClass="adword-textbox" Width="250px" value="Họ tên" onblur="if(this.value==''){ this.value='Họ tên'; this.className = 'adword-textbox'}" onfocus="if(this.value=='Họ tên'){ this.value = ''; this.className = 'adword-textbox2'}"></asp:TextBox>
                                                    </td>
                                                    <td style="width:2px"></td>
                                                    <td style="text-align:left"><asp:TextBox ID="txtemail" runat="server" CssClass="adword-textbox" Width="208px" value="Email" onblur="if(this.value==''){ this.value='Email'; this.className = 'adword-textbox'}" onfocus="if(this.value=='Email'){ this.value = ''; this.className = 'adword-textbox2'}"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" class="spacertd"></td>
                                                </tr>
                                                <tr style="vertical-align:bottom">
                                                    <td>
                                                 <asp:TextBox ID="txtcommenttitle" runat="server" CssClass="adword-textbox" 
                                                            Width="250px" value="Tiêu đề" 
                                                            onblur="if(this.value==''){ this.value='Tiêu đề'; this.className = 'adword-textbox'}" 
                                                            onfocus="if(this.value=='Tiêu đề'){ this.value = ''; this.className = 'adword-textbox2'}"></asp:TextBox>
                                                    </td>
                                                    <td style="width:2px"></td>
                                                    <td style="text-align:left">
                                                    <asp:TextBox ID="txtcaptcha" runat="server" CssClass="adword-textbox" Width="80px" value="Mã xác nhận" onblur="if(this.value==''){ this.value='Mã xác nhận'; this.className = 'adword-textbox'}" onfocus="if(this.value=='Mã xác nhận'){ this.value = ''; this.className = 'adword-textbox2'}"></asp:TextBox>
                                                    <asp:Image ID="captchaImage" runat="server" Height="20px" Width="84px" style="vertical-align:bottom"/>
                                                     </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" class="spacertd"></td>
                                                </tr>
                                                <tr style="vertical-align: top">
                                                    <td colspan="3">
                                                         <asp:TextBox ID="txtcommentcontent" runat="server" CssClass="txt1" TextMode="MultiLine"
                                                            Height="150px" Width="468px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                  <tr>
                                                    <td colspan="3" class="spacertd"></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:Button ID="btnsend" runat="server" Text="Gửi phản hồi" CssClass="btn" OnClick="btnsend_Click" Width="100px" />
                                                        <asp:Button ID="btncancel" runat="server" CssClass="btn" Text="Hủy bỏ" OnClick="btncancel_Click" Width="70px" />
                                                    </td>
                                                </tr>
                                            </table>
           <div style="height:20px"></div>
             </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Panel>
