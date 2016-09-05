$(document).ready(function(){
$("input[type='text'],input[type='password']").focus(function() {
		if(this.value == this.defaultValue) {
			this.value = "";
		}
	}).blur(function(){
		if(this.value.length == 0) {
			this.value = this.defaultValue;
		}
	});
})

// banner slider
$(document).ready(function(){
	$('.bxslider').bxSlider({
		auto: true,
		controls: false
	});
});

// on-click slider menu
$(document).ready(function(){
	$("#top-nav-mob").click(function () {
		$("#top-nav").slideToggle();
		$("#login").hide();
		$(".more-info").hide();
	});
	$("#login-mob").click(function () {
		$("#login").slideToggle();
		$("#top-nav").hide();
		$(".more-info").hide();
	});
	$("#more-info-mob").click(function () {
		$(".more-info").slideToggle();
		$("#top-nav").hide();
		$("#login").hide();
	});
});

// equal height
if (document.documentElement.clientWidth >= 768) {
	$(window).load(function(){
	//$(document).ready(function(){
		//set the starting bigestHeight variable
		var biggestHeight = 0;
		//check each of them
		$('.equal_height').each(function(){
			//if the height of the current element is
			//bigger then the current biggestHeight value
			if($(this).height() > biggestHeight){
				//update the biggestHeight with the
				//height of the current elements
				biggestHeight = $(this).height();
			}
		});
		//when checking for biggestHeight is done set that
		//height to all the elements
		$('.equal_height').height(biggestHeight);
	});
}

// hide address bar
if (!window.addEventListener) {
    window.attachEvent("load", function () {
        // Set a timeout...
        setTimeout(function () {
            // Hide the address bar!
            window.scrollTo(0, 1);
        }, 0);
    });
} else {
    window.addEventListener("load", function () {
        // Set a timeout...
        setTimeout(function () {
            // Hide the address bar!
            window.scrollTo(0, 1);
        }, 0);
    });
}

// pop up
$(document).ready(function(){
	$(".example5").colorbox();
	$("#reset-form").click(function(){ 
		$('#colorbox').css({"display":"block"});
	});
});