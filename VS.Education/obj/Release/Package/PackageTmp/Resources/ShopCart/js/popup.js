$(document).ready(function () {
    $(".closes").click(function () {
        return $("#popuprel3").fadeOut(), $("#fade").css("display", "none"), !1
    }), $("#fade").click(function () {
        return
        $(this).css("display", "none"), !1
    }),
    $("a.popup").click(function () {
        // ShowCart(); // Gọi lại hàm load giỏ hàng trong popup ra
        var e = $(this).attr("rel");
        $("#" + e).fadeIn(),
     $("body").append('<div id="fade"></div>'),
     $("#fade").css({ filter: "alpha(opacity=80)" }).fadeIn();
        var a = ($("#" + e).height() + 10) / 2, n = ($("#" + e).width() + 10) / 2;
        $("#" + e).css({ "margin-top": -a, "margin-left": -n })
    }), $("#fade").click(function () { return $("#popuprel3").fadeOut(), !1 })
});

$(document).ready(function () {
    $('a.popup').click(function () {
        var popupid = $(this).attr('rel');
        $('#' + popupid).fadeIn();
        $('body').append('<div id="fade"></div>');
        $('#fade').css({ 'filter': 'alpha(opacity=80)' }).fadeIn();
        var popuptopmargin = ($('#' + popupid).height() + 10) / 2;
        var popupleftmargin = ($('#' + popupid).width() + 10) / 2;
        $('#' + popupid).css({
            'margin-top': -popuptopmargin,
            'margin-left': -popupleftmargin
        });
    });
    $('#fade').click(function () {
        $('#fade , #popuprel , #popuprel2 , #popuprel3').fadeOut()
        return false;
    });
});

function LoadCart() {
    // make a server call here
    $.ajax({
        url: '/index.aspx/LoadCart',
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //alert(data.d);
            $('#Cart').html(data.d);
        },
        error: function (response) {
            // alert(response.responseText);
        },
        failure: function (response) {
            // alert(response.responseText);
        }
    });
}

function iframe() {
    $(document).ready(function () {
        $('#iframe').html('iframe login');
    });
}

function ShowCart() {
    // make a server call here
    $.ajax({
        url: '/index.aspx/ShowCart',
        dataType: "json",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            //alert(data.d);
            $('#cartContent').html(data.d);
        },
        error: function (response) {
            // alert(response.responseText);
        },
        failure: function (response) {
            // alert(response.responseText);
        }
    });
}

function closeelement() {
    div = document.getElementById("jName"); div.style.display = "none";
}

//onmouseover="UpdateOrder(<%#Eval("ipid")%>,'<%#Eval("Name")%>')"
//onclick="UpdateOrder(<%#Eval("ipid")%>,'<%#Eval("Name")%>')"
// Thêm số lương và sp vào giỏ hàng

//function UpdateOrder(id, name) {
//    var numPro = "#" + id;
//    //bắt đầu
//    $body = $("#Ajaxloading");
//    $body = $("#Ajaxloading");
//    $.ajax({
//        type: "POST",
//        url: "/index.aspx/Up_Order",
//        data: "{id:'" + id.toString() + "',quantity:'" + $(numPro).val().toString() + "'}",
//        contentType: "application/json; charset=utf-8",
//        datatype: "json",
//        async: "true",
//        success: function (response) {
//            //ShowCart();
//            LoadCart();
//        },
//        error: function (response) {
//            //  alert(response.status + ' ' + response.statusText);
//        },
//        beforeSend: function () {
//            $body.addClass('loading');
//        },
//        complete: function () {
//            $body.removeClass("loading");
//        }
//    });
//    //    var soluong = 1;
//    //    var total = parseInt(soluong) + parseInt($("#Cart")[0].innerHTML);
//    //    $("#Cart")[0].innerHTML = total.toString();
//    // kết thúc
//    $("#jName").show();
//    $("#jName")[0].innerHTML = '<a href="/gio-hang.html" style="color:#000; font-size:12px;"><b style="color:#ff3a00; font-size:12px;">Bạn đã thêm <b style="color:#ff3a00; font-size:13px;">' + $(numPro).val().toString() + '</b> Sản phẩm vào giỏ hàng</b><br />' + name + '</a>';
//    setTimeout(closeelement, 3000);
//}

