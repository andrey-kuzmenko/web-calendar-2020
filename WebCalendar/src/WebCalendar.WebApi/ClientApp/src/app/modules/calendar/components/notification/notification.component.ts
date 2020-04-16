import { Component, OnInit } from '@angular/core';
import {PushNotificationService} from "../../../../core/service/push-notification.service";

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss']
})
export class NotificationComponent implements OnInit {

  constructor(public pushNotificationService: PushNotificationService) {

  }

  ngOnInit(): void {
  }

  toggleSubscription() {
    this.pushNotificationService.toggleSubscription();
  }
}
