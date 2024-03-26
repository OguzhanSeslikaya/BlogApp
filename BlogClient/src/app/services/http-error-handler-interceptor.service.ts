import { AlertifyService, MessageType, PositionType } from 'src/app/services/alertify.service';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpStatusCode } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, of } from 'rxjs';
import { BaseComponent, SpinnerType } from '../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { UserService } from './user/user.service';

@Injectable({
  providedIn: 'root'
})
export class HttpErrorHandlerInterceptorService extends BaseComponent implements HttpInterceptor {

  constructor(private userService:UserService,private alertifyService:AlertifyService,spinner : NgxSpinnerService) { super(spinner); }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    
    return next.handle(req).pipe(catchError(error => {
      this.hideSpinner(SpinnerType.ballNewtonCradle);
      switch(error.status){
        case HttpStatusCode.Unauthorized:
          ()=>this.alertifyService.message("Yetkisiz erişim hatası.",MessageType.warning,PositionType.bottomCenter);
          this.userService.refreshToken().then(data => true);
          //burası bitmedi yetkisiz erişimde yeni tokenin gelmesi beklenip o sayfaya yönlendirilecek süresi falan bitmişse ana sayfaya yönlendirebilir.
          break;
        case HttpStatusCode.InternalServerError:
          this.alertifyService.message("Sistemsel hata lütfen yetkili ile iletişime geçin.",MessageType.warning,PositionType.bottomCenter);
          break;
        case 0:
          this.alertifyService.message("Sistemsel hata lütfen yetkili ile iletişime geçin.",MessageType.warning,PositionType.bottomCenter);
          break;
      }
      return of(error);
    }));
  }
}
