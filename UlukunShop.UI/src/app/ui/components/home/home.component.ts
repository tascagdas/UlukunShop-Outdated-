import { Component, OnInit } from '@angular/core';
import {BaseComponent, SpinnerType} from "../../../base/base.component";
import {NgxSpinnerService} from "ngx-spinner";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent extends BaseComponent implements OnInit {

  constructor(_spinner:NgxSpinnerService) {
    super(_spinner);
  }

  ngOnInit(): void {
    this.showSpinner(SpinnerType.BallAtom)

  }

}
