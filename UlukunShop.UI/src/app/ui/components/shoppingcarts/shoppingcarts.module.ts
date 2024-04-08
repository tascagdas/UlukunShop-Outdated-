import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShoppingcartsComponent } from './shoppingcarts.component';
import {RouterModule} from "@angular/router";
import * as path from "path";



@NgModule({
    declarations: [
        ShoppingcartsComponent
    ],
    exports: [
        ShoppingcartsComponent
    ],
    imports: [
        CommonModule,
        RouterModule.forChild([
            {path: '', component: ShoppingcartsComponent},
        ])
    ]
})
export class ShoppingcartsModule { }
