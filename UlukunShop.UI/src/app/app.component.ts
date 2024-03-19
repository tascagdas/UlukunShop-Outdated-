import {Component} from '@angular/core';
import {CustomToastrService, ToastrMessageType} from "./services/ui/custom-toastr.service";

declare var $: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'UlukunShop.UI';
constructor(private toastrService: CustomToastrService)  {
  toastrService.message("UlukunShop.UI","Deneme",ToastrMessageType.Error);
  toastrService.message("UlukunShop.UI","Deneme",ToastrMessageType.Success);
  toastrService.message("UlukunShop.UI","Deneme",ToastrMessageType.Info);
  toastrService.message("UlukunShop.UI","Deneme",ToastrMessageType.Warning);
}
}

