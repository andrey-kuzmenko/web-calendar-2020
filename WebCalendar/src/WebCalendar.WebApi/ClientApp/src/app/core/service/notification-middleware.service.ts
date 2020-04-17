import { Injectable } from '@angular/core';
import {environment} from "../../../environments/environment";
import {HttpClient} from "@angular/common/http";
import {Subscription} from "../models/subscription";
import {AuthenticationService} from "./authentication.service";

@Injectable({
  providedIn: 'root'
})
export class NotificationMiddlewareService {

  public pushNotificationStatus = {
    isSubscribed: false,
    isSupported: false,
    isInProgress: false
  };
  private swRegistration = null;
  public notifications = [];

  constructor(
    private http: HttpClient,
    private authenticationService: AuthenticationService) { }

  init() {
    if ('serviceWorker' in navigator && 'PushManager' in window) {

      navigator.serviceWorker.register('/assets/sw.js')
        .then(swReg => {
          console.log('Service Worker is registered', swReg);

          this.swRegistration = swReg;
          this.checkSubscription();
        })
        .catch(error => {
          console.error('Service Worker Error', error);
        });
      this.pushNotificationStatus.isSupported = true;
    } else {
      this.pushNotificationStatus.isSupported = false;
    }

    navigator.serviceWorker.addEventListener('message', (event) => {
      this.notifications.push(event.data);
    });

  }


  pushSubscription(subscription: Subscription){
    console.log(subscription);
    const currentUser = this.authenticationService.currentUserValue;
    this.http.post<Subscription>(`${environment.apiUrl}/notification/subscribe/${currentUser.id}`, subscription)
      .subscribe();
  }

  pushUnsubscribe(){
    const currentUser = this.authenticationService.currentUserValue;
    this.http.delete(`${environment.apiUrl}/notification/unsubscribe/${currentUser.id}`)
      .subscribe();
  }

  checkSubscription() {
    this.swRegistration.pushManager.getSubscription()
      .then(subscription => {
        console.log(subscription);
        console.log(JSON.stringify(subscription));
        this.pushNotificationStatus.isSubscribed = !(subscription === null);
      });
  }

  toggleSubscription() {
    if (this.pushNotificationStatus.isSubscribed) {
      this.unsubscribeUser();
    } else {
      this.subscribeUser();
    }
  }

  subscribeUser() {
    this.pushNotificationStatus.isInProgress = true;
    //check the source code to get the method below
    const applicationServerKey = this.urlB64ToUint8Array(environment.notificationServerPublicKey);
    this.swRegistration.pushManager.subscribe({
      userVisibleOnly: true,
      applicationServerKey: applicationServerKey
    })
      .then(subscription => {
        const sub = JSON.parse(JSON.stringify(subscription));

        const pushSub: Subscription = {
          endpoint: sub.endpoint,
          auth: sub.keys.auth,
          p256dh: sub.keys.p256dh
        };

        this.pushSubscription(pushSub);

        this.pushNotificationStatus.isSubscribed = true;
      })
      .catch(err => {
        console.log('Failed to subscribe the user: ', err);
      })
      .then(() => {
        this.pushNotificationStatus.isInProgress = false;
      });
  }

  unsubscribeUser() {
    this.pushNotificationStatus.isInProgress = true;
    this.swRegistration.pushManager.getSubscription()
      .then(function (subscription) {
        if (subscription) {
          return subscription.unsubscribe();
        }
      })
      .catch(function (error) {
        console.log('Error unsubscribing', error);
      })
      .then(() => {
        this.pushNotificationStatus.isSubscribed = false;
        this.pushNotificationStatus.isInProgress = false;
      });

    this.pushUnsubscribe();
  }

  urlB64ToUint8Array(base64String) {
    const padding = '='.repeat((4 - base64String.length % 4) % 4);
    const base64 = (base64String + padding)
      .replace(/\-/g, '+')
      .replace(/_/g, '/');

    const rawData = window.atob(base64);
    const outputArray = new Uint8Array(rawData.length);

    for (let i = 0; i < rawData.length; ++i) {
      outputArray[i] = rawData.charCodeAt(i);
    }
    return outputArray;
  }
}
