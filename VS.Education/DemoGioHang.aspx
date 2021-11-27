<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DemoGioHang.aspx.cs" Inherits="VS.E_Commerce.DemoGioHang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <style>
        .header-cart-content {
            width: 100%;
            float: left;
            padding: 15px;
        }

        .content-product-list .item-product {
            min-height: 110px;
            border-bottom: solid 1px #ebebeb;
            overflow: hidden;
            padding: 5px 0;
        }

        .item-product-cart-mobile {
            margin-right: 5px;
        }

        .title-product-cart-mobile {
            width: calc(100% - 160px);
            margin-right: 5px;
        }

            .title-product-cart-mobile h3 {
                line-height: 1;
                margin: 0;
            }

            .title-product-cart-mobile a {
                word-break: break-word;
                font-family: Arial, sans-serif;
                font-size: 12px;
                color: #363636;
            }

            .title-product-cart-mobile p {
                line-height: 2;
                font-size: 12px;
                font-family: Arial, sans-serif;
                color: #898989;
                margin-bottom: 0;
            }

            .title-product-cart-mobile span {
                color: #dc3333;
            }

        .select-item-qty-mobile {
            float: right;
            text-align: center;
        }

            .select-item-qty-mobile .txt_center {
                position: relative;
                width: 70px;
                height: 25px;
            }

                .select-item-qty-mobile .txt_center input {
                    position: absolute;
                    left: 21px;
                    height: 25px;
                    width: 25px;
                    text-align: center;
                    margin: 0px;
                    padding: 0;
                    float: left;
                    display: inline-block;
                    line-height: 1;
                    min-height: auto;
                }

                .select-item-qty-mobile .txt_center .items-count {
                    position: absolute;
                    top: 0;
                    height: 25px;
                    width: 22px;
                    color: #000;
                    border: #ebebeb thin solid;
                    background: none !important;
                    float: left;
                    display: inline-block;
                    line-height: 1;
                    min-width: auto;
                    padding: 0;
                }

                .select-item-qty-mobile .txt_center input {
                    position: absolute;
                    left: 21px;
                    height: 25px;
                    width: 25px;
                    text-align: center;
                    margin: 0px;
                    padding: 0;
                    float: left;
                    display: inline-block;
                    line-height: 1;
                    min-height: auto;
                }

                .select-item-qty-mobile .txt_center .items-count.increase {
                    left: 45px;
                }

            .select-item-qty-mobile a {
                line-height: 3;
                color: #363636;
                font-family: 'Open Sans', sans-serif;
            }

        .content-product-list .item-product {
            min-height: 110px;
            border-bottom: solid 1px #ebebeb;
            overflow: hidden;
            padding: 5px 0;
        }

        .item-product-cart-mobile, .select-item-qty-mobile {
            float: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="header-cart-content" style="background: #fff;">
                <div class="cart_page_mobile content-product-list">
                    <div class="item-product item productid-4566352 ">
                        <div class="item-product-cart-mobile">
                            <a href="/giay-vai-converse-3"></a><a class="product-images1" href="" title="Giày vải Converse 3 - Xanh">
                                <img width="80" height="150" alt="" src="//bizweb.dktcdn.net/thumb/small/100/091/132/products/3-min-2aeb1bce-2365-43fc-ad76-752f82aafd51.jpg"></a>
                        </div>
                        <div class="title-product-cart-mobile">
                            <h3><a href="/giay-vai-converse-3" title="Giày vải Converse 3 - Xanh">Giày vải Converse 3 - Xanh</a></h3>
                            <p>Giá: <span class="pricechange">700.000₫</span></p>
                        </div>
                        <div class="select-item-qty-mobile">
                            <div class="txt_center">
                                <input class="variantID" type="hidden" name="variantId" value="4566352">
                                <button onclick="var result = document.getElementById('qtyMobile4566352'); var qtyMobile4566352 = result.value; if( !isNaN( qtyMobile4566352 ) &amp;&amp; qtyMobile4566352 > 0 ) result.value--;return false;" class="reduced items-count btn-minus" type="button">–</button><input type="number" maxlength="12" min="1" class="input-text number-sidebar qtyMobile4566352" id="qtyMobile4566352" name="Lines" size="4" value="1">
                                <button onclick="var result = document.getElementById('qtyMobile4566352'); var qtyMobile4566352 = result.value; if( !isNaN( qtyMobile4566352 )) result.value++;return false;" class="increase items-count btn-plus" type="button">+</button>
                            </div>
                            <a class="button remove-item remove-item-cart" href="javascript:;" data-id="4566352">Xoá</a>
                        </div>
                    </div>
                    <div class="item-product item productid-4566585 ">
                        <div class="item-product-cart-mobile">
                            <a href="/giay-converse-madison-mono-leather"></a><a class="product-images1" href="" title="Giày Converse Madison Mono - Đỏ">
                                <img width="80" height="150" alt="" src="//bizweb.dktcdn.net/thumb/small/100/091/132/products/15-min.jpg"></a>
                        </div>
                        <div class="title-product-cart-mobile">
                            <h3><a href="/giay-converse-madison-mono-leather" title="Giày Converse Madison Mono - Đỏ">Giày Converse Madison Mono - Đỏ</a></h3>
                            <p>Giá: <span class="pricechange">400.000₫</span></p>
                        </div>
                        <div class="select-item-qty-mobile">
                            <div class="txt_center">
                                <input class="variantID" type="hidden" name="variantId" value="4566585">
                                <button onclick="var result = document.getElementById('qtyMobile4566585'); var qtyMobile4566585 = result.value; if( !isNaN( qtyMobile4566585 ) &amp;&amp; qtyMobile4566585 > 0 ) result.value--;return false;" class="reduced items-count btn-minus" type="button">–</button><input type="number" maxlength="12" min="1" class="input-text number-sidebar qtyMobile4566585" id="qtyMobile4566585" name="Lines" size="4" value="1">
                                <button onclick="var result = document.getElementById('qtyMobile4566585'); var qtyMobile4566585 = result.value; if( !isNaN( qtyMobile4566585 )) result.value++;return false;" class="increase items-count btn-plus" type="button">+</button>
                            </div>
                            <a class="button remove-item remove-item-cart" href="javascript:;" data-id="4566585">Xoá</a>
                        </div>
                    </div>
                </div>
                <div class="header-cart-price" style="">
                    <div class="title-cart ">
                        <h3 class="text-xs-left">Tổng tiền</h3>
                        <a class="text-xs-right totals_price_mobile">1.100.000₫</a>
                    </div>
                    <div class="checkout">
                        <button class="btn-proceed-checkout-mobile" title="Tiến hành thanh toán" type="button" onclick="window.location.href='/checkout'"><span>Tiến hành thanh toán</span></button>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
