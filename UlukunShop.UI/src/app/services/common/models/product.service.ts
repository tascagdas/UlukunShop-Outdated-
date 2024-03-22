import { Injectable } from '@angular/core';
import {HttpClientService} from "../http-client.service";

@Injectable({
  providedIn: 'root'
})
export class ProductService {

  constructor(private _httpClientService:HttpClientService) { }
  create(){

  }
}
