import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HomeComponent } from './home.component';
import {RouterModule} from "@angular/router";
import { HeroComponent } from './hero/hero.component';
import { HomeProductsComponent } from './home-products/home-products.component';



@NgModule({
  declarations: [
    HomeComponent,
    HeroComponent,
    HomeProductsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {path: '', component: HomeComponent},
    ])
  ]
})
export class HomeModule { }
