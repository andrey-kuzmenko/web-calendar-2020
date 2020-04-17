import { Component, OnInit } from '@angular/core';
import {FormBuilder, FormGroup, Validators} from "@angular/forms";
import {ActivatedRoute, Router} from "@angular/router";
import {first} from "rxjs/operators";
import {AuthenticationService} from "../../../../core/service/authentication.service";
import {User} from "../../../../data/schema/user";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  loginForm: FormGroup;
  submitted = false;
  returnUrl: string;

  constructor(
    private _formBuilder: FormBuilder,
    private _router: Router,
    private _route: ActivatedRoute,
    private _authenticationService: AuthenticationService) {
    if (this._authenticationService.currentUserValue) {
      this._router.navigate(['/calendar']);
    }
  }

  ngOnInit() {
    this.loginForm = this._formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6), Validators.pattern(".*[a-zA-Z].*")]]
    });

    //this.returnUrl = this._route.snapshot.queryParams['returnUrl'] || '/';
  }

  get f() {
    return this.loginForm.controls;
  }


  onSubmit() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      console.log(this.loginForm);
      return;
    }

    const user: User = {
      email: this.loginForm.value.email,
      password: this.loginForm.value.password
    }

    //this.loading = true;
    this._authenticationService.login(user.email, user.password)
      .pipe(first())
      .subscribe(
        data => {
          //this._router.navigate([this.returnUrl]);
          this._router.navigate(["calendar"]);
        },
        error => {
          //this.error = error;
          //this.loading = false;
        });
  }
}
