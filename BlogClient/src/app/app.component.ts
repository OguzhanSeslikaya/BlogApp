import { JwtHelperService } from '@auth0/angular-jwt';
import { AlertifyService, MessageType, PositionType } from './services/alertify.service';
import { Component, OnInit } from '@angular/core';
import { AuthService } from './services/user/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import { SignalrService } from './services/common/signalr.service';

declare var $ : any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'BlogClient';
  constructor(public authService : AuthService,private aletrifyService : AlertifyService,private router : Router,private jwt:JwtHelperService
    , private signalRService : SignalrService){
    signalRService.start("https://localhost:7251/daily-logins-hub");
    authService.identityCheck();
    if(localStorage.getItem("accessToken")){
      authService.hasRoleCheck(jwt.decodeToken(localStorage.getItem("accessToken"))["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]);
    }
  }

  ngOnInit(): void {
    $( "nav div #dr" ).on( "click", function() {
      $( "nav div .dropdown #dcr" ).slideToggle( 200 );
    });
    this.signalRService.on("newLoginMessage", message =>alert(message));
  }

  signOut(){
    localStorage.removeItem("accessToken");
    this.authService.identityCheck();
    this.router.navigate([""]);
    this.aletrifyService.message("Çıkış işlemi gerçekleşti.",MessageType.warning,PositionType.bottomCenter);
    
  }

}

