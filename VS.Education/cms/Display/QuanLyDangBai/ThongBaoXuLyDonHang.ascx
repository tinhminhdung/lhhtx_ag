<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ThongBaoXuLyDonHang.ascx.cs" Inherits="VS.E_Commerce.cms.Display.QuanLyDangBai.ThongBaoXuLyDonHang" %>
<asp:Literal ID="ltscript"  runat="server"></asp:Literal>
<div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title">Thông báo xử lý đơn hàng đã mua</h4>
            </div>
            <div class="modal-body">
                <p style="text-align: center">
                    Quý khách vui lòng kiểm tra lại đơn hàng đã mua dưới đây.<br />
                    Nếu quý khách không có vấn đề gì về các đơn hàng này thì sau:
                        <b style="color: red">10</b> ngày hệ thống sẽ tự động duyệt <b style="color: red">(chấp nhận đơn hàng)</b>.
                </p>

                <asp:Literal ID="ltthongbao" runat="server"></asp:Literal>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>

    </div>
</div>


<%--<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#myModal"> myModal
</button>
<button type="button" class="btn btn-primary" data-toggle="modal" data-target="#ThongBaoToanBoNCC">
ThongBaoToanBoNCC
</button>--%>

<div class="modal fade" id="ThongBaoNCC" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header" style=" background:#ffcb03">
                <button type="button" style=" background:#ffcb03" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title" style=" color:#fff">Thông báo !!!</h4>
            </div>
            <div class="modal-body" style=" padding: 10px; ">
                <asp:Literal ID="ltthongbaonhaCC" runat="server"></asp:Literal>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>

    </div>
</div>



<%--<div class="modal fade" id="ThongBaoToanBoNCCs" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header" style=" background:#ffcb03">
                <button type="button" style=" background:#ffcb03" class="close" data-dismiss="modal">&times;</button>
                <h4 class="modal-title" style=" color:#fff">Thông báo nhà cung cấp</h4>
            </div>
            <div class="modal-body">
                
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            </div>
        </div>

    </div>
</div>--%>
<!-- Side Modal Top Right -->
<!-- Button trigger modal -->
 

<!-- Frame Modal Bottom -->
<div class="modal fade bottom" id="ThongBaoToanBoNCC" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">

  <!-- Add class .modal-frame and then add class .modal-bottom (or other classes from list above) to set a position to the modal -->
  <div class="modal-dialog modal-frame modal-bottom" role="document" style="width: 100%!important; margin:10px auto !important">
    <div class="modal-content">
      <div class="modal-body">
        <div class="row d-flex justify-content-center align-items-center" style=" padding: 10px; ">

        <asp:Literal ID="ltthongbaotoanbonhacc" runat="server"></asp:Literal>

          <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
        </div>
      </div>
    </div>
  </div>
</div>
<!-- Frame Modal Bottom -->

  <script>
      function XacNhanNhaCC(ID) {
          debugger;
          $.ajax({
              type: "POST",
              url: "/index.aspx/XacNhanThongTinNCC",
              data: "{ID:'" + ID + "'}",
              contentType: "application/json; charset=utf-8",
              datatype: "json",
              async: "true",
              success: function (response) {
                  alert('Bạn đã xác nhận thành công.');
                  window.location.reload();
              },
              error: function (response) {
                  alert(response.status + ' ' + response.statusText);
              },
              beforeSend: function () {
              },
              complete: function () {
              }
          });
      }
        </script>
<%--
$('#myModal').modal('show');
$('#ThongBaoNCC').modal('show');
$('#ThongBaoToanBoNCC').modal('show');
    $('#ThongBaoToanBoNCC').css("margin-right", $(window).width() - $('.modal-content').width());
    $('#ThongBaoToanBoNCC').css("margin-left", $(window).width() - $('.modal-content').width());
--%>
<%--$('#myModal').modal('show');--%>


<%=ShowTongThongBao() %>