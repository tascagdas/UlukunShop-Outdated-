import { Component, OnInit } from '@angular/core';
import {MatTableDataSource} from "@angular/material/table";
import {List_Product} from "../../../../contracts/List_Product";

@Component({
  selector: 'app-list',
  templateUrl: './list.component.html',
  styleUrls: ['./list.component.scss']
})
export class ListComponent implements OnInit {


  displayedColumns: string[] = ['name', 'stock', 'price', 'createdDate','updatedDate'];
  dataSource : MatTableDataSource<List_Product> = null

  ngOnInit(): void {
  }

}
