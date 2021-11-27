<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThongKe.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.Member.ThongKe" %>
<div id="cph_Main_ContentPane">
    <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
        <ul>
            <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang chủ</a></li>
            <li class="Last"><span>Thống kê</span></li>
        </ul>
    </div>
    <div style="clear: both;"></div>
    <div class="widget">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="row-fluid">
                    <div class="span12">
                        <div class="widget">
                            <div class="widget-title">
                                <h4><i class="icon-reorder"></i>Thống kê</h4>
                            </div>
                            <div class="widget-body">
                                <div class='frm-add'>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td width="400px"></td>
                                            <td></td>
                                            <td>
                                                <strong><font color="#ed1f27"><asp:Literal ID="ltmsg" runat="server"></asp:Literal></font></strong>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <strong style="text-transform: uppercase;color:red"> <img src="/Resources/admin/images/bullet-red.png" border="0" />Báo cáo Thống kê</strong>
                                            </td>
                                            <td></td>
                                            <td></td>
                                        </tr>
                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Tổng AgLand</td>
                                            <td></td>
                                            <td>
                                                <asp:Literal ID="lttongagland" runat="server"></asp:Literal> 
                                            </td>
                                        </tr>
                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Tổng Chi nhánh</td>
                                            <td></td>
                                            <td>
                                               <asp:Literal ID="lttongchinhanh" runat="server"></asp:Literal> 
                                            </td>
                                        </tr>
                                         <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Tổng Ưu tiên</td>
                                            <td></td>
                                            <td>
                                                 <asp:Literal ID="lttonguutien" runat="server"></asp:Literal>
                                            </td>
                                        </tr>
                                         <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>
                                         <tr>
                                            <td style="padding-left: 15px">Tổng nhà cung cấp</td>
                                            <td></td>
                                            <td>
                                                 <asp:Literal ID="lttonghoahongmuaban" runat="server"></asp:Literal> 
                                            </td>
                                        </tr>
                                         <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 15px">Tổng Leader</td>
                                            <td></td>
                                            <td>
                                                <asp:Literal ID="ltlaisuat" runat="server"></asp:Literal> 
                                            </td>
                                        </tr>
                                      
                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>

                                        <tr style="height: 7px;">
                                            <td colspan="3"></td>
                                        </tr>
                                       
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</div>
