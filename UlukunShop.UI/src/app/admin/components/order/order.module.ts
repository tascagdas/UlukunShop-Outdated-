import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from "@angular/router";
import {OrderComponent} from "./order.component";



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      {path:"",component:OrderComponent}
    ]),
    CommonModule
  ]
})
export class OrderModule { }
