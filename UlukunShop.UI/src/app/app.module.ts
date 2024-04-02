import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {AdminModule} from "./admin/admin.module";
import {UiModule} from "./ui/ui.module";
import {BrowserAnimationsModule} from "@angular/platform-browser/animations";
import {ToastrModule} from "ngx-toastr";
import {NgxSpinnerModule} from "ngx-spinner";
import { BaseComponent } from './base/base.component';
import {HttpClientModule} from "@angular/common/http";
import { FileUploadDialogComponent } from './dialogs/file-upload-dialog/file-upload-dialog.component';
import {MatButtonModule} from "@angular/material/button";
import {MatDialogModule} from "@angular/material/dialog";
import {JwtModule} from "@auth0/angular-jwt";

@NgModule({
    declarations: [
        AppComponent
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
      config:{
        tokenGetter:()=>localStorage.getItem("accessToken"),
        allowedDomains:["localhost:7131"]
      }
    })
  ],
    providers: [
        {provide: "baseUrl", useValue: 'https://localhost:7131/api', multi: true},
    ],
    exports: [

    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
