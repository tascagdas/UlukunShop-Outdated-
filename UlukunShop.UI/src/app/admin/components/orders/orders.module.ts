import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OrdersComponent } from './orders.component';
import {RouterModule} from "@angular/router";
import { ListComponent } from './list/list.component';
import {MatSidenavModule} from "@angular/material/sidenav";
import {MatFormFieldModule} from "@angular/material/form-field";
import {MatInputModule} from "@angular/material/input";
import {MatButtonModule} from "@angular/material/button";
import {MatPaginatorModule} from "@angular/material/paginator";
import {MatTableModule} from "@angular/material/table";
import {FileUploadModule} from "../../../services/common/file-upload/file-upload.module";
import {DialogModule} from "../../../dialogs/dialog.module";
import {DeleteDirectiveModule} from "../../../directives/admin/delete.directive.module";



@NgModule({
  declarations: [
    OrdersComponent,
    ListComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: '', component: OrdersComponent }
    ]),
    MatSidenavModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatPaginatorModule,
    MatTableModule,
    FileUploadModule,
    DialogModule,
    DeleteDirectiveModule
  ]
})
export class OrdersModule { }
