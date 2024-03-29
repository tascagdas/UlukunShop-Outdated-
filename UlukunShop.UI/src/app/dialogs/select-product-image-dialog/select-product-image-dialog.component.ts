import {Component, Inject, OnInit, Output} from '@angular/core';
import {BaseDialog} from "../base/base-dialog";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {DeleteState} from "../delete-dialog/delete-dialog.component";
import {FileUploadOptions} from "../../services/common/file-upload/file-upload.component";
import {ProductService} from "../../services/common/models/product.service";
import {List_Product_Image} from "../../contracts/list_product_image";

@Component({
  selector: 'app-select-product-image-dialog',
  templateUrl: './select-product-image-dialog.component.html',
  styleUrls: ['./select-product-image-dialog.component.scss']
})
export class SelectProductImageDialogComponent extends BaseDialog<SelectProductImageDialogComponent> implements OnInit {

  constructor(dialogRef: MatDialogRef<SelectProductImageDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: SelectProductImageState|string,
              private productService: ProductService
              ) {
    super(dialogRef)
  }

@Output() options:Partial<FileUploadOptions>={
    accept:".png, .jpg, .jpeg, .gif",
  action:"upload",
  controller:"products",
  explanation:"Ürün Resimlerini seçin veya buraya sürekleyiniz...",
  isAdminPage:true,
  queryString:`id=${this.data}`,
};

  images:List_Product_Image[];
  async ngOnInit() {
    this.images= await this.productService.getImages(this.data as string);
  }

}


export enum SelectProductImageState {
  Close
}
