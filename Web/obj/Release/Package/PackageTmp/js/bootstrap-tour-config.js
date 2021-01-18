(function(){
	var name = "Anthony";
	var tour = new Tour({
		storage : false,
		keyboard: true,
	});

	tour.addSteps([
	  {
	    element: ".tour-step-one",
	    placement: "bottom",
	    backdrop: true,
	    title: "<p>Welcome!</p>",
	    content: "<img src='img/logo.png'><br>We are so glad you decided to try out our website tour. <br><br> <span>Let's start</span>, how we can call you? <br><br><input autofocus placeholder='Jonathan, John etc...' class='form-control' type='text' maxlength='12' name='your_name'>",
	    onNext : function(tour){
	    	var nameProvided = $("input[name=your_name]").val();
	    	if ($.trim(nameProvided) !== ""){
	    		name = nameProvided;
	    		$('h1 span').html( name ).css('textTransform', 'capitalize');
	    	}
	    },
	  },
	  {
	    element: "#dark-toggle",
	    placement: "bottom",
	    title: "<p>Dark mode is here!</p>",
	    backdrop: true,
		onShow: function(tour) {
			$( ".user-profile" ).children('div').addClass('show');
		},
		onHide: function(tour) {
			$( ".user-profile" ).children('div').removeClass('show');
		},
	    content: function(){ return "<img src='img/logo.png'><br>Change colors in one click! <span>Dark mode</span> turns the light surfaces of the page dark, creating an experience ideal for night.<br><br> <span>Try it out!</span>" }
	  },	  
	  {
	    element: ".sideNavToggle",
	    placement: "bottom",
	    backdrop: false,
	    title: "<p>Side navbar</p>",
	    content: "The side menu will convert itself into a compressed version after the toggle button click.",
	  },

	  {
	    element: ".nav-tour",
	    backdrop: false,
	    placement: "bottom",
	    orphan: true,
	    title: function(){ return "<p>You are ready, " + name + "!</p>" },
	    content: "If you want to start tour again, just click this button! <br><br>Have a <span>nice</span> day!"
	  },
	]);


	tour.init();

		$("#start-tour").click(function() {
		  tour.restart();
	});

}());


