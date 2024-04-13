import {Component, OnInit, ViewChild} from '@angular/core';
import { OrderService } from 'src/app/services/common/models/order.service';
import {AlertifyService, MessageType, Position} from "../../../../services/admin/alertify.service";
import {NgxSpinnerService} from "ngx-spinner";
import {MatTableDataSource} from "@angular/material/table";
import {List_Order} from "../../../../contracts/Order/list_order";
import {MatPaginator} from "@angular/material/paginator";
import {BaseComponent, SpinnerType} from "../../../../base/base.component";
import {DialogService} from "../../../../services/common/dialog.service";
import {OrderDetailDialogComponent} from "../../../../dialogs/order-detail-dialog/order-detail-dialog.component";

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent extends BaseComponent implements OnInit {

  constructor(spinner: NgxSpinnerService,
              private orderService: OrderService,
              private alertifyService: AlertifyService,
              private dialogService: DialogService) {
    super(spinner)
  }


  displayedColumns: string[] = ['orderCode', 'userName', 'totalPrice', 'createdDate','completed','viewDetail', 'delete'];
  dataSource: MatTableDataSource<List_Order> = null;
  @ViewChild(MatPaginator) paginator: MatPaginator;

  async getOrders() {
    this.showSpinner(SpinnerType.BallAtom);

    const allOrders: { totalOrderCount: number; orders: List_Order[] } = await this.orderService.getAllOrders(this.paginator ? this.paginator.pageIndex : 0, this.paginator ? this.paginator.pageSize : 5, () => this.hideSpinner(SpinnerType.BallAtom), (errorMessage: any) => {
      this.alertifyService.message(errorMessage.message, {
        dismissOthers: true,
        messageType: MessageType.Error,
        position: Position.TopRight
      });
    })
    this.dataSource = new MatTableDataSource<List_Order>(allOrders.orders);
    this.paginator.length = allOrders.totalOrderCount;
  }

  async pageChanged() {
    await this.getOrders();
  }

  async ngOnInit() {
    await this.getOrders();
  }

  showDetail(id) {
    this.dialogService.openDialog({
      componentType: OrderDetailDialogComponent,
      data: id,
      options: {
        width: "750px"
      }
    });
  }
}
