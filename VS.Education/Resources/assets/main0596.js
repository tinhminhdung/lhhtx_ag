$(document).ready(function($){"use strict";awe_backtotop();awe_owl();awe_category();awe_menumobile();awe_tab();});function awe_convertVietnamese(str){str=str.toLowerCase();str=str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g,"a");str=str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g,"e");str=str.replace(/ì|í|ị|ỉ|ĩ/g,"i");str=str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g,"o");str=str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g,"u");str=str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g,"y");str=str.replace(/đ/g,"d");str=str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g,"-");str=str.replace(/-+-/g,"-");str=str.replace(/^\-+|\-+$/g,"");return str;}window.awe_convertVietnamese=awe_convertVietnamese;function awe_showNoitice(selector){$(selector).animate({right:'0'},500);setTimeout(function(){$(selector).animate({right:'-300px'},500);},3500);}window.awe_showNoitice=awe_showNoitice;function awe_showLoading(selector){var loading=$('.loader').html();$(selector).addClass("loading").append(loading);}window.awe_showLoading=awe_showLoading;function awe_hideLoading(selector){$(selector).removeClass("loading");$(selector+' .loading-icon').remove();}window.awe_hideLoading=awe_hideLoading;function awe_showPopup(selector){$(selector).addClass('active');}window.awe_showPopup=awe_showPopup;function awe_hidePopup(selector){$(selector).removeClass('active');}window.awe_hidePopup=awe_hidePopup;function awe_convertVietnamese(str){str=str.toLowerCase();str=str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g,"a");str=str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g,"e");str=str.replace(/ì|í|ị|ỉ|ĩ/g,"i");str=str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g,"o");str=str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g,"u");str=str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g,"y");str=str.replace(/đ/g,"d");str=str.replace(/!|@|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\;|\'| |\"|\&|\#|\[|\]|~|$|_/g,"-");str=str.replace(/-+-/g,"-");str=str.replace(/^\-+|\-+$/g,"");return str;}window.awe_convertVietnamese=awe_convertVietnamese;function awe_category(){$('.nav-category .fa-angle-down').click(function(e){$(this).parent().toggleClass('active');});}window.awe_category=awe_category;function awe_menumobile(){$('.menu-bar').click(function(e){e.preventDefault();$('.cate-sidebar, .cate-overlay').toggleClass('open');});$('.cate-overlay').click(function(e){e.preventDefault();$('.cate-sidebar, .cate-overlay').toggleClass('open');});$('#nav .fa').click(function(e){e.preventDefault();$(this).parent().parent().toggleClass('open');});}window.awe_menumobile=awe_menumobile;function awe_accordion(){$('.accordion .nav-link').click(function(e){e.preventDefault;$(this).parent().toggleClass('active');})}window.awe_accordion=awe_accordion;function awe_owl(){$('.owl-carousel:not(.not-aweowl)').each(function(){var xss_item=$(this).attr('data-xss-items');var xs_item=$(this).attr('data-xs-items');var sm_item=$(this).attr('data-sm-items');var md_item=$(this).attr('data-md-items');var lg_item=$(this).attr('data-lg-items');var lgg_item=$(this).attr('data-lgg-items');var margin=$(this).attr('data-margin');var dot=$(this).attr('data-dot');var nav=$(this).attr('data-nav');if(typeof margin!==typeof undefined&&margin!==false){}else{margin=30;}
if(typeof xss_item!==typeof undefined&&xss_item!==false){}else{xss_item=1;}
if(typeof xs_item!==typeof undefined&&xs_item!==false){}else{xs_item=1;}
if(typeof sm_item!==typeof undefined&&sm_item!==false){}else{sm_item=3;}
if(typeof md_item!==typeof undefined&&md_item!==false){}else{md_item=3;}
if(typeof lg_item!==typeof undefined&&lg_item!==false){}else{lg_item=4;}
if(typeof lgg_item!==typeof undefined&&lgg_item!==false){}else{lgg_item=lg_item;}
if(typeof dot!==typeof undefined&&dot!==true){dot=dot;}else{dot=false;}
if(typeof nav!==typeof undefined&&nav!==true){nav=nav;}else{nav=false;}
$(this).owlCarousel({loop:false,margin:Number(margin),responsiveClass:true,dots:dot,nav:nav,responsive:{0:{items:Number(xss_item),margin:10},543:{items:Number(xs_item)},768:{items:Number(sm_item)},992:{items:Number(md_item)},1200:{items:Number(lg_item)},1500:{items:Number(lgg_item)}}})})}window.awe_owl=awe_owl;function awe_backtotop(){if($('.back-to-top').length){var scrollTrigger=100,backToTop=function(){var scrollTop=$(window).scrollTop();if(scrollTop>scrollTrigger){$('.back-to-top').addClass('show');}else{$('.back-to-top').removeClass('show');}
if(scrollTop>($(document).height()-700)){$('.back-to-top').addClass('end');}else{$('.back-to-top').removeClass('end');}};backToTop();$(window).on('scroll',function(){backToTop();});$('.back-to-top').on('click',function(e){e.preventDefault();$('html,body').animate({scrollTop:0},700);});}}window.awe_backtotop=awe_backtotop;function awe_tab(){$(".e-tabs:not(.not-dqtab)").each(function(){$(this).find('.tabs-title li:first-child').addClass('current');$(this).find('.tab-content').first().addClass('current');$(this).find('.tabs-title li').click(function(){var tab_id=$(this).attr('data-tab');var url=$(this).attr('data-url');$(this).closest('.e-tabs').find('.tab-viewall').attr('href',url);$(this).closest('.e-tabs').find('.tabs-title li').removeClass('current');$(this).closest('.e-tabs').find('.tab-content').removeClass('current');$(this).addClass('current');$(this).closest('.e-tabs').find("#"+tab_id).addClass('current');});});}window.awe_tab=awe_tab;function awe_callbackW(){iWishCheck();iWishCheckInCollection();$(".iWishAdd").click(function(){var iWishvId=iWish$(this).parents('form').find("[name='id']").val();if(typeof iWishvId==='undefined'){iWishvId=iWish$(this).parents('form').find("[name='variantId']").val();};var iWishpId=iWish$(this).attr('data-product');if(Bizweb.template=='collection'||Bizweb.template=='index'){iWishvId=iWish$(this).attr('data-variant');}
if(typeof iWishvId==='undefined'||typeof iWishpId==='undefined'){return false;}
if(iwish_cid==0){iWishGotoStoreLogin();}else{var postObj={actionx:'add',cust:iwish_cid,pid:iWishpId,vid:iWishvId};iWish$.post(iWishLink,postObj,function(data){if(iWishFindAndGetVal('#iwish_post_result',data)==undefined)return;var result=(iWishFindAndGetVal('#iwish_post_result',data).toString().toLowerCase()==='true');var redirect=parseInt(iWishFindAndGetVal('#iwish_post_redirect',data),10);if(result){if(Bizweb.template=="product"){iWish$('.iWishAdd').addClass('iWishHidden'),iWish$('.iWishAdded').removeClass('iWishHidden');if(redirect==2){iWishSubmit(iWishLink,{cust:iwish_cid});}}
else if(Bizweb.template=='collection'||Bizweb.template=='index'){iWish$.each(iWish$('.iWishAdd'),function(){var _item=$(this);if(_item.attr('data-variant')==iWishvId){_item.addClass('iWishHidden'),_item.parent().find('.iWishAdded').removeClass('iWishHidden');}});}}},'html');}
return false;});$(".iWishAdded").click(function(){var iWishvId=iWish$(this).parents('form').find("[name='id']").val();if(typeof iWishvId==='undefined'){iWishvId=iWish$(this).parents('form').find("[name='variantId']").val();};var iWishpId=iWish$(this).attr('data-product');if(Bizweb.template=='collection'||Bizweb.template=='index'){iWishvId=iWish$(this).attr('data-variant');}
if(typeof iWishvId==='undefined'||typeof iWishpId==='undefined'){return false;}
if(iwish_cid==0){iWishGotoStoreLogin();}else{var postObj={actionx:'remove',cust:iwish_cid,pid:iWishpId,vid:iWishvId};iWish$.post(iWishLink,postObj,function(data){if(iWishFindAndGetVal('#iwish_post_result',data)==undefined)return;var result=(iWishFindAndGetVal('#iwish_post_result',data).toString().toLowerCase()==='true');var redirect=parseInt(iWishFindAndGetVal('#iwish_post_redirect',data),10);if(result){if(Bizweb.template=="product"){iWish$('.iWishAdd').removeClass('iWishHidden'),iWish$('.iWishAdded').addClass('iWishHidden');}
else if(Bizweb.template=='collection'||Bizweb.template=='index'){iWish$.each(iWish$('.iWishAdd'),function(){var _item=$(this);if(_item.attr('data-variant')==iWishvId){_item.removeClass('iWishHidden'),_item.parent().find('.iWishAdded').addClass('iWishHidden');}});}}},'html');}
return false;});}window.awe_callbackW=awe_callbackW;$('.dropdown-toggle').click(function(){$(this).parent().toggleClass('open');});$('.btn-close').click(function(){$(this).parents('.dropdown').toggleClass('open');});$('body').click(function(event){if(!$(event.target).closest('.dropdown').length){$('.dropdown').removeClass('open');};});$(document).on('click','.qtyplus',function(e){e.preventDefault();fieldName=$(this).attr('data-field');var currentVal=parseInt($('input[data-field='+fieldName+']').val());if(!isNaN(currentVal)){$('input[data-field='+fieldName+']').val(currentVal+1);}else{$('input[data-field='+fieldName+']').val(0);}});$(document).on('click','.qtyminus',function(e){e.preventDefault();fieldName=$(this).attr('data-field');var currentVal=parseInt($('input[data-field='+fieldName+']').val());if(!isNaN(currentVal)&&currentVal>1){$('input[data-field='+fieldName+']').val(currentVal-1);}else{$('input[data-field='+fieldName+']').val(1);}});jQuery(document).ready(function($){$('#nav-mobile .fa').click(function(e){e.preventDefault();$(this).parent().parent().toggleClass('open');});$('.menu-bar').click(function(e){e.preventDefault();$('#nav-mobile').toggleClass('open');});$('.open-filters').click(function(e){$(this).toggleClass('open');$('.dqdt-sidebar').toggleClass('open');});$('.inline-block.account-dr>a').click(function(e){if($(window).width()<992){e.preventDefault();}})});$(document).on('click','.overlay, .close-popup, .btn-continue, .fancybox-close',function(){hidePopup('.awe-popup');setTimeout(function(){$('.loading').removeClass('loaded-content');},500);return false;})
$('.search-icon.inline-block.hidden-md.hidden-sm.hidden-lg .btn').click(function(e){$('.header>.relative form').slideToggle();});$(document).click(function(e){var checktt1=false;var checktt2=false;var container=$("nav .content");var container2=$(".header>.relative .header-left .menu-bar");if(!container.is(e.target)&&container.has(e.target).length===0){checktt1=true;}
if(!container2.is(e.target)&&container2.has(e.target).length===0){checktt2=true;}
if(checktt1==true&&checktt2==true){$('header nav').removeClass('open');}});window.onload=function(e){$('.bizweb-product-reviews-badge').each(function(e){var $this=$(this);setTimeout(function(){if($this[0].childElementCount==0){$this.addClass('hidden');}},1000);})}
$('.search_text').click(function(){$(this).next().slideToggle(200);$('.list_search').show();})
$('.list_search .search_item').on('click',function(e){$('.list_search').hide();var optionSelected=$(this);var title=optionSelected.text();$('.search_text').text(title);var h=$(".collection-selector").width()+10;$('.site-header form input').css('padding-left',h+'px');$(".search-text").focus();optionSelected.addClass('active').siblings().removeClass('active');});$('.header_search form button').click(function(e){if($(window).width()>992){e.preventDefault();searchCollection();setSearchStorage('.header_search form');}});$('#mb_search').click(function(){$('.mb_header_search').slideToggle('fast');});$('.fi-title.drop-down').click(function(){$(this).toggleClass('opentab');});function searchCollection(){var collectionId=$('.list_search .search_item.active').attr('data-coll-id');var searchVal=$('.header_search input[type="search"]').val();var url='';if(collectionId==0){url='/search?q='+searchVal;}
else{url='/search?q=collections:'+collectionId+' AND name:'+searchVal;}
window.location=url;}
function setSearchStorage(form_id){var seach_input=$(form_id).find('.search-text').val();var search_collection=$(form_id).find('.list_search .search_item.active').attr('data-coll-id');sessionStorage.setItem('search_input',seach_input);sessionStorage.setItem('search_collection',search_collection);}
function getSearchStorage(form_id){var search_input_st='';var search_collection_st='';if(sessionStorage.search_input!=''){search_input_st=sessionStorage.search_input;}
if(sessionStorage.search_collection!=''){search_collection_st=sessionStorage.search_collection;}
$(form_id).find('.search-text').val(search_input_st);$(form_id).find('.search_item[data-coll-id="'+search_collection_st+'"]').addClass('active').siblings().removeClass('active');var search_key=$(form_id).find('.search_item[data-coll-id="'+search_collection_st+'"]').text();if(search_key!=''){$(form_id).find('.collection-selector .search_text').text(search_key);}}
function resetSearchStorage(){sessionStorage.removeItem('search_input');sessionStorage.removeItem('search_collection');}
$('li.lev-1.nav-item.has-mega.mega-menu').hover(function(e){if($(window).width()>1200){var meh=$('.cate-sidebar').height();$('li.lev-1.nav-item.has-mega.mega-menu .mega-menu-content').css('min-height',meh+1);}})
$(window).load(function(){if($(window).width()>992){var meh=$('.cate-sidebar').height();$('li.lev-1.nav-item.has-mega.mega-menu .mega-menu-content').css('min-height',meh+1);getSearchStorage('.header_search form');resetSearchStorage();var h=$(".collection-selector").width()+10;$('.site-header form input').css('padding-left',h+'px');$('.bot-header-left').mouseover(function(e){$('.catogory-other-page').addClass('active');})}
$('body').mouseover(function(event){if(!$(event.target).closest('.bot-header-left').length&&!$(event.target).closest('.catogory-other-page').length){$('.catogory-other-page').removeClass('active');};});});$(".not-dqtab").each(function(e){var $this1=$(this);var datasection=$this1.closest('.not-dqtab').attr('data-section');$this1.find('.tabs-title li:first-child').addClass('current');$this1.find('.tab-content').first().addClass('current');$this1.find('.tabs-title.ajax li').click(function(){var $this2=$(this),tab_id=$this2.attr('data-tab'),url=$this2.attr('data-url');var etabs=$this2.closest('.e-tabs');etabs.find('.tab-viewall').attr('href',url);etabs.find('.tabs-title li').removeClass('current');etabs.find('.tab-content').removeClass('current');$this2.addClass('current');etabs.find("."+tab_id).addClass('current');if(!$this2.hasClass('has-content')){$this2.addClass('has-content');getContentTab(url,"."+datasection+" ."+tab_id);}});});$('.not-dqtab .next').click(function(e){var count=0
$(this).parents('.content').find('.tab-content').each(function(e){count+=1;})
var str=$(this).parent().find('.tab-titlexs').attr('data-tab'),res=str.replace("tab-",""),datasection=$(this).closest('.not-dqtab').attr('data-section');res=Number(res);if(res<count){var current=res+1;}else{var current=1;}
action(current,datasection);})
$('.not-dqtab .prev').click(function(e){var count=0
$(this).parents('.content').find('.tab-content').each(function(e){count+=1;})
var str=$(this).parent().find('.tab-titlexs').attr('data-tab'),res=str.replace("tab-",""),datasection=$(this).closest('.not-dqtab').attr('data-section'),res=Number(res);if(res>1){var current=res-1;}else{var current=count;}
action(current,datasection);})
function action(current,datasection){$('.'+datasection+' .tab-titlexs').attr('data-tab','tab-'+current);var text='',url='',tab_id='';$('.'+datasection+' ul.tabs.tabs-title.hidden-xs li').each(function(e){if($(this).attr('data-tab')=='tab-'+current){var $this3=$(this);title=$this3.find('span').text();url=$this3.attr('data-url');tab_id=$this3.attr('data-tab');if(!$this3.hasClass('has-content')){$this3.addClass('has-content');getContentTab(url,"."+datasection+" ."+tab_id);}}})
$("."+datasection+" .tab-titlexs span").text(title);$("."+datasection+" .tab-content").removeClass('current');$("."+datasection+" .tab-"+current).addClass('current');}
function ajaxCarousel(selector,dataLgg){$(selector+' .owl-carousel.ajax-carousel').each(function(){var xss_item=$(this).attr('data-xss-items');var xs_item=$(this).attr('data-xs-items');var sm_item=$(this).attr('data-sm-items');var md_item=$(this).attr('data-md-items');var lg_item=$(this).attr('data-lg-items');if(dataLgg!==typeof undefined){}
var lgg_item=dataLgg;var margin=$(this).attr('data-margin');var dot=$(this).attr('data-dot');var nav=$(this).attr('data-nav');if(typeof margin!==typeof undefined&&margin!==false){}else{margin=30;}
if(typeof xss_item!==typeof undefined&&xss_item!==false){}else{xss_item=1;}
if(typeof xs_item!==typeof undefined&&xs_item!==false){}else{xs_item=1;}
if(typeof sm_item!==typeof undefined&&sm_item!==false){}else{sm_item=3;}
if(typeof md_item!==typeof undefined&&md_item!==false){}else{md_item=3;}
if(typeof lg_item!==typeof undefined&&lg_item!==false){}else{lg_item=4;}
if(typeof lgg_item!==typeof undefined&&lgg_item!==false){}else{lgg_item=lg_item;}
if(typeof dot!==typeof undefined&&dot!==true){dot=dot;}else{dot=false;}
if(typeof nav!==typeof undefined&&nav!==true){nav=nav;}else{nav=false;}
$(this).owlCarousel({loop:false,margin:Number(margin),responsiveClass:true,dots:dot,nav:nav,responsive:{0:{items:Number(xss_item),margin:10},543:{items:Number(xs_item)},768:{items:Number(sm_item)},992:{items:Number(md_item)},1200:{items:Number(lg_item)},1500:{items:Number(lgg_item)}}})})}
$('li.lev-1.nav-item.has-mega.mega-menu .fa').click(function(e){e.preventDefault();})
$('.mega-item .h3 .fa').click(function(e){e.preventDefault();$(this).parent().parent().parent().toggleClass('open');})
$('.drop-mobile .fa').click(function(e){e.preventDefault();$(this).parent().parent().toggleClass('open');})
function fixfun($this){if($this.value=='')$this.value=1;}
$('.xemthem').click(function(e){e.preventDefault();$('ul.site-nav>li').css('display','block');$(this).hide();$('.thugon').show();})
$('.thugon').click(function(e){e.preventDefault();$('ul.site-nav>li').css('display','none');$(this).hide();$('.xemthem').show();})
window.onload=function(e){var lil=$('.section-category ul.site-nav .lev-1').length;var vw=$(window).width();if(lil<9&&vw<1500&&vw>1200){$('li.hidden-lgg').remove();}}
function preventNonNumericalInput(e){e=e||window.event;var charCode=(typeof e.which=="undefined")?e.keyCode:e.which;var charStr=String.fromCharCode(charCode);if(!charStr.match(/^[0-9]+$/))
e.preventDefault();}
if($(window).width()<=991){$(".header-acount").remove();}