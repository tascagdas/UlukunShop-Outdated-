import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {RouterModule} from "@angular/router";
import {HomeComponent} from "./home.component";



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild([
      {path:"",component:HomeComponent}
    ]),
    CommonModule
  ]
})
export class HomeModule { }
