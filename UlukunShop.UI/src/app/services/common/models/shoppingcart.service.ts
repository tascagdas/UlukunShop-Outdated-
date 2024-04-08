import { Injectable } from '@angular/core';
import { firstValueFrom, Observable } from 'rxjs';
import { HttpClientService } from '../http-client.service';
import {List_Shopping_Cart_Item} from "../../../contracts/ShoppingCart/list_shopping_cart_item";
import {Create_Shopping_Cart_Item} from "../../../contracts/ShoppingCart/create_shopping_cart_item";
import {Update_Shopping_Cart_Item} from "../../../contracts/ShoppingCart/update_shopping_cart_item";

@Injectable({
  providedIn: 'root'
})
export class ShoppingCartService {
  constructor(private httpClientService: HttpClientService) { }

  async get() {
    const observable: Observable<List_Shopping_Cart_Item[]> = this.httpClientService.get({
      controller: "shoppingcart"
    });

    return await firstValueFrom(observable);
  }

  async add(shoppingCartItem: Create_Shopping_Cart_Item): Promise<void> {
    const observable: Observable<any> = this.httpClientService.post({
      controller: "shoppingcart"
    }, shoppingCartItem);

    await firstValueFrom(observable);
  }

  async updateQuantity(shoppingCartItem: Update_Shopping_Cart_Item): Promise<void> {
    const observable: Observable<any> = this.httpClientService.put({
      controller: "shoppingcart"
    }, shoppingCartItem)

    await firstValueFrom(observable);
  }

  async remove(shoppingCartItemId: string) {
    const observable: Observable<any> = this.httpClientService.delete({
      controller: "shoppingcart"
    }, shoppingCartItemId);
    await firstValueFrom(observable);
  }
}
