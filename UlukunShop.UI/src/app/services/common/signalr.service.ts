import {Injectable} from '@angular/core';
import {HubConnection, HubConnectionBuilder, HubConnectionState} from "@microsoft/signalr";
import {error} from "@angular/compiler-cli/src/transformers/util";

@Injectable({
  providedIn: 'root'
})
export class SignalrService {


  private _connection: HubConnection;

  get connection(): HubConnection {
    return this._connection;
  }

  start(hubUrl: string) {
    if (!this.connection || this._connection?.state == HubConnectionState.Disconnected) {
      const builder: HubConnectionBuilder = new HubConnectionBuilder();

      const hubConnection: HubConnection = builder.withUrl(hubUrl).withAutomaticReconnect().build();

      hubConnection.start().then(() => {
        console.log("Connection started");
      }).catch(error => setTimeout(() => this.start(hubUrl), 2000));

      //buraya sonradan tekrar BAK!!
      this._connection = hubConnection;

    }

    this._connection.onreconnected(connectionId => {
      console.log("Connection reconnected");
    });

    this._connection.onreconnecting(error1 => console.log("reconnecting"));
    this._connection.onclose(error1 => {
      console.log("close reconnection")
    });
  }

  invoke(procedureName: string, message: any, successCallback: (value) => void, errorCallback: (error) => void) {
    this.connection.invoke(procedureName, message).then(successCallback).catch(errorCallback);
  }

  on(procedureName: string, callback: (...message) => void) {
    this.connection.on(procedureName, callback);
  }
}
