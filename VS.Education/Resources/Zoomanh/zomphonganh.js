
(function($) {
	$.fn.extend({
		zomphonganh: function() {
			$('.pa-loading').hide();
			$('.phonganhct').each(function() {
				$(this).addClass('sp-touch');
				var thumbQty = $('a', this).length;

				// If more than one image
				if (thumbQty > 1) {
					var firstLarge,firstThumb,
						defaultImage = $('a.sp-default', this)[0]?true:false;
					$(this).append('<div class="zz-large"></div><div class="sp-thumbs sp-tb-active"></div>');
					$('a', this).each(function(index) {
						var thumb = $('img', this).attr('src'),
							large = $(this).attr('href'),
							classes = '';
						//set default image
						if((index === 0 && !defaultImage) || $(this).hasClass('sp-default')){
							classes = ' class="sp-current"';
							firstLarge = large;
							firstThumb = $('img', this)[0].src;
						}
						$(this).parents('.phonganhct').find('.sp-thumbs').append('<a href="' + large + '" style="background-image:url(' + thumb + ')"'+classes+'></a>');
						$(this).remove();
					});
					$('.zz-large', this).append('<a href="' + firstLarge + '" class="sp-current-big"><img src="' + firstThumb + '" alt="" /></a>');
					$('.phonganhct').css('display', 'inline-block');
				// If only one image
				} else {
					$(this).append('<div class="zz-large"></div>');
					$('a', this).appendTo($('.zz-large', this)).addClass('.sp-current-big');
					$('.phonganhct').css('display', 'inline-block');
				}
			});


			// Prevent clicking while things are happening
			$(document.body).on('click', '.sp-thumbs', function(event) {
				event.preventDefault();
			});


			// Is this a touch screen or not?
			$(document.body).on('mouseover', function(event) {
				$('.phonganhct').removeClass('sp-touch').addClass('zz-non-touch');
				event.preventDefault();
			});

			$(document.body).on('touchstart', function() {
				$('.phonganhct').removeClass('zz-non-touch').addClass('sp-touch');
			});

			// Clicking a thumbnail
			$(document.body).on('click', '.sp-tb-active a', function(event) {

				event.preventDefault();
				$(this).parent().find('.sp-current').removeClass();
				$(this).addClass('sp-current');
				$(this).parents('.phonganhct').find('.sp-thumbs').removeClass('sp-tb-active');
				$(this).parents('.phonganhct').find('.sp-zoom').remove();

				var currentHeight = $(this).parents('.phonganhct').find('.zz-large').height(),
					currentWidth = $(this).parents('.phonganhct').find('.zz-large').width();
				$(this).parents('.phonganhct').find('.zz-large').css({
					overflow: 'hidden',
					height: currentHeight + 'px',
					width: currentWidth + 'px'
				});

				$(this).addClass('sp-current').parents('.phonganhct').find('.zz-large a').remove();

				var nextLarge = $(this).parent().find('.sp-current').attr('href'),
					nextThumb = get_url_from_background($(this).parent().find('.sp-current').css('backgroundImage'));

				$(this).parents('.phonganhct').find('.zz-large').html('<a href="' + nextLarge + '" class="sp-current-big"><img src="' + nextThumb + '"/></a>');
				$(this).parents('.phonganhct').find('.zz-large').hide().fadeIn(250, function() {

					var autoHeight = $(this).parents('.phonganhct').find('.zz-large img').height();

					$(this).parents('.phonganhct').find('.zz-large').animate({
						height: autoHeight
					}, 'fast', function() {
						$('.zz-large').css({
							height: 'auto',
							width: 'auto'
						});
					});

					$(this).parents('.phonganhct').find('.sp-thumbs').addClass('sp-tb-active');
				});
			});

			// Zoom In non-touch
			$(document.body).on('mouseenter', '.zz-non-touch .zz-large', function(event) {
				var largeUrl = $('a', this).attr('href');
				$(this).append('<div class="sp-zoom"><img src="' + largeUrl + '"/></div>');
				$(this).find('.sp-zoom').fadeIn(250);
				event.preventDefault();
			});

			// Zoom Out non-touch
			$(document.body).on('mouseleave', '.zz-non-touch .zz-large', function(event) {
				$(this).find('.sp-zoom').fadeOut(250, function() {
					$(this).remove();
				});
				event.preventDefault();
			});

			



			// Close on Esc
			$(document).keydown(function(e) {
				if (e.keyCode == 27) {
					closeModal();
					return false;
				}
			});

		


			// Panning zoomed image (non-touch)

			$('.zz-large').mousemove(function(e) {
				var viewWidth = $(this).width(),
					viewHeight = $(this).height(),
					viewOffset = $(this).offset(),
					largeWidth = $(this).find('.sp-zoom').width(),
					largeHeight = $(this).find('.sp-zoom').height(),
					relativeXPosition = (e.pageX - viewOffset.left),
					relativeYPosition = (e.pageY - viewOffset.top),
					moveX = Math.floor((relativeXPosition * (viewWidth - largeWidth) / viewWidth)),
					moveY = Math.floor((relativeYPosition * (viewHeight - largeHeight) / viewHeight));

				$(this).find('.sp-zoom').css({
					left: moveX,
					top: moveY
				});

			});

			function get_url_from_background(bg){
				return bg.match(/url\([\"\']{0,1}(.+)[\"\']{0,1}\)+/i)[1];
			}
		}
	});
})(jQuery);
