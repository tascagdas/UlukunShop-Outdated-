import {Injectable} from '@angular/core';
import {firstValueFrom, Observable} from "rxjs";
import {TokenResponse} from "../../../contracts/Token/tokenResponse";
import {CustomToastrService, ToastrMessageType, ToastrPosition} from "../../ui/custom-toastr.service";
import {SocialUser} from "@abacritt/angularx-social-login";
import {HttpClientService} from "../http-client.service";

@Injectable({
  providedIn: 'root'
})
export class UserAuthService {

  constructor(private _httpClientService: HttpClientService, private toastr: CustomToastrService) {
  }

  async login(userNameOrEmail: string, password: string, callBackFunction?: () => void): Promise<any> {
    const observable: Observable<any | TokenResponse> = this._httpClientService.post<any | TokenResponse>({
      controller: "auth",
      action: "login"
    }, {
      userNameOrEmail,
      password
    })
    const tokenResponse: TokenResponse = await firstValueFrom(observable) as TokenResponse;
    if (tokenResponse) {
      localStorage.setItem("accessToken", tokenResponse.token.accessToken);
      localStorage.setItem("refreshToken", tokenResponse.token.refreshToken);

      this.toastr.message("Kullanici girisi basarili !", "Basarili", {
        messageType: ToastrMessageType.Success,
        position: ToastrPosition.TopRight
      });
    }
    callBackFunction();
  }

  async refreshTokenLogin(refreshToken: string, callBackFunction?: (state) => void) {
    const observable: Observable<any | TokenResponse> = this._httpClientService.post({
      controller: "auth",
      action: "refreshtokenlogin"
    }, {refreshToken: refreshToken});
    try {
      const tokenResponse: TokenResponse = await firstValueFrom(observable) as TokenResponse;
      if (tokenResponse) {
        localStorage.setItem("accessToken", tokenResponse.token.accessToken);
        localStorage.setItem("refreshToken", tokenResponse.token.refreshToken);

      }

      callBackFunction(tokenResponse ? true : false);
    } catch {
      callBackFunction(false)
    }
  }

  async googleLogin(user: SocialUser, callBackFunction?: () => void): Promise<any> {
    const observable: Observable<SocialUser | TokenResponse> =
      this._httpClientService.post<SocialUser | TokenResponse>({
        action: "google-login",
        controller: "auth"
      }, user);

    const tokenResponse: TokenResponse = await firstValueFrom(observable) as TokenResponse;
    if (tokenResponse) {
      localStorage.setItem("accessToken", tokenResponse.token.accessToken);
      localStorage.setItem("refreshToken", tokenResponse.token.refreshToken);
      this.toastr.message("Google ile giris basarılı", "Gırıs basarılı", {
        messageType: ToastrMessageType.Success,
        position: ToastrPosition.TopRight
      })
    }
    callBackFunction();
  }

  async facebookLogin(user: SocialUser, callBackFunction?: () => void): Promise<any> {
    const observable: Observable<SocialUser | TokenResponse> = this._httpClientService.post<SocialUser | TokenResponse>({
      controller: "auth",
      action: "facebook-login"
    }, user);

    const tokenResponse: TokenResponse = await firstValueFrom(observable) as TokenResponse;

    if (tokenResponse) {
      localStorage.setItem("accessToken", tokenResponse.token.accessToken);
      localStorage.setItem("refreshToken", tokenResponse.token.refreshToken);

      this.toastr.message("Facebook Login başarıyla sağlanmıştır.", "Giriş Başarılı", {
        messageType: ToastrMessageType.Success,
        position: ToastrPosition.TopRight
      })
    }

    callBackFunction();
  }

  async passwordReset(email: string, callBackFunction?: () => void) {
    const observable: Observable<any> = this._httpClientService.post({
      controller: "auth",
      action: "password-reset"
    }, { email: email });

    await firstValueFrom(observable);
    callBackFunction();
  }

  async verifyResetToken(resetToken: string, userId: string, callBackFunction?: () => void): Promise<boolean> {
    const observable: Observable<any> = this._httpClientService.post({
      controller: "auth",
      action: "verify-reset-token"
    }, {
      resetToken: resetToken,
      userId: userId
    });

    const state: boolean = await firstValueFrom(observable);
    callBackFunction();
    return state;
  }
}
