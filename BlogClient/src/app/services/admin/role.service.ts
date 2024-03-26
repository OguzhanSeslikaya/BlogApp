import { Injectable } from '@angular/core';
import { HttpClientService } from '../http-client.service';
import { lastValueFrom } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

  constructor(private httpClientService : HttpClientService) { }

  async getRoleByUserName(userName:string,successCallBack:Function,errorCallBack:Function){
    const response = lastValueFrom(this.httpClientService.get("users/getRoleByUserName/"+userName));
    await response.then(()=>successCallBack()).catch(()=>errorCallBack());
    return response
  }
  async create(name : string,successCallBack:Function,errorCallBack:Function){
    const response = lastValueFrom(this.httpClientService.post("Roles", {name:name}));
    await response.then(()=>successCallBack()).catch(()=>errorCallBack());
    return response
  }
  async getRoles(successCallBack:Function,errorCallBack:Function){
    const response = lastValueFrom(this.httpClientService.get("Roles"));
    await response.then(() => successCallBack()).catch(()=>errorCallBack());
    return response;
  }
  async delete(name:string,successCallBack:Function,errorCallBack:Function){
    const response = lastValueFrom(this.httpClientService.delete("roles/deleteRole",{name:name}));
    await response.then(()=>successCallBack()).catch(()=>errorCallBack());
    return response;
  }
  async assignRoleToEndpoints(id:string,roleEndpoints:string[],successCallBack:Function,errorCallBack:Function){
    const response = lastValueFrom(this.httpClientService.post("AuthorizeEndpoints/assignRole",{roleId:id,endPointsCode:roleEndpoints}));
    await response.then(()=>successCallBack()).catch(()=>errorCallBack());
    return response;
  }
}
