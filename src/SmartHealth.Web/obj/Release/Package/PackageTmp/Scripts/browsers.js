// detect browsers
function browsersD(){
	var browsersD = {};
	var ua = navigator.userAgent;
	browsersD.MAC = (/Macintos/).test(ua);
	browsersD.WIN = (/Window/).test(ua);
	browsersD.SF = (/Safari/).test(ua);
	browsersD.FF = (/Mozilla/).test(ua);
	browsersD.Opera = (/Opera/).test(ua);
	browsersD.IE = (/MSIE/).test(ua);
	browsersD.IE6 = (/MSIE 6\.0/).test(ua);
	browsersD.IE7 = (/MSIE 7\.0/).test(ua);
	browsersD.IE8 = (/MSIE 8\.0/).test(ua);
	browsersD.iPad = navigator.userAgent.match(/iPad/i);
	browsersD.iPhone = navigator.userAgent.match(/iPhone/i);
	
	// add class to <html> tag
	if((/iPhone OS/).test(ua) || (/iPad OS/).test(ua)){
		jQuery('html').addClass('ios');
	}
	if((/OS 6/).test(ua)){
		jQuery('html').addClass('ios6');
	}

	if((/Chrome/).test(ua)){
		jQuery('html').addClass('chrome');
	} else {
		if((/Firefox/).test(ua)){
			jQuery('html').addClass('firefox');
		} else {
			if((/Safari/).test(ua)){
				jQuery('html').addClass('safari');
			}
		}
	}
	
	return browsersD;
}

// detect browsers
var global=[];
global["BD"] = browsersD();