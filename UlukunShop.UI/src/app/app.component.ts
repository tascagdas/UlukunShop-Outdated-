import {Component} from '@angular/core';
import {CustomToastrService, ToastrMessageType, ToastrPosition} from "./services/ui/custom-toastr.service";

declare var $: any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  title = 'UlukunShop.UI';
constructor(private toastrService: CustomToastrService)  {

}
}

$.get("https://localhost:7138/api/products", data=>{
  console.log(data);
})


