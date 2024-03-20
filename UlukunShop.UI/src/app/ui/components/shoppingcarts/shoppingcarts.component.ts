import { Component, OnInit } from '@angular/core';
import {NgxSpinnerService} from "ngx-spinner";
import {BaseComponent, SpinnerType} from "../../../base/base.component";

@Component({
  selector: 'app-shoppingcarts',
  templateUrl: './shoppingcarts.component.html',
  styleUrls: ['./shoppingcarts.component.scss']
})
export class ShoppingcartsComponent extends BaseComponent implements OnInit {

  constructor( _spinner:NgxSpinnerService ) {
    super(_spinner)
  }
  ngOnInit(): void {
    this.showSpinner(SpinnerType.BallAtom)

  }

}
