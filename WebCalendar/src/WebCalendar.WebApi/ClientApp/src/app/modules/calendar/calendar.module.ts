import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {CalendarRoutingModule} from "./calendar-routing.module";
import {CalendarComponent} from "./pages/calendar/calendar.component";
import {FullCalendarModule} from "@fullcalendar/angular";
import {NotificationComponent} from "./components/notification/notification.component";



@NgModule({
    declarations: [
        CalendarComponent,
        NotificationComponent
    ],
  imports: [
    CommonModule,
    CalendarRoutingModule,
    FullCalendarModule
  ]
})
export class CalendarModule { }
