import {Component, OnInit} from '@angular/core';
import {ProductService} from "../../../../services/common/models/product.service";
import {Create_Product} from "../../../../contracts/product";
import {BaseComponent, SpinnerType} from "../../../../base/base.component";
import {NgxSpinnerService} from "ngx-spinner";
import {AlertifyService, MessageType} from "../../../../services/admin/alertify.service";

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent extends BaseComponent implements OnInit {

  constructor(spinner:NgxSpinnerService,private productService: ProductService,private alertify:AlertifyService) {
    super(spinner);
  }

  ngOnInit(): void {
  }
  create(name:HTMLInputElement,price:HTMLInputElement,stock:HTMLInputElement){
    this.showSpinner(SpinnerType.BallAtom)
    const create_product:Create_Product=new Create_Product();
    create_product.name=name.value;
    create_product.stock=parseInt(stock.value);
    create_product.price=parseFloat(price.value);

    this.productService.create(create_product,()=> {
      this.hideSpinner(SpinnerType.BallAtom)
      this.alertify.message("Successfully created",{dismissOthers:true,messageType:MessageType.Success});
    });
  }

}
