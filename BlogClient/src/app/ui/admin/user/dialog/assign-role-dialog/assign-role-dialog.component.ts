import { RoleService } from './../../../../../services/admin/role.service';
import { Component, Inject, Optional } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { UserService } from 'src/app/services/user/user.service';

@Component({
  selector: 'app-assign-role-dialog',
  templateUrl: './assign-role-dialog.component.html',
  styleUrls: ['./assign-role-dialog.component.scss']
})
export class AssignRoleDialogComponent extends BaseComponent {
  public request : any;
  public result : string;
  constructor(private userService : UserService,private roleService : RoleService , spinner : NgxSpinnerService,private formBuilder:FormBuilder){
    super(spinner);
  }
  
  roleForm : FormGroup = this.formBuilder.group({
    role : ["",Validators.required]
  });
   

  async ngOnInit(){
    this.showSpinner(SpinnerType.ballNewtonCradle);
    this.request = await this.roleService.getRoles(()=>this.hideSpinner(SpinnerType.ballNewtonCradle),()=>this.hideSpinner(SpinnerType.ballNewtonCradle));
  }


  resultDoldur(){
    if(this.roleForm.valid){
      this.result = this.roleForm.get("role").value;
    }
  }
}
