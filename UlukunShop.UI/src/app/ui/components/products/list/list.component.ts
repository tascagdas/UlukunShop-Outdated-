

import {Component, OnInit} from '@angular/core';
import {ProductService} from "../../../../services/common/models/product.service";
import {List_Product} from "../../../../contracts/List_Product";
import {ActivatedRoute} from "@angular/router";
import {FileService} from "../../../../services/common/models/file.service";
import { BaseUrl } from '../../../../contracts/base_url';

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {

  constructor(private productService: ProductService, private activatedRoute: ActivatedRoute, private fileService: FileService) {
  }

  currentPage: number;
  totalProductCount: number;
  totalPageCount: number;
  pageListSize: number = 15;
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

      this.products=this.products.map<List_Product>(p=>{
        const listProduct: List_Product={
          id:p.id,
          createdDate:p.createdDate,
          imagePath: p.productImages.length ? p.productImages.find(p => p.isThumbnail).path : "",
          name:p.name,
          price:p.price,
          stock:p.stock,
          updatedDate:p.updatedDate,
          productImages:p.productImages
        }
        return listProduct;
      });
      console.log(data)



      this.totalProductCount = data.totalProductCount;
      this.totalPageCount = Math.ceil(this.totalProductCount / this.pageListSize);

      this.pageList = [];

        if (this.currentPage - 3 <= 0) {
          for (let i = 1; i <= 3; i++) {
            this.pageList.push(i);
          }
        } else if (this.currentPage + 3 >= this.totalPageCount) {
          for (let i = this.totalPageCount - 6; i <= this.totalPageCount; i++) {
            this.pageList.push(i);
          }
        } else {
          for (let i = this.currentPage - 3; i <= this.currentPage + 3; i++) {
            this.pageList.push(i);
          }
        }

      });


  }

}