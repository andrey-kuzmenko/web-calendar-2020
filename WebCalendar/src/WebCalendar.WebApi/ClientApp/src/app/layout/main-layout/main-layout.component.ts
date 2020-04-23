import { Component, OnInit } from '@angular/core';
import {AuthenticationService} from '../../core/service/authentication.service';
import {PushNotificationService} from '../../core/service/push-notification.service';

@Component({
    selector: 'app-main-layout',
    templateUrl: './main-layout.component.html',
    styleUrls: ['./main-layout.component.scss']
})
export class MainLayoutComponent implements OnInit {

    constructor(public authenticationService: AuthenticationService,
                private pushNotificationService: PushNotificationService) { }

    ngOnInit(): void {
    }

    logout() {
        this.pushNotificationService.pushUnsubscribe()
            .subscribe(() => this.authenticationService.logout());

    }
}
