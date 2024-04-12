import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthorizeMenuComponent } from './authorize-menu.component';
import {MatTreeModule} from "@angular/material/tree";
import {RouterModule} from "@angular/router";
import {MatButtonModule} from "@angular/material/button";
import {MatIconModule} from "@angular/material/icon";



@NgModule({
  declarations: [
    AuthorizeMenuComponent
  ],
  imports: [
    CommonModule,
    MatTreeModule,
    RouterModule.forChild([
      { path: "", component: AuthorizeMenuComponent }
    ]),
     MatIconModule, MatButtonModule
  ]
})
export class AuthorizeMenuModule { }
