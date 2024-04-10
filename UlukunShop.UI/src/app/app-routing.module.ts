import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {LayoutComponent} from "./admin/layout/layout.component";
import {DashboardComponent} from "./admin/components/dashboard/dashboard.component";
import {HomeComponent} from "./ui/components/home/home.component";
import {AuthGuard} from "./guards/common/auth.guard";

const routes: Routes = [
  {path:"admin",component:LayoutComponent,children:[
      {path:"",component:DashboardComponent,canActivate:[AuthGuard]},
      {path:"customers",loadChildren:()=>import("./admin/components/customers/customers.module").then(module=>module.CustomersModule),canActivate:[AuthGuard]},
      {path:"products",loadChildren:()=>import("./admin/components/products/products.module").then(module=>module.ProductsModule),canActivate:[AuthGuard]},
      {path:"orders",loadChildren:()=>import("./admin/components/orders/orders.module").then(module=>module.OrdersModule),canActivate:[AuthGuard]},
    ],canActivate:[AuthGuard]},
  {path:"",component:HomeComponent},
  {path:"shoppingcart",loadChildren:()=>import("./ui/components/shoppingcarts/shoppingcarts.module").then(module=>module.ShoppingcartsModule)},
  {path:"products",loadChildren:()=>import("./ui/components/products/products.module").then(module=>module.ProductsModule)},
  {path:"products/:page",loadChildren:()=>import("./ui/components/products/products.module").then(module=>module.ProductsModule)},
  {path:"register",loadChildren:()=>import("./ui/components/register/register.module").then(module=>module.RegisterModule)},
  {path:"login",loadChildren:()=>import("./ui/components/login/login.module").then(module=>module.LoginModule)},
  {path:"contact",loadChildren:()=>import("./ui/components/contact/contact.module").then(module=>module.ContactModule)}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
