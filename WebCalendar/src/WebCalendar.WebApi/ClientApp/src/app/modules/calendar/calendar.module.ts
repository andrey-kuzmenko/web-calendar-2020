import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {CalendarRoutingModule} from "./calendar-routing.module";
import {CalendarComponent} from "./pages/calendar/calendar.component";
import {FullCalendarModule} from "@fullcalendar/angular";
import { MainComponent } from './pages/main/main.component';
import { SettingsComponent } from './pages/settings/settings.component';
import {PushNotificationComponent} from "./components/push-notification/push-notification.component";
import { EmailNotificationComponent } from './components/email-notification/email-notification.component';



@NgModule({
    declarations: [
        CalendarComponent,
        PushNotificationComponent,
        MainComponent,
        SettingsComponent,
        EmailNotificationComponent
    ],
  imports: [
    CommonModule,
    CalendarRoutingModule,
    FullCalendarModule
  ]
})
export class CalendarModule { }
