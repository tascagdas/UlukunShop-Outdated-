import { Component, OnInit } from '@angular/core';
import { BaseComponent, SpinnerType } from "../../../base/base.component";
import { NgxSpinnerService } from "ngx-spinner";
import { AlertifyService } from "../../../services/admin/alertify.service";
import { HttpClientService } from "../../../services/common/http-client.service";

@Component ( {
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrls: [ './products.component.scss' ]
} )
export class ProductsComponent extends BaseComponent implements OnInit {
  constructor ( _spinner: NgxSpinnerService, private _alertify: AlertifyService, private _httpClientService: HttpClientService ) {
    super ( _spinner )
  }

  async ngOnInit () {
    this.showSpinner ( SpinnerType.Pacman );




    // this._httpClientService.post({
    //   controller:"products"
    // },{
    //   name:"1topromise deneme",
    //   price:1000,
    //   stock:43
    // }).toPromise();





    // try {
    //   await this.sendRequest ( { name: "a", stock: 1, price: 1 } );
    //   await this.sendRequest ( { name: "b", stock: 2, price: 2 } );
    //   await this.sendRequest ( { name: "c", stock: 3, price: 3 } );
    //   await this.sendRequest ( { name: "d", stock: 4, price: 4 } );
    // } catch ( error ) {
    //   console.error ( error );
    // }

  }

  private async sendRequest ( data: any ) {
    try {
      await this._httpClientService.post ( {
        controller: "products"
      }, data ).toPromise ();
      console.log( data );
    } catch ( error ) {
      console.error ( error );
      throw error;
    }
  }
}
