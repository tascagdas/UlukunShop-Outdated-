import {Component, Inject, OnDestroy, OnInit} from '@angular/core';
import {BaseDialog} from "../base/base-dialog";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
declare var $: any;

@Component({
  selector: 'app-shopping-cart-item-remove-dialog',
  templateUrl: './shopping-cart-item-remove-dialog.component.html',
  styleUrls: ['./shopping-cart-item-remove-dialog.component.scss']
})
export class ShoppingCartItemRemoveDialogComponent extends BaseDialog<ShoppingCartItemRemoveDialogComponent> implements OnDestroy{

  constructor(dialogRef: MatDialogRef<ShoppingCartItemRemoveDialogComponent>,@Inject(MAT_DIALOG_DATA) public data: ShoppingCartItemDeleteState) {
    super(dialogRef);
  }


  ngOnDestroy(): void {
    $("#shoppingCartModal").modal("show");
  }

}

export enum ShoppingCartItemDeleteState {
  Yes,
  No
}