function UpdateOrder(id, name) {
    $body = $("#Ajaxloading");
    var numPro = "#" + id;
    //bắt đầu
    debugger;
    $.ajax({
        type: "POST",
        url: "/index.aspx/Up_Order",
        data: "{id:'" + id.toString() + "',quantity:'" + $(numPro).val().toString() + "'}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: "true",
        success: function (response) {
        },
        error: function (response) {
            // alert(response.status + ' ' + response.statusText);
        },
        beforeSend: function () {
            $body.addClass('loading');
        },
        complete: function () {
            $body.removeClass("loading");
        }
    });
    $("#jName").show();
    $("#jName")[0].innerHTML = '<a href="/gio-hang.html" style="color:#000; font-size:12px;"><b style="color:#ff3a00; font-size:12px;">Bạn đã thêm <b style="color:#ff3a00; font-size:13px;">' + $(numPro).val().toString() + '</b> Sản phẩm vào giỏ hàng</b><br />' + name + '</a>';
    setTimeout(closeelement, 3000);
}

// Xóa sp trong giỏ hàng
function AJdeleteShoppingCartItem(id, name) {
    $body = $("#Ajaxloading");
    var a = confirm("Bạn có muốn xóa sản phẩm này trong giỏ hàng ? ");
    if (a == true) {
        $.ajax({
            type: "POST",
            url: "/index.aspx/DeleteShopCart",
            data: "{id:'" + id.toString() + "',quantity:'1'}",
            contentType: "application/json; charset=utf-8",
            datatype: "json",
            async: "true",
            success: function (response) {
                ShowCart();
                LoadCart();
            },
            error: function (response) {
                // alert(response.status + ' ' + response.statusText);
            },
            beforeSend: function () {
                $body.addClass('loading'); // Gọi lại hàm loading ảnh loading.png // Mỗi khi kích vào sự kiện Ajax là nó sẽ loading ra cái hàm này
            },
            complete: function () {
                $body.removeClass("loading");
            }
        });
    }
}
// Tăng giảm số lượng trong giỏ hàng
function AddShoppingCartItem(id, name, number) {
    //        var a = number.attr('data-abc');
    $body = $("#Ajaxloading");
    $.ajax({
        type: "POST",
        url: "/index.aspx/Updatequantity",
        data: "{id:'" + id.toString() + "',quantity:'" + number.val() + "'}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: "true",
        success: function (response) {
            ShowCart();
            LoadCart();
        },
        error: function (response) {
        },
        beforeSend: function () {
            $body.addClass('loading');
        },
        complete: function () {
            $body.removeClass("loading");
        }
    });
}
function CartItem() {
    ShowCart();
}

// Chi tiet san pham phần kích cỡ và size
$(".size").on("click", function () {
    var count = $(this).parent().find("a").length;
    for (i = 0; i < count; i++) {
        $(this).parent().find("a")[i].className = "size";
    }
    this.className = "size active";
})
$(".Color").on("click", function () {
    var count = $(this).parent().find("a").length;
    for (i = 0; i < count; i++) {
        $(this).parent().find("a")[i].className = "Color";
    }
    this.className = "Color active";
})

function KichCo(id, name) {
    //bắt đầu
    $body = $("#Ajaxloading");
    $.ajax({
        type: "POST",
        url: "/index.aspx/Up_KichCo",
        data: "{id:'" + id.toString() + "',quantity:'1'}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: "true",
        success: function (response) {
        },
        error: function (response) {
        },
        beforeSend: function () {
            $body.addClass('loading');
        },
        complete: function () {
            $body.removeClass("loading");
        }
    });
}

