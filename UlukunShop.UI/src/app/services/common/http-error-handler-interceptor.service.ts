import {Injectable} from '@angular/core';
import {HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpStatusCode} from "@angular/common/http";
import {catchError, Observable, of} from 'rxjs';
import {CustomToastrService, ToastrMessageType, ToastrPosition} from "../ui/custom-toastr.service";

@Injectable({
  providedIn: 'root'
})
export class HttpErrorHandlerInterceptorService implements HttpInterceptor {

  constructor(private toastrService:CustomToastrService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(catchError(err => {
          switch (err.status) {
            case HttpStatusCode.Unauthorized:
                this.toastrService.message(" Haddini bil. Senin yetkin buna yetmiyor. Defol git kendi sinirlarinda oyna.","Kardesim ayagini denk al!!",{
                  messageType:ToastrMessageType.Warning,
                  position:ToastrPosition.TopFullWidth
                })
              break;
            case HttpStatusCode.InternalServerError:
              this.toastrService.message("Sunucuya Erisilmiyor.","Sunucu hatasi",{
                messageType:ToastrMessageType.Warning,
                position:ToastrPosition.TopFullWidth
              })
              break;
            case HttpStatusCode.BadRequest:
              this.toastrService.message("Gecersiz istek yapildi!","Gecersiz istek.",{
                messageType:ToastrMessageType.Warning,
                position:ToastrPosition.TopFullWidth
              })
              break;
            case HttpStatusCode.NotFound:
              this.toastrService.message("sayfa Bulunamadi","404 Hatasi",{
                messageType:ToastrMessageType.Warning,
                position:ToastrPosition.TopFullWidth
              })
              break;
            default:
              this.toastrService.message("Beklenmeyen bir hata meydana gelmistir.","Hata!",{
                messageType:ToastrMessageType.Warning,
                position:ToastrPosition.TopFullWidth
              })
              break;
          }
          return of(err);
        }));
    }
}
