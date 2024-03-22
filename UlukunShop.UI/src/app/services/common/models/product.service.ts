import { Injectable } from '@angular/core';
import {HttpClientService} from "../http-client.service";
import {Create_Product} from "../../../contracts/product";

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private _httpClientService:HttpClientService) { }
  create(product: Create_Product,successCallBack?:any){
this._httpClientService.post({
  controller:"products"
},product).subscribe(
  response=>{
    successCallBack();
  }
)
  }
}
