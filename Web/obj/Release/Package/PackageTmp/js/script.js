/***********************************************

Project		: Aba Admin Dashboard
Version		: 1.0
Last change : 12/12/2019
Author		: zarko-m

/************************************************/

( function ( $ ) {
    "use strict";

    $(function() {
    	darkModeToggle();
      	sideNavigationToggle();
  		sideNavigationDropdown();
  		smartphoneTrigger();
  		checkWidth();
      	darkToggleMenuFix();
      	fullcalendarMenuFix();
      	mobileSearch();
      	scrollArrows();
      	taskCheckmarkToggle();
      	mailClickableRow();
      	formInput();
      	triggerAnimations();
      	modalFix();
      	triggerAlert();
	});

	function darkModeToggle(){
		$('#dark-toggle .checkmark').on('click', function(){
			var bodyID = $('body').attr('id');
			if ( bodyID === 'day-mode' ) {
				$('body').attr('id', 'dark-mode');
			} else {
				$('body').attr('id', 'day-mode');
			}
		});
	}

	function sideNavigationToggle(){
		$('.sideNavToggle').on('click', function(){
			$('aside').toggleClass('side-nav-small');
			$('.contentWrapper').toggleClass('side-nav-small');
		});
	}

	function sideNavigationDropdown(){
		$(".dropdown-section").on('click',function(){
			$('.dropdown-section').removeClass('active');
		    $(this).toggleClass('active');
		    let nextSibling = $(this).next()
			if( nextSibling.hasClass('open')) {
				nextSibling.removeClass('open');
				$(this).removeClass('active');
			} else if (!nextSibling.hasClass('open')) {
				$('.dropdown-container').removeClass('open');
				nextSibling.addClass('open');
			} 
			$('.contentWrapper').on("mouseup",  function() {
		        nextSibling.removeClass('open');
		        $('.dropdown-section').removeClass('active');
			});
		});
	}

	function smartphoneTrigger() {
		$('.smartphone-menu-trigger').on('click', function(){
	    	$('.side-nav').toggleClass('smartphone-menu-active');
	    	$('.contentWrapper').addClass('blur');
	    });
	    $('.contentWrapper').on('click touchstart', function(){
	    	if($('.side-nav').hasClass('smartphone-menu-active')){
				$('.side-nav').toggleClass('smartphone-menu-active');
				$('.contentWrapper').removeClass('blur');
			}
		});
		$('.closeSideNav').on('click', function(){
	    	$('.side-nav').toggleClass('smartphone-menu-active');
	    	$('.contentWrapper').removeClass('blur');
	    });
	}

	function checkWidth(){
		$(window).on('load resize click', function(){
		    var win = $(this);
		    if (win.width() < 661) { 
		      	$('aside').removeClass('side-nav-small');
				$('.contentWrapper').removeClass('side-nav-small');
		    }else if (win.width() <  1075) {
		    	$('aside').addClass('side-nav-small');
				$('.contentWrapper').addClass('side-nav-small');	
		    }

		});
	}

	function darkToggleMenuFix() {
		$("#dark-toggle").on('click', function(e){
		   e.stopPropagation();
		})
	}

	function fullcalendarMenuFix() {
		$("#calendar").on('click', function(e){
		   e.stopPropagation();
		});
	}

	function mobileSearch() {
			$('#search').on('click', function(){ 		
		    var win = $(window).width();
			if (win < 661) {
		    	$('.mobile-search-active').css("display", "block");
		    } 
			});

	    $('.exit-search').on('click', function(){
	        $('.mobile-search-active').css("display", "none");
	    });
	}

	function scrollArrows() {
		$('.scroll-right').on('click', function(){
			$('.team-members-wrapper').animate({
			scrollLeft: "+=280px"
			}, "slow");
		});

		$('.scroll-left').on('click', function(){
			$('.team-members-wrapper').animate({
			scrollLeft: "-=280px"
			}, "slow");
		});
	}

	function taskCheckmarkToggle() {
		$('.task-checkmark').on('click', function(){
			$(this).closest('.task-item').toggleClass('task-muted');
		});
	}

	function mailClickableRow(){
		$(".clickable-row").on('click', function(){
	        window.location = "view-mail.html"
	        console.log("Radi");
	    });
	}

	function formInput() {
		$(".txtb input").on("focus",function(){
			$(this).addClass("focus");
		});

		$(".txtb input").on("blur",function(){
			if($(this).val() == "")
			$(this).removeClass("focus");
		});
	}

	function triggerAnimations() {
		$('.animation-btn').on('click', function(){
			var button_text = $(this).text();
			$(this).parent().parent().parent().find('img').addClass(button_text)
				.delay(1500)
				.queue(function( next ){
				    $(this).removeClass(button_text); 
				    next();
				});
	    });
	}

	function modalFix() {
		$('.modal').on('show.bs.modal', function (e) {
		    $('.contentWrapper').css("z-index", "auto");
		});
		$('.modal').on('hidden.bs.modal', function (e) {
		    $('.contentWrapper').css("z-index", "1");
		});
	}

	function triggerAlert() {		
		$('#triggerNotification').on('click', function(){
			$('.notifications-message').html('<div class="alert-icon"><div class="alert alert-success animated tada" style="font-weight: 500;"role="alert"><i class="dripicons dripicons-checkmark"></i>You are the best! Awesome job! <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button></div></div>');
	    });
	}
})(jQuery);

