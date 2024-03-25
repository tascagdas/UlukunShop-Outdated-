import {Directive, ElementRef, EventEmitter, HostListener, Input, Output, Renderer2} from '@angular/core';
import {HttpClientService} from "../../services/common/http-client.service";
import {ProductService} from "../../services/common/models/product.service";
import {NgxSpinnerService} from "ngx-spinner";
import {SpinnerType} from "../../base/base.component";

declare var $:any;
@Directive({
  selector: '[appDelete]'
})
export class DeleteDirective {

  constructor(private el: ElementRef,
              private _renderer: Renderer2,
              private _productService: ProductService,
              private spinner: NgxSpinnerService)
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

    this.spinner.show(SpinnerType.BallAtom)
    const td:HTMLTableCellElement=this.el.nativeElement;
    await this._productService.delete(this.id)
    $(td.parentElement).fadeOut(800,()=>{
      this.callback.emit();
    });

  }

}
