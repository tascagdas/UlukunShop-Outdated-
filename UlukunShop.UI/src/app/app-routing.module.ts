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
      {path: "authorize-menu", loadChildren: () => import("./admin/components/authorize-menu/authorize-menu.module").then(module => module.AuthorizeMenuModule), canActivate: [AuthGuard] },
      {path: "roles", loadChildren: () => import("./admin/components/role/role.module").then(module => module.RoleModule), canActivate: [AuthGuard] },
      {path: "users", loadChildren: () => import("./admin/components/user/user.module").then(module => module.UserModule), canActivate: [AuthGuard] }
    ],canActivate:[AuthGuard]},
  {path:"",component:HomeComponent},
  {path:"shoppingcart",loadChildren:()=>import("./ui/components/shoppingcarts/shoppingcarts.module").then(module=>module.ShoppingcartsModule)},
  {path:"products",loadChildren:()=>import("./ui/components/products/products.module").then(module=>module.ProductsModule)},
  {path:"products/:page",loadChildren:()=>import("./ui/components/products/products.module").then(module=>module.ProductsModule)},
  {path:"register",loadChildren:()=>import("./ui/components/register/register.module").then(module=>module.RegisterModule)},
  {path:"login",loadChildren:()=>import("./ui/components/login/login.module").then(module=>module.LoginModule)},
  {path:"contact",loadChildren:()=>import("./ui/components/contact/contact.module").then(module=>module.ContactModule)},
  {path: "passwordreset", loadChildren: () => import("./ui/components/passwordreset/passwordreset.module").then(module => module.PasswordresetModule) },
  {path: "passwordupdate/:userId/:resetToken", loadChildren: () => import("./ui/components/passwordupdate/passwordupdate.module").then(module => module.PasswordupdateModule) },
  {path:"thankyou",loadChildren:()=>import("./ui/components/thankyou/thankyou.module").then(module=>module.ThankyouModule)}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
