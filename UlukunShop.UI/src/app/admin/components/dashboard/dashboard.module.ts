import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from "@angular/router";
import {DashboardComponent} from "./dashboard.component";



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
    {path:"",component:DashboardComponent}
  ]),
    CommonModule
  ]
})
export class DashboardModule { }
