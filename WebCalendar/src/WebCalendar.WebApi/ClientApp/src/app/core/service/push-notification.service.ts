import {Injectable, NgZone, OnInit} from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {AuthenticationService} from "./authentication.service";
import {mergeMapTo} from "rxjs/operators";
import {AngularFireMessaging} from "@angular/fire/messaging";
import {BehaviorSubject} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class PushNotificationService implements OnInit {

  public pushNotificationStatus = {
    isSubscribe: false,
    inProgress: false
  }
  public pushNotificationStatusEmitter$ = new BehaviorSubject<{
    isSubscribe: boolean,
    inProgress: boolean
  }>(this.pushNotificationStatus);

  //public notifications = [];

  constructor(
    private http: HttpClient,
    private authenticationService: AuthenticationService,
    private afMessaging: AngularFireMessaging,
    private zone:NgZone) {

    this.checkSubscribe();

    //replace by in-app notify
    afMessaging.onMessage(payload => {
      console.log(payload);
      const title = payload.notification.title
      const options = {
        body: payload.notification.body,
        icon: payload.notification.icon
      };
      new Notification(title, options);
    });
  }

  checkSubscribe() {
    this.pushNotificationStatus.isSubscribe = localStorage.getItem("pushNotificationToken") != null;
    this.pushNotificationStatusEmitter$.next(this.pushNotificationStatus);
  }

  ngOnInit(): void {

  }

  init() {
    /*if(this.isSubscribeInit()){
      return;
    }*/
    this.pushNotificationStatus.inProgress = true;
    this.pushNotificationStatusEmitter$.next(this.pushNotificationStatus);

    this.afMessaging.requestPermission
      .pipe(mergeMapTo(this.afMessaging.tokenChanges))
      .subscribe(
        (token) => {
          console.log('Permission granted! Save to the server!', token);

          localStorage.setItem("pushNotificationInit", JSON.stringify(true));
          this.pushSubscribe();
        },
        (error) => {
          console.error(error);
        },
      );
  }

  isSubscribeInit(): boolean {
    return localStorage.getItem("pushNotificationInit") != null;
  }

  pushSubscribe() {

    const currentUser = this.authenticationService.currentUserValue;

    this.afMessaging.getToken.subscribe(token => {
      localStorage.setItem("pushNotificationToken", JSON.stringify(token));
      this.zone.run(() => this.checkSubscribe());

      this.http.post(`${environment.apiUrl}/notification/push/subscribe/${currentUser.id}`,
        {deviceToken: token})
        .subscribe(result => {
          this.zone.run(() => {
            this.pushNotificationStatus.inProgress = false;
            this.pushNotificationStatusEmitter$.next(this.pushNotificationStatus);
          });
        });
    });
  }

  pushUnsubscribe() {
    this.pushNotificationStatus.inProgress = true;
    this.pushNotificationStatusEmitter$.next(this.pushNotificationStatus);

    const currentUser = this.authenticationService.currentUserValue;

    this.afMessaging.getToken.subscribe(token => {
      this.afMessaging.deleteToken(token);
      localStorage.removeItem("pushNotificationToken");
      this.checkSubscribe();

      this.http.post(`${environment.apiUrl}/notification/push/unsubscribe/${currentUser.id}`,
        {deviceToken: token})
        .subscribe(result => {
          this.zone.run(() => {
            this.pushNotificationStatus.inProgress = false;
            this.pushNotificationStatusEmitter$.next(this.pushNotificationStatus);
          });
        });
    });
  }

  toggleSubscription() {
    if (this.pushNotificationStatus.isSubscribe) {
      this.pushUnsubscribe();
    } else {
      this.init();
    }
  }
}
