$(document).ready(function() {
	$(".topMenuAction-hot").click( function() {
		if ($("#openCloseIdentifier-hot").is(":hidden")) {
			$("#slider-hot").animate({marginTop: "-70px"}, 500 );
			$("#topMenuImage-hot").html('<img src="images/open.png" />');
			$("#openCloseIdentifier-hot").show();
		} else {
			$("#slider-hot").animate({marginTop: "0px"}, 500 );
			$("#topMenuImage-hot").html('<img src="images/close.png" />');
			$("#openCloseIdentifier-hot").hide();
		}
	});  
})