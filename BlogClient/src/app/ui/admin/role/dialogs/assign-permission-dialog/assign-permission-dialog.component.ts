import { Component, Inject, Input, Optional } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { BaseComponent, SpinnerType } from 'src/app/base/base.component';
import { Menu } from 'src/app/contracts/menu';
import { ApplicationService } from 'src/app/services/admin/application.service';

import { FormsModule, FormBuilder, FormGroup, FormControl } from '@angular/forms';

import { ListRolesComponent } from '../../list-roles/list-roles.component';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-assign-permission-dialog',
  templateUrl: './assign-permission-dialog.component.html',
  styleUrls: ['./assign-permission-dialog.component.scss']
})
export class AssignPermissionDialogComponent extends BaseComponent {
  public request : any;
  private controls : any = {};
  public result : string[] = [];
  constructor(private applicationService : ApplicationService , spinner : NgxSpinnerService,private formBuilder:FormBuilder,@Optional()@Inject(MAT_DIALOG_DATA) private id: string){
    super(spinner);
  }
  
  permissionForm : FormGroup = this.formBuilder.group({});
   jsonConcat(o1, o2,accessible:boolean) {
     o1[o2.code] = new FormControl(accessible);
    return o1;
   }

  async ngOnInit(){
    this.showSpinner(SpinnerType.ballNewtonCradle);
    this.request = await this.applicationService.getAuthorizeDefinitionEndpoints(this.id,()=>this.hideSpinner(SpinnerType.ballNewtonCradle),()=>this.hideSpinner(SpinnerType.ballNewtonCradle));
    this.controls = {};
    for (let i of this.request.menus) {
      for (let k of i.actions) {
        let accessible : boolean = false
        for (let eCode of this.request.authorizeEndpoints) {
          if(k.code == eCode){
            accessible = true;
          }
        }
        this.controls = this.jsonConcat(this.controls, k,accessible);
      }
    }
    this.permissionForm.controls = this.controls;
  }


  resultDoldur(){
    let secilenler = [];
    for (const i of this.request.menus) {
      for (const k of i.actions) {
        if(this.permissionForm.get(k.code).value){
          secilenler[secilenler.length] = k.code;
        }
      }
    }
    this.result = secilenler;
  }

}

