import {Directive, ElementRef, EventEmitter, HostListener, Input, Output, Renderer2} from '@angular/core';
import {HttpClientService} from "../../services/common/http-client.service";
import {NgxSpinnerService} from "ngx-spinner";
import {SpinnerType} from "../../base/base.component";
import {MatDialog} from "@angular/material/dialog";
import {DeleteDialogComponent, DeleteState} from "../../dialogs/delete-dialog/delete-dialog.component";
import {AlertifyService, MessageType, Position} from "../../services/admin/alertify.service";
import {HttpErrorResponse} from "@angular/common/http";
import {DialogService} from "../../services/common/dialog.service";

declare var $: any;

@Directive({
  selector: '[appDelete]'
})
export class DeleteDirective {

  constructor(private el: ElementRef,
              private _renderer: Renderer2,
              private _httpClientService: HttpClientService,
              private spinner: NgxSpinnerService,
              public dialog: MatDialog,
              private _alertify: AlertifyService,
              private dialogService: DialogService) {
    const img = _renderer.createElement("img");
    img.setAttribute("src", "../../../../../assets/delete_icon_2.png");
    img.setAttribute("style", "cursor: pointer;");
    img.width = 30;
    _renderer.appendChild(el.nativeElement, img);
  }

  @Input() id: string;
  @Input() controller: string;
  @Output() callback: EventEmitter<any> = new EventEmitter();

  @HostListener("click")
  async onClick() {
    this.dialogService.openDialog({
      componentType:DeleteDialogComponent,
      data:DeleteState.Yes,
      afterClosed:async () => {
        this.spinner.show(SpinnerType.BallAtom)
        const td: HTMLTableCellElement = this.el.nativeElement;
        this._httpClientService.delete({
          controller: this.controller
        }, this.id).subscribe(data => {
          $(td.parentElement).animate({
            opacity: 0,
            left: "+=50",
            height: "toggle",
          }, 500, () => {
            this.callback.emit();
            this._alertify.message(`${this.controller == 'roles' ? 'Rol' : 'Ürün'} başarıyla silinmiştir.`, {
              dismissOthers: true,
              messageType: MessageType.Success,
              position: Position.TopRight
            })
          });
        }, (errorResponse:HttpErrorResponse) => {
          this.spinner.hide(SpinnerType.BallAtom);
          this._alertify.message("Urun Silinirken bir hata ile karsilasildi", {
            dismissOthers: true,
            messageType: MessageType.Error,
            position: Position.TopRight
          });
        });
      }
    });
  }

  // openDialog(afterClosed: any): void {
  //   const dialogRef = this.dialog.open(DeleteDialogComponent, {
  //     width: '500px',
  //     data: DeleteState.Yes,
  //   });
  //
  //   dialogRef.afterClosed().subscribe(result => {
  //     if (result == DeleteState.Yes) {
  //       afterClosed();
  //     }
  //   });
  // }


}
