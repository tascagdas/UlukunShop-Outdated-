import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from "@angular/router";
import {CustomerComponent} from "./customer.component";



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      {path:"",component:CustomerComponent}
    ]),
    CommonModule
  ]
})
export class CustomerModule { }
