import { Component, OnInit } from '@angular/core';
import {BaseComponent, SpinnerType} from "../../../../base/base.component";
import {BaseUrl} from "../../../../contracts/base_url";
import {List_Product} from "../../../../contracts/List_Product";
import {ProductService} from "../../../../services/common/models/product.service";
import {ActivatedRoute} from "@angular/router";
import {FileService} from "../../../../services/common/models/file.service";
import {ShoppingCartService} from "../../../../services/common/models/shoppingcart.service";
import {NgxSpinnerService} from "ngx-spinner";
import {CustomToastrService, ToastrMessageType, ToastrPosition} from "../../../../services/ui/custom-toastr.service";
import {Create_Shopping_Cart_Item} from "../../../../contracts/ShoppingCart/create_shopping_cart_item";

@Component({
  selector: 'app-home-products',
  templateUrl: './home-products.component.html',
  styleUrls: ['./home-products.component.scss']
})
export class HomeProductsComponent extends BaseComponent implements OnInit {

  constructor(private productService: ProductService,
              private activatedRoute: ActivatedRoute,
              private fileService: FileService,
              private shoppingCartService:ShoppingCartService,
              spinner:NgxSpinnerService,
              private toastr:CustomToastrService) {
    super(spinner);
  }

  currentPage: number;
  pageListSize: number = 7;
  pageList: number[] = [];
  baseUrl: BaseUrl;

  products: List_Product[];

  async ngOnInit() {
    this.baseUrl = await this.fileService.getBaseStorageUrl();

    this.activatedRoute.params.subscribe(async params => {

      this.currentPage = parseInt(params["page"] ?? 1);

      const data = await this.productService.read(this.currentPage - 1, this.pageListSize, () => {

      }, errorMessage => {

      });


      this.products = data.products;

      this.products = this.products.map<List_Product>(p => {
        const listProduct: List_Product = {
          id: p.id,
          createdDate: p.createdDate,
          imagePath: p.productImages.length ? p.productImages.find(p => p.isThumbnail).path : "",
          name: p.name,
          price: p.price,
          stock: p.stock,
          updatedDate: p.updatedDate,
          productImages: p.productImages
        }
        return listProduct;
      });

    });

  }
  async addToShoppingCart(product: List_Product) {
    this.showSpinner(SpinnerType.BallAtom);
    let _basketItem: Create_Shopping_Cart_Item = new Create_Shopping_Cart_Item();
    _basketItem.productId = product.id;
    _basketItem.quantity = 1;
    await this.shoppingCartService.add(_basketItem);
    this.hideSpinner(SpinnerType.BallAtom);
    this.toastr.message("Ürün sepete eklenmiştir.", "Sepete Eklendi", {
      messageType: ToastrMessageType.Success,
      position: ToastrPosition.TopRight
    });
  }

}
