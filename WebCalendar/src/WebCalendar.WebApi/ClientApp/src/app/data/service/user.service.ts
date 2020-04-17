import {Injectable} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";
import {User} from "../schema/user";

const httpOptions = {
  headers: new HttpHeaders({ 'Content-Type': 'application/json' })
};

const baseUrl = environment.apiUrl;

@Injectable()
export class UserService {

  constructor(private http:HttpClient) { }

  public registerUser(user: User): Observable<object>{
    return this.http.post(`${baseUrl}/user/register`, user, httpOptions);
  }
}
