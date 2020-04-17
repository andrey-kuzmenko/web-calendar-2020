import {Component, OnInit, ViewChild} from '@angular/core';
import dayGridPlugin from '@fullcalendar/daygrid';
import {FullCalendarComponent} from "@fullcalendar/angular";
import {EventStore} from "@fullcalendar/core/structs/event-store";
import {EventInput} from "@fullcalendar/core/structs/event";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {



  constructor() { }

  ngOnInit() {


  }
}
