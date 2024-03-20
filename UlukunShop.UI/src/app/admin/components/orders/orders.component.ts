import {Component, OnInit} from '@angular/core';
import {NgxSpinnerService} from "ngx-spinner";
import {BaseComponent, SpinnerType} from "../../../base/base.component";

@Component({
  selector: 'app-orders',
  templateUrl: './orders.component.html',
  styleUrls: ['./orders.component.scss']
})
export class OrdersComponent extends BaseComponent implements OnInit {

  constructor(private _spinner:NgxSpinnerService) {
    super(_spinner)
  }

  ngOnInit(): void {
    this.showSpinner(SpinnerType.Triangle)
  }

}
