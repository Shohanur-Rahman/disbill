$(document).ready(function() {
    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();
    $('#calendar').fullCalendar({
        defaultView: 'month',
        contentHeight: 'auto',
        dayNamesShort: ['S', 'M', 'T', 'W', 'T', 'F', 'S'],
        header: {
            left: 'prev, next today',
            center: 'title',
            right: 'month, basicWeek, basicDay'
        },


       
        
    });

});