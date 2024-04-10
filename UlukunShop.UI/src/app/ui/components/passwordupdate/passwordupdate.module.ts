import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PasswordupdateComponent } from './passwordupdate.component';
import {RouterModule} from "@angular/router";



@NgModule({
  declarations: [
    PasswordupdateComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: "", component: PasswordupdateComponent }
    ]),
  ]
})
export class PasswordupdateModule { }
