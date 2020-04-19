import {
  AfterContentChecked,
  AfterContentInit,
  AfterViewInit,
  Component,
  EventEmitter,
  OnInit,
  ViewChild
} from '@angular/core';
import {Router} from "@angular/router";
import {AuthenticationService} from "../../../../core/service/authentication.service";
import {FullCalendarComponent} from "@fullcalendar/angular";
//import dayGridPlugin from "@fullcalendar/daygrid";
import timeGridPlugin from '@fullcalendar/timegrid';
import {EventInput} from "@fullcalendar/core/structs/event";

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent implements OnInit, AfterViewInit{

  @ViewChild('calendar') calendarComponent: FullCalendarComponent;

  calendarPlugins = [timeGridPlugin];

  calendarSettings = {
    height: "parent",
    firstDay: 1,
    eventTimeFormat: {
      hour: '2-digit',
      minute: '2-digit',
      hour12: false
    }
  }
  calendarEvents: EventInput[] = [
    {
      id: 1,
      title: "Test task",
      date: new Date(),
      className: "done"
    }
  ];

  constructor(
    private _router: Router
  ) { }

  ngOnInit(): void {

    //let calendarApi = this.calendarComponent.getApi();
  }

  ngAfterViewInit(): void {

  }

  eventRender($event: any) {
    /*console.log($event);
    $event.el.style.textDecoration = "line-through";*/
  }
}
