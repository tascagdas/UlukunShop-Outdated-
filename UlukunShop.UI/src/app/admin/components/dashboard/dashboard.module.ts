import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import {RouterModule} from "@angular/router";
import {MatGridListModule} from "@angular/material/grid-list";
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";



@NgModule({
  declarations: [
    DashboardComponent
  ],
    imports: [
        CommonModule,
        RouterModule.forChild([
            {path: '', component: DashboardComponent}
        ]),
        MatGridListModule,
        MatProgressSpinnerModule
    ]
})
export class DashboardModule { }
