$(document).ready(function(){
    $("input[type=text]").focus(function () {
		if(this.value == this.defaultValue) {
			this.value = "";
		}
	}).blur(function(){
		if(this.value.length == 0) {
			this.value = this.defaultValue;
		}
	});
})

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