function MauSac(id, name) {
    //bắt đầu
    $body = $("#Ajaxloading");
    $.ajax({
        type: "POST",
        url: "/index.aspx/Up_MauSac",
        data: "{id:'" + id.toString() + "',quantity:'1'}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: "true",
        success: function (response) {
        },
        error: function (response) {
        },
        beforeSend: function () {
            $body.addClass('loading');
        },
        complete: function () {
            $body.removeClass("loading");
        }
    });
}

///Bộ lọc tìm kiếm 
// loại bỏ phân trang đi lấy theo class pageactive và id
//url.replace('?page=' + document.getElementsByClassName('pageactive')[0].id + '&', '?');
//active
jQuery(document).ready(function () {
    // active color
    var color1 = getParameterByName("color");
    if (location.href.lastIndexOf("color") > 0) {
        var idcolor = color1.split(',');
        for (var i = 0; i < idcolor.length; i++) {
            var a = jQuery('div.color a[id=' + idcolor[i] + ']');
            //var a = jQuery('ul.color a[id=' + idcolor[i] + ']').parent();
            a.attr('class', 'sort_list active');
        }
    }
    //active produce
    var size1 = getParameterByName("kichco");
    if (location.href.lastIndexOf("kichco") > 0) {
        var idsize = size1.split(',');
        for (var i = 0; i < idsize.length; i++) {
            var a = jQuery('div.kichco a[id=' + idsize[i] + ']');
            //var a = jQuery('ul.produce a[id=' + idsize[i] + ']').parent();
            a.attr('class', 'sort_list active');
        }
    }

    //active produce
    var pro = getParameterByName("produce");
    if (location.href.lastIndexOf("produce") > 0) {
        var idsize = pro.split(',');
        for (var i = 0; i < idsize.length; i++) {
            var a = jQuery('div.produce a[id=' + idsize[i] + ']');
            //var a = jQuery('ul.produce a[id=' + idsize[i] + ']').parent();
            a.attr('class', 'sort_list active');
        }
    }


    //active price
    var price1 = getParameterByName("price");
    if (location.href.lastIndexOf("price") > 0) {
        var idprice = price1.split(',');
        for (var i = 0; i < idprice.length; i++) {
            var a = jQuery('div.price a[name=' + idprice[i] + ']');
            //var a = jQuery('ul.price li  a[name=' + idprice[i] + ']').parent();
            a.attr('class', 'sort_list active');
        }
    }
});

//end active
// color
function choose_color(tag) {
    jQuery.noConflict();
    var id = jQuery(tag).attr('id');
    var url = location.href.toString();
    if (url.lastIndexOf("?") > 0) {
        if (url.lastIndexOf("color") > 0) {
            var idcolor = getParameterByName("color");
            var arridcolor = idcolor.split(',');
            var strcolor = "";
            var bool = 0;
            for (var i = 0; i < arridcolor.length; i++) {
                if (id == arridcolor[i]) {
                    arridcolor[i] = "";
                    bool = 1;
                }
                if (arridcolor[i] != "")
                    strcolor += arridcolor[i] + ',';
            }
            if (bool == 1) {
                strcolor = strcolor.substring(0, strcolor.length - 1);
            }
            else {
                strcolor += id;
            }
            strcolor = "color=" + strcolor;
            if (bool == 1 && arridcolor.length == 1) {
                var index = location.href.lastIndexOf("color");
                var a = location.href.substring(index - 1, index);
                if (a == "?") {
                    if (location.href.indexOf("&") > 0)
                        location.href = location.href.replace("color=" + idcolor + "&", "");
                    else location.href = location.href.replace("?color=" + idcolor, "");
                }
                else if (a == "&") {
                    location.href = location.href.replace("&color=" + idcolor, "");
                }
                return;
            }
            else {
                url = url.replace("color=" + idcolor, strcolor);
            }
        }
        else url = url + "&color=" + id;
    }
    else
        url = url + "?color=" + id;
    // đoạn này để loại bỏ page , nếu ko sẽ bi lỗi phân trang
    if (url.lastIndexOf("page") > 0) {
        url = url.replace('?page=' + document.getElementsByClassName('pageactive')[0].id + '&', '?');
    }
    location.href = url;
};

