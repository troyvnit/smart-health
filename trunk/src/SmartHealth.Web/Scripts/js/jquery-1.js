$(function () {
$('.bubbleInfo').each(function () {
var distance = 10;
var time = 120;
var hideDelay = 20;
var hideDelayTimer = null;
var beingShown = false;
var shown = false;
var trigger = $('.trigger', this);
var info = $('.popup', this).css('opacity', 0);

$(trigger).mouseover(function () {
	$(this).parents(".bubbleInfo").find(".tip2",0).hide() ;
}) ;

$([trigger.get(0), info.get(0)]).mouseover(function () 
{
	
	/*position=z-index*/
	/*$(function() {
	var zIndexNumber = 1000;
	$('div').each(function() {
		$(this)('.popup').css('zIndex', zIndexNumber);
		zIndexNumber -= 10;
	});
	});*/

	if (hideDelayTimer) clearTimeout(hideDelayTimer);
	if (beingShown || shown) {
	// don't trigger the animation again
	return;
	} else {
	// reset position of info box
	beingShown = true;
	info.css({
	top:20,
	left:10,
	display: 'block'
	}).animate({
	top: '-=' + distance + 'px',
	opacity: 1
	}, time, 'swing', function() {
	beingShown = false;
	shown = true;
	});
	}
	return false;
}).mouseout(function () {

if (hideDelayTimer) clearTimeout(hideDelayTimer);
hideDelayTimer = setTimeout(function () {
hideDelayTimer = null;
info.animate({
top: '-=' + distance + 'px',
opacity: 0
}, time, 'swing', function () {
shown = false;
info.css('display', 'none');
});
}, hideDelay);
return false;
});
});
});