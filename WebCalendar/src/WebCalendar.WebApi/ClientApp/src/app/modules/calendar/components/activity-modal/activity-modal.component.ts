import {Component, Input, OnInit} from '@angular/core';
import {NgbActiveModal, NgbDate} from "@ng-bootstrap/ng-bootstrap";
import {Task} from "../../../../data/schema/task";
import {Calendar} from "@fullcalendar/core";

@Component({
  selector: 'app-activity-modal',
  templateUrl: './activity-modal.component.html',
  styleUrls: ['./activity-modal.component.scss']
})
export class ActivityModalComponent implements OnInit {

  @Input() userCalendars: Array<Calendar>;

  taskContent: Task;
  activity = "task";

  constructor(public activeModal: NgbActiveModal) {
  }

  ngOnInit(): void {
    this.taskContent = {
      title: "",
      description: "",
      date: new Date(),
      calendarId: "calendarId"
    }

    console.log(this.userCalendars);
  }

  save() {
    switch (this.activity) {
      case "task": {
        console.log(this.taskContent);
      }
    }
  }

  setTaskDate($event: NgbDate) {
    this.taskContent.date.setFullYear($event.year, $event.month - 1, $event.day);
  }

  setTaskTime($event: { hour: number; minute: number }) {
    this.taskContent.date.setHours($event.hour, $event.minute);
  }
}
