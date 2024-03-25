import {Directive, ElementRef, EventEmitter, HostListener, Input, Output, Renderer2} from '@angular/core';
import {HttpClientService} from "../../services/common/http-client.service";
import {ProductService} from "../../services/common/models/product.service";
import {NgxSpinnerService} from "ngx-spinner";
import {SpinnerType} from "../../base/base.component";
import {MatDialog} from "@angular/material/dialog";
import {DeleteDialogComponent, DeleteState} from "../../dialogs/delete-dialog/delete-dialog.component";

declare var $:any;
@Directive({
  selector: '[appDelete]'
})
export class DeleteDirective {

  constructor(private el: ElementRef,
              private _renderer: Renderer2,
              private _productService: ProductService,
              private spinner: NgxSpinnerService,
              public dialog: MatDialog)
  {
    const img=_renderer.createElement("img");
    img.setAttribute("src", "../../../../../assets/delete_icon.png");
    img.setAttribute("style", "cursor: pointer;");
    img.width=37;
    _renderer.appendChild(el.nativeElement,img);
  }

  @Input() id:string;
  @Output() callback: EventEmitter<any>=new EventEmitter();
  @HostListener("click")
  async onClick(){
this.openDialog(async ()=>{
  this.spinner.show(SpinnerType.BallAtom)
  const td:HTMLTableCellElement=this.el.nativeElement;
  await this._productService.delete(this.id)
  $(td.parentElement).animate({
    opacity: 0,
    left: "+=50",
    height:"toggle",
  },500,()=>{
    this.callback.emit();
  });
});
  }
  
  openDialog(afterClosed:any): void {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      width: '500px',
      data: DeleteState.Yes,
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result==DeleteState.Yes){
        afterClosed();
      }
    });
  }


}
