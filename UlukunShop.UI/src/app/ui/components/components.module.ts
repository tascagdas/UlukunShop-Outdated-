import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ProductsModule} from "./products/products.module";
import {ShoppingcartsModule} from "./shoppingcarts/shoppingcarts.module";
import {HomeModule} from "./home/home.module";



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    ProductsModule,
    ShoppingcartsModule,
    HomeModule
  ]
})
export class ComponentsModule { }
