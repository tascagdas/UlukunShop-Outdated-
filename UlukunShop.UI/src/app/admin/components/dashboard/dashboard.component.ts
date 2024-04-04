import {Component, OnInit} from '@angular/core';
import {BaseComponent, SpinnerType} from "../../../base/base.component";
import {NgxSpinnerService} from "ngx-spinner";
import {AlertifyService, MessageType, Position} from "../../../services/admin/alertify.service";
import {SignalrService} from "../../../services/common/signalr.service";
import {ReceiveFunctions} from "../../../constants/receive-functions";
import {HubUrls} from "../../../constants/hub-urls";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent extends BaseComponent implements OnInit {

  constructor( _spinner:NgxSpinnerService, private _alertify:AlertifyService,private signalrService:SignalrService) {
    super(_spinner)
    signalrService.start(HubUrls.ProductHub)
  }

  ngOnInit(): void {
    this.signalrService.on(ReceiveFunctions.ProductAddedMessageReceiveFunction, message => {
      this._alertify.message(message, {
        messageType: MessageType.Message,
        position: Position.TopCenter
      })
    });
  this.showSpinner(SpinnerType.Triangle)

  }

}
