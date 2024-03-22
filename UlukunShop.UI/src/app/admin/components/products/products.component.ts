import {Component, OnInit} from '@angular/core';
import {BaseComponent, SpinnerType} from "../../../base/base.component";
import {NgxSpinnerService} from "ngx-spinner";
import {AlertifyService} from "../../../services/admin/alertify.service";
import {HttpClientService} from "../../../services/common/http-client.service";
import {Product} from "../../../contracts/product";

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: ['./products.component.scss']
})
export class ProductsComponent extends BaseComponent implements OnInit {

  constructor( _spinner:NgxSpinnerService, private _alertify:AlertifyService, private _httpClientService:HttpClientService ) {
    super(_spinner)
  }

  ngOnInit(): void {
    this.showSpinner(SpinnerType.Pacman);

    // this._httpClientService.post({
    //   controller: "products"
    // }, {
    //   name: "aaa",
    //   stock: 3,
    //   price: 15
    // }).subscribe();
    //
  }

}
