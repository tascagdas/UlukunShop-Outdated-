import {Injectable} from '@angular/core';
import {HttpClientService} from "../http-client.service";
import {User} from "../../../entities/user";
import {Create_User} from "../../../contracts/users/create_user";
import {firstValueFrom, Observable} from "rxjs";
import {Token} from "../../../contracts/Token/token";
import {CustomToastrService, ToastrMessageType, ToastrPosition} from "../../ui/custom-toastr.service";
import {TokenResponse} from "../../../contracts/Token/tokenResponse";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private _httpClientService: HttpClientService, private toastr:CustomToastrService) {
  }

  async create(user: User):Promise<Create_User> {
    const observable: Observable<Create_User | User> = this._httpClientService.post<Create_User | User>({
      controller: "users"
    }, user);

    return await firstValueFrom(observable) as Create_User;
  }

  async login(userNameOrEmail: string, password: string,callBackFunction?:()=>void):Promise<any> {
   const observable:Observable<any|TokenResponse>= this._httpClientService.post<any|TokenResponse>({
      controller: "users",
      action: "login"
    },{
      userNameOrEmail,
      password
    })
    const tokenResponse:TokenResponse=await firstValueFrom(observable) as TokenResponse;
   if (tokenResponse){
     localStorage.setItem("accessToken",tokenResponse.token.accessToken);

      this.toastr.message("Kullanici girisi basarili !","Basarili",{
        messageType:ToastrMessageType.Success,
        position:ToastrPosition.TopRight
      });
   }
   callBackFunction();
  }
}
