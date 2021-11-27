jQuery(document).ready(function ($) {
    var loadimg = $('.loadings');
    // load ảnh loading.gif trước
    var images = loadimg.find('iframe').get();
    //console.log(images);
    if (images.length > 0) {
        images.forEach(function (item) {
            // console.log('item ', item);
            $(item).attr('load', $(item).attr('src'));
            $(item).attr('src', 'Resources/images/nen.gif');
        });
    }
    // load xong moi load anh
    var swap = function () {
        //Chuyển load ở thẻ attr('load') thành src
        //  $(window).height() cộng thêm chiều cao của trang tính từ thẻ boby
        var top = $(window).scrollTop() + $(window).height();
        var images = $(loadimg).find('iframe').get();
        if (images.length > 0) {
            images.forEach(function (item) {
                //console.log($(item).offset(), top, top > $(item).offset().top);
                if ($(item).is(':visible') && $(item).attr('src') !== undefined && $(item).attr('src') !== '' && top > $(item).offset().top && $(item).offset().top > 0) {
                    $(item).attr('src', $(item).attr('load'));
                    //  console.log($(item).attr('src', $(item).attr('load')));
                }
            });
        }
    }
    // sét thời gian khi vào web 1 giây sau mới load trang
    setTimeout(function () {
        swap();
    }, 1000);

    $(window).scroll(function () {
        swap();
    });

});


$(window).scroll(function () {
    if ($(this).scrollTop() != 0) {
        $('#toTop').fadeIn();
    } else {
        $('#toTop').fadeOut();
    }
});
$('#toTop').click(function () {
    $('body,html').animate(
        {
            scrollTop: 0
        }, 800
    );
});