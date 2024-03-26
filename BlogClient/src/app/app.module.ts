import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AccountModule } from './ui/account/account.module';
import { UserUiModule } from './ui/user-ui/user-ui.module';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { JwtModule } from '@auth0/angular-jwt';
import { AdminModule } from './ui/admin/admin.module';
import { DeleteUserDirective } from './directives/delete-user.directive';
import { HttpErrorHandlerInterceptorService } from './services/http-error-handler-interceptor.service';

@NgModule({
  declarations: [
    AppComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AccountModule,
    UserUiModule,
    HttpClientModule,
    NgxSpinnerModule,
    BrowserAnimationsModule,
    AdminModule,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => localStorage.getItem("accessToken"),
        allowedDomains: ["localhost:7251"]
      }
    })
  ],
  providers: [{provide:"baseUrl",useValue:"https://localhost:7251/api/", multi: true},{provide:HTTP_INTERCEPTORS,useClass:HttpErrorHandlerInterceptorService,multi:true}],
  bootstrap: [AppComponent]
})
export class AppModule { }
