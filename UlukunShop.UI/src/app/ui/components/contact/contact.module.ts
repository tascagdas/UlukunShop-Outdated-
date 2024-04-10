import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ContactComponent} from './contact.component';
import {RouterModule} from "@angular/router";
import {LoginComponent} from "../login/login.component";


@NgModule({
  declarations: [
    ContactComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path: '', component: ContactComponent}
    ])
  ]
})
export class ContactModule {
}
