import {Inject, Injectable} from '@angular/core';
import {HubConnection, HubConnectionBuilder, HubConnectionState} from "@microsoft/signalr";
import {error} from "@angular/compiler-cli/src/transformers/util";

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  constructor(@Inject("baseSignalRUrl") private baseSignalRUrl: string) { }



  start(hubUrl: string) {
    hubUrl = this.baseSignalRUrl + hubUrl;
      const builder: HubConnectionBuilder = new HubConnectionBuilder();

      const hubConnection: HubConnection = builder.withUrl(hubUrl).withAutomaticReconnect().build();

      hubConnection.start().then(() => {
        console.log("Connection started");
      }).catch(error => setTimeout(() => this.start(hubUrl), 2000));

      //buraya sonradan tekrar BAK!!


    hubConnection.onreconnected(connectionId => {
      console.log("Connection reconnected");
    });

    hubConnection.onreconnecting(error1 => console.log("reconnecting"));
    hubConnection.onclose(error1 => {
      console.log("close reconnection")
    });
    return hubConnection;
  }

  //SignalR çoklu bağlantı sağlandı. artık ioc ye giderken singleton olarak gitmiyor. daha önceden o şekildeydi. önceki commitlerden incelenebilir.

  invoke(hubURL:string,procedureName: string, message: any, successCallback: (value) => void, errorCallback: (error) => void) {
    this.start(hubURL).invoke(procedureName, message).then(successCallback).catch(errorCallback);
  }

  on(hubURL:string,procedureName: string, callback: (...message) => void) {
    this.start(hubURL).on(procedureName, callback);
  }
}
