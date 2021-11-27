<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OnpaySuccess.aspx.cs" Inherits="VS.E_Commerce.OnpaySuccess" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style=" text-align:center; line-height:22px;">
     <asp:Literal ID="lblStatus" runat="server"></asp:Literal>
    </div>
    </form>
</body>
</html>


<%--https://mtf.onepay.vn/developer/?page=modul_noidia
Chấp nhận thanh toán các thẻ ATM của các ngân hàng: Vietcombank, Vietinbank, VIB, Đông Á, HD Bank, Techcombank, Tiên Phong Bank, Việt Á, Nam Á, MSB, SHB, Eximbank, MB...

Thông số tài khoản test cổng thanh toán Nội địa: 
URL Payment: https://mtf.onepay.vn/onecomm-pay/vpc.op
MerchantID: ONEPAY
Accesscode: D67342C2 
Hashcode (Tên khác là SECURE_SECRET): A3EFDFABA8653DF2342E8DAC29B51AF0 

Thẻ cũ, chỉ có thể sử dụng trên hệ thống test OP:
Tên: NGUYEN HONG NHUNG
Số thẻ: 6868682607535021 
Tháng/Năm phát hành: 12/08 
Mã OTP: 1234567890 


Thông số tài khoản thật cổng thanh toán Nội địa:
URL Payment: https://onepay.vn/onecomm-pay/vpc.op
MerchantID: YOUR MERCHANT ID
Accesscode: YOUR ACCESSCODE 
Hashcode (Tên khác là SECURE_SECRET): YOUR SECURE SECRET--%>