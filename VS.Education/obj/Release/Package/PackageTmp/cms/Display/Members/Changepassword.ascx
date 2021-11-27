<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Changepassword.ascx.cs" Inherits="VS.E_Commerce.cms.Display.Members.Changepassword" %>
<div class="container loadings">
    <div class="row">
        <section class=" col-md-12 NPMP">
            <h2 class="doimatkhau"><%=label("lt_changepassword")%></h2>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
   <div class="thongbaoss"><asp:Label ID="ltmsg" runat="server" ForeColor=Red></asp:Label></div>
   <div class="frm_Thanhvien" style=" padding:10px">
            <div class="gachke canchieucao">
        <div class="tenthanhvien">
                    <%=label("lt_oldpassword")%></div>
                <div class="Password">
                    <asp:TextBox CssClass='contact3' ID="txtcurrentpass" runat="server"  TextMode="Password" Width="250px"></asp:TextBox> 
                       <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator2" runat="server" ValidationGroup="GInfo" ControlToValidate="txtcurrentpass" ErrorMessage="Mật khẩu không được để trống !" ></asp:RequiredFieldValidator>
                    </div>
            </div>
            <div class="gachke canchieucao">
        <div class="tenthanhvien">
                  <%=label("lt_newpassword") %></div>
                <div class="Password">
                    <asp:TextBox CssClass='contact3' ID="txtnewpassword" runat="server"  TextMode="Password" Width="250px"></asp:TextBox> 
                       <asp:RequiredFieldValidator SetFocusOnError="True" ID="RequiredFieldValidator1" runat="server" ValidationGroup="GInfo" ControlToValidate="txtnewpassword" ErrorMessage="Mật khẩu không được để trống !" ></asp:RequiredFieldValidator>
                       </div>
            </div>
            <div class="gachke canchieucao">
        <div class="tenthanhvien" style="">
                     <%=label("lt_checkpassword")%></div>
                <div class="Password">
                    <asp:TextBox CssClass='contact3' ID="txtnewpasswordconfirm" runat="server"  TextMode="Password" Width="250px"></asp:TextBox> 
                       <asp:RequiredFieldValidator  SetFocusOnError="True" ID="RequiredFieldValidator3" runat="server" ValidationGroup="GInfo" ControlToValidate="txtnewpasswordconfirm" ErrorMessage="Mật khẩu không được để trống !" ></asp:RequiredFieldValidator>
                    </div>
            </div>
           </div>    

            <div class="nutgui">
            <asp:Button ID="btnchangepassword"  ValidationGroup="GInfo" runat="server" CssClass="btnadd" Text="Đổi mật khẩu" OnClick="btnchangepassword_Click" />
       </div>
    </ContentTemplate>
</asp:UpdatePanel>

        </section>
    </div>
</div>
