import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import {ProductService} from "../../../../services/common/models/product.service";
import {Create_Product} from "../../../../contracts/create_product";
import {BaseComponent, SpinnerType} from "../../../../base/base.component";
import {NgxSpinnerService} from "ngx-spinner";
import {AlertifyService, MessageType, Position} from "../../../../services/admin/alertify.service";
import {FileUploadOptions} from "../../../../services/common/file-upload/file-upload.component";

@Component({
  selector: 'app-create',
  templateUrl: './create.component.html',
  styleUrls: ['./create.component.scss']
})
export class CreateComponent extends BaseComponent implements OnInit {

  constructor(spinner: NgxSpinnerService, private productService: ProductService, private alertify: AlertifyService) {
    super(spinner);
  }

  ngOnInit(): void {
  }

  @Output() createdProduct: EventEmitter<Create_Product> = new EventEmitter();


  create(name: HTMLInputElement, price: HTMLInputElement, stock: HTMLInputElement) {
    this.showSpinner(SpinnerType.BallAtom)
    const create_product: Create_Product = new Create_Product();
    create_product.name = name.value;
    create_product.stock = parseInt(stock.value);
    create_product.price = parseFloat(price.value);



    this.productService.create(create_product, () => {
      this.hideSpinner(SpinnerType.BallAtom)
      this.alertify.message("Successfully created", {dismissOthers: true, messageType: MessageType.Success});
      this.createdProduct.emit(create_product);
    }, errorMessage => {
      this.alertify.message(errorMessage, {
        dismissOthers: true,
        messageType: MessageType.Error,
        position: Position.TopRight
      });
    });
  }

}
