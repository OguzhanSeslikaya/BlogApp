import { Inject, inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { AlertifyService, MessageType, PositionType } from '../services/alertify.service';
import { _hasRole, _isAuthenticated } from '../services/user/auth.service';


export const authGuard: CanActivateFn = (route:ActivatedRouteSnapshot, state:RouterStateSnapshot) => {
  // const jwtHelper : JwtHelperService = inject(JwtHelperService);
  const router : Router = inject(Router);
  const alertifyService = inject(AlertifyService);
  
  
  if(!_hasRole){
    // router.navigate(["log-in"],{queryParams:{returnUrl:state.url}});
    alertifyService.message("Bu sayfaya erişmek için yetkinizin olması gerekiyor.",MessageType.warning,PositionType.bottomCenter);
    return false;
  }
  return true;
};
