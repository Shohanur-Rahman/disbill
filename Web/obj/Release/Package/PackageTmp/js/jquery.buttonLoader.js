(function ($) {
    $.fn.buttonLoader = function (action, loadingText) {
        if (!loadingText)
            loadingText = '<span class="spinner"><i class="fa fa-spinner fa-spin"></i></span> Loading';
        else
            loadingText = '<span class="spinner"><i class="fa fa-spinner fa-spin"></i></span> ' + loadingText;
        var self = $(this);
        //start loading animation
        if (action == 'start') {
            if ($(self).attr("disabled") == "disabled") {
                e.preventDefault();
            }
            setTimeout(function () {
                //disable buttons when loading state
                $('.has-spinner').attr("disabled", "disabled");
            }, 100);
            
            $(self).attr('data-btn-text', $(self).text());
            //binding spinner element to button and changing button text
            $(self).html(loadingText);
            $(self).addClass('active');
        }
        //stop loading animation
        if (action == 'stop') {
            $(self).html($(self).attr('data-btn-text'));
            $(self).removeClass('active');
            //enable buttons after finish loading
            $('.has-spinner').removeAttr("disabled");
        }
    }
})(jQuery);