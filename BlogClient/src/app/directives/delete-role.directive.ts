import { Directive, ElementRef, EventEmitter, HostListener, Input, Output } from '@angular/core';
import { RoleService } from '../services/admin/role.service';
import { BaseComponent, SpinnerType } from '../base/base.component';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertifyService, MessageType, PositionType } from '../services/alertify.service';
import { UserService } from '../services/user/user.service';

declare var $ : any;

@Directive({
  selector: '[appDeleteRole]'
})
export class DeleteRoleDirective extends BaseComponent {

  constructor(private element:ElementRef,private roleService : RoleService,spinner:NgxSpinnerService,private alertifyService : AlertifyService,private userService:UserService) { 
    super(spinner);
  }

  @Input() appDeleteRole:string;
  @Output() newRoles:EventEmitter<any> = new EventEmitter();

  @HostListener("click")
  async onClick(){
    
    if(this.appDeleteRole == "roles"){
      var response : any = await this.roleService.delete(this.element.nativeElement.parentElement.parentElement.children[1].innerHTML,()=>this.hideSpinner(SpinnerType.ballNewtonCradle),()=>this.hideSpinner(SpinnerType.ballNewtonCradle));
      if(response.succeeded){
        $(this.element.nativeElement.parentElement.parentElement).fadeOut(500,()=>{this.newRoles.emit();});
        this.alertifyService.message("Rol başarı ile silindi.",MessageType.success,PositionType.bottomCenter);
      }else{
        this.alertifyService.message("Rol silinemedi.",MessageType.error,PositionType.bottomCenter);
      }
    }
    
  }

}
