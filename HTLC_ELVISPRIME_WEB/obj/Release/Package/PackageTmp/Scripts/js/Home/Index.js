﻿$(function () {
	var $visualSlide = $('.visual_slide');
	if ($visualSlide.length > 0) {
		var itemBgUrl = $(".visual_slide .item:eq(0) .bg").css('background-image').match(/^url\("?(.+?)"?\)$/)[1];
		$('<img>').attr('src', itemBgUrl).one('load', function () {
			$(".visual_slide").addClass("load");
			$visualSlide.on('init', function (event, slick) {

				var $item = $visualSlide.find(".item");
				var $itemActive = $visualSlide.find(".slick-active");
				var $lastdot = $('.visual .slick-dots li:last-of-type');
				$lastdot.removeClass("on");
				$item.removeClass("on");
				$item.find(".txt01").removeClass('fadeInDown');
				$item.find(".txt02").removeClass('fadeInUp');
				$itemActive.addClass("on");
				$itemActive.find(".txt01").addClass('fadeInDown');
				$itemActive.find(".txt02").addClass('fadeInUp');
				


			});
			$visualSlide.slick({
				dots: true,
				infinite: true,
				speed: 0,
				fade: true,
				arrows: false,
				autoplay: true,
				autoplaySpeed: 4000,
				pauseOnHover: false
			});
			$visualSlide.on('afterChange', function (event, slick, currentSlide, nextSlide) {
				var $item = $visualSlide.find(".item");
				var $itemActive = $visualSlide.find(".slick-active");
				$item.removeClass("on");
				$itemActive.addClass("on");
			});
		});

		$(document).on("click", ".pause", function (event) {
			$visualSlide.slick('slickPause');
			$(this).hide();
			$('.play').show();
		});
		$(document).on("click", ".play", function (event) {
			$visualSlide.slick('slickPlay');
			$(this).hide();
			$('.pause').show();
		});
		$(document).on("click", ".slick-dots > li", function (event) {
			$visualSlide.slick('slickPause');
			var slickTimer = setTimeout(function () {
				$visualSlide.slick('slickPlay');
				clearTimeout(slickTimer);
			}, 1000);
		});

		if (matchMedia("screen and (max-width: 767px)").matches) { // 모바일
			$('#mn_info .box:eq(0)').waypoint(function () {
				$('#mn_info .box:eq(0)').addClass('fadeInUp');
			}, { offset: '100%' });
			$('#mn_info .box:eq(1)').waypoint(function () {
				$('#mn_info .box:eq(1)').addClass('fadeInUp');
			}, { offset: '100%' });
			$('#mn_info .box:eq(2)').waypoint(function () {
				$('#mn_info .box:eq(2)').addClass('fadeInUp');
			}, { offset: '100%' });
		} else {
			$('#mn_info .animate').waypoint(function () {
				$('#mn_info .box.type1.animate').addClass('fadeInDown');
				$('#mn_info .box.type2.animate').addClass('fadeInUp');
				$('#mn_info .pc_txt.animate').addClass('fadeIn');
			}, { offset: '70%' });
		}

		//Test
		setTimeout(function () {			
			$(".bg").css("opacity", "1");
			$(".bg").css("visibility", "visible");
		}, 500);
	}

	/* twkim / 20200624 / swiper  */
	/* 1. 배경 + 세로 슬라이드 */
	var swiper_inbox = new Swiper('#swiper-container-inbox', {
		direction: 'vertical',
		slidesPerView: 1,
		spaceBetween: 30,
		mousewheel: true,
		pagination: {
			el: '.swiper-pagination',
			clickable: true,
		},
	});

	$('.quick_btn').on('click', function () {
		if ($('.quick_menu').hasClass('on') == true) {
			$('.quick_menu').removeClass('on');
			$('.quick_menu .quick_desc p').stop().slideUp(300, 'linear');
		}
		else {
			$('.quick_menu').addClass('on');
			$('.quick_menu .quick_desc p').stop().slideDown(300, 'linear');
		}
	});
});




//모바일 - 텍스트 클릭 시에도 페이지 이동 가능하게 수정
$(document).on("click", ".quick_list", function () {
	if (matchMedia("screen and (max-width: 767px)").matches) { // 모바일
		var vValue = $(this).attr("name").replace("quick_list", "");
		location.href = '/Business?index=' + vValue;
	}
});
