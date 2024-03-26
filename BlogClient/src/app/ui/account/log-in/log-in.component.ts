import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { lastValueFrom } from 'rxjs';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { FormsService } from 'src/app/services/account/forms.service';
import { AlertifyService, MessageType, PositionType } from 'src/app/services/alertify.service';
import { SignalrService } from 'src/app/services/common/signalr.service';
import { AuthService, _isAuthenticated } from 'src/app/services/user/auth.service';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.scss']
})


export class LogInComponent extends BaseComponent {
  constructor(private formBuilder:FormBuilder,private formsService : FormsService ,spinner : NgxSpinnerService,private alertifyService : AlertifyService,
    public authService : AuthService, private activatedRoute : ActivatedRoute,private router : Router){
    super(spinner);
   
  }

  private formData : FormData;
  girisForm : FormGroup = this.formBuilder.group({
    kullaniciAdi : ["",Validators.required],
    parola : ["",Validators.required]  
  });

  async giris(){
    if(!this.girisForm.valid){
      this.alertifyService.message("Kullanıcı adı veya şifre boş geçilemez.",MessageType.warning,PositionType.bottomCenter,3);
    }
    else{
    this.showSpinner(SpinnerType.ballNewtonCradle);
    this.formData = new FormData();
    this.formData.append("kullaniciAdi",this.girisForm.value.kullaniciAdi);
    this.formData.append("parola",this.girisForm.value.parola);
  
    var response : any = await this.formsService.girisYap(this.formData,()=>{
      this.hideSpinner(SpinnerType.ballNewtonCradle);
    },()=>{
      this.hideSpinner(SpinnerType.ballNewtonCradle);
    });
    if(response.basariDurum){
      localStorage.setItem("accessToken",response.token.accessToken);
      localStorage.setItem("refreshToken",response.token.refreshToken);
      this.alertifyService.message(response.mesaj,MessageType.success,PositionType.bottomCenter)
    }else{
      this.alertifyService.message(response.mesaj,MessageType.warning,PositionType.bottomCenter,5)
    }
    this.authService.identityCheck();
    
    if(_isAuthenticated){
      this.authService.hasRoleCheck(this.girisForm.value.kullaniciAdi);
    this.activatedRoute.queryParams.subscribe(params => {if(params["returnUrl"])this.router.navigate([params["returnUrl"]]);else this.router.navigate([""])});
  }

  }
  }

}
