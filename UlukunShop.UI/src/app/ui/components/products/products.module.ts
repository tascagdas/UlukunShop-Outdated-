import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {ProductsComponent} from './products.component';
import {RouterModule} from "@angular/router";
import {ListComponent} from './list/list.component';
import {MatSidenavModule} from "@angular/material/sidenav";
import {FilterComponent} from './filter/filter.component';


@NgModule({
  declarations: [
    ProductsComponent,
    ListComponent,
    FilterComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path: '', component: ProductsComponent},
    ]),
    MatSidenavModule
  ]
})
export class ProductsModule {
}
