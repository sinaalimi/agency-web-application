(function($) {
	"use strict";

	$(".color1" ).click(function(){
		$("#colors" ).attr("href", "Content/Site/css/skin/color1.css" );
		return false;
	});

	$(".color2" ).click(function(){
		$("#colors" ).attr("href", "Content/Site/css/skin/color2.css" );
		return false;
	});

	$(".color3" ).click(function(){
		$("#colors" ).attr("href", "Content/Site/css/skin/color3.css" );
		return false;
	});

	$(".color4" ).click(function(){
		$("#colors" ).attr("href", "Content/Site/css/skin/color4.css" );
		return false;
	});

	$(".color5" ).click(function(){
		$("#colors" ).attr("href", "Content/Site/css/skin/color5.css" );
		return false;
	});

	$(".color6" ).click(function(){
		$("#colors" ).attr("href", "Content/Site/css/skin/color6.css" );
		return false;
	});
	
	$(".color7" ).click(function(){
		$("#colors" ).attr("href", "Content/Site/css/skin/color7.css" );
		return false;
	});
	
	$(".color8" ).click(function(){
		$("#colors" ).attr("href", "Content/Site/css/skin/color8.css" );
		return false;
	});
	$(".color9" ).click(function(){
		$("#colors" ).attr("href", "Content/Site/css/skin/color9.css" );
		return false;
	});
	$(".color10" ).click(function(){
		$("#colors" ).attr("href", "Content/Site/css/skin/color10.css" );
		return false;
	});
	$(".color11" ).click(function(){
		$("#colors" ).attr("href", "Content/Site/css/skin/color11.css" );
		return false;
	});
	$(".color12" ).click(function(){
		$("#colors" ).attr("href", "Content/Site/css/skin/color12.css" );
		return false;
	});
	
	

	// Color Switcher	
	$('#color-switcher').animate({
		left: '-255px'
	});

	$('#color-switcher h2 a').click(function(e){
		e.preventDefault();
		var div = $('#color-switcher');
		if (div.css('left') === '-255px') {
			$('#color-switcher').animate({
				left: '0px'
			}); 
		} else {
			$('#color-switcher').animate({
				left: '-255px'
			});
		}
	});

	$('.colors li a').click(function(e){
		e.preventDefault();
		$('#color-switcher').animate({
			left: '-255px'
		});
		$(this).parent().parent().find('a').removeClass('active');
		$(this).addClass('active');
	});
    
})(jQuery);

