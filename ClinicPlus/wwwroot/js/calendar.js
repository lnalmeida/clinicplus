document.addEventListener('DOMContentLoaded', function () {
    var calendarEl = document.getElementById('calendar');
    var calendar = new FullCalendar.Calendar(calendarEl, {
        plugins: ['interaction', 'dayGrid'],
        header: false,
        defaultView: 'dayGridMonth',
        editable: true,
        locale: 'pt-br',
        defaultDate: Date.now(),
        events: function (start, end, timezone, callback) {
            $.ajax({
                url: '@Url.Action("LoadCalendar","Consultas")',
                type: 'GET',
                success: function(response){
                    var eventes = [];
                    $(response).each(function (){
                        events.push({
                            id: this.id,
                            title: this.eventTitle,
                            start: this.startEvent,
                            end: this.endEvent,
                            color: this.color
                        });         
                    });
                    callback(events);
                }
            });
        },
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