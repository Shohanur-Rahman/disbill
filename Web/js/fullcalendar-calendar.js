$(document).ready(function() {


/* initialize the external events
	-----------------------------------------------------------------*/

	$('#external-events .fc-event').each(function() {

		// store data so the calendar knows to render an event upon drop
		$(this).data('event', {
			title: $.trim($(this).text()), // use the element's text as the event title
			stick: true, // maintain when user navigates (see docs on the renderEvent method)
			id: $(this).attr('id'),
            color: $(this).data('color')
		});

		// make the event draggable using jQuery UI
		$(this).draggable({
			zIndex: 999,
			revert: true,      // will cause the event to go back to its
			revertDuration: 0  //  original position after the drag
		});

	});

	/* initialize the calendar
	-----------------------------------------------------------------*/

	$('#calendar-full').fullCalendar({
		header: {
			left: 'prev,next today',
			center: 'title',
			right: 'month,agendaWeek,agendaDay'
		},
		editable: true,
		events: [
				{
					title: 'All Day Event',
					start: '2019-09-01',
					color: '#1ce3bb'
				},
				{
					title: 'Long Event',
					start: '2019-09-07',
					end: '2019-09-09',
					color: '#f4ce00'
				},
				{
					id: 999,
					title: 'Repeating Event',
					start: '2019-09-09T16:00:00',
					color: '#4c84ff'
				},
				{
					id: 999,
					title: 'Repeating Event',
					start: '2019-09-16T16:00:00',
					color: '#ff4683'
				},
				{
					title: 'Conference',
					start: '2019-09-11',
					end: '2019-09-13',
					color: '#ff4683'
				},
				{
					title: 'Meeting',
					start: '2019-09-12T09:30:00',
					end: '2019-09-12T12:30:00',
					color: '#4c84ff'
				},
				{
					title: 'Lunch',
					start: '2019-09-12T12:00:00',
					color: '#f4ce00'
				},
				{
					title: 'Meeting',
					start: '2019-09-22T14:30:00',
					color: '#f4ce00'
				},
				{
					title: 'Dinner',
					start: '2019-09-09T20:00:00',
					color: '#f4ce00'
				},
				{
					title: 'Birthday Party',
					start: '2019-09-13T07:00:00',
					color: '#4c84ff'
				},
				{
					title: 'Click for Google',
					url: 'http://google.com/',
					start: '2019-09-28',
					color: '#4c84ff'
				},
				{
					title: 'All Day Event',
					start: '2019-09-01',
					color: '#1ce3bb'
				},
				{
					id: 999,
					title: 'Repeating Event',
					start: '2019-09-09T16:00:00',
					color: '#ff4683'
				},
				{
					id: 999,
					title: 'Repeating Event',
					start: '2019-09-16T16:00:00',
					color: '#ff4683'
				},
				{
					title: 'Meeting',
					start: '2019-09-12T09:30:00',
					end: '2019-09-12T12:30:00',
					color: '#4c84ff'
				},
				{
					title: 'Lunch',
					start: '2019-09-17T12:00:00',
					color: '#4c84ff'
				},
				{
					title: 'Meeting',
					start: '2019-09-15T14:30:00',
					color: '#4c84ff'
				},
				{
					title: 'Happy Hour',
					start: '2019-09-14T17:30:00',
					color: '#ff4683'
				},
				{
					title: 'Click for Google',
					url: 'http://google.com/',
					start: '2019-09-28',
					color: '#f4ce00'
				},

		
				{
					title: 'All Day Event',
					start: '2019-10-11',
					color: '#1ce3bb'
				},
				{
					id: 999,
					title: 'Repeating Event',
					start: '2019-10-16T16:00:00',
					color: '#f4ce00'
				},
				{
					title: 'Meeting',
					start: '2019-10-12T10:30:00',
					end: '2019-10-12T12:30:00',
					color: '#f4ce00'
				},
				{
					title: 'Lunch',
					start: '2019-10-12T12:00:00',
					color: '#4c84ff'
				},
				{
					title: 'Meeting',
					start: '2019-10-12T14:30:00',
					color: '#4c84ff'
				},
				{
					title: 'Happy Hour',
					start: '2019-10-12T17:30:00',
					color: '#ff4683'
				},
				{
					title: 'Click for Google',
					url: 'http://google.com/',
					start: '2019-10-28',
					color: '#f4ce00'
				},

				{
					title: 'All Day Event',
					start: '2019-11-01',
					color: '#1ce3bb'
				},
				{
					title: 'Long Event',
					start: '2019-11-07',
					end: '2019-11-11',
					color: '#f4ce00'
				},
				{
					id: 999,
					title: 'Repeating Event',
					start: '2019-11-11T16:00:00',
					color: '#4c84ff'
				},
				{
					id: 999,
					title: 'Repeating Event',
					start: '2019-11-16T16:00:00',
					color: '#ff4683'
				},
				{
					title: 'Conference',
					start: '2019-11-11',
					end: '2019-11-13',
					color: '#ff4683'
				},
				{
					title: 'Meeting',
					start: '2019-11-12T11:30:00',
					end: '2019-11-12T12:30:00',
					color: '#4c84ff'
				},
				{
					title: 'Lunch',
					start: '2019-11-12T12:00:00',
					color: '#f4ce00'
				},
				{
					title: 'Meeting',
					start: '2019-11-22T14:30:00',
					color: '#f4ce00'
				},
				{
					title: 'Dinner',
					start: '2019-11-11T20:00:00',
					color: '#f4ce00'
				},
				{
					title: 'Birthday Party',
					start: '2019-11-13T07:00:00',
					color: '#4c84ff'
				},
				{
					title: 'Click for Google',
					url: 'http://google.com/',
					start: '2019-11-28',
					color: '#4c84ff'
				},
				{
					title: 'All Day Event',
					start: '2019-11-01',
					color: '#1ce3bb'
				},
				{
					id: 999,
					title: 'Repeating Event',
					start: '2019-11-11T16:00:00',
					color: '#ff4683'
				},
				{
					id: 999,
					title: 'Repeating Event',
					start: '2019-11-16T16:00:00',
					color: '#ff4683'
				},
				{
					title: 'Meeting',
					start: '2019-11-12T11:30:00',
					end: '2019-11-12T12:30:00',
					color: '#4c84ff'
				},
				{
					title: 'Lunch',
					start: '2019-11-17T12:00:00',
					color: '#4c84ff'
				},
				{
					title: 'Meeting',
					start: '2019-11-15T14:30:00',
					color: '#4c84ff'
				},
				{
					title: 'Happy Hour',
					start: '2019-11-14T17:30:00',
					color: '#ff4683'
				},
				{
					title: 'Click for Google',
					url: 'http://google.com/',
					start: '2019-11-28',
					color: '#f4ce00'
				},

			],
		droppable: true, // this allows things to be dropped onto the calendar
		drop: function() {
			// is the "remove after drop" checkbox checked?
			if ($('#drop-remove').is(':checked')) {
				// if so, remove the element from the "Draggable Events" list
				$(this).remove();
			}
		}
	});
});