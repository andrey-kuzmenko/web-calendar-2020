import {Component, Input, OnInit, TemplateRef, ViewChild} from '@angular/core';
import {Router} from '@angular/router';
import {FullCalendarComponent} from '@fullcalendar/angular';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import interactionPlugin from '@fullcalendar/interaction';
import {EventInput} from '@fullcalendar/core/structs/event';
import {Calendar} from '../../../../data/schema/calendar';
import {TaskService} from '../../../../data/service/task.service';
import {OutsidePlacement, RelativePosition, Toppy, ToppyControl} from 'toppy';

@Component({
    selector: 'app-calendar',
    templateUrl: './calendar.component.html',
    styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent implements OnInit {

    @Input() calendars: Array<Calendar>;

    @ViewChild('calendar') calendarComponent: FullCalendarComponent;

    currentEventsPopover: ToppyControl;
    currentTaskId: string;
    @ViewChild('tpl') tpl: TemplateRef<any>;

    calendarPlugins = [timeGridPlugin, dayGridPlugin, interactionPlugin];

    calendarSettings = {
        height: 'parent',
        firstDay: 1,
        eventTimeFormat: {
            hour: '2-digit',
            minute: '2-digit',
            hour12: false
        },
        nowIndicator: true,
        header: {
            left: 'timeGridDay,timeGridWeek,dayGridMonth'
        }

    };

    calendarEvents: EventInput[] = [];

    constructor(
        private router: Router,
        private taskService: TaskService,
        private toppy: Toppy
    ) {
    }

    ngOnInit(): void {
        // this.updateEvents();
        // let calendarApi = this.calendarComponent.getApi();
    }

    eventRender($event: any) {
        $event.el.style.cursor = 'pointer';
        if ($event.event.extendedProps.isDone) {
            $event.el.style.textDecoration = 'line-through';
            $event.el.style.backgroundColor = 'gray';
        }
    }

    // bad way
    public updateEvents(calendars: Array<Calendar>) {
        this.calendars = calendars;
        this.taskService.getAllTasks(this.calendars[0].id).subscribe(tasks => {
            this.calendarEvents = tasks.map(value => {
                const event: EventInput = {
                    id: value.id,
                    title: value.title,
                    date: value.startTime,
                    textColor: 'white',
                    borderColor: '#00a9ff',
                    backgroundColor: '#00a9ff',
                    isDone: value.isDone
                };
                return event;
            });
        });
    }

    onEventClick($event: any) {
        if (this.currentEventsPopover) {
            this.currentEventsPopover.close();
        }

        this.currentTaskId = $event.event.id;

        const position = new RelativePosition({
            placement: OutsidePlacement.TOP,
            src: $event.el,
        });
        console.log($event);
        const overlay = this.toppy
            .position(position)
            .config({
                containerClass: 'event-popup-container',
                wrapperClass: 'event-popup-wrapper'
            })
            .content(this.tpl)
            .create();

        this.currentEventsPopover = overlay;

        overlay.listen('t_close').subscribe(value => {
            this.updateEvents(this.calendars);
        });

        overlay.open();
    }

    onDateClick($event: any) {
        if (this.currentEventsPopover) {
            this.currentEventsPopover.close();
        }
    }
}
