import { Component, OnInit } from '@angular/core';
import {NgxSpinnerService} from "ngx-spinner";
import {BaseComponent, SpinnerType} from "../../../base/base.component";
import {List_Shopping_Cart_Item} from "../../../contracts/ShoppingCart/list_shopping_cart_item";
import {Update_Shopping_Cart_Item} from "../../../contracts/ShoppingCart/update_shopping_cart_item";
import {ShoppingCartService} from "../../../services/common/models/shoppingcart.service";

declare var $: any;

@Component({
  selector: 'app-shoppingcarts',
  templateUrl: './shoppingcarts.component.html',
  styleUrls: ['./shoppingcarts.component.scss']
})
export class ShoppingcartsComponent extends BaseComponent implements OnInit {

  constructor( _spinner:NgxSpinnerService, private shoppingCartService:ShoppingCartService) {
    super(_spinner)
  }

  shoppingCartItems: List_Shopping_Cart_Item[];
  async ngOnInit(): Promise<void> {
    this.showSpinner(SpinnerType.BallAtom)
    this.shoppingCartItems = await this.shoppingCartService.get()
    this.hideSpinner(SpinnerType.BallAtom)
  }

  async changeQuantity(event: any) {
    this.showSpinner(SpinnerType.BallAtom)
    const shoppingCartItemId: string = event.target.previousSibling.value;
console.log(this.shoppingCartItems);
    const quantity: number = event.target.value;
    const shoppingCartItem: Update_Shopping_Cart_Item = new Update_Shopping_Cart_Item();
    shoppingCartItem.shoppingCartItemId = shoppingCartItemId;
    shoppingCartItem.quantity = quantity;
    await this.shoppingCartService.updateQuantity(shoppingCartItem);
    this.hideSpinner(SpinnerType.BallAtom)
  }



  async removeShoppingCartItem(shoppingCartItemId: string) {


    this.showSpinner(SpinnerType.BallAtom);
    await this.shoppingCartService.remove(shoppingCartItemId);
    var a = $("." + shoppingCartItemId)
    $("." + shoppingCartItemId).fadeOut(500, () => this.hideSpinner(SpinnerType.BallAtom));
  }


}
