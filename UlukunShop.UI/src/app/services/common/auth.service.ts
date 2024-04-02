import { Injectable } from '@angular/core';
import {JwtHelperService} from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private jwtHelper:JwtHelperService) { }


  identityCheck(){
    const token: string = localStorage.getItem('accessToken');

    let isExpired: boolean;
    try {
      isExpired = this.jwtHelper.isTokenExpired(token);
    } catch {
      isExpired = true;
    }

    isAuthenticated=token!=null&&!isExpired;
  }
  get _isAuthenticated():boolean{
    return isAuthenticated;
  }
}


export let isAuthenticated :boolean;
