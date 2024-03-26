import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient : HttpClientService) { }

  async getUsers(successCallBack : Function , errorCallBack : Function){
    var data = lastValueFrom(this.httpClient.get("users"));
    await data.then(()=> successCallBack() ).catch(() => errorCallBack());
    return data;
  }

  async assignUserToRole(userId:string,roleName:string,successCallBack:Function,errorCallBack:Function){
    var data = lastValueFrom(this.httpClient.post("users/assignRoleToUser",{userId:userId,roleName:roleName}));
    await data.then(()=> successCallBack() ).catch(() => errorCallBack());
    return data;
  }

  async refreshToken(){
    var data : any = lastValueFrom(this.httpClient.post("users/refreshToken",{refreshToken:localStorage.getItem("refreshToken")}));
    data.then(data=>{
      localStorage.setItem("accessToken",data.token.accessToken);
      localStorage.setItem("refreshToken",data.token.refreshToken);
    } ).catch();
    
    return data
  }

  async createPage(formData:FormData,successCallBack:Function,errorCallBack:Function){
    var data = lastValueFrom(this.httpClient.post("page",formData));
    await data.then(()=> successCallBack() ).catch(() => errorCallBack());
    return data;
  }

  async delete(id:string,successCallBack:Function,errorCallBack:Function){
    var data = lastValueFrom(this.httpClient.delete("users",{id:id}));
    await data.then(()=> successCallBack() ).catch(() => errorCallBack());
    return data;
  }

}