function choose_kichco(tag) {
    jQuery.noConflict();
    var id = jQuery(tag).attr('id');
    var url = location.href.toString();
    if (url.lastIndexOf("?") > 0) {
        if (url.lastIndexOf("kichco") > 0) {
            var idkichco = getParameterByName("kichco");
            var arridkichco = idkichco.split(',');
            var strkichco = "";
            var bool = 0;
            for (var i = 0; i < arridkichco.length; i++) {
                if (id == arridkichco[i]) {
                    arridkichco[i] = "";
                    bool = 1;
                }
                if (arridkichco[i] != "")
                    strkichco += arridkichco[i] + ',';
            }
            if (bool == 1) {
                strkichco = strkichco.substring(0, strkichco.length - 1);
            }
            else {
                strkichco += id;
            }
            strkichco = "kichco=" + strkichco;
            if (bool == 1 && arridkichco.length == 1) {
                var index = location.href.lastIndexOf("kichco");
                var a = location.href.substring(index - 1, index);
                if (a == "?") {
                    if (location.href.indexOf("&") > 0)
                        location.href = location.href.replace("kichco=" + idkichco + "&", "");
                    else location.href = location.href.replace("?kichco=" + idkichco, "");
                }
                else if (a == "&") {
                    location.href = location.href.replace("&kichco=" + idkichco, "");
                }
                return;
            }
            else {
                url = url.replace("kichco=" + idkichco, strkichco);
            }
        }
        else url = url + "&kichco=" + id;
    }
    else
        url = url + "?kichco=" + id;
    // đoạn này để loại bỏ page , nếu ko sẽ bi lỗi phân trang
    if (url.lastIndexOf("page") > 0) {
        url = url.replace('?page=' + document.getElementsByClassName('pageactive')[0].id + '&', '?');
    }
    location.href = url;
};
//produce
function choose_produce(tag) {
    jQuery.noConflict();
    var id = jQuery(tag).attr('id');
    var url = location.href.toString();
    if (url.lastIndexOf("?") > 0) {
        if (url.lastIndexOf("produce") > 0) {
            var idproduce = getParameterByName("produce");
            var arridproduce = idproduce.split(',');
            var strproduce = "";
            var bool = 0;
            for (var i = 0; i < arridproduce.length; i++) {
                if (id == arridproduce[i]) {
                    arridproduce[i] = "";
                    bool = 1;
                }
                if (arridproduce[i] != "")
                    strproduce += arridproduce[i] + ',';
            }
            if (bool == 1) {
                strproduce = strproduce.substring(0, strproduce.length - 1);
            }
            else {
                strproduce += id;
            }
            strproduce = "produce=" + strproduce;
            if (bool == 1 && arridproduce.length == 1) {
                var index = location.href.lastIndexOf("produce");
                var a = location.href.substring(index - 1, index);
                if (a == "?") {
                    if (location.href.indexOf("&") > 0)
                        location.href = location.href.replace("produce=" + idproduce + "&", "");
                    else location.href = location.href.replace("?produce=" + idproduce, "");
                }
                else if (a == "&") {
                    location.href = location.href.replace("&produce=" + idproduce, "");
                }
                return;
            }
            else {
                url = url.replace("produce=" + idproduce, strproduce);
            }
        }
        else url = url + "&produce=" + id;
    }
    else
        url = url + "?produce=" + id;
    // đoạn này để loại bỏ page , nếu ko sẽ bi lỗi phân trang
    if (url.lastIndexOf("page") > 0) {
        url = url.replace('?page=' + document.getElementsByClassName('pageactive')[0].id + '&', '?');
    }
    location.href = url;
};

