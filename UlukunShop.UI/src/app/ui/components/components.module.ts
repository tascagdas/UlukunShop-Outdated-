import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ProductsModule} from "./products/products.module";
import {ShoppingcartsModule} from "./shoppingcarts/shoppingcarts.module";
import {HomeModule} from "./home/home.module";
import {RegisterModule} from "./register/register.module";
import { LoginComponent } from './login/login.component';
import {LoginModule} from "./login/login.module";
import {ContactModule} from "./contact/contact.module";
import { UiFooterComponent } from './ui-footer/ui-footer.component';
import { ThankyouComponent } from './thankyou/thankyou.component';
import {ThankyouModule} from "./thankyou/thankyou.module";
import {RouterLinkWithHref} from "@angular/router";



@NgModule({
  declarations: [

    UiFooterComponent
  ],
  exports: [
    UiFooterComponent
  ],
    imports: [
        CommonModule,
        ProductsModule,
        ShoppingcartsModule,
        HomeModule,
        RegisterModule,
        ContactModule,
        ThankyouModule,
        RouterLinkWithHref
    ]
})
export class ComponentsModule { }
