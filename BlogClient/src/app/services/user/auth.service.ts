import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { UserService } from './user.service';
import { RoleService } from '../admin/role.service';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  constructor(private jwtHelper:JwtHelperService,private roleService : RoleService) { }

  identityCheck(){
    let expired : boolean;
    const token = localStorage.getItem("accessToken");
    try{
      expired = this.jwtHelper.isTokenExpired(token);
    }catch{
      _hasRole = false;
      expired = true;
    }
    if(token == null){
      _isAuthenticated = false;
      _hasRole = false;
    }else{
      _isAuthenticated = !expired;
    }
    
  }

  get isAuthenticated():boolean{
    return _isAuthenticated;
  }

  async hasRoleCheck(userName:string){
    var response : any = await this.roleService.getRoleByUserName(userName,()=>console.log(),()=>console.log("roleCheckError"));
    if(response.roleName){
      _hasRole = true;
      console.log("1");
    }else{
      _hasRole = false;
      console.log("2");
    }
  }

  get hasRole():boolean{
    return _hasRole;
  }

}

export let _isAuthenticated : boolean = false;

export let _hasRole : boolean = false;