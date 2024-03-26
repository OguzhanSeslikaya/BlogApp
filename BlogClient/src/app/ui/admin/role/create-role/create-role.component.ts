import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { RoleService } from 'src/app/services/admin/role.service';
import { AlertifyService, MessageType, PositionType } from 'src/app/services/alertify.service';

@Component({
  selector: 'app-create-role',
  templateUrl: './create-role.component.html',
  styleUrls: ['./create-role.component.scss']
})
export class CreateRoleComponent extends BaseComponent{
  constructor(private formBuilder : FormBuilder,spinner : NgxSpinnerService,private alertifyService:AlertifyService,private roleService : RoleService){
    super(spinner);
  }

  rolForm : FormGroup = this.formBuilder.group(
    {
      rolName : ["",Validators.required],
    }
  );

  async rolOlustur(){
    if(!this.rolForm.valid){
      this.alertifyService.message("Rol ismi boş geçilemez.",MessageType.error,PositionType.bottomCenter);
    }else{
      this.showSpinner(SpinnerType.ballNewtonCradle);
      var response : any = await this.roleService.create(this.rolForm.value.rolName,()=>this.hideSpinner(SpinnerType.ballNewtonCradle),()=>{this.hideSpinner(SpinnerType.ballNewtonCradle);this.alertifyService.message("Sistemsel hata yetkili ile iletişime geçiniz.",MessageType.error,PositionType.bottomCenter)});
      if(response.succeeded){
        this.alertifyService.message("Rol başarı ile oluşturuldu.",MessageType.success,PositionType.bottomCenter);
      }else{
        this.alertifyService.message("Rol oluşturulamadı.",MessageType.error,PositionType.bottomCenter);
      }
    }
  }
}
