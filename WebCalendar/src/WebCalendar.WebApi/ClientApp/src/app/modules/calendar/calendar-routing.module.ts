import {RouterModule, Routes} from "@angular/router";
import {NgModule} from "@angular/core";
import {CalendarComponent} from "./pages/calendar/calendar.component";

const routes: Routes = [
  {
    path: '',
    component: CalendarComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CalendarRoutingModule { }
