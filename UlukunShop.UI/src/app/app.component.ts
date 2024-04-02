import {Component} from '@angular/core';
import {CustomToastrService, ToastrMessageType, ToastrPosition} from "./services/ui/custom-toastr.service";
import {AuthService} from "./services/common/auth.service";
import {Router} from "@angular/router";

declare var $: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'UlukunShop.UI';

  constructor(private toastrService: CustomToastrService, public authService: AuthService,private router:Router) {
    authService.identityCheck();
  }

  logOut() {
    localStorage.removeItem("accessToken");
    this.authService.identityCheck();
    this.router.navigate([""]);
    this.toastrService.message("Oturum kapatildi","Log Out",{
      messageType:ToastrMessageType.Info,
      position:ToastrPosition.TopRight
    });
  }
}



