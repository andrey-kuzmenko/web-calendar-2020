import {Component, OnInit} from '@angular/core';
import {NotificationMiddlewareService} from "./core/service/notification-middleware.service";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  constructor(private notificationMiddleware: NotificationMiddlewareService) {
  }
  ngOnInit(): void {
    this.notificationMiddleware.init();
  }
}
