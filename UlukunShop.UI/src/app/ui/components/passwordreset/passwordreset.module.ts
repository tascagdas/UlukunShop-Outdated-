import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {PasswordresetComponent} from './passwordreset.component';
import {RouterModule} from "@angular/router";


@NgModule({
  declarations: [
    PasswordresetComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path: "", component: PasswordresetComponent}
    ]),
  ]
})
export class PasswordresetModule {
}
