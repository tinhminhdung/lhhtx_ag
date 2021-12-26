<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoadScollAjax.aspx.cs" Inherits="VS.E_Commerce.LoadScollAjax" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <%--<script type="text/javascript">
        var pageIndex = 1;
        var pageCount;
        $(window).scroll(function () {
            if ($(window).scrollTop() == $(document).height() - $(window).height()) {
                // GetRecords();
                Showload();
            }
        });

        function Showload() {

            pageIndex++;
            if (pageIndex == 2 || pageIndex <= pageCount) {
                $("#loader").show();
                $.ajax({
                    type: "POST",
                    url: "/LoadScollAjax.aspx/Showsanpham",
                    data: '{top: ' + pageIndex + '}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    //success: OnSuccess,
                    success: function (data) {
                        alert(data.d);
                        $('#sanphaam').html(data.d);
                    },
                    failure: function (response) {
                        alert(response.d);
                    },
                    error: function (response) {
                        alert(response.d);
                    }
                });
            }
        }

        //function GetRecords() {

        //    pageIndex++;
        //    if (pageIndex == 2 || pageIndex <= pageCount) {
        //        $("#loader").show();
        //        $.ajax({
        //            type: "POST",
        //            url: "/LoadScollAjax.aspx/GetCustomers",
        //            data: '{pageIndex: ' + pageIndex + '}',
        //            contentType: "application/json; charset=utf-8",
        //            dataType: "json",
        //            //success: OnSuccess,
        //            success: function (data) {
        //                alert(data.d);
        //                $('#sanphaam').html(data.d);
        //            },
        //            failure: function (response) {
        //                alert(response.d);
        //            },
        //            error: function (response) {
        //                alert(response.d);
        //            }
        //        });
        //    }
        //}
        //function OnSuccess(response) {
        //    var xmlDoc = $.parseXML(response.d);
        //    var xml = $(xmlDoc);
        //    pageCount = parseInt(xml.find("PageCount").eq(0).find("PageCount").text());
        //    var customers = xml.find("products");
        //    customers.each(function () {
        //        var customer = $(this);
        //        var table = $("#dvCustomers table").eq(0).clone(true);
        //        $(".name", table).html(customer.find("Name").text());
        //        $(".Brief", table).html(customer.find("Brief").text());
        //        $(".Price", table).html(customer.find("Price").text());
        //        $(".ImagesSmall", table).html('<img src=' + customer.find("ImagesSmall").text() + ' style=\'width:100px\' />');
        //        $("#dvCustomers").append(table).append("<br />");
        //    });
        //    $("#loader").hide();
        //}
    </script>--%>

    
<script type="text/javascript">

    var pageSize = 10;
    var pageIndex = 0;

    $(document).ready(function () {
        GetData();

        $(window).scroll(function () {
            if ($(window).scrollTop() ==
               $(document).height() - $(window).height()) {
                GetData();
            }
        });
    });

    function GetData() {
        $.ajax({
            type: 'GET',
            url: '/LoadScollAjax.aspx/Showsanpham',
            data: { "pageindex": pageIndex, "pagesize": pageSize },
            dataType: 'json',
            success: function (data) {
                if (data != null) {
                    for (var i = 0; i < data.length; i++) {
                        $("#container").append("<h2>" +
                        data[i].CompanyName + "</h2>");
                    }
                    pageIndex++;
                }
            },
            beforeSend: function () {
                $("#progress").show();
            },
            complete: function () {
                $("#progress").hide();
            },
            error: function () {
                alert("Error while retrieving data!");
            }
        });
    }
</script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <hr />
            <div id="progress">progress</div>
             <div id="container"></div>
             <div id="container"></div>
            <hr />
             <table>
                <tr>
                    <td>
                        <div id="dvCustomers">
                            <asp:Repeater ID="rptCustomers" runat="server">
                                <ItemTemplate>
                                    <table cellpadding="2" cellspacing="0" border="1" style="width: 200px; height: 100px; border: dashed 2px #04AFEF; background-color: #B0E2F5">
                                        <tr>
                                            <td>
                                                <b><u>
                                                    <div class="ImagesSmall">
                                                        <img src="<%# Eval("ImagesSmall") %>" style="width: 100px" /></div>
                                                    <span class="name">
                                                        <%# Eval("Name") %></span></u></b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Brief: </b><span class="Brief"><%# Eval("Brief") %></span><br />
                                                <b>Price : </b><span class="Price"><%# Eval("Price") %></span><br />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </td>
                    <td valign="bottom">
                        <img id="loader" alt="" src="loading.gif" style="display: none" />
                    </td>
                </tr>
            </table>


        </div>
    </form>
</body>
</html>
