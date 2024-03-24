import {Injectable} from '@angular/core';
import {HttpClientService} from "../http-client.service";
import {Create_Product} from "../../../contracts/create_product";
import {HttpErrorResponse} from "@angular/common/http";
import {List_Product} from "../../../contracts/List_Product";

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private _httpClientService: HttpClientService) {
  }

  create(product: Create_Product, successCallBack?: any,errorCallBack?:any) {
    this._httpClientService.post({
      controller: "products"
    }, product).subscribe(
      response => {
        successCallBack();
      }, (errorResponse: HttpErrorResponse) => {
        const _error: Array<{ key: string, value: Array<string> }> = errorResponse.error;
        let message=""
        _error.forEach((v,index)=>{
          v.value.forEach((_v,_index)=>{
            message+=`${_v}<br>`;
          });
        });
        errorCallBack(message);

      });
  }

  async read():Promise<List_Product[]>{
    const promiseData:Promise<List_Product[]> = this._httpClientService.get<List_Product[]>({
      controller: "products"
    }).toPromise();

    promiseData.then()
      .catch()

    return await promiseData
  }
}
