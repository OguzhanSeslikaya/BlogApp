import { Directive, ElementRef, EventEmitter, HostListener, Input, Output } from '@angular/core';
import { UserService } from '../services/user/user.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertifyService, MessageType, PositionType } from '../services/alertify.service';
import { BaseComponent, SpinnerType } from '../base/base.component';

declare var $ : any;

@Directive({
  selector: '[appDeleteUser]'
})
export class DeleteUserDirective extends BaseComponent{

  constructor(private element:ElementRef,spinner:NgxSpinnerService,private alertifyService : AlertifyService,private userService:UserService) { 
    super(spinner);
  }
  @Input() appDeleteUser:string;
  @Output() newUsers:EventEmitter<any> = new EventEmitter();

  @HostListener("click")
  async onClick(){
  if (this.appDeleteUser == "users"){
    var response : any = await this.userService.delete(this.element.nativeElement.parentElement.parentElement.children[0].innerHTML,()=>this.hideSpinner(SpinnerType.ballNewtonCradle),()=>this.hideSpinner(SpinnerType.ballNewtonCradle));
    if(response.succeeded){
      $(this.element.nativeElement.parentElement.parentElement).fadeOut(500,()=>{this.newUsers.emit();});
      this.alertifyService.message("User başarı ile silindi.",MessageType.success,PositionType.bottomCenter);
    }else{
      this.alertifyService.message("User silinemedi.",MessageType.error,PositionType.bottomCenter);
    }
  }
}
}