//price
function choose_price(tag) {
    jQuery.noConflict();
    var id = jQuery(tag).attr('name');
    var url = location.href.toString();
    if (url.lastIndexOf("?") > 0) {
        if (url.lastIndexOf("price") > 0) {
            var idprice = getParameterByName("price");
            var arridprice = idprice.split(',');
            var strprice = "";
            var bool = 0;
            for (var i = 0; i < arridprice.length; i++) {
                if (id == arridprice[i]) {
                    arridprice[i] = "";
                    bool = 1;
                }
                if (arridprice[i] != "")
                    strprice += arridprice[i] + ',';
            }
            if (bool == 1) {
                strprice = strprice.substring(0, strprice.length - 1);
            }
            else {
                strprice += id;
            }
            strprice = "price=" + strprice;
            if (bool == 1 && arridprice.length == 1) {
                var index = location.href.lastIndexOf("price");
                var a = location.href.substring(index - 1, index);
                if (a == "?") {
                    if (location.href.indexOf("&") > 0)
                        location.href = location.href.replace("price=" + idprice + "&", "");
                    else location.href = location.href.replace("?price=" + idprice, "");
                }
                else if (a == "&") {
                    location.href = location.href.replace("&price=" + idprice, "");
                }
                return;
            }
            else {
                url = url.replace("price=" + idprice, strprice);
            }
        }
        else url = url + "&price=" + id;
    }
    else
        url = url + "?price=" + id;
    // đoạn này để loại bỏ page , nếu ko sẽ bi lỗi phân trang
    if (url.lastIndexOf("page") > 0) {
        url = url.replace('?page=' + document.getElementsByClassName('pageactive')[0].id + '&', '?');
    }
    location.href = url;
};

function getParameterByName(name) {
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"), results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
};

function Xemnhanh(id, name) {
    $body = $("#Ajaxloading");
    $.ajax({
        type: "POST",
        url: "/index.aspx/Detail",
        data: "{id:'" + id.toString() + "',quantity:'1'}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: "true",
        success: function (response) {
            $('#cartContent').html(response.d);
            $('.phonganhct').zomphonganh();
            ActiveMauSize();
        },
        error: function (response) {
        },
        beforeSend: function () {
            $body.addClass('loading');
        },
        complete: function () {
            $body.removeClass("loading");
        }
    });
}

function Dathang(id, name) {
    $body = $("#Ajaxloading");
    var numPro = "#" + id;
    //bắt đầu
    debugger;
    $.ajax({
        type: "POST",
        url: "/index.aspx/Up_Order",
        data: "{id:'" + id.toString() + "',quantity:'" + $(numPro).val().toString() + "'}",
        contentType: "application/json; charset=utf-8",
        datatype: "json",
        async: "true",
        success: function (response) {
            window.location.href = 'gio-hang.html';
        },
        error: function (response) {
            // alert(response.status + ' ' + response.statusText);
        },
        beforeSend: function () {
            $body.addClass('loading');
        },
        complete: function () {
            $body.removeClass("loading");
        }
    });
    $("#jName").show();
    $("#jName")[0].innerHTML = '<a href="/gio-hang.html" style="color:#000; font-size:12px;"><b style="color:#ff3a00; font-size:12px;">Bạn đã thêm <b style="color:#ff3a00; font-size:13px;">' + $(numPro).val().toString() + '</b> Sản phẩm vào giỏ hàng</b><br />' + name + '</a>';
    setTimeout(closeelement, 3000);
    window.location.href = 'gio-hang.html';
}

function ActiveMauSize() {
    $(".size").on("click", function () {
        debugger;
        var count = $(this).parent().find("a").length;
        for (i = 0; i < count; i++) {
            $(this).parent().find("a")[i].className = "size";
        }
        this.className = "size active";
    })
    $(".Color").on("click", function () {
        debugger;
        var count = $(this).parent().find("a").length;
        for (i = 0; i < count; i++) {
            $(this).parent().find("a")[i].className = "Color";
        }
        this.className = "Color active";
    })
}