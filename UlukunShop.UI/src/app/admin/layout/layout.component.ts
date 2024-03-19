import {Component, OnInit} from '@angular/core';
import {AlertifyService, MessageType, Position} from "../../services/admin/alertify.service";

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
