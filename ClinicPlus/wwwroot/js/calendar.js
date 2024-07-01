document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        plugins: ['interaction', 'dayGrid'],
        header: false,
        defaultView: 'dayGridMonth',
        editable: true,
        locale: 'pt-br',
        defaultDate: Date.now(),
        events: [
            {
                title: 'Business Lunch',
                start: '2024-06-03T13:00:00',
                constraint: 'businessHours'
            },
            {
                title: 'Meeting',
                start: '2024-06-13T11:00:00',
                constraint: 'availableForMeeting', // defined below
                color: '#257e4a'
            },
            {
                title: 'Conference',
                start: '2024-06-18',
                end: '2024-06-20'
            },
            {
                title: 'Party Date',
                start: '2024-06-29T20:00:00'
            },

            // areas where "Meeting" must be dropped
            {
                groupId: 'availableForMeeting',
                start: '2024-06-11T10:00:00',
                end: '2024-06-11T16:00:00',
                display: 'background'
            },
            {
                groupId: 'availableForMeeting',
                start: '2024-06-13T10:00:00',
                end: '2024-06-13T16:00:00',
                display: 'background'
            },

            // red areas where no events can be dropped
            {
                title:'Teste 10',
                start: '2024-06-24',
                end: '2024-06-28',
                overlap: false,
                display: 'background',
                color: '#ff9f89'
            },
            {
                title: 'meu evento teste',
                start: '2024-06-06T08:30:00',
                end: '2024-06-06T12:00:00',
                overlap: false,
                display: 'background',
                color: '#ff9f89'
            },
            {
                title: 'meu evento teste 2',
                start: '2024-06-06T08:30:00',
                end: '2024-06-06T09:00:00',
                overlap: false,
                display: 'background',
                color: '#ff9f89'
            }
        ],
        viewRender: (view) => {
            let date
            switch (view.type) {
                case 'dayGridDay':
                    date = view.start.format('DD dddd YYYY')
                    break
                case 'dayGridWeek':
                    date = view.start.format('MMMM')
                    break
                case 'dayGridMonth':
                    date = view.start.format('MMMM')
                    break
            }
            $('#title').text(view.date);
        },
    });


    calendar.render();
    // Next/Prev buttons
    $("#next").on('click', function() {
        calendar.next('next')
    });
    $("#prev").on('click', function() {
        calendar.prev('prev')
    });

    // Select
    $("#select").on('change', function (event) {
        calendar.changeView(this.value)
    })

});