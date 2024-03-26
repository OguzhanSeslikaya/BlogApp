import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { FormsService } from 'src/app/services/account/forms.service';
import { AlertifyService, MessageType, PositionType } from 'src/app/services/alertify.service';
import { AuthService, _isAuthenticated } from 'src/app/services/user/auth.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent extends BaseComponent {
private formData : FormData;

constructor(private formBuilder:FormBuilder,private formsService : FormsService ,spinner : NgxSpinnerService,private alertifyService : AlertifyService,
  public authService : AuthService,private router : Router){
  super(spinner);
}
kayitForm : FormGroup = this.formBuilder.group({
  kullaniciAdi : ["",Validators.required],
  parola : ["",Validators.required]
});
async kayit(){
  if(!this.kayitForm.valid){
    this.alertifyService.message("Kullanıcı adı veya şifre boş geçilemez.",MessageType.warning,PositionType.bottomCenter,3);
  }
  else{
  this.showSpinner(SpinnerType.ballNewtonCradle);
  this.formData = new FormData();
  this.formData.append("kullaniciAdi",this.kayitForm.value.kullaniciAdi);
  this.formData.append("parola",this.kayitForm.value.parola);

  var response : any = await this.formsService.kayitOl(this.formData,()=>{
    this.hideSpinner(SpinnerType.ballNewtonCradle);
  },()=>{
    this.hideSpinner(SpinnerType.ballNewtonCradle);
  });
  if(response.basariDurum){
    localStorage.setItem("accessToken",response.token.accessToken);
    this.alertifyService.message(response.mesaj,MessageType.success,PositionType.bottomCenter)
  }else{
    this.alertifyService.message(response.mesaj,MessageType.warning,PositionType.bottomCenter,8)
  }
this.authService.identityCheck();
    if(_isAuthenticated){
    this.router.navigate([""]);
  }
}
}
}
