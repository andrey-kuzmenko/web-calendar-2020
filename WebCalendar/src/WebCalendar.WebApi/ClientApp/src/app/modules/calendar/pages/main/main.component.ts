import {Component, OnInit} from '@angular/core';
import {NotificationMiddlewareService} from "../../../../core/service/notification-middleware.service";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  constructor(private notificationMiddleware: NotificationMiddlewareService) {
  }

  ngOnInit(): void {
    if (!this.notificationMiddleware.isSubscribeInit()) {
      this.notificationMiddleware.toggleSubscription();
    }
  }
}
