import {Component, OnInit, ViewChild} from '@angular/core';
import {Router} from "@angular/router";
import {AuthenticationService} from "../../../../core/service/authentication.service";
import {FullCalendarComponent} from "@fullcalendar/angular";
import dayGridPlugin from "@fullcalendar/daygrid";
import {EventInput} from "@fullcalendar/core/structs/event";

@Component({
  selector: 'app-calendar',
  templateUrl: './calendar.component.html',
  styleUrls: ['./calendar.component.scss']
})
export class CalendarComponent implements OnInit {

  @ViewChild('calendar') calendarComponent: FullCalendarComponent;

  calendarPlugins = [dayGridPlugin];

  calendarSettings:any = {
    height: "parent",
    firstDay: 1
  }
  calendarEvents: EventInput[] = [
    {
      id: 1,
      title: "Test task",
      date: new Date(),
    }
  ];

  constructor(
    private _authService: AuthenticationService,
    private _router: Router
  ) { }

  ngOnInit(): void {
    //let calendarApi = this.calendarComponent.getApi();
  }

  logout(){
    this._authService.logout();
    this._router.navigate(['/login']);
  }

}
