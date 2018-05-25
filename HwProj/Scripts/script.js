particlesJS.load('particles-js', '/Scripts/particles.json');

function mouseOverPasswordInput(obj) {
        var obj = document.getElementById('password-input');
        obj.type = "text";
        document.getElementById("toggle-icon").classList.remove('fa-eye-slash');
        document.getElementById("toggle-icon").classList.add('fa-eye');
}
function mouseOutPasswordInput(obj) {
	var obj = document.getElementById('password-input');
	obj.type = "password";
	document.getElementById("toggle-icon").classList.remove('fa-eye');
	document.getElementById("toggle-icon").classList.add('fa-eye-slash');
}