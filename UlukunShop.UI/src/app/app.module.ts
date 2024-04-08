import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';

import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {AdminModule} from "./admin/admin.module";
import {UiModule} from "./ui/ui.module";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ToastrModule} from "ngx-toastr";
import {NgxSpinnerModule} from "ngx-spinner";
import {BaseComponent} from './base/base.component';
import {HTTP_INTERCEPTORS, HttpClientModule} from "@angular/common/http";
import {FileUploadDialogComponent} from './dialogs/file-upload-dialog/file-upload-dialog.component';
import {MatButtonModule} from "@angular/material/button";
import {MatDialogModule} from "@angular/material/dialog";
import {JwtModule} from "@auth0/angular-jwt";
import {LoginComponent} from "./ui/components/login/login.component";
import {
  FacebookLoginProvider,
  GoogleLoginProvider,
  SocialAuthServiceConfig,
  SocialLoginModule
} from "@abacritt/angularx-social-login";
import {HttpErrorHandlerInterceptorService} from "./services/common/http-error-handler-interceptor.service";
import {ShoppingcartsModule} from "./ui/components/shoppingcarts/shoppingcarts.module";
import {ShoppingcartsComponent} from "./ui/components/shoppingcarts/shoppingcarts.component";
import { DynamicLoadComponentDirective } from './directives/common/dynamic-load-component.directive';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    DynamicLoadComponentDirective
  ],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        AppRoutingModule,
        AdminModule,
        UiModule,
        ToastrModule.forRoot(),
        NgxSpinnerModule,
        HttpClientModule,
        MatButtonModule,
        MatDialogModule,
        JwtModule.forRoot({
            config: {
                tokenGetter: () => localStorage.getItem("accessToken"),
                allowedDomains: ["localhost:7131"]
            }
        }),
        SocialLoginModule,
        ShoppingcartsModule
    ],
  providers: [
    {provide: "baseUrl", useValue: 'https://localhost:7131/api', multi: true},
    { provide: "baseSignalRUrl", useValue: "https://localhost:7131/", multi: true },
    {
      provide: "SocialAuthServiceConfig",
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider("318442006015-9dpkendfiub1lr792vt03ri6mmvqi6ip.apps.googleusercontent.com")
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider("1155344239096381")
          }
        ],
        onError: err => console.log(err)
      } as SocialAuthServiceConfig
    },
    {provide:HTTP_INTERCEPTORS,useClass:HttpErrorHandlerInterceptorService,multi: true}
  ],
  exports: [
    ShoppingcartsComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule {
}
