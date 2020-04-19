import { Injectable } from '@angular/core';
import {AuthenticationService} from "../../core/service/authentication.service";
import {HttpClient} from "@angular/common/http";
import {Task} from "../schema/task";
import {Observable} from "rxjs";
import {environment} from "../../../environments/environment";
import {map} from "rxjs/operators";

@Injectable()
export class TaskService {

  constructor(
    private authenticationService: AuthenticationService,
    private http: HttpClient
  ) { }

  createTask(task: Task): Observable<Task>{
    const user = this.authenticationService.currentUserValue;
    return this.http.post<Task>(`${environment.apiUrl}/task/${user.id}`, task)
      .pipe(map(task => {
        task.startTime = new Date(task.startTime);
        return task;
      }));
  }
}
