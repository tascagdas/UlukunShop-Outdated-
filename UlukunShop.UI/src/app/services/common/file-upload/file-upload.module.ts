import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FileUploadComponent} from "./file-upload.component";
import {NgxFileDropModule} from "ngx-file-drop";



@NgModule({
  declarations: [
    FileUploadComponent],
  exports: [
    FileUploadComponent
  ],
  imports: [
    CommonModule,
    NgxFileDropModule
  ]
})
export class FileUploadModule { }
