import { Component, Inject, OnDestroy, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { BaseDialog } from '../base/base-dialog';

declare var $: any;

@Component({
  selector: 'app-shopping-cart-complete-dialog',
  templateUrl: './shopping-cart-complete-dialog.component.html',
  styleUrls: ['./shopping-cart-complete-dialog.component.scss']
})
export class ShoppingCartCompleteDialogComponent extends BaseDialog<ShoppingCartCompleteDialogComponent> implements OnDestroy {

  constructor(dialogRef: MatDialogRef<ShoppingCartCompleteDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: ShoppingCartCompleteState) {
    super(dialogRef)
  }

  show: boolean = false;
  complete() {
    this.show = true;
  }

  ngOnDestroy(): void {
    if (!this.show)
      $("#shoppingCartModal").modal("show");
  }
}

export enum ShoppingCartCompleteState {
  Yes,
  No
}
