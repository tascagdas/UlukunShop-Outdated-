import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {DeleteDialogComponent} from "./delete-dialog/delete-dialog.component";
import {FileUploadDialogComponent} from "./file-upload-dialog/file-upload-dialog.component";
import {MatDialogModule} from "@angular/material/dialog";
import {MatButtonModule} from "@angular/material/button";
import { SelectProductImageDialogComponent } from './select-product-image-dialog/select-product-image-dialog.component';
import {FileUploadModule} from "../services/common/file-upload/file-upload.module";
import {MatCardModule} from '@angular/material/card';
import {FormsModule} from "@angular/forms";
import { ShoppingCartItemRemoveDialogComponent } from './shopping-cart-item-remove-dialog/shopping-cart-item-remove-dialog.component';
import { ShoppingCartCompleteDialogComponent } from './shopping-cart-complete-dialog/shopping-cart-complete-dialog.component';
import { OrderDetailDialogComponent } from './order-detail-dialog/order-detail-dialog.component';
import {MatToolbarModule} from "@angular/material/toolbar";
import {MatTableModule} from "@angular/material/table";
import { CompleteOrderDialogComponent } from './complete-order-dialog/complete-order-dialog.component';
import { AuthorizeMenuDialogComponent } from './authorize-menu-dialog/authorize-menu-dialog.component';
import {MatBadgeModule} from "@angular/material/badge";
import {MatListModule} from "@angular/material/list";
import { AuthorizeUserDialogComponent } from './authorize-user-dialog/authorize-user-dialog.component';



@NgModule({
  declarations: [
    DeleteDialogComponent,
    SelectProductImageDialogComponent,
    ShoppingCartItemRemoveDialogComponent,
    ShoppingCartCompleteDialogComponent,
    OrderDetailDialogComponent,
    CompleteOrderDialogComponent,
    AuthorizeMenuDialogComponent,
    AuthorizeUserDialogComponent
  ],
    imports: [
        CommonModule,
        MatDialogModule,
        MatButtonModule,
        FileUploadModule,
        MatCardModule,
        MatToolbarModule,
        MatTableModule,
        MatBadgeModule,
        MatListModule,
    ]
})
export class DialogModule { }
