import {Component, Inject, OnInit} from '@angular/core';
import {BaseDialog} from "../base/base-dialog";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";

@Component({
  selector: 'app-authorize-menu-dialog',
  templateUrl: './authorize-menu-dialog.component.html',
  styleUrls: ['./authorize-menu-dialog.component.scss']
})
export class AuthorizeMenuDialogComponent extends BaseDialog<AuthorizeMenuDialogComponent>  {
  constructor(dialogRef: MatDialogRef<AuthorizeMenuDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any) {
    super(dialogRef)
  }
}

export enum AuthorizeMenuState {
  Yes,
  No
}
