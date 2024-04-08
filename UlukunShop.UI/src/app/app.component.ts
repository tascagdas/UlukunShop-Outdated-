import {Component, ViewChild} from '@angular/core';
import {CustomToastrService, ToastrMessageType, ToastrPosition} from "./services/ui/custom-toastr.service";
import {AuthService} from "./services/common/auth.service";
import {Router} from "@angular/router";
import {ComponentType, DynamicLoadComponentService} from "./services/common/dynamic-load-component.service";
import {DynamicLoadComponentDirective} from "./directives/common/dynamic-load-component.directive";

declare var $: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  @ViewChild(DynamicLoadComponentDirective, { static: true })
  dynamicLoadComponentDirective: DynamicLoadComponentDirective;

  title = 'UlukunShop.UI';

  constructor(private toastrService: CustomToastrService, public authService: AuthService,private router:Router, private dynamicLoadComponentService: DynamicLoadComponentService) {
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

  loadComponent() {
    this.dynamicLoadComponentService.loadComponent(ComponentType.BasketsComponent, this.dynamicLoadComponentDirective.viewContainerRef);
  }
}



