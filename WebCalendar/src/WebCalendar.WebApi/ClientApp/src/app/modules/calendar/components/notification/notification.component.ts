import { Component, OnInit } from '@angular/core';
import {NotificationMiddlewareService} from "../../../../core/service/notification-middleware.service";

@Component({
  selector: 'app-notification',
  templateUrl: './notification.component.html',
  styleUrls: ['./notification.component.scss']
})
export class NotificationComponent implements OnInit {

  constructor(public notificationMiddleware: NotificationMiddlewareService) { }

  ngOnInit(): void {
  }

  toggleSubscription() {
    this.notificationMiddleware.toggleSubscription();
  }
}
