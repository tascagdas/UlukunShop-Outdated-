import {Injectable} from '@angular/core';
import {HttpClientService} from "../http-client.service";
import {User} from "../../../entities/user";
import {Create_User} from "../../../contracts/users/create_user";
import {firstValueFrom, Observable} from "rxjs";
import {CustomToastrService, ToastrMessageType, ToastrPosition} from "../../ui/custom-toastr.service";
import {TokenResponse} from "../../../contracts/Token/tokenResponse";
import {SocialUser} from "@abacritt/angularx-social-login";

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private _httpClientService: HttpClientService, private toastr: CustomToastrService) {}

  async create(user: User): Promise<Create_User> {
    const observable: Observable<Create_User | User> = this._httpClientService.post<Create_User | User>({
      controller: "users"
    }, user);

    return await firstValueFrom(observable) as Create_User;
  }



}