import {AfterViewInit, Component, OnInit} from '@angular/core';
import {NgbCalendar, NgbDate} from "@ng-bootstrap/ng-bootstrap";

@Component({
  selector: 'app-datepicker-popup',
  templateUrl: './datepicker-popup.component.html',
  styleUrls: ['./datepicker-popup.component.scss']
})
export class DatepickerPopupComponent{

  date: NgbDate;

  constructor(private calendar: NgbCalendar) {
    this.date = calendar.getToday();
  }
}
