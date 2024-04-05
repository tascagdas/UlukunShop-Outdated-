import {Injectable} from '@angular/core';
import {HttpClientService} from "../http-client.service";
import {Create_Product} from "../../../contracts/create_product";
import {HttpErrorResponse} from "@angular/common/http";
import {List_Product} from "../../../contracts/List_Product";
import {firstValueFrom, Observable} from "rxjs";
import {List_Product_Image} from "../../../contracts/list_product_image";

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private _httpClientService: HttpClientService) {
  }

  create(product: Create_Product, successCallBack?: () => void, errorCallBack?: (errorMessage: string) => void) {
    this._httpClientService.post({
      controller: "products"
    }, product).subscribe(
      response => {
        successCallBack();
      }, (errorResponse: HttpErrorResponse) => {
        const _error: Array<{ key: string, value: Array<string> }> = errorResponse.error;
        let message = ""
        _error.forEach((v, index) => {
          v.value.forEach((_v, _index) => {
            message += `${_v}<br>`;
          });
        });
        errorCallBack(message);

      });
  }

  async read(page: number = 0, size: number = 5, successCallBack?: () => void, errorCallBack?: (errorMessage: string) => void): Promise<{
    totalProductCount: number,
    products: List_Product[]
  }> {
    const promiseData: Promise<{ totalProductCount: number, products: List_Product[] }> = this._httpClientService.get<{
      totalProductCount: number,
      products: List_Product[]
    }>({
      controller: "products",
      queryString: `page=${page}&size=${size}`,
    }).toPromise();

    promiseData.then(d => successCallBack())
      .catch((errorResponse: HttpErrorResponse) => errorCallBack(errorResponse.message))

    return await promiseData
  }

  async delete(id: string) {
    const deleteObservable: Observable<any> = this._httpClientService.delete<any>({
      controller: "products"
    }, id);

    await firstValueFrom(deleteObservable);
  }

  async getImages(id: string, successCallBack?: () => void): Promise<List_Product_Image[]> {
    const getObservable: Observable<List_Product_Image[]> = this._httpClientService.get<List_Product_Image[]>({
      controller: "products",
      action: "GetProductImages"
    }, id);

    const images: List_Product_Image[] = await firstValueFrom(getObservable);
    successCallBack();
    return images;
  }

  async deleteImage(id: string, imageId: string, successCallBack?: () => void) {
    const deleteObservable = this._httpClientService.delete({
      controller: "products",
      action: "DeleteProductImage",
      queryString: `imageId=${imageId}`
    }, id)
    await firstValueFrom(deleteObservable);
    successCallBack();
  }

  async changeThumbnail(imageId: string, productId: string, successCallBack?: () => void):Promise<void> {
    const changeThumbnailObservable= this._httpClientService.get({
      controller: "products",
      action: "MakeProductImageThumbnail",
      queryString: `imageid=${imageId}&productid=${productId}`
    });

    await firstValueFrom(changeThumbnailObservable);
    successCallBack();
  }
}
