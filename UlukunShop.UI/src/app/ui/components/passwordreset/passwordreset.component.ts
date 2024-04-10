import { Component, OnInit } from '@angular/core';
import {NgxSpinnerService} from "ngx-spinner";
import {UserAuthService} from "../../../services/common/models/user-auth.service";
import {AlertifyService, MessageType, Position} from "../../../services/admin/alertify.service";
import {BaseComponent, SpinnerType} from "../../../base/base.component";

@Component({
  selector: 'app-passwordreset',
  templateUrl: './passwordreset.component.html',
  styleUrls: ['./passwordreset.component.scss']
})
export class PasswordresetComponent extends BaseComponent{

  constructor(spinner: NgxSpinnerService, private userAuthService: UserAuthService, private alertifyService: AlertifyService) {
    super(spinner)
  }

  passwordReset(email: string) {
    this.showSpinner(SpinnerType.BallAtom)
    this.userAuthService.passwordReset(email, () => {
      this.hideSpinner(SpinnerType.BallAtom)
      this.alertifyService.message("Mail başarıyla gönderilmiştir.", {
        messageType: MessageType.Notify,
        position: Position.TopRight
      });
    })
  }

}
