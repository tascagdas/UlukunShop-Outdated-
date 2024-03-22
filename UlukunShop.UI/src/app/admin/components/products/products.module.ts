import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductsComponent } from './products.component';
import {RouterModule} from "@angular/router";
import {MatSidenavModule} from "@angular/material/sidenav";



@NgModule({
  declarations: [
    ProductsComponent
  ],
    imports: [
        CommonModule,
        RouterModule.forChild([
            {path: '', component: ProductsComponent}
        ]),
        MatSidenavModule
    ]
})
export class ProductsModule { }
