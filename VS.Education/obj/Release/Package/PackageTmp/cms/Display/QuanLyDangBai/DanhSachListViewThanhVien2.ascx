<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DanhSachListViewThanhVien2.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.DanhSachListViewThanhVien2" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<h1 class="title-head">
    <span>Thành viên kết nối trực tiếp</span>
</h1>
 <asp:LinkButton ID="lnkxuatExel" runat="server" OnClick="lnkxuatExel_Click" CssClass="vadd toolbar btn btn-info"> Xuất Exel</asp:LinkButton>

<div style="clear: both"></div>
<div class="phanmucc"><b>phân mục cấp:</b> <asp:Literal ID="ltphanmuc" runat="server"></asp:Literal></div>
<div class="my-account">
    <div class="dashboard">
        <div class="recent-orders">
            <div class="table-responsive tab-all" style="overflow-x: auto;">
                  <table class="table table-striped table-bordered table-advance table-hover table table-cart lichsumuahang">
                    <thead class="thead-default">
                        <tr>
                            <th>STT</th>
                            <th>Thành viên</th>
                            <th style=" width: 200px; ">Địa chỉ</th>
                            <th>Loại tài khoản</th>
                            <th>Chi nhánh</th>
                             <th>Ngày đăng ký</th>
                              <th>Tổng thành viên</th>
                             <th>Chi tiết</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rp_items" runat="server" >
                            <ItemTemplate>
                                <tr class="first odd">
                                    <td style="text-align: center"><%= i++ %></td>
                                    <td style="text-align: center"><%#Eval("vfname") %></td>
                                    <td style="text-align: center"><%#Eval("vaddress") %></td>
                                    <td style="text-align: center"><%#MoreAll.MoreAll.Kieuloai(Eval("Type").ToString())%> </td>
                                     <td style="text-align: center"><%#ShowChiNhanh(DataBinder.Eval(Container.DataItem, "IDChiNhanh").ToString())%></td>
                                    <td style="text-align: center"><%#Eval("dcreatedate") %> </td>
                                      <td style="text-align: center"><%#TongThanhVien_Tree(Eval("iuser_id").ToString())%></td> 
                                    <td style="text-align: center"> 
                                         <a style="color:red" href="/Danh-sach-thanh-vien.html?ID=<%#DataBinder.Eval(Container.DataItem,"iuser_id")%>">[Chi tiết]</a>
                                     </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>
            <div class="text-xs-right">
            </div>
              <asp:Literal ID="ltShow" runat="server"></asp:Literal>
        <div style="clear: both;"></div>
        <div class="pager">
             <asp:Literal ID="ltpage" runat="server"></asp:Literal>
        </div>

            <div class="phantrang" style="">

                <cc1:CollectionPager ID="CollectionPager1" runat="server" BackNextDisplay="HyperLinks" BackNextLocation="Split"
                    BackText="<<" ShowFirstLast="True" ResultsLocation="Bottom" PagingMode="QueryString" MaxPages="50" FirstText="Trang đầu" HideOnSinglePage="True" IgnoreQueryString="False" LabelStyle="font-weight: bold;color:red" LabelText="" LastText="Cuối cùng" NextText=">>" PageNumbersDisplay="Numbers"
                    ResultsFormat="Hiển thị từ  {0} Đến {1} (của {2})" ResultsStyle="padding-bottom:5px;padding-top:14px;font-weight: bold;" ShowLabel="False" ShowPageNumbers="True" BackNextStyle="font-weight: bold; margin: 14px;" ControlCssClass="" ControlStyle="" UseSlider="True" PageNumbersSeparator="">
                </cc1:CollectionPager>
            </div>
        </div>
    </div>

</div>
     <input id="hd_id" type="hidden" size="1" name="Hidden2" runat="server">
<asp:HiddenField ID="hdid" runat="server" />
