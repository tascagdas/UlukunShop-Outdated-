import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {List_Product} from "../../../../contracts/List_Product";
import {ProductService} from "../../../../services/common/models/product.service";
import {BaseComponent, SpinnerType} from "../../../../base/base.component";
import {NgxSpinnerService} from "ngx-spinner";
import {AlertifyService, MessageType, Position} from "../../../../services/admin/alertify.service";
import {MatPaginator} from "@angular/material/paginator";

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent extends BaseComponent implements OnInit {

  constructor(private productService: ProductService, private _spinner: NgxSpinnerService, private alertify: AlertifyService) {
    super(_spinner)
  }

  displayedColumns: string[] = ['name', 'stock', 'price', 'createdDate', 'updatedDate'];
  dataSource: MatTableDataSource<List_Product> = null;
  @ViewChild(MatPaginator) paginator: MatPaginator;


  async getProducts(){
    this.showSpinner(SpinnerType.BallAtom);

    const allProducts: List_Product[] = await this.productService.read(() =>
        this.hideSpinner(SpinnerType.BallAtom), errorMessage => {
        this.alertify.message(errorMessage, {
          dismissOthers: true,
          messageType: MessageType.Error,
          position: Position.TopRight
        })
      }
    )
    this.dataSource = new MatTableDataSource<List_Product>(allProducts);
    this.dataSource.paginator = this.paginator;
  }

  async ngOnInit(): Promise<void> {
    await this.getProducts();
  }

}
