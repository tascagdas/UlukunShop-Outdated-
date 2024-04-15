import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard.component';
import {RouterModule} from "@angular/router";
import {MatGridListModule} from "@angular/material/grid-list";
import {MatProgressSpinnerModule} from "@angular/material/progress-spinner";
import {NgApexchartsModule} from "ng-apexcharts";



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
        MatProgressSpinnerModule,
        NgApexchartsModule
    ]
})
export class DashboardModule { }
