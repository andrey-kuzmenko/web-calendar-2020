import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {ReactiveFormsModule} from "@angular/forms";
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {HomeLayoutComponent} from "./layout/home-layout/home-layout.component";
import {UserService} from "./data/service/user.service";
import {JwtInterceptor} from "./core/interceptor/jwt.interceptor";
import {ErrorInterceptor} from "./core/interceptor/error.interceptor";
import { MainLayoutComponent } from './layout/main-layout/main-layout.component';
import {NgbModule} from "@ng-bootstrap/ng-bootstrap";

@NgModule({
  declarations: [
    AppComponent,
    HomeLayoutComponent,
    MainLayoutComponent
  ],
  imports: [
    NgbModule,
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [UserService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },],
  bootstrap: [AppComponent]
})
export class AppModule {
}
