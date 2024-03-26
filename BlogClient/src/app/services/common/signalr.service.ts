import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder, HubConnectionState } from '@microsoft/signalr';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {

  private _connection : HubConnection;

  get connection() : HubConnection {
    return this._connection;
  }

  constructor() {

   }

   start(url : string){
    if(!this.connection || this._connection?.state == HubConnectionState.Disconnected){
      const builder : HubConnectionBuilder = new HubConnectionBuilder();

      const hubConnection : HubConnection = builder.withUrl(url).withAutomaticReconnect().build();

      hubConnection.start().then(()=>
      {
        console.log("connected");
        
      }).catch(error => setTimeout(() => 
      {
        this.start(url)
      }, 2000));
      this._connection = hubConnection;
    }


   }
   
   invoke(methodName:string, message:any, successCallBack?:(value)=>void,errorCallBack?:(error)=> void){
    this.connection.invoke(methodName,message)
      .then(successCallBack)
      .catch(errorCallBack)
   }

   on(methodName:string, callBack:(...message:any)=>void){
    this.connection.on(methodName,callBack);
   }

}
