particlesJS.load('particles-js', '/Scripts/particles.json');

$(document).ready(function() {
	var pathname = window.location.pathname;
	$('.navbar-nav > li > a[href="'+pathname+'"]').parent().addClass('active');
	$('.dropdown-menu > li > a[href="'+pathname+'"]').parent().parent().closest('li').addClass('active');
});

window.setTimeout(function() {
    $(".alert-autohide").fadeTo(500, 0).slideUp(500, function(){
        $(this).remove(); 
    });
}, 4000);