import { Component, OnInit } from '@angular/core';
import {NgxSpinnerService} from "ngx-spinner";
import {BaseComponent, SpinnerType} from "../../../base/base.component";
import {List_Shopping_Cart_Item} from "../../../contracts/ShoppingCart/list_shopping_cart_item";
import {Update_Shopping_Cart_Item} from "../../../contracts/ShoppingCart/update_shopping_cart_item";
import {ShoppingCartService} from "../../../services/common/models/shoppingcart.service";
import {Create_Order} from "../../../contracts/Order/create_order";
import {CustomToastrService, ToastrMessageType, ToastrPosition} from 'src/app/services/ui/custom-toastr.service';
import { Router } from '@angular/router';
import { OrderService } from 'src/app/services/common/models/order.service';

declare var $: any;

@Component({
  selector: 'app-shoppingcarts',
  templateUrl: './shoppingcarts.component.html',
  styleUrls: ['./shoppingcarts.component.scss']
})
export class ShoppingcartsComponent extends BaseComponent implements OnInit {

  constructor( _spinner:NgxSpinnerService, private shoppingCartService:ShoppingCartService, private orderService: OrderService, private toastrService: CustomToastrService, private router: Router) {
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

  async shoppingComplete() {
    this.showSpinner(SpinnerType.BallAtom);
    const order: Create_Order = new Create_Order();
    order.address = "İstanbul adresi Buralar ileride kullanıcan bir form aracılığıyla alınacak.";
    order.description = "Hızlı kargoya verin lütfen...";
    await this.orderService.create(order);
    this.hideSpinner(SpinnerType.BallAtom);
    this.toastrService.message("Sipariş alınmıştır!", "Sipariş Oluşturuldu!", {
      messageType: ToastrMessageType.Info,
      position: ToastrPosition.TopRight
    })
    this.router.navigate(["/"]);
  }


}
