import {Component, OnInit} from '@angular/core';
import {UserService} from "../../../services/common/models/user.service";
import {NgxSpinnerService} from "ngx-spinner";
import {BaseComponent, SpinnerType} from "../../../base/base.component";

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent extends BaseComponent implements OnInit {

  constructor(private userService: UserService, spinner:NgxSpinnerService)   {
    super (spinner);
  }

  ngOnInit(): void {
  }

  async login(usernameOrEmail: string, password: string) {
    this.showSpinner(SpinnerType.Triangle)
    await this.userService.login(usernameOrEmail, password,()=>this.hideSpinner(SpinnerType.Triangle));
  }
}
