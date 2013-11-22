
	// Click Cấp 1
	function initMenu() {
    $('ul.cap2').hide();
    $('ul.cap1 > li.active').children('ul').show();
	/*$('ul.cap1 > li.active').children('ul').slideUp('normal');*/	
    $('ul.cap1 > li > a').click(
    function() {
    var checkElement = $(this).next();
    if((checkElement.is('ul')) && (checkElement.is(':visible'))) {
	checkElement.slideUp('normal');
    return false;
    }
   if((checkElement.is('ul')) && (!checkElement.is(':visible'))) {
   $('ul.cap2:visible').slideUp('normal');
   checkElement.slideDown('normal');
   return false;
   }
   }
   );
   }
   
   // Click Cấp 2
   function initMenu2() {
    $('ul.cap3').hide();

    $('ul.cap2 > li > a').click(
    function() {
    var checkElement = $(this).next();
    if((checkElement.is('ul')) && (checkElement.is(':visible'))) {
	checkElement.slideUp('normal');
    return false;
    }
   if((checkElement.is('ul')) && (!checkElement.is(':visible'))) {
   $('ul.cap3:visible').slideUp('normal');
   checkElement.slideDown('normal');
   return false;
   }
   }
   );
   }
   
   // Click Cấp 3
   function initMenu3() {
    $('ul.cap4').hide();
    $('ul.cap3 > li > a').click(
    function() {
    var checkElement = $(this).next();
    if((checkElement.is('ul')) && (checkElement.is(':visible'))) {
	checkElement.slideUp('normal');
    return false;
    }
   if((checkElement.is('ul')) && (!checkElement.is(':visible'))) {
   $('ul.cap4:visible').slideUp('normal');
   checkElement.slideDown('normal');
   return false;
   }
   }
   );
   }
   
   // Click Cấp 4
   function initMenu4() {
    $('ul.cap5').hide();

    $('ul.cap4 > li > a').click(
    function() {
    var checkElement = $(this).next();
    if((checkElement.is('ul')) && (checkElement.is(':visible'))) {
	checkElement.slideUp('normal');
    return false;
    }
   if((checkElement.is('ul')) && (!checkElement.is(':visible'))) {
   $('ul.cap5:visible').slideUp('normal');
   checkElement.slideDown('normal');
   return false;
   }
   }
   );
   }
   
   // Click Cấp 5
   function initMenu5() {
    $('ul.cap6').hide();

    $('ul.cap5 > li > a').click(
    function() {
    var checkElement = $(this).next();
    if((checkElement.is('ul')) && (checkElement.is(':visible'))) {
	checkElement.slideUp('normal');
    return false;
    }
   if((checkElement.is('ul')) && (!checkElement.is(':visible'))) {
   $('ul.cap6:visible').slideUp('normal');
   checkElement.slideDown('normal');
   return false;
   }
   }
   );
   }
   
   
   
   /**/

   $(document).ready(function() {
	   initMenu5();
	   initMenu4();
	   initMenu3();
	   initMenu2();
	   initMenu();
	   
	   });