var images = [
	'images/b-nau.png',
	'images/b-xam.png',
	'images/b-xanh.png',
	
	'images/nau-b.png',
	'images/xam-b.png',
	'images/xanh-b.png',
	
	'images/cent-nau.png',
	'images/cent-xam.png',
	'images/cent-xanh.png',
	
	'images/dau-nau.png',	
	'images/dau-xam.png',
	'images/dau-xanh.png',
	
	'images/nau-0.png',
	'images/xam-0.png',
	'images/xanh-0.png'
];



$(document).ready(function() {

	//Default Action
	$(".tab_content").hide(); //Hide all content
	$("ul.tabs li:first").addClass("active").show(); //Activate first tab
	$(".tab_content:first").show(); //Show first tab content
	
	//On Click Event
	$("ul.tabs li").click(function() {
		$("ul.tabs li").removeClass("active"); //Remove any "active" class
		$(this).addClass("active"); //Add "active" class to selected tab
		$(".tab_content").hide(); //Hide all tab content
		var activeTab = $(this).find("a").attr("href"); //Find the rel attribute value to identify the active tab + content
		//$(activeTab).fadeIn(); //Fade in the active content
		$(activeTab).show() ;
		return false;
	});

	$(images).each(function() {
		//var image = $('<img />').attr('src', this);
	});
});