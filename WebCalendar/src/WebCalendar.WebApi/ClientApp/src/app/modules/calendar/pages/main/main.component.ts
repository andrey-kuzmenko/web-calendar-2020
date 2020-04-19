import {Component, OnInit} from '@angular/core';
import {PushNotificationService} from "../../../../core/service/push-notification.service";
import {NgbModal} from "@ng-bootstrap/ng-bootstrap";
import {ActivityModalComponent} from "../../components/activity-modal/activity-modal.component";
import {CalendarService} from "../../../../data/service/calendar.service";
import {Calendar} from "../../../../data/schema/calendar";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  userCalendars: Array<Calendar>;

  constructor(private pushNotificationService: PushNotificationService,
              private modalService: NgbModal,
              private calendarService: CalendarService) {
  }

  ngOnInit(): void {
      if(!this.pushNotificationService.isSubscribeInit()) {
        this.pushNotificationService.init();
      }

      this.calendarService.getUserCalendars().subscribe(calendars => this.userCalendars = calendars);
  }

  openAddEventModal() {
    const activityModalRef = this.modalService.open(ActivityModalComponent);
    activityModalRef.componentInstance.userCalendars = this.userCalendars;
  }
}
