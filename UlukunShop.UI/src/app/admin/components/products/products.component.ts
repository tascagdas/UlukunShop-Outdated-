import {Component, OnInit} from '@angular/core';
import {BaseComponent, SpinnerType} from "../../../base/base.component";
import {NgxSpinnerService} from "ngx-spinner";
import {AlertifyService} from "../../../services/admin/alertify.service";

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent extends BaseComponent implements OnInit {

  constructor( _spinner:NgxSpinnerService, private _alertify:AlertifyService ) {
    super(_spinner)
  }

  ngOnInit(): void {
    this.showSpinner(SpinnerType.Pacman)
  }

}
