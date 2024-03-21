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

    this._httpClientService.post({
      controller:"products"
    },{
      name:"99",
      price:0,
      stock:0
    }).subscribe();


    this._httpClientService.post({
      controller:"products"
    },{
      name:"100",
      price:0,
      stock:0
    }).subscribe();


    this._httpClientService.post({
      controller:"products"
    },{
      name:"101",
      price:0,
      stock:0
    }).subscribe();


    this._httpClientService.post({
      controller:"products"
    },{
      name:"102",
      price:0,
      stock:0
    }).subscribe();

  }

}
