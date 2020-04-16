import {Component, OnInit} from '@angular/core';
import {PushNotificationService} from "../../../../core/service/push-notification.service";

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  constructor(private pushNotificationService: PushNotificationService) {
  }

  ngOnInit(): void {
      if(!this.pushNotificationService.isSubscribeInit()) {
        this.pushNotificationService.init();
      }
  }
}
