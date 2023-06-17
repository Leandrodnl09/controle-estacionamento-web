$.noConflict();

jQuery(document).ready(function($) {

	"use strict";

	$('#menuToggle').on('click', function(event) {
		$('body').toggleClass('open');
	});

});