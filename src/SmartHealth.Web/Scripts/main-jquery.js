 $(document).ready( function(){	
		var buttons = { previous:$('#lofslidecontent45 .lof-previous') ,
						next:$('#lofslidecontent45 .lof-next') };
						
		$obj = $('#lofslidecontent45').lofJSidernews( { interval : 4000,
												direction		: 'opacity',	
											 	//easing		: 'easeOutBounce',
												duration		: 1200,
												auto		 	: false,
												maxItemDisplay  : 3,
												mainWidth:380,
												navPosition     : 'horizontal',
												navigatorHeight : 85,
												navigatorWidth  : 95,
												buttons			: buttons} );	
	});