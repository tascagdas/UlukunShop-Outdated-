import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {LayoutModule} from "./layout/layout.module";
import { LayoutComponent } from './layout/layout.component';
import { ComponentsModule } from './components/components.module';

/**
* eğerki bir modül başka bir modülü kendi içinde benimsiycekse
* o modülü import etmesi gerekmekte o yüzden layout u buraya import ediceğiz
 */
@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    LayoutModule,
ComponentsModule
  ],
  exports:[LayoutModule]
})
export class AdminModule { }
