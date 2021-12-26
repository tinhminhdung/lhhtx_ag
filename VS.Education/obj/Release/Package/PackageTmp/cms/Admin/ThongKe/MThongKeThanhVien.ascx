<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MThongKeThanhVien.ascx.cs" Inherits="VS.E_Commerce.cms.Admin.ThongKe.MThongKeThanhVien" %>

<script type="text/javascript" src="https://cdn.fusioncharts.com/fusioncharts/latest/fusioncharts.js"></script>
<script type="text/javascript" src="https://cdn.fusioncharts.com/fusioncharts/latest/themes/fusioncharts.theme.fusion.js"></script>
<div id="cph_Main_ContentPane">
    <div id="">
        <div class="Block Breadcrumb ui-widget-content ui-corner-top ui-corner-bottom" id="Breadcrumb">
            <ul>
                <li class="SecondLast"><a href="/admin.aspx"><i style="font-size: 14px;" class="icon-home"></i>Trang chủ</a></li>
                <li class="Last"><span>Thống kê thành viên</span></li>
            </ul>
        </div>
        <div style="clear: both;"></div>
        <div class="widget">
            <div class="row-fluid">
                <div class="span12">
                    <div class="widget">
                        <div class="widget-title">
                            <h4><i class="icon-reorder"></i>Thống kê thành viên</h4>
                        </div>
                        <div class="widget-body">
                            <div class="row-fluid">
                                <div class="span12">
                                    <div id="sample_1_length" class="dataTables_length">
                                        <asp:DropDownList CssClass="txt" runat="server" ID="ddlchinhanh">
                                        </asp:DropDownList>
                                        <asp:DropDownList CssClass="txt" runat="server" ID="ddlleader">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlAgLand" runat="server" CssClass="txt">
                                            <asp:ListItem Selected="True" Value="-1">= Tất cả AG Land =</asp:ListItem>
                                            <asp:ListItem Value="0">Chưa kích hoạt AgLand</asp:ListItem>
                                            <asp:ListItem Value="1">Đã kích hoạt AgLand</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddluutien" runat="server" CssClass="txt">
                                            <asp:ListItem Selected="True" Value="-1">= Tất cả Ưu tiên=</asp:ListItem>
                                            <asp:ListItem Value="0">Chưa kích hoạt Ưu tiên</asp:ListItem>
                                            <asp:ListItem Value="1">Đã kích hoạt Ưu tiên</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlkieuthanhvien" runat="server" CssClass="txt" >
                                        <asp:ListItem Selected="True" Value="-1">= Tất cả Loại tài khoản =</asp:ListItem>
                                        <asp:ListItem Value="1">Thành viên</asp:ListItem>
                                        <asp:ListItem Value="2">Tất cả nhà cung cấp</asp:ListItem>
                                            <asp:ListItem Value="3">Chỉ có nhà cung cấp đã đăng sản phẩm</asp:ListItem>
                                    </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="list_item">
                                <asp:RadioButton ID="rd_bydate" runat="server" GroupName="reporttype" Text=" Lọc theo tháng " AutoPostBack="True" OnCheckedChanged="rd_bydate_CheckedChanged"></asp:RadioButton>
                                <asp:RadioButton ID="rd_bymonth" runat="server" GroupName="reporttype" Text=" Lọc theo năm " AutoPostBack="True" OnCheckedChanged="rd_bymonth_CheckedChanged"></asp:RadioButton>
                                <br />
                                <br />
                                <asp:Panel ID="pn_bydate" runat="server" Visible="False">
                                    Tháng 
										<asp:DropDownList ID="ddl_month" runat="server">
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                        </asp:DropDownList>
                                    Năm 
										<asp:DropDownList ID="ddl_yearbyday" runat="server">
                                            <asp:ListItem Value="2019">2019</asp:ListItem>
                                            <asp:ListItem Value="2020">2020</asp:ListItem>
                                            <asp:ListItem Value="2021">2021</asp:ListItem>
                                            <asp:ListItem Value="2022">2022</asp:ListItem>
                                            <asp:ListItem Value="2023">2023</asp:ListItem>
                                            <asp:ListItem Value="2024">2024</asp:ListItem>
                                            <asp:ListItem Value="2025">2025</asp:ListItem>
                                        </asp:DropDownList>
                                    <asp:Button ID="btn_showbydate" Font-Bold="True" Text="Show báo cáo" CssClass="vadd toolbar btn btn-info" Style="margin-top: -8px;" runat="server" OnClick="btn_showbydate_Click"></asp:Button>
                                    <asp:Literal ID="lt_reportbydate" runat="server"></asp:Literal>
                                </asp:Panel>
                                <asp:Panel ID="pn_bymonth" runat="server" Visible="False">
                                    Chọn Năm 
										<asp:DropDownList ID="ddl_yearbymonth" runat="server">
                                            <asp:ListItem Value="2019">2019</asp:ListItem>
                                            <asp:ListItem Value="2020">2020</asp:ListItem>
                                            <asp:ListItem Value="2021">2021</asp:ListItem>
                                            <asp:ListItem Value="2022">2022</asp:ListItem>
                                            <asp:ListItem Value="2023">2023</asp:ListItem>
                                            <asp:ListItem Value="2024">2024</asp:ListItem>
                                            <asp:ListItem Value="2025">2025</asp:ListItem>
                                        </asp:DropDownList>
                                    <asp:Button ID="btn_showbymonth" Font-Bold="True" Text="Show báo cáo" runat="server" CssClass="vadd toolbar btn btn-info" Style="margin-top: -8px;" OnClick="btn_showbymonth_Click"></asp:Button></TD>
										<asp:Literal ID="lt_reportbymonth" runat="server"></asp:Literal>
                                </asp:Panel>
                                <div id="chart-container">FusionCharts XT will load here!</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
